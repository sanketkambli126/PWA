using Newtonsoft.Json;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Dashboard
{
	/// <summary>
	/// 
	/// </summary>
	public class NavigationTreeViewModel
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the tooltip.
		/// </summary>
		/// <value>
		/// The tooltip.
		/// </value>
		public string Tooltip { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NavigationTreeViewModel" /> is expanded.
		/// </summary>
		/// <value>
		///   <c>true</c> if expanded; otherwise, <c>false</c>.
		/// </value>
		public bool Expanded { get; set; }

		/// <summary>
		/// Gets or sets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<NavigationTreeViewModel>? Children { get; set; }

		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public string Key { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NavigationTreeViewModel" /> is lazy.
		/// </summary>
		/// <value>
		///   <c>true</c> if lazy; otherwise, <c>false</c>.
		/// </value>
		public bool Lazy { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NavigationTreeViewModel" /> is checkbox.
		/// </summary>
		/// <value>
		///   <c>true</c> if checkbox; otherwise, <c>false</c>.
		/// </value>
		public bool Checkbox { get; set; }

		/// <summary>
		/// Gets or sets the type of the user menu item.
		/// </summary>
		/// <value>
		/// The type of the user menu item.
		/// </value>
		public UserMenuItemType UserMenuItemType { get; set; }

		/// <summary>
		/// Gets or sets the type of the tree.
		/// </summary>
		/// <value>
		/// The type of the tree.
		/// </value>
		public TreeType TreeType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [preload user fleet].
		/// </summary>
		/// <value>
		///   <c>true</c> if [preload user fleet]; otherwise, <c>false</c>.
		/// </value>
		public bool PreloadUserFleet { get; set; }

		/// <summary>
		/// Gets or sets the exclusion.
		/// </summary>
		/// <value>
		/// The exclusion.
		/// </value>
		public List<UserMenuItemType> Exclusion { get; set; }

		/// <summary>
		/// Gets or sets the type of the user.
		/// </summary>
		/// <value>
		/// The type of the user.
		/// </value>
		public UserType UserType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is vessel.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is vessel; otherwise, <c>false</c>.
		/// </value>
		public bool IsVessel { get; set; }

		/// <summary>
		/// Gets or sets the user menu type short code.
		/// </summary>
		/// <value>
		/// The user menu type short code.
		/// </value>
		public string UserMenuTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string Identifier { get; set; }

		/// <summary>
		/// Gets or sets the parent user menu type short code.
		/// </summary>
		/// <value>
		/// The parent user menu type short code.
		/// </value>
		public string ParentUserMenuTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [allow fleet selection].
		/// </summary>
		/// <value>
		///   <c>true</c> if [allow fleet selection]; otherwise, <c>false</c>.
		/// </value>
		public bool AllowFleetSelection { get; set; }

	}
}
