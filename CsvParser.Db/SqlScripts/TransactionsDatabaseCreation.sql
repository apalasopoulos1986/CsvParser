CREATE DATABASE Transactions;


CREATE TABLE ApplicationTransaction (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    ApplicationName NVARCHAR(200) NOT NULL, 
    Email NVARCHAR(200) NOT NULL, 
    Filename NVARCHAR(300) NULL ,
    Url NVARCHAR(2048) NULL ,
    Inception DATETIME NOT NULL ,
    Amount NVARCHAR(200) NOT NULL, 
    Allocation DECIMAL(5, 2) NULL 
);