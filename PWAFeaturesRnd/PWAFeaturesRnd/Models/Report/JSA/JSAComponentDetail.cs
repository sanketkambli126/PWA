using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// This is custom contract for JSAComponentDetail.
    /// </summary>
    public class JSAComponentDetail
    {
        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public string JobId { get; set; }
        /// <summary>
        /// Gets or sets the component identifier.
        /// </summary>
        /// <value>
        /// The component identifier.
        /// </value>
        public string PtrId { get; set; }
        /// <summary>
        /// Gets or sets the job component detail identifier.
        /// </summary>
        /// <value>
        /// The job component detail identifier.
        /// </value>
        public string JcdId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [component active].
        /// </summary>
        /// <value>
        /// <c>true</c> if [component active]; otherwise, <c>false</c>.
        /// </value>
        public bool ComponentActive { get; set; }
        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        public string ComponentName { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string Position { get; set; }
        /// <summary>
        /// Gets or sets the maker.
        /// </summary>
        /// <value>
        /// The maker.
        /// </value>
        public string Maker { get; set; }
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public string Model { get; set; }
        /// <summary>
        /// Gets or sets the class code.
        /// </summary>
        /// <value>
        /// The class code.
        /// </value>
        public string ClassCode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets the is critical.
        /// </summary>
        /// <value>
        /// The is critical.
        /// </value>
        public bool? IsCritical { get; set; }
        /// <summary>
        /// Gets or sets the work order identifier.
        /// </summary>
        /// <value>
        /// The work order identifier.
        /// </value>
        public string WorkOrderId { get; set; }
        /// <summary>
        /// Gets or sets the work order history identifier.
        /// </summary>
        /// <value>
        /// The work order history identifier.
        /// </value>
        public string WorkOrderHistoryId { get; set; }
        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }
        /// <summary>
        /// Gets or sets the work order status identifier.
        /// </summary>
        /// <value>
        /// The work order status identifier.
        /// </value>
        public string WorkOrderStatusId { get; set; }
        /// <summary>
        /// Gets or sets the work order status short code.
        /// </summary>
        /// <value>
        /// The work order status short code.
        /// </value>
        public string WorkOrderStatusShortCode { get; set; }
        /// <summary>
        /// Gets or sets the work order status description.
        /// </summary>
        /// <value>
        /// The work order status description.
        /// </value>
        public string WorkOrderStatusDescription { get; set; }
        /// <summary>
        /// Gets or sets the report work done date.
        /// </summary>
        /// <value>
        /// The report work done date.
        /// </value>
        public DateTime? ReportWorkDoneDate { get; set; }
    }
}
