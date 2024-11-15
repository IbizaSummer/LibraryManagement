using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        // 返回所有作者列表
        public IActionResult Details()
        {
            var authors = _authorService.GetAllAuthors();
            return View(authors);
        }

        // 通过 ID 搜索特定作者
        public IActionResult SearchById(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            // 返回作者详情视图，并显示搜索结果
            return View("Details", new List<Author> { author });
        }

        // 更新作者信息
        [HttpPost]
        public IActionResult Update(int id, Author updatedAuthor)
        {
            if (ModelState.IsValid)
            {
                var author = _authorService.GetAuthorById(id);
                if (author == null)
                {
                    return NotFound();
                }

                author.Name = updatedAuthor.Name;
                _authorService.UpdateAuthor(author);
                return RedirectToAction("Details"); // 返回到作者列表
            }
            return View("Details", updatedAuthor);
        }

        // 删除作者
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _authorService.DeleteAuthor(id);
            return RedirectToAction("Details"); // 返回到作者列表
        }

        // 显示创建作者的页面
        public IActionResult Create()
        {
            return View();
        }

        // 创建新作者
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _authorService.CreateAuthor(author);
                return RedirectToAction("Details"); // 返回到作者列表
            }
            return View(author);
        }
    }
}
