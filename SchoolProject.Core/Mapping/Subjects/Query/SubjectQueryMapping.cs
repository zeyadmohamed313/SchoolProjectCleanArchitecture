using AutoMapper;
using SchoolProject.Core.Features.Subjects.Query.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Subjects
{
    public partial class SubjectMappingProfile : Profile
    {
        public void SubjectQueryMapping()
        {
            CreateMap<Data.Entites.Subjects, GetAllSubjectsResult>().ReverseMap();
            CreateMap<Data.Entites.Subjects, GetSubjectByIdResult>().ReverseMap();
            CreateMap<Data.Entites.Subjects, GetSubjectByInstructorResult>().ReverseMap();
            CreateMap<Data.Entites.Subjects, GetSubjectByStudentResult>().ReverseMap();

        }
    }
}
