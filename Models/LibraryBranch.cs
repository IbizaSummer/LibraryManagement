using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LibraryManagement.Models
{
    public class LibraryBranch
    {
        public int LibraryBranchId { get; set; }
        public string BranchName { get; set; }

        // Navigation property
        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>(); // 初始化
    }
}
