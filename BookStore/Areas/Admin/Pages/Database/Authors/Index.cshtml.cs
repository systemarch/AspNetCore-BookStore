using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Pages.Database.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IBookStoreRepository _repository;

        public IndexModel(IBookStoreRepository repository)
        {
            _repository = repository;
        }

        public IList<Author> Authors { get; set; }

        public async Task OnGetAsync()
        {
            Authors = await _repository.Authors.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _repository.FindAuthorAsync(id);

            if (author != null)
            {
                await _repository.DeleteAuthorAsync(author);
            }

            return RedirectToPage();
        }
    }
}
