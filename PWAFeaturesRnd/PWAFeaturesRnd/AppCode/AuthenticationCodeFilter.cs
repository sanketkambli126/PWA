using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Common;

namespace PWAFeaturesRnd.AppCode
{
    public class AuthenticationCodeFilter : IActionFilter
    {
        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context"></param>
        /// Function not in use
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            Controller controller = context.Controller as Controller;

            if (!(controller.ControllerContext.ActionDescriptor.ControllerName == Constants.AuthCodeController ||
                 controller.ControllerContext.ActionDescriptor.ControllerName == "Notification"))
            {
                if (string.IsNullOrWhiteSpace(session.GetString("UserId")))
                {
                    //convert clientId and secret to base64
                    var clientId = Convert.ToBase64String(Encoding.UTF8.GetBytes(AppSettings.ClientID));
                    var clientSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(AppSettings.ClientSecret));

                    var omniUser = GetOmniUserDetails(context);

                    // exchange authorization code at authorization server for an access and refresh token
                    Dictionary<string, string> post = null;
                    post = new Dictionary<string, string>{
                 {"client_id", clientId}
                ,{"client_secret", clientSecret}
                ,{"grant_type", "password"}
                ,{"userName", omniUser.Email}
                ,{"password", omniUser.Password}
            };

                    var client = new HttpClient();
                    var postContent = new FormUrlEncodedContent(post);
                    var response = client.PostAsync(AppSettings.TokenApiUrl + "omnijwttoken", postContent).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        RedirectResult accessDeniedUrl = new RedirectResult(AppSettings.AccessDeniedURL);
                        context.Result = accessDeniedUrl;
                    }
                    else
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        // received token from authorization server
                        var json = JObject.Parse(content);
                        TokenHelper.AccessToken = json["access_token"].ToString();
                        TokenHelper.AuthorizationScheme = json["token_type"].ToString();
                        TokenHelper.ExpiresIn = json["expires_in"].ToString();
                        if (json["refresh_token"] != null)
                            TokenHelper.RefreshToken = json["refresh_token"].ToString();

                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddMonths(6);
                        context.HttpContext.Response.Cookies.Append("ClientWebToken", TokenHelper.AccessToken, option);

                        IdentityModelEventSource.ShowPII = true;
                        Microsoft.IdentityModel.Tokens.SecurityToken validatedToken;
                        TokenValidationParameters validationParameters = new TokenValidationParameters();
                        validationParameters.ValidateIssuerSigningKey = true;
                        validationParameters.ValidAudience = AppSettings.JWTAudience;
                        validationParameters.ValidIssuer = AppSettings.JWTIssuer;
                        var symmetricKey = Convert.FromBase64String(AppSettings.JWTKey);
                        var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(symmetricKey);
                        validationParameters.IssuerSigningKey = securityKey;
                        ClaimsPrincipal principal1 = new JwtSecurityTokenHandler().ValidateToken(TokenHelper.AccessToken, validationParameters, out validatedToken);

                        var claims = principal1.Identities.First().Claims.ToList();
                        //Filter specific claim    
                        SetHttpSessionData(omniUser.UserType, claims, context);
                        AppendInCookies(claims, option, context);
                    }
                }
            }
        }

        /// <summary>
        /// The get omni user details
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private OmniUserClaim GetOmniUserDetails(ActionExecutingContext context)
        {
            var omniUser = new OmniUserClaim();
            if (context.HttpContext.User.Claims.Any(x => x.Type == "OmniUserClaim"))
            {
                var omniUserClaims = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "OmniUserClaim").Value;
                if (!string.IsNullOrWhiteSpace(omniUserClaims))
                {
                    omniUser = JsonConvert.DeserializeObject<OmniUserClaim>(omniUserClaims);
                }
            }

            omniUser.Email = Convert.ToBase64String(Encoding.UTF8.GetBytes(omniUser.Email)); //get email from identity server claims //"punit.jain@vships.com"
            omniUser.UserType = omniUser.UserType; //get type from identity server claims
            omniUser.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes("omniauth")); //password will need to remain hardcoded

            return omniUser;
        }

        /// <summary>
        /// The set http session data
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="claims"></param>
        /// <param name="context"></param>
        private void SetHttpSessionData(string userType, List<Claim> claims, ActionExecutingContext context)
        {
            if (userType == EnumsHelper.GetDescription(UserType.Internal))
            {
                context.HttpContext.Session.SetString("UserType", EnumsHelper.GetDescription(UserType.Internal));
            }
            else
            {
                context.HttpContext.Session.SetString("UserType", EnumsHelper.GetDescription(UserType.Client));
            }
            context.HttpContext.Session.SetString("UserId", claims.FirstOrDefault(x => x.Type == Constants.UserIDClaimType).Value);
            context.HttpContext.Session.SetString(Constants.UserNameSessionKey, claims.FirstOrDefault(x => x.Type == Constants.UserDisplayNameClaimType).Value);
        }

        /// <summary>
        /// The append in cookies
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="option"></param>
        /// <param name="context"></param>
        private void AppendInCookies(List<Claim> claims, CookieOptions option, ActionExecutingContext context)
        {
            //Getting user id    
            context.HttpContext.Response.Cookies.Append("UserId", claims.FirstOrDefault(x => x.Type == Constants.UserIDClaimType).Value, option);
            List<Role> Roles = new List<Role>();
            foreach (Claim item in claims.Where(x => x.Type == Constants.RoleClaimType))
            {
                //deserialize the role claim 
                Role role = Newtonsoft.Json.JsonConvert.DeserializeObject<Role>(item.Value);
                Roles.Add(role);
            }
            string roleString = string.Join(",", Roles.Select(x => x.RoleId));
            context.HttpContext.Response.Cookies.Append("Roles", roleString, option);
            context.HttpContext.Response.Cookies.Append("SignalRURL", AppSettings.SignalRUrl, option);
            context.HttpContext.Response.Cookies.Append("ApplicationId", AppSettings.ApplicationId.ToString(), option);
            context.HttpContext.Response.Cookies.Append("ClientPortalPWAURL", AppSettings.ClientPortalPWAURL, option);
        }
    }

    /// <summary>
    /// Token Helper
    /// </summary>
    public static class TokenHelper
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public static string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        public static string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the authorization scheme.
        /// </summary>
        /// <value>
        /// The authorization scheme.
        /// </value>
        public static string AuthorizationScheme { get; set; }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        public static string ExpiresIn { get; set; }
    }
}
