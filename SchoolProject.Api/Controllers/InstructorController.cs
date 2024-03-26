using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Instructor.Command.Models;
using SchoolProject.Core.Features.Instructor.Query.Models;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpGet]
        [Route(Router.InstructorRouting.GetByID)]
        [SwaggerOperation(Summary = "البحث بواسطة الرقم التعريفي", OperationId = "InstructorById")]

        public async Task<IActionResult> GetInstructorById([FromQuery] int Id)
        {
            return NewResult(await Mediator.Send(new GetInstructorByIdModel(Id)));
        }

        [HttpGet]
        [Route(Router.InstructorRouting.GetAllInstructors)]
        [SwaggerOperation(Summary = "اظهار كل المدرسين", OperationId = "GetAllInstructors")]

        public async Task<IActionResult> GetAllInstructors()
        {
            return NewResult(await Mediator.Send(new GetAllInstructorsModel()));
        }

        [HttpPost]
        [Route(Router.InstructorRouting.AddInstructor)]
        [SwaggerOperation(Summary = "اضافة مدرس جديد", OperationId = "Add New Instructor")]

        public async Task<IActionResult> AddInstructor([FromBody] AddInstructorModel Command)
        {
            return NewResult(await Mediator.Send(Command));
        }
        [HttpPut]
        [Route(Router.InstructorRouting.UpdateInstructor)]
        [SwaggerOperation(Summary = "تعديل بيانات مدرس", OperationId = "update an instructor")]

        public async Task<IActionResult> UpdateInstructor([FromBody] UpdateInstructorCommandModel Command)
        {
            return NewResult(await Mediator.Send(Command));
        }
        [HttpDelete]
        [Route(Router.InstructorRouting.DeleteInstructor)]
        [SwaggerOperation(Summary = "حذف مدرس", OperationId = "Delete an Instructor")]

        public async Task<IActionResult> DeleteInstructor([FromQuery] int id)
        {
            return NewResult(await Mediator.Send(new DeleteInstructorCommandModel(id)));
        }


    }
}
