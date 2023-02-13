using System.Linq;
using EntityLayer.Dtos;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class GuideManager : IGuideService
    {
        private readonly IGuideDal _guideDal;

        public GuideManager(IGuideDal guideDal)
        {
            _guideDal = guideDal;
        }

        public void Add(Guide guide)
        {
            _guideDal.Add(guide);
        }

        public Guide ChangeStatus(int id)
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

        public void Delete(Guide guide)
        {
            _guideDal.Delete(guide);
        }

        public Guide GetById(int id)
        {
            return _guideDal.Get(p => p.Id == id);
        }

        public List<Guide> GetList()
        {
            return _guideDal.GetList().OrderByDescending(p => p.Id).ToList();
        }

        public List<Guide> GetListByActiveStatus()
        {
            return _guideDal.GetList(p => p.Status == true);
        }

        public void Update(Guide guide)
        {
            _guideDal.Update(guide);
        }
    }
}