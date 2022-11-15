using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Voyage Landing Page Details
	/// </summary>
	public class VoyageLandingPageDetails
	{
		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the type of the vessel profile.
		/// </summary>
		/// <value>
		/// The type of the vessel profile.
		/// </value>
		public string VesselProfileType { get; set; }

		/// <summary>
		/// Gets or sets the sewage capacity.
		/// </summary>
		/// <value>
		/// The sewage capacity.
		/// </value>
		public decimal? SewageCapacity { get; set; }

		/// <summary>
		/// Gets or sets the slop capacity.
		/// </summary>
		/// <value>
		/// The slop capacity.
		/// </value>
		public decimal? SlopCapacity { get; set; }

		/// <summary>
		/// Gets or sets the sludge capacity.
		/// </summary>
		/// <value>
		/// The sludge capacity.
		/// </value>
		public decimal? SludgeCapacity { get; set; }

		/// <summary>
		/// Gets or sets the bilge capacity.
		/// </summary>
		/// <value>
		/// The bilge capacity.
		/// </value>
		public decimal? BilgeCapacity { get; set; }

		/// <summary>
		/// Gets or sets the fw technical capacity.
		/// </summary>
		/// <value>
		/// The fw technical capacity.
		/// </value>
		public decimal? FWTechnicalCapacity { get; set; }

		/// <summary>
		/// Gets or sets the fw domestic capacity.
		/// </summary>
		/// <value>
		/// The fw domestic capacity.
		/// </value>
		public decimal? FWDomesticCapacity { get; set; }

		/// <summary>
		/// Gets or sets the LNG capacity.
		/// </summary>
		/// <value>
		/// The LNG capacity.
		/// </value>
		public decimal? LNGCapacity { get; set; }

		/// <summary>
		/// Gets or sets the go capacity.
		/// </summary>
		/// <value>
		/// The go capacity.
		/// </value>
		public decimal? GOCapacity { get; set; }

		/// <summary>
		/// Gets or sets the do capacity.
		/// </summary>
		/// <value>
		/// The do capacity.
		/// </value>
		public decimal? DOCapacity { get; set; }

		/// <summary>
		/// Gets or sets the lsfo capacity.
		/// </summary>
		/// <value>
		/// The lsfo capacity.
		/// </value>
		public decimal? LSFOCapacity { get; set; }

		/// <summary>
		/// Gets or sets the fo capacity.
		/// </summary>
		/// <value>
		/// The fo capacity.
		/// </value>
		public decimal? FoCapacity { get; set; }

		/// <summary>
		/// Gets or sets the distance travelled.
		/// </summary>
		/// <value>
		/// The distance travelled.
		/// </value>
		public decimal DistanceTravelled { get; set; }

		/// <summary>
		/// Gets or sets the remaining distance.
		/// </summary>
		/// <value>
		/// The remaining distance.
		/// </value>
		public float? RemainingDistance { get; set; }

		/// <summary>
		/// Gets or sets the sewage rob.
		/// </summary>
		/// <value>
		/// The sewage rob.
		/// </value>
		public decimal? SewageRob { get; set; }

		/// <summary>
		/// Gets or sets the slops rob.
		/// </summary>
		/// <value>
		/// The slops rob.
		/// </value>
		public decimal? SlopsRob { get; set; }

		/// <summary>
		/// Gets or sets the bilge rob.
		/// </summary>
		/// <value>
		/// The bilge rob.
		/// </value>
		public decimal? BilgeRob { get; set; }

		/// <summary>
		/// Gets or sets the sludge rob.
		/// </summary>
		/// <value>
		/// The sludge rob.
		/// </value>
		public decimal? SludgeRob { get; set; }

		/// <summary>
		/// Gets or sets the technical fw rob.
		/// </summary>
		/// <value>
		/// The technical fw rob.
		/// </value>
		public decimal? TechnicalFWRob { get; set; }

		/// <summary>
		/// Gets or sets the domestic fw rob.
		/// </summary>
		/// <value>
		/// The domestic fw rob.
		/// </value>
		public decimal? DomesticFWRob { get; set; }

		/// <summary>
		/// Gets or sets the vessel description.
		/// </summary>
		/// <value>
		/// The vessel description.
		/// </value>
		public string VesselDescription { get; set; }

		/// <summary>
		/// Gets or sets the LNG rob.
		/// </summary>
		/// <value>
		/// The LNG rob.
		/// </value>
		public decimal? LNGRob { get; set; }

		/// <summary>
		/// Gets or sets the vessel built date.
		/// </summary>
		/// <value>
		/// The vessel built date.
		/// </value>
		public DateTime? VesselBuiltDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the next port.
		/// </summary>
		/// <value>
		/// The name of the next port.
		/// </value>
		public string NextPortName { get; set; }

		/// <summary>
		/// Gets or sets the next port identifier.
		/// </summary>
		/// <value>
		/// The next port identifier.
		/// </value>
		public string NextPortId { get; set; }

		/// <summary>
		/// Gets or sets the has to port alert.
		/// </summary>
		/// <value>
		/// The has to port alert.
		/// </value>
		public bool? HasToPortAlert { get; set; }

		/// <summary>
		/// Converts to portid.
		/// </summary>
		/// <value>
		/// To port identifier.
		/// </value>
		public string ToPortId { get; set; }

		/// <summary>
		/// Gets or sets the has from port alert.
		/// </summary>
		/// <value>
		/// The has from port alert.
		/// </value>
		public bool? HasFromPortAlert { get; set; }

		/// <summary>
		/// Gets or sets from port identifier.
		/// </summary>
		/// <value>
		/// From port identifier.
		/// </value>
		public string FromPortId { get; set; }

		/// <summary>
		/// Gets or sets the charter identifier.
		/// </summary>
		/// <value>
		/// The charter identifier.
		/// </value>
		public string CharterId { get; set; }

		/// <summary>
		/// Gets or sets the is port bad weather.
		/// </summary>
		/// <value>
		/// The is port bad weather.
		/// </value>
		public bool? IsPortBadWeather { get; set; }

		/// <summary>
		/// Gets or sets the vessel imo number.
		/// </summary>
		/// <value>
		/// The vessel imo number.
		/// </value>
		public string VesselImoNumber { get; set; }

		/// <summary>
		/// Gets or sets the division factor.
		/// </summary>
		/// <value>
		/// The division factor.
		/// </value>
		public decimal? DivisionFactor { get; set; }

		/// <summary>
		/// Gets or sets the next port date.
		/// </summary>
		/// <value>
		/// The next port date.
		/// </value>
		public DateTime? NextPortDate { get; set; }

		/// <summary>
		/// Gets or sets the security level.
		/// </summary>
		/// <value>
		/// The security level.
		/// </value>
		public string SecurityLevel { get; set; }

		/// <summary>
		/// Gets or sets the is delay.
		/// </summary>
		/// <value>
		/// The is delay.
		/// </value>
		public bool? IsDelay { get; set; }

		/// <summary>
		/// Gets or sets the longitude direction.
		/// </summary>
		/// <value>
		/// The longitude direction.
		/// </value>
		public string LongitudeDirection { get; set; }

		/// <summary>
		/// Gets or sets the longitude minute.
		/// </summary>
		/// <value>
		/// The longitude minute.
		/// </value>
		public int? LongitudeMinute { get; set; }

		/// <summary>
		/// Gets or sets the longitude degree.
		/// </summary>
		/// <value>
		/// The longitude degree.
		/// </value>
		public int? LongitudeDegree { get; set; }

		/// <summary>
		/// Gets or sets the lantitude direction.
		/// </summary>
		/// <value>
		/// The lantitude direction.
		/// </value>
		public string LantitudeDirection { get; set; }

		/// <summary>
		/// Gets or sets the lantitude minute.
		/// </summary>
		/// <value>
		/// The lantitude minute.
		/// </value>
		public int? LantitudeMinute { get; set; }

		/// <summary>
		/// Gets or sets the lantitude degree.
		/// </summary>
		/// <value>
		/// The lantitude degree.
		/// </value>
		public int? LantitudeDegree { get; set; }

		/// <summary>
		/// Gets or sets the is agent available.
		/// </summary>
		/// <value>
		/// The is agent available.
		/// </value>
		public bool? IsAgentAvailable { get; set; }

		/// <summary>
		/// Gets or sets the lastest event date.
		/// </summary>
		/// <value>
		/// The lastest event date.
		/// </value>
		public DateTime? LastestEventDate { get; set; }

		/// <summary>
		/// Gets or sets the next count code.
		/// </summary>
		/// <value>
		/// The next count code.
		/// </value>
		public string NextCntCode { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is next position available.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is next position available; otherwise, <c>false</c>.
		/// </value>
		public bool IsNextPositionAvailable { get; set; }

		/// <summary>
		/// Gets or sets the go rob.
		/// </summary>
		/// <value>
		/// The go rob.
		/// </value>
		public decimal? GORob { get; set; }

		/// <summary>
		/// Gets or sets the do rob.
		/// </summary>
		/// <value>
		/// The do rob.
		/// </value>
		public decimal? DORob { get; set; }

		/// <summary>
		/// Gets or sets the lsfo rob.
		/// </summary>
		/// <value>
		/// The lsfo rob.
		/// </value>
		public decimal? LSFORob { get; set; }

		/// <summary>
		/// Gets or sets the charterest do cons.
		/// </summary>
		/// <value>
		/// The charterest do cons.
		/// </value>
		public decimal? CharterestDoCons { get; set; }

		/// <summary>
		/// Gets or sets the charterest ls fo cons.
		/// </summary>
		/// <value>
		/// The charterest ls fo cons.
		/// </value>
		public decimal? CharterestLsFoCons { get; set; }

		/// <summary>
		/// Gets or sets the charterest fo cons.
		/// </summary>
		/// <value>
		/// The charterest fo cons.
		/// </value>
		public decimal? CharterestFoCons { get; set; }

		/// <summary>
		/// Gets or sets the charter speed.
		/// </summary>
		/// <value>
		/// The charter speed.
		/// </value>
		public decimal? CharterSpeed { get; set; }

		/// <summary>
		/// Gets or sets the charter voyage.
		/// </summary>
		/// <value>
		/// The charter voyage.
		/// </value>
		public int? CharterVoyage { get; set; }

		/// <summary>
		/// Gets or sets the type of the charter.
		/// </summary>
		/// <value>
		/// The type of the charter.
		/// </value>
		public string CharterType { get; set; }

		/// <summary>
		/// Gets or sets the charter number.
		/// </summary>
		/// <value>
		/// The charter number.
		/// </value>
		public string CharterNumber { get; set; }

		/// <summary>
		/// Gets or sets the charter vessel code.
		/// </summary>
		/// <value>
		/// The charter vessel code.
		/// </value>
		public string CharterVesselCode { get; set; }

		/// <summary>
		/// Gets or sets the charter company.
		/// </summary>
		/// <value>
		/// The charter company.
		/// </value>
		public string CharterCompany { get; set; }

		/// <summary>
		/// Gets or sets the total distance.
		/// </summary>
		/// <value>
		/// The total distance.
		/// </value>
		public decimal TotalDistance { get; set; }

		/// <summary>
		/// Converts to cntcode.
		/// </summary>
		/// <value>
		/// To count code.
		/// </value>
		public string ToCntCode { get; set; }

		/// <summary>
		/// Converts to portname.
		/// </summary>
		/// <value>
		/// The name of to port.
		/// </value>
		public string ToPortName { get; set; }

		/// <summary>
		/// Gets or sets from count code.
		/// </summary>
		/// <value>
		/// From count code.
		/// </value>
		public string FromCntCode { get; set; }

		/// <summary>
		/// Gets or sets the name of from port.
		/// </summary>
		/// <value>
		/// The name of from port.
		/// </value>
		public string FromPortName { get; set; }

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
		/// Gets or sets the type of the position.
		/// </summary>
		/// <value>
		/// The type of the position.
		/// </value>
		public string PosType { get; set; }

		/// <summary>
		/// Gets or sets the name of the activity.
		/// </summary>
		/// <value>
		/// The name of the activity.
		/// </value>
		public string ActivityName { get; set; }

		/// <summary>
		/// Gets or sets the pla identifier.
		/// </summary>
		/// <value>
		/// The pla identifier.
		/// </value>
		public string PLA_ID { get; set; }

		/// <summary>
		/// Gets or sets the position identifier.
		/// </summary>
		/// <value>
		/// The position identifier.
		/// </value>
		public string POS_ID { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VES_ID { get; set; }

		/// <summary>
		/// Gets or sets the charterest go cons.
		/// </summary>
		/// <value>
		/// The charterest go cons.
		/// </value>
		public decimal? CharterestGoCons { get; set; }

		/// <summary>
		/// Gets or sets the charterest LNG cons.
		/// </summary>
		/// <value>
		/// The charterest LNG cons.
		/// </value>
		public decimal? CharterestLNGCons { get; set; }

		/// <summary>
		/// Gets or sets the charter swell length desc.
		/// </summary>
		/// <value>
		/// The charter swell length desc.
		/// </value>
		public string CharterSwellLengthDesc { get; set; }

		/// <summary>
		/// Gets or sets the charter wind force desc.
		/// </summary>
		/// <value>
		/// The charter wind force desc.
		/// </value>
		public string CharterWindForceDesc { get; set; }

		/// <summary>
		/// Gets or sets the fo rob.
		/// </summary>
		/// <value>
		/// The fo rob.
		/// </value>
		public decimal? FORob { get; set; }

		/// <summary>
		/// Gets or sets the sg count.
		/// </summary>
		/// <value>
		/// The sg count.
		/// </value>
		public int? SGCount { get; set; }

		/// <summary>
		/// Gets or sets the FWG count.
		/// </summary>
		/// <value>
		/// The FWG count.
		/// </value>
		public int? FWGCount { get; set; }

		/// <summary>
		/// Gets or sets the tg count.
		/// </summary>
		/// <value>
		/// The tg count.
		/// </value>
		public int? TGCount { get; set; }

		/// <summary>
		/// Gets or sets the dg count.
		/// </summary>
		/// <value>
		/// The dg count.
		/// </value>
		public int? DGCount { get; set; }

		/// <summary>
		/// Gets or sets me count.
		/// </summary>
		/// <value>
		/// Me count.
		/// </value>
		public int? MECount { get; set; }

		/// <summary>
		/// Gets or sets the average speed.
		/// </summary>
		/// <value>
		/// The average speed.
		/// </value>
		public decimal? AvgSpeed { get; set; }

		/// <summary>
		/// Gets or sets the LNG consumption.
		/// </summary>
		/// <value>
		/// The LNG consumption.
		/// </value>
		public decimal? LNGConsumption { get; set; }

		/// <summary>
		/// Gets or sets the go consumption.
		/// </summary>
		/// <value>
		/// The go consumption.
		/// </value>
		public decimal? GOConsumption { get; set; }

		/// <summary>
		/// Gets or sets the do consumption.
		/// </summary>
		/// <value>
		/// The do consumption.
		/// </value>
		public decimal? DOConsumption { get; set; }

		/// <summary>
		/// Gets or sets the has next port alert.
		/// </summary>
		/// <value>
		/// The has next port alert.
		/// </value>
		public bool? HasNextPortAlert { get; set; }

		/// <summary>
		/// Gets or sets the lsfo consumption.
		/// </summary>
		/// <value>
		/// The lsfo consumption.
		/// </value>
		public decimal? LSFOConsumption { get; set; }

		/// <summary>
		/// Gets or sets the faop status.
		/// </summary>
		/// <value>
		/// The faop status.
		/// </value>
		public string FaopStatus { get; set; }

		/// <summary>
		/// Gets or sets the un berth status.
		/// </summary>
		/// <value>
		/// The un berth status.
		/// </value>
		public string UnBerthStatus { get; set; }

		/// <summary>
		/// Gets or sets the berth status.
		/// </summary>
		/// <value>
		/// The berth status.
		/// </value>
		public string BerthStatus { get; set; }

		/// <summary>
		/// Gets or sets the eosp status.
		/// </summary>
		/// <value>
		/// The eosp status.
		/// </value>
		public string EospStatus { get; set; }

		/// <summary>
		/// Gets or sets the faop date.
		/// </summary>
		/// <value>
		/// The faop date.
		/// </value>
		public DateTime? FaopDate { get; set; }

		/// <summary>
		/// Gets or sets the un berth date.
		/// </summary>
		/// <value>
		/// The un berth date.
		/// </value>
		public DateTime? UnBerthDate { get; set; }

		/// <summary>
		/// Gets or sets the berth date.
		/// </summary>
		/// <value>
		/// The berth date.
		/// </value>
		public DateTime? BerthDate { get; set; }

		/// <summary>
		/// Gets or sets the eosp date.
		/// </summary>
		/// <value>
		/// The eosp date.
		/// </value>
		public DateTime? EospDate { get; set; }

		/// <summary>
		/// Gets or sets the charter swell length identifier.
		/// </summary>
		/// <value>
		/// The charter swell length identifier.
		/// </value>
		public string CharterSwellLengthId { get; set; }

		/// <summary>
		/// Gets or sets the charter wind force scale.
		/// </summary>
		/// <value>
		/// The charter wind force scale.
		/// </value>
		public int? CharterWindForceScale { get; set; }

		/// <summary>
		/// Gets or sets the fo consumption.
		/// </summary>
		/// <value>
		/// The fo consumption.
		/// </value>
		public decimal? FOConsumption { get; set; }

		/// <summary>
		/// Gets or sets the weather detail.
		/// </summary>
		/// <value>
		/// The weather detail.
		/// </value>
		public List<VoyageActivityBadWeatherDetail> WeatherDetail { get; set; }

		/// <summary>
		/// The next activity id
		/// </summary>
		public string NextActivityId { get; set; }

		/// <summary>
		/// The next activity id
		/// </summary>
		public string PreviousActivityId { get; set; }
	}
}
