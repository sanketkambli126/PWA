using PWAFeaturesRnd.Common.Paging;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Model for requesting Port Search
    /// </summary>
    public class SearchPortRequest
    {
        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>

        public string CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>

        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the un locode.
        /// </summary>
        /// <value>
        /// The un locode.
        /// </value>

        public string UNLocode { get; set; }

        /// <summary>
        /// Gets or sets the paged request.
        /// </summary>
        /// <value>
        /// The paged request.
        /// </value>
        public PagedRequest pagedRequest { get; set; }
    }
}
