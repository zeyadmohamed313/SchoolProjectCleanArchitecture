using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Command.Models
{
	public class DeleteUserCommand:IRequest<Response<string>>
	{
		public int Id {  get; set; }
        public DeleteUserCommand(int Id)
        {
            this.Id = Id;
        }
    }
}
