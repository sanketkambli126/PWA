using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
    /// <summary>
    /// ViewModel used for exporting excel in PMS List
    /// </summary>
    public class WorkBasketDetailExportViewModel
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DisplayName("Job Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [DisplayName("Due Date")]
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        [DisplayName("Job Name")]
        public string Job { get; set; }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        [DisplayName("Component Name")]
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [DisplayName("Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        [DisplayName("Interval")]
        public string Interval { get; set; }

        /// <summary>
        /// Gets or sets the resp.
        /// </summary>
        /// <value>
        /// The resp.
        /// </value>
        [DisplayName("Resp")]
        public string Resp { get; set; }


        /// <summary>
        /// Gets or sets the left hours.
        /// </summary>
        /// <value>
        /// The left hours.
        /// </value>
        [DisplayName("Left Hours")]
        public int? LeftHours { get; set; }

        /// <summary>
        /// Gets or sets the required spare count.
        /// </summary>
        /// <value>
        /// The required spare count.
        /// </value>
        [DisplayName("Required Spare")]
        public int RequiredSpareCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Is Critical")]
        public string IsCritical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has mapped jsa.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has mapped jsa; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Has JSA Mapped")]
        public string HasMappedJSA { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has permit jsa.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has permit jsa; otherwise, <c>false</c>.
        /// </value>
        /// the property referes to IsJSAToBeMapped
        [DisplayName("Is JSA to be Mapped")]
        public string IsJSAToBeMapped { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has rounds job icon.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has rounds job icon; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Has RoundJob")]
        public string HasRoundsJobIcon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rob less than req.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rob less than req; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Is Rob less than req")]
        public string IsRobLessThanReq { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is jsa permit required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is jsa permit required; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Is JSA Permit required")]
        public string IsJSAPermitRequired { get; set; }
    }
}
