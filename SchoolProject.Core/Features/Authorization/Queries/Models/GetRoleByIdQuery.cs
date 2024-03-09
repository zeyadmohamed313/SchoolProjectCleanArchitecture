
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery:IRequest<Response<GetRoleByIdResult>>
    {
        public  int Id {  get; set; }
        public GetRoleByIdQuery(int id) 
        {
            Id= id;
        }

    }
}
