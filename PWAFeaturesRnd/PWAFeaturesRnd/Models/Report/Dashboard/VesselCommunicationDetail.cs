using System;

namespace PWAFeaturesRnd.Models.Report.Dashboard
{
    /// <summary>
    /// Vessel communication detail
    /// </summary>
    public class VesselCommunicationDetail
    {
        /// <summary>
        /// The com deleted
        /// </summary>
        public bool? ComDeleted { get; set; }

        /// <summary>
        /// The com desc
        /// </summary>
        public string ComDesc { get; set; }

        /// <summary>
        /// The com expiry date
        /// </summary>
        public DateTime? ComExpiryDate { get; set; }

        /// <summary>
        /// The com id
        /// </summary>
        public string ComId { get; set; }

        /// <summary>
        /// The com number
        /// </summary>
        public string ComNumber { get; set; }

        /// <summary>
        /// The com primary contact
        /// </summary>
        public bool? ComPrimaryContact { get; set; }

        /// <summary>
        /// The com provider
        /// </summary>
        public string ComProvider { get; set; }

        /// <summary>
        /// The com start date
        /// </summary>
        public DateTime? ComStartDate { get; set; }

        /// <summary>
        /// The com update by
        /// </summary>
        public string ComUpdatedBy { get; set; }

        /// <summary>
        /// The com updated on
        /// </summary>
        public DateTime? ComUpdatedOn { get; set; }

        /// <summary>
        /// The cty id
        /// </summary>
        public string CtyId { get; set; }

        /// <summary>
        /// The vessel id
        /// </summary>
        public string VesId { get; set; }

        /// <summary>
        /// The cmp name
        /// </summary>
        public string CmpName { get; set; }

        /// <summary>
        /// The cty name
        /// </summary>
        public string CtyName { get; set; }

    }
}
