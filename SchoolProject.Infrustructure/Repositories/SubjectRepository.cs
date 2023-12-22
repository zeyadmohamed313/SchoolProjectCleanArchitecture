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
	public class SubjectRepository : GenericRepositoryAsync<Subjects>, ISubjectRepository
	{
		#region Fields
		private readonly DbSet<Subjects> _subject;
		#endregion
		#region Constructor
		public SubjectRepository(ApplicationDbContext context) : base(context)
		{
			_subject = context.Set<Subjects>();
		}
		#endregion
		#region Handle Function

		#endregion
	
	}
}
