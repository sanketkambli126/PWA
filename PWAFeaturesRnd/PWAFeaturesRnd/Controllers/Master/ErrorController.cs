using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.IO;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Report.Crew;
using PWAFeaturesRnd.ViewModels.Crew;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class ErrorController : AuthenticatedController
    {
        #region Private Variables

        /// <summary>
        /// The client
        /// </summary>
		private SharedClient _sharedClient;

        /// <summary>
        /// The web host environment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// The crew client
        /// </summary>
        private CrewClient _crewClient;

        private readonly ILogger<ErrorController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// </summary>
        /// <param name="sharedClient">The shared client.</param>
        /// <param name="webHostEnvironment">The web host environment.</param>
        /// <param name="crewClient">The crew client.</param>
        public ErrorController(SharedClient sharedClient, IWebHostEnvironment webHostEnvironment, CrewClient crewClient, ILogger<ErrorController> logger)
        {
            _sharedClient = sharedClient;
            _crewClient = crewClient;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }


        #endregion

        #region Actions

        /// <summary>
        /// Errors the log.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ErrorLog()
        {
            _sharedClient.AccessToken = GetAccessToken();
            var response = await _sharedClient.PostGetFleetDetailById("GLAS00074720");
            var userName = HttpContext.Session.GetString(Constants.UserNameSessionKey);
            var userid = HttpContext.Session.GetString("UserId");

            string message, source, lineno, columnNo, error;
            if (Request != null)
            {
                message = (string)Request.Form["message"];
                source = (string)Request.Form["source"];
                lineno = (string)Request.Form["lineno"];
                columnNo = (string)Request.Form["columnNo"];
                error = (string)Request.Form["error"];
                LogErrorToFile(message, source, lineno, columnNo, error);
                //Log(NLog.LogLevel.Error, message, error, null);
                _logger.LogError(message);
            }
            //_logger.LogInformation("HomeController.Index method called!!!");

            EmptyResult empty = new EmptyResult();
            return empty;
        }

        private void Log(NLog.LogLevel level, string message, string log, Exception ex)
        {
            var logEvent = new LogEventInfo(level, "PWA Client", message);

            _logger.LogError("", logEvent);
        }


        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> SaveErrorLog()
        {
            _crewClient.AccessToken = GetAccessToken();

            CrewSummaryRequest summaryRequest = new CrewSummaryRequest();
            summaryRequest.VesselId = "GLAS00015461";
            summaryRequest.FromDate = DateTime.Now.Date.AddMonths(-1);
            summaryRequest.ToDate = DateTime.Now.Date;
            summaryRequest.PastNumberOfDays = 30;
            CrewSummaryResponseViewModel responseVM = await _crewClient.PostGetCrewSummary(summaryRequest);
            return new EmptyResult();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Logs the error to file.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="source">The source.</param>
        /// <param name="lineno">The lineno.</param>
        /// <param name="columnNo">The column no.</param>
        /// <param name="errorInfo">The error information.</param>
        private void LogErrorToFile(string message, string source, string lineno, string columnNo, string errorInfo)
        {
            string userName = HttpContext.Session.GetString(Constants.UserNameSessionKey);
            string userid = HttpContext.Session.GetString("UserId");

            string ErrorColumnNo, ErrorMessage, ErrorSource, ErrorLineNo, ErrorInfo;

            ErrorColumnNo = columnNo ?? string.Empty;
            ErrorMessage = message ?? string.Empty;
            ErrorSource = source ?? string.Empty;
            ErrorLineNo = lineno ?? string.Empty;
            ErrorInfo = errorInfo ?? string.Empty;
            var line = Environment.NewLine + Environment.NewLine;

            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string contentRootPath = _webHostEnvironment.ContentRootPath + "\\ErrorLogs";
                string filepath = contentRootPath + "/ExceptionDetailsFile_";

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";

                if (!System.IO.File.Exists(filepath))
                {
                    System.IO.File.Create(filepath).Dispose();
                }

                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Type:" + "Client Side Error" + line + "User Name: " + userName + line + "User Id: " + userid + line + "Error Message: " + ErrorMessage + line + "Exception Source: " + ErrorSource + line + " Error Line No: " + ErrorLineNo + line + " Error Info: " + ErrorInfo + line + "Error Column No: " + ErrorColumnNo + line;
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

        #endregion
    }
}