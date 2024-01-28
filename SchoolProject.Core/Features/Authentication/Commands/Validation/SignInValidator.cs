using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Commands.Validation
{
	public class SignInValidator : AbstractValidator<SignInCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResourses> _localizer;
		#endregion
		#region Constuctors
		public SignInValidator(
			 IStringLocalizer<SharedResourses> localizer
			)
		{
			_localizer = localizer;
			ApplyValidationRules();
			ApplyCustomValidationRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationRules()
		{
			RuleFor(x => x.UserName)
			   .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);
			
		}
		public void ApplyCustomValidationRules()
		{
		
			
		}
		#endregion
	}

}
