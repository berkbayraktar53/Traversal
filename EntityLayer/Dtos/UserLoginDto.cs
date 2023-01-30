using CoreLayer.EntityLayer.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Dtos
{
    public class UserLoginDto : IDto
    {
        [Required(ErrorMessage = "Lütfen email giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        public string Password { get; set; }
    }
}