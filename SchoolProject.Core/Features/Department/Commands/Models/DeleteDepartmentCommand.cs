using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Commands.Models
{
    public class DeleteDepartmentCommand:IRequest<Response<string>>
    {
        public int DID {  get; set; }
        public DeleteDepartmentCommand(int dID)
        {
            DID = dID;
        }
    }
}
