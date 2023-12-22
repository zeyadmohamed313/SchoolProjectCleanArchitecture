using Azure;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Results;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
	public class GetStudentByIDQuery: IRequest<Bases.Response<GetSingleStudentResponse>>
	{
		public int Id {  get; set; }
        public GetStudentByIDQuery(int id)
        {
            Id = id;
        }
    }
}
