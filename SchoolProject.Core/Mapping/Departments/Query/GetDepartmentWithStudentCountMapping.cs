using AutoMapper;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Data.Entites.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentsProfile : Profile
    {
        public void GetDepartmentWithStudentCountMapping()
        {
            CreateMap<ViewDepartment, GetDepartmentWithStudentCountResult>()
            .ForMember(dist => dist.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
            .ForMember(dist => dist.StudentCount, opt => opt.MapFrom(src => src.StudentCount))
            .ReverseMap();
        }
    }
}
