using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Repositories
{
	public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
	{
		#region Fields
		private readonly DbSet<Instructor> _instuctors;
		#endregion
		#region Constructor
		public InstructorRepository(ApplicationDbContext context) : base(context)
		{
			_instuctors = context.Set<Instructor>();
		}
		#endregion
		#region Handle Function

		#endregion
	
	}
}
