using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Model
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Логин не указан")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
