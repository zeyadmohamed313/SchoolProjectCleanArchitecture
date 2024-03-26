using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Wrapper;
using SchoolProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
    public class GetAllDepartmentsQuery : IRequest<PaginatedResult<GetAllDepartmentResult>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DepartmentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
