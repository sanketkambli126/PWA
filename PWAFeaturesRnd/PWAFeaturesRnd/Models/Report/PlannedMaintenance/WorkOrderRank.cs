namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Work Order Rank
	/// </summary>
	public class WorkOrderRank
	{
		/// <summary>
		/// Gets or sets the PHR identifier.
		/// </summary>
		/// <value>
		/// The PHR identifier.
		/// </value>
		public string PhrId { get; set; }

		/// <summary>
		/// Gets or sets the TMR identifier.
		/// </summary>
		/// <value>
		/// The TMR identifier.
		/// </value>
		public string TmrId { get; set; }

		/// <summary>
		/// Gets or sets the RNK identifier.
		/// </summary>
		/// <value>
		/// The RNK identifier.
		/// </value>
		public string RnkId { get; set; }

		/// <summary>
		/// Gets or sets the rank description.
		/// </summary>
		/// <value>
		/// The rank description.
		/// </value>
		public string RankDescription { get; set; }

		/// <summary>
		/// Gets or sets the rank short code.
		/// </summary>
		/// <value>
		/// The rank short code.
		/// </value>
		public string RankShortCode { get; set; }

		/// <summary>
		/// Gets or sets the man hours.
		/// </summary>
		/// <value>
		/// The man hours.
		/// </value>
		public float? ManHours { get; set; }

		/// <summary>
		/// Gets or sets the cost.
		/// </summary>
		/// <value>
		/// The cost.
		/// </value>
		public decimal Cost { get; set; }
	}
}
