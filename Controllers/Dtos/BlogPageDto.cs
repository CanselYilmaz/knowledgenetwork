using System.Collections.Generic;
using knowledgenetwork.Models;

namespace knowledgenetwork.Controllers.Dtos
{
    public class BlogPageDto
    {
        public List<Blog> Blogs { get; set; }
        public List<Tags> Tags { get; set; }
    }
}