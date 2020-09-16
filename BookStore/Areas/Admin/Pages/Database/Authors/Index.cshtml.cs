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

namespace BookStore.Areas.Admin.Pages.Database.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IBookStoreRepository _repository;

        public IndexModel(IBookStoreRepository repository)
        {
            _repository = repository;
        }


        public PaginatedList<Author> Authors { get; set; }

        public string IdSort { get; set; }
        public string NameSort { get; set; }
        public string DateOfBirthSort { get; set; }
        public string CurrentSort { get; set; }

        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            IdSort = sortOrder == "id" ? "id_desc" : "id";
            NameSort = sortOrder == "name" ? "name_desc" : "name";
            DateOfBirthSort = sortOrder == "dateOfBirth" ? "dateOfBirth_desc" : "dateOfBirth";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            var authorsQuery = from a in _repository.Authors
                               select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                authorsQuery = authorsQuery.Where(
                    a => a.Name.ToLower().Contains(
                        searchString.ToLower()));
            }

            authorsQuery = sortOrder switch
            {
                "id_desc" => authorsQuery.OrderByDescending(a => a.AuthorId),
                "name" => authorsQuery.OrderBy(a => a.Name),
                "name_desc" => authorsQuery.OrderByDescending(a => a.Name),
                "dateOfBirth" => authorsQuery.OrderBy(a => a.DateOfBirth),
                "dateOfBirth_desc" => authorsQuery.OrderByDescending(a => a.DateOfBirth),
                _ => authorsQuery.OrderBy(a => a.AuthorId),
            };

            int pageSize = 5;
            Authors = await PaginatedList<Author>.CreateAsync(
                authorsQuery.AsNoTracking(), pageIndex ?? 1, pageSize);
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
