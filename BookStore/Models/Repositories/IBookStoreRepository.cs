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
    }
}
