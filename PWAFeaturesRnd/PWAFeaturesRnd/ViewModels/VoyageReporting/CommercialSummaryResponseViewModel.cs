using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// Voyage Reporting Summary Response ViewModel
	/// </summary>
	public class CommercialSummaryResponseViewModel
    {
		#region Properties

		/// <summary>
		/// Gets or sets the unplanned off hire hrs.
		/// </summary>
		/// <value>
		/// The UnplannedOffHire Hrs.
		/// </value>
		public string UnplannedOffHireHrs { get; set; }

		/// <summary>
		/// Gets or sets the unplanned off hire hrs priority.
		/// </summary>
		/// <value>
		/// The UnplannedOffHire Hrs priority.
		/// </value>
		public int UnplannedOffHireHrsPriority { get; set; }

		/// <summary>
		/// Gets or sets the fuel under performance.
		/// </summary>
		/// <value>
		/// The FuelUnderPerformance.
		/// </value>
		public string FuelUnderPerformance { get; set; }

		/// <summary>
		/// Gets or sets the fuel under performance priority.
		/// </summary>
		/// <value>
		/// The FuelUnderPerformancePriority.
		/// </value>
		public int FuelUnderPerformancePriority { get; set; }

		/// <summary>
		/// Gets or sets the speed under performance.
		/// </summary>
		/// <value>
		/// The SpeedUnderPerformance.
		/// </value>
		public string SpeedUnderPerformance { get; set; }

		/// <summary>
		/// Gets or sets the speed under performance priority.
		/// </summary>
		/// <value>
		/// The SpeedUnderPerformancePriority.
		/// </value>
		public int SpeedUnderPerformancePriority { get; set; }

		/// <summary>
		/// Gets or sets the predicted bad weather.
		/// </summary>
		/// <value>
		/// The PredictedBadWeather.
		/// </value>
		public int PredictedBadWeather { get; set; }

		/// <summary>
		/// Gets or sets the predicted bad weather priority.
		/// </summary>
		/// <value>
		/// The PredictedBadWeatherPriority.
		/// </value>
		public int PredictedBadWeatherPriority { get; set; }

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

		#endregion

		#region Navigation Links
		/// <summary>
		/// Gets or sets the unplanned off hire hrs url.
		/// </summary>
		/// <value>
		/// The unplanned off hire hrs url.
		/// </value>
		public string UnplannedOffHireHrsUrl { get; set; }

		/// <summary>
		/// Gets or sets the fuel under performance url.
		/// </summary>
		/// <value>
		/// The fuel under performance url.
		/// </value>
		public string FuelUnderPerformanceUrl { get; set; }

		/// <summary>
		/// Gets or sets the speed under performance url.
		/// </summary>
		/// <value>
		/// The speed under performance url.
		/// </value>
		public string SpeedUnderPerformanceUrl { get; set; }

		/// <summary>
		/// Gets or sets the predicte bad weather url.
		/// </summary>
		/// <value>
		/// The predicte bad weather url.
		/// </value>
		public string PredicteBadWeatherUrl { get; set; }

		/// <summary>
		/// Gets or sets the view more URL.
		/// </summary>
		/// <value>
		/// The view more URL.
		/// </value>
		public string ViewMoreURL { get; set; }

		#endregion
	}
}
