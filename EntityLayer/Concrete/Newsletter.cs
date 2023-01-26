using System;
using CoreLayer.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Newsletter : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}