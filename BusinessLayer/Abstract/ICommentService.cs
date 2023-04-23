using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
	public interface ICommentService
	{
		void Add(Comment comment);
		void Delete(Comment comment);
		void Update(Comment comment);
		Comment ChangeStatus(int id);
		Comment GetById(int id);
		List<Comment> GetList();
		List<Comment> GetListByActiveStatus();
		List<Comment> GetCommentListWithDestinationAndUser();
		List<Comment> GetCommentListWithDestinationAndUserByActiveStatus();
	}
}