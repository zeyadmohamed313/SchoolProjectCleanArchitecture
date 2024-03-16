using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Validations
{
	public class AddRoleValidator : AbstractValidator<AddRoleCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
		private readonly IAuthorizationServices _authorizationService;
		#endregion
		#region Constructors

		#endregion
		public AddRoleValidator(IStringLocalizer<SharedResourses> stringLocalizer,
								 IAuthorizationServices authorizationService)
		{
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}

		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.RoleName)
				 .NotEmpty().WithMessage(_stringLocalizer[SharedResoursesKeys.NotEmpty])
				 .NotNull().WithMessage(_stringLocalizer[SharedResoursesKeys.Required]);
		}

		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.RoleName)
				.MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleExstists(Key))
				.WithMessage(_stringLocalizer[SharedResoursesKeys.IsExist]);
		}

		#endregion
	}
}
