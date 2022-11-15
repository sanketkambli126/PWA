namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Performance summary response
    /// </summary>
    public class PerformanceSummaryResponse
    {
        /// <summary>
        /// Gets or sets the last24 hour consumption.
        /// </summary>
        /// <value>
        /// The last24 hour consumption.
        /// </value>
        public decimal? Last24HourConsumption { get; set; }


        /// <summary>
        /// Gets or sets the last24 hour consumption priority.
        /// </summary>
        /// <value>
        /// The last24 hour consumption priority.
        /// </value>
        public int Last24HourConsumptionPriority { get; set; }


        /// <summary>
        /// Gets or sets the last24 hour consumption background priority.
        /// </summary>
        /// <value>
        /// The last24 hour consumption background priority.
        /// </value>
        public int Last24HourConsumptionBackgroundPriority { get; set; }


        /// <summary>
        /// Gets or sets the last24 hour speed.
        /// </summary>
        /// <value>
        /// The last24 hour speed.
        /// </value>
        public decimal? Last24HourSpeed { get; set; }


        /// <summary>
        /// Gets or sets the last24 hour speed priority.
        /// </summary>
        /// <value>
        /// The last24 hour speed priority.
        /// </value>
        public int Last24HourSpeedPriority { get; set; }


        /// <summary>
        /// Gets or sets the last24 hour speed background priority.
        /// </summary>
        /// <value>
        /// The last24 hour speed background priority.
        /// </value>
        public int Last24HourSpeedBackgroundPriority { get; set; }


        /// <summary>
        /// Gets or sets the voyage average speed.
        /// </summary>
        /// <value>
        /// The voyage average speed.
        /// </value>
        public decimal? VoyageAverageSpeed { get; set; }


        /// <summary>
        /// Gets or sets the voyage average speed priority.
        /// </summary>
        /// <value>
        /// The voyage average speed priority.
        /// </value>
        public int VoyageAverageSpeedPriority { get; set; }


        /// <summary>
        /// Gets or sets the voyage average speed background priority.
        /// </summary>
        /// <value>
        /// The voyage average speed background priority.
        /// </value>
        public int VoyageAverageSpeedBackgroundPriority { get; set; }


        /// <summary>
        /// Gets or sets the voyage average consumption.
        /// </summary>
        /// <value>
        /// The voyage average consumption.
        /// </value>
        public decimal? VoyageAverageConsumption { get; set; }


        /// <summary>
        /// Gets or sets the voyage average consumption priority.
        /// </summary>
        /// <value>
        /// The voyage average consumption priority.
        /// </value>
        public int VoyageAverageConsumptionPriority { get; set; }


        /// <summary>
        /// Gets or sets the voyage average consumption background priority.
        /// </summary>
        /// <value>
        /// The voyage average consumption background priority.
        /// </value>
        public int VoyageAverageConsumptionBackgroundPriority { get; set; }


        /// <summary>
        /// Gets or sets the cp adjusted speed.
        /// </summary>
        /// <value>
        /// The cp adjusted speed.
        /// </value>
        public decimal? CPAdjustedSpeed { get; set; }


        /// <summary>
        /// Gets or sets the cp adjusted speed priority.
        /// </summary>
        /// <value>
        /// The cp adjusted speed priority.
        /// </value>
        public int CPAdjustedSpeedPriority { get; set; }


        /// <summary>
        /// Gets or sets the cp adjusted speed background priority.
        /// </summary>
        /// <value>
        /// The cp adjusted speed background priority.
        /// </value>
        public int CPAdjustedSpeedBackgroundPriority
        { get; set; }


        /// <summary>
        /// Gets or sets the cp adjusted consumption.
        /// </summary>
        /// <value>
        /// The cp adjusted consumption.
        /// </value>
        public decimal? CPAdjustedConsumption { get; set; }


        /// <summary>
        /// Gets or sets the cp adjusted consumption priority.
        /// </summary>
        /// <value>
        /// The cp adjusted consumption priority.
        /// </value>
        public int CPAdjustedConsumptionPriority { get; set; }


        /// <summary>
        /// Gets or sets the cp adjusted consumption background priority.
        /// </summary>
        /// <value>
        /// The cp adjusted consumption background priority.
        /// </value>
        public int CPAdjustedConsumptionBackgroundPriority { get; set; }


        /// <summary>
        /// Gets or sets the cp orders speed.
        /// </summary>
        /// <value>
        /// The cp orders speed.
        /// </value>
        public decimal? CPOrdersSpeed { get; set; }


        /// <summary>
        /// Gets or sets the cp orders speed priority.
        /// </summary>
        /// <value>
        /// The cp orders speed priority.
        /// </value>
        public int CPOrdersSpeedPriority { get; set; }


        /// <summary>
        /// Gets or sets the cp orders speed background priority.
        /// </summary>
        /// <value>
        /// The cp orders speed background priority.
        /// </value>
        public int CPOrdersSpeedBackgroundPriority { get; set; }


        /// <summary>
        /// Gets or sets the cp orders consumption.
        /// </summary>
        /// <value>
        /// The cp orders consumption.
        /// </value>
        public decimal? CPOrdersConsumption { get; set; }


        /// <summary>
        /// Gets or sets the cp orders consumption priority.
        /// </summary>
        /// <value>
        /// The cp orders consumption priority.
        /// </value>
        public int CPOrdersConsumptionPriority { get; set; }


        /// <summary>
        /// Gets or sets the cp orders consumption background priority.
        /// </summary>
        /// <value>
        /// The cp orders consumption background priority.
        /// </value>
        public int CPOrdersConsumptionBackgroundPriority { get; set; }

        /// <summary>
        /// Gets or sets the pos id.
        /// </summary>
        /// <value>
        /// The cp orders pos id.
        /// </value>
        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the rank id.
        /// </summary>
        /// <value>
        /// The cp orders rank id.
        /// </value>
        public int RankId { get; set; }
    }
}
