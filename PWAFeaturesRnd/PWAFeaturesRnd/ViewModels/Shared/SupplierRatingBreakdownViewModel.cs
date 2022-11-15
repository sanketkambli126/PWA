namespace PWAFeaturesRnd.ViewModels.Shared
{
    public class SupplierRatingBreakdownViewModel
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

        /// <summary>
        /// Gets or sets the average rating.
        /// </summary>
        /// <value>
        /// The average rating.
        /// </value>
        public double AverageRating { get; set; }

        /// <summary>
        /// Gets or sets the average color of the rating.
        /// </summary>
        /// <value>
        /// The average color of the rating.
        /// </value>
        public string AverageRatingColor { get; set; }

        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        /// <value>
        /// The name of the order.
        /// </value>
        public string OrderName { get; set; }

        public double FourStarPercent { get; set; }
        public double ThreeStarPercent { get; set; }
        public double TwoStarPercent { get; set; }
        public double OneStarPercent { get; set; }

    }
}
