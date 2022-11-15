namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Work Order Header Detail ViewModel
    /// </summary>
    public class WorkOrderHeaderDetailViewModel
    {
        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job type description.
        /// </summary>
        /// <value>
        /// The job type description.
        /// </value>
        public string JobTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the work order status code.
        /// </summary>
        /// <value>
        /// The work order status code.
        /// </value>
        public string WorkOrderStatusCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in range work order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in range work order; otherwise, <c>false</c>.
        /// </value>
        public bool IsInRangeWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public string Interval { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is running HRS range work order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running HRS range work order; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunningHrsRangeWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is calendar range work order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is calendar range work order; otherwise, <c>false</c>.
        /// </value>
        public bool IsCalendarRangeWorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is CBM task.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is CBM task; otherwise, <c>false</c>.
        /// </value>
        public bool IsCbmTask { get; set; }

        /// <summary>
        /// Gets or sets the running hours range.
        /// </summary>
        /// <value>
        /// The running hours range.
        /// </value>
        public string RunningHoursRange { get; set; }

        /// <summary>
        /// Gets or sets the calendar range.
        /// </summary>
        /// <value>
        /// The calendar range.
        /// </value>
        public string CalendarRange { get; set; }

        /// <summary>
        /// Gets or sets the current due date range.
        /// </summary>
        /// <value>
        /// The current due date range.
        /// </value>
        public string CurrentDueDateRange { get; set; }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the maker.
        /// </summary>
        /// <value>
        /// The name of the maker.
        /// </value>
        public string MakerName { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the designer.
        /// </summary>
        /// <value>
        /// The designer.
        /// </value>
        public string Designer { get; set; }

        /// <summary>
        /// Gets or sets the alternate number.
        /// </summary>
        /// <value>
        /// The alternate number.
        /// </value>
        public string AlternateNumber { get; set; }

        /// <summary>
        /// Gets or sets the encrypted system area identifier.
        /// </summary>
        /// <value>
        /// The encrypted system area identifier.
        /// </value>
        public string EncryptedSystemAreaId { get; set; }

        /// <summary>
        /// Gets or sets the type of the alternate.
        /// </summary>
        /// <value>
        /// The type of the alternate.
        /// </value>
        public string AlternateType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is status completed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is status completed; otherwise, <c>false</c>.
		/// </value>
		public bool IsStatusCompleted { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can process reschedule wo.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can process reschedule wo; otherwise, <c>false</c>.
		/// </value>
		public bool CanProcessRescheduleWO { get; set; }

		/// <summary>
		/// Gets or sets the interval type identifier.
		/// </summary>
		/// <value>
		/// The interval type identifier.
		/// </value>
		public string IntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int? IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical { get; set; }

		/// <summary>
		/// Gets or sets the job interval type identifier.
		/// </summary>
		/// <value>
		/// The job interval type identifier.
		/// </value>
		public string JobIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the extended days note.
		/// </summary>
		/// <value>
		/// The extended days note.
		/// </value>
		public string ExtendedDaysNote { get; set; }

        /// <summary>
        /// Gets or sets the maximum counter extension value.
        /// </summary>
        /// <value>
        /// The maximum counter extension value.
        /// </value>
        public int MaximumCounterExtensionValue { get; set; }

        /// <summary>
        /// Gets or sets the months value.
        /// </summary>
        /// <value>
        /// The months value.
        /// </value>
        public int MaximumIntervalDays { get; set; }
    }
}
