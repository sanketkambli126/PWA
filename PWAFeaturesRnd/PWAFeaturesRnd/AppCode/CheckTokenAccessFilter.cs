using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PWAFeaturesRnd.Common;

namespace PWAFeaturesRnd.AppCode
{
    /// <summary>
    /// The check token access filter
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IActionFilter" />
    public class CheckTokenAccessFilter : IActionFilter
    {
        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            Controller controller = context.Controller as Controller;

            if (!(controller.ControllerContext.ActionDescriptor.ControllerName == Constants.AuthCodeController ||
                 controller.ControllerContext.ActionDescriptor.ControllerName == "Notification"))
            {
                if (string.IsNullOrWhiteSpace(session.GetString("UserId")))
                {
                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        context.Result = new UnauthorizedObjectResult("User Invalid");
                    }
                }
            }
        }
    }
}
