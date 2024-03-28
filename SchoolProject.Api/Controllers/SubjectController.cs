using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Classes.Commands.Models;
using SchoolProject.Core.Features.Classes.Query.Models;
using SchoolProject.Core.Features.Subjects.Command.Models;
using SchoolProject.Core.Features.Subjects.Query.Models;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class SubjectController : AppControllerBase
    {
        [HttpGet]
        [Route(Router.SubjectRouting.List)]
        [SwaggerOperation(Summary = "عرض قائمة المواد", OperationId = "GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            return NewResult(await Mediator.Send(new GetAllSubjectsQuery()));
        }

        [HttpGet]
        [Route(Router.SubjectRouting.GetByID)]
        [SwaggerOperation(Summary = "عرض مادة بالرقم المعرف", OperationId = "GetSubjectById")]
        public async Task<IActionResult> GetClassById([FromQuery] int Id)
        {
            return NewResult(await Mediator.Send(new GetSubjectByIdQuery(Id)));
        }

        

        [HttpGet]
        [Route(Router.SubjectRouting.GetSubjectsForStudent)]
        [SwaggerOperation(Summary = "عرض المواد الخاصة باحد الطلاب", OperationId = "GetSubjectsForStudent")]
        public async Task<IActionResult> GetSubjectsForStudent([FromQuery] int Id)
        {
            return NewResult(await Mediator.Send(new GetSubjectsByStudentQuery(Id)));
        }

        [HttpGet]
        [Route(Router.SubjectRouting.GetSubjectsForInstructor)]
        [SwaggerOperation(Summary = "عرض المواد الخاصة باحد المدرسين", OperationId = "GetSubjectsForInstructor")]
        public async Task<IActionResult> GetSubjectsForInstructor([FromQuery] int Id)
        {
            return NewResult(await Mediator.Send(new GetSubjectsByInstructorQuery(Id)));
        }

        [HttpPost]
        [Route(Router.SubjectRouting.AddSubject)]
        [SwaggerOperation(Summary = "اضافة مادة", OperationId = "AddSubject")]
        public async Task<IActionResult> AddSubject([FromBody] AddSubjectCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Router.SubjectRouting.AddSubjectToStudent)]
        [SwaggerOperation(Summary = "اضافة مادة لاحد الطلاب", OperationId = "AddSubjectToStudent")]
        public async Task<IActionResult> AddSubjectToStudent([FromBody] AddSubjectToStudentCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Router.SubjectRouting.AddSubjectToInstructor)]
        [SwaggerOperation(Summary = "اضافة مادة لاحد المدرسين", OperationId = "AddSubjectToInstructor")]
        public async Task<IActionResult> AddSubjectToInstructor([FromBody] AddSubjectToInstructorCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Router.SubjectRouting.RemoveSubjectFromStudent)]
        [SwaggerOperation(Summary = "حذف مادة من احد الطلاب", OperationId = "RemoveSubjectFromStudent")]
        public async Task<IActionResult> RemoveSubjectFromStudent([FromBody] RemoveSubjectFromStudentCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut]
        [Route(Router.SubjectRouting.RemoveSubjectFromInstructor)]
        [SwaggerOperation(Summary = "حذف مادة من احد المدرسين", OperationId = "RemoveSubjectFromInstructor")]
        public async Task<IActionResult> RemoveSubjectFromInstructor([FromBody] RemoveSubjectFromInstructorCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }


        [HttpDelete]
        [Route(Router.SubjectRouting.DeleteSubject)]
        [SwaggerOperation(Summary = "حذف مادة", OperationId = "DeleteSubject")]
        public async Task<IActionResult> DeleteClass([FromBody] RemoveSubjectCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

    }
}
