using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IDestinationService
    {
        List<Destination> GetListByActiveStatus();
    }
}