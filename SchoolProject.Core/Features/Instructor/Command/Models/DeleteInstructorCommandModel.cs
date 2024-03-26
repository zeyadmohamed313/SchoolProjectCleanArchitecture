using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructor.Command.Models
{
    public class DeleteInstructorCommandModel:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteInstructorCommandModel(int Id)
        {
            this.Id = Id;   
        }
    }
}
