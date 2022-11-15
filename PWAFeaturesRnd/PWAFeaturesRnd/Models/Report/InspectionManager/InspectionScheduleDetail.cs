using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Contract class for inspection schedule detail.
    /// </summary>
    public class InspectionScheduleDetail
    {
        /// <summary>
        /// Gets or sets the VLK identifier.
        /// </summary>
        /// <value>
        /// The VLK identifier.
        /// </value>
        public string VlkId { get; set; }

        /// <summary>
        /// Gets or sets the vse identifier.
        /// </summary>
        /// <value>
        /// The vse identifier.
        /// </value>
        public string VseId { get; set; }

        /// <summary>
        /// Gets or sets the schedule detail.
        /// </summary>
        /// <value>
        /// The schedule detail.
        /// </value>
        public string ScheduleDetail { get; set; }

        /// <summary>
        /// Gets or sets the VLK description.
        /// </summary>
        /// <value>
        /// The VLK description.
        /// </value>
        public string VlkDescription { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the schedule date.
        /// </summary>
        /// <value>
        /// The schedule date.
        /// </value>
        public DateTime? ScheduleDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }
    }
}
