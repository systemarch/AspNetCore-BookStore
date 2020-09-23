using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Areas.Api.Models;
using BookStore.Infrastructure;
using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBookStoreRepository _repository;

        public AuthorsController(IBookStoreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorsListPage(
             [FromQuery] AuthorsQueryParameters parameters)
        {
            var authorsQuery = from a in _repository.Authors
                               where a.Name.ToLower().Contains(parameters.Name.ToLower())
                               select a;
            var authorsListPage = await ListPage<Author>.CreateAsync(
                authorsQuery.AsNoTracking(),
                    parameters.PageIndex, parameters.PageSize);

            return Ok(authorsListPage);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var author = await _repository.FindAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
    }
}
