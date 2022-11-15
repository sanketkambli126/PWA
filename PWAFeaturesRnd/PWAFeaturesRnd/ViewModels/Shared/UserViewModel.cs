using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.Shared;

namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// User ViewModel
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user login log identifier.
        /// </summary>
        /// <value>
        /// The user login log identifier.
        /// </value>
        public string UserLoginLogId { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the encrypted client identifier.
        /// </summary>
        /// <value>
        /// The encrypted client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is client logo available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is client logo available; otherwise, <c>false</c>.
        /// </value>
        public bool IsClientLogoAvailable { get; set; }

        /// <summary>
        /// Gets or sets the user title.
        /// </summary>
        /// <value>
        /// The user title.
        /// </value>
        public string UserTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the user fore.
        /// </summary>
        /// <value>
        /// The name of the user fore.
        /// </value>
        public string UserForeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user sur.
        /// </summary>
        /// <value>
        /// The name of the user sur.
        /// </value>
        public string UserSurName { get; set; }
    }
}
