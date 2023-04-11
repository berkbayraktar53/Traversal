using Microsoft.AspNetCore.Http;

namespace EntityLayer.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }
        public IFormFile UserImage { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AboutUser { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}