using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Voyage Activity Bad Weather Detail
	/// </summary>
	public class VoyageActivityBadWeatherDetail
	{
		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VES_ID { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [bad weather alert].
		/// </summary>
		/// <value>
		///   <c>true</c> if [bad weather alert]; otherwise, <c>false</c>.
		/// </value>
		public bool BadWeatherAlert { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [bad weather].
		/// </summary>
		/// <value>
		///   <c>true</c> if [bad weather]; otherwise, <c>false</c>.
		/// </value>
		public bool BadWeather { get; set; }

		/// <summary>
		/// Gets or sets the distance log.
		/// </summary>
		/// <value>
		/// The distance log.
		/// </value>
		public decimal? DistanceLog { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is break in passage.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is break in passage; otherwise, <c>false</c>.
		/// </value>
		public bool IsBreakInPassage { get; set; }

		/// <summary>
		/// Gets or sets the distance on passage.
		/// </summary>
		/// <value>
		/// The distance on passage.
		/// </value>
		public decimal? DistanceOnPassage { get; set; }

		/// <summary>
		/// Gets or sets the spa identifier.
		/// </summary>
		/// <value>
		/// The spa identifier.
		/// </value>
		public string SpaId { get; set; }

		/// <summary>
		/// Gets or sets the spa date.
		/// </summary>
		/// <value>
		/// The spa date.
		/// </value>
		public DateTime? SpaDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is IDL.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is IDL; otherwise, <c>false</c>.
		/// </value>
		public bool IsIdl { get; set; }
	}
}
