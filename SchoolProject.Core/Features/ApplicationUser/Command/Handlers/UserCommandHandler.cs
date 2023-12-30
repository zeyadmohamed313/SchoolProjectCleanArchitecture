using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Command.Models;
using SchoolProject.Core.Features.ApplicationUser.Query.Results;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Command.Handlers
{
    public class UserCommandHandler : ResponseHandler
        , IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand,Response<string>>
    {
        #region Feilds
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructor
        public UserCommandHandler(IStringLocalizer<SharedResourses> localizer
            , IMapper mapper , UserManager<User>userManager) : base(localizer)
        {
            _localizer = localizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion
        #region HandleFunction
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // Check if Email is not uniqe
            var User = await _userManager.FindByEmailAsync(request.Email);
            if (User != null) return BadRequest<string>(_localizer[SharedResoursesKeys.EmailAlreadyExsists]);
            // Check if the user name Uniqe
            var UserByUserName =await _userManager.FindByNameAsync(request.UserName);
            if (UserByUserName != null) return BadRequest<string>(_localizer[SharedResoursesKeys.UserNameIsAlreadyExsists]);
            // Mapping
            var mapping = _mapper.Map<User>(request);
            var IdentityResult =await _userManager.CreateAsync(mapping,request.PassWord);
            if(!IdentityResult.Succeeded)
            {
                //return BadRequest<string>(_localizer[SharedResoursesKeys.FaliedToAddUser]);
                return BadRequest<string>(IdentityResult.Errors.FirstOrDefault().Description);
			}
            return Created("");

		}

	
		public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			//check if user is exist
			var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
			//if Not Exist notfound
			if (oldUser == null) return NotFound<string>();
			//mapping
			var newUser = _mapper.Map(request, oldUser);

			//update
			var result = await _userManager.UpdateAsync(newUser);
			//result is not success
			if (!result.Succeeded) return BadRequest<string>(_localizer[SharedResoursesKeys.UpdateFailed] +" => "+ result.Errors.FirstOrDefault().Description);
			//message
			return Success((string)_localizer[SharedResoursesKeys.Update]);
		}
		#endregion
	}
}
