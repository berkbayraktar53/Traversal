using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class AboutManager : IAboutService
	{
		private readonly IAboutDal _aboutDal;

		public AboutManager(IAboutDal aboutDal)
		{
			_aboutDal = aboutDal;
		}

		public void Add(About about)
		{
			_aboutDal.Add(about);
		}

		public About ChangeStatus(int id)
		{
			var about = GetById(id);
			if (about.Status == true)
			{
				about.Status = false;
			}
			else
			{
				about.Status = true;
			}
			Update(about);
			return about;
		}

		public void Delete(About about)
		{
			_aboutDal.Delete(about);
		}

		public About GetById(int id)
		{
			return _aboutDal.Get(p => p.Id == id);
		}

		public List<About> GetList()
		{
			return _aboutDal.GetList();
		}

		public List<About> GetListByActiveStatus()
		{
			return _aboutDal.GetList(p => p.Status == true);
		}

		public void Update(About about)
		{
			_aboutDal.Update(about);
		}
	}
}