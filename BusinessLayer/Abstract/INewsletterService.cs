using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface INewsletterService
	{
		void Add(Newsletter newsletter);
		void Delete(Newsletter newsletter);
		void Update(Newsletter newsletter);
		Newsletter GetById(int id);
		Newsletter ChangeStatus(int id);
		List<Newsletter> GetList();
		List<Newsletter> GetListByActiveStatus();
	}
}