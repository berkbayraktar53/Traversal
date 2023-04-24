﻿using System.Linq;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class ReservationManager : IReservationService
	{
		private readonly IReservationDal _reservationDal;
		private readonly IUserService _userService;

		public ReservationManager(IReservationDal reservationDal, IUserService userService)
		{
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

		public Reservation ChangeStatus(int id)
		{
			var result = GetById(id);
			if (result.Status == true)
			{
				result.Status = false;
			}
			else
			{
				result.Status = true;
			}
			Update(result);
			return result;
		}

		public void Delete(Reservation reservation)
		{
			_reservationDal.Delete(reservation);
		}

		public Reservation GetById(int id)
		{
			return _reservationDal.Get(p => p.Id == id);
		}

		public List<Reservation> GetList()
		{
			return _reservationDal.GetList();
		}

		public List<Reservation> GetListWithDestination()
		{
			return _reservationDal.GetListWithDestination().OrderByDescending(p => p.Date).ToList();
		}

		public List<Reservation> GetListWithDestinationAndUser()
		{
			return _reservationDal.GetListWithDestinationAndUser().OrderByDescending(p => p.Date).ToList();
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

		public Reservation GetReservationWithDestinationAndUser(int id)
		{
			return _reservationDal.GetReservationWithDestinationAndUser(id);
		}

		public void Update(Reservation reservation)
		{
			_reservationDal.Update(reservation);
		}
	}
}