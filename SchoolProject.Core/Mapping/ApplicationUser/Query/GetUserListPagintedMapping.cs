﻿using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Query.Models;
using SchoolProject.Core.Features.ApplicationUser.Query.Results;
using SchoolProject.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
	public partial class ApplicationUserProfile
	{
        public void GetUserPagintionMapping()
		{
			CreateMap<User, GetUserListResponsePaginated>();
		}
    }
}
