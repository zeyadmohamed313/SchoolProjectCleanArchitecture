using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Classes.Commands.Models
{
    public class RemoveClassCommand:IRequest<Response<string>>
    {
        public int Id {  get; set; }
        public RemoveClassCommand(int Id)
        {
            this.Id = Id;
        }
    }
}
