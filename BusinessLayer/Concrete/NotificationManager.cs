using System.Linq;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class NotificationManager : INotificationService
	{
		private readonly INotificationDal _notificationDal;

		public NotificationManager(INotificationDal notificationDal)
		{
			_notificationDal = notificationDal;
		}

		public void Add(Notification notification)
		{
			_notificationDal.Add(notification);
		}

		public Notification ChangeStatus(int id)
		{
			var notification = GetById(id);
			if (notification.Status == true)
			{
				notification.Status = false;
			}
			else
			{
				notification.Status = true;
			}
			Update(notification);
			return notification;
		}

		public void Delete(Notification notification)
		{
			_notificationDal.Delete(notification);
		}

		public Notification GetById(int id)
		{
			return _notificationDal.Get(p => p.Id == id);
		}

		public List<Notification> GetList()
		{
			return _notificationDal.GetList().OrderByDescending(p => p.Date).ToList();
		}

		public List<Notification> GetListByActiveStatus()
		{
			return _notificationDal.GetList(p => p.Status == true).OrderByDescending(p => p.Date).ToList();
		}

		public void Update(Notification notification)
		{
			_notificationDal.Update(notification);
		}
	}
}