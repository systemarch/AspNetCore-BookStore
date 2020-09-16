using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IList<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _repository.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.BookLanguage)
                .Include(b => b.Publisher)
                .ToListAsync();
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
