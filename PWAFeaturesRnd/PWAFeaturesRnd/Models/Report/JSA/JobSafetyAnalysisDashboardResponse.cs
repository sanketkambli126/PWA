using System;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// JobSafetyAnalysisDashboardResponse
    /// </summary>
    public class JobSafetyAnalysisDashboardResponse
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
