using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Command.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class ApplicationUserController : AppControllerBase
	{
		[HttpPost(Router.ApplicationUserRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddUserCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}
}
