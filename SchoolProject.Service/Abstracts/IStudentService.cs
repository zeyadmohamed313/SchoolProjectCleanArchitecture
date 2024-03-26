using SchoolProject.Data.Entites;
using SchoolProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
	{
		public Task<List<Student>> GetStudentListAsync();
		public Task<Student> GetByIdWithIncludeAsync(int id);
		public Task<Student> GetByIdAsync(int id);
		public Task<string> AddStudentAsync(Student student);
		public Task<bool> IsStudentEnExists(string name);
		public  Task<bool> IsStudentArExists(string name);
		public Task<bool> IsStudentExsistsArExculdeMe(string name, int id);
		public Task<bool> IsStudentExsistsEnExculdeMe(string name, int id);
		public Task<string> EditAsync(Student student);
		public Task<string> DeleteAsync(Student student);
		public IQueryable<Student> GetAllStudentsQueryable();
		public IQueryable<Student> GetAllStudentsByDepartmentIdQueryable(int id);
		public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum,string Search);
	}
}
