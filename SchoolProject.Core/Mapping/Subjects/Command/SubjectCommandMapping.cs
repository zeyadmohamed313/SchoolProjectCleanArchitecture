using AutoMapper;
using SchoolProject.Core.Features.Subjects.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Subjects
{
    public partial class SubjectMappingProfile : Profile
    {
        public void SubjectCommandMapping()
        {
            CreateMap<Data.Entites.Subjects,AddSubjectCommand>().ReverseMap();
        }
    }
}
