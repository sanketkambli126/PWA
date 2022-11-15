using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Breaks And Bad Weather Detail
	/// </summary>
	public class BreaksAndBadWeatherDetail
	{
		/// <summary>
		/// Gets or sets the spa identifier.
		/// </summary>
		/// <value>
		/// The spa identifier.
		/// </value>
		public string SpaId { get; set; }

		/// <summary>
		/// Gets or sets the maximum sea passage wind force scale.
		/// </summary>
		/// <value>
		/// The maximum sea passage wind force scale.
		/// </value>
		public int? MaxSeaPassageWindForceScale { get; set; }

		/// <summary>
		/// Gets or sets the maximum sea passage wind force.
		/// </summary>
		/// <value>
		/// The maximum sea passage wind force.
		/// </value>
		public string MaxSeaPassageWindForce { get; set; }

		/// <summary>
		/// Gets or sets the maximum length of the sea passage swell.
		/// </summary>
		/// <value>
		/// The maximum length of the sea passage swell.
		/// </value>
		public string MaxSeaPassageSwellLength { get; set; }

		/// <summary>
		/// Gets or sets the maximum sea passage swell length description.
		/// </summary>
		/// <value>
		/// The maximum sea passage swell length description.
		/// </value>
		public string MaxSeaPassageSwellLengthDescription { get; set; }

		/// <summary>
		/// Gets or sets the charter sea passage wind force scale.
		/// </summary>
		/// <value>
		/// The charter sea passage wind force scale.
		/// </value>
		public int? CharterSeaPassageWindForceScale { get; set; }

		/// <summary>
		/// Gets or sets the charter sea passage wind force.
		/// </summary>
		/// <value>
		/// The charter sea passage wind force.
		/// </value>
		public string CharterSeaPassageWindForce { get; set; }

		/// <summary>
		/// Gets or sets the length of the charter sea passage swell.
		/// </summary>
		/// <value>
		/// The length of the charter sea passage swell.
		/// </value>
		public string CharterSeaPassageSwellLength { get; set; }

		/// <summary>
		/// Gets or sets the charter sea passage swell length description.
		/// </summary>
		/// <value>
		/// The charter sea passage swell length description.
		/// </value>
		public string CharterSeaPassageSwellLengthDescription { get; set; }

		/// <summary>
		/// Gets or sets the list of breaks.
		/// </summary>
		/// <value>
		/// The list of breaks.
		/// </value>
		public List<VoyageActivityDelay> ListOfBreaks { get; set; }

		/// <summary>
		/// Gets or sets the spa date.
		/// </summary>
		/// <value>
		/// The spa date.
		/// </value>
		public DateTime? SpaDate { get; set; }
	}
}
