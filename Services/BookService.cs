using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class BookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public void CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _context.Books.Find(book.BookId);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.AuthorId = book.AuthorId;
                existingBook.LibraryBranchId = book.LibraryBranchId;
                _context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public List<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public List<LibraryBranch> GetLibraryBranches()
        {
            return _context.LibraryBranches.ToList();
        }
    }


}
