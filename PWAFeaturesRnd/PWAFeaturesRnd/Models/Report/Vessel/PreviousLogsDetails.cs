using System;

namespace PWAFeaturesRnd.Models.Report.Vessel
{
	/// <summary>
	/// Previous Logs Details
	/// </summary>
	public class PreviousLogsDetails
	{
		/// <summary>
		/// Gets or sets the log status identifier.
		/// </summary>
		/// <value>
		/// The log status identifier.
		/// </value>
		public string LogStatusId { get; set; }

		/// <summary>
		/// Gets or sets the log status.
		/// </summary>
		/// <value>
		/// The log status.
		/// </value>
		public string LogStatus { get; set; }

		/// <summary>
		/// Gets or sets the performed by identifier.
		/// </summary>
		/// <value>
		/// The performed by identifier.
		/// </value>
		public string PerformedById { get; set; }

		/// <summary>
		/// Gets or sets the name of the performed by.
		/// </summary>
		/// <value>
		/// The name of the performed by.
		/// </value>
		public string PerformedByName { get; set; }

		/// <summary>
		/// Gets or sets the performed by role identifier.
		/// </summary>
		/// <value>
		/// The performed by role identifier.
		/// </value>
		public string PerformedByRoleIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the name of the performed by role.
		/// </summary>
		/// <value>
		/// The name of the performed by role.
		/// </value>
		public string PerformedByRoleName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [role applicable to vessel].
		/// </summary>
		/// <value>
		///   <c>true</c> if [role applicable to vessel]; otherwise, <c>false</c>.
		/// </value>
		public bool RoleApplicableToVessel { get; set; }

		/// <summary>
		/// Gets or sets the performed date.
		/// </summary>
		/// <value>
		/// The performed date.
		/// </value>
		public DateTime PerformedDate { get; set; }

		/// <summary>
		/// Gets or sets the perfomed UTC date.
		/// </summary>
		/// <value>
		/// The perfomed UTC date.
		/// </value>
		public DateTime PerfomedUTCDate { get; set; }
	}
}
