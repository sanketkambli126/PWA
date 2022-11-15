namespace PWAFeaturesRnd.Models.Report.Vessel
{
	/// <summary>
	/// Possible Workflow Details
	/// </summary>
	public class PossibleWorkflowDetails
	{
		/// <summary>
		/// Gets or sets the work flow identifier.
		/// </summary>
		/// <value>
		/// The work flow identifier.
		/// </value>
		public string WorkFlowId { get; set; }

		/// <summary>
		/// Gets or sets the name of the work flow.
		/// </summary>
		/// <value>
		/// The name of the work flow.
		/// </value>
		public string WorkFlowName { get; set; }

		/// <summary>
		/// Gets or sets the role identifier.
		/// </summary>
		/// <value>
		/// The role identifier.
		/// </value>
		public string RoleIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the name of the role.
		/// </summary>
		/// <value>
		/// The name of the role.
		/// </value>
		public string RoleName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [role applicable to vessel].
		/// </summary>
		/// <value>
		///   <c>true</c> if [role applicable to vessel]; otherwise, <c>false</c>.
		/// </value>
		public bool RoleApplicableToVessel { get; set; }

		/// <summary>
		/// Gets or sets the status identifier.
		/// </summary>
		/// <value>
		/// The status identifier.
		/// </value>
		public string StatusId { get; set; }

		/// <summary>
		/// Gets or sets the status description.
		/// </summary>
		/// <value>
		/// The status description.
		/// </value>
		public string StatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the activity priority.
		/// </summary>
		/// <value>
		/// The activity priority.
		/// </value>
		public int? ActivityPriority { get; set; }

		/// <summary>
		/// Gets or sets the UI priority.
		/// </summary>
		/// <value>
		/// The UI priority.
		/// </value>
		public int? UIPriority { get; set; }

		/// <summary>
		/// Gets or sets the lookup status identifier.
		/// </summary>
		/// <value>
		/// The lookup status identifier.
		/// </value>
		public string LookupStatusId { get; set; }

		/// <summary>
		/// Gets or sets the sort order.
		/// </summary>
		/// <value>
		/// The sort order.
		/// </value>
		public decimal? SortOrder { get; set; }
	}
}
