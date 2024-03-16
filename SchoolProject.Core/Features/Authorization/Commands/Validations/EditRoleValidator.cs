using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Validations
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        #endregion
        #region Constructors

        #endregion
        public EditRoleValidator(IStringLocalizer<SharedResourses> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResoursesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResoursesKeys.Required]);
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResoursesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResoursesKeys.Required]);
        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
