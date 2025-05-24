using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorDto>> GetAuthorsAsync(string filter);
    Task<AuthorDto?> GetAuthorByIdAsync(int id);
    Task AddAuthorAsync(AuthorDto author);
    Task UpdateAuthorAsync(AuthorDto author);
    Task DeleteAuthorAsync(int id);
}