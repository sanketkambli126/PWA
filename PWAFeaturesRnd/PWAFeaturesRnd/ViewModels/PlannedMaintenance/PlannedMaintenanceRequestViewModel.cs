using System;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// used for passing details from list to detail
    /// </summary>
    public class PlannedMaintenanceRequestViewModel
    {
        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the component identifier.
        /// </summary>
        /// <value>
        /// The component identifier.
        /// </value>
        public string ComponentId { get; set; }

        /// <summary>
        /// Gets or sets the system area identifier.
        /// </summary>
        /// <value>
        /// The system area identifier.
        /// </value>
        public string SystemAreaId { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the stage.
        /// </summary>
        /// <value>
        /// The name of the stage.
        /// </value>
        public string StageName { get; set; }

        /// <summary>
        /// Gets or sets the work order identifier.
        /// </summary>
        /// <value>
        /// The work order identifier.
        /// </value>
        public string WorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the schedule task identifier.
        /// </summary>
        /// <value>
        /// The schedule task identifier.
        /// </value>
        public string ScheduleTaskId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reschedule history done selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is reschedule history done selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsNavigatedFromDone { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the work order history identifier.
        /// </summary>
        /// <value>
        /// The work order history identifier.
        /// </value>
        public string WorkOrderHistoryId { get; set; }

        /// <summary>
        /// Gets or sets the defect work order identifier.
        /// </summary>
        /// <value>
        /// The defect work order identifier.
        /// </value>
        public string DefectWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the work order indication identifier.
        /// </summary>
        /// <value>
        /// The work order indication identifier.
        /// </value>
        public string WorkOrderIndicationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is swo.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is swo; otherwise, <c>false</c>.
        /// </value>
        public bool IsSWO { get; set; }

    }
}
