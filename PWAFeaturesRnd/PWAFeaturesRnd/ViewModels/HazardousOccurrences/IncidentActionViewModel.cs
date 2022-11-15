using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// 
    /// </summary>
    public class IncidentActionViewModel
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the status desc.
        /// </summary>
        /// <value>
        /// The status desc.
        /// </value>
        public string StatusDesc { get; set; }

        /// <summary>
        /// Gets or sets the color of the status.
        /// </summary>
        /// <value>
        /// The color of the status.
        /// </value>
        public KPI StatusColor { get; set; }

        /// <summary>
        /// Gets or sets the action date.
        /// </summary>
        /// <value>
        /// The action date.
        /// </value>
        public System.DateTime ActionDate { get; set; }
       
        /// <summary>
        /// Gets or sets the action taken.
        /// </summary>
        /// <value>
        /// The action taken.
        /// </value>
        public System.String ActionTaken { get; set; }

        /// <summary>
        /// Gets or sets the action to be taken.
        /// </summary>
        /// <value>
        /// The action to be taken.
        /// </value>
        public System.String ActionToBeTaken { get; set; }

        /// <summary>
        /// Gets or sets the closure date.
        /// </summary>
        /// <value>
        /// The closure date.
        /// </value>
        public Nullable<System.DateTime> ClosureDate { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public Nullable<System.DateTime> CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the deadline.
        /// </summary>
        /// <value>
        /// The deadline.
        /// </value>
        public Nullable<System.DateTime> Deadline { get; set; }
    }
}
