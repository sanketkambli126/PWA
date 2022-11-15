using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Class holding fleet detail.
    /// </summary>
    public class FleetDetail
    {
        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>        
        public string FleetIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the name of the fleet.
        /// </summary>
        /// <value>
        /// The name of the fleet.
        /// </value>        
        public string FleetName { get; set; }

        /// <summary>
        /// Gets or sets the name of the office.
        /// </summary>
        /// <value>
        /// The name of the office.
        /// </value>        
        public string OfficeName { get; set; }

        /// <summary>
        /// Gets or sets the office identifier.
        /// </summary>
        /// <value>
        /// The office identifier.
        /// </value>        
        public string OfficeId { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>        
        public string DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>
        /// The name of the department.
        /// </value>        
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>        
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>        
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>        
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the type of the fleet.
        /// </summary>
        /// <value>
        /// The type of the fleet.
        /// </value>        
        public FleetType? FleetType { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        /// <value>
        /// The updated on.
        /// </value>        
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>        
        public string UpdatedBy { get; set; }
    }
}
