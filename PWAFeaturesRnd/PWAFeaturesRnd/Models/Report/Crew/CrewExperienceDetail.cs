using System;
using System.Collections.Generic;
using System.Text;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Crew experience detail.
    /// </summary>
    public class CrewExperienceDetail
    {
        /// <summary>
        /// Gets or sets the experience in days.
        /// </summary>
        /// <value>
        /// The experience in days.
        /// </value>
        public int ExperienceInDays { get; set; }

        /// <summary>
        /// Gets or sets the Experience type.
        /// </summary>
        /// <value>
        /// The Experience type.
        /// </value>
        public string ExperienceType { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The vessel start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the experience code.
        /// </summary>
        /// <value>
        /// The experience code.
        /// </value>
        public string ExperienceCode { get; set; }

        /// <summary>
        /// Gets or sets the is current rank.
        /// </summary>
        /// <value>
        /// The is current rank.
        /// </value>
        public bool IsCurrentRank { get; set; }

        /// <summary>
        /// Gets or sets the experience code description.
        /// </summary>
        /// <value>
        /// The experience code description.
        /// </value>
        public string ExperienceCodeDescription { get; set; }
    }
}
