using System;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string PersonCount { get; set; }
        public DateTime Date { get; set; }
        public string ReservationStatus { get; set; }
        public bool Status { get; set; }

        #region Table relationship
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}