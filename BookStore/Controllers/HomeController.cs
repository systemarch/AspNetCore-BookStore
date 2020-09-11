using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookStoreRepository _repository;

        public HomeController(IBookStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return RedirectToPage("/Database/Authors/Index", new { area = "Admin" });
            //return View(_repository.Authors.Include(a => a.Books));
        }
    }
}
