using SchoolProject.Data.Entites.Identity;
using SchoolProject.Data.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationServices
	{
		public Task<JwtAuthResult> GetJWTToken(User user);
		public  Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken JwtToken, DateTime? ExpiryDate, string refreshToken);
		public Task<(string,DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
		public Task<string> ValidateToken(string accessToken);
		public JwtSecurityToken ReadJwtToken(string AccessToken);
		public Task<string> ConfirmEmail(int UserId, string Code);
		public Task<string> ResetPasswordCode(string Email);
		public  Task<string> ConfirmResetPassword(string Code, string Email);
		public  Task<string> ResetPassword(string Email, string Password);




    }
}
