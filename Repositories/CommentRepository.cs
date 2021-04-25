using System.Collections.Generic;
using System.Threading.Tasks;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace knowledgenetwork.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContext _context;

        public CommentRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Comment t)
        {
            await _context.Comment.AddAsync(t);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> DeleteByIdAsync(int Id)
        {
            Comment com = await _context.Comment.FirstOrDefaultAsync(c => c.Id == Id);
            if (com == null)
            {
                return false;
            }
            _context.Comment.Remove(com);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int Id)
        {
            return await _context.Comment.FirstOrDefaultAsync(c => c.Id == Id);
        }
        public async Task<bool> UpdateAsync(Comment t)
        {
            _context.Comment.Update(t);
            return await _context.SaveChangesAsync() != 0;
        }
    }
}