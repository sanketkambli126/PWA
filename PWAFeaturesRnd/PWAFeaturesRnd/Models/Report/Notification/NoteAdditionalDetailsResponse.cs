namespace PWAFeaturesRnd.Models.Report.Notification
{
	/// <summary>
	/// Note Additional Detail Response
	/// </summary>
	public class NoteAdditionalDetailsResponse
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
		/// Gets or sets the context parameters.
		/// </summary>
		/// <value>
		/// The context parameters.
		/// </value>
		public string ContextParams { get; set; }
	}
}
