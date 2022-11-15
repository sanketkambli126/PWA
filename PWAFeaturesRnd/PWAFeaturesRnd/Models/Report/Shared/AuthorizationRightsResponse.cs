namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// Authorization Rights Response
	/// </summary>
	public class AuthorizationRightsResponse
	{
		/// <summary>
		/// Gets or sets the role identifier.
		/// </summary>
		/// <value>
		/// The role identifier.
		/// </value>
		public string RoleIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public int Priority { get; set; }
	}
}
