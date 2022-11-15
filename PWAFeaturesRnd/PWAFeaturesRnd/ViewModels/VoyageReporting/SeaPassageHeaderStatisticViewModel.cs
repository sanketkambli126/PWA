using PWAFeaturesRnd.Common;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// Sea Passage Header Statistic ViewModel
    /// </summary>
    public class SeaPassageHeaderStatisticViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the first count.
        /// </summary>
        /// <value>
        /// The first count.
        /// </value>
        public decimal FirstCount { get; set; }

        /// <summary>
        /// Gets or sets the second count.
        /// </summary>
        /// <value>
        /// The second count.
        /// </value>
        public decimal SecondCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum speed.
        /// </summary>
        /// <value>
        /// The maximum speed.
        /// </value>
        public decimal MaxSpeed { get; set; }

        /// <summary>
        /// The maximum speed
        /// </summary>
        private const decimal MaximumSpeed = 60;

        /// <summary>
        /// Gets or sets the name of the charter.
        /// </summary>
        /// <value>
        /// The name of the charter.
        /// </value>
        public string CharterName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is greater.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is greater; otherwise, <c>false</c>.
        /// </value>
        public bool IsGreater { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SeaPassageHeaderStatisticViewModel"/> class.
        /// </summary>
        public SeaPassageHeaderStatisticViewModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeaPassageHeaderStatisticViewModel"/> class.
        /// </summary>
        /// <param name="Entity">The entity.</param>
        /// <param name="IsVesselLoadedFlag">if set to <c>true</c> [is vessel loaded flag].</param>
        /// <param name="AverageSpeed">The average speed.</param>
        public SeaPassageHeaderStatisticViewModel(SeaPassageReportDetailsViewModel Entity, bool IsVesselLoadedFlag, float AverageSpeed)
        {
            FirstCount = IsVesselLoadedFlag ? (decimal)Entity.SpeedCharterRequirementLoaded : (decimal)Entity.SpeedCharterRequirementBallast;
            SecondCount = (decimal)AverageSpeed;
            MaxSpeed = FirstCount > MaximumSpeed ? FirstCount : MaximumSpeed;
            CharterName = IsVesselLoadedFlag ? Constants.CharterLoaded : Constants.CharterBallast;
        }

        #endregion
    }
}
