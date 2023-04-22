using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface IClientService
	{
		void Add(Client client);
		void Delete(Client client);
		void Update(Client client);
		Client GetById(int id);
		Client ChangeStatus(int id);
		List<Client> GetList();
		List<Client> GetListByActiveStatus();
	}
}