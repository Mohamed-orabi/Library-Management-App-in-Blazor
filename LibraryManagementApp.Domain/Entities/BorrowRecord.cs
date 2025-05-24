using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Domain.Entities
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } = default!;

        [Required(ErrorMessage = "Borrower name is required.")]
        [StringLength(100, ErrorMessage = "Borrower name cannot exceed 100 characters.")]
        public string BorrowerName { get; set; } = string.Empty;

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
