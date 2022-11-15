
namespace PWAFeaturesRnd.Models.Report
{
	/// <summary>
	/// Base Filter
	/// </summary>
	public class BaseFilter
	{
		/// <summary>
		/// Gets or sets the search string.
		/// </summary>
		/// <value>
		/// The search string.
		/// </value>
		public string SearchString { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [search with filters].
		/// </summary>
		/// <value>
		///   <c>true</c> if [search with filters]; otherwise, <c>false</c>.
		/// </value>

		public bool SearchWithFilters { get; set; }
	}
}
