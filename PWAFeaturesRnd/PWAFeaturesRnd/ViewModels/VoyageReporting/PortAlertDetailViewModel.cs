namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// PortAlertDetailViewModel
	/// </summary>
	public class PortAlertDetailViewModel
	{
		/// <summary>
		/// Gets or sets the document request URL.
		/// </summary>
		/// <value>
		/// The document request URL.
		/// </value>
		public string DocumentRequestUrl { get; set; }

		/// <summary>
		/// Gets or sets the PRT identifier.
		/// </summary>
		/// <value>
		/// The PRT identifier.
		/// </value>
		public string PrtId { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is acknowledged.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is acknowledged; otherwise, <c>false</c>.
		/// </value>
		public bool IsAcknowledged { get; set; }

		/// <summary>
		/// Gets or sets the acknowledge date.
		/// </summary>
		/// <value>
		/// The acknowledge date.
		/// </value>
		public string AcknowledgeDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the acknowledge user.
		/// </summary>
		/// <value>
		/// The name of the acknowledge user.
		/// </value>
		public string AcknowledgeUserName { get; set; }

		/// <summary>
		/// Gets or sets the acknowledge user rank.
		/// </summary>
		/// <value>
		/// The acknowledge user rank.
		/// </value>
		public string AcknowledgeUserRank { get; set; }
	}
}
