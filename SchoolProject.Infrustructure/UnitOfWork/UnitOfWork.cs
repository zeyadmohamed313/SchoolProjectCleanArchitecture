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
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Students = new StudentRepository(_context);
			Departments = new DepartmentRepository(_context);
		}
		// didnot use cause there is async methods 
		public int Complete()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
