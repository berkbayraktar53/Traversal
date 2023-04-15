using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface IReservationService
	{
		void Add(Reservation reservation);
		List<Reservation> GetList();
		List<Reservation> GetListWithDestination();
		List<Reservation> GetListWithDestinationByActiveStatus();
		List<Reservation> GetListWithReservationByAccepted(int id);
		List<Reservation> GetListWithReservationByPrevious(int id);
		List<Reservation> GetListWithReservationByWaitAprroval(int id);
	}
}