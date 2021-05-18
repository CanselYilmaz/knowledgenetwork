using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories.Interfaces;
using knowledgenetwork.Repositories;
using knowledgenetwork.Controllers.Dtos;

namespace knowledgenetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommentRepository commentRepository;
        private readonly IBlogRepository blogRepository;
        private readonly ICategoryRepository catRepository;
        private readonly ITagsRepository tagsRepository;


        public HomeController(ICommentRepository cRepo, IBlogRepository blogRepo, ICategoryRepository catRepo, ITagsRepository tagsRepo)
        {
            commentRepository = cRepo;
            blogRepository = blogRepo;
            catRepository = catRepo;
            tagsRepository = tagsRepo;
        }
        public HomeController() { }

        public async Task<IActionResult> Index()
        {
            BlogPageDto blogPageDto = new BlogPageDto();
            blogPageDto.Blogs = await blogRepository.GetAllAsync();
            blogPageDto.Tags = await tagsRepository.GetAllAsync();
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
