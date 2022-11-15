using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.ExportToExcel;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report.Dashboard;

namespace PWAFeaturesRnd.Controllers.Base
{
    /// <summary>
    /// Base Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets or sets the accessible modules.
        /// </summary>
        /// <value>
        /// The accessible modules.
        /// </value>
        public List<string> AccessibleModules { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is demo mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is demo mode; otherwise, <c>false</c>.
        /// </value>
        public bool IsDemoMode
        {
            get
            {
                return GetDemoMode();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Exports to excel.
        /// Data is not added to the request, to make request independent of any type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public FileStreamResult ExportToExcel<T>(List<T> data, ExportToExcelRequest request)
        {
            var stream = new MemoryStream();
            string excelName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            if (request != null)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                    using (ExcelRange rng = workSheet.Cells[1, 1, 1, request.ColumnCount])
                    {
                        rng.Merge = true;
                        rng.Value = request.Title;
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.Font.Bold = true;
                    }

                    using (ExcelRange rng = workSheet.Cells[2, 1, 2, request.ColumnCount])
                    {
                        rng.Merge = true;
                        rng.Value = request.Summary;
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        rng.Style.WrapText = true;
                    }
                    workSheet.Row(2).Height = 15 * request.SummaryRowCount;

                    workSheet.Row(4).Style.Font.SetFromFont(new Font("Calibri", 11, FontStyle.Bold));
                    using (ExcelRange rng = workSheet.Cells[4, 1, data.Count + 5, request.ColumnCount])
                    {
                        rng.LoadFromCollection(data, true);
                    }

                    workSheet.Cells.AutoFitColumns();
                    package.Save();
                }
                stream.Position = 0;
                excelName = $"{request.FileName}_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            }

            return File(stream, "application/octet-stream", excelName);
        }

        /// <summary>
        /// Gets the role rights storage.
        /// </summary>
        /// <returns></returns>
        public List<ControlPermission> GetRoleRightsStorage()
        {
            return CommonUtil.GetSessionObject<List<ControlPermission>>(HttpContext.Session, "RoleRights");
        }

        /// <summary>
        /// Sets the role rights storage.
        /// </summary>
        /// <param name="controlPermissions">The control permissions.</param>
        public void SetRoleRightsStorage(List<ControlPermission> controlPermissions)
        {
            CommonUtil.SetSessionObject(HttpContext.Session, "RoleRights", controlPermissions);
        }

        /// <summary>
        /// Toggles the demo mode.
        /// </summary>
        public void ToggleDemoMode()
        {
            CommonUtil.SetSessionObject(HttpContext.Session, Constants.DemoMode, !IsDemoMode);
        }

        /// <summary>
        /// Removes the RemoveData.
        /// </summary>
        /// <param name="Provider">The provider.</param>
        /// <param name="LatestVesselName">Name of the latest vessel.</param>
        /// <param name="PageFilterKey">The page filter TempData Key.</param>
        /// <param name="DetailsAction">The details action name.</param>
        /// <param name="ListAction">The current list action. default is 'List'</param>
        public void RemoveTempData(IDataProtectionProvider Provider, string LatestVesselName, string PageFilterKey, string DetailsAction, string ListAction = null)
        {
            string referer = Request.Headers["Referer"].ToString();
            ListAction = ListAction == null ? "List" : ListAction;

            if (referer.Contains("/" + ListAction + "/"))
            {
                if (TempData["VesselIdFilter"] != null)
                {
                    string vesselIdTempData = (string)TempData["VesselIdFilter"];
                    string decreptedVesselTempData = Provider.CreateProtector("Vessel").Unprotect(vesselIdTempData);
                    string VesselNameTempData = decreptedVesselTempData.Split(Constants.Separator)[1];
                    if (LatestVesselName != VesselNameTempData)
                    {
                        TempData["VesselIdFilter"] = null;
                        TempData[PageFilterKey] = null;
                    }
                    else
                    {
                        TempData.Keep("VesselIdFilter");
                        TempData.Keep(PageFilterKey);
                    }
                }
            }
            else if (!referer.Contains("/" + DetailsAction + "/"))
            {
                TempData["VesselIdFilter"] = null;
                TempData[PageFilterKey] = null;
            }
        }

        /// <summary>
        /// Sets the session detail.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <param name="parentKey">The parent key.</param>
        /// <param name="defaultParameter">The default parameter.</param>
        public void SetSessionDetail(string pageKey, string parentKey, string defaultParameter)
        {
            string referer = Request.Headers["Referer"].ToString();
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null)
            {
                if (!string.IsNullOrWhiteSpace(parentKey))
                {
                    var parentSession = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, parentKey);
                    if (parentSession != null)
                    {
                        if (!parentSession.ContainsKey("childKey") || parentSession["childKey"] == null)
                        {
                            var childSession = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
                            if (childSession != null)
                            {
                                HttpContext.Session.Remove(pageKey);
                            }
                            var request = new Dictionary<string, object>()
                            {
                                { "src", referer },
                                {"defaultParameter", defaultParameter },
                                { "filter", defaultParameter }
                            };
                            HttpContext.Session.SetSessionObject(pageKey, request);
                            AddChildInParentSession(pageKey, parentKey);
                        }
                        else if (parentSession.ContainsKey("childKey") && parentSession["childKey"] != null)
                        {
                            List<string> childKeys = CommonUtil.GetParameterFromDictionary<List<string>>(parentSession, "childKey");

                            if (!childKeys.Any(x => x == pageKey))
                            {
                                childKeys.Remove(pageKey);
                                parentSession["childKey"] = childKeys;
                                HttpContext.Session.SetSessionObject(parentKey, parentSession);
                                var childSession = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
                                if (childSession != null)
                                {
                                    HttpContext.Session.Remove(pageKey);
                                }
                                var request = new Dictionary<string, object>()
                                {
                                    { "src", referer },
                                    {"defaultParameter", defaultParameter },
                                    { "filter", defaultParameter }
                                };
                                HttpContext.Session.SetSessionObject(pageKey, request);
                                AddChildInParentSession(pageKey, parentKey);
                            }
                        }
                    }
                }
            }
            else
            {
                var request = new Dictionary<string, object>()
                {
                    { "src", referer },
                    {"defaultParameter", defaultParameter },
                    { "filter", defaultParameter }
                };
                HttpContext.Session.SetSessionObject(pageKey, request);
                if (!string.IsNullOrWhiteSpace(parentKey))
                {
                    AddChildInParentSession(pageKey, parentKey);
                }

            }
        }

