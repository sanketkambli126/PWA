using System;

namespace PWAFeaturesRnd.ViewModels.Vessel
{
	/// <summary>
	/// WorkflowDetailViewModel
	/// </summary>
	public class WorkflowDetailViewModel
	{
		/// <summary>
		/// Gets or sets the name of the activity.
		/// </summary>
		/// <value>
		/// The name of the activity.
		/// </value>
		public string ActivityName { get; set; }

		/// <summary>
		/// Gets or sets the name of the performed by role.
		/// </summary>
		/// <value>
		/// The name of the performed by role.
		/// </value>
		public string PerformedByRoleName { get; set; }

		/// <summary>
		/// Gets or sets the name of the performed by.
		/// </summary>
		/// <value>
		/// The name of the performed by.
		/// </value>
		public string PerformedByName { get; set; }

		/// <summary>
		/// Gets or sets the perfomed UTC date.
		/// </summary>
		/// <value>
		/// The perfomed UTC date.
		/// </value>
		public DateTime PerfomedUTCDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is applicable to vessel.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is applicable to vessel; otherwise, <c>false</c>.
		/// </value>
		public bool IsApplicableToVessel { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is done.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is done; otherwise, <c>false</c>.
		/// </value>
		public bool IsDone { get; set; }

		/// <summary>
		/// Gets or sets the sortorder.
		/// </summary>
		/// <value>
		/// The sortorder.
		/// </value>
		public decimal SortOrder { get; set; }
	}
}
