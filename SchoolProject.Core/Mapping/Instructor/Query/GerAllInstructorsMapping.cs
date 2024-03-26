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
        public void GetAllInstructorsMapping()
        {
            CreateMap<Data.Entites.Instructor, GetAllInstructorsResult>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.ENameAr, src.ENameEn)))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Localize(src.Department.DNameAr, src.Department.DNameEn)))
                ;//.ReverseMap();
			//.ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Localize(src.Department.DNameAr, src.Department.DNameEn)))
            
        }
    }
}
