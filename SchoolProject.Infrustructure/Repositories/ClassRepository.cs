using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Repositories
{
    public class ClassRepository : GenericRepositoryAsync<Class>, IClassRepository
    {
        #region Feilds
        private readonly ApplicationDbContext _applicationDbContext;
        #endregion
        #region Constructor
        public ClassRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }
        #endregion
        #region HandleFunctions
        public async Task<Class>? GetByIdWithInstructors(int Id)
        {
            return await _applicationDbContext.classes.Include(e => e.Instructors).FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<Class>? GetByIdWithStudents(int Id)
        {
            return await _applicationDbContext.classes.Include(e => e.Students).FirstOrDefaultAsync(c => c.Id == Id);
        }
        #endregion
    }
}
