using SchoolProject.Data.Entites;
using SchoolProject.Data.Results.StoredProceduresResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts.StoredProcedures
{
    public interface IStoredProcedures
    {
        public  Task<List<GetDepartmentWithStudentsResult>> GetDepartmentsWithStudentsStoredProcedure();
    }
}
