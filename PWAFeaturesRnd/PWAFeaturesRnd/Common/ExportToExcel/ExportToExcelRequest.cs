using System.Collections.Generic;

namespace PWAFeaturesRnd.Common.ExportToExcel
{
	/// <summary>
	/// Export To Excel Request
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ExportToExcelRequest
	{
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>
		/// The name of the file.
		/// </value>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the summary.
		/// </summary>
		/// <value>
		/// The summary.
		/// </value>
		public string Summary { get; set; }

		/// <summary>
		/// Gets or sets the column count.
		/// </summary>
		/// <value>
		/// The column count.
		/// </value>
		public int ColumnCount { get; set; }

		/// <summary>
		/// Gets or sets the summary row count.
		/// </summary>
		/// <value>
		/// The summary row count.
		/// </value>
		public int SummaryRowCount { get; set; }
	}
}
