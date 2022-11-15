namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Authorise Quote Request
	/// </summary>
	public class AuthoriseQuoteRequest
	{
		/// <summary>
		/// Gets or sets the order identifier.
		/// </summary>
		/// <value>
		/// The order identifier.
		/// </value>
		public string OrderId { get; set; } //found in header service

		/// <summary>
		/// Gets or sets the supplier order identifier.
		/// </summary>
		/// <value>
		/// The supplier order identifier.
		/// </value>
		public string SupplierOrderId { get; set; } //found in list service

		/// <summary>
		/// Gets or sets a value indicating whether this instance is feedback required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is feedback required; otherwise, <c>false</c>.
		/// </value>
		public bool IsFeedbackRequired { get; set; } //false

		/// <summary>
		/// Gets or sets the feedback reason description.
		/// </summary>
		/// <value>
		/// The feedback reason description.
		/// </value>
		public string FeedbackReasonDescription { get; set; } //""

		/// <summary>
		/// Gets or sets the feedback comments.
		/// </summary>
		/// <value>
		/// The feedback comments.
		/// </value>
		public string FeedbackComments { get; set; } //""

		/// <summary>
		/// Gets or sets the feedback supplier order identifier.
		/// </summary>
		/// <value>
		/// The feedback supplier order identifier.
		/// </value>
		public string FeedbackSupplierOrderId { get; set; } //""

		/// <summary>
		/// Gets or sets the justification comment.
		/// </summary>
		/// <value>
		/// The justification comment.
		/// </value>
		public string JustificationComment { get; set; } //fetch from textarea

		/// <summary>
		/// Gets or sets the account identifier.
		/// </summary>
		/// <value>
		/// The account identifier.
		/// </value>
		public string AccountId { get; set; } //fetch

		/// <summary>
		/// Gets or sets the accruals.
		/// </summary>
		/// <value>
		/// The accruals.
		/// </value>
		public decimal Accruals { get; set; } //(CompanyVesselBudget != null) ? CompanyVesselBudget.TotalAccruals : 0;

		/// <summary>
		/// Gets or sets the justification reason identifier.
		/// </summary>
		/// <value>
		/// The justification reason identifier.
		/// </value>
		public int? JustificationReasonId { get; set; } //drop down value - need to be converted into int

		/// <summary>
		/// Gets or sets the auxiliaries.
		/// </summary>
		/// <value>
		/// The auxiliaries.
		/// </value>
		public AuxiliaryDetail Auxiliaries { get; set; } //wil be taken later in phase 2
	}
}
