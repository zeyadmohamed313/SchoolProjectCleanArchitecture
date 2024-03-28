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
    public class GetAllSubjectsQuery:IRequest<Response<List<GetAllSubjectsResult>>>
    {
    }
}
