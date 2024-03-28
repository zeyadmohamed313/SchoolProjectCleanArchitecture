using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Subjects.Command.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Command.Validations
{
    public class AddSubjectValidations:AbstractValidator<AddSubjectCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly ISubjectServices _subjectServices;
        #endregion
        #region Constructor
        public AddSubjectValidations(IStringLocalizer<SharedResourses> localizer,
            IDepartmentServices departmentServices, ISubjectServices subjectServices)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _subjectServices = subjectServices;
        }
        #endregion
        #region Functions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.SubjectNameAr)
               .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
               .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);
            RuleFor(x => x.SubjectNameEn)
              .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
              .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);
            RuleFor(x => x.Period)
           .GreaterThanOrEqualTo(5).WithMessage("Capacity must be at least 5");

        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.SubjectNameAr)
                .MustAsync(async (Key, CancellationToken) => !await _subjectServices.IsSubjectArExsists(Key))
                .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
            RuleFor(x => x.SubjectNameEn)
           .MustAsync(async (Key, CancellationToken) => !await _subjectServices.IsSubjectArExsists(Key))
           .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
        }
        #endregion
    }
}
