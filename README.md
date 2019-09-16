URL Shortening Service

Follow the steps to run the service successfully:

Step 1: Execute the SQL Query first to setup the database.
       
       CREATE DATABASE NintexDB
       GO
       USE NintexDB

       CREATE TABLE UrlShorteningDetails(
	        [Id] [bigint] IDENTITY(1,1) NOT NULL,
	        [LongUrl] [varchar](max) NULL,
	        [DateCreated] [smalldatetime] NULL,
	        [DateExpiry] [smalldatetime] NULL,
	        [Active] [char](1) NULL
       )
       
Step 2: Change the connection string accordingly in ShortnerDbContext.cs file (if required)
