using LibraryManagement.Data;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    public class LibraryBranchService
    {
        private readonly AppDbContext _context;

        public LibraryBranchService(AppDbContext context)
        {
            _context = context;
        }

        public List<LibraryBranch> GetAllLibraryBranches()
        {
            return _context.LibraryBranches.ToList();
        }

        public LibraryBranch GetLibraryBranchById(int id)
        {
            return _context.LibraryBranches.Find(id);
        }

        public void CreateLibraryBranch(LibraryBranch branch)
        {
            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch));
            }
            _context.LibraryBranches.Add(branch);
            _context.SaveChanges();
        }

        public void UpdateLibraryBranch(LibraryBranch branch)
        {
            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch));
            }

            // 检查上下文中的本地缓存是否已跟踪目标实体
            var trackedBranch = _context.LibraryBranches.Local.FirstOrDefault(b => b.LibraryBranchId == branch.LibraryBranchId);

            if (trackedBranch != null)
            {
                // 手动解除上下文对已跟踪实体的跟踪
                _context.Entry(trackedBranch).State = EntityState.Detached;
            }

            // 标记传入的实体为已修改
            _context.LibraryBranches.Attach(branch);
            _context.Entry(branch).State = EntityState.Modified;

            _context.SaveChanges();
        }



        public void DeleteLibraryBranch(int id)
        {
            var branch = _context.LibraryBranches.Find(id);
            if (branch != null)
            {
                _context.LibraryBranches.Remove(branch);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"LibraryBranch with ID {id} does not exist.");
            }
        }
    }
}
