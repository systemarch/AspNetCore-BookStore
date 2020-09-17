using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Infrastructure;
using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Pages.Database.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookStoreRepository _repository;

        public IndexModel(IBookStoreRepository repository)
        {
            _repository = repository;
        }

        public PaginatedList<Book> Books { get; set; }

        public string IdSort { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CategorySort { get; set; }
        public string PublicationDateSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentSort { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            IdSort = sortOrder == "id" ? "id_desc" : "id";
            TitleSort = sortOrder == "title" ? "title_desc" : "title";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CategorySort = sortOrder == "category" ? "category_desc" : "category";
            PublicationDateSort = sortOrder == "publicationDate" ? "publicationDate_desc" : "publicationDate";
            PriceSort = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            var booksQuery = (from b in _repository.Books
                              select b);

            if (!string.IsNullOrEmpty(searchString))
            {
                booksQuery = booksQuery.Where(
                    b => b.Title.ToLower().Contains(
                        searchString.ToLower()));
            }

            booksQuery = sortOrder switch
            {
                "id_desc" => booksQuery.OrderByDescending(b => b.BookId),
                "title" => booksQuery.OrderBy(b => b.Title),
                "title_desc" => booksQuery.OrderByDescending(b => b.Title),
                "author" => booksQuery.OrderBy(b => b.Author.Name),
                "author_desc" => booksQuery.OrderByDescending(b => b.Author.Name),
                "category" => booksQuery.OrderBy(b => b.Category.Name),
                "category_desc" => booksQuery.OrderByDescending(b => b.Category.Name),
                "publicationDate" => booksQuery.OrderBy(b => b.PublicationDate),
                "publicationDate_desc" => booksQuery.OrderByDescending(b => b.PublicationDate),
                "price" => booksQuery.OrderBy(b => b.Price),
                "price_desc" => booksQuery.OrderByDescending(b => b.Price),
                _ => booksQuery.OrderBy(b => b.BookId),
            };

            int pageSize = 4;
            Books = await PaginatedList<Book>.CreateAsync(
                booksQuery.Include(b => b.Author)
                              .Include(b => b.Category)
                              .Include(b => b.BookLanguage)
                              .Include(b => b.Publisher).AsNoTracking(), pageIndex ?? 1, pageSize);

            //Books = await _repository.Books
            //    
            //    .ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.FindBookAsync(id);

            if (book != null)
            {
                await _repository.DeleteBookAsync(book);
            }

            return RedirectToPage();
        }
    }
}
