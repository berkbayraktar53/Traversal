using CoreLayer.EntityLayer;
using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class Role : IdentityRole<int>, IEntity
    {
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}