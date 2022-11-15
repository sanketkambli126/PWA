namespace PWAFeaturesRnd.Models.Report.Dashboard
{
    /// <summary>
    /// Approval summary response
    /// </summary>
    public class ApprovalSummaryResponse
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
    }
}
