﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Bases;
using System.Net;

namespace SchoolProject.Api.Base
{
	[ApiController]
	public class AppControllerBase : ControllerBase
	{
		#region Properties
		/*private readonly IMediator _mediatR;
		public AppControllerBase(IMediator mediatR)
		{
			_mediatR = mediatR;
		}*/
		private IMediator _mediatorInstance;
		protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
		#endregion
		#region Actions
		public ObjectResult NewResult<T>(Response<T> response)
		{
			// there is no list here
			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					return new OkObjectResult(response);
				case HttpStatusCode.Created:
					return new CreatedResult(string.Empty, response);
				case HttpStatusCode.Unauthorized:
					return new UnauthorizedObjectResult(response);
				case HttpStatusCode.BadRequest:
					return new BadRequestObjectResult(response);
				case HttpStatusCode.NotFound:
					return new NotFoundObjectResult(response);
				case HttpStatusCode.Accepted:
					return new AcceptedResult(string.Empty, response);
				case HttpStatusCode.UnprocessableEntity:
					return new UnprocessableEntityObjectResult(response);
				default:
					return new BadRequestObjectResult(response);
			}
		}
		#endregion
	}
}
