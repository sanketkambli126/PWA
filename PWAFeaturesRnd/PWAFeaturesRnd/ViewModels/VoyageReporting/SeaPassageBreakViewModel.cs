using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// SeaPassageBreakViewModel
    /// </summary>
    public class SeaPassageBreakViewModel
    {

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public DateTime? From { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public DateTime? To { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [off hire].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [off hire]; otherwise, <c>false</c>.
        /// </value>
        public bool OffHire { get; set; }

        /// <summary>
        /// Gets or sets the type of the off hire.
        /// </summary>
        /// <value>
        /// The type of the off hire.
        /// </value>
        public string OffHireType { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is delay.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is delay; otherwise, <c>false</c>.
        /// </value>
        public bool IsDelay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is medical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is medical; otherwise, <c>false</c>.
        /// </value>
        public bool IsMedical { get; set; }

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the out distance.
        /// </summary>
        /// <value>
        /// The out distance.
        /// </value>
        public float? OutDistance { get; set; }

        /// <summary>
        /// Gets or sets the fo.
        /// </summary>
        /// <value>
        /// The fo.
        /// </value>
        public float? Fo { get; set; }

        /// <summary>
        /// Gets or sets the do.
        /// </summary>
        /// <value>
        /// The do.
        /// </value>
        public float? Do { get; set; }

        /// <summary>
        /// Gets or sets the lsfo.
        /// </summary>
        /// <value>
        /// The lsfo.
        /// </value>
        public float? Lsfo { get; set; }

        /// <summary>
        /// Gets or sets the go.
        /// </summary>
        /// <value>
        /// The go.
        /// </value>
        public float? Go { get; set; }


        /// <summary>
        /// Gets or sets the LNG.
        /// </summary>
        /// <value>
        /// The LNG.
        /// </value>
        public decimal? Lng { get; set; }

        /// <summary>
        /// Gets or sets the SPB lngcargo.
        /// </summary>
        /// <value>
        /// The SPB lngcargo.
        /// </value>
        public decimal? SpbLngcargo { get; set; }

        /// <summary>
        /// Gets or sets the is out of service.
        /// </summary>
        /// <value>
        /// The is out of service.
        /// </value>
        public byte? IsOutOfService { get; set; }

        /// <summary>
        /// Gets or sets the break time d.
        /// </summary>
        /// <value>
        /// The break time d.
        /// </value>
        public string BreakTimeD { get; set; }

        /// <summary>
        /// Gets or sets the break time.
        /// </summary>
        /// <value>
        /// The break time.
        /// </value>
        public TimeSpan? BreakTime { get; set; }

        /// <summary>
        /// Gets or sets the type of the PLK identifier break in passage.
        /// </summary>
        /// <value>
        /// The type of the PLK identifier break in passage.
        /// </value>
        public string PlkIdBreakInPassageType { get; set; }
    }
}
