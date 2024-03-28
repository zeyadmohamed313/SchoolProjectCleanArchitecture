using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Command.Models
{
    public class AddSubjectToStudentCommand:IRequest<Response<string>>
    {
        public int StudentId { get; set; }
        public int SubjectId {  get; set; }
    }
}
