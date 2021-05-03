using System.Collections.Generic;
using System.Threading.Tasks;
using knowledgenetwork.Models;

namespace knowledgenetwork.Repositories.Interfaces
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<List<Blog>> blogListWithPagination(int pageNumber);
        Task<int> totalBlogCount();
    }
}