using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
	public interface IDepartmentServices
	{
		public Task<Department> GetDepartmentById(int id);
	}
}
