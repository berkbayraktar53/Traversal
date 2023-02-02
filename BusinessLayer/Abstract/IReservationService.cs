using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IReservationService
    {
        void Add(Reservation reservation);
        List<Reservation> GetList();
        List<Reservation> GetListWithDestination();
    }
}