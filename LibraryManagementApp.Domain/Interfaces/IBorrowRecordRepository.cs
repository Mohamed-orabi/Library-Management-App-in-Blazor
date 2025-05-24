using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Domain.Interfaces
{
    public interface IBorrowRecordRepository
    {
        Task<List<BorrowRecord>> GetBorrowRecordsAsync(int? bookId);
        Task<BorrowRecord?> GetBorrowRecordByIdAsync(int id);
        Task AddBorrowRecordAsync(BorrowRecord record);
        Task UpdateBorrowRecordAsync(BorrowRecord record);
        Task DeleteBorrowRecordAsync(int id);
    }
}
