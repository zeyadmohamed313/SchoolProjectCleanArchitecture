using AutoMapper;
using SchoolProject.Core.Features.Department.Queries.Results;
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
		public void GetDepartmentByIdMapping()
		{
			CreateMap<Department, GetDepartmentByIdResponse>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
				.ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor.Localize(src.Instructor.ENameAr, src.Instructor.ENameEn)))
				.ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.DepartmentSubjects))
				//.ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
				.ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.Instructors));

			CreateMap<DepartmetSubject, SubjectResponse>()
				 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
				 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));

			// we used pagination instead of it
			/*CreateMap<Student, StudentResponse>()
		       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
			   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
			*/
			CreateMap<Data.Entites.Instructor, InstructorResponse>()
				 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
				 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.ENameAr, src.ENameEn)));
		}
	}
}
