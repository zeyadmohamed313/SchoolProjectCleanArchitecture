using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace SchoolProject.Infrustructure.Repositories
{
	public class DepartmentRepository : GenericRepositoryAsync<Department>,IDepartmentRepository
	{
		#region Fields
		private readonly DbSet<Student> _department;
		#endregion
		#region Constructor
		public DepartmentRepository(ApplicationDbContext context):base(context)
		{
			_department = context.Set<Student>();
		}
		#endregion
		#region Handle Function

		#endregion
	}
}
