namespace PWAFeaturesRnd.Common.Paging
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PagedResponse<T>
	{
		/// <summary>
		/// the data for the specific page
		/// </summary>
		/// <value>
		/// The result.
		/// </value>
		public T Result { get; set; }

		/// <summary>
		/// the start index for the records retrieved
		/// </summary>
		/// <value>
		/// The page number.
		/// </value>
		public int PageNumber { get; set; }

		/// <summary>
		/// The total number of records in the entire set of data
		/// </summary>
		/// <value>
		/// The total pages.
		/// </value>
		public int TotalPages { get; set; }

		/// <summary>
		/// the total number of records for each page
		/// </summary>
		/// <value>
		/// The size of the page.
		/// </value>
		public int PageSize { get; set; }

		/// <summary>
		/// The total number of records in the entire set of data
		/// </summary>
		/// <value>
		/// The total records.
		/// </value>
		public int TotalRecords { get; set; }

		/// <summary>
		/// Gets or sets the size of the requested page.
		/// </summary>
		/// <value>
		/// The size of the requested page.
		/// </value>
		public int RequestedPageSize { get; set; }
	}
}
