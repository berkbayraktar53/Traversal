using EntityLayer.Concrete;
using CoreLayer.DataAccessLayer.Abstract;

namespace DataAccessLayer.Abstract
{
    public interface IContactDal : IEntityRepository<Contact>
    {

    }
}