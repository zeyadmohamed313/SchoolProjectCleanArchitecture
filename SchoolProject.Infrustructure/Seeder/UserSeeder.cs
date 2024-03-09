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
	public static class UserSeeder
	{
		public static async Task SeedAsync(UserManager<User> _userManager)
		{
			var usersCount =  _userManager.Users.Count();
			if (usersCount <= 0)
			{
				var defaultuser = new User()
				{
					UserName = "admin",
					Email = "admin@project.com",
					FullName = "schoolProject",
					Country = "Egypt",
					PhoneNumber = "123456",
					Address = "Egypt",
					EmailConfirmed = true,
					PhoneNumberConfirmed = true
				};
				await _userManager.CreateAsync(defaultuser, "123456Aa*");
				await _userManager.AddToRoleAsync(defaultuser, "Admin");
			}
		}
	}
}
