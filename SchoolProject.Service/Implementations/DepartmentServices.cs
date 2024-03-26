using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolProject.Data.Entites;
using SchoolProject.Data.Entites.Views;
using SchoolProject.Data.Enums;
using SchoolProject.Data.Results.StoredProceduresResult;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Abstracts.StoredProcedures;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Infrustructure.Repositories.Views;
using SchoolProject.Infrustructure.UnitOfwork;
using SchoolProject.Service.Abstracts;
namespace SchoolProject.Service.Implementations
{
    public class DepartmentServices : IDepartmentServices
	{
		#region Fields
		private readonly IUnitOfWork _unitofwork;
        private readonly IViewRepository<ViewDepartment> _viewRepository;
        private readonly IStoredProcedures _storedProcedure;
		#endregion

		#region Constructors
		public DepartmentServices(IUnitOfWork unitofwork,
            IViewRepository<ViewDepartment>viewRepository,
            IStoredProcedures storedProcedure)
		{
		   _unitofwork = unitofwork;
            _storedProcedure= storedProcedure;
           _viewRepository = viewRepository;
		}
		#endregion

		#region Handle Functions
		public  async Task<Department> GetDepartmentById(int id)
		{
			var Department = await _unitofwork.Departments.GetTableNoTracking().Where(d => d.DID.Equals(id))
				 .Include(Ds => Ds.DepartmentSubjects).ThenInclude(d=>d.Subject)
				 .Include(x=>x.Instructors)
				 //.Include(x=>x.Instructor)
				 .FirstOrDefaultAsync();
			
			return Department;
			
		}

		public  async Task<bool> IsDepartmentExsists(int id)
		{
			// any one satsify the condition return true
			return await _unitofwork.Departments.GetTableNoTracking().AnyAsync(x => x.DID == id);
		}


        public async Task<string> CreateDepartment(Department department)
		{
			await _unitofwork.Departments.AddAsync(department);
            return "Added SuccessFully";
        }


        public async Task<bool> IsDepartmentArExists(string name)
        {
            // Check if there is student with the same Arabic Name  
            var DepartmentCheck = _unitofwork.Departments.GetTableNoTracking()
                .Where(x => x.DNameAr.Equals(name)).FirstOrDefault();
            if (DepartmentCheck == null) return false;
            return true;
        }
        public async Task<bool> IsDepartmentEnExists(string name)
        {
            // Check if there is student with the same English Name  
            var DepartmentCheck = _unitofwork.Departments.GetTableNoTracking()
                .Where(x => x.DNameEn.Equals(name)).FirstOrDefault();
            if (DepartmentCheck == null) return false;
            return true;
        }

        public async Task<List<Department>> GetAllDepartments()
		{
			var Departments = await _unitofwork.Departments.GetTableNoTracking().ToListAsync();
			return Departments;
		}


        public IQueryable<Department> FilterDepartmentPaginatedQuerable(DepartmentOrderingEnum orderingEnum, string Search)
        {
            var querable = _unitofwork.Departments.GetTableNoTracking().AsQueryable();
            if (Search != null)
                querable = querable.Where(x => x.DNameAr.Contains(Search) || x.DNameEn.Contains(Search));
            switch (orderingEnum)
            {
                case DepartmentOrderingEnum.DepartId:
                    querable = querable.OrderBy(x => x.DID);
                    break;
                case DepartmentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.DNameEn);
                    break;
               
                
                default:
                    querable = querable.OrderBy(x => x.DID);
                    break;
            }
            return querable;
        }
        public async Task<string> UpdateDepartment(Department department)
        {
            if (department == null)
                return "Null";
            await _unitofwork.Departments.UpdateAsync(department);
            return "Success";
        }

        public async Task DeleteDepartment(Department department)
        {
            await  _unitofwork.Departments.DeleteAsync(department);
            return;
        }

        public async Task<List<ViewDepartment>> GetViewDepartment()
        {
            var viewDepartment = await _viewRepository.GetTableNoTracking().ToListAsync();
            return viewDepartment;
        }

        public async Task<List<GetDepartmentWithStudentsResult>> GetDepartmentWithStudentsStoredProcedure()
        {
            return await _storedProcedure.GetDepartmentsWithStudentsStoredProcedure();
           
        }


        #endregion
    }
}
