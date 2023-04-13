using System.Linq;
using EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using CoreLayer.DataAccessLayer.Concrete.EntityFramework;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfCommentDal : EfEntityRepositoryBase<Comment, DatabaseContext>, ICommentDal
    {
        public List<Comment> GetCommentListWithDestination()
        {
            var context = new DatabaseContext();
            return context.Comments.Include(p => p.Destination).ToList();
        }

        public List<Comment> GetCommentListWithDestinationAndUser(int id)
        {
            var context = new DatabaseContext();
            return context.Comments.Where(p => p.UserId == id).Include(p => p.Destination).Include(p => p.User).ToList();
        }
    }
}