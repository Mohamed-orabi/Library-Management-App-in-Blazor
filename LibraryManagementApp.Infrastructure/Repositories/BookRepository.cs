using Microsoft.EntityFrameworkCore;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Interfaces;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetBooksAsync(string filter)
    {
        var query = _context.Books
            .Include(b => b.Category)
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(b => b.Title.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                                    b.ISBN.Contains(filter, StringComparison.OrdinalIgnoreCase));
        }

        return await query.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Category)
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        var existing = await _context.Books
            .Include(b => b.BookAuthors)
            .FirstOrDefaultAsync(b => b.Id == book.Id);

        if (existing != null)
        {
            existing.Title = book.Title;
            existing.ISBN = book.ISBN;
            existing.PublicationDate = book.PublicationDate;
            existing.CategoryId = book.CategoryId;
            _context.BookAuthors.RemoveRange(existing.BookAuthors);
            existing.BookAuthors = book.BookAuthors;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}