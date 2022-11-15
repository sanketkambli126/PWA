using System;

namespace PWAFeaturesRnd.Models.Report.Dashboard
{
	/// <summary>
	/// Vessel Dashboard Response
	/// </summary>
	public class VesselDashboardResponse
	{
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

		/// <summary>
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets the manage start date.
		/// </summary>
		/// <value>
		/// The manage start date.
		/// </value>
		public DateTime? ManageStartDate { get; set; }

		/// <summary>
		/// Gets or sets the certificate.
		/// </summary>
		/// <value>
		/// The certificate.
		/// </value>
		public int Certificate { get; set; }

		/// <summary>
		/// Gets or sets the purchase order.
		/// </summary>
		/// <value>
		/// The purchase order.
		/// </value>
		public int PurchaseOrder { get; set; }

		/// <summary>
		/// Gets or sets the PMS.
		/// </summary>
		/// <value>
		/// The PMS.
		/// </value>
		public int PMS { get; set; }

		/// <summary>
		/// Gets or sets the defects.
		/// </summary>
		/// <value>
		/// The defects.
		/// </value>
		public int Defects { get; set; }

		/// <summary>
		/// Gets or sets the inspection.
		/// </summary>
		/// <value>
		/// The inspection.
		/// </value>
		public int Inspection { get; set; }

		/// <summary>
		/// Gets or sets the drill and campaign.
		/// </summary>
		/// <value>
		/// The drill and campaign.
		/// </value>
		public int DrillAndCampaign { get; set; }

		/// <summary>
		/// Gets or sets the crew.
		/// </summary>
		/// <value>
		/// The crew.
		/// </value>
		public int Crew { get; set; }

		/// <summary>
		/// Gets or sets the jsa and moc.
		/// </summary>
		/// <value>
		/// The jsa and moc.
		/// </value>
		public int JSAAndMOC { get; set; }

		/// <summary>
		/// Gets or sets the haz occ.
		/// </summary>
		/// <value>
		/// The haz occ.
		/// </value>
		public int HazOcc { get; set; }

		/// <summary>
		/// Gets or sets the opex.
		/// </summary>
		/// <value>
		/// The opex.
		/// </value>
		public int Opex { get; set; }

		/// <summary>
		/// Gets or sets the opex over spend.
		/// </summary>
		/// <value>
		/// The opex over spend.
		/// </value>
		public decimal OpexOverSpend { get; set; }

		/// <summary>
		/// Gets or sets the commercial.
		/// </summary>
		/// <value>
		/// The commercial.
		/// </value>
		public int Commercial { get; set; }

		/// <summary>
		/// Gets or sets the environment.
		/// </summary>
		/// <value>
		/// The environment.
		/// </value>
		public int Environment { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the destination port address.
		/// </summary>
		/// <value>
		/// The destination port address.
		/// </value>
		public string DestinationPortAddress { get; set; }

		/// <summary>
		/// Gets or sets the estimated date.
		/// </summary>
		/// <value>
		/// The estimated date.
		/// </value>
		public DateTime? EstimatedDate { get; set; }

		/// <summary>
		/// Gets or sets the lat degree.
		/// </summary>
		/// <value>
		/// The lat degree.
		/// </value>
		public decimal? LatDegree { get; set; }

		/// <summary>
		/// Gets or sets the lat indicator.
		/// </summary>
		/// <value>
		/// The lat indicator.
		/// </value>
		public string LatIndicator { get; set; }

		/// <summary>
		/// Gets or sets the lat minimum.
		/// </summary>
		/// <value>
		/// The lat minimum.
		/// </value>
		public decimal? LatMin { get; set; }

		/// <summary>
		/// Gets or sets the long degree.
		/// </summary>
		/// <value>
		/// The long degree.
		/// </value>
		public decimal? LongDegree { get; set; }

		/// <summary>
		/// Gets or sets the long indicator.
		/// </summary>
		/// <value>
		/// The long indicator.
		/// </value>
		public string LongIndicator { get; set; }

		/// <summary>
		/// Gets or sets the long minimum.
		/// </summary>
		/// <value>
		/// The long minimum.
		/// </value>
		public decimal? LongMin { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is sea passage.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is sea passage; otherwise, <c>false</c>.
		/// </value>
		public bool IsSeaPassage { get; set; }

		/// <summary>
		/// Gets or sets from country code.
		/// </summary>
		/// <value>
		/// From country code.
		/// </value>
		public string FromCountryCode { get; set; }

		/// <summary>
		/// Converts to countrycode.
		/// </summary>
		/// <value>
		/// To country code.
		/// </value>
		public string ToCountryCode { get; set; }

		/// <summary>
		/// Gets or sets from port identifier.
		/// </summary>
		/// <value>
		/// From port identifier.
		/// </value>
		public string FromPortId { get; set; }

		/// <summary>
		/// Gets or sets the next port identifier.
		/// </summary>
		/// <value>
		/// The next port identifier.
		/// </value>
		public string NextPortId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [from port is alert added].
		/// </summary>
		/// <value>
		///   <c>true</c> if [from port is alert added]; otherwise, <c>false</c>.
		/// </value>
		public bool FromPortIsAlertAdded { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [next port is alert added].
		/// </summary>
		/// <value>
		///   <c>true</c> if [next port is alert added]; otherwise, <c>false</c>.
		/// </value>
		public bool NextPortIsAlertAdded { get; set; }

		/// <summary>
		/// Gets or sets the port from date.
		/// </summary>
		/// <value>
		/// The port from date.
		/// </value>
		public DateTime? PortFromDate { get; set; }

		/// <summary>
		/// Gets or sets the imo number.
		/// </summary>
		/// <value>
		/// The imo number.
		/// </value>
		public string ImoNumber { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show commercial.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show commercial; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowCommercial { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show haz occ.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show haz occ; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowHazOcc { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show crewing.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show crewing; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowCrewing { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show environment.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show environment; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowEnvironment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show financials.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show financials; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowFinancials { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show certificates.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show certificates; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowCertificates { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show defects.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show defects; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowDefects { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show PMS.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show PMS; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowPMS { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show procurement.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show procurement; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowProcurement { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show inspections and ratings.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show inspections and ratings; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowInspectionsAndRatings { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel chief engg.
		/// </summary>
		/// <value>
		/// The name of the vessel chief engg.
		/// </value>
		public string VesselChiefEnggName { get; set; }

		/// <summary>
		/// Gets or sets the type of the vessel.
		/// </summary>
		/// <value>
		/// The type of the vessel.
		/// </value>
		public string VesselType { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel master.
		/// </summary>
		/// <value>
		/// The name of the vessel master.
		/// </value>
		public string VesselMasterName { get; set; }
	}
}
