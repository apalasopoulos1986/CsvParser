CsvParser Api A .Net 8 Web Api Project that parses a CSV file with transaction data from various applications.
It handles Upsert, Delete, Get operations for transactional data

Technologies used:  C# (NET 8 Framework) ,SQL Server (2022 Edition),  Dapper (For ORM Functionalities), Csv Helper (for Csv Parsing capabilities).

Installation / Basic Use

After you clone the repo, in appsettings.json of CsvParser Api project you complete in the ConnectionStrings section the connection string of your choice 
Example: "ConnectionStrings": { "DbConnection": "Server=localhost;Database=Transactions ;User Id=whatever;Password=whatever;" }.
and the File section inside Logging "File": {
   "Path": "Whatever\\Logs\\log.txt"
 }
for logging purposes.
Note that the path and the connection string have to be pre filled.
After that you utilize the sql scripts in the Sql.Scripts folder of CsvParser.Db project for database and table creation.

Once this is setup, you press the debug mode of Visual Studio 2022 and you are all set to go!

Overview

A Restful API using .NET that will parse a CSV file with transaction data from various applications. The API should be able to:
a.	Read a CSV file (Examples provided), validate the data and persist the information. 
•	File can include either new transactions or update existing transactions
•	Feel free to choose any (not in-memory) database you see fit
b.	Fetch all transactions paginated, using the transaction’s date as sorting field. User should be able to select page and/or number of transactions per page
c.	Delete a transaction
d.	Upsert a transaction
e.	Fetch a transaction
Data specification
Name	Details
Id	Mandatory, a unique identifier for the transaction. GUID, assigned by the API upon saving the transaction
ApplicationName	Mandatory, the name of the application that was used to record the transaction. Cannot exceed 200 characters
Email	Mandatory, the user’s email. Cannot exceed 200 characters.
Filename	Optional, the name of a file attached to the transaction. Cannot exceed 300 characters. Valid extensions: png, mp3, tiff, xls, pdf
Url	Optional, the URL of the external application. When provided, should be a valid URL
Inception	Mandatory, the transaction’s date. Should be in the past
Amount	Mandatory, amount with currency. An existing transaction cannot change currency
Allocation	Optional, a positive decimal between 0-100

CSV
Csv file will include headers.
Requirements
Application should be structured into logical layers. Final code should be production ready. Errors should be handled gracefully.

