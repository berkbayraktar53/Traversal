using System;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
	public class Notification : IEntity
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public bool Status { get; set; }
	}
}