namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// EventXMLDetails
    /// </summary>
    public class EventXMLDetailsViewModel
    {


        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>
        /// The type of the event.
        /// </value>
        public string EventType { get; set; }


        /// <summary>
        /// Gets or sets the voyage.
        /// </summary>
        /// <value>
        /// The voyage.
        /// </value>
        public string Voyage { get; set; }


        /// <summary>
        /// Gets or sets the operation mode.
        /// </summary>
        /// <value>
        /// The operation mode.
        /// </value>
        public string OperationMode { get; set; }


        /// <summary>
        /// Gets or sets the charter.
        /// </summary>
        /// <value>
        /// The charter.
        /// </value>
        public string Charter { get; set; }


        /// <summary>
        /// Gets or sets the event date.
        /// </summary>
        /// <value>
        /// The event date.
        /// </value>
        public string EventDate { get; set; }

        /// <summary>
        /// Gets or sets the external integration event data.
        /// </summary>
        /// <value>
        /// The external integration event data.
        /// </value>
        public string ExternalIntegrationEventData { get; set; }
    }
}
