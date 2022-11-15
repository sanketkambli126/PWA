using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Common.Paging
{
	/// <summary>
	/// 
	/// </summary>
	public class SortInfo
	{
		/// <summary>
		/// Gets or sets the type of the enum.
		/// </summary>
		/// <value>
		/// The type of the enum.
		/// </value>
		public string EnumType { get; set; }

		/// <summary>
		/// Gets or sets the index of the enum.
		/// </summary>
		/// <value>
		/// The index of the enum.
		/// </value>
		public int EnumIndex { get; set; }

		/// <summary>
		/// Gets or sets the sort direction.
		/// </summary>
		/// <value>
		/// The sort direction.
		/// </value>
		public SortDirection SortDirection { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SortInfo"/> class.
		/// </summary>
		public SortInfo()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SortInfo"/> class.
		/// </summary>
		/// <param name="fieldIndex">Index of the field.</param>
		/// <param name="sortDirection">The sort direction.</param>
		public SortInfo(Enum fieldIndex, SortDirection sortDirection)
		{
			EnumType = fieldIndex.GetType().AssemblyQualifiedName;
			EnumIndex = Convert.ToInt32(fieldIndex);

			SortDirection = sortDirection;
		}

		/// <summary>
		/// Converts to enum.
		/// </summary>
		/// <returns></returns>
		public Enum ToEnum()
		{
			var e = Type.GetType(EnumType);
			return (Enum)Enum.Parse(e, EnumIndex.ToString());
		}
	}
}
