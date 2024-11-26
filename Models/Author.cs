using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LibraryManagement.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        // Navigation property
        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
