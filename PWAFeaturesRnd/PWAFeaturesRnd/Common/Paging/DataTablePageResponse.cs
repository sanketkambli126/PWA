namespace PWAFeaturesRnd.Common.Paging
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DataTablePageResponse<T>
	{
		/// <summary>
		/// Gets or sets the draw.
		/// </summary>
		/// <value>
		/// The draw.
		/// </value>
		public int Draw { get; set; }

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public T Data { get; set; }

		/// <summary>
		/// Gets or sets the records total.
		/// </summary>
		/// <value>
		/// The records total.
		/// </value>
		public int RecordsTotal { get; set; }

		/// <summary>
		/// Gets or sets the records filtered.
		/// </summary>
		/// <value>
		/// The records filtered.
		/// </value>
		public int RecordsFiltered { get; set; }
	}
}
