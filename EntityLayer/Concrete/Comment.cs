using System;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public string UserImage { get; set; }
        public string NameSurname { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
        public bool Status { get; set; }

        #region Table relationship
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
        #endregion
    }
}