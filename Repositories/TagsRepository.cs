using System.Collections.Generic;
using System.Threading.Tasks;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace knowledgenetwork.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly DatabaseContext _context;

        public TagsRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Tags t)
        {
            await _context.Tags.AddAsync(t);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> DeleteByIdAsync(int Id)
        {
            Tags tag = await _context.Tags.FirstOrDefaultAsync(c => c.Id == Id);
            if (tag == null)
            {
                return false;
            }
            _context.Tags.Remove(tag);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<List<Tags>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tags> GetByIdAsync(int Id)
        {
            return await _context.Tags.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<bool> UpdateAsync(Tags t)
        {
            _context.Tags.Update(t);
            return await _context.SaveChangesAsync() != 0;
        }
    }
}