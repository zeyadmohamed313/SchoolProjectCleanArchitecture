using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Query.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Query.Models
{
    public class GetSubjectByIdQuery:IRequest<Response<GetSubjectByIdResult>>
    {
        public int Id { get; set; }
        public GetSubjectByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
