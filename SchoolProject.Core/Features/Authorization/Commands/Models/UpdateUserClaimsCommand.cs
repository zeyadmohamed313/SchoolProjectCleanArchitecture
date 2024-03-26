using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand:UpdateUserClaimsRequests,IRequest<Response<string>>
    {
    }
}
