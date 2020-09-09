using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class EFBookStoreRepository : IBookStoreRepository
    {
        private readonly BookStoreContext _context;

        public EFBookStoreRepository(BookStoreContext context)
        {
            _context = context;
        }

        public IQueryable<Author> Authors => _context.Author;
        public IQueryable<Book> Books => _context.Book;
        public IQueryable<Category> Categories => _context.Category;
        public IQueryable<BookLanguage> Languages => _context.BookLanguage;
        public IQueryable<Publisher> Publishers => _context.Publisher;
    }
}
