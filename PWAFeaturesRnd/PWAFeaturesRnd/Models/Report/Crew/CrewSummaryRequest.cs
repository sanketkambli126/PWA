using System;

namespace PWAFeaturesRnd.Models.Report.Crew
{
	/// <summary>
	/// Crew Summary Request
	/// </summary>
	public class CrewSummaryRequest
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets the past number of days.
		/// </summary>
		/// <value>
		/// The past number of days.
		/// </value>
		public int PastNumberOfDays { get; set; }

		/// <summary>
		/// Gets or sets the overdue to date.
		/// </summary>
		/// <value>
		/// The overdue to date.
		/// </value>
		public DateTime? OverdueToDate { get; set; }

		/// <summary>
		/// Gets or sets the unplanned to date.
		/// </summary>
		/// <value>
		/// The unplanned to date.
		/// </value>
		public DateTime? UnplannedToDate { get; set; }

		/// <summary>
		/// Gets or sets from medical sign off.
		/// </summary>
		/// <value>
		/// From medical sign off.
		/// </value>
		public DateTime? FromMedicalSignOff { get; set; }

		/// <summary>
		/// Converts to medicalsignoff.
		/// </summary>
		/// <value>
		/// To medical sign off.
		/// </value>
		public DateTime? ToMedicalSignOff { get; set; }

		/// <summary>
		/// Gets or sets the crew change from date.
		/// </summary>
		/// <value>
		/// The crew change from date.
		/// </value>
		public DateTime? CrewChangeFromDate { get; set; }

		/// <summary>
		/// Gets or sets the overdue priority limit.
		/// </summary>
		/// <value>
		/// The overdue priority limit.
		/// </value>
		public int OverduePriorityLimit { get; set; }

		/// <summary>
		/// Gets or sets the unplanned birth priority limit.
		/// </summary>
		/// <value>
		/// The unplanned birth priority limit.
		/// </value>
		public int UnplannedBirthPriorityLimit { get; set; }

		/// <summary>
		/// Gets or sets the medical sign off priority limit.
		/// </summary>
		/// <value>
		/// The medical sign off priority limit.
		/// </value>
		public int MedicalSignOffPriorityLimit { get; set; }
	}
}
