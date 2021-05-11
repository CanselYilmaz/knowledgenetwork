using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories.Interfaces;
using knowledgenetwork.Repositories;

namespace knowledgenetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommentRepository commentRepository;
        private readonly IBlogRepository blogRepository;
        private readonly ICategoryRepository catRepository;


        public HomeController(ICommentRepository cRepo, IBlogRepository blogRepo, ICategoryRepository catRepo)
        {
            commentRepository = cRepo;
            blogRepository = blogRepo;
            catRepository = catRepo;
        }
        public HomeController() { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlogPost()
        {
            return View();
        }

        public async Task<IActionResult> Blog()
        {
            return View(await blogRepository.GetAllAsync());
        }

        public async Task<IActionResult> BlogPage(int? id)
        {
            Blog blog = new Blog();
            if (id != null)
            {
                blog = await blogRepository.GetByIdAsync(id.Value);
            }
            ViewBag.CategoryId = catRepository.GetDropboxList();
            return View(blog);
        }

        public async Task<IActionResult> GetComments()
        {
            List<Comment> comments = await commentRepository.GetAllAsync();
            return View(comments);
        }
    }
}
