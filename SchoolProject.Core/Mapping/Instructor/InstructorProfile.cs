using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Instructor
{
    public partial class InstructorProfile:Profile
    {
        public InstructorProfile()
        {
            GetByIdMapping();
            AddInstructorMapping();
            GetAllInstructorsMapping();
            UpdateInstructorMapping();
        }
    }
}
