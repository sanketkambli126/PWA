using System;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection Omv Screening Lite contract class
    /// </summary>
    public class InspectionOmvScreeningLite
    {
        /// <summary>
        /// Gets or sets the action date.
        /// </summary>
        /// <value>
        /// The action date.
        /// </value>
        public DateTime? ActionDate { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the countryt identifier.
        /// </summary>
        /// <value>
        /// The countryt identifier.
        /// </value>
        public string CountrytId { get; set; }

        /// <summary>
        /// Gets or sets the inspection identifier.
        /// </summary>
        /// <value>
        /// The inspection identifier.
        /// </value>
        public string InspectionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the omv screening identifier.
        /// </summary>
        /// <value>
        /// The omv screening identifier.
        /// </value>
        public string OmvScreeningId { get; set; }

        /// <summary>
        /// Gets or sets the screening date.
        /// </summary>
        /// <value>
        /// The screening date.
        /// </value>
        public DateTime ScreeningDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the screening.
        /// </summary>
        /// <value>
        /// The type of the screening.
        /// </value>
        public string ScreeningType { get; set; }

        /// <summary>
        /// Gets or sets the screening type identifier.
        /// </summary>
        /// <value>
        /// The screening type identifier.
        /// </value>
        public string ScreeningTypeId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public bool? StatusId { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }
    }
}
