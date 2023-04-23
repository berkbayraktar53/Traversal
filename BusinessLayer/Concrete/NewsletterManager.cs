using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class NewsletterManager : INewsletterService
	{
		private readonly INewsletterDal _newsletterDal;

		public NewsletterManager(INewsletterDal newsletterDal)
		{
			_newsletterDal = newsletterDal;
		}

		public void Add(Newsletter newsletter)
		{
			_newsletterDal.Add(newsletter);
		}

		public Newsletter ChangeStatus(int id)
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

		public void Delete(Newsletter newsletter)
		{
			_newsletterDal.Delete(newsletter);
		}

		public Newsletter GetById(int id)
		{
			return _newsletterDal.Get(p => p.Id == id);
		}

		public List<Newsletter> GetList()
		{
			return _newsletterDal.GetList();
		}

		public List<Newsletter> GetListByActiveStatus()
		{
			return _newsletterDal.GetList(p => p.Status == true);
		}

		public void Update(Newsletter newsletter)
		{
			_newsletterDal.Update(newsletter);
		}
	}
}