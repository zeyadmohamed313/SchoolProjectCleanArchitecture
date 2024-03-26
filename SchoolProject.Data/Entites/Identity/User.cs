using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
namespace SchoolProject.Data.Entites.Identity
{
	public class User : IdentityUser<int>
	{
		public User() 
		{
			UserRefreshTokens = new HashSet<UserRefreshToken>();
		}
        public string FullName { get; set; }
        public string? Address {  get; set; }
		public string? Country {  get; set; }
		[EncryptColumn]
		public string? Code {  get; set; }
		[InverseProperty("user")]
		public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
	}
}
