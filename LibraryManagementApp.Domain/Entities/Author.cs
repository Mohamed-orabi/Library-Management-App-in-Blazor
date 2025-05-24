using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }
        public List<BookAuthor> BookAuthors { get; set; } = new();
    }
}
