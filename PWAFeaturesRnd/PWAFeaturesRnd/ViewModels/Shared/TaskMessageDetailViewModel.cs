using System;

namespace PWAFeaturesRnd.ViewModels.Shared
{
	/// <summary>
	/// Task Message Detail View Model
	/// </summary>
	public class TaskMessageDetailViewModel
	{
		/// <summary>
		/// Gets or sets the content of the message.
		/// </summary>
		/// <value>
		/// The content of the message.
		/// </value>
		public string MessageContent { get; set; }

		/// <summary>
		/// Gets or sets the message date.
		/// </summary>
		/// <value>
		/// The message date.
		/// </value>
		public DateTime MessageDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TaskMessageDetail" /> is success.
		/// </summary>
		/// <value>
		///   <c>true</c> if success; otherwise, <c>false</c>.
		/// </value>
		public bool Success { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is file.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is file; otherwise, <c>false</c>.
		/// </value>
		public bool IsFile { get; set; }

		/// <summary>
		/// Gets or sets the download document URL.
		/// </summary>
		/// <value>
		/// The download document URL.
		/// </value>
		public string DownloadDocumentUrl { get; set; }
	}
}
