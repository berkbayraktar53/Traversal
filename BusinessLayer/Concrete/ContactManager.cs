using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Concrete
{
	public class ContactManager : IContactService
	{
		private readonly IContactDal _contactDal;

		public ContactManager(IContactDal contactDal)
		{
			_contactDal = contactDal;
		}

		public void Add(Contact contact)
		{
			_contactDal.Add(contact);
		}

		public Contact ChangeStatus(int id)
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

		public void Delete(Contact contact)
		{
			_contactDal.Delete(contact);
		}

		public Contact GetById(int id)
		{
			return _contactDal.Get(p => p.Id == id);
		}

		public List<Contact> GetList()
		{
			return _contactDal.GetList().OrderByDescending(p => p.Id).ToList();
		}

		public List<Contact> GetListByActiveStatus()
		{
			return _contactDal.GetList(p => p.Status == true);
		}

		public void Update(Contact contact)
		{
			_contactDal.Update(contact);
		}
	}
}