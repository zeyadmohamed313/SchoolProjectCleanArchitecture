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
			bool signInResult = await _userManager.CheckPasswordAsync(user, request.Password);

			//var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			//if Failed Return Passord is wrong
			if (!signInResult) return BadRequest<JwtAuthResult>(_localizer[SharedResoursesKeys.PasswordNotCorrect]);
			
			//Generate Token
			var result = await _authenticationServices.GetJWTToken(user);
			//return Token 
			return Success(result);
		}

		public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var jwtToken = _authenticationServices.ReadJwtToken(request.AccessToken);
			var userIdAndExpireDate = await _authenticationServices.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
			switch (userIdAndExpireDate)
			{
				case ("AlgorithmIsWrong",null): return Unauthorized<JwtAuthResult>(_localizer[SharedResoursesKeys.AlgorithmIsWrong]);
				case ("TokenIsNotExpired",null): return Unauthorized<JwtAuthResult>(_localizer[SharedResoursesKeys.TokenIsNotExpired]);
				case ("RefreshTokenIsNotFound",null): return Unauthorized<JwtAuthResult>(_localizer[SharedResoursesKeys.RefreshTokenIsNotFound]);
				case ("RefreshTokenIsExpired",null): return Unauthorized<JwtAuthResult>(_localizer[SharedResoursesKeys.RefreshTokenIsExpired]);
			}
			var (UserId, ExpireDate) = userIdAndExpireDate;
			var user = await _userManager.FindByIdAsync(UserId);
			if (user == null)
			{
				return NotFound<JwtAuthResult>();
			}
			var result = await _authenticationServices.GetRefreshToken(user, jwtToken, ExpireDate, request.RefreshToken);
			return Success(result);
		}
		#endregion
	}
}
