using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        // Foreign Key for Author
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        // Foreign Key for LibraryBranch
        public int LibraryBranchId { get; set; }
        public LibraryBranch? LibraryBranch { get; set; }
    }
}
