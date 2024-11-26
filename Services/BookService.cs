using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

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
            var trackedBook = _context.Books.Local.FirstOrDefault(b => b.BookId == book.BookId);

            if (trackedBook != null)
            {
                // 手动解除上下文对已跟踪实体的跟踪
                _context.Entry(trackedBook).State = EntityState.Detached;
            }

            // 标记传入的实体为已修改
            _context.Books.Attach(book);
            _context.Entry(book).State = EntityState.Modified;

            _context.SaveChanges();
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
