using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Application.Interfaces;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Interfaces;

namespace LibraryManagementApp.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<List<AuthorDto>> GetAuthorsAsync(string filter)
    {
        var authors = await _authorRepository.GetAuthorsAsync(filter);
        return authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            BirthDate = a.BirthDate
        }).ToList();
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(id);
        return author == null ? null : new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            BirthDate = author.BirthDate
        };
    }

    public async Task AddAuthorAsync(AuthorDto authorDto)
    {
        var author = new Author
        {
            Name = authorDto.Name,
            BirthDate = authorDto.BirthDate
        };
        await _authorRepository.AddAuthorAsync(author);
    }

    public async Task UpdateAuthorAsync(AuthorDto authorDto)
    {
        var author = new Author
        {
            Id = authorDto.Id,
            Name = authorDto.Name,
            BirthDate = authorDto.BirthDate
        };
        await _authorRepository.UpdateAuthorAsync(author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        await _authorRepository.DeleteAuthorAsync(id);
    }
}