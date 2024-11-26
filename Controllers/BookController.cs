using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;
using System.Collections.Generic;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        // 显示所有书籍
        public IActionResult Details()
        {
            var books = _bookService.GetAllBooks();
            ViewBag.Authors = _bookService.GetAuthors(); // 获取作者数据
            ViewBag.LibraryBranches = _bookService.GetLibraryBranches(); // 获取分馆数据
            return View(books);
        }

        // 根据 ID 搜索书籍
        public IActionResult SearchById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                throw new BookNotFoundException($"Book ID {id} Not found！");
            }
            return View("Details", new List<Book> { book }); // 返回单本书籍作为列表
        }

        [HttpPost]
        public IActionResult Update(int BookId, string Title, int AuthorId, int LibraryBranchId)
        {
            var updatedBook = new Book
            {
                BookId = BookId,
                Title = Title,
                AuthorId = AuthorId,
                LibraryBranchId = LibraryBranchId
            };

            if (ModelState.IsValid)
            {
                _bookService.UpdateBook(updatedBook);
                return RedirectToAction("Details");
            }

            // 重新加载数据并返回视图，如果有验证失败
            ViewBag.Authors = _bookService.GetAuthors();
            ViewBag.LibraryBranches = _bookService.GetLibraryBranches();
            var books = _bookService.GetAllBooks();
            return View("Details", books);
        }


        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction("Details"); // 删除后重定向到书籍列表页面
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                // 如果验证失败，重新加载数据并返回视图
                ViewBag.Authors = _bookService.GetAuthors();
                ViewBag.LibraryBranches = _bookService.GetLibraryBranches();
                return View("Details", _bookService.GetAllBooks());
            }

            _bookService.CreateBook(book);
            return RedirectToAction("Details");
        }
    }
}

