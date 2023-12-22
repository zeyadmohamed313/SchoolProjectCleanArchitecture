using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class DepartmentController : AppControllerBase
	{
		[HttpGet]
		[Route(Router.DepartmentRouting.GetByID)]
		public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery Query) 
		{
			return NewResult(await Mediator.Send(Query));
		}
	}
}
