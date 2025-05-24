using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync(string filter);
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
