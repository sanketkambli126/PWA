using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Controllers.Base;

namespace PWAFeaturesRnd.AppCode
{
	/// <summary>
	/// Module Access Filter
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IActionFilter" />
	public class ModuleAccessFilter : IActionFilter
	{
		/// <summary>
		/// Called before the action executes, after model binding is complete.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var session = context.HttpContext.Session;
			Controller controller = context.Controller as Controller;
			BaseController baseController = context.Controller as BaseController;

			if (baseController != null && baseController.AccessibleModules != null && baseController.AccessibleModules.Any())
			{
				List<string> userAccessibleModules = session.GetSessionObject<List<string>>(Constants.AccessModuleSessionKey);
				List<string> controllerAccessibleModules = baseController.AccessibleModules;
				if (userAccessibleModules != null && userAccessibleModules.Any() && controllerAccessibleModules != null && controllerAccessibleModules.Any() && !userAccessibleModules.Where(x => controllerAccessibleModules.Contains(x)).Any())
				{
					RedirectResult accessDeniedUrl = new RedirectResult("/Dashboard/ModuleAccessDenied/");
					context.Result = accessDeniedUrl;
				}
			}
		}

		/// <summary>
		/// Called after the action executes, before the action result.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}
	}
}