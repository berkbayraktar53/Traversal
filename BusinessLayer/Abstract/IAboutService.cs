using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IAboutService
    {
        List<About> GetListByActiveStatus();
    }
}