using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
	public class AddStudentValidator : AbstractValidator<AddStudentCommand>
	{
		#region Fields
		private readonly IStudentService _studentService;
		private readonly IStringLocalizer<SharedResourses> _localizer;
		#endregion
		#region Constuctors
		public AddStudentValidator(IStudentService studentService
			, IStringLocalizer<SharedResourses> localizer)
		{
			_studentService = studentService;
			_localizer = localizer;
			ApplyValidationRules();
			ApplyCustomValidationRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationRules()
		{
			RuleFor(x => x.NameAr)
			   .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
			   .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);

			RuleFor(x => x.Address)
				.NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);
		}
		public void ApplyCustomValidationRules()
		{
			RuleFor(x => x.NameAr)
				.MustAsync(async (Key, CancellationToken) => !await _studentService.IsStudentArExists(Key))
				.WithMessage(_localizer[SharedResoursesKeys.IsExist]);
			RuleFor(x => x.NameEn)
				.MustAsync(async (Key, CancellationToken) => !await _studentService.IsStudentEnExists(Key))
				.WithMessage(_localizer[SharedResoursesKeys.IsExist]);
		}
		#endregion
	}
}
