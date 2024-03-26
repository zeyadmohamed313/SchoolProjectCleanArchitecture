using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Email.Command.Model;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Email.Command.Handler
{
    public class EmailCommandHandler:ResponseHandler,
        IRequestHandler<SendEmailCommand,Response<string>>
    {
        #region Fields
        private readonly IEmailServices _emailsService;
        private readonly IStringLocalizer<SharedResourses> _localizer;
        #endregion
        #region Constructors
        public EmailCommandHandler(IStringLocalizer<SharedResourses> stringLocalizer,
                                    IEmailServices emailsService) : base(stringLocalizer)
        {
            _emailsService = emailsService;
            _localizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailsService.SendEmail(request.Email, request.Massege, null);
            if (response == "Success")
                return Success<string>("");
            return BadRequest<string>(_localizer[SharedResoursesKeys.SendEmailFailed]);
        }
        #endregion
    }
}
