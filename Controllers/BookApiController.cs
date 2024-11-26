using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookApiController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookApiController(BookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// 获取所有书籍
        /// </summary>
        /// <returns>书籍列表的 JSON 数据</returns>
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        /// <summary>
        /// 根据 ID 获取单本书籍
        /// </summary>
        /// <param name="id">书籍的唯一标识</param>
        /// <returns>书籍的 JSON 数据</returns>
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found." });
            }
            return Ok(book);
        }

        /// <summary>
        /// 创建新书籍
        /// </summary>
        /// <param name="book">新书籍的 JSON 数据</param>
        /// <returns>创建成功的书籍数据</returns>
        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookService.CreateBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }

        /// <summary>
        /// 更新书籍信息
        /// </summary>
        /// <param name="id">书籍的唯一标识</param>
        /// <param name="book">更新后的书籍 JSON 数据</param>
        /// <returns>无内容返回，表示更新成功</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest(new { Message = "ID mismatch in request body and URL." });
            }

            var existingBook = _bookService.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found." });
            }

            _bookService.UpdateBook(book);
            return NoContent();
        }

        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id">书籍的唯一标识</param>
        /// <returns>无内容返回，表示删除成功</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found." });
            }

            _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
