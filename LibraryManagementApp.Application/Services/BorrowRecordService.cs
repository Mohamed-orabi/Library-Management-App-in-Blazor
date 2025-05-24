using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Application.Interfaces;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Interfaces;

namespace LibraryManagementApp.Application.Services;

public class BorrowRecordService : IBorrowRecordService
{
    private readonly IBorrowRecordRepository _borrowRecordRepository;
    private readonly IBookRepository _bookRepository;

    public BorrowRecordService(IBorrowRecordRepository borrowRecordRepository, IBookRepository bookRepository)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _bookRepository = bookRepository;
    }

    public async Task<List<BorrowRecordDto>> GetBorrowRecordsAsync(int? bookId)
    {
        var records = await _borrowRecordRepository.GetBorrowRecordsAsync(bookId);
        return records.Select(r => new BorrowRecordDto
        {
            Id = r.Id,
            BookId = r.BookId,
            BookTitle = r.Book?.Title ?? string.Empty,
            BorrowerName = r.BorrowerName,
            BorrowDate = r.BorrowDate,
            ReturnDate = r.ReturnDate
        }).ToList();
    }

    public async Task<BorrowRecordDto?> GetBorrowRecordByIdAsync(int id)
    {
        var record = await _borrowRecordRepository.GetBorrowRecordByIdAsync(id);
        return record == null ? null : new BorrowRecordDto
        {
            Id = record.Id,
            BookId = record.BookId,
            BookTitle = record.Book?.Title ?? string.Empty,
            BorrowerName = record.BorrowerName,
            BorrowDate = record.BorrowDate,
            ReturnDate = record.ReturnDate
        };
    }

    public async Task AddBorrowRecordAsync(BorrowRecordDto recordDto)
    {
        var record = new BorrowRecord
        {
            BookId = recordDto.BookId,
            BorrowerName = recordDto.BorrowerName,
            BorrowDate = recordDto.BorrowDate,
            ReturnDate = recordDto.ReturnDate
        };
        await _borrowRecordRepository.AddBorrowRecordAsync(record);
    }

    public async Task UpdateBorrowRecordAsync(BorrowRecordDto recordDto)
    {
        var record = new BorrowRecord
        {
            Id = recordDto.Id,
            BookId = recordDto.BookId,
            BorrowerName = recordDto.BorrowerName,
            BorrowDate = recordDto.BorrowDate,
            ReturnDate = recordDto.ReturnDate
        };
        await _borrowRecordRepository.UpdateBorrowRecordAsync(record);
    }

    public async Task DeleteBorrowRecordAsync(int id)
    {
        await _borrowRecordRepository.DeleteBorrowRecordAsync(id);
    }
}