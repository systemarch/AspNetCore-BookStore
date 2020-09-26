using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Areas.Api.Models;
using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookStoreRepository _repository;

        public BooksController(IBookStoreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksListPage(
             [FromQuery] BooksQueryParameters parameters)
        {
            var booksQuery = from b in _repository.Books
                             where (b.Title.ToLower().Contains(parameters.Title.ToLower())
                                && b.Author.Name.ToLower().Contains(parameters.Author.ToLower())
                                )
                             select b;
            var booksListPage = await ListPage<Book>.CreateAsync(
                booksQuery.AsNoTracking(),
                    parameters.PageIndex, parameters.PageSize);

            return Ok(booksListPage);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _repository.FindBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}
