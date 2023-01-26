using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICommentService
    {
        List<Comment> GetListByActiveStatus();
    }
}