using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Department.Commands.Models;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Data.AppMetaData;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class DepartmentController : AppControllerBase
	{
		[HttpGet]
		[Route(Router.DepartmentRouting.GetByID)]
        [SwaggerOperation(Summary = "البحث بواسطة الرقم التعريفي", OperationId = "DepartmentById")]

        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery Query) 
		{
			return NewResult(await Mediator.Send(Query));
		}
		[HttpPost]
        [Route(Router.DepartmentRouting.Create)]
        [SwaggerOperation(Summary = " اضافة قسم جديد", OperationId = "Create New Department")]

        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand Command)
        {
            return NewResult(await Mediator.Send(Command));
        }

        [HttpGet]
        [Route(Router.DepartmentRouting.GetAllDepartmentsPaginated)]
        [SwaggerOperation(Summary = "عرض كل الاقسام", OperationId = "Show all department paginated")]

        public async Task<IActionResult> GetAllTheDepartmentPaginated([FromQuery] GetAllDepartmentsQuery Query)
        {
            var response = await Mediator.Send(Query);
            return Ok(response);
        }
        [HttpGet]
        [Route(Router.DepartmentRouting.GetDepartmentWithStudentsStoredProcedure)]
        [SwaggerOperation(Summary = " عرض كل الاقسام باستخدام الدوال المحفوظة", OperationId = "GetDepartmentWithStudentsStoredProcedure")]

        public async Task<IActionResult> GetDepartmentWithStudentsStoredProcedure()
        {
            var response = await Mediator.Send(new GetDepartmentsWithStudentsStoredProcedure());
            return Ok(response);
        }
        [HttpGet(Router.DepartmentRouting.GetDepartmentStudentsCount)]
        [SwaggerOperation(Summary = "الاقسام مع عدد الطلبة", OperationId = "GetDepartmentsWithStudentCount")]

        public async Task<IActionResult> GetDepartmentStudentsCount()
        {
            return NewResult(await Mediator.Send(new GetDepartmentsWithStudentCountModel()));
        }

        [HttpPut]
        [Route(Router.DepartmentRouting.Update)]
        [SwaggerOperation(Summary = "تعديل قسم", OperationId = "update Department")]

        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentCommand Command)
        {
            return NewResult(await Mediator.Send(Command));
        }

        [HttpDelete]
        [Route(Router.DepartmentRouting.Delete)]
        [SwaggerOperation(Summary = "حذف قسم ", OperationId = "Delete Department")]

        public async Task<IActionResult> UpdateDepartment([FromQuery] int id)
        {
            return NewResult(await Mediator.Send(new DeleteDepartmentCommand(id)));
        }
    }
}
