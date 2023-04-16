using System.Linq;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class DestinationManager : IDestinationService
	{
		private readonly IDestinationDal _destinationDal;

		public DestinationManager(IDestinationDal destinationDal)
		{
			_destinationDal = destinationDal;
		}

		public void Add(Destination destination)
		{
			_destinationDal.Add(destination);
		}

		public Destination ChangeStatus(int id)
		{
			var values = _destinationDal.Get(p => p.Id == id);
			if (values.Status == true)
			{
				values.Status = false;
				Update(values);
				return values;
			}
			else
			{
				values.Status = true;
				Update(values);
				return values;
			}
		}

		public void Delete(Destination destination)
		{
			_destinationDal.Delete(destination);
		}

		public Destination GetById(int id)
		{
			return _destinationDal.Get(p => p.Id == id);
		}

		public List<Destination> GetEightDestinationByActiveStatus()
		{
			return _destinationDal.GetList(p => p.Status == true).TakeLast(8).OrderByDescending(p => p.Date).ToList();
		}

		public List<Destination> GetFourDestinationByActiveStatus()
		{
			return _destinationDal.GetList(p => p.Status == true).TakeLast(4).OrderByDescending(p => p.Date).ToList();
		}

		public List<Destination> GetList()
		{
			return _destinationDal.GetList().OrderByDescending(p => p.Id).ToList();
		}

		public List<Destination> GetListByActiveStatus()
		{
			return _destinationDal.GetList(p => p.Status == true).OrderByDescending(p => p.Date).ToList();
		}

		public void Update(Destination destination)
		{
			_destinationDal.Update(destination);
		}
	}
}