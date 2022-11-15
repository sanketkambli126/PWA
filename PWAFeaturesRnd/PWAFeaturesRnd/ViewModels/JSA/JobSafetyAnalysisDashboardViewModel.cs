using System;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    /// <summary>
    /// JobSafetyAnalysisDashboardViewModel
    /// </summary>
    public class JobSafetyAnalysisDashboardViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the vessel description.
        /// </summary>
        /// <value>
        /// The vessel description.
        /// </value>
        public string VesselDescription { get; set; }

        /// <summary>
        /// Gets or sets the vessel age.
        /// </summary>
        /// <value>
        /// The vessel age.
        /// </value>
        public DateTime? VesselBuiltDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is next position available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is next position available; otherwise, <c>false</c>.
        /// </value>
        public bool IsNextPositionAvailable { get; set; }

        /// <summary>
        /// Gets or sets the open count.
        /// </summary>
        /// <value>
        /// The open count.
        /// </value>
        public int OpenCount { get; set; }

        /// <summary>
        /// Gets or sets the residual risk low count.
        /// </summary>
        /// <value>
        /// The residual risk low count.
        /// </value>
        public int ResidualRiskLowCount { get; set; }

        /// <summary>
        /// Gets or sets the residual risk medium and high count.
        /// </summary>
        /// <value>
        /// The residual risk medium and high count.
        /// </value>
        public int ResidualRiskMediumAndHighCount { get; set; }

        /// <summary>
        /// Gets or sets the pending office approval count.
        /// </summary>
        /// <value>
        /// The pending office approval count.
        /// </value>
        public int PendingOfficeApprovalCount { get; set; }

        /// <summary>
        /// Gets or sets the completed count.
        /// </summary>
        /// <value>
        /// The completed count.
        /// </value>
        public int CompletedCount { get; set; }

        /// <summary>
        /// Gets or sets the overdue for closure count.
        /// </summary>
        /// <value>
        /// The overdue for closure count.
        /// </value>
        public int OverdueForClosureCount { get; set; }

        /// <summary>
        /// Gets or sets the overview URL.
        /// </summary>
        /// <value>
        /// The overview URL.
        /// </value>
        public string OverviewURL { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the low URL.
        /// </summary>
        /// <value>
        /// The low URL.
        /// </value>
        public string LowUrl { get; set; }

        /// <summary>
        /// Gets or sets the mid high URL.
        /// </summary>
        /// <value>
        /// The mid high URL.
        /// </value>
        public string MidHighUrl { get; set; }

        /// <summary>
        /// Gets or sets the completed URL.
        /// </summary>
        /// <value>
        /// The completed URL.
        /// </value>
        public string CompletedUrl { get; set; }

        /// <summary>
        /// Gets or sets the pending office approval URL.
        /// </summary>
        /// <value>
        /// The pending office approval URL.
        /// </value>
        public string PendingOfficeApprovalUrl { get; set; }

        /// <summary>
        /// Gets or sets the overdue for closure.
        /// </summary>
        /// <value>
        /// The overdue for closure.
        /// </value>
        public string OverdueForClosureUrl { get; set; }

        /// <summary>
        /// Gets or sets the total URL.
        /// </summary>
        /// <value>
        /// The total URL.
        /// </value>
        public string TotalUrl { get; set; }

        /// <summary>
        /// Gets or sets the open priority.
        /// </summary>
        /// <value>
        /// The open priority.
        /// </value>
        public int OpenPriority { get; set; }

        /// <summary>
        /// Gets or sets the residual risk medium and high priority.
        /// </summary>
        /// <value>
        /// The residual risk medium and high priority.
        /// </value>
        public int ResidualRiskMediumAndHighPriority { get; set; }

        /// <summary>
        /// Gets or sets the overdue for closure priority.
        /// </summary>
        /// <value>
        /// The overdue for closure priority.
        /// </value>
        public int OverdueForClosurePriority { get; set; }

        /// <summary>
        /// Gets or sets the pending office approval priority.
        /// </summary>
        /// <value>
        /// The pending office approval priority.
        /// </value>
        public int PendingOfficeApprovalPriority { get; set; }

    }
}
