using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Roles
{
    public partial class RolesProfile:Profile
    {
        public RolesProfile() 
        {
            GetRolesListMapping();
            GetRoleByIdMapping();
        }
    }
}
