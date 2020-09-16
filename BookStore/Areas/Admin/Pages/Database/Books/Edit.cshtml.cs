using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Repositories;
using BookStore.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Pages.Database.Books
{
    public class EditModel : CreateOrEditModel
    {
        private readonly IBookStoreRepository _repository;

        public EditModel(IBookStoreRepository repository)
        {
            _repository = repository;
            PopulateAuthorsSelectList(_repository);
            PopulatePublishersSelectList(_repository);
            PopulateCategoriesSelectList(_repository);
            PopulateLanguagesSelectList(_repository);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _repository.FindBookAsync(id);

            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CoverImageFile != null)
            {
                var imageService = new ImageService();

                (Book.CoverImage, Book.CoverImageType) =
                    await imageService.GetImageFromFormFile(CoverImageFile);
            }
            else if (!string.IsNullOrEmpty(CoverImageUrl))
            {
                var imageService = new ImageService();

                (Book.CoverImage, Book.CoverImageType) =
                    await imageService.GetImageFromUrl(CoverImageUrl);
            }

            try
            {
                await _repository.EditBookAsync(Book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.BookExists(Book.BookId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
