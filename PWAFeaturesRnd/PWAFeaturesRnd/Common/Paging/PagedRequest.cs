using System.Collections.Generic;

namespace PWAFeaturesRnd.Common.Paging
{
	/// <summary>
	/// 
	/// </summary>
	public class PagedRequest
	{
		/// <summary>
		/// the start index for the records retrieved
		/// </summary>
		/// <value>
		/// The page number.
		/// </value>
		public int PageNumber { get; set; }

		/// <summary>
		/// the number of records to retrieve
		/// maximum number is 1000
		/// </summary>
		/// <value>
		/// The size of the page.
		/// </value>
		public int PageSize { get; set; }

		/// <summary>
		/// the number of total records, if not known do not set
		/// If the value is known it can be set to avoid another db fetch on the total count
		/// </summary>
		/// <value>
		/// The total records.
		/// </value>
		public int? TotalRecords { get; set; }

		/// <summary>
		/// Gets or sets the sorts.
		/// </summary>
		/// <value>
		/// The sorts.
		/// </value>
		public List<SortInfo> Sorts { get; set; }

		/// <summary>
		/// Gets or sets the server sorting.
		/// </summary>
		/// <value>
		/// The server sorting.
		/// </value>
		public SortRequest ServerSorting { get; set; }

		/// <summary>
		/// Gets or sets the search filter.
		/// </summary>
		/// <value>
		/// The search filter.
		/// </value>
		public string SearchFilter { get; set; }
	}
}
