using System;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// Authoriser Detail
	/// </summary>
	public class AuthoriserDetailViewModel
	{
		
		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the name of the role.
		/// </summary>
		/// <value>
		/// The name of the role.
		/// </value>
		public string RoleName { get; set; }

		/// <summary>
		/// Gets or sets the authorisation date.
		/// </summary>
		/// <value>
		/// The authorisation date.
		/// </value>
		public DateTime? AuthorisationDate { get; set; }
	}
}
