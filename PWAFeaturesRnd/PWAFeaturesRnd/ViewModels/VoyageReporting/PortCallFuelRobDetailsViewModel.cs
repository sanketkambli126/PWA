using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    public class PortCallFuelRobDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the list of fuel.
        /// </summary>
        /// <value>
        /// The list of fuel.
        /// </value>
        public List<string> ListOfFuel { get; set; }

        /// <summary>
        /// Gets or sets the tank capacity.
        /// </summary>
        /// <value>
        /// The tank capacity.
        /// </value>
        public decimal? TankCapacity { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rob mismatch details added.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rob mismatch details added; otherwise, <c>false</c>.
        /// </value>
        public bool IsRobMismatchDetailsAdded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rob mismatch.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rob mismatch; otherwise, <c>false</c>.
        /// </value>
        public bool IsRobMismatch { get; set; }

        /// <summary>
        /// Gets or sets the rob details.
        /// </summary>
        /// <value>
        /// The rob details.
        /// </value>
        public List<PortEventAttributeDetailViewModel> RobDetails { get; set; }

        /// <summary>
        /// Gets or sets the rob details reason.
        /// </summary>
        /// <value>
        /// The rob details reason.
        /// </value>
        public string ROBDetailsReason { get; set; }
    }
}
