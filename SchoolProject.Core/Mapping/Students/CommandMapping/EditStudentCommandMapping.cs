using AutoMapper;
using SchoolProject.Core.Features.Students.Commands.Models;
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
		public void EditStudentMapping()
		{
			CreateMap<EditStudentCommand, Student>()
			  .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId))
			  .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id))
			  .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
			  .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn));

		}
	}
}
