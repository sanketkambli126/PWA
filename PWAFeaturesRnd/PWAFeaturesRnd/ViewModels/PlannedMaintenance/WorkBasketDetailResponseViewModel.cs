using System;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Work Basket Detail Response ViewModel
    /// </summary>
    public class WorkBasketDetailResponseViewModel
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        public string Job { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public string Interval { get; set; }

        /// <summary>
        /// Gets or sets the resp.
        /// </summary>
        /// <value>
        /// The resp.
        /// </value>
        public string Resp { get; set; }

        /// <summary>
        /// Gets or sets the resp description.
        /// </summary>
        /// <value>
        /// The resp description.
        /// </value>
        public string RespDescription { get; set; }

        /// <summary>
        /// Gets or sets the left hours.
        /// </summary>
        /// <value>
        /// The left hours.
        /// </value>
        public int? LeftHours { get; set; }

        /// <summary>
        /// Gets or sets the planned maintenance details request URL.
        /// </summary>
        /// <value>
        /// The planned maintenance details request URL.
        /// </value>
        public string PlannedMaintenanceDetailsRequestURL { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
        /// </value>
        public bool IsCritical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is over due visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is over due visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverDueVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is overdue period visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is overdue period visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverduePeriodVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is due.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is due; otherwise, <c>false</c>.
        /// </value>
        public bool IsDue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has mapped jsa.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has mapped jsa; otherwise, <c>false</c>.
        /// </value>
        public bool HasMappedJSA { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has permit jsa.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has permit jsa; otherwise, <c>false</c>.
        /// </value>
        /// the property referes to IsJSAToBeMapped
        public bool HasPermitJSA { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has rounds job icon.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has rounds job icon; otherwise, <c>false</c>.
        /// </value>
        public bool HasRoundsJobIcon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allocated spare red geometry].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allocated spare red geometry]; otherwise, <c>false</c>.
        /// </value>
        public bool AllocatedSpareRedGeometry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allocated spare purple geometry].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allocated spare purple geometry]; otherwise, <c>false</c>.
        /// </value>
        public bool AllocatedSparePurpleGeometry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rob less than req.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rob less than req; otherwise, <c>false</c>.
        /// </value>
        public bool IsRobLessThanReq { get; set; }

        /// <summary>
        /// Gets or sets the required spare count.
        /// </summary>
        /// <value>
        /// The required spare count.
        /// </value>
        public int RequiredSpareCount { get; set; }

        /// <summary>
        /// Gets or sets the jsa tooltip.
        /// </summary>
        /// <value>
        /// The jsa tooltip.
        /// </value>
        public string JsaTooltip { get; set; }

        /// <summary>
        /// Gets or sets the defect details URL.
        /// </summary>
        /// <value>
        /// The defect details URL.
        /// </value>
        public string DefectDetailsUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is defect work order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is defect work order; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefectWorkOrder { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is jsa permit required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is jsa permit required; otherwise, <c>false</c>.
        /// </value>
        public bool IsJSAPermitRequired { get; set; }

        /// <summary>
        /// Gets or sets the is jsa permit required tooltip.
        /// </summary>
        /// <value>
        /// The is jsa permit required tooltip.
        /// </value>
        public string IsJSAPermitRequiredTooltip { get; set; }

		/// <summary>
		/// Gets or sets the work order identifier.
		/// </summary>
		/// <value>
		/// The work order identifier.
		/// </value>
		public string WorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the channel count.
        /// </summary>
        /// <value>
        /// The channel count.
        /// </value>
        public int ChannelCount { get; set; }

        /// <summary>
        /// Gets or sets the notes count.
        /// </summary>
        /// <value>
        /// The notes count.
        /// </value>
        public int NotesCount { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

		/// <summary>
		/// Gets or sets the dwo identifier.
		/// </summary>
		/// <value>
		/// The dwo identifier.
		/// </value>
		public string DwoId { get; set; }
	}
}
