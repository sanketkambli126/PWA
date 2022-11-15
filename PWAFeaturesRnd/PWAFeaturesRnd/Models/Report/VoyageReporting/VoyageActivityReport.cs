using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Voyage Activity Report
	/// </summary>
	public class VoyageActivityReport
	{
		/// <summary>
		/// Gets or sets the bad weather date.
		/// </summary>
		/// <value>
		/// The bad weather date.
		/// </value>
		public DateTime? BadWeatherDate { get; set; }

		/// <summary>
		/// Gets or sets the CHT identifier.
		/// </summary>
		/// <value>
		/// The CHT identifier.
		/// </value>
		public string ChtId { get; set; }

		/// <summary>
		/// Gets or sets the type of the trade.
		/// </summary>
		/// <value>
		/// The type of the trade.
		/// </value>
		public string TradeType { get; set; }

		/// <summary>
		/// Gets or sets the maximum sea passage swell length description.
		/// </summary>
		/// <value>
		/// The maximum sea passage swell length description.
		/// </value>
		public string MaxSeaPassageSwellLengthDescription { get; set; }

		/// <summary>
		/// Gets or sets the maximum length of the sea passage swell.
		/// </summary>
		/// <value>
		/// The maximum length of the sea passage swell.
		/// </value>
		public string MaxSeaPassageSwellLength { get; set; }

		/// <summary>
		/// Gets or sets the charter swell length description.
		/// </summary>
		/// <value>
		/// The charter swell length description.
		/// </value>
		public string CharterSwellLengthDescription { get; set; }

		/// <summary>
		/// Gets or sets the length of the charter swell.
		/// </summary>
		/// <value>
		/// The length of the charter swell.
		/// </value>
		public string CharterSwellLength { get; set; }

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
		/// Gets or sets the pla identifier.
		/// </summary>
		/// <value>
		/// The pla identifier.
		/// </value>
		public string PlaId { get; set; }

		/// <summary>
		/// Gets or sets the event codes.
		/// </summary>
		/// <value>
		/// The event codes.
		/// </value>
		public List<string> EventCodes { get; set; }

		/// <summary>
		/// Gets or sets the event dates.
		/// </summary>
		/// <value>
		/// The event dates.
		/// </value>
		public List<DateTime?> EventDates { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is editable.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is editable; otherwise, <c>false</c>.
		/// </value>
		public bool IsEditable { get; set; }

		/// <summary>
		/// Gets or sets the port event to date.
		/// </summary>
		/// <value>
		/// The port event to date.
		/// </value>
		public DateTime? PortEventToDate { get; set; }

		/// <summary>
		/// Gets or sets the activity description.
		/// </summary>
		/// <value>
		/// The activity description.
		/// </value>
		public string ActivityDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is delay.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is delay; otherwise, <c>false</c>.
		/// </value>
		public bool IsDelay { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is off hire.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
		/// </value>
		public bool IsOffHire { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is bad weather.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is bad weather; otherwise, <c>false</c>.
		/// </value>
		public bool IsBadWeather { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has port agent.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has port agent; otherwise, <c>false</c>.
		/// </value>
		public bool HasPortAgent { get; set; }

		/// <summary>
		/// Converts to portid.
		/// </summary>
		/// <value>
		/// To port identifier.
		/// </value>
		public string ToPortId { get; set; }

		/// <summary>
		/// Converts to portservicemapped.
		/// </summary>
		/// <value>
		/// To port service mapped.
		/// </value>
		public bool? ToPortServiceMapped { get; set; }

		/// <summary>
		/// Converts to portalertadded.
		/// </summary>
		/// <value>
		/// To port alert added.
		/// </value>
		public bool? ToPortAlertAdded { get; set; }

		/// <summary>
		/// Gets or sets the port service mapped.
		/// </summary>
		/// <value>
		/// The port service mapped.
		/// </value>
		public bool? PortServiceMapped { get; set; }

		/// <summary>
		/// Gets or sets the port alert added.
		/// </summary>
		/// <value>
		/// The port alert added.
		/// </value>
		public bool? PortAlertAdded { get; set; }

		/// <summary>
		/// Gets or sets from port identifier.
		/// </summary>
		/// <value>
		/// From port identifier.
		/// </value>
		public string FromPortId { get; set; }

		/// <summary>
		/// Gets or sets the agent email.
		/// </summary>
		/// <value>
		/// The agent email.
		/// </value>
		public string AgentEmail { get; set; }

		/// <summary>
		/// Gets or sets the agent telephone.
		/// </summary>
		/// <value>
		/// The agent telephone.
		/// </value>
		public string AgentTelephone { get; set; }

		/// <summary>
		/// Gets or sets the last report event date.
		/// </summary>
		/// <value>
		/// The last report event date.
		/// </value>
		public DateTime? LastReportEventDate { get; set; }

		/// <summary>
		/// Gets or sets the agent address.
		/// </summary>
		/// <value>
		/// The agent address.
		/// </value>
		public string AgentAddress { get; set; }

		/// <summary>
		/// Gets or sets the date1.
		/// </summary>
		/// <value>
		/// The date1.
		/// </value>
		public DateTime? Date1 { get; set; }

		/// <summary>
		/// Gets or sets the date2.
		/// </summary>
		/// <value>
		/// The date2.
		/// </value>
		public DateTime? Date2 { get; set; }

		/// <summary>
		/// Gets or sets the agent3 status.
		/// </summary>
		/// <value>
		/// The agent3 status.
		/// </value>
		public string Agent3Status { get; set; }

		/// <summary>
		/// Gets or sets the agent3 identifier.
		/// </summary>
		/// <value>
		/// The agent3 identifier.
		/// </value>
		public string Agent3Id { get; set; }

		/// <summary>
		/// Gets or sets the agent2 status.
		/// </summary>
		/// <value>
		/// The agent2 status.
		/// </value>
		public string Agent2Status { get; set; }

		/// <summary>
		/// Gets or sets the agent2 identifier.
		/// </summary>
		/// <value>
		/// The agent2 identifier.
		/// </value>
		public string Agent2Id { get; set; }

		/// <summary>
		/// Gets or sets the agent1 status.
		/// </summary>
		/// <value>
		/// The agent1 status.
		/// </value>
		public string Agent1Status { get; set; }

		/// <summary>
		/// Gets or sets the agent1 identifier.
		/// </summary>
		/// <value>
		/// The agent1 identifier.
		/// </value>
		public string Agent1Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the agent.
		/// </summary>
		/// <value>
		/// The name of the agent.
		/// </value>
		public string AgentName { get; set; }

		/// <summary>
		/// Gets or sets the total LNG.
		/// </summary>
		/// <value>
		/// The total LNG.
		/// </value>
		public float TotalLng { get; set; }

		/// <summary>
		/// Gets or sets the total fresh water technical.
		/// </summary>
		/// <value>
		/// The total fresh water technical.
		/// </value>
		public float TotalFreshWaterTechnical { get; set; }

		/// <summary>
		/// Gets or sets the total fresh water domestic.
		/// </summary>
		/// <value>
		/// The total fresh water domestic.
		/// </value>
		public float TotalFreshWaterDomestic { get; set; }

		/// <summary>
		/// Gets or sets the speed.
		/// </summary>
		/// <value>
		/// The speed.
		/// </value>
		public float? Speed { get; set; }

		/// <summary>
		/// Gets or sets the sea postion activity identifier.
		/// </summary>
		/// <value>
		/// The sea postion activity identifier.
		/// </value>
		public string SeaPostionActivityId { get; set; }

		/// <summary>
		/// Gets or sets the RPM.
		/// </summary>
		/// <value>
		/// The RPM.
		/// </value>
		public float? Rpm { get; set; }

		/// <summary>
		/// Gets or sets the activity identifier.
		/// </summary>
		/// <value>
		/// The activity identifier.
		/// </value>
		public string ActivityId { get; set; }

		/// <summary>
		/// Gets or sets the type of the sea passage activity.
		/// </summary>
		/// <value>
		/// The type of the sea passage activity.
		/// </value>
		public string SeaPassageActivityType { get; set; }

		/// <summary>
		/// Gets or sets the lsfo.
		/// </summary>
		/// <value>
		/// The lsfo.
		/// </value>
		public float? Lsfo { get; set; }

		/// <summary>
		/// Gets or sets the liquefied natural gas.
		/// </summary>
		/// <value>
		/// The liquefied natural gas.
		/// </value>
		public float LiquefiedNaturalGas { get; set; }

		/// <summary>
		/// Gets or sets the go.
		/// </summary>
		/// <value>
		/// The go.
		/// </value>
		public float? Go { get; set; }

		/// <summary>
		/// Gets or sets the breaks.
		/// </summary>
		/// <value>
		/// The breaks.
		/// </value>
		public int Breaks { get; set; }

		/// <summary>
		/// Gets or sets the fresh water consumption technial.
		/// </summary>
		/// <value>
		/// The fresh water consumption technial.
		/// </value>
		public float FreshWaterConsumptionTechnial { get; set; }

		/// <summary>
		/// Gets or sets the fo.
		/// </summary>
		/// <value>
		/// The fo.
		/// </value>
		public float? Fo { get; set; }

		/// <summary>
		/// Gets or sets the do.
		/// </summary>
		/// <value>
		/// The do.
		/// </value>
		public float? Do { get; set; }

		/// <summary>
		/// Gets or sets the distance to go.
		/// </summary>
		/// <value>
		/// The distance to go.
		/// </value>
		public float? DistanceToGo { get; set; }

		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>
		/// The distance.
		/// </value>
		public float? Distance { get; set; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>
		/// The date.
		/// </value>
		public DateTime? Date { get; set; }

		/// <summary>
		/// Gets or sets the course.
		/// </summary>
		/// <value>
		/// The course.
		/// </value>
		public float? Course { get; set; }

		/// <summary>
		/// Gets or sets the charter.
		/// </summary>
		/// <value>
		/// The charter.
		/// </value>
		public string Charter { get; set; }

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>
		/// The position.
		/// </value>
		public string Position { get; set; }

		/// <summary>
		/// Gets or sets the fresh water consumption domestic.
		/// </summary>
		/// <value>
		/// The fresh water consumption domestic.
		/// </value>
		public float FreshWaterConsumptionDomestic { get; set; }

		/// <summary>
		/// Gets or sets the port agent mapped.
		/// </summary>
		/// <value>
		/// The port agent mapped.
		/// </value>
		public bool? PortAgentMapped { get; set; }

		/// <summary>
		/// Gets or sets the time sailed.
		/// </summary>
		/// <value>
		/// The time sailed.
		/// </value>
		public string TimeSailed { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the total go.
		/// </summary>
		/// <value>
		/// The total go.
		/// </value>
		public float TotalGo { get; set; }

		/// <summary>
		/// Gets or sets the total do.
		/// </summary>
		/// <value>
		/// The total do.
		/// </value>
		public float TotalDo { get; set; }

		/// <summary>
		/// Gets or sets the total lsfo.
		/// </summary>
		/// <value>
		/// The total lsfo.
		/// </value>
		public float TotalLsfo { get; set; }

		/// <summary>
		/// Gets or sets the total fo.
		/// </summary>
		/// <value>
		/// The total fo.
		/// </value>
		public float TotalFo { get; set; }

		/// <summary>
		/// Gets or sets the average speed.
		/// </summary>
		/// <value>
		/// The average speed.
		/// </value>
		public float AverageSpeed { get; set; }

		/// <summary>
		/// Gets or sets the total distance.
		/// </summary>
		/// <value>
		/// The total distance.
		/// </value>
		public float TotalDistance { get; set; }

		/// <summary>
		/// Gets or sets the total time formatted.
		/// </summary>
		/// <value>
		/// The total time formatted.
		/// </value>
		public string TotalTimeFormatted { get; set; }

		/// <summary>
		/// Gets or sets the office voyage number.
		/// </summary>
		/// <value>
		/// The office voyage number.
		/// </value>
		public string OfficeVoyageNumber { get; set; }

		/// <summary>
		/// Gets or sets the true slip.
		/// </summary>
		/// <value>
		/// The true slip.
		/// </value>
		public float? TrueSlip { get; set; }

		/// <summary>
		/// Gets or sets the ship voyage number.
		/// </summary>
		/// <value>
		/// The ship voyage number.
		/// </value>
		public string ShipVoyageNumber { get; set; }

		/// <summary>
		/// Gets or sets the sea passage activity description.
		/// </summary>
		/// <value>
		/// The sea passage activity description.
		/// </value>
		public string SeaPassageActivityDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is vessel loaded flag.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is vessel loaded flag; otherwise, <c>false</c>.
		/// </value>
		public bool IsVesselLoadedFlag { get; set; }

		/// <summary>
		/// Converts to portcountrycode.
		/// </summary>
		/// <value>
		/// To port country code.
		/// </value>
		public string ToPortCountryCode { get; set; }

		/// <summary>
		/// Converts to portname.
		/// </summary>
		/// <value>
		/// The name of to port.
		/// </value>
		public string ToPortName { get; set; }

		/// <summary>
		/// Gets or sets from port country code.
		/// </summary>
		/// <value>
		/// From port country code.
		/// </value>
		public string FromPortCountryCode { get; set; }

		/// <summary>
		/// Gets or sets the name of from port.
		/// </summary>
		/// <value>
		/// The name of from port.
		/// </value>
		public string FromPortName { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is sea passage event.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is sea passage event; otherwise, <c>false</c>.
		/// </value>
		public bool IsSeaPassageEvent { get; set; }

		/// <summary>
		/// Gets or sets the events.
		/// </summary>
		/// <value>
		/// The events.
		/// </value>
		public List<PositionListPreviewEvent> Events { get; set; }

		/// <summary>
		/// Gets or sets the company port mapped.
		/// </summary>
		/// <value>
		/// The company port mapped.
		/// </value>
		public bool? CompanyPortMapped { get; set; }

		#region FromPortDetails

		/// <summary>
		/// Gets or sets the name of the country.
		/// </summary>
		/// <value>
		/// The name of the country.
		/// </value>
		public string FromPortCountryName { get; set; }

		/// <summary>
		/// Gets or sets the un locode.
		/// </summary>
		/// <value>
		/// The un locode.
		/// </value>
		public string FromPortUNLocode { get; set; }

		/// <summary>
		/// Gets or sets the lat degree.
		/// </summary>
		/// <value>
		/// The lat degree.
		/// </value>
		public decimal? FromPortLatDegree { get; set; }

		/// <summary>
		/// Gets or sets the lat indicator.
		/// </summary>
		/// <value>
		/// The lat indicator.
		/// </value>
		public string FromPortLatIndicator { get; set; }

		/// <summary>
		/// Gets or sets the lat minimum.
		/// </summary>
		/// <value>
		/// The lat minimum.
		/// </value>
		public decimal? FromPortLatMin { get; set; }

		/// <summary>
		/// Gets or sets the long degree.
		/// </summary>
		/// <value>
		/// The long degree.
		/// </value>
		public decimal? FromPortLongDegree { get; set; }

		/// <summary>
		/// Gets or sets the long indicator.
		/// </summary>
		/// <value>
		/// The long indicator.
		/// </value>
		public string FromPortLongIndicator { get; set; }

		/// <summary>
		/// Gets or sets the long minimum.
		/// </summary>
		/// <value>
		/// The long minimum.
		/// </value>
		public decimal? FromPortLongMin { get; set; }

		/// <summary>
		/// Gets or sets the is key hub port.
		/// </summary>
		/// <value>
		/// The is key hub port.
		/// </value>
		public bool? FromPortIsKeyHubPort { get; set; }

		#endregion

		#region ToPortDetail

		/// <summary>
		/// Gets or sets the name of the country.
		/// </summary>
		/// <value>
		/// The name of the country.
		/// </value>
		public string ToPortCountryName { get; set; }

		/// <summary>
		/// Gets or sets the un locode.
		/// </summary>
		/// <value>
		/// The un locode.
		/// </value>
		public string ToPortUNLocode { get; set; }

		/// <summary>
		/// Gets or sets the lat degree.
		/// </summary>
		/// <value>
		/// The lat degree.
		/// </value>
		public decimal? ToPortLatDegree { get; set; }

		/// <summary>
		/// Gets or sets the lat indicator.
		/// </summary>
		/// <value>
		/// The lat indicator.
		/// </value>
		public string ToPortLatIndicator { get; set; }

		/// <summary>
		/// Gets or sets the lat minimum.
		/// </summary>
		/// <value>
		/// The lat minimum.
		/// </value>
		public decimal? ToPortLatMin { get; set; }

		/// <summary>
		/// Gets or sets the long degree.
		/// </summary>
		/// <value>
		/// The long degree.
		/// </value>
		public decimal? ToPortLongDegree { get; set; }

		/// <summary>
		/// Gets or sets the long indicator.
		/// </summary>
		/// <value>
		/// The long indicator.
		/// </value>
		public string ToPortLongIndicator { get; set; }

		/// <summary>
		/// Gets or sets the long minimum.
		/// </summary>
		/// <value>
		/// The long minimum.
		/// </value>
		public decimal? ToPortLongMin { get; set; }

		/// <summary>
		/// Gets or sets the is key hub port.
		/// </summary>
		/// <value>
		/// The is key hub port.
		/// </value>
		public bool? ToPortIsKeyHubPort { get; set; }

		#endregion


		/// <summary>
		/// Gets or sets the name of the agent2.
		/// </summary>
		/// <value>
		/// The name of the agent2.
		/// </value>
		public string Agent2Name { get; set; }

		/// <summary>
		/// Gets or sets the name of the agent3.
		/// </summary>
		/// <value>
		/// The name of the agent3.
		/// </value>
		public string Agent3Name { get; set; }
	}
}
