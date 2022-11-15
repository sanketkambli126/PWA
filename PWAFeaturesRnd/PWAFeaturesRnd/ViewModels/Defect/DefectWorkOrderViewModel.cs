using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// View model for defect details
    /// </summary>
    public class DefectWorkOrderViewModel
    {
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
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets the defect description.
        /// </summary>
        /// <value>
        /// The defect description.
        /// </value>
        public string DefectDescription { get; set; }

        /// <summary>
        /// Gets or sets the repair specification.
        /// </summary>
        /// <value>
        /// The repair specification.
        /// </value>
        public string RepairSpecification { get; set; }

        /// <summary>
        /// Gets or sets the action list.
        /// </summary>
        /// <value>
        /// The action list.
        /// </value>
        public List<DefectActionTakenViewModel> ActionList { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the defect system area.
        /// </summary>
        /// <value>
        /// The defect system area.
        /// </value>
        public string DefectSystemArea { get; set; }

        /// <summary>
        /// Gets or sets the defect sub system area.
        /// </summary>
        /// <value>
        /// The defect sub system area.
        /// </value>
        public string DefectSubSystemArea { get; set; }

        /// <summary>
        /// Gets or sets the rank description.
        /// </summary>
        /// <value>
        /// The rank description.
        /// </value>
        public string RankDescription { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the type of the site.
        /// </summary>
        /// <value>
        /// The type of the site.
        /// </value>
        public string SiteType { get; set; }

        /// <summary>
        /// Gets or sets the proposed start date.
        /// </summary>
        /// <value>
        /// The proposed start date.
        /// </value>
        public string ProposedStartDate { get; set; }

        /// <summary>
        /// Gets or sets the estimated completion date.
        /// </summary>
        /// <value>
        /// The estimated completion date.
        /// </value>
        public string EstimatedCompletionDate { get; set; }

        /// <summary>
        /// Gets or sets the found date.
        /// </summary>
        /// <value>
        /// The found date.
        /// </value>
        public string FoundDate { get; set; }

        /// <summary>
        /// Gets or sets the include in damage form.
        /// </summary>
        /// <value>
        /// The include in damage form.
        /// </value>
        public bool IncludeInDamageForm { get; set; }

        /// <summary>
        /// Gets or sets the is critical.
        /// </summary>
        /// <value>
        /// The is critical.
        /// </value>
        public bool IsCritical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is js arequired.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is js arequired; otherwise, <c>false</c>.
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
        /// Gets or sets a value indicating whether this instance is off hire.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
        /// </value>
        public bool IsOffHire { get; set; }

        /// <summary>
        /// Gets or sets the estimated period.
        /// </summary>
        /// <value>
        /// The estimated period.
        /// </value>
        public string EstimatedPeriod { get; set; }

        /// <summary>
        /// Gets or sets the off hire period identifier.
        /// </summary>
        /// <value>
        /// The off hire period identifier.
        /// </value>
        public string OffHirePeriodId { get; set; }

        /// <summary>
        /// Gets or sets the impact.
        /// </summary>
        /// <value>
        /// The impact.
        /// </value>
        public string Impact { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is impact.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is impact; otherwise, <c>false</c>.
        /// </value>
        public bool IsImpact { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is regulatory authority.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is regulatory authority; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegulatoryAuthority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [dispensation in place].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dispensation in place]; otherwise, <c>false</c>.
        /// </value>
        public bool DispensationInPlace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is gas free.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is gas free; otherwise, <c>false</c>.
        /// </value>
        public bool IsGasFree { get; set; }

        /// <summary>
        /// Gets or sets the owner specific identifier.
        /// </summary>
        /// <value>
        /// The owner specific identifier.
        /// </value>
        public string OwnerSpecificId { get; set; }

        /// <summary>
        /// Gets or sets the specific attribute value.
        /// </summary>
        /// <value>
        /// The specific attribute value.
        /// </value>
        public string SpecificAttributeValue { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is completed or closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is completed or closed; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompletedOrClosed { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is guarantee claim code.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is guarantee claim code; otherwise, <c>false</c>.
		/// </value>
		public bool IsGuaranteeClaimCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is status completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is status completed; otherwise, <c>false</c>.
        /// </value>
        public bool IsStatusCompleted { get; set; }

        /// <summary>
        /// Gets or sets the defect status description.
        /// </summary>
        /// <value>
        /// The defect status description.
        /// </value>
        public string DefectStatusDescription { get; set; }
    }
}
