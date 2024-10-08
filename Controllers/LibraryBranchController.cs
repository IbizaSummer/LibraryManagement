using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers
{
    public class LibraryBranchController : Controller
    {
        private readonly LibraryBranchService _libraryBranchService;

        public LibraryBranchController(LibraryBranchService libraryBranchService)
        {
            _libraryBranchService = libraryBranchService;
        }

        public IActionResult Details()
        {
            var libraryBranches = _libraryBranchService.GetAllLibraryBranches();
            return View(libraryBranches);
        }

        public IActionResult SearchById(int id)
        {
            var branch = _libraryBranchService.GetLibraryBranchById(id);
            if (branch == null)
            {
                return NotFound();
            }
            return View("Details", new List<LibraryBranch> { branch });
        }

        [HttpPost]
        public IActionResult Update(int id, LibraryBranch updatedBranch)
        {
            if (ModelState.IsValid)
            {
                var branch = _libraryBranchService.GetLibraryBranchById(id);
                if (branch == null)
                {
                    return NotFound();
                }

                branch.BranchName = updatedBranch.BranchName;  // 更新属性
                _libraryBranchService.UpdateLibraryBranch(branch);
                return RedirectToAction("Details", new { id = branch.LibraryBranchId });
            }
            return View("Details", updatedBranch);
        }

        public IActionResult Delete(int id)
        {
            _libraryBranchService.DeleteLibraryBranch(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LibraryBranch branch)
        {
            if (ModelState.IsValid)
            {
                _libraryBranchService.CreateLibraryBranch(branch);
                return RedirectToAction("Details", new { id = branch.LibraryBranchId });
            }
            return View(branch);
        }
    }
}
