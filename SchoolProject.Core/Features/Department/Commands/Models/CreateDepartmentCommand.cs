using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Commands.Models
{
    public class CreateDepartmentCommand:IRequest<Response<string>>
    {
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
    }
}
