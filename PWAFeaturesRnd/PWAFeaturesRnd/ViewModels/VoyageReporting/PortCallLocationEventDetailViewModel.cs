using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// PortCallLocationEventDetailViewModel
	/// </summary>
	public class PortCallLocationEventDetailViewModel
	{
		/// <summary>
		/// Gets or sets the document request URL.
		/// </summary>
		/// <value>
		/// The document request URL.
		/// </value>
		public string DocumentRequestUrl { get; set; }

		/// <summary>
		/// Gets or sets the name of the event.
		/// </summary>
		/// <value>
		/// The name of the event.
		/// </value>
		public string EventName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is in complete.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is in complete; otherwise, <c>false</c>.
		/// </value>
		public bool IsInComplete { get; set; }

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
		/// Gets or sets a value indicating whether this instance is ellapsed time visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is ellapsed time visible; otherwise, <c>false</c>.
		/// </value>
		public TimeSpan? ElapsedTime { get; set; }

		/// <summary>
		/// Gets or sets the total elapsed hours.
		/// </summary>
		/// <value>
		/// The total elapsed hours.
		/// </value>
		public string TotalElapsedHours { get; set; }

		/// <summary>
		/// Gets or sets the total elapsed minutes.
		/// </summary>
		/// <value>
		/// The total elapsed minutes.
		/// </value>
		public string TotalElapsedMinutes { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is lop.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is lop; otherwise, <c>false</c>.
		/// </value>
		public bool IsLop { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is off hire.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
		/// </value>
		public bool IsOffHire { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is delay.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is delay; otherwise, <c>false</c>.
		/// </value>
		public bool IsDelay { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is bad weather.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is bad weather; otherwise, <c>false</c>.
		/// </value>
		public bool IsBadWeather { get; set; }

		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>
		/// The distance.
		/// </value>
		public decimal? Distance { get; set; }

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
		/// Gets or sets the total lsfo.
		/// </summary>
		/// <value>
		/// The total lsfo.
		/// </value>
		public decimal? TotalLsfo { get; set; }

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
		/// Gets or sets the PpfId.
		/// </summary>
		/// <value>
		/// The PpfId.
		/// </value>
		public string PpfId { get; set; }
		/// <summary>
		/// Gets or sets the PpfId.
		/// </summary>
		/// <value>
		/// The PpfId.
		/// </value>
		public string PsfId { get; set; }

		/// <summary>
		/// Gets or sets the PosId.
		/// </summary>
		/// <value>
		/// The PosId.
		/// </value>
		public string PosId { get; set; }

		/// <summary>
		/// Gets or sets the VesselId.
		/// </summary>
		/// <value>
		/// The VesselId.
		/// </value>
		public string VesselId { get; set; }
		/// <summary>
		/// Gets or sets the View Name.
		/// </summary>
		/// <value>
		/// The View Name.
		/// </value>
		public string ViewName { get; set; }
		

	}
}