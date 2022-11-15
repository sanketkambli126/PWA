﻿namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// The inspection type detail request
    /// </summary>
    public class InspectionTypeDetailRequest
    {
        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string Ves_Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is manual creation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is manual creation; otherwise, <c>false</c>.
        /// </value>
        public bool IsManualCreation { get; set; }
    }
}
