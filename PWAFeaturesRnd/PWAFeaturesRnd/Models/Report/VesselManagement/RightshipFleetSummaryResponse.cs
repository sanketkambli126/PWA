﻿namespace PWAFeaturesRnd.Models.Report.VesselManagement
{
	/// <summary>
	/// Right ship Fleet Summary Response
	/// </summary>
	public class RightshipFleetSummaryResponse
	{
		/// <summary>
		/// Gets or sets the right ship rate.
		/// </summary>
		/// <value>
		/// The right ship rate.
		/// </value>
		public decimal RightShipRate { get; set; }

		/// <summary>
		/// Gets or sets the right ship priority.
		/// </summary>
		/// <value>
		/// The right ship priority.
		/// </value>
		public int RightShipPriority { get; set; }

		/// <summary>
		/// Gets or sets the right ship information.
		/// </summary>
		/// <value>
		/// The right ship information.
		/// </value>
		public string RightShipInfo { get; set; }
	}
}
