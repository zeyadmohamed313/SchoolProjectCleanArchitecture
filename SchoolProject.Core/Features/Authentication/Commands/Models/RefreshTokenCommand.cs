using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
	{
		public string AccessToken {  get; set; }
		public string RefreshToken { get; set; }
	}
}
