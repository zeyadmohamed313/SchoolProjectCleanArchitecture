using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
	public static class ModuleServiceDependencies
	{
		public static IServiceCollection AddServiceDependendcies(this IServiceCollection services)
		{
			services.AddTransient<IStudentService, StudentService>();
			services.AddTransient<IDepartmentServices, DepartmentServices>();
			return services;
		}
	}
}