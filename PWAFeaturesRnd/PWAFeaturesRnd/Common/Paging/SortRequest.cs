using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Common.Paging
{
	/// <summary>
	/// 
	/// </summary>
	public class SortRequest
	{
		/// <summary>
		/// Gets or sets the name of the column.
		/// </summary>
		/// <value>
		/// The name of the column.
		/// </value>
		public string ColumnName { get; set; }

		/// <summary>
		/// Gets or sets the sort direction.
		/// </summary>
		/// <value>
		/// The sort direction.
		/// </value>
		public SortDirection SortDirection { get; set; }
	}
}
