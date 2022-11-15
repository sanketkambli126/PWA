using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PWAFeaturesRnd.Models.Report.VoyageReporting;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// Vessel Routing Client
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class VesselRoutingClient : BaseHttpClient
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="VesselRoutingClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public VesselRoutingClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider)
            : base(client, true)
        {
            client.BaseAddress = new Uri(AppSettings.VesselRoutingApi);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        /// <summary>
        /// Gets the alert data.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RouteForecastAlert>> GetAlertData()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/v1/forecastalert"));
            List<RouteForecastAlert> response = await GetAsync<List<RouteForecastAlert>>(requestUrl);

            return response;
        }

    }
}
