using System;

namespace PWAFeaturesRnd.Models.Common
{
    /// <summary>
    /// This contract for control permission detail
    /// </summary>
    public class ControlPermission
    {
        /// <summary>
        /// Gets or sets the control identifier.
        /// </summary>
        /// <value>
        /// The control identifier.
        /// </value>
        public Guid ControlId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ControlPermission"/> is permission.
        /// </summary>
        /// <value>
        ///   <c>true</c> if permission; otherwise, <c>false</c>.
        /// </value>
        public bool Permission { get; set; }
    }
}
