using SchoolProject.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IApplicationUserServices
    {
        public Task<string> AddUserAsync(User user, string password);

    }
}
