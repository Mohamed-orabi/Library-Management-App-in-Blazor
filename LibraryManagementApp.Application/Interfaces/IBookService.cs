using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetBooksAsync(string filter);
    Task<BookDto?> GetBookByIdAsync(int id);
    Task AddBookAsync(BookDto book);
    Task UpdateBookAsync(BookDto book);
    Task DeleteBookAsync(int id);
}