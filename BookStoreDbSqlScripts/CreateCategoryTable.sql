-- Create a new table called 'Category' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Category', 'U') IS NOT NULL
DROP TABLE dbo.Category
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Category
(
    CategoryId INT NOT NULL PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL

    CONSTRAINT AK_Category_Name UNIQUE(Name)
);
GO

SELECT * FROM Category;