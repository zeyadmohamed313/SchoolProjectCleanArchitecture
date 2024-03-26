using SchoolProject.Data.Entites;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustructure.UnitOfwork;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class InstructorServices : IInstructorServices
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        public InstructorServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region HandleFunctions
        public async Task<string> AddInstructor(Instructor instructor)
        {
            if(instructor == null)
            {
                return "Null";
            }
            await _unitOfWork.Instructors.AddAsync(instructor);
            return "Added SuccessFully";
        }

        public async Task DeleteInstructor(Instructor instructor)
        {
            await _unitOfWork.Instructors.DeleteAsync(instructor);
        }

        public IQueryable<Instructor> FilterDepartmentPaginatedQuerable(InstructorOrderingEnum orderingEnum, string Search)
        {
            var querable = _unitOfWork.Instructors.GetTableNoTracking().AsQueryable();
            if (Search != null)
                querable = querable.Where(x => x.ENameAr.Contains(Search) || x.ENameEn.Contains(Search));
            switch (orderingEnum)
            {
                case InstructorOrderingEnum.insId:
                    querable = querable.OrderBy(x => x.InsId);
                    break;
                case InstructorOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.ENameEn);
                    break;
                default:
                    querable = querable.OrderBy(x => x.InsId);
                    break;
            }
            return querable;
        }

        public async Task<List<Instructor>> GetAllInstructors()
        {
            var instructors =  await _unitOfWork.Instructors.GetInstructorListAsync(); // eager loading doesnot work
            return instructors;
        }

        public async Task<Instructor> GetInstructorById(int id)
        {
            return await _unitOfWork.Instructors.GetByIdAsync(id);
        }

        public async Task<bool> IsInstructorArExists(string name)
        {
            // Check if there is student with the same Arabic Name  
            var DepartmentCheck = _unitOfWork.Instructors.GetTableNoTracking()
                .Where(x => x.ENameAr.Equals(name)).FirstOrDefault();
            if (DepartmentCheck == null) return false;
            return true;
        }

        public async Task<bool> IsInstructorEnExists(string name)
        {
            // Check if there is student with the same Arabic Name  
            var DepartmentCheck = _unitOfWork.Instructors.GetTableNoTracking()
                .Where(x => x.ENameEn.Equals(name)).FirstOrDefault();
            if (DepartmentCheck == null) return false;
            return true;
        }

        public Task<bool> IsInstructorExsists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateInstuctor(Instructor instructor)
        {
            await _unitOfWork.Instructors.UpdateAsync(instructor);
            return "Success";
        }
        #endregion
    }
}
