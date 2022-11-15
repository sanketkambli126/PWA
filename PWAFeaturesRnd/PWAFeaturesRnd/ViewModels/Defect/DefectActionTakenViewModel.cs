using System;

namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// Defect Action Taken ViewModel
    /// </summary>
    public class DefectActionTakenViewModel
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or sets the reported by.
        /// </summary>
        /// <value>
        /// The reported by.
        /// </value>
        public string ReportedBy { get; set; }

        /// <summary>
        /// Gets or sets the reported by identifier.
        /// </summary>
        /// <value>
        /// The reported by identifier.
        /// </value>
        public string ReportedById { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }
    }
}
