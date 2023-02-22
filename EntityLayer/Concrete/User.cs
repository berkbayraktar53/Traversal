using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    public class User : IdentityUser<int>, IEntity
    {
        public string UserImage { get; set; }
        public string NameSurname { get; set; }
        public string AboutUser { get; set; }
        public bool Status { get; set; }

        #region Table relationship
        public List<Comment> Comments { get; set; }
        public List<Reservation> Reservations { get; set; }
        #endregion
    }
}