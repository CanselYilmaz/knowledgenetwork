using System.Threading.Tasks;
using knowledgenetwork.Controllers.Dtos;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories;
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

        public IActionResult LoginPage()
        {
            return View();
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await _userRepo.LoginWithEmailAndPassword(email, password);
            if (user != null)
            {
                // Login successfull
            }
            return Redirect("LoginPage");
        }

        public IActionResult RegisterPage(RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                registerDto = new RegisterDto();
            }
            return View(registerDto);
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