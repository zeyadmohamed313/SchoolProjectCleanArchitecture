using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Classes.Commands.Models;
using SchoolProject.Core.Features.Classes.Query.Models;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class ClassController : AppControllerBase
    {
        [HttpGet]
        [Route(Router.ClassRouting.List)]
        [SwaggerOperation(Summary = "عرض قائمة الفصول", OperationId = "GetAllClasses")]
        public async Task<IActionResult> GetAllClasses()
        {
            return NewResult(await Mediator.Send(new GetAllClassesQuery()));
        }

        [HttpGet]
        [Route(Router.ClassRouting.GetByID)]
        [SwaggerOperation(Summary = "عرض فصل بالرقم المعرف", OperationId = "GetClassByID")]
        public async Task<IActionResult> GetClassById([FromQuery] int Id)
        {
            return NewResult(await Mediator.Send(new GetClassByIdQuery(Id)));
        }

        [HttpGet]
        [Route(Router.ClassRouting.GetAvailableSpaces)]
        [SwaggerOperation(Summary = "عرض الاماكن الفارغة باحد الفصول", OperationId = "GetAvailableSpaceInSomeClass")]
        public async Task<IActionResult> GetAvailablePlaces([FromQuery] int Id)
        {
            return NewResult(await Mediator.Send(new GetClassAvailableSpaceQuery(Id)));
        }

        [HttpGet]
        [Route(Router.ClassRouting.GetClassesForStudent)]
        [SwaggerOperation(Summary = "عرض الفصول الخاصة باحد الطلاب", OperationId = "GetClassesForStudent")]
        public async Task<IActionResult> GetClassesForStudent([FromQuery] int Id)
        {
            return NewResult(await Mediator.Send(new GetClassesByStudentQuery(Id)));
        }

        [HttpGet]
        [Route(Router.ClassRouting.GetClassesForInstructor)]
        [SwaggerOperation(Summary = "عرض الفصول الخاصة باحد المدرسين", OperationId = "GetClassesForInstructor")]
        public async Task<IActionResult> GetClassesForInstructor([FromQuery] int Id)
        {
            return NewResult(await Mediator.Send(new GetClassesByInsturctorQuery(Id)));
        }

        [HttpPost]
        [Route(Router.ClassRouting.AddClass)]
        [SwaggerOperation(Summary = "اضافة فصل", OperationId = "AddClass")]
        public async Task<IActionResult> AddClass([FromBody] AddClassCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Router.ClassRouting.AddStudentToClass)]
        [SwaggerOperation(Summary = "اضافة طالب لاحد الفصول", OperationId = "AddStudentToClass")]
        public async Task<IActionResult> AddStudentToClass([FromBody] AddStudentToClassCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Router.ClassRouting.AddInstructorToClass)]
        [SwaggerOperation(Summary = "اضافة مدرس لاحد الفصول", OperationId = "AddInstructorToClass")]
        public async Task<IActionResult> AddInstructorToClass([FromBody] AddInstructorToClassCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Router.ClassRouting.RemoveStudentFromClass)]
        [SwaggerOperation(Summary = "حذف طالب من احد الفصول", OperationId = "RemoveStudentFromClass")]
        public async Task<IActionResult> RemoveStudentFromClass([FromBody] RemoveStudentFromClassCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Router.ClassRouting.RemoveInstructorFromClass)]
        [SwaggerOperation(Summary = "حذف مدرس من احد الفصول", OperationId = "RemoveInstructorFromClass")]
        public async Task<IActionResult> RemoveInstructorFromClass([FromBody] RemoveInstructorFromClassCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }


        [HttpDelete]
        [Route(Router.ClassRouting.DeleteClass)]
        [SwaggerOperation(Summary = "حذف فصل", OperationId = "DeleteClass")]
        public async Task<IActionResult> DeleteClass([FromBody] RemoveClassCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }




    }
}
