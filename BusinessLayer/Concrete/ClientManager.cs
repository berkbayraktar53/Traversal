using System.Linq;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class ClientManager : IClientService
	{
		private readonly IClientDal _clientDal;

		public ClientManager(IClientDal clientDal)
		{
			_clientDal = clientDal;
		}

		public void Add(Client client)
		{
			_clientDal.Add(client);
		}

		public Client ChangeStatus(int id)
		{
			var client = GetById(id);
			if (client.Status == true)
			{
				client.Status = false;
			}
			else
			{
				client.Status = true;
			}
			Update(client);
			return client;
		}

		public void Delete(Client client)
		{
			_clientDal.Delete(client);
		}

		public Client GetById(int id)
		{
			return _clientDal.Get(p => p.Id == id);
		}

		public List<Client> GetList()
		{
			return _clientDal.GetList().OrderByDescending(p => p.Id).ToList();
		}

		public List<Client> GetListByActiveStatus()
		{
			return _clientDal.GetList(p => p.Status == true).OrderByDescending(p => p.Id).ToList();
		}

		public void Update(Client client)
		{
			_clientDal.Update(client);
		}
	}
}