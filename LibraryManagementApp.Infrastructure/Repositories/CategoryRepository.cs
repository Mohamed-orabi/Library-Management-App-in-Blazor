using Microsoft.EntityFrameworkCore;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Interfaces;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly LibraryContext _context;

    public CategoryRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var existing = await _context.Categories.FindAsync(category.Id);
        if (existing != null)
        {
            existing.Name = category.Name;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}