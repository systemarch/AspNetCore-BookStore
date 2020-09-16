using Microsoft.EntityFrameworkCore;
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

        public bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorId == id);
        }
        public async Task<Author> FindAuthorAsync(int? id)
        {
            return await _context.Author.FindAsync(id);
        }
        public async Task AddAuthorAsync(Author author)
        {
            _context.Entry(author).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        public async Task EditAuthorAsync(Author author)
        {
            _context.Attach(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAuthorAsync(Author author)
        {
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
        }

        public bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }
        public async Task<Book> FindBookAsync(int? id)
        {
            return await _context.Book.FindAsync(id);
        }
        public async Task AddBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        public async Task EditBookAsync(Book book)
        {
            _context.Attach(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBookAsync(Book book)
        {
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
