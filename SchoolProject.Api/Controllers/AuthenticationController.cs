using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class AuthenticationController : AppControllerBase
	{
		[HttpPost(Router.AuthenticationRouting.SignIn)]
        [SwaggerOperation(Summary = "تسجيل الدخول", OperationId = "SignIn")]

        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpPost(Router.AuthenticationRouting.RefreshToken)]
        [SwaggerOperation(Summary = "اعادة تنشيط التوكن", OperationId = "RefreshToken")]

        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[HttpGet(Router.AuthenticationRouting.ValidateToken)]
        [SwaggerOperation(Summary = "التاكد من صلاحية التوكن", OperationId = "ValidateToken")]

        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery Query)
		{
			var response = await Mediator.Send(Query);
			return NewResult(response);
		}
        [HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
        [SwaggerOperation(Summary = "توثيق الحساب", OperationId = "ConfirmEmail")]

        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.SendResetPasswordCode)]
        [SwaggerOperation(Summary = "ارسال الكود للمستخدم", OperationId = "SendResetPassword")]

        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.AuthenticationRouting.ConfirmResetPasswordCode)]
        [SwaggerOperation(Summary = "تجربة ادخال الكود الذي تم ارسالة للمستخدم", OperationId = "ConfirmResetPassword")]

        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.ResetPassword)]
        [SwaggerOperation(Summary = "تغيير الرقم السري بعد التاكد من الكود", OperationId = "ResetPassword")]

        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
