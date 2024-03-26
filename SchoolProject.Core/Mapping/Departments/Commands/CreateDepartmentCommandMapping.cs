using AutoMapper;
using SchoolProject.Core.Features.Department.Commands.Models;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentsProfile : Profile
    {
        public void CreateDepartmentCommandMapping()
        {
            CreateMap<CreateDepartmentCommand, Department>().ReverseMap();

        }
    }
}
