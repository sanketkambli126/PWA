using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PWAFeaturesRnd.Controllers
{
    public class AccessController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var clientId = Convert.ToBase64String(Encoding.UTF8.GetBytes(AppSettings.ClientID));
            var clientSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(AppSettings.ClientSecret));
            var redirect_url = AppSettings.RedirectUrl;

            var authorizeArgs = new Dictionary<string, string>
            {
                {"client_id", clientId},
                {"client_secret", clientSecret},
                {"response_type", "code"},
                {"scope", ""},
                {"redirect_uri", redirect_url}
            };

            var content = new FormUrlEncodedContent(authorizeArgs);
            var contentAsString = await content.ReadAsStringAsync();
            return Redirect(AppSettings.oAuthUrl + "?" + contentAsString);
        }
    }
}