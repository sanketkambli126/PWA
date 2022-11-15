using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Voyage Event Bad Weather Details Wrapper
	/// </summary>
	public class VoyageEventBadWeatherDetailsWrapper
	{
		/// <summary>
		/// Gets or sets the bad weather details.
		/// </summary>
		/// <value>
		/// The bad weather details.
		/// </value>
		public List<VoyageEventBadWeatherDetails> BadWeatherDetails { get; set; }

		/// <summary>
		/// Gets or sets the charter wind force scale.
		/// </summary>
		/// <value>
		/// The charter wind force scale.
		/// </value>
		public int? CharterWindForceScale { get; set; }

		/// <summary>
		/// Gets or sets the charter wind force.
		/// </summary>
		/// <value>
		/// The charter wind force.
		/// </value>
		public string CharterWindForce { get; set; }

		/// <summary>
		/// Gets or sets the charter length scale.
		/// </summary>
		/// <value>
		/// The charter length scale.
		/// </value>
		public string CharterLengthScale { get; set; }

		/// <summary>
		/// Gets or sets the length of the charter swell.
		/// </summary>
		/// <value>
		/// The length of the charter swell.
		/// </value>
		public string CharterSwellLength { get; set; }

		/// <summary>
		/// Gets or sets the position identifier.
		/// </summary>
		/// <value>
		/// The position identifier.
		/// </value>
		public string PosId { get; set; }

		/// <summary>
		/// Gets or sets the pla identifier.
		/// </summary>
		/// <value>
		/// The pla identifier.
		/// </value>
		public string PlaId { get; set; }
	}
}
