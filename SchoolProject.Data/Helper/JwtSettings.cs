using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Helper
{
	public class JwtSettings
	{
		public string Secret { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public bool ValidateIssuer { get; set; }
		public bool ValidateAudience { get; set; }
		public bool ValidateLifeTime { get; set; }
		public bool ValidateIssuerSigningKey { get; set; }
		public int AccessTokenExpireDate {  get; set; }
		public int RefreshTokenExpireDate { get; set; }
	}
}
