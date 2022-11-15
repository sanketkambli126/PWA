using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.Login;

namespace PWAFeaturesRnd.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            //LoginViewModel response = await RestAppClientFactory.Instance.GetAccessToken(loginViewModel);

            //if (response.AuthenticationError == null || response.AuthenticationError == "")
            //{
            //    CreateUserIdentity(response);

            //}
            //else
            //{
            //    return View("Index");
            //}

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        

        #region Private Methods
        private void CreateUserIdentity(LoginViewModel user)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username)}, user.ClientId);
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)//AppSettings.IdentityCookieExpireTime
            };

            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }

       
        #endregion
    }
}