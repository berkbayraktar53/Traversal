using CoreLayer.EntityLayer.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Dtos
{
    public class UserRegisterDto : IDto
    {
        [Required(ErrorMessage = "Lütfen ad soyad giriniz")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Lütfen email giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifre tekrar giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler birbiriyle uyumlu değil")]
        public string ConfirmPassword { get; set; }
    }
}