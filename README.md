# BrewPoint Café POS

Windows desktop point-of-sale application built with Visual Basic .NET and WinForms. Start the app and sign in as an Admin or Cashier. On first run, it creates the TXT data files beside the application executable and seeds a small café menu and starter accounts.

## Starter sign-ins

- Admin: `fritz` / `admin123`
- Cashier: `jedrick` / `cashier123`

Change starter passwords by editing the corresponding account file before distributing the application.

## Text files

All operational data is stored as UTF-8, pipe-delimited `.txt` files in the application folder:

- `admin_accounts.txt`: `username|password|fullname`
- `cashier_accounts.txt`: `username|password|fullname`
- `products.txt`: `productID|productName|price|stock|category`
- `orders.txt`: `orderID|cashierName|dateTime|totalAmount`
- `order_items.txt`: `orderID|productName|quantity|price|subtotal`
- `sales.txt`: `date|totalSales`
- `settings.txt`: internal application settings and next ID counters
- `price_log.txt`: product price change history
- `messages.txt`: internal message history

Product descriptions and images are not part of the required five-column product record. Avoid using `|` in text fields because it is the field separator.
