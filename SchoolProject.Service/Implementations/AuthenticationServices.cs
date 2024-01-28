using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrustructure.UnitOfwork;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
	public class AuthenticationServices : IAuthenticationServices
	{
		#region Feilds
		private readonly JwtSettings _jwtSettings;
		//private readonly ConcurrentDictionary<string, RefreshToken> _UserRefreshToken; // map works in concurrent enviroment
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
        #endregion
        #region Constructor
        public AuthenticationServices(JwtSettings jwtSettings,
			ConcurrentDictionary<string, RefreshToken> UserRefreshToken ,
			IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
			_jwtSettings = jwtSettings;   
			//_UserRefreshToken = UserRefreshToken;
			_unitOfWork = unitOfWork;
			_userManager = userManager;
        }
		#endregion
		#region HandleFunctions
		public async Task <JwtAuthResult> GetJWTToken(User user)
		{
			//var JwtToken = GenerateJwtToken(user);
			var (JwtToken, AccessToken) =  GenerateJwtToken(user);
			var refreshToken = GetRefreshToken(user.UserName);
			var userRefreshToken = new UserRefreshToken
			{
				AddedTime = DateTime.Now,
				ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
				IsUsed = true,
				IsRevoked = false,
				JwtId = JwtToken.Id,
				RefreshToken = refreshToken.TokenString,
				Token = AccessToken,
				UserId = user.Id
			};
			await _unitOfWork.RefreshToken.AddAsync(userRefreshToken);
			var response = new JwtAuthResult()
			{
				AccessToken = AccessToken,
			    refreshToken = refreshToken,
			};
			return response;
		}

		

		private (JwtSecurityToken,string) GenerateJwtToken(User user)
		{
			var claims = GetClaims(user);
			var JwtToken = new JwtSecurityToken(
				_jwtSettings.Issuer,
				_jwtSettings.Audience,
				claims,
				expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpireDate),
				signingCredentials:
				new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
				);
			var AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
			return (JwtToken,AccessToken);
		}
		private RefreshToken GetRefreshToken(string UserName)
		{
			var refreshroken = new RefreshToken()
			{
				TokenString = GenerateRefreshToken(),
				UserName = UserName,
				ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
			};
			//_UserRefreshToken.AddOrUpdate(refreshroken.TokenString, refreshroken,(s,t)=>refreshroken);
			return refreshroken;
		}
		private string GenerateRefreshToken()
		{
			var RandomNumber = new byte[32];
			var RandomNumberGenerate = RandomNumberGenerator.Create();
			RandomNumberGenerate.GetBytes(RandomNumber);
			return Convert.ToBase64String(RandomNumber);
		}
		private List<Claim> GetClaims(User user)
		{
			var claims = new List<Claim>()
			{
				new Claim(nameof(UserClaimsModel.UserName),user.UserName),
				new Claim(nameof(UserClaimsModel.Email),user.Email),
				new Claim(nameof(UserClaimsModel.PhoneNumber),user.PhoneNumber),
				new Claim(nameof(UserClaimsModel.Id),user.Id.ToString()),
			};
			return claims;
		}
		public async Task<JwtAuthResult> GetRefreshToken(string AccessToken, string RefreshToken)
		{
			var jwtToken = ReadJwtToken(AccessToken);
			if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
			{
				throw new SecurityTokenException("AlgorithmIsWrong");
			}
			if (jwtToken.ValidTo > DateTime.UtcNow)
			{
				throw new SecurityTokenException("TokenIsNotExpired");
			}

			//Get User
			var username = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.UserName)).Value;
			var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id)).Value;
			var userRefreshToken = await _unitOfWork.RefreshToken.GetTableNoTracking()
											 .FirstOrDefaultAsync(x => x.Token == AccessToken
											 && x.RefreshToken == RefreshToken &&
											 x.UserId == int.Parse(userId));
			if (userRefreshToken == null)
			{
				throw new SecurityTokenException("RefreshTokenIsNotFound");
			}

			if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
			{
				userRefreshToken.IsRevoked = true;
				userRefreshToken.IsUsed = false;
				await _unitOfWork.RefreshToken.UpdateAsync(userRefreshToken);
				throw new SecurityTokenException("RefreshTokenIsExpired");
			}
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				throw new SecurityTokenException("User Is Not Found");
			}
			var (jwtSecurityToken,NewToken)  = GenerateJwtToken(user); 
			var response = new JwtAuthResult();
			response.AccessToken = NewToken;
			var refreshtokenresult = new RefreshToken();
			refreshtokenresult.TokenString = RefreshToken;
			refreshtokenresult.ExpireAt = userRefreshToken.ExpiryDate;
			refreshtokenresult.UserName = username;
			response.refreshToken=refreshtokenresult;
			return response;
		}
		private JwtSecurityToken ReadJwtToken(string AccessToken)
		{
			if (string.IsNullOrEmpty(AccessToken))
			{
				throw new ArgumentNullException(nameof(AccessToken));
			}
			var handler = new JwtSecurityTokenHandler();
			var response = handler.ReadJwtToken(AccessToken);
			return response;
		}
		public async Task<string> ValidateToken(string accessToken)
		{
			var handler = new JwtSecurityTokenHandler();
			var parameters = new TokenValidationParameters
			{
				ValidateIssuer = _jwtSettings.ValidateIssuer,
				ValidIssuers = new[] { _jwtSettings.Issuer },
				ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
				ValidAudience = _jwtSettings.Audience,
				ValidateAudience = _jwtSettings.ValidateAudience,
				ValidateLifetime = _jwtSettings.ValidateLifeTime,
			};
			try
			{
				var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

				if (validator == null)
				{
					return "InvalidToken";
				}

				return "NotExpired";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
		#endregion
	}
}
