using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// JSA Attribute Lookup Custom Contract
    /// </summary>
    public class JSAAttributeLookup
    {
        /// <summary>
        /// Gets or sets the JSL identifier.
        /// </summary>
        /// <value>
        /// The JSL identifier.
        /// </value>
        public string JslId { get; set; }

        /// <summary>
        /// Gets or sets the JSL lookup code.
        /// </summary>
        /// <value>
        /// The JSL lookup code.
        /// </value>
        public string JslLookupCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the JSL.
        /// </summary>
        /// <value>
        /// The name of the JSL.
        /// </value>
        public string JslName { get; set; }

        /// <summary>
        /// Gets or sets the JSL description.
        /// </summary>
        /// <value>
        /// The JSL description.
        /// </value>
        public string JslDescription { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the parameter detail.
        /// </summary>
        /// <value>
        /// The parameter detail.
        /// </value>
        public string ParameterDetail { get; set; }
    }
}
