using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Command.Models;
using SchoolProject.Core.Features.ApplicationUser.Query.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
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

		[HttpGet(Router.ApplicationUserRouting.Paginated)]
		public async Task<IActionResult>Paginated([FromQuery] GetUserListQueryPaginated query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}
		[HttpGet(Router.ApplicationUserRouting.GetByID)]
		public async Task<IActionResult> GetStudentByID([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetUserByIdQuery(id));
			return NewResult(response);
		}
		[HttpPut(Router.ApplicationUserRouting.Edit)]
		public async Task<IActionResult> Edit([FromBody] UpdateUserCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}

	
}
