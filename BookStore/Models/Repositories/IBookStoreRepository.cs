using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public interface IBookStoreRepository
    {
        IQueryable<Author> Authors { get; }
        IQueryable<Book> Books { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<BookLanguage> Languages { get; }
        IQueryable<Publisher> Publishers { get; }

        bool AuthorExists(int id);
        Task<Author> FindAuthorAsync(int? id);
        Task AddAuthorAsync(Author author);
        Task EditAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);

        bool BookExists(int id);
        Task<Book> FindBookAsync(int? id);
        Task AddBookAsync(Book book);
        Task EditBookAsync(Book book);
        Task DeleteBookAsync(Book book);
    }
}
