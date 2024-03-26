using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Results;
using SchoolProject.Infrustructure.Context;
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
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationServices : IAuthenticationServices
	{
		#region Feilds
		private readonly JwtSettings _jwtSettings;
		//private readonly ConcurrentDictionary<string, RefreshToken> _UserRefreshToken; // map works in concurrent enviroment
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly ApplicationDbContext _applicationDbContext;
		private readonly IEmailServices _emailServices;
        #endregion
        #region Constructor
        public AuthenticationServices(JwtSettings jwtSettings,
			ConcurrentDictionary<string, RefreshToken> UserRefreshToken ,
			IUnitOfWork unitOfWork, UserManager<User> userManager,
            ApplicationDbContext applicationDbContext,
			IEmailServices emailServices)
        {
			_jwtSettings = jwtSettings;   
			//_UserRefreshToken = UserRefreshToken;
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_applicationDbContext = applicationDbContext;
			_emailServices = emailServices;
        }
		#endregion
		#region HandleFunctions
		public async Task <JwtAuthResult> GetJWTToken(User user)
		{
			//var JwtToken = GenerateJwtToken(user);
			var (JwtToken, AccessToken) = await GenerateJwtToken(user);
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

		

		private async Task<  (JwtSecurityToken,string)> GenerateJwtToken(User user)
		{

			var claims =await GetClaims(user);
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
		private async Task< List<Claim>> GetClaims(User user)
		{
            var Roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
			{
				new Claim(nameof(UserClaimsModel.UserName),user.UserName),
				new Claim(nameof(UserClaimsModel.Email),user.Email),
				new Claim(nameof(UserClaimsModel.PhoneNumber),user.PhoneNumber),
				new Claim(nameof(UserClaimsModel.Id),user.Id.ToString()),
			};
			// adding roles
			foreach(var role in Roles )
			   claims.Add(new Claim(ClaimTypes.Role,role.ToString()));
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            return claims;
		}
		public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken JwtToken,  DateTime? ExpiryDate , string refreshToken)
		{
			// Not Needed For Now var jwtToken = ReadJwtToken(AccessToken);
			
			if (user == null)
			{
				throw new SecurityTokenException("User Is Not Found");
			}
			var (jwtSecurityToken,NewToken)  =await GenerateJwtToken(user); 
			var response = new JwtAuthResult();
			response.AccessToken = NewToken;
			var refreshtokenresult = new RefreshToken();
			refreshtokenresult.TokenString = refreshToken;
			refreshtokenresult.ExpireAt = (DateTime)ExpiryDate; // Converting from DateTime? to DataTime
			var username = JwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.UserName)).Value;
			refreshtokenresult.UserName = username;
			response.refreshToken=refreshtokenresult;
			return response;
		}
		public JwtSecurityToken ReadJwtToken(string AccessToken)
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

		public async Task<(string,DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken)
		{
			if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
			{
				return ("AlgorithmIsWrong",null);
			}
			if (jwtToken.ValidTo > DateTime.UtcNow)
			{
				 return ("TokenIsNotExpired", null);
			}

			//Get User
			var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id)).Value;
			var userRefreshToken = await _unitOfWork.RefreshToken.GetTableNoTracking()
											 .FirstOrDefaultAsync(x => x.Token == AccessToken
											 && x.RefreshToken == RefreshToken &&
											 x.UserId == int.Parse(userId));
			if (userRefreshToken == null)
			{
			    return ("RefreshTokenIsNotFound", null);
			}

			if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
			{
				userRefreshToken.IsRevoked = true;
				userRefreshToken.IsUsed = false;
				await _unitOfWork.RefreshToken.UpdateAsync(userRefreshToken);
				return ("RefreshTokenIsExpired", null);
			}
			var ExpireDate = userRefreshToken.ExpiryDate;
			return (userId,ExpireDate);
		}

        public async Task<string> ConfirmEmail(int UserId, string Code)
        {
            if (UserId == null || Code == null)
                return "ErrorWhenConfirmEmail";
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, Code);
            if (!confirmEmail.Succeeded)
                return "ErrorWhenConfirmEmail";
            return "Success";
        }

        public async Task<string> ResetPasswordCode(string Email)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                //user
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                //Generate Random Number

                //Random generator = new Random();
                //string randomNumber = generator.Next(0, 1000000).ToString("D6");
                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                //update User In Database Code
                user.Code = randomNumber;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return "ErrorInUpdateUser";
                var message = "Code To Reset Passsword : " + user.Code;
                //Send Code To  Email 
                await _emailServices.SendEmail(user.Email, message, "Reset Password");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            //Get User
            //user
            var user = await _userManager.FindByEmailAsync(Email);
            //user not Exist => not found
            if (user == null)
                return "UserNotFound";
            //Decrept Code From Database User Code
            var userCode = user.Code;
            //Equal With Code
            if (userCode == Code) return "Success";
            return "Failed";
        }

        public async Task<string> ResetPassword(string Email, string Password)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                await _userManager.RemovePasswordAsync(user);
                if (!await _userManager.HasPasswordAsync(user))
                {
                    await _userManager.AddPasswordAsync(user, Password);
                }
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        #endregion
    }
}
