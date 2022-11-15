using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PWAFeaturesRnd.Common;

namespace PWAFeaturesRnd.AppCode
{
    /// <summary>
    /// Session Timeout Action Filter
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IActionFilter" />
    public class SessionTimeoutActionFilter : IActionFilter
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

            if (controller != null)
            {
                //following code not execute for notification & authcode controller 
                if (!(controller.ControllerContext.ActionDescriptor.ControllerName == "Notification" || controller.ControllerContext.ActionDescriptor.ControllerName == Constants.AuthCodeController))
                {
                    if (string.IsNullOrWhiteSpace(session.GetString("UserId")))
                    {
                        RedirectResult logOutURL = new RedirectResult("/Authcode/Logout");
                        context.Result = logOutURL;
                    }
                }                
            }
        }
    }
}
