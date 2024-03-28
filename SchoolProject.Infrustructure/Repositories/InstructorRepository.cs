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
        public async Task<List<Instructor>> GetInstructorListAsync()
        {
            return await _instuctors.Include(x => x.Department).ToListAsync();
        }
        // you should include from here because the next layer doesnot have the department property

        public async Task<Instructor> GetInstructorWithClassesAsync(int id)
        {
            return await _instuctors.Include(c => c.classes).FirstOrDefaultAsync(i => i.InsId == id);
        }

        #endregion

    }
}
