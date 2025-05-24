using LibraryManagementApp.Application.DTOs;

namespace LibraryManagementApp.Application.Interfaces;

public interface IBorrowRecordService
{
    Task<List<BorrowRecordDto>> GetBorrowRecordsAsync(int? bookId);
    Task<BorrowRecordDto?> GetBorrowRecordByIdAsync(int id);
    Task AddBorrowRecordAsync(BorrowRecordDto record);
    Task UpdateBorrowRecordAsync(BorrowRecordDto record);
    Task DeleteBorrowRecordAsync(int id);
}