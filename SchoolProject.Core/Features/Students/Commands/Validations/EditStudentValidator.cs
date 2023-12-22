using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
	public class EditStudentValidator:AbstractValidator<EditStudentCommand>
	{
		#region Fields
		private readonly IStudentService _studentService;
		private readonly IStringLocalizer<SharedResourses> _localizer;
		#endregion
		#region Constuctors
		public EditStudentValidator(IStudentService studentService , IStringLocalizer<SharedResourses> stringLocalizer)
		{
			_studentService = studentService;
			_localizer = stringLocalizer;
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
			// Must async take lambda expression as parameter
			RuleFor(x => x.NameAr)
			   .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsStudentExsistsArExculdeMe(Key, model.Id))
			   .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
			RuleFor(x => x.NameEn)
		   .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsStudentExsistsEnExculdeMe(Key, model.Id))
		   .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
		}
		#endregion
	}
}
