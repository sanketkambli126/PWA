using PWAFeaturesRnd.Models.Report.Dashboard;

namespace PWAFeaturesRnd.ViewModels.Approval
{
	/// <summary>
	/// 
	/// </summary>
	public class ApprovalListViewModel
	{
		/// <summary>
		/// Gets or sets the dashboard parameter.
		/// </summary>
		/// <value>
		/// The dashboard parameter.
		/// </value>
		public DashboardParameter DashboardParameter { get; set; }

		/// <summary>
		/// Gets or sets the header node short code.
		/// </summary>
		/// <value>
		/// The header node short code.
		/// </value>
		public string HeaderNodeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the node short code.
		/// </summary>
		/// <value>
		/// The node short code.
		/// </value>
		public string NodeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the active mobile tab class.
        /// </summary>
        /// <value>
        /// The active mobile tab class.
        /// </value>
        public string ActiveMobileTabClass { get; set; }
    }
}
