using LibraryManagement.Data;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    public class AuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author GetAuthorById(int id)
        {
            return _context.Authors.Find(id);
        }

        public void CreateAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            // 检查上下文中的本地缓存是否已跟踪目标实体
            var trackedAuthor = _context.Authors.Local.FirstOrDefault(a => a.AuthorId == author.AuthorId);

            if (trackedAuthor != null)
            {
                // 手动解除上下文对已跟踪实体的跟踪
                _context.Entry(trackedAuthor).State = EntityState.Detached;
            }

            // 标记传入的实体为已修改
            _context.Authors.Attach(author);
            _context.Entry(author).State = EntityState.Modified;

            _context.SaveChanges();
        }



        public void DeleteAuthor(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }
    }
}
