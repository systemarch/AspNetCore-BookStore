using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Areas.Admin.Pages.Database.Books
{
    public class CreateModel : CreateOrEditModel
    {
        private readonly IBookStoreRepository _repository;

        public CreateModel(IBookStoreRepository repository)
        {
            _repository = repository;
            PopulateAuthorsSelectList(_repository);
            PopulatePublishersSelectList(_repository);
            PopulateCategoriesSelectList(_repository);
            PopulateLanguagesSelectList(_repository);
        }


        [BindProperty]
        [Required]
        [DisplayName("Author")]
        public int? AuthorId { get; set; }

        [BindProperty]
        [Required]
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [BindProperty]
        [Required]
        [DisplayName("Publisher")]
        public int? PublisherId { get; set; }

        [BindProperty]
        [Required]
        [DisplayName("Language")]
        public int? BookLanguageId { get; set; }


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

            Book.AuthorId = AuthorId.Value;
            Book.CategoryId = CategoryId.Value;
            Book.PublisherId = PublisherId.Value;
            Book.BookLanguageId = BookLanguageId.Value;

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
                    await imageService.GetImageFromFormFile(CoverImageFile);
            }

            await _repository.AddBookAsync(Book);

            return RedirectToPage("./Index");
        }
    }
}
