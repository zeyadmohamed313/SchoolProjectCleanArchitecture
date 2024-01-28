using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.Repositories;
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

			return services;
		}

	}
}