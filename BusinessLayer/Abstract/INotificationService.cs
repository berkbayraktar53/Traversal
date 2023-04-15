using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface INotificationService
	{
		void Add(Notification notification);
		void Delete(Notification notification);
		void Update(Notification notification);
		Notification GetById(int id);
		Notification ChangeStatus(int id);
		List<Notification> GetList();
		List<Notification> GetListByActiveStatus();
	}
}