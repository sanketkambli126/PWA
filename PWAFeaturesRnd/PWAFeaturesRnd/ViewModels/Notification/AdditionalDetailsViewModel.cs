using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Notification
{
	/// <summary>
	/// Additional Details View Model
	/// </summary>
	public class AdditionalDetailsViewModel
	{
		/// <summary>
		/// Gets or sets the URL details.
		/// </summary>
		/// <value>
		/// The URL details.
		/// </value>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the context parameters details.
		/// </summary>
		/// <value>
		/// The context parameters details.
		/// </value>
		public List<KeyValuePair<string, string>> ContextParametersDetails { get; set; }
	}
}
