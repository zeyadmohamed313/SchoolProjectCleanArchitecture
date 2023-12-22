using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Core.Behaviors;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System.Reflection;

namespace SchoolProject.Core
{
	public static class ModuleCoreDependencies
	{
		public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
		{
			//Configuration Of Mediator
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			// Configuration of AutoMapper
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			// Get Validators
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			// 
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			return services;
		}
	}
}