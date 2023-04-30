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
		Task UpdateMember(UserListDto userListDto);
		Task<bool> GetRoleExist(User user);
		Task<IEnumerable> GetUserRoles(User user);
		int GetById();
		User GetByUser();
		Task<User> GetById(int id);
		Task<IList<string>> GetRoles(User user);
		Task<User> ChangeStatus(int id);
		Task<User> GetByEmail(string email);
		Task<IdentityResult> AddRole(Task<User> user, string roleName);
		Task<bool> IsInRole(User user, string roleName);
		Task<IdentityResult> DeleteRole(Task<User> user, string roleName);
		Task<List<User>> GetList();
		Task<List<User>> GetListByActiveStatus();
	}
}