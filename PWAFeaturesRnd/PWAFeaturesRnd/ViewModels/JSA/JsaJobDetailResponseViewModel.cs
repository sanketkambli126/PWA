using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    /// <summary>
    /// JsaJobDetailResponseViewModel
    /// </summary>
    public class JsaJobDetailResponseViewModel
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the reference no.
        /// </summary>
        /// <value>
        /// The reference no.
        /// </value>
        public string JSADetails { get; set; }

        /// <summary>
        /// Gets or sets the reference no.
        /// </summary>
        /// <value>
        /// The reference no.
        /// </value>
        public string RefNo { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public string StartDateUI { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public string EndDateUI { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the job detail.
        /// </summary>
        /// <value>
        /// The job detail.
        /// </value>
        public string JobDetail { get; set; }

        /// <summary>
        /// Gets or sets the job detail.
        /// </summary>
        /// <value>
        /// The job detail.
        /// </value>
        public string jsaNo { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the maximum risk.
        /// </summary>
        /// <value>
        /// The maximum risk.
        /// </value>
        public string MaxRisk { get; set; }

        /// <summary>
        /// Gets or sets the system area.
        /// </summary>
        /// <value>
        /// The system area.
        /// </value>
        public string SystemArea { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public KPI StatusKPI { get; set; }

        /// <summary>
        /// Gets or sets the risk kpi.
        /// </summary>
        /// <value>
        /// The risk kpi.
        /// </value>
        public KPI RiskKPI { get; set; }

		/// <summary>
		/// Gets or sets the job identifier.
		/// </summary>
		/// <value>
		/// The job identifier.
		/// </value>
		public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the channel count.
        /// </summary>
        /// <value>
        /// The channel count.
        /// </value>
        public int ChannelCount { get; set; }

        /// <summary>
        /// Gets or sets the notes count.
        /// </summary>
        /// <value>
        /// The notes count.
        /// </value>
        public int NotesCount { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public bool SimultaneousJobVisible { get; set; }

        /// <summary>
        /// Gets or sets the responsibility.
        /// </summary>
        /// <value>
        /// The responsibility.
        /// </value>
        public string Responsibility { get; set; }

        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }
    }
}
