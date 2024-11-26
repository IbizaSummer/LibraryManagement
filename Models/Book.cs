using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        // Foreign Key for Author
        public int AuthorId { get; set; }

        [JsonIgnore]
        public Author? Author { get; set; }

        // Foreign Key for LibraryBranch
        public int LibraryBranchId { get; set; }

        [JsonIgnore]
        public LibraryBranch? LibraryBranch { get; set; }
    }
}
