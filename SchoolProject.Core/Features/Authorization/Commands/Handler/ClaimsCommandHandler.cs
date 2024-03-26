using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handler
{
    public class ClaimsCommandHandler:ResponseHandler,
        IRequestHandler<UpdateUserClaimsCommand,Response<string>>
    {
        #region Fileds
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IAuthorizationServices _authorizationService;

        #endregion
        #region Constructors
        public ClaimsCommandHandler(IStringLocalizer<SharedResourses> stringLocalizer,
                                    IAuthorizationServices authorizationService) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _localizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_localizer[SharedResoursesKeys.UserIsNotFound]);
                case "FailedToRemoveOldClaims": return BadRequest<string>(_localizer[SharedResoursesKeys.FailedToRemoveOldClaims]);
                case "FailedToAddNewClaims": return BadRequest<string>(_localizer[SharedResoursesKeys.FailedToAddNewClaims]);
                case "FailedToUpdateClaims": return BadRequest<string>(_localizer[SharedResoursesKeys.FailedToUpdateClaims]);
            }
            return Success<string>(_localizer[SharedResoursesKeys.Success]);
        }
        #endregion
    }
}
