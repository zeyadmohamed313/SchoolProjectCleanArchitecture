using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Instructor.Command.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructor.Command.Validations
{
    public class AddInstructorValidations:AbstractValidator<AddInstructorModel>
    {

        #region Fields
        private readonly IInstructorServices _instructorServices;
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IDepartmentServices _departmentService;
        #endregion
        #region Constuctors
        public AddInstructorValidations(IInstructorServices instructorServices
            , IStringLocalizer<SharedResourses> localizer,
                 IDepartmentServices departmentService)
        {
            _instructorServices = instructorServices;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _departmentService = departmentService;
        }
        #endregion
        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.ENameAr)
               .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
               .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);

            RuleFor(x => x.ENameEn)
              .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
              .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);

            RuleFor(x => x.DID)
                .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.ENameAr)
                .MustAsync(async (Key, CancellationToken) => !await _instructorServices.IsInstructorArExists(Key))
                .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
            RuleFor(x => x.ENameEn)
                .MustAsync(async (Key, CancellationToken) => !await _instructorServices.IsInstructorEnExists(Key))
                .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
       
            RuleFor(x => x.DID)
                    .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentExsists(Key??0))
                    .WithMessage(_localizer[SharedResoursesKeys.DepartmentNotExsists]);
            RuleFor(x => x.SupervisorId)
                   .MustAsync(async (Key, CancellationToken) => await _instructorServices.GetInstructorById(Key ?? 0)!=null)
                   .WithMessage(_localizer[SharedResoursesKeys.NotFound]);

        }
        #endregion
    }
}
