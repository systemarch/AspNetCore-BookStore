using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Pages.Database.Books
{
    public class CreateOrEditModel : PageModel
    {
        [BindProperty]
        public Book Book { get; set; }

        [BindProperty]
        [DisplayName("Choose a cover image file...")]
        public IFormFile CoverImageFile { get; set; }

        [BindProperty]
        [Url]
        [DisplayName("or enter a cover image URL:")]
        public string CoverImageUrl { get; set; }


        public SelectList AuthorNameSelectList { get; set; }
        public SelectList PublisherNameSelectList { get; set; }
        public SelectList CategoryNameSelectList { get; set; }
        public SelectList LanguageNameSelectList { get; set; }

        public void PopulateAuthorsSelectList(IBookStoreRepository repository,
            object selectedAuthor = null)
        {
            var AuthorsQuery = from a in repository.Authors
                                  orderby a.Name
                                  select a;
            AuthorNameSelectList = new SelectList(AuthorsQuery.AsNoTracking(),
                "AuthorId", "Name", selectedAuthor);
        }
        public void PopulatePublishersSelectList(IBookStoreRepository repository,
            object selectedPublisher = null)
        {
            var publishersQuery = from p in repository.Publishers
                                  orderby p.Name
                                  select p;
            PublisherNameSelectList = new SelectList(publishersQuery.AsNoTracking(),
                "PublisherId", "Name", selectedPublisher);
        }
        public void PopulateCategoriesSelectList(IBookStoreRepository repository,
            object selectedCategory = null)
        {
            var CategoriesQuery = from c in repository.Categories
                                  orderby c.Name
                                  select c;
            CategoryNameSelectList = new SelectList(CategoriesQuery.AsNoTracking(),
                "CategoryId", "Name", selectedCategory);
        }
        public void PopulateLanguagesSelectList(IBookStoreRepository repository,
            object selectedLanguage = null)
        {
            var LanguagesQuery = from l in repository.Languages
                                  orderby l.Name
                                  select l;
            LanguageNameSelectList = new SelectList(LanguagesQuery.AsNoTracking(),
                "BookLanguageId", "Name", selectedLanguage);
        }
    }
}
