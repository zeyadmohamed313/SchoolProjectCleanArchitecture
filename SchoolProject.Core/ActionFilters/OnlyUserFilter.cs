using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Service.CurrentUserServices.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.ActionFilters
{
    public class OnlyUserFilter : IAsyncActionFilter
    {
        private readonly ICurrentUserServices _currentUserService;
        private readonly UserManager<User> _userManager;
        public OnlyUserFilter(ICurrentUserServices currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == true)
            {
                var roles = await _currentUserService.GetCurrentUserRolesAsync();
                if (roles.All(x => x != "User")) // all of them doesnot equal user
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
                else
                {
                    await next();
                }

            }
        }
    }
}
