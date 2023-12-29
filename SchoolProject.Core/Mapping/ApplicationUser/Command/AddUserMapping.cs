using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Command.Models;
using SchoolProject.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
	public partial class ApplicationUserProfile : Profile
	{
		public void AddUserMapping()
		{
			CreateMap<AddUserCommand,User>();
		}
	}
}
