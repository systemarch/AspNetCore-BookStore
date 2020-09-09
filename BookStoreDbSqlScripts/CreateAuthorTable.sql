-- Create a new table called 'Author' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Author', 'U') IS NOT NULL
DROP TABLE dbo.Author
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Author
(
    AuthorId INT NOT NULL PRIMARY KEY IDENTITY,
    Name NVARCHAR(168) NOT NULL,
    PhotoImage VARBINARY(MAX) NULL,
    PhotoImageType VARCHAR(7) NULL,
    DateOfBirth DATE NOT NULL,
    DateOfDeath DATE NULL,
    Biography NVARCHAR(MAX) NULL,
    Website NVARCHAR(800) NULL,

    CONSTRAINT AK_Author_Name UNIQUE(Name),
);
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Author_Website
ON Author(Website)
WHERE Website IS NOT NULL;

SELECT * FROM Author;