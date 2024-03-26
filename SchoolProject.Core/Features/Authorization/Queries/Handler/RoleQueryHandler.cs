using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Results;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Infrustructure.UnitOfwork;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Queries.Handler
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>,
        IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>>

    {

        #region Properties
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IAuthorizationServices _authorizationServices;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructor
        public RoleQueryHandler(IStringLocalizer<SharedResourses> localizer
            , IMapper mapper , IAuthorizationServices authorizationServices,
            UserManager<User>userManager ) : base(localizer)
        {
            _localizer = localizer;
            _mapper = mapper;
            _authorizationServices = authorizationServices;
            _userManager = userManager;
        }
        #endregion

        #region Functions

        public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationServices.GetRolesAsync();
            var result = _mapper.Map<List<GetRolesListResult>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationServices.GetRoleById(request.Id);
            if (role == null) return NotFound<GetRoleByIdResult>(_localizer[SharedResoursesKeys.NotFound]);
            var result = _mapper.Map<GetRoleByIdResult>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserRolesResult>(_localizer[SharedResoursesKeys.UserIsNotFound]);
            var result = await _authorizationServices.ManageUserRolesData(user);
            return Success(result);
        }
        #endregion
    }
}
