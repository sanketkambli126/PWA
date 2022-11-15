using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
	public class HazOccIncidentSummary
    {
		/// <summary>
		/// Gets or sets the imr identifier.
		/// </summary>
		/// <value>
		/// The imr identifier.
		/// </value>
		public string ImrId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the report date.
		/// </summary>
		/// <value>
		/// The report date.
		/// </value>
		public DateTime? ReportDate { get; set; }

		/// <summary>
		/// Gets or sets the updated date.
		/// </summary>
		/// <value>
		/// The updated date.
		/// </value>
		public DateTime? UpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the created by.
		/// </summary>
		/// <value>
		/// The created by.
		/// </value>
		public string CreatedBy { get; set; }

		/// <summary>
		/// Gets or sets the incident date time.
		/// </summary>
		/// <value>
		/// The incident date time.
		/// </value>
		public DateTime? IncidentDateTime { get; set; }

		/// <summary>
		/// Gets or sets the safety officer.
		/// </summary>
		/// <value>
		/// The safety officer.
		/// </value>
		public string SafetyOfficer { get; set; }

		/// <summary>
		/// Gets or sets the parent report identifier.
		/// </summary>
		/// <value>
		/// The parent report identifier.
		/// </value>
		public string ParentReportId { get; set; }

		/// <summary>
		/// Gets or sets the classification identifier.
		/// </summary>
		/// <value>
		/// The classification identifier.
		/// </value>
		public string ClassificationId { get; set; }

		/// <summary>
		/// Gets or sets the classification.
		/// </summary>
		/// <value>
		/// The classification.
		/// </value>
		public string Classification { get; set; }

        /// <summary>
        /// Gets or sets the classification description.
        /// </summary>
        /// <value>
        /// The classification description.
        /// </value>
        public string ClassificationDescription { get; set; }

        /// <summary>
        /// Gets or sets the name of the master.
        /// </summary>
        /// <value>
        /// The name of the master.
        /// </value>
        public string MasterName { get; set; }

		/// <summary>
		/// Gets or sets the ship reference no.
		/// </summary>
		/// <value>
		/// The ship reference no.
		/// </value>
		public string ShipRefNo { get; set; }

		/// <summary>
		/// Gets or sets the potential severity identifier.
		/// </summary>
		/// <value>
		/// The potential severity identifier.
		/// </value>
		public string PotentialSeverityId { get; set; }

		/// <summary>
		/// Gets or sets the actual severity identifier.
		/// </summary>
		/// <value>
		/// The actual severity identifier.
		/// </value>
		public string ActualSeverityId { get; set; }

		/// <summary>
		/// Gets or sets the actual severity.
		/// </summary>
		/// <value>
		/// The actual severity.
		/// </value>
		public string ActualSeverity { get; set; }

		/// <summary>
		/// Gets or sets the ship location identifier.
		/// </summary>
		/// <value>
		/// The ship location identifier.
		/// </value>
		public string ShipLocationId { get; set; }

		/// <summary>
		/// Gets or sets the other ship location.
		/// </summary>
		/// <value>
		/// The other ship location.
		/// </value>
		public string OtherShipLocation { get; set; }

		/// <summary>
		/// Gets or sets the vessel location identifier.
		/// </summary>
		/// <value>
		/// The vessel location identifier.
		/// </value>
		public string VesselLocationId { get; set; }

		/// <summary>
		/// Gets or sets the manoeuvring identifier.
		/// </summary>
		/// <value>
		/// The manoeuvring identifier.
		/// </value>
		public string ManoeuvringId { get; set; }

		/// <summary>
		/// Gets or sets the port identifier.
		/// </summary>
		/// <value>
		/// The port identifier.
		/// </value>
		public string PortId { get; set; }

		/// <summary>
		/// Gets or sets the name of the port.
		/// </summary>
		/// <value>
		/// The name of the port.
		/// </value>
		public string PortName { get; set; }

		/// <summary>
		/// Gets or sets the country identifier.
		/// </summary>
		/// <value>
		/// The country identifier.
		/// </value>
		public string CountryId { get; set; }

		/// <summary>
		/// Gets or sets the name of the dock.
		/// </summary>
		/// <value>
		/// The name of the dock.
		/// </value>
		public string DockName { get; set; }

		/// <summary>
		/// Gets or sets the name of the location.
		/// </summary>
		/// <value>
		/// The name of the location.
		/// </value>
		public string LocationName { get; set; }

		/// <summary>
		/// Gets or sets the latitude degrees.
		/// </summary>
		/// <value>
		/// The latitude degrees.
		/// </value>
		public short? LatitudeDegrees { get; set; }

		/// <summary>
		/// Gets or sets the latitude minimum.
		/// </summary>
		/// <value>
		/// The latitude minimum.
		/// </value>
		public short? LatitudeMin { get; set; }

		/// <summary>
		/// Gets or sets the latitude ind.
		/// </summary>
		/// <value>
		/// The latitude ind.
		/// </value>
		public string LatitudeInd { get; set; }

		/// <summary>
		/// Gets or sets the long degrees.
		/// </summary>
		/// <value>
		/// The long degrees.
		/// </value>
		public short? LongDegrees { get; set; }

		/// <summary>
		/// Gets or sets the long minimum.
		/// </summary>
		/// <value>
		/// The long minimum.
		/// </value>
		public short? LongMin { get; set; }

		/// <summary>
		/// Gets or sets the long ind.
		/// </summary>
		/// <value>
		/// The long ind.
		/// </value>
		public string LongInd { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is day light.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is day light; otherwise, <c>false</c>.
		/// </value>
		public bool IsDayLight { get; set; }

		/// <summary>
		/// Gets or sets the is ashore.
		/// </summary>
		/// <value>
		/// The is ashore.
		/// </value>
		public bool IsAshore { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the type of the equipment.
		/// </summary>
		/// <value>
		/// The type of the equipment.
		/// </value>
		public string EquipmentType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [collision stationary].
		/// </summary>
		/// <value>
		///   <c>true</c> if [collision stationary]; otherwise, <c>false</c>.
		/// </value>
		public bool CollisionStationary { get; set; }

		/// <summary>
		/// Gets or sets the closed date.
		/// </summary>
		/// <value>
		/// The closed date.
		/// </value>
		public DateTime? ClosedDate { get; set; }

		/// <summary>
		/// Gets or sets the MSQ comments.
		/// </summary>
		/// <value>
		/// The MSQ comments.
		/// </value>
		public string MsqComments { get; set; }

		/// <summary>
		/// Gets or sets the fleet manager comments.
		/// </summary>
		/// <value>
		/// The fleet manager comments.
		/// </value>
		public string FleetManagerComments { get; set; }

		/// <summary>
		/// Gets or sets the report type identifier.
		/// </summary>
		/// <value>
		/// The report type identifier.
		/// </value>
		public string ReportTypeId { get; set; }

		/// <summary>
		/// Gets or sets the MSQ comment date.
		/// </summary>
		/// <value>
		/// The MSQ comment date.
		/// </value>
		public DateTime? MsqCommentDate { get; set; }

		/// <summary>
		/// Gets or sets the fleet manager comment date.
		/// </summary>
		/// <value>
		/// The fleet manager comment date.
		/// </value>
		public DateTime? FleetManagerCommentDate { get; set; }

		/// <summary>
		/// Gets or sets the is equipment failure.
		/// </summary>
		/// <value>
		/// The is equipment failure.
		/// </value>
		public bool? IsEquipmentFailure { get; set; }

		/// <summary>
		/// Gets or sets the imd equip required.
		/// </summary>
		/// <value>
		/// The imd equip required.
		/// </value>
		public bool? ImdEquipRequired { get; set; }

		/// <summary>
		/// Gets or sets the ship location.
		/// </summary>
		/// <value>
		/// The ship location.
		/// </value>
		public string ShipLocation { get; set; }
	}
}
