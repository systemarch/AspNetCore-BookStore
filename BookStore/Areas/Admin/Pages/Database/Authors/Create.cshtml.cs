using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Areas.Admin.Pages.Database.Authors
{
    public class CreateModel : CreateOrEditModel
    {
        private readonly IBookStoreRepository _repository;

        public CreateModel(IBookStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
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

            await _repository.AddAuthorAsync(Author);

            return RedirectToPage("./Index");
        }
    }
}
