namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Model to hold Port details
    /// </summary>
    public class PortDetail
    {
        /// <summary>
        /// Gets or sets the port identifier.
        /// </summary>
        /// <value>
        /// The port identifier.
        /// </value>

        public string PortIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>

        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>

        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>

        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the un locode.
        /// </summary>
        /// <value>
        /// The un locode.
        /// </value>

        public string UNLocode { get; set; }

        /// <summary>
        /// Gets or sets the world region.
        /// </summary>
        /// <value>
        /// The world region.
        /// </value>

        public string WorldRegion { get; set; }

        /// <summary>
        /// Gets or sets the continent.
        /// </summary>
        /// <value>
        /// The continent.
        /// </value>

        public string Continent { get; set; }

        /// <summary>
        /// Gets or sets the port alert added.
        /// </summary>
        /// <value>
        /// The port alert added.
        /// </value>

        public bool PortAlertAdded { get; set; }

        /// <summary>
        /// Gets or sets the port service mapped.
        /// </summary>
        /// <value>
        /// The port service mapped.
        /// </value>

        public bool PortServiceMapped { get; set; }

        /// <summary>
        /// Gets or sets the port agent mapped.
        /// </summary>
        /// <value>
        /// The port agent mapped.
        /// </value>

        public bool PortAgentMapped { get; set; }

        /// <summary>
        /// Gets or sets the company port mapped.
        /// </summary>
        /// <value>
        /// The company port mapped.
        /// </value>

        public bool CompanyPortMapped { get; set; }

        /// <summary>
        /// Gets or sets the CMT identifier.
        /// </summary>
        /// <value>
        /// The CMT identifier.
        /// </value>

        public string CmtId { get; set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get
            {
                if (!string.IsNullOrEmpty(PortName))
                {
                    return PortName;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id
        {
            get
            {
                return PortIdentifier;
            }
        }
    }
}
