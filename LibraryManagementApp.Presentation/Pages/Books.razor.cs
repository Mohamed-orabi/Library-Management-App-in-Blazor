using LibraryManagementApp.Application.DTOs;
using Microsoft.JSInterop;

namespace LibraryManagementApp.Presentation.Pages
{
    public partial class Books
    {
        private List<BookDto> books = new();
        private List<CategoryDto> categories = new();
        private List<AuthorDto> authors = new();
        private BookDto newBook = new() { PublicationDate = DateTime.Today };
        private int? editingBookId = null;
        private string filter = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            books = await BookService.GetBooksAsync(filter);
            categories = await CategoryService.GetCategoriesAsync();
            authors = await AuthorService.GetAuthorsAsync(string.Empty);
        }

        private async Task AddOrUpdateBook()
        {
            if (newBook.CategoryId == 0)
            {
                await JS.InvokeVoidAsync("showNotification", "Please select a category.");
                return;
            }

            if (editingBookId == null)
            {
                await BookService.AddBookAsync(newBook);
                await JS.InvokeVoidAsync("showNotification", "Book added successfully!");
            }
            else
            {
                newBook.Id = editingBookId.Value;
                await BookService.UpdateBookAsync(newBook);
                await JS.InvokeVoidAsync("showNotification", "Book updated successfully!");
                editingBookId = null;
            }

            newBook = new BookDto { PublicationDate = DateTime.Today };
            await LoadData();
        }

        private async Task EditBook(BookDto book)
        {
            newBook = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                CategoryId = book.CategoryId,
                AuthorIds = book.AuthorIds
            };
            editingBookId = book.Id;
        }

        private async Task DeleteBook(int id)
        {
            await BookService.DeleteBookAsync(id);
            await JS.InvokeVoidAsync("showNotification", "Book deleted successfully!");
            await LoadData();
        }

        private async Task CancelEdit()
        {
            newBook = new BookDto { PublicationDate = DateTime.Today };
            editingBookId = null;
        }
    }
}
