using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Crew
{
    /// <summary>
    /// For Holding the CrewHeader Information
    /// </summary>
    public class CrewHeaderDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the crew status.
        /// </summary>
        /// <value>
        /// The crew status.
        /// </value>
        public string CrewStatus { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets the PCN.
        /// </summary>
        /// <value>
        /// The PCN.
        /// </value>
        public string PCN { get; set; }

        /// <summary>
        /// Gets or sets the airport.
        /// </summary>
        /// <value>
        /// The airport.
        /// </value>
        public string Airport { get; set; }

        /// <summary>
        /// Gets or sets the join date.
        /// </summary>
        /// <value>
        /// The join date.
        /// </value>
        public DateTime JoinDate { get; set; }

        /// <summary>
        /// Gets or sets the experiences.
        /// </summary>
        /// <value>
        /// The experiences.
        /// </value>
        public List<CrewExperienceInYearsWrapper> Experiences { get; set; }

        /// <summary>
        /// Gets or sets the rank details.
        /// </summary>
        /// <value>
        /// The rank details.
        /// </value>
        public CrewExperienceInYearsWrapper RankDetails { get; set; }

    }
}
