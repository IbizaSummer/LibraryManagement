using LibraryManagement.Data;
using LibraryManagement.Models;

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
            _context.LibraryBranches.Add(branch);
            _context.SaveChanges();
        }

        public void UpdateLibraryBranch(LibraryBranch branch)
        {
            _context.LibraryBranches.Update(branch);
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
        }
    }
}