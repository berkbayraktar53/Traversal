using System.Linq;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
	public class CommentManager : ICommentService
	{
		private readonly ICommentDal _commentDal;
		private readonly IUserService _userService;

		public CommentManager(ICommentDal commentDal, IUserService userService)
		{
			_commentDal = commentDal;
			_userService = userService;
		}

		public List<Comment> GetListByActiveStatus()
		{
			return _commentDal.GetList(p => p.Status == true);
		}

		public List<Comment> GetCommentListWithDestinationAndUserByActiveStatus()
		{
			return _commentDal.GetCommentListWithDestinationAndUser(_userService.GetById()).Where(p => p.Status == true).OrderByDescending(p => p.CommentDate).ToList();
		}

		public List<Comment> GetCommentListWithDestinationAndUser()
		{
			return _commentDal.GetCommentListWithDestinationAndUser().OrderByDescending(p => p.CommentDate).ToList();
		}

		public void Add(Comment comment)
		{
			_commentDal.Add(comment);
		}

		public void Delete(Comment comment)
		{
			_commentDal.Delete(comment);
		}

		public void Update(Comment comment)
		{
			_commentDal.Update(comment);
		}

		public Comment ChangeStatus(int id)
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

		public Comment GetById(int id)
		{
			return _commentDal.Get(p => p.Id == id);
		}

		public List<Comment> GetList()
		{
			return _commentDal.GetList();
		}
	}
}