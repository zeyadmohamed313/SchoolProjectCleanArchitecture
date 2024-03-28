using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Classes.Query.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Classes.Query.Models
{
    public class GetClassesByStudentQuery:IRequest<Response<List<GetClassByResult>>>
    {
        public int Id {  get; set; }
        public GetClassesByStudentQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
