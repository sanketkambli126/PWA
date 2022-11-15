using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
	/// <summary>
	/// Omv Findings Summary View Model
	/// </summary>
	public class OmvFindingsSummaryViewModel
	{
		/// <summary>
		/// Gets or sets the inspection count.
		/// </summary>
		/// <value>
		/// The inspection count.
		/// </value>
		public int InspectionCount { get; set; }

		/// <summary>
		/// Gets or sets the finding count.
		/// </summary>
		/// <value>
		/// The finding count.
		/// </value>
		public int FindingCount { get; set; }

		/// <summary>
		/// Gets or sets the omv rate.
		/// </summary>
		/// <value>
		/// The omv rate.
		/// </value>
		public string OmvRate { get; set; }

		/// <summary>
		/// Gets or sets the data list.
		/// </summary>
		/// <value>
		/// The data list.
		/// </value>
		public List<OmvFindingsResponseViewModel> DataList { get; set; }
	}
}
