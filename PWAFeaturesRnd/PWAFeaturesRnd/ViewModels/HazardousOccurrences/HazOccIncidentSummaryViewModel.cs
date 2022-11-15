using System;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    public class HazOccIncidentSummaryViewModel
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
		public string ReportDate { get; set; }

		/// <summary>
		/// Gets or sets the updated date.
		/// </summary>
		/// <value>
		/// The updated date.
		/// </value>
		public string UpdatedDate { get; set; }

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
		public string IncidentDateTime { get; set; }

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
		/// Gets or sets the parent report.
		/// </summary>
		/// <value>
		/// The parent report.
		/// </value>
		public string ParentReport { get; set; }

		/// <summary>
		/// Gets or sets the classification identifier.
		/// </summary>
		/// <value>
		/// The classification identifier.
		/// </value>
		public string ClassificationId { get; set; }

        /// <summary>
        /// Gets or sets the classification description.
        /// </summary>
        /// <value>
        /// The classification description.
        /// </value>
        public string ClassificationDescription { get; set; }

        /// <summary>
        /// Gets or sets the classification.
        /// </summary>
        /// <value>
        /// The classification.
        /// </value>
        public string Classification { get; set; }

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
		/// Gets or sets the ship location.
		/// </summary>
		/// <value>
		/// The ship location.
		/// </value>
		public string ShipLocation { get; set; }

		/// <summary>
		/// Gets or sets the other ship location.
		/// </summary>
		/// <value>
		/// The other ship location.
		/// </value>
		public string OtherShipLocation { get; set; }

		/// <summary>
		/// Gets or sets the vessel location.
		/// </summary>
		/// <value>
		/// The vessel location.
		/// </value>
		public string VesselLocation { get; set; }

		/// <summary>
		/// Gets or sets the manoeuvring.
		/// </summary>
		/// <value>
		/// The manoeuvring.
		/// </value>
		public string Manoeuvring { get; set; }

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
		public string LatitudeDegrees { get; set; }

		/// <summary>
		/// Gets or sets the long degrees.
		/// </summary>
		/// <value>
		/// The long degrees.
		/// </value>
		public string LongDegrees { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is day light.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is day light; otherwise, <c>false</c>.
		/// </value>
		public string Light { get; set; }

		/// <summary>
		/// Gets or sets the is ashore.
		/// </summary>
		/// <value>
		/// The is ashore.
		/// </value>
		public string Where { get; set; }

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
		/// Gets or sets the collision stationary.
		/// </summary>
		/// <value>
		/// The collision stationary.
		/// </value>
		public string CollisionStationary { get; set; }

		/// <summary>
		/// Gets or sets the closed date.
		/// </summary>
		/// <value>
		/// The closed date.
		/// </value>
		public string ClosedDate { get; set; }

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
		public string MsqCommentDate { get; set; }

		/// <summary>
		/// Gets or sets the fleet manager comment date.
		/// </summary>
		/// <value>
		/// The fleet manager comment date.
		/// </value>
		public string FleetManagerCommentDate { get; set; }

		/// <summary>
		/// Gets or sets the ship location label.
		/// </summary>
		/// <value>
		/// The ship location label.
		/// </value>
		public string ShipLocationLabel { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show port].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show port]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowPort { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show maneuvering].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show maneuvering]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowManeuvering { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show location name].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show location name]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowLocationName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show long lat].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show long lat]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowLongLat { get; set; }

		/// <summary>
		/// Gets or sets the location lookup label.
		/// </summary>
		/// <value>
		/// The location lookup label.
		/// </value>
		public string LocationLookupLabel { get; set; }

		/// <summary>
		/// Gets or sets the is equipment failure.
		/// </summary>
		/// <value>
		/// The is equipment failure.
		/// </value>
		public string IsEquipmentFailure { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show collision stationary].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show collision stationary]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowCollisionStationary { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is closure comment visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is closure comment visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsClosureCommentVisible { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is fleet manager comments visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is fleet manager comments visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsFleetManagerCommentsVisible { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show other ship location].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show other ship location]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowOtherShipLocation { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is hseq manager comments visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is hseq manager comments visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsHSEQManagerCommentsVisible { get; set; }

		/// <summary>
		/// Gets or sets the closure comments.
		/// </summary>
		/// <value>
		/// The closure comments.
		/// </value>
		public string ClosureComments { get; set; }
	}
}
