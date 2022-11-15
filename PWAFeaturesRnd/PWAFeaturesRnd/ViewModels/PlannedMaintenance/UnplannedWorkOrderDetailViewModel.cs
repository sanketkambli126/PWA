using System;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// UnplannedWorkOrderDetailViewModel
    /// </summary>
    public class UnplannedWorkOrderDetailViewModel
    {
        /// <summary>
        /// Gets or sets the responsible department.
        /// </summary>
        /// <value>
        /// The responsible department.
        /// </value>
        public string ResponsibleDepartment { get; set; }

        /// <summary>
        /// Gets or sets the responsibility.
        /// </summary>
        /// <value>
        /// The responsibility.
        /// </value>
        public string Responsibility { get; set; }

        /// <summary>
        /// Gets or sets the approver.
        /// </summary>
        /// <value>
        /// The approver.
        /// </value>
        public string Approver { get; set; }

        /// <summary>
        /// Gets or sets the hod approval.
        /// </summary>
        /// <value>
        /// The hod approval.
        /// </value>
        public string HodApproval { get; set; }

        /// <summary>
        /// Gets or sets the shore contractor involved.
        /// </summary>
        /// <value>
        /// The shore contractor involved.
        /// </value>
        public string ShoreContractorInvolved { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the guide lines.
        /// </summary>
        /// <value>
        /// The guide lines.
        /// </value>
        public string GuideLines { get; set; }

        /// <summary>
        /// Gets or sets the work order history identifier.
        /// </summary>
        /// <value>
        /// The work order history identifier.
        /// </value>
        public string WorkOrderHistoryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show current RWD].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show current RWD]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowCurrentRWD { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the duedate.
		/// </summary>
		/// <value>
		/// The duedate.
		/// </value>
		public DateTime? DueDate { get; set; }
    }
}
