using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Email.Command.Model;
using SchoolProject.Core.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Email.Command.Validations
{
    public class SendEmailValidator: AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizer;
        #endregion
        #region Constructors
        public SendEmailValidator(IStringLocalizer<SharedResourses> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);
            RuleFor(x => x.Massege)
                 .NotEmpty().WithMessage(_localizer[SharedResoursesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResoursesKeys.Required]);
        }
        #endregion
    }
}
