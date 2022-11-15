using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Mapped Risk Assessment Detail
	/// </summary>
	public class MappedRiskAssessmentDetail
	{
		/// <summary>
		/// Gets or sets the risk assessments.
		/// </summary>
		/// <value>
		/// The risk assessments.
		/// </value>
		public List<RiskAssessmentDetail> RiskAssessments { get; set; }

		/// <summary>
		/// Gets or sets the additional hazard list.
		/// </summary>
		/// <value>
		/// The additional hazard list.
		/// </value>
		public List<HazardDetail> AdditionalHazardList { get; set; }
	}
}
