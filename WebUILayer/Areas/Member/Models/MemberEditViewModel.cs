using Microsoft.AspNetCore.Http;

namespace WebUILayer.Areas.Member.Models
{
    public class MemberEditViewModel
    {
        public IFormFile UserImage { get; set; }
        public string NameSurname { get; set; }
        public string AboutUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}