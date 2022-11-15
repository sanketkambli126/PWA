namespace PWAFeaturesRnd.ViewModels.Inspection
{
	/// <summary>
	/// 
	/// </summary>
	public class InspectionFindingResponseViewModel
	{
		/// <summary>
		/// Gets or sets the inspection identifier.
		/// </summary>
		/// <value>
		/// The inspection identifier.
		/// </value>
		public string InspectionId { get; set; }

		/// <summary>
		/// Gets or sets the inspection type identifier.
		/// </summary>
		/// <value>
		/// The inspection type identifier.
		/// </value>
		public string InspectionTypeId { get; set; }

		/// <summary>
		/// Gets or sets the inspection finding identifier.
		/// </summary>
		/// <value>
		/// The inspection finding identifier.
		/// </value>
		public string InspectionFindingId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the inspection finding type.
		/// </summary>
		/// <value>
		/// The name of the inspection finding type.
		/// </value>
		public string InspectionFindingTypeName { get; set; }

		/// <summary>
		/// Gets or sets the vessel reference no.
		/// </summary>
		/// <value>
		/// The vessel reference no.
		/// </value>
		public string VesselReferenceNo { get; set; }

		/// <summary>
		/// Gets or sets the inspection reference no.
		/// </summary>
		/// <value>
		/// The inspection reference no.
		/// </value>
		public string InspectionReferenceNo { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the name of the risk assessment category.
		/// </summary>
		/// <value>
		/// The name of the risk assessment category.
		/// </value>
		public string RiskAssessmentCategoryName { get; set; }

		/// <summary>
		/// Gets or sets the PSC action code.
		/// </summary>
		/// <value>
		/// The PSC action code.
		/// </value>
		public string PscActionCode { get; set; }

		/// <summary>
		/// Gets or sets the PSC group code.
		/// </summary>
		/// <value>
		/// The PSC group code.
		/// </value>
		public string PscGroupCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the risk assessment area.
		/// </summary>
		/// <value>
		/// The name of the risk assessment area.
		/// </value>
		public string RiskAssessmentAreaName { get; set; }

		/// <summary>
		/// Gets or sets the date due.
		/// </summary>
		/// <value>
		/// The date due.
		/// </value>
		public string DateDue { get; set; }

		/// <summary>
		/// Gets or sets the date cleared.
		/// </summary>
		/// <value>
		/// The date cleared.
		/// </value>
		public string DateCleared { get; set; }

		/// <summary>
		/// Gets or sets the action URL.
		/// </summary>
		/// <value>
		/// The action URL.
		/// </value>
		public string ActionUrl { get; set; }

		/// <summary>
		/// Gets or sets the channel count.
		/// </summary>
		/// <value>
		/// The channel count.
		/// </value>
		public int ChannelCount { get; set; }

		/// <summary>
		/// Gets or sets the notes count.
		/// </summary>
		/// <value>
		/// The notes count.
		/// </value>
		public int NotesCount { get; set; }

		/// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }
	}
}
