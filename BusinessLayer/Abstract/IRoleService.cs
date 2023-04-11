using EntityLayer.Concrete;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IRoleService
    {
        Task Add(Role role);
        Task Delete(Role role);
        Task Update(Role role);
        Task<Role> GetById(int id);
        Task<Role> ChangeStatus(int id);
        Task<List<Role>> GetList();
    }
}