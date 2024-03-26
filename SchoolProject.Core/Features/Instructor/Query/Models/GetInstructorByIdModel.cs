using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructor.Query.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructor.Query.Models
{
    public class GetInstructorByIdModel:IRequest<Response<GetInstructorByIdResult>>
    {
        public int Id { get; set; }
        public GetInstructorByIdModel(int Id)
        {
            this.Id = Id;
        }
    }
}
