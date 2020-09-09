-- Create a new table called 'Book' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Book', 'U') IS NOT NULL
DROP TABLE dbo.Book
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Book
(
    BookId INT NOT NULL PRIMARY KEY IDENTITY,
    Title NVARCHAR(150) NOT NULL,
    Subtitle NVARCHAR(100) NULL,
    AuthorId INT NOT NULL FOREIGN KEY REFERENCES Author(AuthorId),
    PublisherId INT NOT NULL FOREIGN KEY REFERENCES Publisher(PublisherId),
    PublicationDate DATE NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    ISBN10 CHAR(10) NULL,
    ISBN13 CHAR(13) NOT NULL,
    TotalPages INT NOT NULL,
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES Category(CategoryId),
    IsMature BIT NOT NULL,
    CoverImage VARBINARY(MAX) NULL,
    CoverImageType VARCHAR(7) NULL,
    BookLanguageId INT NOT NULL FOREIGN KEY REFERENCES BookLanguage(BookLanguageId),
    Price MONEY NOT NULL CHECK(Price >= 0),
    DownloadLink NVARCHAR(800) NOT NULL

    CONSTRAINT AK_Book_ISBN10 UNIQUE(ISBN10),
    CONSTRAINT AK_Book_ISBN13 UNIQUE(ISBN13),
    CONSTRAINT AK_Book_TitleSubtitleAuthorId UNIQUE(Title, Subtitle, AuthorId),
    CONSTRAINT AK_Book_DownloadLink UNIQUE(DownloadLink)
);
GO

SELECT * FROM Book;