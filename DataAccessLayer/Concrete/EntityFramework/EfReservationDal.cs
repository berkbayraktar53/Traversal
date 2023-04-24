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

		public List<Reservation> GetListWithDestinationAndUser()
		{
			var context = new DatabaseContext();
			return context.Reservations.Include(x => x.Destination).Include(p => p.User).ToList();
		}

		public Reservation GetReservationWithDestinationAndUser(int id)
		{
			var context = new DatabaseContext();
			return context.Reservations.Where(p => p.Id == id).Include(x => x.Destination).Include(p => p.User).SingleOrDefault();
		}
	}
}