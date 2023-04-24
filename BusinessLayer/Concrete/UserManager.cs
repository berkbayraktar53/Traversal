using System;
using System.IO;
using System.Linq;
using X.PagedList;
using EntityLayer.Dtos;
using System.Collections;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concrete
{
	public class UserManager : IUserService
	{
		private readonly IFileService _fileService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		public UserManager(IFileService fileService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			_fileService = fileService;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public async Task Add(UserListDto userListDto)
		{
			User user = new()
			{
				NameSurname = userListDto.NameSurname,
				AboutUser = userListDto.AboutUser,
				UserName = userListDto.Email,
				Email = userListDto.Email,
				Status = true
			};
			if (userListDto.UserImage != null)
			{
				var resource = Directory.GetCurrentDirectory();
				var extension = Path.GetExtension(userListDto.UserImage.FileName);
				var imageName = Guid.NewGuid() + extension;
				var saveLocation = resource + "/wwwroot/images/user/" + imageName;
				var stream = new FileStream(saveLocation, FileMode.Create);
				await userListDto.UserImage.CopyToAsync(stream);
				user.UserImage = imageName;
			}
			user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userListDto.Password);
			await _userManager.CreateAsync(user);
		}

		public async Task<IdentityResult> AddRole(Task<User> user, string roleName)
		{
			return await _userManager.AddToRoleAsync(await user, roleName);
		}

		public async Task<User> ChangeStatus(int id)
		{
			var user = GetById(id).Result;
			if (user.Status == true)
			{
				user.Status = false;
			}
			else
			{
				user.Status = true;
			}
			await _userManager.UpdateAsync(user);
			return user;
		}

		public async Task Delete(User user)
		{
			await _userManager.DeleteAsync(user);
		}

		public async Task<IdentityResult> DeleteRole(Task<User> user, string roleName)
		{
			return await _userManager.RemoveFromRoleAsync(await user, roleName);
		}

		public async Task<User> GetById(int id)
		{
			return await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);
		}

		public int GetById()
		{
			return _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result.Id;
		}

		public User GetByUser()
		{
			return _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result;
		}

		public async Task<List<User>> GetList()
		{
			return await _userManager.Users.ToListAsync();
		}

		public async Task<List<User>> GetListByActiveStatus()
		{
			return await _userManager.Users.ToListAsync().Result.Where(p => p.Status == true).ToListAsync();
		}

		public async Task<bool> GetRoleExist(User user)
		{
			return await _userManager.Users.ContainsAsync(user);
		}

		public async Task<IEnumerable> GetUserRoles(User user)
		{
			return await _userManager.GetRolesAsync(user);
		}

		public async Task Update(UserListDto userListDto)
		{
			var user = GetByUser();
			user.Id = userListDto.Id;
			user.UserImage = _fileService.UserFileSave(userListDto.UserImage, user.UserImage);
			user.NameSurname = userListDto.NameSurname;
			user.AboutUser = userListDto.AboutUser;
			user.UserName = userListDto.Email;
			user.Email = userListDto.Email;
			user.Status = userListDto.Status;
			user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userListDto.Password);
			await _userManager.UpdateAsync(user);
		}
	}
}