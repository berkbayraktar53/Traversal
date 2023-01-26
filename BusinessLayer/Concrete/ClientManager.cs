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

        public List<Client> GetListByActiveStatus()
        {
            return _clientDal.GetList(p => p.Status == true);
        }
    }
}