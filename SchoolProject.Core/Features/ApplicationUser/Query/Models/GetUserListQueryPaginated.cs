using MediatR;
using SchoolProject.Core.Features.ApplicationUser.Query.Results;
using SchoolProject.Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Query.Models
{
	public class GetUserListQueryPaginated:IRequest<PaginatedResult<GetUserListResponsePaginated>>
	{ 
		public int PageNumber {  get; set; }
		public int PageSize { get; set; }

	}
}
