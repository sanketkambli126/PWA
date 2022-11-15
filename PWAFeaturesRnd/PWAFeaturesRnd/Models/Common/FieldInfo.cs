using System;

namespace PWAFeaturesRnd.Models.Common
{
	/// <summary>
	/// 
	/// </summary>
	public class FieldInfo
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
		/// Initializes a new instance of the <see cref="FieldInfo" /> class.
		/// </summary>
		public FieldInfo()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FieldInfo" /> class.
		/// </summary>
		/// <param name="fieldIndex">Index of the field.</param>
		public FieldInfo(Enum fieldIndex)
		{
			EnumType = fieldIndex.GetType().AssemblyQualifiedName;
			EnumIndex = Convert.ToInt32(fieldIndex);
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
