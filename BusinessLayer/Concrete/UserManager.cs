using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<User> GetList()
        {
            return _userDal.GetList();
        }
    }
}