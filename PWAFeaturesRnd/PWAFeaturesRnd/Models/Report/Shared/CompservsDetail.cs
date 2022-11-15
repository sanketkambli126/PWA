using System;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// CompservsDetail
	/// </summary>
	public class CompservsDetail
	{
		/// <summary>
		/// Gets or sets the CMP identifier.
		/// </summary>
		/// <value>
		/// The CMP identifier.
		/// </value>
		public string CmpId { get; set; }

		/// <summary>
		/// Gets or sets the CMT identifier.
		/// </summary>
		/// <value>
		/// The CMT identifier.
		/// </value>
		public string CmtId { get; set; }

		/// <summary>
		/// Gets or sets the CMS identifier.
		/// </summary>
		/// <value>
		/// The CMS identifier.
		/// </value>
		public string CmsId { get; set; }

		/// <summary>
		/// Gets or sets the CMS deleted.
		/// </summary>
		/// <value>
		/// The CMS deleted.
		/// </value>
		public bool? CmsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the CMS blocked.
		/// </summary>
		/// <value>
		/// The CMS blocked.
		/// </value>
		public byte CmsBlocked { get; set; }

		/// <summary>
		/// Gets or sets the CMS updated on.
		/// </summary>
		/// <value>
		/// The CMS updated on.
		/// </value>
		public DateTime CmsUpdatedOn { get; set; }

		/// <summary>
		/// Gets or sets the CMS updated by.
		/// </summary>
		/// <value>
		/// The CMS updated by.
		/// </value>
		public string CmsUpdatedBy { get; set; }

		/// <summary>
		/// Gets or sets the rowguid.
		/// </summary>
		/// <value>
		/// The rowguid.
		/// </value>
		public Guid Rowguid { get; set; }

	}
}
