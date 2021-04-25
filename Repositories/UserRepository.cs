using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using knowledgenetwork.Models;
using Microsoft.EntityFrameworkCore;

namespace knowledgenetwork.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context){
            _context = context;
        }
        public async Task<bool> AddAsync(User user)
        {
            await _context.User.AddAsync(user);
            return  await _context.SaveChangesAsync() != 0; 
        }

        public async Task<bool> CheckEmailAsync(string Email)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(Email));
            return user == null;
        }

        public async Task<bool> DeleteByIdAsync(int Id)
        {
            User user = await _context.User.FindAsync(Id);
            if (user == null)
            {
                return false;
            }
            _context.User.Remove(user);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User.Where(u => u.Role == Role.AUTHOR).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int Id)
        {
            return await _context.User.FindAsync(Id);
        }

        public async Task<User> LoginWithEmailAndPassword(string email, string password)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password)); 
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.User.Update(user);
             return await _context.SaveChangesAsync() != 0;
        }
    }
}