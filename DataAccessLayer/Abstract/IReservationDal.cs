using EntityLayer.Concrete;
using System.Collections.Generic;
using CoreLayer.DataAccessLayer.Abstract;

namespace DataAccessLayer.Abstract
{
    public interface IReservationDal : IEntityRepository<Reservation>
    {
        List<Reservation> GetListWithDestination();
    }
}