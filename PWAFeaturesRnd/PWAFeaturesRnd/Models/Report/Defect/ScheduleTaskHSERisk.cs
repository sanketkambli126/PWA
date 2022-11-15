using System;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class ScheduleTaskHSERisk
    {

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the likelihood.
        /// </summary>
        /// <value>
        /// The likelihood.
        /// </value>
        public string Likelihood { get; set; }

        /// <summary>
        /// Gets or sets the likelehood identifier.
        /// </summary>
        /// <value>
        /// The likelehood identifier.
        /// </value>
        public string LikelehoodId { get; set; }

        /// <summary>
        /// Gets or sets the impact.
        /// </summary>
        /// <value>
        /// The impact.
        /// </value>
        public string Impact { get; set; }

        /// <summary>
        /// Gets or sets the impact identifier.
        /// </summary>
        /// <value>
        /// The impact identifier.
        /// </value>
        public string ImpactId { get; set; }

        /// <summary>
        /// Gets or sets the vessel task risk hazard.
        /// </summary>
        /// <value>
        /// The vessel task risk hazard.
        /// </value>
        public string VesselTaskRiskHazard { get; set; }

        /// <summary>
        /// Gets or sets the deleted date.
        /// </summary>
        /// <value>
        /// The deleted date.
        /// </value>
        public DateTime? DeletedDate { get; set; }

        /// <summary>
        /// Gets or sets the risk date.
        /// </summary>
        /// <value>
        /// The risk date.
        /// </value>
        public DateTime? RiskDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        /// <value>
        /// The name of the task.
        /// </value>
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the PST identifier.
        /// </summary>
        /// <value>
        /// The PST identifier.
        /// </value>
        public string PstId { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets the RNK identifier.
        /// </summary>
        /// <value>
        /// The RNK identifier.
        /// </value>
        public string RnkId { get; set; }

        /// <summary>
        /// Gets or sets the HSV identifier.
        /// </summary>
        /// <value>
        /// The HSV identifier.
        /// </value>
        public string HsvId { get; set; }

        /// <summary>
        /// Gets or sets the vessel task risk number.
        /// </summary>
        /// <value>
        /// The vessel task risk number.
        /// </value>
        public int? VesselTaskRiskNumber { get; set; }

        /// <summary>
        /// Gets or sets the vessel task risk controls required.
        /// </summary>
        /// <value>
        /// The vessel task risk controls required.
        /// </value>
        public string VesselTaskRiskControlsRequired { get; set; }
    }
}
