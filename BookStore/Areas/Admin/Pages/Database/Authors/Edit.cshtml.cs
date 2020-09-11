using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Repositories;
using BookStore.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Pages.Database.Authors
{
    public class EditModel : CreateOrEditModel
    {
        private readonly IBookStoreRepository _repository;

        public EditModel(IBookStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author = await _repository.FindAuthorAsync(id);

            if (Author == null)
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

            if (PhotoImageFile != null)
            {
                var imageService = new ImageService();

                (Author.PhotoImage, Author.PhotoImageType) =
                    await imageService.GetImageFromFormFile(PhotoImageFile);
            }
            else if (!string.IsNullOrEmpty(PhotoImageUrl))
            {
                var imageService = new ImageService();

                (Author.PhotoImage, Author.PhotoImageType) =
                    await imageService.GetImageFromUrl(PhotoImageUrl);
            }

            try
            {
                await _repository.EditAuthorAsync(Author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.AuthorExists(Author.AuthorId))
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
