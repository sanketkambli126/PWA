namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// 
    /// </summary>
    public class VoyageDetails
	{
        /// <summary>
        /// Gets or sets the id of the vessel.
        /// </summary>
        /// <value>
        /// The id of the vessel.
        /// </value>
        public string VesselId { get; set; }

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
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>
        public decimal TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the distance travelled.
        /// </summary>
        /// <value>
        /// The distance travelled.
        /// </value>
        public decimal DistanceTravelled { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string POS_ID { get; set; }

        /// <summary>
        /// Gets or sets the pla identifier.
        /// </summary>
        /// <value>
        /// The pla identifier.
        /// </value>
        public string PLA_ID { get; set; }

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