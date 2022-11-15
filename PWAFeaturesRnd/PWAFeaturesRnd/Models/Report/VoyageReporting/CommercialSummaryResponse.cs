using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    public class CommercialSummaryResponse
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
		public decimal? FuelUnderPerformance { get; set; }

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
		public decimal? SpeedUnderPerformance { get; set; }

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
		public int? PredictedBadWeather { get; set; }

		/// <summary>
		/// Gets or sets the predicted bad weather priority.
		/// </summary>
		/// <value>
		/// The PredictedBadWeatherPriority.
		/// </value>
		public int PredictedBadWeatherPriority { get; set; }

		#endregion
	}
}
