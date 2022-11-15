using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Port Activity Details
	/// </summary>
	public class PortActivityDetails
	{
		/// <summary>
		/// Gets or sets the do total on berth.
		/// </summary>
		/// <value>
		/// The do total on berth.
		/// </value>
		public float? DoTotalOnBerth { get; set; }

		/// <summary>
		/// Gets or sets the LNG total on berth.
		/// </summary>
		/// <value>
		/// The LNG total on berth.
		/// </value>
		public float? LngTotalOnBerth { get; set; }

		/// <summary>
		/// Gets or sets the fo total in transit.
		/// </summary>
		/// <value>
		/// The fo total in transit.
		/// </value>
		public float? FoTotalInTransit { get; set; }

		/// <summary>
		/// Gets or sets the ls fo total in transit.
		/// </summary>
		/// <value>
		/// The ls fo total in transit.
		/// </value>
		public float? LsFoTotalInTransit { get; set; }

		/// <summary>
		/// Gets or sets the go total in transit.
		/// </summary>
		/// <value>
		/// The go total in transit.
		/// </value>
		public float? GoTotalInTransit { get; set; }

		/// <summary>
		/// Gets or sets the do total in transit.
		/// </summary>
		/// <value>
		/// The do total in transit.
		/// </value>
		public float? DoTotalInTransit { get; set; }

		/// <summary>
		/// Gets or sets the LNG total in transit.
		/// </summary>
		/// <value>
		/// The LNG total in transit.
		/// </value>
		public float? LngTotalInTransit { get; set; }

		/// <summary>
		/// Gets or sets the go total on berth.
		/// </summary>
		/// <value>
		/// The go total on berth.
		/// </value>
		public float? GoTotalOnBerth { get; set; }

		/// <summary>
		/// Gets or sets the fo total.
		/// </summary>
		/// <value>
		/// The fo total.
		/// </value>
		public float? FoTotal { get; set; }

		/// <summary>
		/// Gets or sets the go total.
		/// </summary>
		/// <value>
		/// The go total.
		/// </value>
		public float? GoTotal { get; set; }

		/// <summary>
		/// Gets or sets the do total.
		/// </summary>
		/// <value>
		/// The do total.
		/// </value>
		public float? DoTotal { get; set; }

		/// <summary>
		/// Gets or sets the LNG total.
		/// </summary>
		/// <value>
		/// The LNG total.
		/// </value>
		public float? LngTotal { get; set; }

		/// <summary>
		/// Gets or sets the type of the trade.
		/// </summary>
		/// <value>
		/// The type of the trade.
		/// </value>
		public string TradeType { get; set; }

		/// <summary>
		/// Gets or sets the eosp status.
		/// </summary>
		/// <value>
		/// The eosp status.
		/// </value>
		public PositionListDateStatus EospStatus { get; set; }

		/// <summary>
		/// Gets or sets the berth status.
		/// </summary>
		/// <value>
		/// The berth status.
		/// </value>
		public PositionListDateStatus BerthStatus { get; set; }

		/// <summary>
		/// Gets or sets the un berth status.
		/// </summary>
		/// <value>
		/// The un berth status.
		/// </value>
		public PositionListDateStatus UnBerthStatus { get; set; }

		/// <summary>
		/// Gets or sets the ls fo total.
		/// </summary>
		/// <value>
		/// The ls fo total.
		/// </value>
		public float? LsFoTotal { get; set; }

		/// <summary>
		/// Gets or sets the faop status.
		/// </summary>
		/// <value>
		/// The faop status.
		/// </value>
		public PositionListDateStatus FaopStatus { get; set; }

		/// <summary>
		/// Gets or sets the ls fo total on berth.
		/// </summary>
		/// <value>
		/// The ls fo total on berth.
		/// </value>
		public float? LsFoTotalOnBerth { get; set; }

		/// <summary>
		/// Gets or sets the total time.
		/// </summary>
		/// <value>
		/// The total time.
		/// </value>
		public TimeSpan? TotalTime { get; set; }

		/// <summary>
		/// Gets or sets the CHT company.
		/// </summary>
		/// <value>
		/// The CHT company.
		/// </value>
		public string ChtCompany { get; set; }

		/// <summary>
		/// Gets or sets the CHT company code.
		/// </summary>
		/// <value>
		/// The CHT company code.
		/// </value>
		public string ChtCompanyCode { get; set; }

		/// <summary>
		/// Gets or sets the CHT vessel.
		/// </summary>
		/// <value>
		/// The CHT vessel.
		/// </value>
		public string ChtVessel { get; set; }

		/// <summary>
		/// Gets or sets the type of the CHT.
		/// </summary>
		/// <value>
		/// The type of the CHT.
		/// </value>
		public string ChtType { get; set; }

		/// <summary>
		/// Gets or sets the CHT voycode.
		/// </summary>
		/// <value>
		/// The CHT voycode.
		/// </value>
		public string ChtVoycode { get; set; }

		/// <summary>
		/// Gets or sets the CHT number.
		/// </summary>
		/// <value>
		/// The CHT number.
		/// </value>
		public string ChtNum { get; set; }

		/// <summary>
		/// Gets or sets the CHT voyage.
		/// </summary>
		/// <value>
		/// The CHT voyage.
		/// </value>
		public string ChtVoyage { get; set; }

		/// <summary>
		/// Gets or sets the fo total on berth.
		/// </summary>
		/// <value>
		/// The fo total on berth.
		/// </value>
		public float? FoTotalOnBerth { get; set; }

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		/// <value>
		/// The port.
		/// </value>
		public string Port { get; set; }

		/// <summary>
		/// Gets or sets the berthed.
		/// </summary>
		/// <value>
		/// The berthed.
		/// </value>
		public DateTime? Berthed { get; set; }

		/// <summary>
		/// Gets or sets the un berthed.
		/// </summary>
		/// <value>
		/// The un berthed.
		/// </value>
		public DateTime? UnBerthed { get; set; }

		/// <summary>
		/// Gets or sets the faop.
		/// </summary>
		/// <value>
		/// The faop.
		/// </value>
		public DateTime? Faop { get; set; }

		/// <summary>
		/// Gets or sets the cargo operation time.
		/// </summary>
		/// <value>
		/// The cargo operation time.
		/// </value>
		public TimeSpan? CargoOperationTime { get; set; }

		/// <summary>
		/// Gets or sets the berth time.
		/// </summary>
		/// <value>
		/// The berth time.
		/// </value>
		public TimeSpan? BerthTime { get; set; }

		/// <summary>
		/// Gets or sets the port time.
		/// </summary>
		/// <value>
		/// The port time.
		/// </value>
		public TimeSpan? PortTime { get; set; }

		/// <summary>
		/// Gets or sets the outofservice time.
		/// </summary>
		/// <value>
		/// The outofservice time.
		/// </value>
		public TimeSpan? OutofserviceTime { get; set; }

		/// <summary>
		/// Gets or sets the eosp.
		/// </summary>
		/// <value>
		/// The eosp.
		/// </value>
		public DateTime? EOSP { get; set; }

		/// <summary>
		/// Gets or sets the last reported event.
		/// </summary>
		/// <value>
		/// The last reported event.
		/// </value>
		public DateTime? LastReportedEvent { get; set; }
	}
}
