using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace PWAFeaturesRnd.Services
{
    public class ConfigureCookieAuthenticationOptions : IPostConfigureOptions<CookieAuthenticationOptions>
    {
        private readonly ITicketStore _ticketStore;
        private readonly IConfiguration configuration;

        public ConfigureCookieAuthenticationOptions(ITicketStore ticketStore, IConfiguration configuration)
        {
            _ticketStore = ticketStore;
            this.configuration = configuration;
        }

        public void PostConfigure(string name,
                 CookieAuthenticationOptions options)
        {
            options.SessionStore = _ticketStore;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(AppSettings.CookieTimeout);
            //options.AccessDeniedPath = new PathString(configuration.GetValue<string>("AzureAd:AccessDeniedPath"));
            //options.Cookie.Domain = configuration.GetValue<string>("CookieDomain");
            //options.Cookie.Name = configuration.GetValue<string>("CookieName");
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.MaxAge = TimeSpan.FromMinutes(AppSettings.CookieTimeout);
        }
    }
}

