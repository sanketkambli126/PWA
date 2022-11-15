using System;

namespace PWAFeaturesRnd.Models.Report.Approval
{
	/// <summary>
	/// Pms Pending Approval Response
	/// </summary>
	public class PmsPendingApprovalResponse
	{
		/// <summary>
		/// Gets or sets the work order identifier.
		/// </summary>
		/// <value>
		/// The work order identifier.
		/// </value>
		public string WorkOrderId { get; set; }

		/// <summary>
		/// Gets or sets the schedule task identifier.
		/// </summary>
		/// <value>
		/// The schedule task identifier.
		/// </value>
		public string ScheduleTaskId { get; set; }

		/// <summary>
		/// Gets or sets the work order indication type identifier.
		/// </summary>
		/// <value>
		/// The work order indication type identifier.
		/// </value>
		public string WorkOrderIndicationTypeId { get; set; }

		/// <summary>
		/// Gets or sets the dwo identifier.
		/// </summary>
		/// <value>
		/// The dwo identifier.
		/// </value>
		public string DwoId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the job identifier.
		/// </summary>
		/// <value>
		/// The job identifier.
		/// </value>
		public string JobId { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the status identifier.
		/// </summary>
		/// <value>
		/// The status identifier.
		/// </value>
		public string StatusId { get; set; }

		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		/// <value>
		/// The status code.
		/// </value>
		public string StatusCode { get; set; }

		/// <summary>
		/// Gets or sets the status description.
		/// </summary>
		/// <value>
		/// The status description.
		/// </value>
		public string StatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the job type identifier.
		/// </summary>
		/// <value>
		/// The job type identifier.
		/// </value>
		public string JobTypeId { get; set; }

		/// <summary>
		/// Gets or sets the job type short code.
		/// </summary>
		/// <value>
		/// The job type short code.
		/// </value>
		public string JobTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the job type description.
		/// </summary>
		/// <value>
		/// The job type description.
		/// </value>
		public string JobTypeDescription { get; set; }

		/// <summary>
		/// Gets or sets the responsible rank identifier.
		/// </summary>
		/// <value>
		/// The responsible rank identifier.
		/// </value>
		public string ResponsibleRankId { get; set; }

		/// <summary>
		/// Gets or sets the responsible rank short code.
		/// </summary>
		/// <value>
		/// The responsible rank short code.
		/// </value>
		public string ResponsibleRankShortCode { get; set; }

		/// <summary>
		/// Gets or sets the responsible rank description.
		/// </summary>
		/// <value>
		/// The responsible rank description.
		/// </value>
		public string ResponsibleRankDescription { get; set; }

		/// <summary>
		/// Gets or sets the frequency.
		/// </summary>
		/// <value>
		/// The frequency.
		/// </value>
		public int? Frequency { get; set; }

		/// <summary>
		/// Gets or sets the frequency type short code.
		/// </summary>
		/// <value>
		/// The frequency type short code.
		/// </value>
		public string FrequencyTypeShortCode { get; set; }
	}
}

