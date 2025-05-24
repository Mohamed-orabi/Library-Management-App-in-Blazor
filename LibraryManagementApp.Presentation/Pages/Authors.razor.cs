using LibraryManagementApp.Application.DTOs;
using Microsoft.JSInterop;

namespace LibraryManagementApp.Presentation.Pages
{
    public partial class Authors
    {
        private List<AuthorDto> authors = new();
        private AuthorDto newAuthor = new() { BirthDate = DateTime.Today.AddYears(-30) };
        private int? editingAuthorId = null;
        private string filter = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadAuthors();
        }

        private async Task LoadAuthors()
        {
            authors = await AuthorService.GetAuthorsAsync(filter);
        }

        private async Task AddOrUpdateAuthor()
        {
            if (editingAuthorId == null)
            {
                await AuthorService.AddAuthorAsync(newAuthor);
                await JS.InvokeVoidAsync("showNotification", "Author added successfully!");
            }
            else
            {
                newAuthor.Id = editingAuthorId.Value;
                await AuthorService.UpdateAuthorAsync(newAuthor);
                await JS.InvokeVoidAsync("showNotification", "Author updated successfully!");
                editingAuthorId = null;
            }

            newAuthor = new AuthorDto { BirthDate = DateTime.Today.AddYears(-30) };
            await LoadAuthors();
        }

        private async Task EditAuthor(AuthorDto author)
        {
            newAuthor = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                BirthDate = author.BirthDate
            };
            editingAuthorId = author.Id;
        }

        private async Task DeleteAuthor(int id)
        {
            await AuthorService.DeleteAuthorAsync(id);
            await JS.InvokeVoidAsync("showNotification", "Author deleted successfully!");
            await LoadAuthors();
        }

        private async Task CancelEdit()
        {
            newAuthor = new AuthorDto { BirthDate = DateTime.Today.AddYears(-30) };
            editingAuthorId = null;
        }
    }
}
