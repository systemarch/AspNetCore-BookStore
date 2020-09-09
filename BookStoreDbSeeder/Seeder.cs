using BookStore.Models;
using GoogleBooksService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDbSeeder
{
    public class Seeder
    {
        static readonly object synchlock = new object();

        private readonly BookStoreContext _context;
        private readonly string _initialDataFolderPath;

        public Seeder(BookStoreContext context, string initialDataFolderPath)
        {
            _context = context;
            _initialDataFolderPath = initialDataFolderPath;
        }

        public virtual void Seed()
        {
            if (_context.Author.Count() == 0)
            {
                lock (synchlock)
                {
                    Console.WriteLine("Generating authors...");
                    var authors = GenerateAuthors();
                    _context.Author.AddRange(authors);
                    _context.SaveChanges();
                    Console.WriteLine($"{authors.Count} authors have been saved to the database.");
                }
            }
            if (_context.BookLanguage.Count() == 0)
            {
                lock (synchlock)
                {
                    Console.WriteLine("Generating languages...");
                    var languages = GenerateLanguages();
                    _context.AddRange(languages);
                    _context.SaveChanges();
                    Console.WriteLine($"{languages.Count} languages have been saved to the database.");
                }
            }
            if (_context.Publisher.Count() == 0)
            {
                lock (synchlock)
                {
                    Console.WriteLine("Generating publishers...");
                    var publishers = GeneratePublishers();
                    _context.Publisher.AddRange(publishers);
                    _context.SaveChanges();
                    Console.WriteLine($"{publishers.Count} publishers have been saved to the database.");
                }
            }
            if (_context.Category.Count() == 0)
            {
                lock (synchlock)
                {
                    Console.WriteLine("Generating categories...");
                    var categories = GenerateCategories();
                    _context.Category.AddRange(categories);
                    _context.SaveChanges();
                    Console.WriteLine($"{categories.Count} categories have been saved to the database.");
                }
            }
            if (_context.Book.Count() == 0)
            {
                lock (synchlock)
                {
                    Console.WriteLine("Generating books...");
                    var books = GenerateBooksAsync().Result;
                    _context.AddRange(books);
                    _context.SaveChanges();
                    Console.WriteLine($"{books.Count} books have been saved to the database.");
                }
            }
        }

        private List<Author> GenerateAuthors()
        {
            var authors = new List<Author>()
            {
                new Author()
                {
                    Name = "Isaac Asimov",
                    DateOfBirth = new DateTime(1920, 1, 2),
                    DateOfDeath = new DateTime(1992, 4, 6),
                    Website = @"http://www.asimovonline.com"
                },
                new Author()
                {
                    Name = "J.R.R. Tolkien",
                    DateOfBirth = new DateTime(1892, 1, 3),
                    DateOfDeath = new DateTime(1973, 9, 2),
                    Website = @"http://www.tolkienestate.com"
                },
                new Author()
                {
                    Name = "George R. R. Martin",
                    DateOfBirth = new DateTime(1948, 9, 20),
                    Website = @"http://www.georgerrmartin.com"
                },
                new Author()
                {
                    Name = "Terry Pratchett",
                    DateOfBirth = new DateTime(1948, 4, 28),
                    DateOfDeath = new DateTime(2015, 5, 12),
                    Website = @"http://www.terrypratchett.co.uk"
                }
            };

            foreach (var author in authors)
            {
                string folderPath = $"{_initialDataFolderPath}\\Authors\\{author.Name}\\";
                using (StreamReader reader = new StreamReader(folderPath + "Bio.txt"))
                {
                    author.Biography = reader.ReadToEnd();
                }
                author.PhotoImage = File.ReadAllBytes(folderPath + "Photo.jpg");
                author.PhotoImageType = "jpeg";
            }

            return authors;
        }

        private List<BookLanguage> GenerateLanguages()
        {
            var languages = new List<BookLanguage>()
            {
                new BookLanguage()
                {
                    Name = "English",
                    Code = "en"
                },
                new BookLanguage()
                {
                    Name = "Russian",
                    Code = "ru"
                }
            };

            return languages;
        }

        private List<Publisher> GeneratePublishers()
        {
            var publishers = new List<Publisher>();

            foreach (var item in VolumeService.GetPublishers())
            {
                publishers.Add(new Publisher() { Name = item });
            }

            return publishers;
        }

        private List<Category> GenerateCategories()
        {
            var categories = new List<Category>();

            foreach (var item in VolumeService.GetCategories())
            {
                categories.Add(new Category() { Name = item });
            }

            return categories;
        }

        private async Task<List<Book>> GenerateBooksAsync()
        {
            var authors = _context.Author.ToList();
            var languages = _context.BookLanguage.ToList();
            var publishers = _context.Publisher.ToList();
            var categories = _context.Category.ToList();
            var books = new List<Book>();

            var service = new VolumeService();

            Console.WriteLine("Downloading data from the Google Books...");
            foreach (var author in authors)
            {
                var authorBooks = new List<Book>();

                Console.WriteLine($"Searching for the books by {author.Name}...");
                var searchResult = await service.SearchByAuthorAsync(author.Name, 80);
                Console.WriteLine($"{searchResult.Items.Count()} books have been found and downloaded.");

                Console.WriteLine("Processing book data...");
                foreach (var item in searchResult.Items)
                {
                    // Skip Google Books which are not for sale or which do not match the database
                    if (item.SaleInfo.Saleability != "FOR_SALE"
                        || author.Name != item.VolumeInfo.Authors.First()
                        || !publishers.Any(p => p.Name == item.VolumeInfo.Publisher)
                        || !categories.Any(c => c.Name == item.VolumeInfo.Categories.First())
                        || !languages.Any(l => l.Code == item.VolumeInfo.Language)
                        || !item.VolumeInfo.IndustryIdentifiers.Any(ii => ii.Type == "ISBN_13")
                        )
                    {
                        continue;
                    }

                    var volumeData = item.VolumeInfo;
                    var saleData = item.SaleInfo;

                    var book = new Book()
                    {
                        Title = volumeData.Title,
                        Subtitle = volumeData.Subtitle,
                        AuthorId = author.AuthorId,
                        PublisherId = publishers.First(p => p.Name == volumeData.Publisher).PublisherId,
                        Description = volumeData.Description,
                        IsMature = volumeData.MaturityRating != "NOT_MATURE",
                        Isbn10 = volumeData.IndustryIdentifiers.First(ii => ii.Type == "ISBN_10").Identifier,
                        Isbn13 = volumeData.IndustryIdentifiers.First(ii => ii.Type == "ISBN_13").Identifier,
                        TotalPages = volumeData.PageCount,
                        CategoryId = categories.First(c => c.Name == volumeData.Categories.First()).CategoryId,
                        CoverImage = await ImageHelper.DownloadImageAsync(volumeData.ImageLinks.Thumbnail),
                        CoverImageType = "jpeg",
                        BookLanguageId = languages.First(l => l.Code == volumeData.Language).BookLanguageId,
                        Price = saleData.RetailPrice.Amount,
                        DownloadLink = volumeData.CanonicalVolumeLink
                    };

                    string publicationDate = volumeData.PublishedDate;
                    if (publicationDate.Length == 4) // If there is only year in the publication date
                    {
                        publicationDate += "-01-01"; // add month and day to be able to store the date
                    }
                    book.PublicationDate = DateTime.Parse(publicationDate);

                    authorBooks.Add(book);
                }
                books.AddRange(authorBooks);
                Console.WriteLine($"{authorBooks.Count()} have been saved.");
            }

            return books;
        }
    }
}
