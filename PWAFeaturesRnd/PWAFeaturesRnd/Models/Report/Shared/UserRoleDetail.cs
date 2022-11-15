namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// User Role Detail
    /// </summary>
    public class UserRoleDetail
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { set; get; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public string RoleId { set; get; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { set; get; }
    }
}
