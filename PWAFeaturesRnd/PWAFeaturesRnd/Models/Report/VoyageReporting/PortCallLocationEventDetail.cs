using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// PortCallLocationEventDetail
	/// </summary>
	public class PortCallLocationEventDetail
	{
		/// <summary>
		/// Gets or sets a value indicating whether this instance is delay.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is delay; otherwise, <c>false</c>.
		/// </value>
		public bool IsDelay { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is inbound outbound.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is inbound outbound; otherwise, <c>false</c>.
		/// </value>
		public bool IsInboundOutbound { get; set; }

		/// <summary>
		/// Gets or sets the PPF identifier.
		/// </summary>
		/// <value>
		/// The PPF identifier.
		/// </value>
		public string PpfId { get; set; }

		/// <summary>
		/// Gets or sets the fw technical.
		/// </summary>
		/// <value>
		/// The fw technical.
		/// </value>
		public decimal FwTechnical { get; set; }

		/// <summary>
		/// Gets or sets the fw domestic.
		/// </summary>
		/// <value>
		/// The fw domestic.
		/// </value>
		public decimal FwDomestic { get; set; }

		/// <summary>
		/// Gets or sets the name of the view.
		/// </summary>
		/// <value>
		/// The name of the view.
		/// </value>
		public string ViewName { get; set; }

		/// <summary>
		/// Gets or sets the document path.
		/// </summary>
		/// <value>
		/// The document path.
		/// </value>
		public string DocumentPath { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has documents.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has documents; otherwise, <c>false</c>.
		/// </value>
		public bool HasDocuments { get; set; }

		/// <summary>
		/// Gets or sets the comments.
		/// </summary>
		/// <value>
		/// The comments.
		/// </value>
		public string Comments { get; set; }

		/// <summary>
		/// Gets or sets the total LNG.
		/// </summary>
		/// <value>
		/// The total LNG.
		/// </value>
		public decimal? TotalLng { get; set; }

		/// <summary>
		/// Gets or sets the total LNG Cargo.
		/// </summary>
		/// <value>
		/// The total LNG Cargo.
		/// </value>
		public decimal? TotalLngCargo { get; set; }

		/// <summary>
		/// Gets or sets the total go.
		/// </summary>
		/// <value>
		/// The total go.
		/// </value>
		public decimal? TotalGo { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is bad weather.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is bad weather; otherwise, <c>false</c>.
		/// </value>
		public bool IsBadWeather { get; set; }

		/// <summary>
		/// Gets or sets the total do.
		/// </summary>
		/// <value>
		/// The total do.
		/// </value>
		public decimal? TotalDo { get; set; }

		/// <summary>
		/// Gets or sets the total fo.
		/// </summary>
		/// <value>
		/// The total fo.
		/// </value>
		public decimal? TotalFo { get; set; }

		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>
		/// The distance.
		/// </value>
		public decimal? Distance { get; set; }

		/// <summary>
		/// Gets or sets the off hire.
		/// </summary>
		/// <value>
		/// The off hire.
		/// </value>
		public bool? OffHire { get; set; }

		/// <summary>
		/// Gets or sets the lop.
		/// </summary>
		/// <value>
		/// The lop.
		/// </value>
		public bool? Lop { get; set; }

		/// <summary>
		/// Gets or sets the time elapsed.
		/// </summary>
		/// <value>
		/// The time elapsed.
		/// </value>
		public TimeSpan? TimeElapsed { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the event.
		/// </summary>
		/// <value>
		/// The name of the event.
		/// </value>
		public string EventName { get; set; }

		/// <summary>
		/// Gets or sets the event identifier.
		/// </summary>
		/// <value>
		/// The event identifier.
		/// </value>
		public string EventId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the position list identifier.
		/// </summary>
		/// <value>
		/// The position list identifier.
		/// </value>
		public string PositionListId { get; set; }

		/// <summary>
		/// Gets or sets the total lsfo.
		/// </summary>
		/// <value>
		/// The total lsfo.
		/// </value>
		public decimal? TotalLsfo { get; set; }

		/// <summary>
		/// Gets or sets the PSF is incomplete event.
		/// </summary>
		/// <value>
		/// The PSF is incomplete event.
		/// </value>
		public bool? PsfIsIncompleteEvent { get; set; }
	}
}
