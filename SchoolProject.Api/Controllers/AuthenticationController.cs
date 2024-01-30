using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class AuthenticationController : AppControllerBase
	{
		[HttpPost(Router.AuthenticationRouting.SignIn)]
		public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPost(Router.AuthenticationRouting.RefreshToken)]
		public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpGet(Router.AuthenticationRouting.ValidateToken)]
		public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery Query)
		{
			var response = await Mediator.Send(Query);
			return NewResult(response);
		}
	}
}
