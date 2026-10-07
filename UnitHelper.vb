Option Strict On
Option Explicit On

' PHASE 1 (new file).  Ingredients are stored in BASE units (g, ml, pcs)
' so recipe deductions are exact: Coffee Beans 5 kg = 5000 g.
Public NotInheritable Class UnitHelper

    Private Sub New()
    End Sub

    Public Const Gram As String = "g"
    Public Const Milliliter As String = "ml"
    Public Const Piece As String = "pcs"

    ''' <summary>Converts an entered quantity (kg, g, L, ml, pcs) to the base unit.</summary>
    Public Shared Function ToBase(qty As Decimal, unit As String) As Decimal
        Select Case (If(unit, "")).Trim().ToLowerInvariant()
            Case "kg" : Return qty * 1000D
            Case "l" : Return qty * 1000D
            Case Else : Return qty
        End Select
    End Function

    ''' <summary>The base unit that a entered unit belongs to.</summary>
    Public Shared Function BaseUnitOf(unit As String) As String
        Select Case (If(unit, "")).Trim().ToLowerInvariant()
            Case "kg", "g" : Return Gram
            Case "l", "ml" : Return Milliliter
            Case Else : Return Piece
        End Select
    End Function

    ''' <summary>Friendly text: 5000 g -> "5 kg", 180 ml -> "180 ml".</summary>
    Public Shared Function Display(baseQty As Decimal, baseUnit As String) As String
        If baseUnit = Gram AndAlso Math.Abs(baseQty) >= 1000D Then
            Return (baseQty / 1000D).ToString("0.##") & " kg"
        End If
        If baseUnit = Milliliter AndAlso Math.Abs(baseQty) >= 1000D Then
            Return (baseQty / 1000D).ToString("0.##") & " L"
        End If
        Return baseQty.ToString("0.##") & " " & baseUnit
    End Function

End Class
