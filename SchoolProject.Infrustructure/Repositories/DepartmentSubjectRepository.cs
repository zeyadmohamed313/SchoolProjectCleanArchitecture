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
    public class DepartmentSubjectRepository : GenericRepositoryAsync<DepartmetSubject>, IDepartmentSubjectRepository
    {

        #region Fields
        private readonly DbSet<DepartmetSubject> _DepartmentSubjects;
        #endregion
        #region Constructor
        public DepartmentSubjectRepository(ApplicationDbContext context) : base(context)
        {
            _DepartmentSubjects = context.Set<DepartmetSubject>();
        }
        #endregion
    }
}
