CsvParser Api A .Net 8 Web Api Project that parses a CSV file with transaction data from various applications.
It handles Upsert, Delete, Get operations for transactional data

Technologies used:  C# (NET 8 Framework) ,SQL Server (2022 Edition),  Dapper (For ORM Functionalities), Csv Helper (for Csv Prsing capabilities).

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
