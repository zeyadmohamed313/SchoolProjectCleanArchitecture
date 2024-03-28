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
    public class InstructorSubjectRepository:GenericRepositoryAsync<Ins_Subject>,IInstructorSubjectRepository
    {
        #region Fields
        private readonly DbSet<Ins_Subject> _InsSubjects;
        #endregion
        #region Constructor
        public InstructorSubjectRepository(ApplicationDbContext context) : base(context)
        {
            _InsSubjects = context.Set<Ins_Subject>();
        }
        #endregion
        #region HandleFunctions
        public async Task<Ins_Subject> GetInstructorSubject(int InsID,int SubId)
        {
            return await _InsSubjects.FirstOrDefaultAsync(e=>e.SubId==SubId&&e.InsId==InsID);
        }

        #endregion
    }
}
