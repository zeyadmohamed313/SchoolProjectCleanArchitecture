using AutoMapper;
using SchoolProject.Core.Features.Classes.Query.Results;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Classes
{
    public partial class ClassMappingProfile : Profile
    {
        public void ClassQueryMapping()
        {
            CreateMap<Class,GetAllClassesResult>().ReverseMap();
            CreateMap<Class, GetClassAvailableSpaceResult>().ReverseMap();
            CreateMap<Class, GetClassByIdResult>().ReverseMap();
            CreateMap<Class, GetClassByResult>().ReverseMap();

        }
    }
}
