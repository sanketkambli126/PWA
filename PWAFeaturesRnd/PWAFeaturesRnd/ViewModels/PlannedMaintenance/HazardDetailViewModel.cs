namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
	/// <summary>
	/// Hazard Detail View Model
	/// </summary>
	public class HazardDetailViewModel
	{
		#region Properties

		/// <summary>
		/// Gets or sets the risk area.
		/// </summary>
		/// <value>
		/// The risk area.
		/// </value>
		public string RiskArea { get; set; }

		/// <summary>
		/// Gets or sets the system area.
		/// </summary>
		/// <value>
		/// The system area.
		/// </value>
		public string SystemArea { get; set; }

		/// <summary>
		/// Gets or sets the hazard number.
		/// </summary>
		/// <value>
		/// The hazard number.
		/// </value>
		public int HazardNumber { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the likelihood description.
		/// </summary>
		/// <value>
		/// The likelihood description.
		/// </value>
		public string LikelihoodDescription { get; set; }

		/// <summary>
		/// Gets or sets the risk factor description.
		/// </summary>
		/// <value>
		/// The risk factor description.
		/// </value>
		public string RiskFactorDescription { get; set; }

		/// <summary>
		/// Gets or sets the severity description.
		/// </summary>
		/// <value>
		/// The severity description.
		/// </value>
		public string SeverityDescription { get; set; }

		/// <summary>
		/// Gets or sets the color of the likelihood.
		/// </summary>
		/// <value>
		/// The color of the likelihood.
		/// </value>
		public string LikelihoodColor { get; set; }

		/// <summary>
		/// Gets or sets the color of the risk factor.
		/// </summary>
		/// <value>
		/// The color of the risk factor.
		/// </value>
		public string RiskFactorColor { get; set; }

		/// <summary>
		/// Gets or sets the color of the severity.
		/// </summary>
		/// <value>
		/// The color of the severity.
		/// </value>
		public string SeverityColor { get; set; }

		/// <summary>
		/// Gets or sets the initial color of the risk.
		/// </summary>
		/// <value>
		/// The initial color of the risk.
		/// </value>
		public string InitialRiskColor { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is initial risk visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is initial risk visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsInitialRiskVisible { get; set; }

		/// <summary>
		/// Gets or sets the initial risk factor description.
		/// </summary>
		/// <value>
		/// The initial risk factor description.
		/// </value>
		public string InitialRiskFactorDescription { get; set; }

		#endregion
	}
}
