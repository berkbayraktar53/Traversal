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
    }
}