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
    public class StudentSubjectRepository:GenericRepositoryAsync<StudentSubject>,IStudentSubjectRepository
    {
        #region Fields
        private readonly DbSet<StudentSubject> _StdSubjects;
        #endregion
        #region Constructor
        public StudentSubjectRepository(ApplicationDbContext context) : base(context)
        {
            _StdSubjects = context.Set<StudentSubject>();
        }
        #endregion
        #region HandleFunctions
        public async Task<StudentSubject> GetStudentSubject(int StdID, int SubId)
        {
            return await _StdSubjects.FirstOrDefaultAsync(e => e.StudID == StdID && e.SubID == SubId);
        }

        #endregion
    }
}
