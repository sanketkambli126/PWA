using System;
using System.Collections.Generic;
using System.Text;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// 
    /// </summary>
    public class CrewExperienceRequest
    {
        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the type of the experience.
        /// </summary>
        /// <value>
        /// The type of the experience.
        /// </value>
        public ExperienceType ExperienceType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is only currrent experience required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is only currrent experience required; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnlyCurrrentExperienceRequired { get; set; }
    }
}
