using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// SeapassageNoonSynopsis
    /// </summary>
    public class SeapassageNoonSynopsisViewModel
    {
        /// <summary>
        /// Gets or sets the SPN identifier.
        /// </summary>
        /// <value>
        /// The SPN identifier.
        /// </value>
        public string SpnId { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>
        public string SpaId { get; set; }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>
        /// The type identifier.
        /// </value>
        public string TypeId { get; set; }
        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public string FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public string ToDate { get; set; }

        /// <summary>
        /// Gets or sets the time difference.
        /// </summary>
        /// <value>
        /// The time difference.
        /// </value>
        public decimal? TimeDifference { get; set; }

        /// <summary>
        /// Gets or sets the distance on passage.
        /// </summary>
        /// <value>
        /// The distance on passage.
        /// </value>
        public decimal? DistanceOnPassage { get; set; }

        /// <summary>
        /// Gets or sets the distance by engine.
        /// </summary>
        /// <value>
        /// The distance by engine.
        /// </value>
        public decimal? DistanceByEngine { get; set; }

        /// <summary>
        /// Gets or sets the average speed.
        /// </summary>
        /// <value>
        /// The average speed.
        /// </value>
        public decimal? AvgSpeed { get; set; }

        /// <summary>
        /// Gets or sets the master ordered speed.
        /// </summary>
        /// <value>
        /// The master ordered speed.
        /// </value>
        public decimal? MasterOrderedSpeed { get; set; }
        /// <summary>
        /// Gets or sets the speed over ground.
        /// </summary>
        /// <value>
        /// The speed over ground.
        /// </value>
        public decimal? SpeedOverGround { get; set; }

        /// <summary>
        /// Gets or sets the course.
        /// </summary>
        /// <value>
        /// The course.
        /// </value>
        public decimal? Course { get; set; }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>
        /// The name of the type.
        /// </value>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the PLK identifier significant current.
        /// </summary>
        /// <value>
        /// The PLK identifier significant current.
        /// </value>
        public string PlkIdSignificantCurrent { get; set; }

        /// <summary>
        /// Gets or sets the effect.
        /// </summary>
        /// <value>
        /// The effect.
        /// </value>
        public decimal? Effect { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the type of the selected synopsis.
        /// </summary>
        /// <value>
        /// The type of the selected synopsis.
        /// </value>
        public string SelectedSynopsisType { get; set; }

    }
}
