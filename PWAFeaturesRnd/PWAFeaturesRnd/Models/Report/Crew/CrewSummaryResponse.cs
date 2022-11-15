namespace PWAFeaturesRnd.Models.Report.Crew
{
	/// <summary>
	/// Crew Summary Response
	/// </summary>
	public class CrewSummaryResponse
    {
		/// <summary>
		/// Gets or sets the overdue count.
		/// </summary>
		/// <value>
		/// The overdue count.
		/// </value>
		public int? OverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue priority.
		/// </summary>
		/// <value>
		/// The overdue priority.
		/// </value>
		public int OverduePriority { get; set; }

		/// <summary>
		/// Gets or sets the onboard count.
		/// </summary>
		/// <value>
		/// The onboard count.
		/// </value>
		public int? OnboardCount { get; set; }

		/// <summary>
		/// Gets or sets the onboard priority.
		/// </summary>
		/// <value>
		/// The onboard priority.
		/// </value>
		public int OnboardPriority { get; set; }

		/// <summary>
		/// Gets or sets the unplanned berth count.
		/// </summary>
		/// <value>
		/// The unplanned berth count.
		/// </value>
		public int? UnplannedBerthCount { get; set; }

		/// <summary>
		/// Gets or sets the unplanned berth priority.
		/// </summary>
		/// <value>
		/// The unplanned berth priority.
		/// </value>
		public int UnplannedBerthPriority { get; set; }

		/// <summary>
		/// Gets or sets the sign on count.
		/// </summary>
		/// <value>
		/// The sign on count.
		/// </value>
		public int? SignOnCount { get; set; }

		/// <summary>
		/// Gets or sets the sign on priority.
		/// </summary>
		/// <value>
		/// The sign on priority.
		/// </value>
		public int SignOnPriority { get; set; }

		/// <summary>
		/// Gets or sets the officer prom new hire count.
		/// </summary>
		/// <value>
		/// The officer prom new hire count.
		/// </value>
		public int? OfficerPromNewHireCount { get; set; }

		/// <summary>
		/// Gets or sets the officer prom new hire priority.
		/// </summary>
		/// <value>
		/// The officer prom new hire priority.
		/// </value>
		public int OfficerPromNewHirePriority { get; set; }

		/// <summary>
		/// Gets or sets the medical sign off count.
		/// </summary>
		/// <value>
		/// The medical sign off count.
		/// </value>
		public int? MedicalSignOffCount { get; set; }

		/// <summary>
		/// Gets or sets the medical sign off priority.
		/// </summary>
		/// <value>
		/// The medical sign off priority.
		/// </value>
		public int MedicalSignOffPriority { get; set; }

		/// <summary>
		/// Gets or sets the crew change (sign on) in x no of days count.
		/// </summary>
		/// <value>
		/// The crew change count.
		/// </value>
		public int? CrewChangeCount { get; set; }

		/// <summary>
		/// Gets or sets the crew change priority.
		/// </summary>
		/// <value>
		/// The crew change priority.
		/// </value>
		public int CrewChangePriority { get; set; }
	}
}
