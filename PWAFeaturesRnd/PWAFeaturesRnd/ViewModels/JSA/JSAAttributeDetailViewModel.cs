using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    public class JSAAttributeDetailViewModel
    {
        /// <summary>
        /// Gets or sets the name of the attribute.
        /// </summary>
        /// <value>
        /// The name of the attribute.
        /// </value>
        public string AttributeName { get; set; }

        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>
        /// The other.
        /// </value>
        public string Other { get; set; }

        /// <summary>
        /// Gets or sets the permit number.
        /// </summary>
        /// <value>
        /// The permit number.
        /// </value>
        public string PermitNumber { get; set; }

        /// <summary>
        /// Gets or sets the permit request date time.
        /// </summary>
        /// <value>
        /// The permit request date time.
        /// </value>
        public DateTime? PermitRequestDateTime { get; set; }

        /// <summary>
        /// Gets or sets the validity from date time.
        /// </summary>
        /// <value>
        /// The validity from date time.
        /// </value>
        public DateTime? ValidityFromDateTime { get; set; }

        /// <summary>
        /// Gets or sets the validity to date time.
        /// </summary>
        /// <value>
        /// The validity to date time.
        /// </value>
        public DateTime? ValidityToDateTime { get; set; }

        /// <summary>
        /// Gets or sets the type of the attribute.
        /// </summary>
        /// <value>
        /// The type of the attribute.
        /// </value>
        public string AttributeType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the JSL identifier.
        /// </summary>
        /// <value>
        /// The JSL identifier.
        /// </value>
        public string JslId { get; set; }
    }
}
