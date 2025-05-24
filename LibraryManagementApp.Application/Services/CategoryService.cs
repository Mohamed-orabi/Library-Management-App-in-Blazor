using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Application.Interfaces;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Interfaces;

namespace LibraryManagementApp.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _categoryRepository.GetCategoriesAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        return category == null ? null : new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task AddCategoryAsync(CategoryDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name
        };
        await _categoryRepository.AddCategoryAsync(category);
    }

    public async Task UpdateCategoryAsync(CategoryDto categoryDto)
    {
        var category = new Category
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };
        await _categoryRepository.UpdateCategoryAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _categoryRepository.DeleteCategoryAsync(id);
    }
}