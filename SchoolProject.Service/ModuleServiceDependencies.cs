using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Results;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.CurrentUserServices.Abstracts;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
	{
		public static IServiceCollection AddServiceDependendcies(this IServiceCollection services)
		{
			services.AddTransient<IStudentService, StudentService>();
			services.AddTransient<IDepartmentServices, DepartmentServices>();
			services.AddTransient<IAuthenticationServices, AuthenticationServices>();
			services.AddSingleton<System.Collections.Concurrent.ConcurrentDictionary<string, RefreshToken>>();
			services.AddTransient<IAuthorizationServices, AuthorizationServices>();
			services.AddTransient<IInstructorServices, InstructorServices>();
            services.AddTransient<IEmailServices, EmailServices>();
			services.AddTransient<IApplicationUserServices, ApplicationUserServices>();
            services.AddTransient<ICurrentUserServices, CurrentUserServices.Implementation.CurrentUserServices>();
			services.AddTransient<IClassServices, ClassServices>();
			services.AddTransient<ISubjectServices, SubjectServices>();
            return services;
		}
	}
}