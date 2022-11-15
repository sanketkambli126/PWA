using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// Authoriser Detail
    /// </summary>
    public class AuthoriserDetail
    {
        /// <summary>
        /// Gets or sets the order authorisation identifier.
        /// </summary>
        /// <value>
        /// The order authorisation identifier.
        /// </value>
        public string OrderAuthorisationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the authorisation date.
        /// </summary>
        /// <value>
        /// The authorisation date.
        /// </value>
        public DateTime? AuthorisationDate { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the user login identifier.
        /// </summary>
        /// <value>
        /// The user login identifier.
        /// </value>
        public string UserLoginId { get; set; }
    }
}
