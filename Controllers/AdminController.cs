using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using knowledgenetwork.Controllers.Dtos;
using knowledgenetwork.Filter;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories;
using knowledgenetwork.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace knowledgenetwork.Controllers
{
    [AdminFilter]
    public class AdminController : Controller
    {
        private readonly ICategoryRepository _catRepo;
        private readonly IBlogRepository _blogRepo;
        private readonly IUserRepository _userRepo;

        public AdminController(ICategoryRepository catRepo, IBlogRepository blogRepo, IUserRepository userRepo)
        {
            _catRepo = catRepo;
            _blogRepo = blogRepo;
            _userRepo = userRepo;
        }

        public async Task<IActionResult> Category()
        {
            return View(await _catRepo.GetAllAsync());
        }

        public async Task<IActionResult> Blog()
        {
            return View(await _blogRepo.GetAllAsync());
        }

        public async Task<IActionResult> BlogPage(int? id)
        {
            Blog blog = new Blog();
            if (id != null)
            {
                blog = await _blogRepo.GetByIdAsync(id.Value);
            }
            ViewBag.CategoryId = _catRepo.GetDropboxList();
            return View(blog);
        }
        [HttpPost]
        public async Task<IActionResult> BlogSave(Blog blog)
        {
            if (blog != null)
            {
                var file = Request.Form.Files.First();
                string savePath = Path.Combine("wwwroot", "Images");
                var fileName = $"{DateTime.Now:MMddHHmmss}.{file.FileName.Split(".").Last()}";
                var fileUrl = Path.Combine(savePath, fileName);
                using (var fileStream = new FileStream(fileUrl, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                blog.BannerUrl = "\\Images\\" + fileName;
                int? userId = HttpContext.Session.GetInt32("id");
                blog.UserId = userId.Value;
                blog.IsPublish = true;
                await _blogRepo.AddAsync(blog);
                return Json(true);
            }
            return Json(false);
        }

        public async Task<IActionResult> Authors()
        {
            return View(await _userRepo.GetAllAsync());
        }
        public IActionResult Tags()
        {
            return View();
        }

        public IActionResult AdminPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category cat)
        {
            await _catRepo.AddAsync(cat);
            return RedirectToAction("Category");
        }
        public async Task<IActionResult> DeleteCategory(int? Id)
        {
            await _catRepo.DeleteByIdAsync(Id.Value);
            return RedirectToAction("Category");
        }

        public async Task<IActionResult> DeleteAuthor(int? Id)
        {
            await _userRepo.DeleteByIdAsync(Id.Value);
            return RedirectToAction("Authors");
        }
        [HttpPost]
        public async Task<IActionResult> AuthorSave(User user)
        {
            user.Role = Role.AUTHOR;
            if (user.Id == 0)
            {
                await _userRepo.AddAsync(user);
            }
            else
            {
                await _userRepo.UpdateAsync(user);
            }
            return RedirectToAction("Authors");
        }
        public async Task<IActionResult> AuthorSavePage(int? Id)
        {
            User user = null;
            if (Id != null)
            {
                user = await _userRepo.GetByIdAsync(Id.Value);
            }
            if (user == null)
            {
                user = new User();
            }
            return View(user);
        }

    }
}