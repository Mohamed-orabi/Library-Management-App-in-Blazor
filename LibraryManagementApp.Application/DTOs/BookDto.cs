namespace LibraryManagementApp.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        //public int AuthorId { get; set; }
        //public string AuthorName { get; set; } = string.Empty;
        public List<int> AuthorIds { get; set; } = new();
    }
}
