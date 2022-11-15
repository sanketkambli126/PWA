namespace PWAFeaturesRnd.Models.Report.Notification
{
	/// <summary>
	/// Channel Additional Details Request
	/// </summary>
	public class ChannelAdditionalDetailsRequest
	{
		/// <summary>
		/// Gets or sets the channel identifier.
		/// </summary>
		/// <value>
		/// The channel identifier.
		/// </value>
		public int ChannelId { get; set; }

		/// <summary>
		/// Gets or sets the application identifier.
		/// </summary>
		/// <value>
		/// The application identifier.
		/// </value>
		public int ApplicationId { get; set; }
	}
}
