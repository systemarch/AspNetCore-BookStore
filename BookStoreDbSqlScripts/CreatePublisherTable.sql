-- Create a new table called 'Publisher' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Publisher', 'U') IS NOT NULL
DROP TABLE dbo.Publisher
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Publisher
(
    PublisherId INT NOT NULL PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL

    CONSTRAINT AK_Publisher_Name UNIQUE(Name)
);
GO

SELECT * FROM Publisher;