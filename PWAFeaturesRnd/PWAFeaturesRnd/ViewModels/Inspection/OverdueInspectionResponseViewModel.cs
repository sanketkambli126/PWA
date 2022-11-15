using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// OverdueInspectionResponseViewModel
    /// </summary>
    public class OverdueInspectionResponseViewModel
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the type of the inspection.
        /// </summary>
        /// <value>
        /// The type of the inspection.
        /// </value>
        public string InspectionType { get; set; }

        /// <summary>
        /// Gets or sets the last done date.
        /// </summary>
        /// <value>
        /// The last done date.
        /// </value>
        public DateTime? LastDoneDate { get; set; }

        /// <summary>
        /// Gets or sets the next due date.
        /// </summary>
        /// <value>
        /// The next due date.
        /// </value>
        public DateTime? NextDueDate { get; set; }

        /// <summary>
        /// Gets or sets the encrypted finding URL.
        /// </summary>
        /// <value>
        /// The encrypted finding URL.
        /// </value>
        public string EncryptedInspectionURL { get; set; }
    }
}
