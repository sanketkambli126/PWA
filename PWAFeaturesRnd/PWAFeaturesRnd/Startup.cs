using FluentValidation.AspNetCore;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PWAFeaturesRnd.AppCode;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Services;
using PWAFeaturesRnd.ViewModels;
using PWAFeaturesRnd.ViewModels.Common;

namespace PWAFeaturesRnd
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureConfigurationSettings(services);
            ConfigureMVCServices(services);
            services.AddProgressiveWebApp();

            services.AddSingleton<IConfiguration>(_configuration);
            services.AddScoped<ValidateReCaptchaAttribute>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(AppSettings.SessionTimeout);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("cookie", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(AppSettings.CookieTimeout);
                options.Cookie.MaxAge = options.ExpireTimeSpan; // optional
                options.SlidingExpiration = true;
            })

            .AddOpenIdConnect("oidc", options =>
            {
                options.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                options.Authority = _configuration["OmniSettings:AuthorityUrl"];
                options.ClientId = _configuration["OmniSettings:ClientId"];
                options.ClientSecret = _configuration["OmniSettings:ClientSecret"];
                options.ResponseType = "code";
                options.UsePkce = true;
                options.ResponseMode = "query";
                options.SignInScheme = "cookie";
                options.Scope.Add(_configuration["OmniSettings:Scopes:0"]);
                options.SaveTokens = true;

                options.Events = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents
                {
                    OnTokenValidated = async CTX =>
                    {
                        var logger = CTX.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                        var accessToken = CTX.TokenEndpointResponse.AccessToken;
                        logger.LogInformation(accessToken.ToString(), null);

                        HttpClientHandler clientHandler = new HttpClientHandler();
                        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, errors) => { return true; };

                        //// Pass the handler to httpclient(from you are calling api)
                        var client = new HttpClient(clientHandler);
                        var metaDataResponse = await client.GetDiscoveryDocumentAsync(_configuration.GetValue<string>("OmniSettings:AuthorityUrl"));
                        //logger.LogInformation(metaDataResponse.ToString(), null);

                        var response = await client.GetUserInfoAsync(new UserInfoRequest
                        {
                            Address = metaDataResponse.UserInfoEndpoint,
                            Token = accessToken
                        });
                        if (response.IsError)
                        {
                            throw new Exception("Problem while fetching data from the UserInfo endpoint", response.Exception);
                        }

                        var addressClaim = response.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email);
                        if (addressClaim != null)
                        {
                            Claim claim = new Claim(ClaimsIdentity.DefaultNameClaimType, addressClaim.Value);
                            var identities = CTX.Principal.Identities;
                            identities.First().AddClaim(claim);
                            if (response.Claims != null && response.Claims.Any())
                            {
                                // Get client web token on login
                                await AuthenticateClientWebToken(CTX, logger, client, response, addressClaim, identities);
                                AddUserDetailsToIdentities(response, identities);
                                AddPortalsToIdentities(CTX, response, identities);
                            }
                        }
                    }
                };
            });


            services.AddControllers(config =>
            {
                config.Filters.Add(new SessionTimeoutActionFilter());
                config.Filters.Add(new ModuleAccessFilter());
            });
            services.AddTransient<ITicketStore, TicketStore>();
            services.AddSingleton<IPostConfigureOptions<CookieAuthenticationOptions>, ConfigureCookieAuthenticationOptions>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDataProtection();
            services.AddHttpClient<PurchasingClient>();
            services.AddHttpClient<MarineClient>();
            services.AddHttpClient<SharedClient>();
            services.AddHttpClient<FinanceClient>();
            services.AddHttpClient<CrewClient>();
            services.AddHttpClient<TechnicalDashboardClient>();
            services.AddHttpClient<DocumentClient>();
            services.AddHttpClient<VesselRoutingClient>();
            services.AddHttpClient<NotificationClient>();
            services.AddHttpClient<NotificationDocumentClient>();
            services.AddHttpClient<MarineWCFClient>();
            services.AddHttpClient<HSEQManagerDashboardClient>();
            services.AddHttpClient<SSMarineClient>();
        }

        /// <summary>
        /// Adds the portals to identities.
        /// </summary>
        /// <param name="CTX">The CTX.</param>
        /// <param name="response">The response.</param>
        /// <param name="identities">The identities.</param>
        private void AddPortalsToIdentities(TokenValidatedContext CTX, UserInfoResponse response, IEnumerable<ClaimsIdentity> identities)
        {
            bool hasAccess = false;
            List<OmniClaimPortal> portalList;
            var portalClaims = response.Claims.Where(x => x.Type == "Portals").FirstOrDefault();
            portalList = portalClaims != null ? (JsonConvert.DeserializeObject<List<OmniClaimPortal>>(portalClaims.Value)) : new List<OmniClaimPortal>();


            if (portalList != null && portalList.Any())
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") //PiDev
                {
                    hasAccess = portalList.Any(x => x.NavigationURL == _configuration.GetValue<string>("ConfigurationSettings:ClientPortalPWAURL"));
                }
                else
                {
                    hasAccess = portalList.Any(x => (x.NavigationURL ?? "").Contains(CTX.HttpContext.Request.Host.Value));
                }
            }

            if (hasAccess)
            {
                portalList = portalList.Where(x => x.ClientId != _configuration.GetValue<string>("OmniSettings:ClientId")).ToList();
                Claim portalsClaim = new Claim("PortalsAccess", JsonConvert.SerializeObject(portalList));
                identities.First().AddClaim(portalsClaim);
            }
            else
            {
                portalList = new List<OmniClaimPortal>();
                Claim portalsClaim = new Claim("PortalsAccess", JsonConvert.SerializeObject(portalList));
                identities.First().AddClaim(portalsClaim);
            }
        }

        /// <summary>
        /// Adds the user details to identities.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="identities">The identities.</param>
        private static void AddUserDetailsToIdentities(UserInfoResponse response, IEnumerable<ClaimsIdentity> identities)
        {
            OmniUserClaim userClaim = new OmniUserClaim();
            userClaim.Name = response.Claims.Where(x => x.Type == "name").FirstOrDefault().Value;
            userClaim.Email = response.Claims.Where(x => x.Type == "email").FirstOrDefault().Value;
            userClaim.UserType = response.Claims.Where(x => x.Type == "UserType").FirstOrDefault().Value;

            Claim OmniUserClaim = new Claim("OmniUserClaim", JsonConvert.SerializeObject(userClaim));
            identities.First().AddClaim(OmniUserClaim);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="settings">The settings.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<ConfigurationSettings> settings)
        {
            app.UseCors(options => options.AllowAnyOrigin());

            LoadAppsettings(settings);

            ConfigureDashboardVessselDetails();

            if (env.IsDevelopment())
            {
                //app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Common/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}");
            });
        }

        private static void LoadAppsettings(IOptions<ConfigurationSettings> settings)
        {
            AppSettings.PurchasingWebApiUrl = settings.Value.PurchasingWebApiUrl;
            AppSettings.MarineWebApiUrl = settings.Value.MarineWebApiUrl;
            AppSettings.SharedWebApiUrl = settings.Value.SharedWebApiUrl;
            AppSettings.TokenApiUrl = settings.Value.TokenApiUrl;
            AppSettings.ClientID = settings.Value.ClientID;
            AppSettings.ClientSecret = settings.Value.ClientSecret;
            AppSettings.JWTKey = settings.Value.JWTKey;
            AppSettings.JWTIssuer = settings.Value.JWTIssuer;
            AppSettings.JWTAudience = settings.Value.JWTAudience;
            AppSettings.RedirectUrl = settings.Value.RedirectUrl;
            AppSettings.oAuthUrl = settings.Value.oAuthUrl;
            AppSettings.LogInPageURL = settings.Value.LogInPageURL;
            AppSettings.FinanceWebApiUrl = settings.Value.FinanceWebApiUrl;
            AppSettings.CrewWebApiUrl = settings.Value.CrewWebApiUrl;
            AppSettings.IdentityCookieExpireTime = double.Parse(settings.Value.IdentityCookieExpireTime);
            AppSettings.SessionTimeout = double.Parse(settings.Value.SessionTimeout);
            AppSettings.TechnicalDashboardApiUrl = settings.Value.TechnicalDashboardApiUrl;
            AppSettings.DocumentApiUrl = settings.Value.DocumentApiUrl;
            AppSettings.SignalRUrl = settings.Value.SignalRUrl;
            AppSettings.VesselRoutingApi = settings.Value.VesselRoutingApi;
            AppSettings.FleetTrackerURL = settings.Value.FleetTrackerURL;
            AppSettings.NotificationApiURL = settings.Value.NotificationApiURL;
            AppSettings.NotificationDocumentURL = settings.Value.NotificationDocumentURL;
            AppSettings.ApplicationId = int.Parse(settings.Value.ApplicationId);
            AppSettings.NotificationChatURL = settings.Value.NotificationChatURL;
            AppSettings.NotificationCreateDiscussionURL = settings.Value.NotificationCreateDiscussionURL;
            AppSettings.ClientPortalPWAURL = settings.Value.ClientPortalPWAURL;
            AppSettings.PageNotFoundURL = settings.Value.PageNotFoundURL;
            AppSettings.ErrorLoggingOption = settings.Value.ErrorLoggingOption;
            AppSettings.ErrorRedirectOption = settings.Value.ErrorRedirectOption;
            AppSettings.ManageAccountURL = settings.Value.ManageAccountURL;
            AppSettings.AccessDeniedURL = settings.Value.AccessDeniedURL;
            AppSettings.LogOutPageURL = settings.Value.LogOutPageURL;
            AppSettings.MarineWCFApiUrl = settings.Value.MarineWCFApiUrl;
            AppSettings.NotificationChatDetailURL = settings.Value.NotificationChatDetailURL;
            AppSettings.NotificationChatDiscussionURL = settings.Value.NotificationChatDiscussionURL;
            AppSettings.CookieTimeout = double.Parse(settings.Value.CookieTimeout);
            AppSettings.EnableTour = Convert.ToBoolean(settings.Value.EnableTour);
            AppSettings.LogsEnable = Convert.ToBoolean(settings.Value.LogsEnable);
            AppSettings.LogsFileLocation = settings.Value.LogsFileLocation;
            AppSettings.HSEQManagerDashboardApiUrl = settings.Value.HSEQManagerDashboardApiUrl;
            AppSettings.SSMarineApiUrl = settings.Value.SSMarineApiUrl;
        }

        #region Private Methods

        /// <summary>
        /// Configures the configuration settings.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureConfigurationSettings(IServiceCollection services)
        {
            services.Configure<ConfigurationSettings>(_configuration.GetSection("ConfigurationSettings"));
        }

        /// <summary>
        /// Configures the MVC services.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureMVCServices(IServiceCollection services)
        {
            services.AddControllersWithViews(x =>
            {
                x.Filters.Add(typeof(ModelValidationAttribute));
                x.Filters.Add(typeof(GlobalExceptionFilter));
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        /// <summary>
        /// Configures the configuration settings.
        /// </summary>
        private void ConfigureDashboardVessselDetails()
        {
            AppSettings.VesselId1 = _configuration["DashboardVesselDetails:VesselId1"];
            AppSettings.VesselId2 = _configuration["DashboardVesselDetails:VesselId2"];
            AppSettings.VesselId3 = _configuration["DashboardVesselDetails:VesselId3"];
            AppSettings.VesselName1 = _configuration["DashboardVesselDetails:VesselName1"];
            AppSettings.VesselName2 = _configuration["DashboardVesselDetails:VesselName2"];
            AppSettings.VesselName3 = _configuration["DashboardVesselDetails:VesselName3"];
            AppSettings.CoyId1 = _configuration["DashboardVesselDetails:CoyId1"];
            AppSettings.CoyId2 = _configuration["DashboardVesselDetails:CoyId2"];
            AppSettings.CoyId3 = _configuration["DashboardVesselDetails:CoyId3"];
        }

        #endregion


        /// <summary>
        /// The Authenticate Client Web Token
        /// </summary>
        /// <param name="CTX"></param>
        /// <param name="logger"></param>
        /// <param name="client"></param>
        /// <param name="response"></param>
        /// <param name="addressClaim"></param>
        /// <returns></returns>
        private async Task AuthenticateClientWebToken(TokenValidatedContext CTX, ILogger<Startup> logger, HttpClient client, UserInfoResponse response, Claim addressClaim, IEnumerable<ClaimsIdentity> identities)
        {
            try
            {
                Dictionary<string, string> post = null;
                post = new Dictionary<string, string>{
                                        {"client_id", Convert.ToBase64String(Encoding.UTF8.GetBytes(AppSettings.ClientID))}
                                        ,{"client_secret", Convert.ToBase64String(Encoding.UTF8.GetBytes(AppSettings.ClientSecret))}
                                        ,{"grant_type", "password"}
                                        ,{"userName", Convert.ToBase64String(Encoding.UTF8.GetBytes(addressClaim.Value))}
                                        ,{"password", Convert.ToBase64String(Encoding.UTF8.GetBytes("omniauth"))}
                                    };

                using (client = new HttpClient())
                {
                    var postContent = new FormUrlEncodedContent(post);
                    HttpResponseMessage responseMessage = await client.PostAsync(AppSettings.TokenApiUrl + "omnijwttoken", postContent);
                    //if (!responseMessage.IsSuccessStatusCode)
                    //{
                    //    CTX.HttpContext.Response.Redirect(AppSettings.AccessDeniedURL);
                    //    CTX.HandleResponse();
                    //}

                    var content = "{\"access_token\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vVlNoaXAvU2hpcFN1cmUvQ2xpZW50SUQiOlsiNDAzOUEzNzgtMjlEQi00Mjk4LUIxRjEtOEQ0M0VGNjM2MzIzIiwiNDAzOUEzNzgtMjlEQi00Mjk4LUIxRjEtOEQ0M0VGNjM2MzIzIl0sInVuaXF1ZV9uYW1lIjoia2h5YXRpLnBhcmlraEB2c2hpcHMuY29tIiwiaHR0cDovL1ZTaGlwL1NoaXBTdXJlL1VzZXJJRCI6IlZHUlAwMDAwNDEyMCIsImh0dHA6Ly9WU2hpcC9TaGlwU3VyZS9TaXRlSUQiOiJTWVNUMDAwMDAwMDEiLCJodHRwOi8vVlNoaXAvU2hpcFN1cmUvRGVwYXJ0bWVudElEIjoiQUNDVCIsImh0dHA6Ly9WU2hpcC9TaGlwU3VyZS9Vc2VyRGlzcGxheU5hbWUiOiJLaHlhdGkgUGFyaWtoIiwiaHR0cDovL1ZTaGlwL1NoaXBTdXJlL1VzZXJFbWFpbCI6IktoeWF0aS5QYXJpa2hAdnNoaXBzLmNvbSIsImh0dHA6Ly9WU2hpcC9TaGlwU3VyZS9TaGlwU3VyZVJvbGUiOlsie1wiUm9sZUlkXCI6XCJGTlJBMDAwMDAwMDZcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiU2FsZXMgSW52b2ljZSBMZXZlbCA0XCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkZOUkEwMDAwMDAxMVwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJTYWxlcyBDcmVkaXQgTm90ZSBMZXZlbCA0XCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkZOUkEwMDAwMDAxNVwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJKb3VybmFsIExldmVsIDNcIixcIklzUHJpbWFyeVwiOmZhbHNlfSIsIntcIlJvbGVJZFwiOlwiRk5SQTAwMDAwMDE4XCIsXCJSb2xlRGVzY3JpcHRpb25cIjpcIkpvdXJuYWwgTGV2ZWwgNlwiLFwiSXNQcmltYXJ5XCI6ZmFsc2V9Iiwie1wiUm9sZUlkXCI6XCJGTlJBMDAwMDAwMjNcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiTm9uIFBPIEludm9pY2UgTGV2ZWwgNVwiLFwiSXNQcmltYXJ5XCI6ZmFsc2V9Iiwie1wiUm9sZUlkXCI6XCJGTlJBMDAwMDAwMjdcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiTm9uIFBPIENyZWRpdCBOb3RlIExldmVsIDRcIixcIklzUHJpbWFyeVwiOmZhbHNlfSIsIntcIlJvbGVJZFwiOlwiRk5SQTAwMDAwMDI4XCIsXCJSb2xlRGVzY3JpcHRpb25cIjpcIk5vbiBQTyBDcmVkaXQgTm90ZSBMZXZlbCA1XCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkZOUkEwMDAwMDAzMlwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJBY2NvdW50cyBFbnRpdHkgLSBKb3VybmFsIEFwcHJvdmVyXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkZOUkEwMDAwMDAzNlwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJFbnRpdHkgQWNjb3VudHMgLSBUcmF2ZWwgSW50ZXJmYWNlXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkZOUkEwMDAwMDAzN1wiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJFbnRpdHkgQWNjb3VudHMgLSBUcmF2ZWwgQlNQXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkZOUkEwMDAwMDAzOFwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJFbnRpdHkgQWNjb3VudHMgLSBNVFMgSW50ZXJmYWNlXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkdMQVMwMDAwMDAwMlwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJGbGVldCBNYW5hZ2VyXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkdMQVMwMDAwMDAwNlwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJBY2NvdW50cyAtIFZlc3NlbCBFbGVjdHJvbmljIFBheW1lbnRzXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkdMQVMwMDAwMDAwOFwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJBY2NvdW50cyAtIExlZGdlckdlbmVyYWwgRmluYW5jZU1hbmFnZXIgVkZDXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkdMQVMwMDAwMDAxMlwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJJbmZvcm1hdGlvbiBTeXN0ZW1zIERlcGFydG1lbnRcIixcIklzUHJpbWFyeVwiOmZhbHNlfSIsIntcIlJvbGVJZFwiOlwiR0xBUzAwMDAwMDg5XCIsXCJSb2xlRGVzY3JpcHRpb25cIjpcIk1hc3RlclwiLFwiSXNQcmltYXJ5XCI6ZmFsc2V9Iiwie1wiUm9sZUlkXCI6XCJHTEFTMDAwMDAxMTRcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiQWNjb3VudHMgLSBTZWN1cmUgQUxMIFJhdGVzIEN1cnJlbmN5IFVwbG9hZGVyXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIkdMQVMwMDAwMDExNVwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJBY2NvdW50cyAtIFNlY3VyZSBQZXJpb2RpYyBSYXRlcyBDdXJyZW5jeSBVcGxvYWRlclwiLFwiSXNQcmltYXJ5XCI6ZmFsc2V9Iiwie1wiUm9sZUlkXCI6XCJHTEFTMDAwMDAxMTlcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiQWNjb3VudHMgLSBTZWN1cmUgTWFzdGVyIENoYXJ0IFVwZGF0ZVwiLFwiSXNQcmltYXJ5XCI6ZmFsc2V9Iiwie1wiUm9sZUlkXCI6XCJHTEFTMDAwMDAxMjBcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiQWNjb3VudHMgLSBTZWN1cmUgQmFuayBVcGRhdGVcIixcIklzUHJpbWFyeVwiOmZhbHNlfSIsIntcIlJvbGVJZFwiOlwiR0xBUzAwMDAwMTIxXCIsXCJSb2xlRGVzY3JpcHRpb25cIjpcIkFjY291bnRzIC0gU2VjdXJlIFRheCBDb2RlcyBNYWludGFpblwiLFwiSXNQcmltYXJ5XCI6ZmFsc2V9Iiwie1wiUm9sZUlkXCI6XCJHTEFTMDAwMDAxMjNcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiRW50aXR5IC0gU3lzdGVtIEFkbWluIChyZXN0cmljdGVkIHVzZXJzKVwiLFwiSXNQcmltYXJ5XCI6ZmFsc2V9Iiwie1wiUm9sZUlkXCI6XCJHTEFTMDAwMDAxMjVcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiQWNjb3VudHMgRW50aXR5IC0gU3lzdGVtIEFkbWluIChyZXN0cmljdGVkKVwiLFwiSXNQcmltYXJ5XCI6ZmFsc2V9Iiwie1wiUm9sZUlkXCI6XCJHTEFTMDAwMDAxMjZcIixcIlJvbGVEZXNjcmlwdGlvblwiOlwiQWNjb3VudHMgRW50aXR5IC1TZW5pb3IgTWFuYWdlbWVudCBSZWFkIE9ubHlcIixcIklzUHJpbWFyeVwiOmZhbHNlfSIsIntcIlJvbGVJZFwiOlwiR0xBUzAwMDAwMTQwXCIsXCJSb2xlRGVzY3JpcHRpb25cIjpcIkFjY291bnRzIC0gU2VjdXJlIFZlc3NlbCBFeGNoUmV2YWwgUmUtcnVuXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIk1SUkEwMDAwMDAwMVwiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJTdXBlciBBZG1pblwiLFwiSXNQcmltYXJ5XCI6dHJ1ZX0iLCJ7XCJSb2xlSWRcIjpcIlBVUkEwMDAwMDAwM1wiLFwiUm9sZURlc2NyaXB0aW9uXCI6XCJQTyBMZXZlbCAzXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iLCJ7XCJSb2xlSWRcIjpcIi0xXCIsXCJSb2xlRGVzY3JpcHRpb25cIjpcIkFwaVRlc3RSb2xlXCIsXCJJc1ByaW1hcnlcIjpmYWxzZX0iXSwibmJmIjoxNjcxNzY5NTA5LCJleHAiOjE2NzE4MDY3MDksImlhdCI6MTY3MTc2OTUwOSwiaXNzIjoiVlNoaXBzVG9rZW5BcGkiLCJhdWQiOiJWU2hpcHNSZXNvdXJjZUFwaSJ9.aq4LgdBPlfE5JJLzo6hn76gl7v42i7ghAmFghO7XV4M\",\"token_type\":\"bearer\",\"expires_in\":37199,\"refresh_token\":\"2656d891df42488a952b8f7d19372a1a\"}"; //
                    //var content = await responseMessage.Content.ReadAsStringAsync();
                    // received token from authorization server
                    var json = JObject.Parse(content);
                    TokenHelper.AccessToken = json["access_token"].ToString();
                    TokenHelper.AuthorizationScheme = json["token_type"].ToString();
                    TokenHelper.ExpiresIn = json["expires_in"].ToString();
                    if (json["refresh_token"] != null)
                        TokenHelper.RefreshToken = json["refresh_token"].ToString();

                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMonths(6);
                    Claim clientPortalTokenClaim = new Claim("ClientWebToken", TokenHelper.AccessToken);
                    identities.First().AddClaim(clientPortalTokenClaim);


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
                    var userType = response.Claims.Where(x => x.Type == "UserType").FirstOrDefault().Value;
                    AppendInCookies(claims, option, CTX);
                    SetHttpSessionData(userType, claims, CTX);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, CTX.Principal.Identity.Name, "Error while calling Authorisation Service");
            }
        }

        /// <summary>
        /// The SetHttpSessionData
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="claims"></param>
        /// <param name="context"></param>
        private void SetHttpSessionData(string userType, List<Claim> claims, TokenValidatedContext context)
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
        /// The AppendInCookies
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="option"></param>
        /// <param name="context"></param>
        private void AppendInCookies(List<Claim> claims, CookieOptions option, TokenValidatedContext context)
        {
            //Getting user id 
            context.Response.Cookies.Append("UserId", claims.FirstOrDefault(x => x.Type == Constants.UserIDClaimType).Value, option);
            context.Response.Cookies.Append("EmailId", claims.FirstOrDefault(x => x.Type == Constants.UserEmailClaimType).Value, option);
            List<Role> Roles = new List<Role>();
            foreach (Claim item in claims.Where(x => x.Type == Constants.RoleClaimType))
            {
                //deserialize the role claim 
                Role role = Newtonsoft.Json.JsonConvert.DeserializeObject<Role>(item.Value);
                Roles.Add(role);
            }
            string roleString = string.Join(",", Roles.Select(x => x.RoleId));
            context.Response.Cookies.Append("Roles", roleString, option);
            context.Response.Cookies.Append("SignalRURL", AppSettings.SignalRUrl, option);
            context.Response.Cookies.Append("ApplicationId", AppSettings.ApplicationId.ToString(), option);
            context.Response.Cookies.Append("ClientPortalPWAURL", AppSettings.ClientPortalPWAURL, option);
        }

    }
}
