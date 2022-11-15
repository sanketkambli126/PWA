using System;

namespace PWAFeaturesRnd.Models.Report.Notification
{
	/// <summary>
	/// Message Category Dto
	/// </summary>
	public class MessageCategoryDto
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

		/// <summary>
		/// Gets or sets the is attachment allowed.
		/// </summary>
		/// <value>
		/// The is attachment allowed.
		/// </value>
		public bool? IsAttachmentAllowed { get; set; }

		/// <summary>
		/// Gets or sets the created on.
		/// </summary>
		/// <value>
		/// The created on.
		/// </value>
		public DateTime? CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the content payload details query.
		/// </summary>
		/// <value>
		/// The content payload details query.
		/// </value>
		public string ContentPayloadDetailsQuery { get; set; }

		/// <summary>
		/// Gets or sets the parent cat identifier.
		/// </summary>
		/// <value>
		/// The parent cat identifier.
		/// </value>
		public int? ParentCatId { get; set; }

		/// <summary>
		/// Gets or sets the context payload template.
		/// </summary>
		/// <value>
		/// The context payload template.
		/// </value>
		public string ContextPayloadTemplate { get; set; }
	}
}