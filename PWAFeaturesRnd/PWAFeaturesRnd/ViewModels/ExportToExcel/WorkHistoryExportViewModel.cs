using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
    /// <summary>
    /// WorkHistoryExportViewModel
    /// </summary>
    public class WorkHistoryExportViewModel
    {
        /// <summary>
        /// Gets or sets the done date.
        /// </summary>
        /// <value>
        /// The done date.
        /// </value>
        [DisplayName("Done Date")]
        public string DoneDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        [DisplayName("Component Name")]
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        [DisplayName("Job Name")]
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the dept.
        /// </summary>
        /// <value>
        /// The dept.
        /// </value>
        [DisplayName("Dept")]
        public string Dept { get; set; }

        /// <summary>
        /// Gets or sets the resp.
        /// </summary>
        /// <value>
        /// The resp.
        /// </value>
        [DisplayName("Resp")]
        public string Resp { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DisplayName("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        [DisplayName("Interval")]
        public string Interval { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Is Critical")]
        public string IsCritical { get; set; }

    }
}
