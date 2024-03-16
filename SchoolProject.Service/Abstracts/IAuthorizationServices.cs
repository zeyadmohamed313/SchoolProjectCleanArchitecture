using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
	public interface IAuthorizationServices
	{
		Task<string> AddRoleAsync(string roleName);
		Task<bool> IsRoleExstists(string roleName);
		Task<string> EditRoleAsync(EditRoleRequest editRoleRequest);
		Task<string> DeleteRoleAsync(int roleId);
		Task<List<Role>> GetRolesAsync();
	    Task<Role> GetRoleById(int id);
		Task<ManageUserRolesResult> ManageUserRolesData(User user);
    }
}
