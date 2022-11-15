namespace PWAFeaturesRnd.Models.Report.Notification
{
	/// <summary>
	/// Channel Additional Details Response
	/// </summary>
	public class ChannelAdditionalDetailsResponse
	{
		/// <summary>
		/// Gets or sets the category identifier.
		/// </summary>
		/// <value>
		/// The category identifier.
		/// </value>
		public int? CategoryId { get; set; }

		/// <summary>
		/// Gets or sets the navigation parameters.
		/// </summary>
		/// <value>
		/// The navigation parameters.
		/// </value>
		public string NavigationParameters { get; set; }

		/// <summary>
		/// Gets or sets the context payload.
		/// </summary>
		/// <value>
		/// The context payload.
		/// </value>
		public string ContextPayload { get; set; }
	}
}
