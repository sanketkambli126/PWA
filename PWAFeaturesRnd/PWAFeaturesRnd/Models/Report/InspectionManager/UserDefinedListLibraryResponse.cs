using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDefinedListLibraryResponse
    {
        /// <summary>
        /// Gets or sets the qal identifier.
        /// </summary>
        /// <value>
        /// The qal identifier.
        /// </value>
        public string QalId { get; set; }

        /// <summary>
        /// Gets or sets the qal identifier parent.
        /// </summary>
        /// <value>
        /// The qal identifier parent.
        /// </value>
        public string QalIdParent { get; set; }

        /// <summary>
        /// Gets or sets the name of the list.
        /// </summary>
        /// <value>
        /// The name of the list.
        /// </value>
        public string ListName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int? Sequence { get; set; }

        /// <summary>
        /// Gets or sets the is finding required.
        /// </summary>
        /// <value>
        /// The is finding required.
        /// </value>
        public bool? IsFindingRequired { get; set; }

        /// <summary>
        /// Gets or sets the color identifier.
        /// </summary>
        /// <value>
        /// The color identifier.
        /// </value>
        public string ColorId { get; set; }

        /// <summary>
        /// Gets or sets the color description.
        /// </summary>
        /// <value>
        /// The color description.
        /// </value>
        public string ColorDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is used in question.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is used in question; otherwise, <c>false</c>.
        /// </value>
        public bool IsUsedInQuestion { get; set; }

        /// <summary>
        /// Gets or sets the list values.
        /// </summary>
        /// <value>
        /// The list values.
        /// </value>
        public List<UserDefinedListLibraryResponse> ListValues { get; set; }

        /// <summary>
        /// Gets or sets the total questions mapped.
        /// </summary>
        /// <value>
        /// The total questions mapped.
        /// </value>
        public int? TotalQuestionsMapped { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the is capture risk rating.
        /// </summary>
        /// <value>
        /// The is capture risk rating.
        /// </value>
        public bool? IsCaptureRiskRating { get; set; }

        /// <summary>
        /// Gets or sets the risk rating.
        /// </summary>
        /// <value>
        /// The risk rating.
        /// </value>
        public decimal? RiskRating { get; set; }
    }
}
