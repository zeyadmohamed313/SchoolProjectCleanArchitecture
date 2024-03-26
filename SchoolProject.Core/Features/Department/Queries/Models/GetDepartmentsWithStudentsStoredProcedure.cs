
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results.StoredProceduresResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
    public class GetDepartmentsWithStudentsStoredProcedure:IRequest<Response<List<GetDepartmentWithStudentsResult>>>
    {
    }
}
