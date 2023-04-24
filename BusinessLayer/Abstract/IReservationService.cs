using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface IReservationService
	{
		void Add(Reservation reservation);
		void Delete(Reservation reservation);
		void Update(Reservation reservation);
		Reservation GetById(int id);
		Reservation GetReservationWithDestinationAndUser(int id);
		Reservation ChangeStatus(int id);
		List<Reservation> GetList();
		List<Reservation> GetListWithDestination();
		List<Reservation> GetListWithDestinationAndUser();
		List<Reservation> GetListWithDestinationByActiveStatus();
		List<Reservation> GetListWithReservationByAccepted(int id);
		List<Reservation> GetListWithReservationByPrevious(int id);
		List<Reservation> GetListWithReservationByWaitAprroval(int id);
	}
}