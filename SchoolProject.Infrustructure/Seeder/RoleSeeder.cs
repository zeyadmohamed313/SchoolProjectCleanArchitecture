using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Seeder
{
	public static class RoleSeeder
	{
		public static async Task SeedAsync(RoleManager<Role> _roleManager)
		{
			var rolesCount =  _roleManager.Roles.Count();
			if (rolesCount <= 0)
			{

				await _roleManager.CreateAsync(new Role()
				{
					Name = "Admin"
				});
				await _roleManager.CreateAsync(new Role()
				{
					Name = "User"
				});
			}
		}
	}
}
