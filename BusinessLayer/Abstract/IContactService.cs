using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface IContactService
	{
		void Add(Contact contact);
		void Delete(Contact contact);
		void Update(Contact contact);
		Contact GetById(int id);
		Contact ChangeStatus(int id);
		List<Contact> GetList();
		List<Contact> GetListByActiveStatus();
		List<Contact> GetFourContactByActiveStatus();
	}
}