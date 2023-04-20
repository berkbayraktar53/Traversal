using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface IAboutService
	{
		void Add(About about);
		void Delete(About about);
		void Update(About about);
		About GetById(int id);
		About ChangeStatus(int id);
		List<About> GetList();
		List<About> GetListByActiveStatus();
	}
}