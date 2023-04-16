using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface IDestinationService
	{
		void Add(Destination destination);
		void Delete(Destination destination);
		void Update(Destination destination);
		Destination GetById(int id);
		Destination ChangeStatus(int id);
		List<Destination> GetList();
		List<Destination> GetListByActiveStatus();
		List<Destination> GetFourDestinationByActiveStatus();
		List<Destination> GetEightDestinationByActiveStatus();
	}
}