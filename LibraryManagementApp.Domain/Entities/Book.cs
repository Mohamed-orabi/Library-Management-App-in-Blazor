using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, ErrorMessage = "ISBN must be 13 characters.", MinimumLength = 13)]
        public string ISBN { get; set; } = string.Empty;

        public DateTime PublicationDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public List<BookAuthor> BookAuthors { get; set; } = new();
        public List<BorrowRecord> BorrowRecords { get; set; } = new();
    }
}
