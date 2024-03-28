using AutoMapper;
using SchoolProject.Core.Features.Classes.Commands.Models;
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
        public void ClassCommandMapping()
        {
            CreateMap<Class, AddClassCommand>().ReverseMap();
        }
    }
}
