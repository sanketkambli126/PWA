namespace PWAFeaturesRnd.ViewModels
{
    /// <summary>
    /// Configuration settings
    /// </summary>
    public class ConfigurationSettings
    {
        /// <summary>
        /// Gets or sets the purchasing web API URL.
        /// </summary>
        /// <value>
        /// The purchasing web API URL.
        /// </value>
        public string PurchasingWebApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the marine web API URL.
        /// </summary>
        /// <value>
        /// The marine web API URL.
        /// </value>
        public string MarineWebApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the shared web API URL.
        /// </summary>
        /// <value>
        /// The shared web API URL.
        /// </value>
        public string SharedWebApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the token API URL.
        /// </summary>
        /// <value>
        /// The token API URL.
        /// </value>
        public string TokenApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientID { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the JWT key.
        /// </summary>
        /// <value>
        /// The JWT key.
        /// </value>
        public string JWTKey { get; set; }

        /// <summary>
        /// Gets or sets the JWT issuer.
        /// </summary>
        /// <value>
        /// The JWT issuer.
        /// </value>
        public string JWTIssuer { get; set; }

        /// <summary>
        /// Gets or sets the JWT audience.
        /// </summary>
        /// <value>
        /// The JWT audience.
        /// </value>
        public string JWTAudience { get; set; }

        /// <summary>
        /// Gets a value indicating whether [show default common error message].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show default common error message]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowDefaultCommonErrorMessage { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ConfigurationSettings" /> is environment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if environment; otherwise, <c>false</c>.
        /// </value>
        public bool Environment { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the o authentication URL.
        /// </summary>
        /// <value>
        /// The o authentication URL.
        /// </value>
        public string oAuthUrl { get; set; }

        /// <summary>
        /// Gets or sets the v ships framework web API.
        /// </summary>
        /// <value>
        /// The v ships framework web API.
        /// </value>
        public string VShipsFrameworkWebApi { get; set; }

        /// <summary>
        /// Gets or sets the log in page URL.
        /// </summary>
        /// <value>
        /// The log in page URL.
        /// </value>
        public string LogInPageURL { get; set; }

        /// <summary>
        /// Gets or sets the identity cookie expire time.
        /// </summary>
        /// <value>
        /// The identity cookie expire time.
        /// </value>
        public string IdentityCookieExpireTime { get; set; }

        /// <summary>
        /// The session timeout
        /// </summary>
        /// <value>
        /// The session timeout.
        /// </value>
        public string SessionTimeout { get; set; }

        /// <summary>
        /// Gets or sets the finance web API URL.
        /// </summary>
        /// <value>
        /// The finance web API URL.
        /// </value>
        public string FinanceWebApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the crew web API URL.
        /// </summary>
        /// <value>
        /// The crew web API URL.
        /// </value>
        public string CrewWebApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the technical dashboard API URL.
        /// </summary>
        /// <value>
        /// The technical dashboard API URL.
        /// </value>
        public string TechnicalDashboardApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the document API URL.
        /// </summary>
        /// <value>
        /// The document API URL.
        /// </value>
        public string DocumentApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the signal r URL.
        /// </summary>
        /// <value>
        /// The signal r URL.
        /// </value>
        public string SignalRUrl { get; set; }

        /// <summary>
        /// Gets or sets the vessel routing API.
        /// </summary>
        /// <value>
        /// The vessel routing API.
        /// </value>
        public string VesselRoutingApi { get; set; }

        /// <summary>
        /// Gets or sets the fleet tracker URL.
        /// </summary>
        /// <value>
        /// The fleet tracker URL.
        /// </value>
        public string FleetTrackerURL { get; set; }

        /// <summary>
        /// Gets or sets the notification API URL.
        /// </summary>
        /// <value>
        /// The notification API URL.
        /// </value>
        public string NotificationApiURL { get; set; }

        /// <summary>
        /// Gets or sets the notification document URL.
        /// </summary>
        /// <value>
        /// The notification document URL.
        /// </value>
        public string NotificationDocumentURL { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the notification chat URL.
        /// </summary>
        /// <value>
        /// The notification chat URL.
        /// </value>
        public string NotificationChatURL { get; set; }

        /// <summary>
        /// Gets or sets the notification chat detail URL.
        /// </summary>
        /// <value>
        /// The notification chat detail URL.
        /// </value>
        public string NotificationChatDetailURL { get; set; }

        /// <summary>
        /// Gets or sets the notification chat discussion URL.
        /// </summary>
        /// <value>
        /// The notification chat discussion URL.
        /// </value>
        public string NotificationChatDiscussionURL { get; set; }

        /// <summary>
        /// Gets or sets the notification create discussion URL.
        /// </summary>
        /// <value>
        /// The notification create discussion URL.
        /// </value>
        public string NotificationCreateDiscussionURL { get; set; }

        /// <summary>
        /// Gets or sets the client portal pwaurl.
        /// </summary>
        /// <value>
        /// The client portal pwaurl.
        /// </value>
        public string ClientPortalPWAURL { get; set; }

        /// <summary>
        /// Gets or sets the page not found URL.
        /// </summary>
        /// <value>
        /// The page not found URL.
        /// </value>
        public string PageNotFoundURL { get; set; }

        /// <summary>
        /// Gets or sets the error logging option.
        /// </summary>
        /// <value>
        /// The error logging option.
        /// </value>
        public int ErrorLoggingOption { get; set; }

        /// <summary>
        /// Gets or sets the error redirect option.
        /// </summary>
        /// <value>
        /// The error redirect option.
        /// </value>
        public int ErrorRedirectOption { get; set; }

        /// <summary>
        /// Gets or sets the manage account URL.
        /// </summary>
        /// <value>
        /// The manage account URL.
        /// </value>
        public string ManageAccountURL { get; set; }

        /// <summary>
        /// Gets or sets the access denied URL.
        /// </summary>
        /// <value>
        /// The access denied URL.
        /// </value>
        public string AccessDeniedURL { get; set; }

        /// <summary>
        /// Gets or sets the log out page URL.
        /// </summary>
        /// <value>
        /// The log out page URL.
        /// </value>
        public string LogOutPageURL { get; set; }

        /// <summary>
        /// Gets or sets the marine WFC API URL.
        /// </summary>
        /// <value>
        /// The marine WFC API URL.
        /// </value>
        public string MarineWCFApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the CookieTimeout.
        /// </summary>
        /// <value>
        /// The CookieTimeout.
        /// </value>
        public string CookieTimeout { get; set; }

        /// <summary>
        /// Gets or sets the Enable tour.
        /// </summary>
        /// <value>
        /// The Enable tour.
        /// </value>
        public string EnableTour { get; set; }

        /// <summary>
        /// Gets or sets the Enable tour.
        /// </summary>
        /// <value>
        /// The Enable tour.
        /// </value>
        public string LogsEnable { get; set; }

        /// <summary>
        /// Gets or sets the Enable tour.
        /// </summary>
        /// <value>
        /// The Enable tour.
        /// </value>
        public string LogsFileLocation { get; set; }

        /// <summary>
        /// Gets or sets the hseq manager dashboard API URL.
        /// </summary>
        /// <value>
        /// The hseq manager dashboard API URL.
        /// </value>
        public string HSEQManagerDashboardApiUrl { get; set; }

		/// <summary>
		/// Gets or sets the ss marine API URL.
		/// </summary>
		/// <value>
		/// The ss marine API URL.
		/// </value>
		public string SSMarineApiUrl { get; set; }
	}
}
