using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.UnitOfwork;

namespace SchoolProject.Infrustructure.Repositories
{
	public class StudentRepository : GenericRepositoryAsync<Student> ,IStudentRepository
	{
		#region Fields
		private readonly DbSet<Student> _students;
		#endregion
		#region Constructor
		public StudentRepository(ApplicationDbContext context):base(context)
		{
		    _students= context.Set<Student>();
		}
		#endregion
		#region HandleFunction
		public async Task<List<Student>> GetStudentListAsync()
		{
			return await _students.Include(x=>x.Department).ToListAsync();

		}
        public async Task<Student> GetStudentWithClasses(int StudId)
		{
			var student = await _students.Include(c => c.EnrolledClasses).FirstOrDefaultAsync(s=>s.StudID==StudId);
			return student;
		}
        public async Task<ICollection<StudentSubject>> GetStudentWithSubjects(int StudId)
        {
            var student = await  _students.Include(c => c.StudentSubjects).FirstOrDefaultAsync(s => s.StudID == StudId);
			var Sub = student.StudentSubjects;
            return Sub;
        }

        #endregion
    }
}
