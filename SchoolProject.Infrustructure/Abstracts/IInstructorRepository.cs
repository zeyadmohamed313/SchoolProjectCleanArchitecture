using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts
{
	public interface IInstructorRepository:IGenericRepositoryAsync<Instructor>
	{
        public Task<List<Instructor>> GetInstructorListAsync();
        public Task<Instructor> GetInstructorWithClassesAsync(int id);

    }
}
