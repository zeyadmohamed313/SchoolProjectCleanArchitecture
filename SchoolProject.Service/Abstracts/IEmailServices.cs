using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IEmailServices
    {
        public Task<string> SendEmail(string email, string Message, string? reason);

    }
}
