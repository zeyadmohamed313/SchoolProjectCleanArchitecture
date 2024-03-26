using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Department.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Commands.Validations
{
    public class UpdateDepartmentValidations:AbstractValidator<UpdateDepartmentCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IDepartmentServices _departmentServices;
        #endregion
        #region Constructor
        public UpdateDepartmentValidations(IStringLocalizer<SharedResourses> localizer, IDepartmentServices departmentServices)
        {
            _localizer = localizer;
            _departmentServices = departmentServices;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Functions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.DNameAr)
               .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
               .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);
            RuleFor(x => x.DNameEn)
               .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
               .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.DNameAr)
                .MustAsync(async (Key, CancellationToken) => !await _departmentServices.IsDepartmentArExists(Key))
                .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
            RuleFor(x => x.DNameEn)
                .MustAsync(async (Key, CancellationToken) => !await _departmentServices.IsDepartmentEnExists(Key))
                .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
        }
        #endregion
    }
}
