using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Command.Models;
using SchoolProject.Core.Features.ApplicationUser.Query.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class ApplicationUserController : AppControllerBase
	{
		[HttpPost(Router.ApplicationUserRouting.Create)]
        [SwaggerOperation(Summary = "انشاء مستخدم", OperationId = "CreateUser")]

        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpGet(Router.ApplicationUserRouting.Paginated)]
        [SwaggerOperation(Summary = "عرض المستخدمين", OperationId = "Paginated")]

        public async Task<IActionResult>Paginated([FromQuery] GetUserListQueryPaginated query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}
		[HttpGet(Router.ApplicationUserRouting.GetByID)]
        [SwaggerOperation(Summary = " عرض المستخدمين باستخدام الرقم المعرف", OperationId = "GetStudentByID")]

        public async Task<IActionResult> GetUserByID([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetUserByIdQuery(id));
			return NewResult(response);
		}
		[HttpPut(Router.ApplicationUserRouting.Edit)]
        [SwaggerOperation(Summary = " تعديل مستخدم", OperationId = "EditUser")]

        public async Task<IActionResult> Edit([FromBody] UpdateUserCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpDelete(Router.ApplicationUserRouting.Delete)]
        [SwaggerOperation(Summary = " حذف مستخدم", OperationId = "DeleteUser")]

        public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteUserCommand(id));
			return NewResult(response);
		}
		[HttpPut(Router.ApplicationUserRouting.ChangePassword)]
        [SwaggerOperation(Summary = "تغيير الرقم السري", OperationId = "ChangePassword")]

        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}

	
}
