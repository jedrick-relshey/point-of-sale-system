Option Strict On
Option Explicit On

Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions

'=====================================================================
' DATASTORE_MESSAGING.VB  -  NEW FILE (second half of the DataStore module)
'
' One conversation per cashier. No change to the ChatMessage class or to
' messages.txt: the cashier's FULL NAME is stored in the existing fields.
'
'   cashier -> admin :  Sender = cashier name        Receiver = "Admin"
'   admin   -> cashier: Sender = signed-in admin     Receiver = cashier name
'
' Old messages (Sender "Cashier" / "Admin", Receiver "Admin" / "Cashier") still load:
'   - old cashier messages that start with "[Stock adjustment] Name:" go to that cashier
'   - old admin messages with no named cashier (Receiver "Cashier") are NOT shown in any chat any more,
'     so a newly created cashier always starts with an empty conversation (the lines stay in messages.txt)
'
' Read / unread is kept in message_reads.txt (one line per reader + cashier).
'=====================================================================
Partial Public Module DataStore

    ''' <summary>Raised when someone opens a conversation and its unread state changes.</summary>
    Public Event ReadStatesChanged As EventHandler

    Private ReadOnly readAtByKey As New Dictionary(Of String, DateTime)(StringComparer.OrdinalIgnoreCase)
    Private readStatesLoaded As Boolean = False

    '------------------------------------------------------------------
    ' who sent it / which conversation does it belong to
    '------------------------------------------------------------------
    Private Function IsCashierName(name As String) As Boolean
        If String.IsNullOrWhiteSpace(name) Then Return False
        For Each acc As CashierAccount In Cashiers.ToList()
            If String.Equals(acc.FullName, name.Trim(), StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        Return False
    End Function

    ''' <summary>True when the message was written by a cashier (false = written by the admin).</summary>
    Public Function IsFromCashier(m As ChatMessage) As Boolean
        If String.Equals(If(m.Receiver, "").Trim(), "Admin", StringComparison.OrdinalIgnoreCase) Then Return True
        If String.Equals(If(m.Sender, "").Trim(), "Cashier", StringComparison.OrdinalIgnoreCase) Then Return True
        Return IsCashierName(m.Sender)
    End Function

    ''' <summary>
    ''' The cashier whose conversation this message belongs to.
    ''' "*" = old admin message with no cashier (shown to everyone), "" = cannot tell.
    ''' </summary>
    Public Function ThreadOwnerOf(m As ChatMessage) As String
        If IsFromCashier(m) Then
            If IsCashierName(m.Sender) Then Return m.Sender.Trim()
            Dim hit As Match = Regex.Match(If(m.Message, ""), "^\s*\[[^\]]+\]\s*(.+?)\s*:")
            If hit.Success Then Return hit.Groups(1).Value.Trim()
            Return ""
        End If

        Dim target As String = If(m.Receiver, "").Trim()
        If target = "" OrElse String.Equals(target, "Cashier", StringComparison.OrdinalIgnoreCase) Then Return "*"
        Return target
    End Function

    ''' <summary>All messages of one cashier's conversation, oldest first. Old broadcast messages are only included when includeBroadcast = True (default False).</summary>
    Public Function ThreadMessages(cashierName As String, Optional includeBroadcast As Boolean = False) As List(Of ChatMessage)
        Dim result As New List(Of ChatMessage)()
        If String.IsNullOrWhiteSpace(cashierName) Then Return result

        For Each m As ChatMessage In ChatMessages.ToList()
            Dim owner As String = ThreadOwnerOf(m)
            If owner = "*" Then
                If includeBroadcast Then result.Add(m)
            ElseIf String.Equals(owner, cashierName.Trim(), StringComparison.OrdinalIgnoreCase) Then
                result.Add(m)
            End If
        Next
        Return result.OrderBy(Function(x) x.TimeSent).ToList()
    End Function

    ''' <summary>Newest message of the conversation (not counting old broadcast messages), or Nothing.</summary>
    Public Function ThreadLast(cashierName As String) As ChatMessage
        Dim list As List(Of ChatMessage) = ThreadMessages(cashierName, False)
        If list.Count = 0 Then Return Nothing
        Return list(list.Count - 1)
    End Function

    '------------------------------------------------------------------
    ' read / unread
    '------------------------------------------------------------------
    Private Function ReadKey(asAdmin As Boolean, cashierName As String) As String
        Return If(asAdmin, "A:", "C:") & SafeField(cashierName)
    End Function

    Private Sub EnsureReadStatesLoaded()
        If readStatesLoaded Then Return
        readStatesLoaded = True
        Try
            Dim filePath As String = FilePathOf("message_reads.txt")
            If Not File.Exists(filePath) Then Return
            For Each line As String In File.ReadAllLines(filePath, Encoding.UTF8)
                Dim f As String() = Fields(line)
                Dim ticks As Long
                If f.Length >= 2 AndAlso Long.TryParse(f(1), NumberStyles.Integer, CultureInfo.InvariantCulture, ticks) Then
                    readAtByKey(f(0)) = New DateTime(ticks)
                End If
            Next
        Catch ex As Exception
            ' unreadable file: everything simply counts as unread
        End Try
    End Sub

    Private Sub SaveReadStates()
        Try
            File.WriteAllLines(FilePathOf("message_reads.txt"),
                               readAtByKey.Select(Function(kv) SafeField(kv.Key) & "|" & kv.Value.Ticks.ToString(CultureInfo.InvariantCulture)).ToArray(),
                               Encoding.UTF8)
        Catch ex As Exception
            ' not being able to save read marks must never break chatting
        End Try
    End Sub

    Private Function ReadAtOf(asAdmin As Boolean, cashierName As String) As DateTime
        EnsureReadStatesLoaded()
        Dim t As DateTime
        If readAtByKey.TryGetValue(ReadKey(asAdmin, cashierName), t) Then Return t
        Return DateTime.MinValue
    End Function

    ''' <summary>When the cashier last opened the chat (used for the "Read" mark on admin messages).</summary>
    Public Function CashierReadAt(cashierName As String) As DateTime
        Return ReadAtOf(False, cashierName)
    End Function

    ''' <summary>When the admin last opened this cashier's chat (used for the "Read" mark on the cashier's messages).</summary>
    Public Function AdminReadAt(cashierName As String) As DateTime
        Return ReadAtOf(True, cashierName)
    End Function

    ''' <summary>Messages from this cashier that the admin has not opened yet.</summary>
    Public Function UnreadForAdmin(cashierName As String) As Integer
        Dim readAt As DateTime = ReadAtOf(True, cashierName)
        Dim n As Integer = 0
        For Each m As ChatMessage In ThreadMessages(cashierName, False)
            If IsFromCashier(m) AndAlso m.TimeSent > readAt Then n += 1
        Next
        Return n
    End Function

    ''' <summary>Unread messages from all cashiers (the badge on the Messages menu).</summary>
    Public Function TotalUnreadForAdmin() As Integer
        EnsureReadStatesLoaded()
        Dim n As Integer = 0
        For Each m As ChatMessage In ChatMessages.ToList()
            If Not IsFromCashier(m) Then Continue For
            Dim owner As String = ThreadOwnerOf(m)
            If owner = "" OrElse owner = "*" Then Continue For
            If m.TimeSent > ReadAtOf(True, owner) Then n += 1
        Next
        Return n
    End Function

    ''' <summary>Marks the conversation as opened by the admin (asAdmin = True) or by the cashier.</summary>
    Public Sub MarkThreadRead(asAdmin As Boolean, cashierName As String)
        If String.IsNullOrWhiteSpace(cashierName) Then Return
        EnsureReadStatesLoaded()

        ' newest message that the reader has to look at
        Dim latest As DateTime = DateTime.MinValue
        For Each m As ChatMessage In ThreadMessages(cashierName, False)
            If IsFromCashier(m) = asAdmin AndAlso m.TimeSent > latest Then latest = m.TimeSent
        Next
        If latest = DateTime.MinValue Then Return

        Dim key As String = ReadKey(asAdmin, cashierName)
        Dim current As DateTime
        If readAtByKey.TryGetValue(key, current) AndAlso current >= latest Then Return

        readAtByKey(key) = If(DateTime.Now > latest, DateTime.Now, latest)
        SaveReadStates()
        RaiseEvent ReadStatesChanged(Nothing, EventArgs.Empty)
    End Sub

    '------------------------------------------------------------------
    ' EDITING A MESSAGE  (a sender may correct his OWN message)
    ' The ChatMessage class and messages.txt are unchanged: the "Edited" mark
    ' is kept in message_edits.txt (key = time sent + sender).
    '------------------------------------------------------------------
    Private ReadOnly editedAtByKey As New Dictionary(Of String, DateTime)(StringComparer.Ordinal)
    Private editsLoaded As Boolean = False

    Private Function EditKey(m As ChatMessage) As String
        Return m.TimeSent.Ticks.ToString(CultureInfo.InvariantCulture) & "#" & SafeField(If(m.Sender, ""))
    End Function

    Private Sub EnsureEditsLoaded()
        If editsLoaded Then Return
        editsLoaded = True
        Try
            Dim filePath As String = FilePathOf("message_edits.txt")
            If Not File.Exists(filePath) Then Return
            For Each line As String In File.ReadAllLines(filePath, Encoding.UTF8)
                Dim f As String() = Fields(line)
                Dim ticks As Long
                If f.Length >= 2 AndAlso Long.TryParse(f(1), NumberStyles.Integer, CultureInfo.InvariantCulture, ticks) Then
                    editedAtByKey(f(0)) = New DateTime(ticks)
                End If
            Next
        Catch ex As Exception
            ' unreadable file: messages simply show no "Edited" mark
        End Try
    End Sub

    Private Sub SaveEdits()
        File.WriteAllLines(FilePathOf("message_edits.txt"),
                           editedAtByKey.Select(Function(kv) kv.Key & "|" & kv.Value.Ticks.ToString(CultureInfo.InvariantCulture)).ToArray(),
                           Encoding.UTF8)
    End Sub

    ''' <summary>When the message was last edited (DateTime.MinValue = never).</summary>
    Public Function MessageEditedAt(m As ChatMessage) As DateTime
        If m Is Nothing Then Return DateTime.MinValue
        EnsureEditsLoaded()
        Dim t As DateTime
        If editedAtByKey.TryGetValue(EditKey(m), t) Then Return t
        Return DateTime.MinValue
    End Function

    ''' <summary>Automatic reports ("[Stock adjustment] ...", "[Ingredient Wastage] ...") are records, not chat: never editable.</summary>
    Public Function IsSystemMessage(m As ChatMessage) As Boolean
        Return Regex.IsMatch(If(m.Message, ""), "^\s*\[(Stock adjustment|Ingredient [^\]]*)\]", RegexOptions.IgnoreCase)
    End Function

    ''' <summary>asAdmin = True: the admin's own messages.  False: the cashier's own messages.</summary>
    Public Function CanEditMessage(m As ChatMessage, asAdmin As Boolean, editorName As String) As Boolean
        If m Is Nothing OrElse IsSystemMessage(m) Then Return False
        If Not String.Equals(If(m.Sender, "").Trim(), If(editorName, "").Trim(), StringComparison.OrdinalIgnoreCase) Then Return False
        Return IsFromCashier(m) <> asAdmin
    End Function

    Public Function EditMessage(m As ChatMessage, newText As String, asAdmin As Boolean, editorName As String,
                                ByRef errorMessage As String) As Boolean
        errorMessage = ""
        If Not CanEditMessage(m, asAdmin, editorName) Then errorMessage = "You can only edit your own messages." : Return False
        If Not AllChatMessages.Contains(m) OrElse Not ShopContext.CanAccess(m.ShopId) Then errorMessage = "Message not found." : Return False
        If String.IsNullOrWhiteSpace(newText) Then errorMessage = "A message cannot be empty." : Return False
        Dim cleaned As String = newText.Trim()
        If String.Equals(cleaned, m.Message, StringComparison.Ordinal) Then Return True      ' nothing changed

        EnsureEditsLoaded()
        Dim oldText As String = m.Message
        Dim key As String = EditKey(m)
        Dim hadMark As Boolean = editedAtByKey.ContainsKey(key)
        Dim oldMark As DateTime = If(hadMark, editedAtByKey(key), DateTime.MinValue)
        m.Message = cleaned
        editedAtByKey(key) = DateTime.Now
        Try
            SaveMessages()
            SaveEdits()
        Catch ex As Exception
            m.Message = oldText
            If hadMark Then editedAtByKey(key) = oldMark Else editedAtByKey.Remove(key)
            errorMessage = "The message could not be saved: " & ex.Message
            Return False
        End Try
        RaiseEvent MessagesChanged(Nothing, EventArgs.Empty)
        Return True
    End Function

End Module