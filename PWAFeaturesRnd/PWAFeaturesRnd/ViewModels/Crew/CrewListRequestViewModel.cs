using System;

namespace PWAFeaturesRnd.ViewModels.Crew
{
	/// <summary>
	/// Crew List Request ViewModel
	/// </summary>
	public class CrewListRequestViewModel
    {
		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime ToDate { get; set; }
	}
}
