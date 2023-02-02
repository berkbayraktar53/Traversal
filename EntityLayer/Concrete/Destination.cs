using System;
using System.Collections.Generic;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    public class Destination : IEntity
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string DayNight { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        #region Table relationship
        public List<Comment> Comments { get; set; }
        public List<Reservation> Reservations { get; set; }
        #endregion
    }
}