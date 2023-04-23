using EntityLayer.Concrete;
using System.Collections.Generic;
using CoreLayer.DataAccessLayer.Abstract;

namespace DataAccessLayer.Abstract
{
	public interface ICommentDal : IEntityRepository<Comment>
	{
		List<Comment> GetCommentListWithDestination();
		List<Comment> GetCommentListWithDestinationAndUser();
		List<Comment> GetCommentListWithDestinationAndUser(int id);
	}
}