using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Bases
{
	public class ResponseHandler
	{
		private readonly IStringLocalizer<SharedResourses> _localizer;
		public ResponseHandler(IStringLocalizer<SharedResourses> localizer)
		{
			_localizer = localizer;
		}
		public Response<T> Deleted<T>(string massege = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = massege == null ? _localizer[SharedResoursesKeys.Deleted] : massege
			};
		}
		public Response<T> Success<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = _localizer[SharedResoursesKeys.Success],
				Meta = Meta
			};
		}
		public Response<T> Unauthorized<T>(string massege = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.Unauthorized,
				Succeeded = true,
				Message = massege == null ? _localizer[SharedResoursesKeys.UnAuthorized] : massege
			};
		}
		public Response<T> BadRequest<T>(string Message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Succeeded = false,
				Message = Message == null ? "Bad Request" : Message
			};
		}
		public Response<T> UnprocessableEntity<T>(string Message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
				Succeeded = false,
				Message = Message == null ? "UnprocessableEntity" : Message
			};
		}

		public Response<T> NotFound<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Succeeded = false,
				Message = message == null ? "Not Found" : message
			};
		}

		public Response<T> Created<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.Created,
				Succeeded = true,
				Message = _localizer[SharedResoursesKeys.Created],
				Meta = Meta
			};
		}
	}


}
