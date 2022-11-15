using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// OffHireResponseViewModel
    /// </summary>
    public class OffHireResponseViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PositionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the date to.
        /// </summary>
        /// <value>
        /// The date to.
        /// </value>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Gets or sets the date from.
        /// </summary>
        /// <value>
        /// The date from.
        /// </value>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the duration of the delay.
        /// </summary>
        /// <value>
        /// The duration of the delay.
        /// </value>
        public string DelayDuration { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the type of the off hire.
        /// </summary>
        /// <value>
        /// The type of the off hire.
        /// </value>
        public string OffHireType { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the seapassage URL.
        /// </summary>
        /// <value>
        /// The seapassage URL.
        /// </value>
        public string SeaPassageURL { get; set; }

        /// <summary>
        /// Gets or sets the port call URL.
        /// </summary>
        /// <value>
        /// The port call URL.
        /// </value>
        public string PortCallURL { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sea passage event.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sea passage event; otherwise, <c>false</c>.
        /// </value>
        public bool IsSeaPassageEvent { get; set; }
                
    }
}
