using System;

namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Management vessel detail for lookup
    /// </summary>
    public class ManagementVesselDetail
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
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the management start date.
        /// </summary>
        /// <value>
        /// The management start date.
        /// </value>
        public DateTime? ManagementStartDate { get; set; }

        /// <summary>
        /// Gets or sets the management end date.
        /// </summary>
        /// <value>
        /// The management end date.
        /// </value>
        public DateTime? ManagementEndDate { get; set; }

        /// <summary>
        /// Gets or sets the technical office identifier.
        /// </summary>
        /// <value>
        /// The technical office identifier.
        /// </value>
        public string TechnicalOfficeId { get; set; }

        /// <summary>
        /// Gets or sets the vessel management identifier.
        /// </summary>
        /// <value>
        /// The vessel management identifier.
        /// </value>
        public string VesselManagementId { get; set; }

        /// <summary>
        /// Gets or sets the vessel management purchasing date end.
        /// </summary>
        /// <value>
        /// The vessel management purchasing date end.
        /// </value>
        public DateTime? VesselManagementPurchasingDateEnd { get; set; }

        /// <summary>
        /// Gets or sets the vessel management purchasing date start.
        /// </summary>
        /// <value>
        /// The vessel management purchasing date start.
        /// </value>
        public DateTime? VesselManagementPurchasingDateStart { get; set; }

        /// <summary>
        /// Gets or sets the vessel management office type description.
        /// </summary>
        /// <value>
        /// The vessel management office type description.
        /// </value>
        public string VesselManagementOfficeTypeDescription { get; set; }
    }
}
