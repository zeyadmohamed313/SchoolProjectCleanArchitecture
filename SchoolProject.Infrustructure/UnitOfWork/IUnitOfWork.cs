using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.UnitOfwork
{
	public interface IUnitOfWork:IDisposable
	{
		IStudentRepository Students { get; }
		IDepartmentRepository Departments { get; }
		IRefreshTokenRepository RefreshToken { get; }
		IInstructorRepository Instructors { get; }
		IClassRepository Classes { get; }
		ISubjectRepository Subjects { get; }
		IDepartmentSubjectRepository DepartmentSubjects { get; }
		IStudentSubjectRepository StudentSubjects { get; }
		IInstructorSubjectRepository InstructorSubjects { get; }
		int Complete();
	}
}
