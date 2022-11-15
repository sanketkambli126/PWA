namespace PWAFeaturesRnd.Models.Report.Notification
{
	/// <summary>
	/// Note Additional Detail Request
	/// </summary>
	public class NoteAdditionalDetailsRequest
	{
		/// <summary>
		/// Gets or sets the note identifier.
		/// </summary>
		/// <value>
		/// The note identifier.
		/// </value>
		public int NoteId { get; set; }

		/// <summary>
		/// Gets or sets the application identifier.
		/// </summary>
		/// <value>
		/// The application identifier.
		/// </value>
		public int ApplicationId { get; set; }
	}
}
