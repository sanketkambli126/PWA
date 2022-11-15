namespace PWAFeaturesRnd.Models.Report.Notification
{
	/// <summary>
	/// Message Category Details
	/// </summary>
	public class MessageCategoryDetails
	{
		/// <summary>
		/// Gets or sets the context payload template.
		/// </summary>
		/// <value>
		/// The context payload template.
		/// </value>
		public string ContextPayloadTemplate { get; set; }

		/// <summary>
		/// Gets or sets the category identifier.
		/// </summary>
		/// <value>
		/// The category identifier.
		/// </value>
		public int CategoryId { get; set; }

		/// <summary>
		/// Gets or sets the name of the category.
		/// </summary>
		/// <value>
		/// The name of the category.
		/// </value>
		public string CategoryName { get; set; }

		/// <summary>
		/// Gets or sets the message template.
		/// </summary>
		/// <value>
		/// The message template.
		/// </value>
		public string MessageTemplate { get; set; }
	}
}
