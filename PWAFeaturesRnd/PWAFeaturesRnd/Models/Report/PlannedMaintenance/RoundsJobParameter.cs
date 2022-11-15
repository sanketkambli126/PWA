namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// RoundsJobParameter
	/// </summary>
	public class RoundsJobParameter
	{
		/// <summary>
		/// Gets or sets the RST identifier.
		/// </summary>
		/// <value>
		/// The RST identifier.
		/// </value>
		public string RstId { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the RJP identifier.
		/// </summary>
		/// <value>
		/// The RJP identifier.
		/// </value>
		public string RjpId { get; set; }

		/// <summary>
		/// Gets or sets the PJB identifier.
		/// </summary>
		/// <value>
		/// The PJB identifier.
		/// </value>
		public string PjbId { get; set; }

		/// <summary>
		/// Gets or sets the PST identifier.
		/// </summary>
		/// <value>
		/// The PST identifier.
		/// </value>
		public string PstId { get; set; }

		/// <summary>
		/// Gets or sets the name of the parameter.
		/// </summary>
		/// <value>
		/// The name of the parameter.
		/// </value>
		public string ParameterName { get; set; }

		/// <summary>
		/// Gets or sets the parameter value.
		/// </summary>
		/// <value>
		/// The parameter value.
		/// </value>
		public string ParameterValue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is read only.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is read only; otherwise, <c>false</c>.
		/// </value>
		public bool IsReadOnly { get; set; }

		/// <summary>
		/// Gets or sets the type of the mla identifier data.
		/// </summary>
		/// <value>
		/// The type of the mla identifier data.
		/// </value>
		public string MlaIdDataType { get; set; }

		/// <summary>
		/// Gets or sets the type of the data.
		/// </summary>
		/// <value>
		/// The type of the data.
		/// </value>
		public string DataType { get; set; }

		/// <summary>
		/// Gets or sets the sequence number.
		/// </summary>
		/// <value>
		/// The sequence number.
		/// </value>
		public int SequenceNumber { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the PTR identifier.
		/// </summary>
		/// <value>
		/// The PTR identifier.
		/// </value>
		public string PtrId { get; set; }

		/// <summary>
		/// Gets or sets the PWH identifier.
		/// </summary>
		/// <value>
		/// The PWH identifier.
		/// </value>
		public string PwhId { get; set; }

		/// <summary>
		/// Gets or sets the RWH identifier.
		/// </summary>
		/// <value>
		/// The RWH identifier.
		/// </value>
		public string RwhId { get; set; }
	}
}
