using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Application.Interfaces;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Interfaces;

namespace LibraryManagementApp.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
        _authorRepository = authorRepository;
    }

    public async Task<List<BookDto>> GetBooksAsync(string filter)
    {
        var books = await _bookRepository.GetBooksAsync(filter);
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            ISBN = b.ISBN,
            PublicationDate = b.PublicationDate,
            CategoryId = b.CategoryId,
            CategoryName = b.Category?.Name ?? string.Empty,
            AuthorIds = b.BookAuthors.Select(ba => ba.AuthorId).ToList()
        }).ToList();
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null) return null;
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            PublicationDate = book.PublicationDate,
            CategoryId = book.CategoryId,
            CategoryName = book.Category?.Name ?? string.Empty,
            AuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList()
        };
    }

    public async Task AddBookAsync(BookDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            ISBN = bookDto.ISBN,
            PublicationDate = bookDto.PublicationDate,
            CategoryId = bookDto.CategoryId,
            BookAuthors = bookDto.AuthorIds.Select(aid => new BookAuthor { AuthorId = aid }).ToList()
        };
        await _bookRepository.AddBookAsync(book);
    }

    public async Task UpdateBookAsync(BookDto bookDto)
    {
        var book = new Book
        {
            Id = bookDto.Id,
            Title = bookDto.Title,
            ISBN = bookDto.ISBN,
            PublicationDate = bookDto.PublicationDate,
            CategoryId = bookDto.CategoryId,
            BookAuthors = bookDto.AuthorIds.Select(aid => new BookAuthor { AuthorId = aid }).ToList()
        };
        await _bookRepository.UpdateBookAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteBookAsync(id);
    }
}