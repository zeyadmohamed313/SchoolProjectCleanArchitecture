using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Command.Models
{
	public class UpdateUserCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string? Address { get; set; }
		public string? Country { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
