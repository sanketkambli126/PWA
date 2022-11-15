namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// Pending Order Authorisation Detail ViewModel
	/// </summary>
	public class PendingOrderAuthorisationDetailViewModel
	{
		/// <summary>
		/// Gets or sets a value indicating whether this instance is current user authorisation required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is current user authorisation required; otherwise, <c>false</c>.
		/// </value>
		public bool IsCurrentUserAuthorisationRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is further order authorisation required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is further order authorisation required; otherwise, <c>false</c>.
		/// </value>
		public bool IsFurtherOrderAuthorisationRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is order authorisation processed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is order authorisation processed; otherwise, <c>false</c>.
		/// </value>
		public bool IsOrderAuthorisationProcessed { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is client authorisation required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is client authorisation required; otherwise, <c>false</c>.
		/// </value>
		public bool IsClientAuthorisationRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is client authorisation processed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is client authorisation processed; otherwise, <c>false</c>.
		/// </value>
		public bool IsClientAuthorisationProcessed { get; set; }
	}
}
