namespace PWAFeaturesRnd.Models.Common
{
	/// <summary>
	/// 
	/// </summary>
	public class Role
	{
		/// <summary>
		/// Gets or sets the role identifier.
		/// </summary>
		/// <value>
		/// The role identifier.
		/// </value>
		public string RoleId { get; set; }

		/// <summary>
		/// Gets or sets the role description.
		/// </summary>
		/// <value>
		/// The role description.
		/// </value>
		public string RoleDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is primary.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is primary; otherwise, <c>false</c>.
		/// </value>
		public bool IsPrimary { get; set; }
	}
}
