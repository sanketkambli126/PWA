using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// DataContract for hazocc direct cause.
    /// </summary>
    public class HazOccDirectCause
    {
        /// <summary>
        /// Gets or sets the direct causes.
        /// </summary>
        /// <value>
        /// The direct causes.
        /// </value>
        public List<HazOccQuestions> DirectCauses { get; set; }

        /// <summary>
        /// Gets or sets the sub standard act comment.
        /// </summary>
        /// <value>
        /// The sub standard act comment.
        /// </value>
        public string SubStandardActComment { get; set; }

        /// <summary>
        /// Gets or sets the sub standard condition comment.
        /// </summary>
        /// <value>
        /// The sub standard condition comment.
        /// </value>
        public string SubStandardConditionComment { get; set; }
    }
}
