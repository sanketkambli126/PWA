using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
	/// <summary>
	/// 
	/// </summary>
	public class HazoccDashboardResponse
	{
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
		/// Gets or sets the vessel description.
		/// </summary>
		/// <value>
		/// The vessel description.
		/// </value>
		public string VesselDescription { get; set; }

		/// <summary>
		/// Gets or sets the vessel age.
		/// </summary>
		/// <value>
		/// The vessel age.
		/// </value>
		public DateTime? VesselBuiltDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is next position available.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is next position available; otherwise, <c>false</c>.
		/// </value>
		public bool IsNextPositionAvailable { get; set; }

		/// <summary>
		/// Gets or sets the open accident details.
		/// </summary>
		/// <value>
		/// The open accident details.
		/// </value>
		public List<OpenAccidentDetail> OpenAccidentDetails { get; set; }

		/// <summary>
		/// Gets or sets the open incident details.
		/// </summary>
		/// <value>
		/// The open incident details.
		/// </value>
		public List<OpenIncidentDetail> OpenIncidentDetails { get; set; }

		/// <summary>
		/// Gets or sets the ltif count.
		/// </summary>
		/// <value>
		/// The ltif count.
		/// </value>
		public decimal LtifCount { get; set; }

		/// <summary>
		/// Gets or sets the TRCF count.
		/// </summary>
		/// <value>
		/// The TRCF count.
		/// </value>
		public decimal TrcfCount { get; set; }

		/// <summary>
		/// Gets or sets the mexp HRS.
		/// </summary>
		/// <value>
		/// The mexp HRS.
		/// </value>
		public decimal MexpHrs { get; set; }

		/// <summary>
		/// Gets or sets the near miss count.
		/// </summary>
		/// <value>
		/// The near miss count.
		/// </value>
		public int NearMissCount { get; set; }

		/// <summary>
		/// Gets or sets the unsafe act count.
		/// </summary>
		/// <value>
		/// The unsafe act count.
		/// </value>
		public int UnsafeActCount { get; set; }

		/// <summary>
		/// Gets or sets the safe act condition count.
		/// </summary>
		/// <value>
		/// The safe act condition count.
		/// </value>
		public int SafeActConditionCount { get; set; }

		/// <summary>
		/// Gets or sets my reviews count.
		/// </summary>
		/// <value>
		/// My reviews count.
		/// </value>
		public int MyReviewsCount { get; set; }

		/// <summary>
		/// Gets or sets the near miss summary.
		/// </summary>
		/// <value>
		/// The near miss summary.
		/// </value>
		public IncidentMonthSummary NearMissSummary { get; set; }

		/// <summary>
		/// Gets or sets the un safe summary.
		/// </summary>
		/// <value>
		/// The un safe summary.
		/// </value>
		public IncidentMonthSummary UnSafeSummary { get; set; }

		/// <summary>
		/// Gets or sets the safe summary.
		/// </summary>
		/// <value>
		/// The safe summary.
		/// </value>
		public IncidentMonthSummary SafeSummary { get; set; }

		/// <summary>
		/// Gets or sets the lti count.
		/// </summary>
		/// <value>
		/// The lti count.
		/// </value>
		public int LtiCount { get; set; }

		/// <summary>
		/// Gets or sets the TRC count.
		/// </summary>
		/// <value>
		/// The TRC count.
		/// </value>
		public int TrcCount { get; set; }

		/// <summary>
		/// Gets or sets the un safe condtion count.
		/// </summary>
		/// <value>
		/// The un safe condtion count.
		/// </value>
		public int UnSafeCondtionCount { get; set; }

		/// <summary>
		/// Gets or sets the open items count.
		/// </summary>
		/// <value>
		/// The open items count.
		/// </value>
		public int OpenItemsCount { get; set; }

		/// <summary>
		/// Gets or sets the crew fatality medical count.
		/// </summary>
		/// <value>
		/// The crew fatality medical count.
		/// </value>
		public int CrewFatalityMedicalCount { get; set; }

		/// <summary>
		/// Gets or sets the third party detail.
		/// </summary>
		/// <value>
		/// The third party detail.
		/// </value>
		public List<ThirdPartyAccidentDetail> ThirdPartyAccidentList { get; set; }

		/// <summary>
		/// Gets or sets the passenger accident list.
		/// </summary>
		/// <value>
		/// The passenger accident list.
		/// </value>
		public List<PassengerAccidentDetail> PassengerAccidentList { get; set; }

		/// <summary>
		/// Gets or sets the passenger mexp HRS.
		/// </summary>
		/// <value>
		/// The passenger mexp HRS.
		/// </value>
		public decimal PassengerMexpHrs { get; set; }

		/// <summary>
		/// Gets or sets the illness classification detail.
		/// </summary>
		/// <value>
		/// The illness classification detail.
		/// </value>
		public List<IllnessClassificationDetail> IllnessClassificationDetail { get; set; }
	}
}
