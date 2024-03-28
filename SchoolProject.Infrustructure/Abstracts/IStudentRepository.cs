using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts
{
	public interface IStudentRepository:IGenericRepositoryAsync<Student>
	{
		public Task<List<Student>> GetStudentListAsync();
		public Task<Student> GetStudentWithClasses(int StudId);
		public Task<ICollection<StudentSubject>> GetStudentWithSubjects(int StudId);



    }
}
