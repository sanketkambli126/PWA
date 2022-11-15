namespace PWAFeaturesRnd.ViewModels.Crew
{
	/// <summary>
	/// Crew Summary Response ViewModel
	/// </summary>
	public class CrewSummaryResponseViewModel
    {
		#region Properties

		/// <summary>
		/// Gets or sets the overdue count.
		/// </summary>
		/// <value>
		/// The overdue count.
		/// </value>
		public int OverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the onboard count.
		/// </summary>
		/// <value>
		/// The onboard count.
		/// </value>
		public int OnboardCount { get; set; }

		/// <summary>
		/// Gets or sets the unplanned berth count.
		/// </summary>
		/// <value>
		/// The unplanned berth count.
		/// </value>
		public int UnplannedBerthCount { get; set; }

		/// <summary>
		/// Gets or sets the top4 sign on count.
		/// </summary>
		/// <value>
		/// The top4 sign on count.
		/// </value>
		public int Top4SignOnCount { get; set; }

		/// <summary>
		/// Gets or sets the officer prom new hire count.
		/// </summary>
		/// <value>
		/// The officer prom new hire count.
		/// </value>
		public int OfficerPromNewHireCount { get; set; }

		/// <summary>
		/// Gets or sets the medical sign off count.
		/// </summary>
		/// <value>
		/// The medical sign off count.
		/// </value>
		public int MedicalSignOffCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue priority.
		/// </summary>
		/// <value>
		/// The overdue priority.
		/// </value>
		public int OverduePriority { get; set; }

		/// <summary>
		/// Gets or sets the onboard priority.
		/// </summary>
		/// <value>
		/// The onboard priority.
		/// </value>
		public int OnboardPriority { get; set; }

		/// <summary>
		/// Gets or sets the unplanned berth priority.
		/// </summary>
		/// <value>
		/// The unplanned berth priority.
		/// </value>
		public int UnplannedBerthPriority { get; set; }

		/// <summary>
		/// Gets or sets the sign on priority.
		/// </summary>
		/// <value>
		/// The sign on priority.
		/// </value>
		public int SignOnPriority { get; set; }

		/// <summary>
		/// Gets or sets the officer prom new hire priority.
		/// </summary>
		/// <value>
		/// The officer prom new hire priority.
		/// </value>
		public int OfficerPromNewHirePriority { get; set; }

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

		#endregion

		#region Navigation Links

		/// <summary>
		/// Gets or sets the overdue URL.
		/// </summary>
		/// <value>
		/// The overdue URL.
		/// </value>
		public string OverdueURL { get; set; }

		/// <summary>
		/// Gets or sets the onboard URL.
		/// </summary>
		/// <value>
		/// The onboard URL.
		/// </value>
		public string OnboardURL { get; set; }

		/// <summary>
		/// Gets or sets the unplanned berth URL.
		/// </summary>
		/// <value>
		/// The unplanned berth URL.
		/// </value>
		public string UnplannedBerthURL { get; set; }

		/// <summary>
		/// Gets or sets the top4 sign on URL.
		/// </summary>
		/// <value>
		/// The top4 sign on URL.
		/// </value>
		public string Top4SignOnURL { get; set; }

		/// <summary>
		/// Gets or sets the officer prom new hire URL.
		/// </summary>
		/// <value>
		/// The officer prom new hire URL.
		/// </value>
		public string OfficerPromNewHireURL { get; set; }

		/// <summary>
		/// Gets or sets the medical sign off URL.
		/// </summary>
		/// <value>
		/// The medical sign off URL.
		/// </value>
		public string MedicalSignOffURL { get; set; }

		/// <summary>
		/// Gets or sets the view more URL.
		/// </summary>
		/// <value>
		/// The view more URL.
		/// </value>
		public string ViewMoreURL { get; set; }

		/// <summary>
		/// Gets or sets the crew change URL.
		/// </summary>
		/// <value>
		/// The crew change URL.
		/// </value>
		public string CrewChangeUrl { get; set; }

		#endregion
	}
}
