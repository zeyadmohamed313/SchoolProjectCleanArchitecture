using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Classes
{
    public partial class ClassMappingProfile:Profile
    {
        public ClassMappingProfile()
        {
            ClassQueryMapping();
            ClassCommandMapping();
        }
    }
}
