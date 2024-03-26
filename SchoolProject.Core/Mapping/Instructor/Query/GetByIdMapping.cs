using AutoMapper;
using SchoolProject.Core.Features.Instructor.Query.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Instructor
{
    public partial class InstructorProfile : Profile
    {
        public void GetByIdMapping()
        {
            CreateMap<Data.Entites.Instructor,GetInstructorByIdResult>()
            
            .ReverseMap();
        }
    }
}
