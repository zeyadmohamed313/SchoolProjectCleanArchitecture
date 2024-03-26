using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SchoolProject.Data.Entites;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.UnitOfwork;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
	{
		#region Fields
		private readonly IUnitOfWork _unitOfWork;
		#endregion
		#region Constructor
		public StudentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		#endregion
		#region HandleFunctions
		public async Task<List<Student>> GetStudentListAsync()
		{
			return await _unitOfWork.Students.GetStudentListAsync();
		}
		public async Task<Student> GetByIdWithIncludeAsync(int id)
		{
			var student =  _unitOfWork.Students.GetTableNoTracking().Include(x => x.Department)
				.Where(x => x.StudID.Equals(id)).FirstOrDefault();
			return student;
		}
		public async Task<Student> GetByIdAsync(int id)
		{
			var student = _unitOfWork.Students.GetTableNoTracking()
				.Where(x => x.StudID.Equals(id)).FirstOrDefault();
			return student;
		}

		public async Task<string> AddStudentAsync(Student student)
		{
			// adding student 
			await _unitOfWork.Students.AddAsync(student);
			return "Added SuccessFully";
			
		}

		public async Task<bool> IsStudentArExists(string name)
		{
			// Check if there is student with the same Arabic Name  
			var studentCheck = _unitOfWork.Students.GetTableNoTracking()
				.Where(x => x.NameAr.Equals(name)).FirstOrDefault();
			if (studentCheck == null) return false;
			return true;
		}
		public async Task<bool> IsStudentEnExists(string name)
		{
			// Check if there is student with the same English Name  
			var studentCheck = _unitOfWork.Students.GetTableNoTracking()
				.Where(x => x.NameEn.Equals(name)).FirstOrDefault();
			if (studentCheck == null) return false;
			return true;
		}
		public async Task<bool> IsStudentExsistsArExculdeMe(string name, int id)
		{
			// Check if there is student with the same Name and differant ID
			var studentCheck = await _unitOfWork.Students.GetTableNoTracking()
				.Where(x => x.NameAr.Equals(name)&&!x.StudID.Equals(id)).FirstOrDefaultAsync();
			if (studentCheck == null) return false;
			return true;
		}
		public async Task<bool> IsStudentExsistsEnExculdeMe(string name, int id)
		{
			// Check if there is student with the same Name and differant ID
			var studentCheck = await _unitOfWork.Students.GetTableNoTracking()
				.Where(x => x.NameEn.Equals(name) && !x.StudID.Equals(id)).FirstOrDefaultAsync();
			if (studentCheck == null) return false;
			return true;
		}
		public async Task<string> EditAsync(Student student)
		{
			await _unitOfWork.Students.UpdateAsync(student);
			return "Success";
		}
		public async Task<string>DeleteAsync(Student student)
		{
			// do groub of tranactiom or never do any of it
			var Trans = _unitOfWork.Students.BeginTransaction();
			try
			{
				await _unitOfWork.Students.DeleteAsync(student);
				await Trans.CommitAsync();
				return "Success";
			}
			catch 
			{
				await Trans.RollbackAsync();
				return "Failed";
			}
		}

		public IQueryable<Student> GetAllStudentsQueryable()
		{
			return _unitOfWork.Students.GetTableNoTracking().Include(x => x.Department).AsQueryable();
		}

		public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string Search)
		{
			var querable =  _unitOfWork.Students.GetTableNoTracking().Include(x => x.Department).AsQueryable();
			if(Search!=null)
			querable = querable.Where(x => x.NameEn.Contains(Search) ||x.Address.Contains(Search) );
			switch (orderingEnum)
			{
				case StudentOrderingEnum.StudId:
					querable = querable.OrderBy(x => x.StudID);
					break;
				case StudentOrderingEnum.Name:
					querable = querable.OrderBy(x => x.NameEn);
					break;
				case StudentOrderingEnum.Address:
					querable = querable.OrderBy(x => x.Address);
					break;
				case StudentOrderingEnum.DepartmentName:
					querable = querable.OrderBy(x => x.Department.DNameEn);
					break;
				default:
					querable = querable.OrderBy(x => x.StudID);
					break;
			}
			return querable;
		}

		public IQueryable<Student> GetAllStudentsByDepartmentIdQueryable(int id)
		{
			return _unitOfWork.Students.GetTableNoTracking().Where(s=>s.DID.Equals(id)).AsQueryable();
		}
		#endregion
	}
}
