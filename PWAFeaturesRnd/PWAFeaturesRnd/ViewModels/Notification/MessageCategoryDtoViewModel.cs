using System;

namespace PWAFeaturesRnd.ViewModels.Notification
{
	/// <summary>
	/// Message Category Dto View Model
	/// </summary>
	public class MessageCategoryDtoViewModel
	{
		/// <summary>
		/// Gets or sets the cat identifier.
		/// </summary>
		/// <value>
		/// The cat identifier.
		/// </value>
		public int CatId { get; set; }

		/// <summary>
		/// Gets or sets the name of the category.
		/// </summary>
		/// <value>
		/// The name of the category.
		/// </value>
		public string CategoryName { get; set; }
	}
}
