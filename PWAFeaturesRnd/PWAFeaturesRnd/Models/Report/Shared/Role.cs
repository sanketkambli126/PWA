namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Role
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary role.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is primary role; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimaryRole { get; set; }
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { get; set; }
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public string RoleId { get; set; }
    }
}
