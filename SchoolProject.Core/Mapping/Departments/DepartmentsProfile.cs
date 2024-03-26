using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Departments
{
	public partial class DepartmentsProfile : Profile
	{
		public DepartmentsProfile() 
		{
			GetDepartmentByIdMapping();
			CreateDepartmentCommandMapping();
			UpdateDepartmentMapping();
			GetDepartmentWithStudentCountMapping();
        }
	}
}
