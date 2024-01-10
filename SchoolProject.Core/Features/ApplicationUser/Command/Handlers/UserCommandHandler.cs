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
        IRequestHandler<UpdateUserCommand,Response<string>>,
        IRequestHandler<DeleteUserCommand,Response<string>>,
        IRequestHandler<ChangePasswordCommand,Response<string>>
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
			//if username is Exist
			var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
			//username is Exist
			if (userByUserName != null) return BadRequest<string>(_localizer[SharedResoursesKeys.UserNameIsAlreadyExsists]);
			//update
			var result = await _userManager.UpdateAsync(newUser);
			//result is not success
			if (!result.Succeeded) return BadRequest<string>(_localizer[SharedResoursesKeys.UpdateFailed] +" => "+ result.Errors.FirstOrDefault().Description);
			//message
			return Success((string)_localizer[SharedResoursesKeys.Update]);
		}

		public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
            var User = await _userManager.FindByIdAsync(request.Id.ToString());
            if(User == null) return NotFound<string>(_localizer[SharedResoursesKeys.NotFound]); 
            var Result = await _userManager.DeleteAsync(User);
            if (!Result.Succeeded) return BadRequest<string>(_localizer[SharedResoursesKeys.FailedToDeleteUser]);
            return Success<string>(_localizer[SharedResoursesKeys.Deleted]);
		}

		public  async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
		{

			//get user
			//check if user is exist
			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			//if Not Exist notfound
			if (user == null) return NotFound<string>();

			//Change User Password
			var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
			//var user1=await _userManager.HasPasswordAsync(user);
			//await _userManager.RemovePasswordAsync(user);
			//await _userManager.AddPasswordAsync(user, request.NewPassword);

			//result
			if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
			return Success((string)_localizer[SharedResoursesKeys.Success]);
		}
		#endregion
	}
}
