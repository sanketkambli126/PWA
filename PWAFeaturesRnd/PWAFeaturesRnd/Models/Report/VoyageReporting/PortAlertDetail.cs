using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Port Alert Detail
	/// </summary>
	public class PortAlertDetail
	{
		/// <summary>
		/// Gets or sets the pat identifier.
		/// </summary>
		/// <value>
		/// The pat identifier.
		/// </value>
		public string PatId { get; set; }

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
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the row identifier.
		/// </summary>
		/// <value>
		/// The row identifier.
		/// </value>
		public Guid RowIdentifier { get; set; }

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
		public DateTime? AcknowledgeDate { get; set; }

		/// <summary>
		/// Gets or sets the acknowledge user identifier.
		/// </summary>
		/// <value>
		/// The acknowledge user identifier.
		/// </value>
		public string AcknowledgeUserId { get; set; }

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

		/// <summary>
		/// Gets or sets the name of the PRT.
		/// </summary>
		/// <value>
		/// The name of the PRT.
		/// </value>
		public string PrtName { get; set; }

		/// <summary>
		/// Gets or sets the port reference identifier.
		/// </summary>
		/// <value>
		/// The port reference identifier.
		/// </value>
		public string PortReferenceId { get; set; }
	}
}
