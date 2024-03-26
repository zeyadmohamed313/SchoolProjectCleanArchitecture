using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Results
{
    public class ManageUserRolesResult
    {
        public int UserId { get; set; }
        public List<UserRoles> Roles { get; set; }

    }
    // top level class cannot be any thing other internal or public
    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
