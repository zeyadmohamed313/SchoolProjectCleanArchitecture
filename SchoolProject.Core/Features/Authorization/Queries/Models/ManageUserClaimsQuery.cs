using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery:IRequest<Response<ManageUserClaimsResults>>
    {
        public int Id { get; set; }
        public ManageUserClaimsQuery(int Id)
        { 
            this.Id = Id;
        }
    }
}
