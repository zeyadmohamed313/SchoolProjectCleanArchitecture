﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Queries.Handler
{
	public class AuthenticationQueryHandler : ResponseHandler,
		IRequestHandler<AuthorizeUserQuery, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResourses> _localizer;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IAuthenticationServices _authenticationServices;

		#endregion
		#region Constructor
		public AuthenticationQueryHandler(IStringLocalizer<SharedResourses> localizer,
			UserManager<User> userManager, SignInManager<User> signInManager
			, IAuthenticationServices authenticationServices)
			: base(localizer)
		{
			_localizer = localizer;
			_userManager = userManager;
			_signInManager = signInManager;
			_authenticationServices = authenticationServices;
		}




		#endregion
		#region HandleFunctions
		
		public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
		{

			var result = await _authenticationServices.ValidateToken(request.AccessToken);
			if(result == "NotExpired")
			{
				return Success<string>(result);
			}
			return BadRequest<string>("Expired");
		}
		#endregion
	}
}
