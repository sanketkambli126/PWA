using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Linked Insurance Claim Request
    /// </summary>
    public class LinkedInsuranceClaimRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the haz occ identifier.
        /// </summary>
        /// <value>
        /// The haz occ identifier.
        /// </value>
        public string HazOccId { get; set; }

        /// <summary>
        /// Gets or sets the claim number.
        /// </summary>
        /// <value>
        /// The claim number.
        /// </value>
        public string ClaimNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the claim.
        /// </summary>
        /// <value>
        /// The type of the claim.
        /// </value>
        public string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the open date.
        /// </summary>
        /// <value>
        /// The open date.
        /// </value>
        public DateTime? OpenDate { get; set; }

        /// <summary>
        /// Gets or sets the closed date.
        /// </summary>
        /// <value>
        /// The closed date.
        /// </value>
        public DateTime? ClosedDate { get; set; }
    }
}
