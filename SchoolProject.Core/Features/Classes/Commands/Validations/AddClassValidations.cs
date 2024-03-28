using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Classes.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Classes.Commands.Validations
{
    public class AddClassValidations:AbstractValidator<AddClassCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IClassServices _classServices;
        #endregion
        #region Constructor
        public AddClassValidations(IStringLocalizer<SharedResourses> localizer, 
            IDepartmentServices departmentServices,IClassServices classServices)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _classServices = classServices; 
        }
        #endregion
        #region Functions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required])
               .MaximumLength(100).WithMessage(_localizer[SharedResoursesKeys.MaxLengthis100]);
            RuleFor(x => x.Capacity)
           .GreaterThanOrEqualTo(5).WithMessage("Capacity must be at least 5");

        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _classServices.IsClassExsists(Key))
                .WithMessage(_localizer[SharedResoursesKeys.IsExist]);
           
        }
        #endregion
    }
}
