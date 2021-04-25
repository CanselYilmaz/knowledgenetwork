using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using knowledgenetwork.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace knowledgenetwork.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Category t)
        {
            await _context.Category.AddAsync(t);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> DeleteByIdAsync(int Id)
        {
            Category cat = await _context.Category.FirstOrDefaultAsync(c => c.Id == Id);
            if (cat == null)
            {
                return false;
            }
            _context.Category.Remove(cat);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int Id)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public List<SelectListItem> GetDropboxList()
        {
            return _context.Category.Select(w =>
               new SelectListItem
               {
                   Text = w.Title,
                   Value = w.Id.ToString()
               }
            ).ToList();
        }

        public async Task<bool> UpdateAsync(Category t)
        {
            t.UpdateAt = DateTime.Now;
            _context.Category.Update(t);
            return await _context.SaveChangesAsync() != 0;
        }
    }
}