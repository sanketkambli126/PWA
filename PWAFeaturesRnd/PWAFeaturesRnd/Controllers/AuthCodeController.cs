using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.ViewModels.Common;

namespace PWAFeaturesRnd.Controllers
{
    /// <summary>
    /// Auth Code Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class AuthCodeController : AuthenticatedController
    {
        /// <summary>
        /// Indexes the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// Function not in use
        public async Task<ActionResult> Index()
        {
            //convert clientId and secret to base64
            var clientId = Convert.ToBase64String(Encoding.UTF8.GetBytes(AppSettings.ClientID));
            var clientSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(AppSettings.ClientSecret));
            
            var omniUser = GetOmniUserDetails();

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
            var response = await client.PostAsync(AppSettings.TokenApiUrl + "omnijwttoken", postContent);
            if (!response.IsSuccessStatusCode)
            {
                return Redirect(AppSettings.AccessDeniedURL);
            }
            var content = await response.Content.ReadAsStringAsync();

            // received token from authorization server
            var json = JObject.Parse(content);
            TokenHelper.AccessToken = json["access_token"].ToString();
            TokenHelper.AuthorizationScheme = json["token_type"].ToString();
            TokenHelper.ExpiresIn = json["expires_in"].ToString();
            if (json["refresh_token"] != null)
                TokenHelper.RefreshToken = json["refresh_token"].ToString();

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMonths(6);
            Response.Cookies.Append("ClientWebToken", TokenHelper.AccessToken, option);

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
            SetHttpSessionData(omniUser.UserType, claims);
            AppendInCookies(claims, option);
            
            return RedirectToAction("Index", "Dashboard");
        }
    
        private OmniUserClaim GetOmniUserDetails()
        {
            var omniUser = new OmniUserClaim();
            if (User.Claims.Any(x => x.Type == "OmniUserClaim"))
            {
                var omniUserClaims = User.Claims.FirstOrDefault(x => x.Type == "OmniUserClaim").Value;
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
        
        private void SetHttpSessionData(string userType,List<Claim> claims)
        {
            if (userType == EnumsHelper.GetDescription(UserType.Internal))
            {
                HttpContext.Session.SetString("UserType", EnumsHelper.GetDescription(UserType.Internal));
            }
            else
            {
                HttpContext.Session.SetString("UserType", EnumsHelper.GetDescription(UserType.Client));
            }
            HttpContext.Session.SetString("UserId", claims.FirstOrDefault(x => x.Type == Constants.UserIDClaimType).Value);
            HttpContext.Session.SetString(Constants.UserNameSessionKey, claims.FirstOrDefault(x => x.Type == Constants.UserDisplayNameClaimType).Value);
        }
    
        private void AppendInCookies(List<Claim> claims, CookieOptions option)
        {
            //Getting user id    
            Response.Cookies.Append("UserId", claims.FirstOrDefault(x => x.Type == Constants.UserIDClaimType).Value, option);
            List<Role> Roles = new List<Role>();
            foreach (Claim item in claims.Where(x => x.Type == Constants.RoleClaimType))
            {
                //deserialize the role claim 
                Role role = Newtonsoft.Json.JsonConvert.DeserializeObject<Role>(item.Value);
                Roles.Add(role);
            }
            string roleString = string.Join(",", Roles.Select(x => x.RoleId));
            Response.Cookies.Append("Roles", roleString, option);
            Response.Cookies.Append("SignalRURL", AppSettings.SignalRUrl, option);
            Response.Cookies.Append("ApplicationId", AppSettings.ApplicationId.ToString(), option);
            Response.Cookies.Append("ClientPortalPWAURL", AppSettings.ClientPortalPWAURL, option);
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            var result = LogoutCommand();

            return View("/Views/Shared/Logout.cshtml");
        }

        /// <summary>
        /// Logouts the command.
        /// </summary>
        public async Task LogoutCommand()
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync("cookie");
                await HttpContext.SignOutAsync("oidc");
                
                if (Request.Cookies["UserId"] != null)
                {
                    Response.Cookies.Delete("UserId");
                }

                if (Request.Cookies["Roles"] != null)
                {
                    Response.Cookies.Delete("Roles");
                }

                if (Request.Cookies["SignalRURL"] != null)
                {
                    Response.Cookies.Delete("SignalRURL");
                }

                if (Request.Cookies["ApplicationId"] != null)
                {
                    Response.Cookies.Delete("ApplicationId");
                }

                if (Request.Cookies["ClientPortalPWAURL"] != null)
                {
                    Response.Cookies.Delete("ClientPortalPWAURL");
                }

                ////Session cleare from only this portal. From other portals this session will not clear.
                HttpContext.Session.Clear();
            }
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