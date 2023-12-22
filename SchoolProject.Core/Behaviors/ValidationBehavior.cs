using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	   where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;
		private readonly IStringLocalizer<SharedResourses> _localizer;
		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators
			, IStringLocalizer<SharedResourses> localizer)
		{
			_validators = validators;
			_localizer = localizer;
		}

		#region ReadMe
		/*
		 1- it go to controller 
		 2- it hits the mediator line 
		 3- it go to validator first to see if there is rules or not
		 4- it goes to validator bahaviors to check if there any valiation violation
		 5- if there is it throw the exeption and exption middleware translate it to badrequest
		 6- if not it goes to the next step in mediator pipline which it is the handler it self
		 7- return the response 
		 */
		#endregion
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);
				var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
				var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

				if (failures.Count != 0)
				{
					var message = failures.Select(x => _localizer[$"{x.PropertyName}"] + ":" + _localizer[x.ErrorMessage]).FirstOrDefault();

					throw new ValidationException(message);

				}
			}
			return await next();
		}
	}

}
