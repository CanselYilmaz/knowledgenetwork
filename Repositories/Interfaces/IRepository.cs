using System.Collections.Generic;
using System.Threading.Tasks;

namespace knowledgenetwork.Repositories
{
    public interface IRepository<T>
    {
        Task<bool> AddAsync(T t);
        Task<bool> UpdateAsync(T t);
        Task<T> GetByIdAsync(int Id);
        Task<List<T>> GetAllAsync();
        Task<bool> DeleteByIdAsync(int Id);
    }
}