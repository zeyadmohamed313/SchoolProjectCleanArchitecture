using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
	public class GetDepartmentByIdQuery:IRequest<Response<GetDepartmentByIdResponse>>
	{
		public int Id {  get; set; }
		public int StudentPageNumber {  get; set; }
		public int StudentPageSize { get; set; }
	}
}
