using System;

namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Dashboard Vessel Detail
    /// </summary>
    public class SentinelDashboardVesselDetail
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the name of the fleet.
        /// </summary>
        /// <value>
        /// The name of the fleet.
        /// </value>
        public string FleetName { get; set; }

        /// <summary>
        /// Gets or sets the vessel model total value.
        /// </summary>
        /// <value>
        /// The vessel model total value.
        /// </value>
        public decimal? VesselModelTotalValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the vessel model total value.
        /// </summary>
        /// <value>
        /// The color of the vessel model total value.
        /// </value>
        public string VesselModelTotalValueColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [new to management].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [new to management]; otherwise, <c>false</c>.
        /// </value>
        public bool? NewToManagement { get; set; }

        /// <summary>
        /// Gets or sets the drydock period start date.
        /// </summary>
        /// <value>
        /// The drydock period start date.
        /// </value>
        public DateTime? DrydockPeriodStartDate { get; set; }

        /// <summary>
        /// Gets or sets the drydock period end date.
        /// </summary>
        /// <value>
        /// The drydock period end date.
        /// </value>
        public DateTime? DrydockPeriodEndDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the drydock port.
        /// </summary>
        /// <value>
        /// The name of the drydock port.
        /// </value>
        public string DrydockPortName { get; set; }

        /// <summary>
        /// Gets or sets the drydock port country code.
        /// </summary>
        /// <value>
        /// The drydock port country code.
        /// </value>
        public string DrydockPortCountryCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [drydock port alert added].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [drydock port alert added]; otherwise, <c>false</c>.
        /// </value>
        public bool DrydockPortAlertAdded { get; set; }

        /// <summary>
        /// Gets or sets the vessel flag.
        /// </summary>
        /// <value>
        /// The vessel flag.
        /// </value>
        public string VesselFlag { get; set; }
    }
}
