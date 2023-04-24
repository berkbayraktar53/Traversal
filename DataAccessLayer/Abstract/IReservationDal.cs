using EntityLayer.Concrete;
using System.Collections.Generic;
using CoreLayer.DataAccessLayer.Abstract;

namespace DataAccessLayer.Abstract
{
	public interface IReservationDal : IEntityRepository<Reservation>
	{
		Reservation GetReservationWithDestinationAndUser(int id);
		List<Reservation> GetListWithDestination();
		List<Reservation> GetListWithDestinationAndUser();
	}
}