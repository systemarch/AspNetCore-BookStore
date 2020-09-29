# Book Store

This repository is an educational project, containing a book store ASP.NET Core 3.1 web app, 
as well as the SQL Server scripts for the books database. It also features the database seeder project,
which uses the [BookServices](https://github.com/systemarch/dotNetStandard-BookServices) library to get the book data.

- Used frameworks: ASP.NET Core 3.1, Entity Framework Core 3.1 (Database-first)
- Used patterns and technologies: MVC, Razor Pages, WebAPI
- Used client-side libraries: Bootstrap 4.5.2

## Database

Currently there are only book data tables in the database.

### Currently implemented features

- Book data tables:
    - Authors
    - Publishers
    - Categories
    - Languages
    - Books (only electronic)

### Possible future improvements

- Separate inherited tables for physical and electronic books
- More separate book data tables:
    - Editions
    - Series
    - Covers
- Order data tables
- Warehouse table (for physical books)
- Views, triggers, schemas, etc.

## Database Seeder

The database seeder includes manually-added author data, taken from [GoodReads](https://goodreads.com) (currently 4 authors).
For each author it gets the list of his books using the [BookServices](https://github.com/systemarch/dotNetStandard-BookServices) library methods,
and saves the matching information to the database.

## Web app - Admin panel

It is possible to manage only the authors and books.

### Currently implemented features

- Index pages with search, sorting and pagination
- Create\Edit pages with basic validation
- Create\Edit  pages allow selecting images as files or downloading
    them by URL
- Deletion confirmation modal

### Known issues

- Index pages are not responsible
- No image URL validation
- An attempt to delete an author who has books leads to an exception

### Possible future improvements

- Better validation (f. e. RegExp for ISBN)
- Page links with numbers for Index pages
- Responsible Index pages (cards\lists)
- CRUD pages for other entities
- Dashboard with the overview information
- Better UI (f. e. vertical menu)

## Web app - Main part

Coming soon...

## Web app - API

Currently the API has only the GET methods for authors and books.

### Currently implemented features

- Authors
    - GET the list of authors with search and pagination
    - GET one author by ID
- Books
    - GET the list of books with search and pagination
    - GET one book by ID

### Possible future improvements

- POST, DELETE methods
- Authentication

