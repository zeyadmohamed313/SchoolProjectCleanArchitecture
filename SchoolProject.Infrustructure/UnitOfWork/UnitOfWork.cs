using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.UnitOfwork
{
	public class UnitOfWork:IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public IStudentRepository Students { get; private set; }
		public IDepartmentRepository Departments { get; private set; }
		public IRefreshTokenRepository RefreshToken { get; private set; }
		public IInstructorRepository Instructors { get; private set; }
        public IClassRepository Classes { get; private set; }
		public ISubjectRepository Subjects { get; private set; }
		public  IDepartmentSubjectRepository DepartmentSubjects { get; private set; }
        public IStudentSubjectRepository StudentSubjects { get; private set; }
        public IInstructorSubjectRepository InstructorSubjects { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Students = new StudentRepository(_context);
			Departments = new DepartmentRepository(_context);
			RefreshToken = new RefreshTokenRepository(_context);
			Instructors = new InstructorRepository(_context);
            Classes = new ClassRepository(_context);
			Subjects = new SubjectRepository(_context);
			DepartmentSubjects = new DepartmentSubjectRepository(_context);
            StudentSubjects = new StudentSubjectRepository(_context);
            InstructorSubjects = new InstructorSubjectRepository(_context);

        }
        // didnot use cause there is async methods 
        public int Complete()
		{
			return  _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
