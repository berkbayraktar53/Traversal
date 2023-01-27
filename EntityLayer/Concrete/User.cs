using CoreLayer.EntityLayer;
using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class User : IdentityUser<int>, IEntity
    {
        public string UserImage { get; set; }
        public string NameSurname { get; set; }
        public string AboutUser { get; set; }
        public bool Status { get; set; }
    }
}