        /// <summary>
        /// Adds the child in parent session.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <param name="parentKey">The parent key.</param>
        private void AddChildInParentSession(string pageKey, string parentKey)
        {
            var parentSession = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, parentKey);
            if (parentSession != null)
            {
                if (parentSession.ContainsKey("childKey") && parentSession["childKey"] != null)
                {
                    List<string> childKeys = CommonUtil.GetParameterFromDictionary<List<string>>(parentSession, "childKey");
                    childKeys.Add(pageKey);
                    parentSession["childKey"] = childKeys;
                }
                else
                {
                    parentSession.Add("childKey", new List<string>() { pageKey });
                }
                HttpContext.Session.SetSessionObject(parentKey, parentSession);
            }
        }

        /// <summary>
        /// Sets the session filter.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="vesselIdFilter">The vessel identifier filter.</param>
        public void SetSessionFilter(string pageKey, string filter, string vesselIdFilter)
        {
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null)
            {
                if (session.ContainsKey("vesselIdFilter"))
                {
                    session["filter"] = filter;
                    session["vesselIdFilter"] = vesselIdFilter;
                }
                else
                {
                    session["filter"] = filter;
                    session.Add("vesselIdFilter", vesselIdFilter);
                }
                HttpContext.Session.SetSessionObject(pageKey, session);
            }
        }

        /// <summary>
        /// Removes the session filter.
        /// </summary>
        /// <param name="Provider">The provider.</param>
        /// <param name="pageKey">The page key.</param>
        /// <param name="parentKey">The parent key.</param>
        /// <param name="latestVesselId">Name of the latest vessel identifier.</param>
        public void RemoveSessionFilter(IDataProtectionProvider Provider, string pageKey, string parentKey, string latestVesselId)
        {
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null)
            {
                if (session.ContainsKey("vesselIdFilter") && session["vesselIdFilter"] != null)
                {
                    string vesselIdTempData = session["vesselIdFilter"].ToString();
                    string decreptedVesselTempData = Provider.CreateProtector("Vessel").Unprotect(vesselIdTempData);
                    string VesselIdTempData = decreptedVesselTempData.Split(Constants.Separator)[0];
                    if (latestVesselId != VesselIdTempData)
                    {
                        session["filter"] = session["defaultParameter"].ToString();
                        session["vesselIdFilter"] = null;
                        HttpContext.Session.SetSessionObject(pageKey, session);
                    }
                }

                if (string.IsNullOrWhiteSpace(parentKey))
                {
                    RemoveNavigationSession(pageKey);
                }
            }
        }

        /// <summary>
        /// Removes the navigation session.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        private void RemoveNavigationSession(string pageKey)
        {
            var parentSession = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (parentSession != null)
            {
                if (parentSession.ContainsKey("childKey") && parentSession["childKey"] != null)
                {
                    List<string> childKeys = CommonUtil.GetParameterFromDictionary<List<string>>(parentSession, "childKey");
                    foreach (string key in childKeys)
                    {
                        var childSession = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, key);
                        if (childSession != null)
                        {
                            HttpContext.Session.Remove(key);
                        }
                    }
                    parentSession.Remove("childKey");
                }
                HttpContext.Session.SetSessionObject(pageKey, parentSession);
            }
        }

        /// <summary>
        /// Gets the session filter.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns></returns>
        public string GetSessionFilter(string pageKey)
        {
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null && session.ContainsKey("filter") && session["filter"] != null)
            {
                return session["filter"].ToString();
            }
            return null;
        }

        /// <summary>
        /// Gets the session vessel filter.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns></returns>
        public string GetSessionVesselFilter(string pageKey)
        {
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);

            if (session != null && session.ContainsKey("vesselIdFilter") && session["vesselIdFilter"] != null)
            {
                return session["vesselIdFilter"].ToString();
            }
            return null;
        }

        /// <summary>
        /// Gets the source URL.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns></returns>
        public ActionResult GetSourceURL(string pageKey)
        {
            string url = "";
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null)
            {
                url = session["src"].ToString();
                RemoveSessionDetail(pageKey);
            }
            return new JsonResult(url);
        }

        /// <summary>
        /// Gets the source URL string.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns></returns>
        public string GetSourceURLString(string pageKey)
        {
            string url = "";
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null)
            {
                url = session["src"].ToString();
                RemoveSessionDetail(pageKey);
            }
            return url;
        }

        /// <summary>
        /// Removes the session detail.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        private void RemoveSessionDetail(string pageKey)
        {
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null)
            {
                HttpContext.Session.Remove(pageKey);
            }
        }

        public bool AllowFleetSelection()
        {
            string referer = Request.Headers["Referer"].ToString();
            string refererWithoutRequestParameter = referer.Split("?")[0];
            string[] blocks = refererWithoutRequestParameter.Split("/");
            string lastReferer = blocks[blocks.Length - 1];
            if (string.IsNullOrWhiteSpace(lastReferer) && blocks.Length > 0)
            {
                lastReferer = blocks[blocks.Length - 2];
            }
            return lastReferer.Equals("Dashboard", StringComparison.OrdinalIgnoreCase)
                    || blocks.Any(x => x.ToUpper().Equals("Approval".ToUpper()));
        }

        /// <summary>
        /// Gets the last referrer.
        /// </summary>
        /// <returns></returns>
        public string GetLastReferrer()
        {
            string referer = Request.Headers["Referer"].ToString();
            string refererWithoutRequestParameter = referer.Split("?")[0];
            string[] blocks = refererWithoutRequestParameter.Split("/");
            string lastReferer = blocks[blocks.Length - 1];
            if (string.IsNullOrWhiteSpace(lastReferer) && blocks.Length > 0)
            {
                lastReferer = blocks[blocks.Length - 2];
            }
            return lastReferer;
        }

        /// <summary>
        /// Clears the page navigation session.
        /// </summary>
        public void ClearPageNavigationSession()
        {
            List<string> pageKeys = Enum.GetValues(typeof(NavigationPageKey))
                                    .Cast<NavigationPageKey>().Select(v => EnumsHelper.GetKeyValue(v)).ToList();
            if (pageKeys != null && pageKeys.Any())
            {
                foreach (string key in pageKeys)
                {
                    var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, key);
                    if (session != null)
                    {
                        HttpContext.Session.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the dashboard filter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void SetDashboardFilter(DashboardParameter parameter)
        {
            HttpContext.Session.SetSessionObject(Constants.DashboardFilterKey, parameter);
        }

        /// <summary>
        /// Gets the dashboard filter.
        /// </summary>
        /// <returns></returns>
        public DashboardParameter GetDashboardFilter()
        {
            return CommonUtil.GetSessionObject<DashboardParameter>(HttpContext.Session, Constants.DashboardFilterKey);
        }

        /// <summary>
        /// Gets the user modules from session.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserModulesFromSession()
        {
            List<string> accessModules = CommonUtil.GetSessionObject<List<string>>(HttpContext.Session, Constants.AccessModuleSessionKey);
            return new JsonResult(accessModules);
        }

        //TODO: Need to check below code for session usability
        /// <summary>
        /// Sets the session int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetSessionIntValue(string key, int value)
        {
            CommonUtil.SetSessionObject(HttpContext.Session, key, value);
        }

        //TODO: Need to check below code for session usability
        /// <summary>
        /// Gets the session int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public int GetSessionIntValue(string key)
        {
            int? sessionValue = 0;
            sessionValue = HttpContext.Session.GetInt32(key);
            return sessionValue.GetValueOrDefault();
        }

        /// <summary>
        /// Sets the approval filter.
        /// </summary>
        /// <param name="encryptedParameter">The encrypted parameter.</param>
        /// <returns></returns>
        public void SetApprovalFilter(string encryptedParameter)
        {
            string referer = Request.Headers["Referer"].ToString();
            var request = new Dictionary<string, object>()
                            {
                                { "src", referer },
                                {"parameter", encryptedParameter }
                            };
            HttpContext.Session.SetSessionObject(Constants.ApprovalListPageKey, request);
        }

        /// <summary>
        /// Gets the approval filter.
        /// </summary>
        /// <returns></returns>
        public string GetApprovalFilter()
        {
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, Constants.ApprovalListPageKey);
            if (session != null && session.ContainsKey("parameter") && session["parameter"] != null)
            {
                return session["parameter"].ToString();
            }
            return null;
        }

        /// <summary>
        /// Set session storage detail
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="defaultParameter"></param>
        /// <returns></returns>
        public string SetSessionStorageDetail(IDataProtectionProvider provider, object defaultParameter)
        {
            string referer = Request.Headers["Referer"].ToString();

            var request = new Dictionary<string, object>()
                {
                    { "src", referer },
                    {"defaultParameter", defaultParameter },
                    { "filter", defaultParameter }
                };

            return CommonUtil.GetEncryptedURL<Dictionary<string, object>>(provider, "SessionStorage", request);
        }

        /// <summary>
        /// Sets the selected tab.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <param name="selectedTab">The selected tab.</param>
        /// <returns></returns>
        public void SetSelectedTab(string pageKey, string selectedTab)
        {
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null)
            {
                if (session.ContainsKey("ActiveMobileTabClass"))
                {
                    session["ActiveMobileTabClass"] = selectedTab;
                }
                else
                {
                    session.Add("ActiveMobileTabClass", selectedTab);
                }
                HttpContext.Session.SetSessionObject(pageKey, session);
            }
        }

        /// <summary>
        /// Gets the selected tab.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns></returns>
        public string GetSelectedTab(string pageKey)
        {
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, pageKey);
            if (session != null && session.ContainsKey("ActiveMobileTabClass") && session["ActiveMobileTabClass"] != null)
            {
                return session["ActiveMobileTabClass"].ToString();
            }
            return null;
        }

        /// <summary>
        /// Sets the tab.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <param name="requestTab">The request tab.</param>
        /// <param name="defaultTab">The default tab.</param>
        /// <returns></returns>
        public string SetTab(string pageKey, string requestTab, string defaultTab)
        {
            string selectedTab = String.Empty;
            string sessiontab = GetSelectedTab(pageKey);
            if (!String.IsNullOrWhiteSpace(sessiontab))
            {
                selectedTab = sessiontab;
            }
            else if (!String.IsNullOrWhiteSpace(requestTab))
            {
                selectedTab = requestTab;
            }
            else
            {
                selectedTab = defaultTab;
            }
            SetSelectedTab(pageKey, selectedTab);
            return selectedTab;
        }

        /// <summary>
        /// Determines whether [is from view record value] [the specified context].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <c>true</c> if [is from view record value] [the specified context]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFromViewRecordVal(string context)
        {
            if (!string.IsNullOrWhiteSpace(context))
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get access token
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            string token = null;
            if (User.Claims.Any(x => x.Type == "ClientWebToken"))
            {
                token = User.Claims.FirstOrDefault(x => x.Type == "ClientWebToken").Value;
            }
            return token;
        }

        /// <summary>
        /// Get notification access token
        /// </summary>
        /// <returns></returns>        
        public async Task<string> GetNotificationAccessToken(ITicketStore ticketStore = null)
        {
            string token = null;
            var identities = User.Identities;
            if (string.IsNullOrWhiteSpace(identities.First().Name)) //Check user are authenticated or not
            {
                string key = Request.Cookies["RetrievingKey"];
                AuthenticationTicket authenticationTicket = await ticketStore.RetrieveAsync(key);
                ClaimsPrincipal principal = authenticationTicket.Principal;
                var claims = principal.Identities.First().Claims.ToList();
                if (claims.Any(x => x.Type == "NotificationWebToken"))
                {
                    token = claims.FirstOrDefault(x => x.Type == "NotificationWebToken").Value;
                }
            }
            else
            {
                //Checking token exist or not in claims, if exists then avoid to add in claims
                if (User.Claims.Any(x => x.Type == "NotificationWebToken"))
                {
                    token = User.Claims.FirstOrDefault(x => x.Type == "NotificationWebToken").Value;
                }
            }
            return token;
        }

        /// <summary>
        /// Stored notification token
        /// </summary>
        /// <param name="notificationJwtToken"></param>
        public string StoredNoticationToken(string notificationJwtToken, ITicketStore ticketStore)
        {
            string key = "_pwa";
            var identities = User.Identities;
            if (string.IsNullOrWhiteSpace(identities.First().Name))//Check user are authenticated or not
            {
                Microsoft.IdentityModel.Tokens.SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();
                validationParameters.ValidateIssuerSigningKey = true;
                validationParameters.ValidAudience = AppSettings.JWTAudience;
                validationParameters.ValidIssuer = AppSettings.JWTIssuer;
                var symmetricKey = Convert.FromBase64String(AppSettings.JWTKey);
                var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(symmetricKey);
                validationParameters.IssuerSigningKey = securityKey;
                ClaimsPrincipal principal1 = new JwtSecurityTokenHandler().ValidateToken(notificationJwtToken, validationParameters, out validatedToken);
                Claim clientPortalTokenClaim = new Claim("NotificationWebToken", notificationJwtToken);
                var claims = principal1.Identities.First().Claims.ToList();
                Claim claim = new Claim(ClaimsIdentity.DefaultNameClaimType, claims.Where(x => x.Type == ClaimsIdentity.DefaultNameClaimType).FirstOrDefault().Value);
                identities.First().AddClaim(claim);
                principal1.Identities.First().AddClaim(clientPortalTokenClaim);
                AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal1, "cookie");
                key = claims.Where(x => x.Type == ClaimsIdentity.DefaultNameClaimType).FirstOrDefault().Value + "_pwa";
                ticketStore.StoreAsync(authenticationTicket);
            }
            else
            {
                //Checking token exist or not in claims, if exists then avoid to add in claims
                if (!User.Claims.Any(x => x.Type == "NotificationWebToken"))
                {
                    Claim clientPortalTokenClaim = new Claim("NotificationWebToken", notificationJwtToken);
                    identities.First().AddClaim(clientPortalTokenClaim);
                }
            }

            return key;
        }

        /// <summary>
        /// Appends the log.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="text">The text.</param>
        public void AppendLog(string fileName, string text)
        {
            try
            {
                if (AppSettings.LogsEnable)
                {
                    if (!Directory.Exists(AppSettings.LogsFileLocation))
                    {
                        Directory.CreateDirectory(AppSettings.LogsFileLocation);
                    }

                    System.IO.File.AppendAllText(AppSettings.LogsFileLocation + fileName, Environment.NewLine + text);

                }
            }
            catch
            {

            }
            

        }


        /// <summary>
        /// Gets the demo mode.
        /// </summary>
        /// <returns></returns>
        private bool GetDemoMode()
        {
            return CommonUtil.GetSessionObject<bool>(HttpContext.Session, Constants.DemoMode);
        }

        #endregion
    }
}