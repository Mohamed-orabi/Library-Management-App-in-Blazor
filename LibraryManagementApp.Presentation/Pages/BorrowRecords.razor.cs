using LibraryManagementApp.Application.DTOs;
using Microsoft.JSInterop;

namespace LibraryManagementApp.Presentation.Pages
{
    public partial class BorrowRecords
    {
        private List<BorrowRecordDto> borrowRecords = new();
        private List<BookDto> books = new();
        private BorrowRecordDto newRecord = new() { BorrowDate = DateTime.Today };
        private int? editingRecordId = null;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            borrowRecords = await BorrowRecordService.GetBorrowRecordsAsync(null);
            books = await BookService.GetBooksAsync(string.Empty);
        }

        private async Task AddOrUpdateRecord()
        {
            if (newRecord.BookId == 0)
            {
                await JS.InvokeVoidAsync("showNotification", "Please select a book.");
                return;
            }

            if (editingRecordId == null)
            {
                await BorrowRecordService.AddBorrowRecordAsync(newRecord);
                await JS.InvokeVoidAsync("showNotification", "Borrow record added successfully!");
            }
            else
            {
                newRecord.Id = editingRecordId.Value;
                await BorrowRecordService.UpdateBorrowRecordAsync(newRecord);
                await JS.InvokeVoidAsync("showNotification", "Borrow record updated successfully!");
                editingRecordId = null;
            }

            newRecord = new BorrowRecordDto { BorrowDate = DateTime.Today };
            await LoadData();
        }

        private async Task EditRecord(BorrowRecordDto record)
        {
            newRecord = new BorrowRecordDto
            {
                Id = record.Id,
                BookId = record.BookId,
                BorrowerName = record.BorrowerName,
                BorrowDate = record.BorrowDate,
                ReturnDate = record.ReturnDate
            };
            editingRecordId = record.Id;
        }

        private async Task DeleteRecord(int id)
        {
            await BorrowRecordService.DeleteBorrowRecordAsync(id);
            await JS.InvokeVoidAsync("showNotification", "Borrow record deleted successfully!");
            await LoadData();
        }

        private async Task CancelEdit()
        {
            newRecord = new BorrowRecordDto { BorrowDate = DateTime.Today };
            editingRecordId = null;
        }
    }
}
