using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Models.Report.TechnicalDashboard;
using PWAFeaturesRnd.Models.Report.Vessel;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.VesselManagement;

namespace PWAFeaturesRnd.Helper
{
    public class TechnicalDashboardClient : BaseHttpClient
    {
        #region Private Properties

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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CrewClient"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public TechnicalDashboardClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider, IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
        {
            client.BaseAddress = new Uri(AppSettings.TechnicalDashboardApiUrl);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        #endregion

        /// <summary>
        /// Posts the get crew summary.
        /// </summary>
        /// <param name="crewRequest">The crew request.</param>
        /// <returns></returns>
        public async Task<string> GetOltVesselPerformance(string vesselId)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
            string VesselId = decreptedString.Split(Constants.Separator)[0];
            string rightShip = "N/A";
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "v1.0/OltVesselPerformance/" + VesselId));

            OfficeFleetCellMatrixFleetViewModel response = await GetAsync<OfficeFleetCellMatrixFleetViewModel>(requestUrl);

            if (response != null && response.RightShip != null)
            {
                rightShip = response.RightShip.ToString();
            }

            return rightShip;
        }

        /// <summary>
        /// Gets the fp date list for historic data.
        /// </summary>
        /// <param name="requestType">Type of the request.</param>
        /// <returns></returns>
        public async Task<List<FPHistoricDateDetailsViewModel>> GetFPDateListForHistoricData(int requestType)
        {
            List<FPHistoricDateDetailsViewModel> result = new List<FPHistoricDateDetailsViewModel>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "v1/FleetPerformanceDashboard/getFPDateListForHistoricData/" + requestType));
            List<FPHistoricDateDetails> response = await GetAsync<List<FPHistoricDateDetails>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (FPHistoricDateDetails item in response)
                {
                    FPHistoricDateDetailsViewModel rightShip = new FPHistoricDateDetailsViewModel();
                    rightShip.MonthDate = item.MonthDate;
                    result.Add(rightShip);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the right ship GHG rating details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<RightShipResponseViewModel> GetRightShipGHGRatingDetails(RightShipRequestViewModel input)
        {
            RightShipRequest request = new RightShipRequest();
            RightShipResponseViewModel rightShip = new RightShipResponseViewModel();

            string requestVesselId = GetVesselId(input.VesselId);
            string VesselId = !string.IsNullOrWhiteSpace(requestVesselId) ? requestVesselId : null;
            string strMonthDate = input.MonthDate.HasValue ? input.MonthDate.Value.ToString(Constants.FullDateTimeFormat) : null;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "v1/FleetPerformanceDashboard/getRightShipVesselList/" + strMonthDate));
            List<RightShipSummaryDetails> response = await GetAsync<List<RightShipSummaryDetails>>(requestUrl);

            if (response != null && response.Any())
            {
                RightShipSummaryDetails item = response.Where(x => x.VesId == VesselId).FirstOrDefault();
                if (item != null)
                {
                    rightShip.VesselName = item.VesselName;
                    rightShip.RightShipScore = item.RighShipValue ?? 0;
                    rightShip.GHGRating = item.Ghgrating;
                }
            }
            return rightShip;
        }


        /// <summary>
        /// Gets the vessel identifier.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        private string GetVesselId(string encryptedVesselDetail)
        {
            if (!string.IsNullOrWhiteSpace(encryptedVesselDetail))
            {
                string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
                return decryptedString.Split(Constants.Separator)[0];
            }
            return null;
        }
    }
}