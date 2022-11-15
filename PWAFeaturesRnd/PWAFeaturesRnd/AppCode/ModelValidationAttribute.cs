using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Exceptions;

namespace PWAFeaturesRnd.AppCode
{
	public class ModelValidationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				context.HttpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
				//  throw new Exception("Server side validation failed.");// returns 400 with error

				var errors = context.ModelState.Select(x => x.Value.Errors)
							   .Where(y => y.Count > 0)
							   .ToList();

				throw new CustomException(AppSettings.Environment + " : " + errors.FirstOrDefault().FirstOrDefault().ErrorMessage)
				{
					Category = ExceptionCategory.ValidationError,
					DetailMessaage = errors.FirstOrDefault().FirstOrDefault().ErrorMessage
				};
			}
		}
	}
}
