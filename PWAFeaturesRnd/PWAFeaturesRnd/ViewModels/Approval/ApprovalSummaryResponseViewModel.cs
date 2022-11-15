using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Dashboard
{
    public class ApprovalSummaryResponseViewModel
    {
        /// <summary>
        /// Gets or sets the header node short code.
        /// </summary>
        /// <value>
        /// The header node short code.
        /// </value>
        public string HeaderNodeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the header node.
        /// </summary>
        /// <value>
        /// The header node.
        /// </value>
        public string HeaderNode { get; set; }

        /// <summary>
        /// Gets or sets the node short code.
        /// </summary>
        /// <value>
        /// The node short code.
        /// </value>
        public string NodeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>
        /// The node.
        /// </value>
        public string Node { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

		/// <summary>
		/// Gets or sets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
		public List<ApprovalSummaryResponseViewModel> Children { get; set; }

		/// <summary>
		/// Gets or sets the approval list URL.
		/// </summary>
		/// <value>
		/// The approval list URL.
		/// </value>
		public string ApprovalListUrl { get; set; }

        /// <summary>
        /// Gets or sets the approval over view URL.
        /// </summary>
        /// <value>
        /// The approval over view URL.
        /// </value>
        public string ApprovalOverViewUrl { get; set; }
	}
}
