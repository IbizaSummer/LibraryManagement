using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorApiController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorApiController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// 获取所有作者
        /// </summary>
        /// <returns>作者列表</returns>
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var authors = _authorService.GetAllAuthors();
            return Ok(authors);
        }

        /// <summary>
        /// 根据 ID 获取作者
        /// </summary>
        /// <param name="id">作者的唯一标识</param>
        /// <returns>指定 ID 的作者信息</returns>
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(new { Message = $"Author with ID {id} not found." });
            }
            return Ok(author);
        }

        /// <summary>
        /// 创建新作者
        /// </summary>
        /// <param name="author">作者数据</param>
        /// <returns>创建成功的作者信息</returns>
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authorService.CreateAuthor(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorId }, author);
        }

        /// <summary>
        /// 更新作者信息
        /// </summary>
        /// <param name="id">作者的唯一标识</param>
        /// <param name="author">更新后的作者数据</param>
        /// <returns>无内容表示更新成功</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest(new { Message = "ID mismatch in request body and URL." });
            }

            var existingAuthor = _authorService.GetAuthorById(id);
            if (existingAuthor == null)
            {
                return NotFound(new { Message = $"Author with ID {id} not found." });
            }

            _authorService.UpdateAuthor(author);
            return NoContent();
        }

        /// <summary>
        /// 删除作者
        /// </summary>
        /// <param name="id">作者的唯一标识</param>
        /// <returns>无内容表示删除成功</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(new { Message = $"Author with ID {id} not found." });
            }

            _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}
