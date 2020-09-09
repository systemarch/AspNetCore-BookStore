-- Create a new table called 'Language' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.BookLanguage', 'U') IS NOT NULL
DROP TABLE dbo.BookLanguage
GO
-- Create the table in the specified schema
CREATE TABLE dbo.BookLanguage
(
    BookLanguageId INT NOT NULL PRIMARY KEY IDENTITY,
    Code CHAR(2) NOT NULL,
    Name NVARCHAR(30) NOT NULL,

    CONSTRAINT AK_BookLanguage_Code UNIQUE(Code),
    CONSTRAINT AK_BookLanguage_Name UNIQUE(Name)
);
GO

SELECT * FROM BookLanguage;