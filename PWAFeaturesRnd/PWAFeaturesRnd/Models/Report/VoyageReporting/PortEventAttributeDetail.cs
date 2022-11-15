using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// The port event attribute detail
    /// </summary>
    public class PortEventAttributeDetail
    {
        /// <summary>
        /// Gets or sets the rob type identifier.
        /// </summary>
        /// <value>
        /// The rob type identifier.
        /// </value>
        public string RobTypeId { get; set; }
        /// <summary>
        /// Gets or sets the ppe identifier.
        /// </summary>
        /// <value>
        /// The ppe identifier.
        /// </value>
        public string PpeId { get; set; }
        /// <summary>
        /// Gets or sets the attribute identifier.
        /// </summary>
        /// <value>
        /// The attribute identifier.
        /// </value>
        public string AttributeId { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal? Value { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is view only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is view only; otherwise, <c>false</c>.
        /// </value>
        public bool IsViewOnly { get; set; }
        /// <summary>
        /// Gets or sets the index of the sort.
        /// </summary>
        /// <value>
        /// The index of the sort.
        /// </value>
        public int SortIndex { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets the event date.
        /// </summary>
        /// <value>
        /// The event date.
        /// </value>
        public DateTime? EventDate { get; set; }
        /// <summary>
        /// Gets or sets the PPF identifier.
        /// </summary>
        /// <value>
        /// The PPF identifier.
        /// </value>
        public string PpfId { get; set; }
    }
}
