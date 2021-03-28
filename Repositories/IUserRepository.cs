using System.Threading.Tasks;
using knowledgenetwork.Models;

namespace knowledgenetwork.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
         Task<bool> CheckEmailAsync(string Email);
         Task<User> LoginWithEmailAndPassword(string email,string password);
    }
}