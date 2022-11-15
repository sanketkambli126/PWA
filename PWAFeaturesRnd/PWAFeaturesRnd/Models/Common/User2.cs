using System.Collections.Generic;
using System.Linq;

namespace PWAFeaturesRnd.Models.Common
{
	/// <summary>
	/// 
	/// </summary>
	public class User2
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public string UserId { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>
		/// The username.
		/// </value>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the display name of the user.
		/// </summary>
		/// <value>
		/// The display name of the user.
		/// </value>
		public string UserDisplayName { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the site identifier.
		/// </summary>
		/// <value>
		/// The site identifier.
		/// </value>
		public string SiteId { get; set; }

		/// <summary>
		/// Gets or sets the type of the site.
		/// </summary>
		/// <value>
		/// The type of the site.
		/// </value>
		public string SiteType { get; set; }

		/// <summary>
		/// Gets or sets the fleet identifier.
		/// </summary>
		/// <value>
		/// The fleet identifier.
		/// </value>
		public string FleetId { get; set; }

		/// <summary>
		/// Gets or sets the user company.
		/// </summary>
		/// <value>
		/// The user company.
		/// </value>
		public string UserCompany { get; set; }

		/// <summary>
		/// Gets or sets the department identifier.
		/// </summary>
		/// <value>
		/// The department identifier.
		/// </value>
		public string DepartmentId { get; set; }

		/// <summary>
		/// Gets or sets the name of the portal client.
		/// </summary>
		/// <value>
		/// The name of the portal client.
		/// </value>
		public string PortalClientName { get; set; }

		/// <summary>
		/// Gets or sets the roles.
		/// </summary>
		/// <value>
		/// The roles.
		/// </value>
		public List<Role> Roles { get; set; }

		/// <summary>
		/// Gets the primary role.
		/// </summary>
		/// <value>
		/// The primary role.
		/// </value>
		public virtual Role PrimaryRole
		{
			get
			{
				if (Roles.Any(x => x.IsPrimary))
				{
					return Roles.First(x => x.IsPrimary);
				}
				return null;
			}
		}

	}
}
