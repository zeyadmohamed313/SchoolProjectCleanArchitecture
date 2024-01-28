using SchoolProject.Data.Entites.Identity;
using SchoolProject.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
	public interface IAuthenticationServices
	{
		public Task<JwtAuthResult> GetJWTToken(User user);
		public Task<JwtAuthResult> GetRefreshToken(string AccessToken, string RefreshToken);
		public  Task<string> ValidateToken(string accessToken);

	}
}
