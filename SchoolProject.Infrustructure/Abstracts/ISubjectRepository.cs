using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts
{
	public interface ISubjectRepository:IGenericRepositoryAsync<Subjects>
	{
		public Task<Subjects> GetByIdWithInstructors(int Id);
        public Task<Subjects> GetByIdWithStudents(int Id);


    }
}
