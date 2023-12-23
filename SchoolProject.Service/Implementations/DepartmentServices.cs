using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.UnitOfwork;
using SchoolProject.Service.Abstracts;
namespace SchoolProject.Service.Implementations
{
	public class DepartmentServices : IDepartmentServices
	{
		#region Fields
		private readonly IUnitOfWork _unitofwork;
		#endregion

		#region Constructors
		public DepartmentServices(IUnitOfWork unitofwork)
		{
		   _unitofwork = unitofwork;
		}
		#endregion

		#region Handle Functions
		public  async Task<Department> GetDepartmentById(int id)
		{
			var Department = await _unitofwork.Departments.GetTableNoTracking().Where(d => d.DID.Equals(id))
				 .Include(Ds => Ds.DepartmentSubjects).ThenInclude(d=>d.Subject)
				 .Include(x=>x.Instructors)
				 .Include(x=>x.Instructor)
				 .FirstOrDefaultAsync();
			
			return Department;
			
		}

		public  async Task<bool> IsDepartmentExsists(int id)
		{
			// any one satsify the condition return true
			return await _unitofwork.Departments.GetTableNoTracking().AnyAsync(x => x.DID == id);
		}


		#endregion
	}
}
