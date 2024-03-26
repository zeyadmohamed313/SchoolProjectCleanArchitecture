using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Email.Command.Model
{
    public class SendEmailCommand:IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Massege { get; set; }
    }
}
