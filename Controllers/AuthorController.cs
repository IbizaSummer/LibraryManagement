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

		public IActionResult Details()
		{
			var authors = _authorService.GetAllAuthors();
			return View(authors);
		}

		public IActionResult SearchById(int id)
		{
			var author = _authorService.GetAuthorById(id);
			if (author == null)
			{
				return NotFound();
			}
			return View("Details", new List<Author> { author });
		}

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
				return RedirectToAction("Details", new { id = author.AuthorId });
			}
			return View("Details", updatedAuthor);
		}

		public IActionResult Delete(int id)
		{
			_authorService.DeleteAuthor(id);
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Author author)
		{
			if (ModelState.IsValid)
			{
				_authorService.CreateAuthor(author);
				return RedirectToAction("Details", new { id = author.AuthorId });
			}
			return View(author);
		}
	}
}
