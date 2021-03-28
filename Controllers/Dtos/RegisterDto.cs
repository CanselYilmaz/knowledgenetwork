using knowledgenetwork.Models;

namespace knowledgenetwork.Controllers.Dtos
{
    public class RegisterDto: User
    {
        public string PasswordConfirm { get; set; }
        public string ErrorMessage { get; set; }
    }
}