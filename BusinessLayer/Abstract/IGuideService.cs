using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IGuideService
    {
        void Add(Guide guide);
        void Delete(Guide guide);
        void Update(Guide guide);
        Guide ChangeStatus(int id);
        Guide GetById(int id);
        List<Guide> GetList();
        List<Guide> GetListByActiveStatus();
    }
}