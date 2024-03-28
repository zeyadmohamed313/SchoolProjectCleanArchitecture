using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IStudentSubjectRepository:IGenericRepositoryAsync<StudentSubject>
    {
        public Task<StudentSubject> GetStudentSubject(int StdID, int SubId);

    }
}
