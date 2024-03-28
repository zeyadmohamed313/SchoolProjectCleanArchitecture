using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Classes.Commands.Models
{
    public class AddInstructorToClassCommand:IRequest<Response<string>>
    {
        public int InsId {  get; set; }
        public int ClassId {  get; set; }
    }
}
