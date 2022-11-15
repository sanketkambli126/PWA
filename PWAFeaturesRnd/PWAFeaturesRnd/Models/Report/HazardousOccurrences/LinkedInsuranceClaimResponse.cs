using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Linked Insurance Claim Response
    /// </summary>
    public class LinkedInsuranceClaimResponse
    {
        /// <summary>
        /// Gets or sets the claim identifier.
        /// </summary>
        /// <value>
        /// The claim identifier.
        /// </value>
        public string ClaimId { get; set; }

        /// <summary>
        /// Gets or sets the claim number.
        /// </summary>
        /// <value>
        /// The claim number.
        /// </value>
        public string ClaimNumber { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

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

        /// <summary>
        /// Gets or sets the system area.
        /// </summary>
        /// <value>
        /// The system area.
        /// </value>
        public string SystemArea { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the vma identifier.
        /// </summary>
        /// <value>
        /// The vma identifier.
        /// </value>
        public string VmaId { get; set; }

        /// <summary>
        /// Gets or sets the claim date.
        /// </summary>
        /// <value>
        /// The claim date.
        /// </value>
        public DateTime? ClaimDate { get; set; }
    }
}
