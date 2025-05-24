using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAuthorsAsync(string filter);
        Task<Author?> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
    }
}
