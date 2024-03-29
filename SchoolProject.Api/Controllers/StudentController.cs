using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.ActionFilters;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	[Authorize(Roles ="Admin")]
	public class StudentController : AppControllerBase
	{
		
		[HttpGet(Router.StudentRouting.List)]
		// to access it , you will need the admin and the user claims 
		[Authorize(Roles ="User")] //this will not work with changed token here so i must use filter
		[ServiceFilter(typeof(OnlyUserFilter))]
		public async Task<IActionResult>GetStudentList()
		{
			var response = await Mediator.Send(new GetStudentListQuery());
			return NewResult(response);
		}
		[Authorize]
		[HttpGet(Router.StudentRouting.Paginated)]
		public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}
		[HttpGet(Router.StudentRouting.GetByID)]
		public async Task<IActionResult> GetStudentByID([FromRoute]int id)
		{
			var response = await Mediator.Send(new GetStudentByIDQuery(id));
			return NewResult(response);
		}
		[Authorize(Policy ="Create")] // Now only The Create Claim Owners can Create Add new student
		[HttpPost(Router.StudentRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddStudentCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
        [Authorize(Policy = "Edit")]
        [HttpPut(Router.StudentRouting.Edit)]
		public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
        [Authorize(Policy = "Delete")]
        [HttpDelete(Router.StudentRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteStudentCommand(id));
			return NewResult(response);
		}
	}
}
