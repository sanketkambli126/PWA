using System.Collections.Generic;

namespace PWAFeaturesRnd.Common.Paging
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DataTablePageRequest<T>
	{
		/// <summary>
		/// Gets or sets the draw.
		/// </summary>
		/// <value>
		/// The draw.
		/// </value>
		public int Draw { get; set; }

		/// <summary>
		/// Gets or sets the start.
		/// </summary>
		/// <value>
		/// The start.
		/// </value>
		public int Start { get; set; }

		/// <summary>
		/// Gets or sets the length.
		/// </summary>
		/// <value>
		/// The length.
		/// </value>
		public int Length { get; set; }

		/// <summary>
		/// Gets or sets the columns.
		/// </summary>
		/// <value>
		/// The columns.
		/// </value>
		public List<Column> Columns { get; set; }

		/// <summary>
		/// Gets or sets the search.
		/// </summary>
		/// <value>
		/// The search.
		/// </value>
		public Search Search { get; set; }

		/// <summary>
		/// Gets or sets the order.
		/// </summary>
		/// <value>
		/// The order.
		/// </value>
		public List<Order> Order { get; set; }

		/// <summary>
		/// Gets or sets the search filter.
		/// </summary>
		/// <value>
		/// The search filter.
		/// </value>
		public T SearchFilter { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class Column
	{
		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public string Data { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Column"/> is searchable.
		/// </summary>
		/// <value>
		///   <c>true</c> if searchable; otherwise, <c>false</c>.
		/// </value>
		public bool Searchable { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Column"/> is orderable.
		/// </summary>
		/// <value>
		///   <c>true</c> if orderable; otherwise, <c>false</c>.
		/// </value>
		public bool Orderable { get; set; }

		/// <summary>
		/// Gets or sets the search.
		/// </summary>
		/// <value>
		/// The search.
		/// </value>
		public Search Search { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class Search
	{
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets the regex.
		/// </summary>
		/// <value>
		/// The regex.
		/// </value>
		public string Regex { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class Order
	{
		/// <summary>
		/// Gets or sets the column.
		/// </summary>
		/// <value>
		/// The column.
		/// </value>
		public int Column { get; set; }

		/// <summary>
		/// Gets or sets the dir.
		/// </summary>
		/// <value>
		/// The dir.
		/// </value>
		public string Dir { get; set; }
	}
}
