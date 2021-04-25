using System;
using System.Threading.Tasks;
using knowledgenetwork.Controllers.Dtos;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace knowledgenetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult LoginPage(string ErrorMessage)
        {
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Home/Index");
            }
            return View(ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await _userRepo.LoginWithEmailAndPassword(email, email);
            if (user != null)
            {
                HttpContext.Session.SetInt32("id", user.Id);
                HttpContext.Session.SetString("name", user.Name);
                if(user.Role == Role.ADMIN){
                return RedirectToAction("AdminPage", "Admin");
                }
                return RedirectToAction("LoginPage");
            }
            return RedirectToAction("LoginPage", "Email veya parola hatalı.");
        }

        public IActionResult RegisterPage(RegisterDto registerDto)
        {
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Home/Index");
            }
            if (registerDto == null)
            {
                registerDto = new RegisterDto();
            }
            return View(registerDto);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!registerDto.Password.Equals(registerDto.PasswordConfirm))
            {
                registerDto.ErrorMessage = "Girilen parolalar aynı değil.";
            }
            else
            {
                if (!await _userRepo.CheckEmailAsync(registerDto.Email))
                {
                    registerDto.ErrorMessage = "Bu email başka bir kullanıcı tarafından kullanılıyor.";
                }
                else
                {
                    await _userRepo.AddAsync(
                        new User
                        {
                            Name = registerDto.Name,
                            Surname = registerDto.Surname,
                            Email = registerDto.Email,
                            Password = registerDto.Password,
                            Role = Role.USER
                        }
                    );
                    return Redirect("LoginPage");
                }
            }
            return RedirectToAction("RegisterPage", registerDto);
        }
    }
}