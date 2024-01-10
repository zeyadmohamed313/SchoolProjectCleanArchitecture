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
	public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResourses> _localizer;

		#endregion
		#region Constructors
		public ChangePasswordValidator(IStringLocalizer<SharedResourses> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}

		#endregion
		#region Handle Functions
		public void ApplyValidationsRules()
		{

			RuleFor(x => x.Id)
				.NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);
				

			RuleFor(x => x.CurrentPassword)
				 .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
				 .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);
			RuleFor(x => x.NewPassword)
				 .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
				 .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);
			RuleFor(x => x.ConfirmPassword)
				 .Equal(x => x.NewPassword).WithMessage(_localizer[SharedResoursesKeys.PasswordNotEqualConfirmPass]);

		}

		public void ApplyCustomValidationsRules()
		{

		}

		#endregion
	}
}
