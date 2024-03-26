using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Queries.Handler
{
    public class ClaimQueryHandler:ResponseHandler,
      IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResults>>
    {
        #region Fileds
        private readonly IAuthorizationServices _authorizationService;
    private readonly UserManager<User> _userManager;
    private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
    #endregion
    #region Constructors
    public ClaimQueryHandler(IStringLocalizer<SharedResourses> stringLocalizer,
                              IAuthorizationServices authorizationService,
                              UserManager<User> userManager) : base(stringLocalizer)
    {
        _authorizationService = authorizationService;
        _userManager = userManager;
        _stringLocalizer = stringLocalizer;
    }
    #endregion
    #region Handle Functions
    public async Task<Response<ManageUserClaimsResults>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null) return NotFound<ManageUserClaimsResults>(_stringLocalizer[SharedResoursesKeys.UserIsNotFound]);
            var result = await _authorizationService.ManageUserClaimData(user);
        return Success(result);
    }
    #endregion
}
}
