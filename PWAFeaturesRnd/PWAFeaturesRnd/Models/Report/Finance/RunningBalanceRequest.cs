using System;

namespace PWAFeaturesRnd.Models.Report.Finance
{
    /// <summary>
    /// The request class for Running Balance
    /// </summary>    
    public class RunningBalanceRequest
    {
        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the acc identifier.
        /// </summary>
        /// <value>
        /// The acc identifier.
        /// </value>
        public string AccId { get; set; }

        /// <summary>
        /// Gets or sets the financial start date.
        /// </summary>
        /// <value>
        /// The financial start date.
        /// </value>
        public DateTime FinancialStartDate { get; set; }

        /// <summary>
        /// Gets or sets the chart identifier.
        /// </summary>
        /// <value>
        /// The chart identifier.
        /// </value>
        public string ChartId { get; set; }

        /// <summary>
        /// Gets or sets the gl line date tran.
        /// </summary>
        /// <value>
        /// The gl line date tran.
        /// </value>
        public DateTime? GLLineDateTran { get; set; }

        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }
    }
}
