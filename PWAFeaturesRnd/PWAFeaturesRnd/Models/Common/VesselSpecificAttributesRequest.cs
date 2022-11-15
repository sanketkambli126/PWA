using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Common
{
    /// <summary>
    /// VesselSpecificAttributesRequest
    /// </summary>
    public class VesselSpecificAttributesRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public List<VesselSpecificAttributeType> Permission { get; set; }

    }
}
