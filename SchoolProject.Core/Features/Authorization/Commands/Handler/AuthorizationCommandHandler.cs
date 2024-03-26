using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Handler
{
	public class AuthorizationCommandHandler : ResponseHandler,
		IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
		IRequestHandler<DeleteRoleCommand,Response<string>>,
		IRequestHandler<UpdateRolesCommand,Response<string>>


    {
        #region Feilds
        private readonly IStringLocalizer<SharedResourses> _localizar;
		private readonly IAuthorizationServices _authorizationServices;
		#endregion
		#region Constructor
		public AuthorizationCommandHandler(IStringLocalizer<SharedResourses> localizar
			, IAuthorizationServices authorizationServices) :base(localizar)
		{
			_localizar = localizar;
			_authorizationServices = authorizationServices;
		}
		#endregion
		#region HandleFunctions
		
		public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
		{
			var result =await _authorizationServices.AddRoleAsync(request.RoleName);
			if (result == "Success") 
			{ 
				return Success("");
			}
			return BadRequest<string>(_localizar[SharedResoursesKeys.FaliedToAddRole]);
		}

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.EditRoleAsync(request);
            if (result == "notFound") return NotFound<string>();
            else if (result == "Success") return Success((string)_localizar[SharedResoursesKeys.Success]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>(_localizar[SharedResoursesKeys.RoleIsUsed]);
            else if (result == "Success") return Success((string)_localizar[SharedResoursesKeys.Deleted]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_localizar[SharedResoursesKeys.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_localizar[SharedResoursesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddNewRoles": return BadRequest<string>(_localizar[SharedResoursesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateUserRoles": return BadRequest<string>(_localizar[SharedResoursesKeys.FailedToUpdateUserRoles]);
            }
            return Success<string>(_localizar[SharedResoursesKeys.Success]);
        }
        #endregion
    }
}
