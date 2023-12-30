using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Command.Models;
using SchoolProject.Core.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Command.Validations
{
	public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResourses> _localizer;

		#endregion
		#region Constructors
		public UpdateUserValidator(IStringLocalizer<SharedResourses> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}

		#endregion
		#region Handle Functions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.FullName)
				 .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
				 .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);

			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);

			RuleFor(x => x.Email)
				 .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
				 .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);
			
		}

		public void ApplyCustomValidationsRules()
		{

		}

		#endregion
	}

}
