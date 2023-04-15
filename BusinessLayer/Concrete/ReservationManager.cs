using System.Linq;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class ReservationManager : IReservationService
	{
		private readonly IDestinationService _destinationService;
		private readonly IReservationDal _reservationDal;
		private readonly IUserService _userService;

		public ReservationManager(IDestinationService destinationService, IReservationDal reservationDal, IUserService userService)
		{
			_destinationService = destinationService;
			_reservationDal = reservationDal;
			_userService = userService;
		}

		public void Add(Reservation reservation)
		{
			reservation.UserId = _userService.GetById();
			reservation.Status = true;
			reservation.ReservationStatus = "Onaylandı";
			_reservationDal.Add(reservation);
		}

		public List<Reservation> GetList()
		{
			return _reservationDal.GetList();
		}

		public List<Reservation> GetListWithDestination()
		{
			return _reservationDal.GetListWithDestination().OrderByDescending(p => p.Date).ToList();
		}

		public List<Reservation> GetListWithDestinationByActiveStatus()
		{
			return _reservationDal.GetListWithDestination().Where(p => p.Status == true).OrderByDescending(p => p.Date).ToList();
		}

		public List<Reservation> GetListWithReservationByAccepted(int id)
		{
			return GetListWithDestinationByActiveStatus().Where(p => p.ReservationStatus == "Onaylandı").ToList();
		}

		public List<Reservation> GetListWithReservationByPrevious(int id)
		{
			return GetListWithDestinationByActiveStatus().Where(p => p.ReservationStatus == "Geçmiş Rezervasyon").ToList();
		}

		public List<Reservation> GetListWithReservationByWaitAprroval(int id)
		{
			return GetListWithDestinationByActiveStatus().Where(p => p.ReservationStatus == "Onay Bekliyor").ToList();
		}
	}
}