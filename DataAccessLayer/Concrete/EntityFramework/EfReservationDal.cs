using System.Linq;
using EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using CoreLayer.DataAccessLayer.Concrete.EntityFramework;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfReservationDal : EfEntityRepositoryBase<Reservation, DatabaseContext>, IReservationDal
    {
        public List<Reservation> GetListWithDestination()
        {
            var context = new DatabaseContext();
            return context.Reservations.Include(x => x.Destination).ToList();
        }
    }
}