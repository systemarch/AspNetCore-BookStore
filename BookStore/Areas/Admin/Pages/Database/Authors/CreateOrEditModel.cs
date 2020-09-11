using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Pages.Database.Authors
{
    public class CreateOrEditModel : PageModel
    {
        [BindProperty]
        public Author Author { get; set; }

        [BindProperty]
        [DisplayName("Choose a photo image file...")]
        public IFormFile PhotoImageFile { get; set; }

        [BindProperty]
        [Url]
        [DisplayName("or enter a photo image URL:")]
        public string PhotoImageUrl { get; set; }
    }
}
