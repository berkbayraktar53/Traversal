using EntityLayer.Dtos;
using System.Collections;
using EntityLayer.Concrete;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task Add(UserListDto userListDto);
        Task Delete(User user);
        Task Update(UserListDto userListDto);
        Task<bool> GetRoleExist(User user);
        Task<IEnumerable> GetUserRoles(User user);
        Task<User> GetById(int id);
        int GetById();
        User GetByUser();
        Task<User> ChangeStatus(int id);
        Task<IdentityResult> AddRole(Task<User> user, string roleName);
        Task<IdentityResult> DeleteRole(Task<User> user, string roleName);
        Task<List<User>> GetList();
    }
}