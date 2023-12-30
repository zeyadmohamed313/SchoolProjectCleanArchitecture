using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Command.Models;
using SchoolProject.Data.Entites.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
	public partial class ApplicationUserProfile:Profile
	{
		public void UpdateUserMapping()
		{
			CreateMap<UpdateUserCommand, User>();
		}
	}
}
