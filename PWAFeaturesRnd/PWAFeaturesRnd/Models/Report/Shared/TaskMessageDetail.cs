using System;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// Task Message Detail
	/// </summary>
	public class TaskMessageDetail
	{
		/// <summary>
		/// Gets or sets the task message identifier.
		/// </summary>
		/// <value>
		/// The task message identifier.
		/// </value>
		public Guid TaskMessageId { get; set; }

		/// <summary>
		/// Gets or sets the content of the message.
		/// </summary>
		/// <value>
		/// The content of the message.
		/// </value>
		public string MessageContent { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public string UserId { get; set; }

		/// <summary>
		/// Gets or sets the message date.
		/// </summary>
		/// <value>
		/// The message date.
		/// </value>
		public DateTime MessageDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TaskMessageDetail"/> is success.
		/// </summary>
		/// <value>
		///   <c>true</c> if success; otherwise, <c>false</c>.
		/// </value>
		public bool Success { get; set; }

		/// <summary>
		/// Gets or sets the exception detail.
		/// </summary>
		/// <value>
		/// The exception detail.
		/// </value>
		public string ExceptionDetail { get; set; }

		/// <summary>
		/// Gets or sets the rowguid.
		/// </summary>
		/// <value>
		/// The rowguid.
		/// </value>
		public Guid Rowguid { get; set; }

		/// <summary>
		/// Gets or sets the generated filename.
		/// </summary>
		/// <value>
		/// The generated filename.
		/// </value>
		public string GeneratedFilename { get; set; }

		/// <summary>
		/// Gets or sets the name of the friendly file.
		/// </summary>
		/// <value>
		/// The name of the friendly file.
		/// </value>
		public string FriendlyFileName { get; set; }

		/// <summary>
		/// Gets or sets the ull identifier.
		/// </summary>
		/// <value>
		/// The ull identifier.
		/// </value>
		public string UllId { get; set; }
	}
}
