using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IClassRepository:IGenericRepositoryAsync<Class>
    {
        public Task<Class> GetByIdWithStudents(int Id);
        public Task<Class> GetByIdWithInstructors(int Id);

    }
}
