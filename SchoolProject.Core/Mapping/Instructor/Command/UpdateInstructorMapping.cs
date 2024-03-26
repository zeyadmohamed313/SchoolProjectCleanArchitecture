using AutoMapper;
using SchoolProject.Core.Features.Instructor.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SchoolProject.Core.Mapping.Instructor
{
    public partial class InstructorProfile : Profile
    {
        public void UpdateInstructorMapping()
        {
            CreateMap<Data.Entites.Instructor, UpdateInstructorCommandModel>().ReverseMap();
        }
    }
}
