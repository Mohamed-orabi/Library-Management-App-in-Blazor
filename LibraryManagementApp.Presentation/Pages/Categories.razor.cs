using LibraryManagementApp.Application.DTOs;
using Microsoft.JSInterop;

namespace LibraryManagementApp.Presentation.Pages
{
    public partial class Categories
    {
        private List<CategoryDto> categories = new();
        private CategoryDto newCategory = new();
        private int? editingCategoryId = null;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            categories = await CategoryService.GetCategoriesAsync();
        }

        private async Task AddOrUpdateCategory()
        {
            if (editingCategoryId == null)
            {
                await CategoryService.AddCategoryAsync(newCategory);
                await JS.InvokeVoidAsync("showNotification", "Category added successfully!");
            }
            else
            {
                newCategory.Id = editingCategoryId.Value;
                await CategoryService.UpdateCategoryAsync(newCategory);
                await JS.InvokeVoidAsync("showNotification", "Category updated successfully!");
                editingCategoryId = null;
            }

            newCategory = new CategoryDto();
            await LoadCategories();
        }

        private async Task EditCategory(CategoryDto category)
        {
            newCategory = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
            editingCategoryId = category.Id;
        }

        private async Task DeleteCategory(int id)
        {
            await CategoryService.DeleteCategoryAsync(id);
            await JS.InvokeVoidAsync("showNotification", "Category deleted successfully!");
            await LoadCategories();
        }

        private async Task CancelEdit()
        {
            newCategory = new CategoryDto();
            editingCategoryId = null;
        }
    }
}
