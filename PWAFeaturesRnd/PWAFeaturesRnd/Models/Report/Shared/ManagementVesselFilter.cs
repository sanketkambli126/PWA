namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Management vessel filter for lookup
    /// </summary>
    public class ManagementVesselFilter
    {
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
        /// Gets or sets a value indicating whether this instance is vessels currently in management.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is vessels currently in management; otherwise, <c>false</c>.
        /// </value>
        public bool IsVesselsCurrentlyInManagement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is quick search.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is quick search; otherwise, <c>false</c>.
        /// </value>
        public bool IsQuickSearch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fetch only activated coys].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch only activated coys]; otherwise, <c>false</c>.
        /// </value>
        public bool FetchOnlyActivatedAccountingCompanies { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is fetch for all companies.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fetch for all companies; otherwise, <c>false</c>.
        /// </value>
        public bool IsFetchForAllCompanies { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is fetch for all local companies.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fetch for all local companies; otherwise, <c>false</c>.
        /// </value>
        public bool IsFetchForAllLocalCompanies { get; set; }

        /// <summary>Gets or sets a value indicating whether [fetch only activated coys].</summary>
        /// <value>
        ///   <c>true</c> if [fetch only activated coys]; otherwise, <c>false</c>.</value>
        public bool? FetchOnlyActivatedCoys { get; set; }

        /// <summary>
        /// Gets or sets the avoid coy identifier check.
        /// </summary>
        /// <value>
        /// The avoid coy identifier check.
        /// </value>
        public bool? AvoidCoyIdCheck { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is vessel in purchasing management.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vessel in purchasing management; otherwise, <c>false</c>.
        /// </value>
        public bool IsVesselInPurchasingManagement { get; set; }

        /// <summary>
        /// Gets or sets the type of the fleet menu.
        /// </summary>
        /// <value>
        /// The type of the fleet menu.
        /// </value>
        public string FleetMenuType { get; set; }
    }
}
