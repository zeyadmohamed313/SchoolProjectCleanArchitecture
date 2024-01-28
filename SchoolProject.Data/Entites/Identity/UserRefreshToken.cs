using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entites.Identity
{
	public class UserRefreshToken
	{
		[Key]
		public int Id { get; set; }
		public int UserId { get; set; }
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
		public string? JwtId { get; set; }
		public bool IsUsed { get; set; }
		public bool IsRevoked { get; set; }
		public DateTime AddedTime { get; set; }
		public DateTime ExpiryDate { get; set; }
		[ForeignKey(nameof(UserId))]
		[InverseProperty(nameof(User.UserRefreshTokens))]
		public virtual User? user { get; set; }
	}
}
