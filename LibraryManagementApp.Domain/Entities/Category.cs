using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        public List<Book> Books { get; set; } = new();
    }
}
