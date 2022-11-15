namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// The supplier rating breakdown
    /// </summary>
    public class SupplierRatingBreakdown
    {
        /// <summary>
        /// Gets or sets the four star.
        /// </summary>
        /// <value>
        /// The four star.
        /// </value>
        public int FourStar { get; set; }

        /// <summary>
        /// Gets or sets the three star.
        /// </summary>
        /// <value>
        /// The three star.
        /// </value>
        public int ThreeStar { get; set; }

        /// <summary>
        /// Gets or sets the two star.
        /// </summary>
        /// <value>
        /// The two star.
        /// </value>
        public int TwoStar { get; set; }

        /// <summary>
        /// Gets or sets the one star.
        /// </summary>
        /// <value>
        /// The one star.
        /// </value>
        public int OneStar { get; set; }

        /// <summary>
        /// Gets or sets the total orders.
        /// </summary>
        /// <value>
        /// The total orders.
        /// </value>
        public int TotalOrders { get; set; }
    }
}
