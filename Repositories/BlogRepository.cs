using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace knowledgenetwork.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DatabaseContext _context;

        public BlogRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Blog t)
        {
            await _context.Blog.AddAsync(t);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<List<Blog>> blogListWithPagination(int pageNumber)
        {
            return await _context.Blog.OrderByDescending(b => b.CreateAt).Take(3).ToListAsync();
        }

        public async Task<bool> DeleteByIdAsync(int Id)
        {
            Blog blog = await _context.Blog.FirstOrDefaultAsync(c => c.Id == Id);
            if (blog == null)
            {
                return false;
            }
            _context.Blog.Remove(blog);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blog.Include(b => b.Category).ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(int Id)
        {
            return await _context.Blog.Include(b => b.Category).FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<int> totalBlogCount()
        {
           return await _context.Blog.CountAsync();
        }

        public async Task<bool> UpdateAsync(Blog t)
        {
            _context.Blog.Update(t);
            return await _context.SaveChangesAsync() != 0;
        }
    }
}