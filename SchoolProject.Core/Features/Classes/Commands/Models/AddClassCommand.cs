using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Classes.Commands.Models
{
    public class AddClassCommand:IRequest<Response<string>>
    {
        public string Name { get; set; }
        public int Capacity {  get; set; }
    }
}
