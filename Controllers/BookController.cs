using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Details()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        public IActionResult SearchById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View("Details", new List<Book> { book }); // 以列表形式返回书籍，复用现有的视图
        }

        [HttpPost]
        public IActionResult Update(int id, Book updatedBook)
        {
            if (ModelState.IsValid)
            {
                var book = _bookService.GetBookById(id);
                if (book == null)
                {
                    return NotFound();
                }

                book.Title = updatedBook.Title;
                book.AuthorId = updatedBook.AuthorId;
                book.LibraryBranchId = updatedBook.LibraryBranchId;
                _bookService.UpdateBook(book);
                return RedirectToAction("Details", new { id = book.BookId });
            }
            return View("Details", updatedBook);
        }

        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.CreateBook(book);
                return RedirectToAction("Details", new { id = book.BookId });
            }
            return View(book);
        }
    }
}
