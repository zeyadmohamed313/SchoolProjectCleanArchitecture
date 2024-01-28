using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Commands.Handler
{
	public class AuthenticationCommandHandler : 
		ResponseHandler,IRequestHandler<SignInCommand,Response<JwtAuthResult>>,
		IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>

	{
		#region Fields
		private readonly IStringLocalizer<SharedResourses> _localizer;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IAuthenticationServices _authenticationServices;
		
        #endregion
        #region Constructor
        public AuthenticationCommandHandler(IStringLocalizer<SharedResourses> localizer, 
			UserManager<User> userManager , SignInManager<User> signInManager
			, IAuthenticationServices authenticationServices)
            :base(localizer)
        {
            _localizer = localizer;   
			_userManager = userManager;
			_signInManager = signInManager;
			_authenticationServices = authenticationServices;
        }




		#endregion
		#region Handle Function
		public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
		{
			//Check if user is exist or not
			var user = await _userManager.FindByNameAsync(request.UserName);
			//Return The UserName Not Found
			if (user == null) return BadRequest<JwtAuthResult>(_localizer[SharedResoursesKeys.UserNameIsNotExist]);
			//try To Sign in 
			var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			//if Failed Return Passord is wrong
			if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>(_localizer[SharedResoursesKeys.PasswordNotCorrect]);
			
			//Generate Token
			var result = await _authenticationServices.GetJWTToken(user);
			//return Token 
			return Success(result);
		}

		public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var result = await _authenticationServices.GetRefreshToken(request.AccessToken, request.RefreshToken);
			return Success(result);
		}
		#endregion
	}
}
