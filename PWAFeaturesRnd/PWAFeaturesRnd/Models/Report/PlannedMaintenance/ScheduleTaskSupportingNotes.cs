using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// ScheduleTaskSupportingNotes
	/// </summary>
	public class ScheduleTaskSupportingNotes
	{
		/// <summary>
		/// Gets or sets the PSN identifier.
		/// </summary>
		/// <value>
		/// The PSN identifier.
		/// </value>
		public string PsnId { get; set; }

		/// <summary>
		/// Gets or sets the PST identifier.
		/// </summary>
		/// <value>
		/// The PST identifier.
		/// </value>
		public string PstId { get; set; }

		/// <summary>
		/// Gets or sets the note text.
		/// </summary>
		/// <value>
		/// The note text.
		/// </value>
		public string NoteText { get; set; }

		/// <summary>
		/// Gets or sets the created by.
		/// </summary>
		/// <value>
		/// The created by.
		/// </value>
		public string CreatedBy { get; set; }

		/// <summary>
		/// Gets or sets the created date.
		/// </summary>
		/// <value>
		/// The created date.
		/// </value>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }
	}
}
