using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels;

namespace PWAFeaturesRnd.AppCode
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter" />
    public class GlobalExceptionFilter : IExceptionFilter 
    {
        #region Private Variables

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly ConfigurationSettings _configuration;

        /// <summary>
        /// The web host environment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// The shared client
        /// </summary>
        private readonly SharedClient _sharedClient;

        /// <summary>
		/// The ticket store
		/// </summary>
		private ITicketStore _ticketStore;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionFilter" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="webHostEnvironment">The web host environment.</param>
        /// <param name="sharedClient">The shared client.</param>
        /// <param name="ticketStore">The ticket store</param>
        public GlobalExceptionFilter(IOptions<ConfigurationSettings> configuration, IWebHostEnvironment webHostEnvironment, SharedClient sharedClient, ITicketStore ticketStore)
        {
            _configuration = configuration.Value;
            _webHostEnvironment = webHostEnvironment;
            _sharedClient = sharedClient;
            _ticketStore = ticketStore;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        /// <exception cref="System.ArgumentNullException">Context cannot be null.</exception>
        /// <exception cref="ArgumentNullException">Context cannot be null.</exception>
        public async void OnException(ExceptionContext context)
        {
            if (context is null)
            {
                //ToDo - Message should come from constants file.
                throw new ArgumentNullException("Context cannot be null.");
            }
            var host = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}";

            LogUnhandeledException(context.HttpContext, context.Exception);

            context.ExceptionHandled = true;
            string ErrorMessage = "";

            if (context.Exception is BusinessException)
            {
                BusinessException customException = context.Exception as BusinessException;
                ErrorMessage = $@"{customException.Message}";
                // commented for while because
                //if (customException.Category == ExceptionCategory.BusinessError)
                //{
                //	ErrorMessage = $@"{EnumsHelper.GetErrorMessageFromValue(customException.Detail)}";
                //}
                //else
                //if (customException.Category == ExceptionCategory.ValidationError || customException.Category == ExceptionCategory.InputFormatError)
                //{
                //    ErrorMessage = $@"{customException.Message}";
                //}
                //else //Unexpected error are handled here
                //{
                //    ErrorMessage = $@"{EnumsHelper.GetErrorMessageFromValue(customException.Category)}";
                //}
            }
            else
            {
                ErrorMessage = $@"{context.Exception.Message}";
            }

            if (context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                if (context.Exception is BusinessException)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    if (context.Exception.GetType().ToString() == "System.UnauthorizedAccessException")
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                }

                context.Result = new JsonResult(new
                {
                    Message = ErrorMessage
                });
            }
            var port = context.HttpContext.Request.Host.Port;
            var controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerFullName = controllerDescriptor.ControllerTypeInfo.FullName;
            var actionName = controllerDescriptor.ActionName;
            var StackTrace = context.Exception.StackTrace;

            //Custom Error log in file
            //LogErrorToFile(context.HttpContext, context.Exception);

            //if (AppSettings.ErrorRedirectOption == 1)
            //{
            //    //Will redirect to an error page
            //    RedirectResult pageNotFoundURL = new RedirectResult(host + AppSettings.PageNotFoundURL, true);
            //    context.Result = pageNotFoundURL;
            //}
            //else if (AppSettings.ErrorRedirectOption == 2)
            //{
            //    // will show toaster alert
            //}
            //else if (AppSettings.ErrorRedirectOption == 3)
            //{
            //    // will not show error
            //}

            if (AppSettings.ErrorLoggingOption == 1)
            {
                //Only logs error in DB
                //DB entry of error log
                await SaveErrorLogInDb(context, actionName);
            }
            else if (AppSettings.ErrorLoggingOption == 2)
            {
                //Only logs error in File
                Logging.logger.Log(
                      NLog.LogLevel.Error,
                      Environment.NewLine +
                      "Project Name: " + "Client Portal" + ", Host:" + host + ", Port:" + port + ", Controller: " + controllerFullName + ", ActionName: " + actionName + ", Exception: " + context.Exception.Message + ", Stack Trace: " + StackTrace + ", DateTime: " + DateTime.Now.ToString() + Environment.NewLine);
            }
            else if (AppSettings.ErrorLoggingOption == 3)
            {
                //Will log error in both DB and file
                await SaveErrorLogInDb(context, actionName);
                Logging.logger.Log(
                      NLog.LogLevel.Error,
                      Environment.NewLine +
                      "Project Name: " + "Client Portal" + ", Host:" + host + ", Port:" + port + ", Controller: " + controllerFullName + ", ActionName: " + actionName + ", Exception: " + context.Exception.Message + ", Stack Trace: " + StackTrace + ", DateTime: " + DateTime.Now.ToString() + Environment.NewLine);
            }
            else if (AppSettings.ErrorLoggingOption == 4)
            {
                //Will not log error
            }

            //another way of NLog implementation
            //_logger.Log(LogLevel.Error, context.Exception, ErrorMessage);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Logs the unhandeled exception.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="exception">The exception.</param>
        private void LogUnhandeledException(HttpContext httpContext, Exception exception)
        {
            //ErrorLogRequest errorLogRequest = new ErrorLogRequest();

            //if (httpContext != null)
            //{
            //	Claim userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            //	errorLogRequest.UserId = userIdClaim != null ? Convert.ToInt32(userIdClaim.Value) : (int?)null;

            //	Claim userLoginIdClaim = httpContext.User.FindFirst(ClaimTypes.Name);
            //	errorLogRequest.UserLoginId = userLoginIdClaim != null ? userLoginIdClaim.Value : httpContext.Session.GetString("LoginId");

            //	errorLogRequest.SourceURL = httpContext.Request.Path;

            //	string uaString = httpContext.Request.Headers["User-Agent"];
            //	uaString = uaString.Trim();
            //	var uaParser = Parser.GetDefault();
            //	var userAgentDetails = uaParser.Parse(uaString);

            //	if (userAgentDetails != null)
            //	{
            //		errorLogRequest.OtherDetails = "OS : " + userAgentDetails.OS.ToString()
            //							+ ", Device OS Details : " + userAgentDetails.Device.ToString()
            //							+ ", Client Device Name : " + Dns.GetHostEntry(httpContext.Connection.RemoteIpAddress).HostName
            //							+ ", Client IP Address : " + httpContext.Connection.RemoteIpAddress.ToString();
            //		errorLogRequest.ClientBrowserDetails = userAgentDetails.UA.ToString();
            //	}
            //}
            //if (exception != null)
            //{
            //	errorLogRequest.Message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
            //	errorLogRequest.StackTrace = exception.StackTrace;

            //	errorLogRequest.AetId = EnumHelpers.GetErrorCodeValue(ExceptionCategory.UnexpectedError);
            //}
            //ErrorLogger.LogException(errorLogRequest);

        }



        /// <summary>
        /// Logs the error to file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        private void LogErrorToFile(HttpContext context, Exception ex)
        {
            string StackTrace, Errormsg, extype, ErrorMessage;

            var line = Environment.NewLine + Environment.NewLine;

            StackTrace = ex.StackTrace;
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();

            ErrorMessage = ex.Message.ToString();
            var userName = context.Session.GetString(Constants.UserNameSessionKey);
            var userid = context.Session.GetString("UserId");
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string contentRootPath = _webHostEnvironment.ContentRootPath + "\\ErrorLogs";
                string filepath = contentRootPath + "/ExceptionDetailsFile_";  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt"; //Text File Name

                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Type:" + " Server Side Error" + line + "User name " + userName + line + "User Id " + userid + line + "Error Stack Trace :" + " " + StackTrace + line + "Exception Type:" + " " + extype + line + "Error Message :" + " " + ErrorMessage + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        /// <summary>
        /// Saves the error log in database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="methodName">Name of the method.</param>
        private async Task SaveErrorLogInDb(ExceptionContext context, string methodName)
        {
            string token = null;
            string notificationWebToken = null;
            if (context.HttpContext.User.Claims.Any(x => x.Type == "ClientWebToken"))
            {
                token = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "ClientWebToken").Value;
            }

            var identities = context.HttpContext.User.Identities;
            if (string.IsNullOrWhiteSpace(identities.First().Name)) //Check user are authenticated or not
            {
                string key = context.HttpContext.Request.Cookies["RetrievingKey"];
                AuthenticationTicket authenticationTicket = await _ticketStore.RetrieveAsync(key);
                ClaimsPrincipal principal = authenticationTicket.Principal;
                var claims = principal.Identities.First().Claims.ToList();
                if (claims.Any(x => x.Type == "NotificationWebToken"))
                {
                    notificationWebToken = claims.FirstOrDefault(x => x.Type == "NotificationWebToken").Value;
                }
            }
            else
            {
                //Checking token exist or not in claims
                if (context.HttpContext.User.Claims.Any(x => x.Type == "NotificationWebToken"))
                {
                    notificationWebToken = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NotificationWebToken").Value;
                }
            }            

            if (token != null || notificationWebToken != null)
            {
                _sharedClient.AccessToken = token ?? notificationWebToken;

                ErrorLogRequest logRequest = new ErrorLogRequest();
                logRequest.ExceptionType = "Error";
                logRequest.SourceApplication = "Client Portal";
                logRequest.ExceptionCategory = context.Exception.GetType().ToString();
                logRequest.Message = context.Exception.Message.ToString();
                logRequest.StackTrace = context.Exception.StackTrace;
                //logRequest.InnerException = context.Exception != null ? context.Exception.InnerException.ToString() : string.Empty;
                logRequest.AdditionalInfo = context.Exception.Message;
                logRequest.MethodName = methodName;
                try
                {
                    var response = await _sharedClient.SaveErrorLog(logRequest);
                }
                catch (Exception ex)
                {
                    await SaveLogInFile(context, "");
                }
            }
            else
            {
                await SaveLogInFile(context, "Token not found.");
            }
        }

        /// <summary>
        /// Saves the log in file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="additionalMessage">The additional message.</param>
        private async Task SaveLogInFile(ExceptionContext context, string additionalMessage)
        {
            try
            {
                var host = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}";
                var port = context.HttpContext.Request.Host.Port;
                var controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var controllerFullName = controllerDescriptor.ControllerTypeInfo.FullName;
                var actionName = controllerDescriptor.ActionName;
                var StackTrace = context.Exception.StackTrace;

                Logging.logger.Log(
                          NLog.LogLevel.Error,
                          Environment.NewLine +
                          "Project Name: " + "Client Portal" + ", Host:" + host + ", Port:" + port + ", Controller: " + controllerFullName + ", ActionName: " + actionName + ", Exception: " + additionalMessage + " " + context.Exception.Message + ", Stack Trace: " + StackTrace + ", DateTime: " + DateTime.Now.ToString() + Environment.NewLine);
            }
            catch(Exception ex)       
            {
                // This worst condtion if file not created.
            }
        }

        #endregion
    }
}
