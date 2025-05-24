using Microsoft.EntityFrameworkCore;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Interfaces;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.Infrastructure.Repositories;

public class BorrowRecordRepository : IBorrowRecordRepository
{
    private readonly LibraryContext _context;

    public BorrowRecordRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<BorrowRecord>> GetBorrowRecordsAsync(int? bookId)
    {
        var query = _context.BorrowRecords
            .Include(r => r.Book)
            .AsQueryable();
        if (bookId.HasValue)
        {
            query = query.Where(r => r.BookId == bookId.Value);
        }
        return await query.ToListAsync();
    }

    public async Task<BorrowRecord?> GetBorrowRecordByIdAsync(int id)
    {
        return await _context.BorrowRecords
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddBorrowRecordAsync(BorrowRecord record)
    {
        _context.BorrowRecords.Add(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBorrowRecordAsync(BorrowRecord record)
    {
        var existing = await _context.BorrowRecords.FindAsync(record.Id);
        if (existing != null)
        {
            existing.BookId = record.BookId;
            existing.BorrowerName = record.BorrowerName;
            existing.BorrowDate = record.BorrowDate;
            existing.ReturnDate = record.ReturnDate;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteBorrowRecordAsync(int id)
    {
        var record = await _context.BorrowRecords.FindAsync(id);
        if (record != null)
        {
            _context.BorrowRecords.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}