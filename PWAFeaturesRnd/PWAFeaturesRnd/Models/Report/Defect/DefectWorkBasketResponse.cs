using System;

namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// Defect Work Basket Response
	/// </summary>
	public class DefectWorkBasketResponse
	{
		/// <summary>
		/// Gets or sets the dwo identifier.
		/// </summary>
		/// <value>
		/// The dwo identifier.
		/// </value>
		public string DwoId { get; set; }

		/// <summary>
		/// Gets or sets the defect number.
		/// </summary>
		/// <value>
		/// The defect number.
		/// </value>
		public string DefectNumber { get; set; }

		/// <summary>
		/// Gets or sets the name of the defect.
		/// </summary>
		/// <value>
		/// The name of the defect.
		/// </value>
		public string DefectName { get; set; }

		/// <summary>
		/// Gets or sets the damage form number.
		/// </summary>
		/// <value>
		/// The damage form number.
		/// </value>
		public string DamageFormNumber { get; set; }

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>
		/// The category.
		/// </value>
		public string Category { get; set; }

		/// <summary>
		/// Gets or sets the site type identifier.
		/// </summary>
		/// <value>
		/// The site type identifier.
		/// </value>
		public string SiteTypeId { get; set; }

		/// <summary>
		/// Gets or sets the site type short code.
		/// </summary>
		/// <value>
		/// The site type short code.
		/// </value>
		public string SiteTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the site type description.
		/// </summary>
		/// <value>
		/// The site type description.
		/// </value>
		public string SiteTypeDescription { get; set; }

		/// <summary>
		/// Gets or sets the requisition count.
		/// </summary>
		/// <value>
		/// The requisition count.
		/// </value>
		public int? RequisitionCount { get; set; }

		/// <summary>
		/// Gets or sets the status identifier.
		/// </summary>
		/// <value>
		/// The status identifier.
		/// </value>
		public string StatusId { get; set; }

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
		/// Gets or sets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public string Priority { get; set; }

		/// <summary>
		/// Gets or sets the project code.
		/// </summary>
		/// <value>
		/// The project code.
		/// </value>
		public string ProjectCode { get; set; }

		/// <summary>
		/// Gets or sets the project description.
		/// </summary>
		/// <value>
		/// The project description.
		/// </value>
		public string ProjectDescription { get; set; }

		/// <summary>
		/// Gets or sets the account code.
		/// </summary>
		/// <value>
		/// The account code.
		/// </value>
		public string AccountCode { get; set; }

		/// <summary>
		/// Gets or sets the account code description.
		/// </summary>
		/// <value>
		/// The account code description.
		/// </value>
		public string AccountCodeDescription { get; set; }

		/// <summary>
		/// Gets or sets the guarantee claim code.
		/// </summary>
		/// <value>
		/// The guarantee claim code.
		/// </value>
		public string GuaranteeClaimCode { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Gets or sets the estimated complete date.
		/// </summary>
		/// <value>
		/// The estimated complete date.
		/// </value>
		public DateTime? EstimatedCompleteDate { get; set; }

		/// <summary>
		/// Gets or sets the actual complete date.
		/// </summary>
		/// <value>
		/// The actual complete date.
		/// </value>
		public DateTime? ActualCompleteDate { get; set; }

		/// <summary>
		/// Gets or sets the is critical.
		/// </summary>
		/// <value>
		/// The is critical.
		/// </value>
		public bool? IsCritical { get; set; }

		/// <summary>
		/// Gets or sets the yard guarantee claim number.
		/// </summary>
		/// <value>
		/// The yard guarantee claim number.
		/// </value>
		public string YardGuaranteeClaimNumber { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [include in damage form].
		/// </summary>
		/// <value>
		///   <c>true</c> if [include in damage form]; otherwise, <c>false</c>.
		/// </value>
		public bool? IncludeInDamageForm { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [guarantee claim required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [guarantee claim required]; otherwise, <c>false</c>.
		/// </value>
		public bool? GuaranteeClaimRequired { get; set; }

		/// <summary>
		/// Gets or sets the index of the defect number sort.
		/// </summary>
		/// <value>
		/// The index of the defect number sort.
		/// </value>
		public decimal? DefectNumberSortIndex { get; set; }

		/// <summary>
		/// Gets or sets the index of the damage number sort.
		/// </summary>
		/// <value>
		/// The index of the damage number sort.
		/// </value>
		public decimal? DamageNumberSortIndex { get; set; }

		/// <summary>
		/// Gets or sets the index of the guarantee number sort.
		/// </summary>
		/// <value>
		/// The index of the guarantee number sort.
		/// </value>
		public decimal? GuaranteeNumberSortIndex { get; set; }

		/// <summary>
		/// Gets or sets the reschedule count.
		/// </summary>
		/// <value>
		/// The reschedule count.
		/// </value>
		public int? RescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the original due date.
		/// </summary>
		/// <value>
		/// The original due date.
		/// </value>
		public DateTime? OriginalDueDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is off hire.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
		/// </value>
		public bool IsOffHire { get; set; }

		/// <summary>
		/// Gets or sets the off hire period.
		/// </summary>
		/// <value>
		/// The off hire period.
		/// </value>
		public string OffHirePeriod { get; set; }

		/// <summary>
		/// Gets or sets the off hire time.
		/// </summary>
		/// <value>
		/// The off hire time.
		/// </value>
		public int? OffHireTime { get; set; }

		/// <summary>
		/// Gets or sets the system area.
		/// </summary>
		/// <value>
		/// The system area.
		/// </value>
		public string SystemArea { get; set; }

		/// <summary>
		/// Gets or sets the sub system area.
		/// </summary>
		/// <value>
		/// The sub system area.
		/// </value>
		public string SubSystemArea { get; set; }

		/// <summary>
		/// Gets or sets the impact identifier.
		/// </summary>
		/// <value>
		/// The impact identifier.
		/// </value>
		public string ImpactId { get; set; }

		/// <summary>
		/// Gets or sets the impact.
		/// </summary>
		/// <value>
		/// The impact.
		/// </value>
		public string Impact { get; set; }

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
		/// Gets or sets the owner specific.
		/// </summary>
		/// <value>
		/// The owner specific.
		/// </value>
		public string OwnerSpecific { get; set; }

		/// <summary>
		/// Gets or sets the owner specific value.
		/// </summary>
		/// <value>
		/// The owner specific value.
		/// </value>
		public string OwnerSpecificValue { get; set; }

		/// <summary>
		/// Gets or sets the PGR identifier.
		/// </summary>
		/// <value>
		/// The PGR identifier.
		/// </value>
		public string PgrId { get; set; }

		/// <summary>
		/// Gets or sets the PTR identifier.
		/// </summary>
		/// <value>
		/// The PTR identifier.
		/// </value>
		public string PtrId { get; set; }

		/// <summary>
		/// Gets or sets the document count.
		/// </summary>
		/// <value>
		/// The document count.
		/// </value>
		public int? DocumentCount { get; set; }

		/// <summary>
		/// Gets or sets the mapped addition job count.
		/// </summary>
		/// <value>
		/// The mapped addition job count.
		/// </value>
		public int? MappedAdditionJobCount { get; set; }

		/// <summary>
		/// Gets or sets the requested due date.
		/// </summary>
		/// <value>
		/// The requested due date.
		/// </value>
		public DateTime? RequestedDueDate { get; set; }

		/// <summary>
		/// Gets or sets the rescheduled due date.
		/// </summary>
		/// <value>
		/// The rescheduled due date.
		/// </value>
		public DateTime? RescheduledDueDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is rob less than req.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is rob less than req; otherwise, <c>false</c>.
		/// </value>
		public bool IsRobLessThanReq { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has allocated spare.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has allocated spare; otherwise, <c>false</c>.
		/// </value>
		public bool HasAllocatedSpare { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is rob less than allocated qty.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is rob less than allocated qty; otherwise, <c>false</c>.
		/// </value>
		public bool IsROBLessThanAllocatedQty { get; set; }

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
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the off hire desc.
        /// </summary>
        /// <value>
        /// The off hire desc.
        /// </value>
        public string OffHireDesc { get; set; }
    }
}
