using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace SchoolProject.Data.Entites.Identity
{
	public class User : IdentityUser<int>
	{
        public string FullName { get; set; }
        public string? Address {  get; set; }
		public string? Country {  get; set; }
	}
}
