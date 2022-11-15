using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class OperatingCostBarChartRequest
    {
        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the report definition.
        /// </summary>
        /// <value>
        /// The type of the report definition.
        /// </value>
        public ReportDefinitionType ReportDefinitionType { get; set; }

        /// <summary>
        /// Gets or sets the parent3 acc and desc.
        /// </summary>
        /// <value>
        /// The parent3 acc and desc.
        /// </value>
        public string Parent3AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the parent2 acc and desc.
        /// </summary>
        /// <value>
        /// The parent2 acc and desc.
        /// </value>
        public string Parent2AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the account level.
        /// </summary>
        /// <value>
        /// The account level.
        /// </value>
        public int AccountLevel { get; set; }

        /// <summary>
        /// Gets or sets the parent1 acc and desc.
        /// </summary>
        /// <value>
        /// The parent1 acc and desc.
        /// </value>
        public string Parent1AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        /// <value>
        /// The name of the label.
        /// </value>
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or sets the variance first x month limit.
        /// </summary>
        /// <value>
        /// The variance first x month limit.
        /// </value>
        public int VariancePercentageFirstXMonthLimit { get; set; }

        /// <summary>
        /// Gets or sets the variancec second x month limit.
        /// </summary>
        /// <value>
        /// The variancec second x month limit.
        /// </value>
        public int VariancecPercentageSecondXMonthLimit { get; set; }

        /// <summary>
        /// Gets or sets the duration of the month.
        /// </summary>
        /// <value>
        /// The duration of the month.
        /// </value>
        public int MonthsDuration { get; set; }
    }
}
