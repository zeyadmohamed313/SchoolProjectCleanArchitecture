using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Queries.Models
{
    public class ConfirmResetPasswordQuery:IRequest<Response<string>>
    {
        public string Code {  get; set; }
        public string Email { get; set; }
    }
}
