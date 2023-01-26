using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IClientService
    {
        List<Client> GetListByActiveStatus();
    }
}