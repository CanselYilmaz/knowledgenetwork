using System.Collections.Generic;
using knowledgenetwork.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace knowledgenetwork.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<SelectListItem> GetDropboxList();
    }
}