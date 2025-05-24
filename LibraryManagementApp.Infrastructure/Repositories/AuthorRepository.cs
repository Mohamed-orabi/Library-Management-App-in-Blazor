using Microsoft.EntityFrameworkCore;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Interfaces;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryContext _context;

    public AuthorRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetAuthorsAsync(string filter)
    {
        var query = _context.Authors.AsQueryable();
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(a => a.Name.Contains(filter, StringComparison.OrdinalIgnoreCase));
        }
        return await query.ToListAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task AddAuthorAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        var existing = await _context.Authors.FindAsync(author.Id);
        if (existing != null)
        {
            existing.Name = author.Name;
            existing.BirthDate = author.BirthDate;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}