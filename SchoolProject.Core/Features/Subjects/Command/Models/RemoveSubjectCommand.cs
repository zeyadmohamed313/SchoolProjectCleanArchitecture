using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Command.Models
{
    public class RemoveSubjectCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public RemoveSubjectCommand(int Id)
        {
            this.Id = Id;
        }
    }
}
