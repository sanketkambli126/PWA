using System;

namespace PWAFeaturesRnd.Models.Common
{
    /// <summary>
    /// Custom contract to hold the Vessel Specific Attribute
    /// </summary>
    public class VesselSpecificAttribute
    {
        /// <summary>
        /// Gets or sets the vse identifier.
        /// </summary>
        /// <value>
        /// The vse identifier.
        /// </value>
        public string VseId { get; set; }

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the VMD identifier.
        /// </summary>
        /// <value>
        /// The VMD identifier.
        /// </value>
        public string VmdId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the VLK identifier lookup code.
        /// </summary>
        /// <value>
        /// The VLK identifier lookup code.
        /// </value>
        public string VlkIdLookupCode { get; set; }
        /// <summary>
        /// Gets or sets the VLK identifier description.
        /// </summary>
        /// <value>
        /// The VLK identifier description.
        /// </value>
        public string VlkIdDescription { get; set; }

        /// <summary>
        /// Gets or sets the name of the attribute.
        /// </summary>
        /// <value>
        /// The name of the attribute.
        /// </value>
        public string AttributeName { get; set; }

        /// <summary>
        /// Gets or sets the attribute description.
        /// </summary>
        /// <value>
        /// The attribute description.
        /// </value>
        public string AttributeDescription { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        /// <value>
        /// The updated on.
        /// </value>
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the attribute detail.
        /// </summary>
        /// <value>
        /// The attribute detail.
        /// </value>
        public VesselSpecificAttributeDetail AttributeDetail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [value bit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [value bit]; otherwise, <c>false</c>.
        /// </value>
        public bool? ValueBit { get; set; }

        /// <summary>
        /// Gets or sets the value int.
        /// </summary>
        /// <value>
        /// The value int.
        /// </value>
        public int? ValueInt { get; set; }

        /// <summary>
        /// Gets or sets the value date.
        /// </summary>
        /// <value>
        /// The value date.
        /// </value>
        public DateTime? ValueDate { get; set; }
    }
}
