using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entites.Views;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Abstracts.StoredProcedures;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.Repositories;
using SchoolProject.Infrustructure.Repositories.StoredProcedures;
using SchoolProject.Infrustructure.Repositories.Views;
using SchoolProject.Infrustructure.UnitOfwork;

namespace SchoolProject.Infrustructure
{
	public static class ModuleInfrustructureDependencies
	{
		public static IServiceCollection AddInfrustructureDependendcies(this IServiceCollection services)
		{
			services.AddTransient<IStudentRepository,StudentRepository>();
			services.AddTransient<IDepartmentRepository, DepartmentRepository>();
			services.AddTransient<ISubjectRepository, SubjectRepository>();
			services.AddTransient<IInstructorRepository, InstructorRepository>();

			services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
			services.AddTransient<IUnitOfWork,UnitOfWork>();
			services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();
			services.AddTransient<IStoredProcedures, StoredProcedures>();
            services.AddTransient<IClassRepository, ClassRepository>();
            services.AddTransient<IStudentSubjectRepository, StudentSubjectRepository>();
            services.AddTransient<IInstructorSubjectRepository, InstructorSubjectRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();



            return services;
		}

	}
}