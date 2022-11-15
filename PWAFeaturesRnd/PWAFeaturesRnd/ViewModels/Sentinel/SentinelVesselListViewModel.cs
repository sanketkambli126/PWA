using System.Collections.Generic;
using PWAFeaturesRnd.Common.Paging;

namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Sentinel Model Dimension Vessel Value Response ViewModel
    /// </summary>
    public class SentinelVesselListViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the model dimension vessel.
        /// </summary>
        /// <value>
        /// The model dimension vessel.
        /// </value>
        public PagedResponse<List<FleetVesselDetailResponseViewModel>> VesselList { get; set; }

        /// <summary>
        /// Gets or sets the model dimension fleet.
        /// </summary>
        /// <value>
        /// The model dimension fleet.
        /// </value>
        public List<OfficeViewDetailResponseViewModel> FleetList { get; set; }

        /// <summary>
        /// Gets or sets the fleet request.
        /// </summary>
        /// <value>
        /// The fleet request.
        /// </value>
        public string FleetRequest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is vl office search available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vl office search available; otherwise, <c>false</c>.
        /// </value>
        public bool IsVLOfficeSearchAvailable { get; set; }

        /// <summary>
        /// Gets or sets the vessel list request.
        /// </summary>
        /// <value>
        /// The vessel list request.
        /// </value>
        public FleetVesselDetailRequestViewModel VesselListRequest { get; set; }
    }
}
