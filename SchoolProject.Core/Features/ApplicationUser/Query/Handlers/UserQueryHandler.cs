using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Query.Models;
using SchoolProject.Core.Features.ApplicationUser.Query.Results;
using SchoolProject.Core.Resourses;
using SchoolProject.Core.Wrapper;
using SchoolProject.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Query.Handlers
{
	public class UserQueryHandler : ResponseHandler,
		IRequestHandler<GetUserListQueryPaginated, PaginatedResult<GetUserListResponsePaginated>>
		, IRequestHandler<GetUserByIdQuery,Response<GetUserByIdResponse>>
	{ 
		#region Fields
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResourses> _sharedResources;
		private readonly UserManager<User> _userManager;
		#endregion

		#region Constructors
		public UserQueryHandler(IStringLocalizer<SharedResourses> stringLocalizer,
								  IMapper mapper,
								  UserManager<User> userManager) : base(stringLocalizer)
		{
			_mapper = mapper;
			_sharedResources = stringLocalizer;
			_userManager = userManager;
		}
		#endregion

		#region Handle Functions
		public async Task<PaginatedResult<GetUserListResponsePaginated>> Handle(GetUserListQueryPaginated request, CancellationToken cancellationToken)
		{
			var users = _userManager.Users.AsQueryable();
			var paginatedList = await _mapper.ProjectTo<GetUserListResponsePaginated>(users)
											.ToPaginatedListAsync(request.PageNumber, request.PageSize);
			return paginatedList;
		}


		public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			//var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id==request.Id);
			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			if (user == null) return NotFound<GetUserByIdResponse>(_sharedResources[SharedResoursesKeys.NotFound]);
			var result = _mapper.Map<GetUserByIdResponse>(user);
			return Success(result);
		}
		#endregion
	}
}
