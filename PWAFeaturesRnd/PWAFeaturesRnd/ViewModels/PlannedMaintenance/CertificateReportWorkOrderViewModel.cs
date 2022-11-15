namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Certificate Report Work Order ViewModel
    /// </summary>
    public class CertificateReportWorkOrderViewModel
    {
        /// <summary>
        /// Gets or sets the name of the work order.
        /// </summary>
        /// <value>
        /// The name of the work order.
        /// </value>
        public string WorkOrderName { get; set; }

        /// <summary>
        /// Gets or sets the type of the job.
        /// </summary>
        /// <value>
        /// The type of the job.
        /// </value>
        public string JobType { get; set; }

        /// <summary>
        /// Gets or sets the work order status short code.
        /// </summary>
        /// <value>
        /// The work order status short code.
        /// </value>
        public string WorkOrderStatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in range work order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in range work order; otherwise, <c>false</c>.
        /// </value>
        public bool IsInRangeWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is calendar range work order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is calendar range work order; otherwise, <c>false</c>.
        /// </value>
        public bool IsCalendarRangeWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is running HRS range work order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running HRS range work order; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunningHrsRangeWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public int Interval { get; set; }

        /// <summary>
        /// Gets or sets the type of the interval.
        /// </summary>
        /// <value>
        /// The type of the interval.
        /// </value>
        public string IntervalType { get; set; }

        /// <summary>
        /// Gets or sets from range interval value.
        /// </summary>
        /// <value>
        /// From range interval value.
        /// </value>
        public string FromRangeIntervalValue { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets from due date.
        /// </summary>
        /// <value>
        /// From due date.
        /// </value>
        public string FromDueDate { get; set; }

        /// <summary>
        /// Gets or sets the work done date.
        /// </summary>
        /// <value>
        /// The work done date.
        /// </value>
        public string WorkDoneDate { get; set; }

        /// <summary>
        /// Gets or sets the original due date.
        /// </summary>
        /// <value>
        /// The original due date.
        /// </value>
        public string OriginalDueDate { get; set; }

        /// <summary>
        /// Gets or sets the responsible rank.
        /// </summary>
        /// <value>
        /// The responsible rank.
        /// </value>
        public string ResponsibleRank { get; set; }

        /// <summary>
        /// Gets or sets the office job description.
        /// </summary>
        /// <value>
        /// The office job description.
        /// </value>
        public string OfficeJobDescription { get; set; }

        /// <summary>
        /// Gets or sets the PWH identifier.
        /// </summary>
        /// <value>
        /// The PWH identifier.
        /// </value>
        public string PwhId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is counter based.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is counter based; otherwise, <c>false</c>.
        /// </value>
        public bool IsCounterBased { get; set; }

        /// <summary>
        /// Gets or sets the job description part1.
        /// </summary>
        /// <value>
        /// The job description part1.
        /// </value>
        public string JobDescriptionPart1 { get; set; }

        /// <summary>
        /// Gets or sets the job description part2.
        /// </summary>
        /// <value>
        /// The job description part2.
        /// </value>
        public string JobDescriptionPart2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [job description check].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [job description check]; otherwise, <c>false</c>.
        /// </value>
        public bool JobDescriptionCheck { get; set; }
    }
}
