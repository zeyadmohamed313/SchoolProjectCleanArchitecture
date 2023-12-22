using AutoMapper;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Students
{
	public partial class StudentProfile
	{
		public void GetStudentIdMapping()
		{
			CreateMap<Student, GetSingleStudentResponse>()
			  .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Localize(src.Department.DNameAr,src.Department.DNameEn))).
			  ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr,src.NameEn)));
		}
	}
}
