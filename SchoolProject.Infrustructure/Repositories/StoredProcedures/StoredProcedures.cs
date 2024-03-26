using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entites;
using SchoolProject.Data.Results.StoredProceduresResult;
using SchoolProject.Infrustructure.Abstracts.StoredProcedures;
using SchoolProject.Infrustructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Repositories.StoredProcedures
{
    public class StoredProcedures:IStoredProcedures
    {
        #region Feilds 
        private readonly ApplicationDbContext _applicationDbContext;
        #endregion
        #region Constructor
        public StoredProcedures(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        #endregion
        public async Task<List<GetDepartmentWithStudentsResult>> GetDepartmentsWithStudentsStoredProcedure()
        {
            var result = await _applicationDbContext.Set<GetDepartmentWithStudentsResult>()
            .FromSqlRaw("EXEC GetAllDepartmentsWithStudents")
            .ToListAsync();
            return result;
        }
    }
}
