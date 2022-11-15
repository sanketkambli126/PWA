using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// Bad Weather Detail View Model
	/// </summary>
	public class BadWeatherViewModel
	{
		/// <summary>
		/// Gets or sets the bad weather list.
		/// </summary>
		/// <value>
		/// The bad weather list.
		/// </value>
		public List<BadWeatherDetailViewModel> BadWeatherList { get; set; }

		/// <summary>
		/// Gets or sets the charter wind force.
		/// </summary>
		/// <value>
		/// The charter wind force.
		/// </value>
		public string CharterWindForce { get; set; }

		/// <summary>
		/// Gets or sets the length of the charter swell.
		/// </summary>
		/// <value>
		/// The length of the charter swell.
		/// </value>
		public string CharterSwellLength { get; set; }
	}
}
