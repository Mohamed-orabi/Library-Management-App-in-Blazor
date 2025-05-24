using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(CategoryDto category);
    Task UpdateCategoryAsync(CategoryDto category);
    Task DeleteCategoryAsync(int id);
}