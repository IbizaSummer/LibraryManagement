using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;
using System.Collections.Generic;

namespace LibraryManagement.Controllers
{
    public class LibraryBranchController : Controller
    {
        private readonly LibraryBranchService _libraryBranchService;

        public LibraryBranchController(LibraryBranchService libraryBranchService)
        {
            _libraryBranchService = libraryBranchService;
        }

        // 显示所有分支并支持在同一页面创建新分支
        public IActionResult Details()
        {
            var libraryBranches = _libraryBranchService.GetAllLibraryBranches();
            return View(libraryBranches);
        }

        // 处理在 Details 页面上的创建请求
        [HttpPost]
        public IActionResult Create(LibraryBranch branch)
        {
            if (ModelState.IsValid)
            {
                _libraryBranchService.CreateLibraryBranch(branch);
                ViewData["CreateDebugMessage"] = "Branch created successfully!";
            }
            else
            {
                ViewData["CreateDebugMessage"] = "Branch creation failed!";
            }
            return RedirectToAction("Details");
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
                    ViewData["UpdateDebugMessage"] = "Branch not found!";
                    return NotFound();
                }

                branch.BranchName = updatedBranch.BranchName;
                _libraryBranchService.UpdateLibraryBranch(branch);
                ViewData["UpdateDebugMessage"] = "Branch updated successfully!";
            }
            else
            {
                ViewData["UpdateDebugMessage"] = "Branch update failed!";
            }
            return RedirectToAction("Details");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _libraryBranchService.DeleteLibraryBranch(id);
            ViewData["DeleteDebugMessage"] = "Branch deleted successfully!";
            return RedirectToAction("Details");
        }
    }
}



