using SchoolProject.Data.Entites;
using SchoolProject.Data.Entites.Views;
using SchoolProject.Data.Enums;
using SchoolProject.Data.Results.StoredProceduresResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentServices
	{
		public Task<Department> GetDepartmentById(int id);
		public  Task<bool> IsDepartmentExsists(int id);
		public Task<string> CreateDepartment(Department department);
		public  Task<bool> IsDepartmentArExists(string name);
		public  Task<bool> IsDepartmentEnExists(string name);
		public IQueryable<Department> FilterDepartmentPaginatedQuerable(DepartmentOrderingEnum orderingEnum, string Search);
		public Task<string> UpdateDepartment(Department department);
		public Task DeleteDepartment(Department department);
		public Task<List<ViewDepartment>> GetViewDepartment();
		public Task<List<GetDepartmentWithStudentsResult>> GetDepartmentWithStudentsStoredProcedure();
    }
}
