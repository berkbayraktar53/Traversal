using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleManager(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Add(Role role)
        {
            Role addedRole = new()
            {
                Name = role.Name,
                NormalizedName = role.Name,
                Description = role.Description,
                Status = true
            };
            await _roleManager.CreateAsync(addedRole);
        }

        public async Task<Role> ChangeStatus(int id)
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
            await _roleManager.UpdateAsync(user);
            return user;
        }

        public async Task Delete(Role role)
        {
            await _roleManager.DeleteAsync(role);
        }

        public async Task<Role> GetById(int id)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Role>> GetList()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task Update(Role role)
        {
            Role updatedRole = new()
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.Name,
                Description = role.Description,
                ConcurrencyStamp = role.ConcurrencyStamp,
                Status = role.Status
            };
            await _roleManager.UpdateAsync(updatedRole);
        }
    }
}