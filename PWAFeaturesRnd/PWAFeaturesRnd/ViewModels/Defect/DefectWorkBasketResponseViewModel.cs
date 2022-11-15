using System;

namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// Defect Work Basket Response ViewModel
    /// </summary>
    public class DefectWorkBasketResponseViewModel
    {
        /// <summary>
        /// Gets or sets the document count.
        /// </summary>
        /// <value>
        /// The document count.
        /// </value>
        public int DocumentCount { get; set; }

        /// <summary>
        /// Gets or sets the defect work order identifier.
        /// </summary>
        /// <value>
        /// The defect work order identifier.
        /// </value>
        public string DefectWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the defect no.
        /// </summary>
        /// <value>
        /// The defect no.
        /// </value>
        public string DefectNo { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [guarantee claim code].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [guarantee claim code]; otherwise, <c>false</c>.
        /// </value>
        public bool GuaranteeClaimCode { get; set; }

        /// <summary>
        /// Gets or sets the off hire.
        /// </summary>
        /// <value>
        /// The off hire.
        /// </value>
        public string OffHire { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [tech defect].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [tech defect]; otherwise, <c>false</c>.
        /// </value>
        public bool TechDefect { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        /// <value>
        /// The status description.
        /// </value>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the system area.
        /// </summary>
        /// <value>
        /// The system area.
        /// </value>
        public string SystemArea { get; set; }

        /// <summary>
        /// Gets or sets the current due date.
        /// </summary>
        /// <value>
        /// The current due date.
        /// </value>
        public DateTime? EstimatedCompleteDate { get; set; }

        /// <summary>
        /// Gets or sets the due date before resc.
        /// </summary>
        /// <value>
        /// The due date before resc.
        /// </value>
        public DateTime? DueDateBeforeResc { get; set; }

        /// <summary>
        /// Gets or sets the adv od days.
        /// </summary>
        /// <value>
        /// The adv od days.
        /// </value>
        public int? AdvODDays { get; set; }

        /// <summary>
        /// Gets or sets the sub system area.
        /// </summary>
        /// <value>
        /// The sub system area.
        /// </value>
        public string SubSystemArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current due date visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is current due date visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrentDueDateVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is over due visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is over due visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverDueVisible { get; set; }

        /// <summary>
        /// Gets or sets the defect damage form number.
        /// </summary>
        /// <value>
        /// The defect damage form number.
        /// </value>
        public string DefectDamageFormNumber { get; set; }

        /// <summary>
        /// Gets or sets the guarantee claim number.
        /// </summary>
        /// <value>
        /// The guarantee claim number.
        /// </value>
        public string GuaranteeClaimNumber { get; set; }

        /// <summary>
        /// Gets or sets the actual complete date.
        /// </summary>
        /// <value>
        /// The actual complete date.
        /// </value>
        public DateTime? ActualCompleteDate { get; set; }

        /// <summary>
        /// Gets or sets the impact.
        /// </summary>
        /// <value>
        /// The impact.
        /// </value>
        public string Impact { get; set; }

        /// <summary>
        /// Gets or sets the site type description.
        /// </summary>
        /// <value>
        /// The site type description.
        /// </value>
        public string SiteTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the requisition count.
        /// </summary>
        /// <value>
        /// The requisition count.
        /// </value>
        public int RequisitionCount { get; set; }

        /// <summary>
        /// Gets or sets the additional jobs.
        /// </summary>
        /// <value>
        /// The additional jobs.
        /// </value>
        public int AdditionalJobs { get; set; }

        /// <summary>
        /// Gets or sets the project code.
        /// </summary>
        /// <value>
        /// The project code.
        /// </value>
        public string ProjectCode { get; set; }

        /// <summary>
        /// Gets or sets the account code.
        /// </summary>
        /// <value>
        /// The account code.
        /// </value>
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the yard guarantee claim number.
        /// </summary>
        /// <value>
        /// The yard guarantee claim number.
        /// </value>
        public string YardGuaranteeClaimNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reschedule count.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is reschedule count; otherwise, <c>false</c>.
        /// </value>
        public bool IsRescheduleCount { get; set; }

        /// <summary>
        /// Gets or sets the is critical.
        /// </summary>
        /// <value>
        /// The is critical.
        /// </value>
        public bool IsCritical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is jsa required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is jsa required; otherwise, <c>false</c>.
        /// </value>
        public bool IsJSARequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is moc required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moc required; otherwise, <c>false</c>.
        /// </value>
        public bool IsMOCRequired { get; set; }

        /// <summary>
        /// Gets or sets the defect detail URL.
        /// </summary>
        /// <value>
        /// The defect detail URL.
        /// </value>
        public string DefectDetailURL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rob less than req.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rob less than req; otherwise, <c>false</c>.
        /// </value>
        public bool IsRobLessThanReq { get; set; }

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
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }
    }
}
