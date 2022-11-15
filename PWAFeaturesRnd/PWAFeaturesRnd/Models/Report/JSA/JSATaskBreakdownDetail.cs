using System;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// This is custom contract for JSATaskBreakdownDetail.
    /// </summary>
    public class JSATaskBreakdownDetail
    {
        /// <summary>
        /// Gets or sets the job task break down identifier.
        /// </summary>
        /// <value>
        /// The job task break down identifier.
        /// </value>
        public string JtdId { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the crew identifier tp.
        /// </summary>
        /// <value>
        /// The crew identifier tp.
        /// </value>
        public string CrewIdTp { get; set; }
        /// <summary>
        /// Gets or sets the name of the crew fore.
        /// </summary>
        /// <value>
        /// The name of the crew fore.
        /// </value>
        public string CrewForeName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the crew.
        /// </summary>
        /// <value>
        /// The last name of the crew.
        /// </value>
        public string CrewLastName { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>
        public string RankId { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets the step description.
        /// </summary>
        /// <value>
        /// The step description.
        /// </value>
        public string StepDescription { get; set; }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>
        /// The task description.
        /// </value>
        public string TaskDescription { get; set; }

        /// <summary>
        /// Gets or sets the estimate completion date time.
        /// </summary>
        /// <value>
        /// The estimate completion date time.
        /// </value>
        public DateTime? EstCompletionDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }
    }
}
