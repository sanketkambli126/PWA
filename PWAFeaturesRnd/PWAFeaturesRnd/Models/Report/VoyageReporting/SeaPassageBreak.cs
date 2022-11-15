using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// SeaPassageBreak
    /// </summary>
    public class SeaPassageBreak
    {



        /// <summary>
        /// Gets or sets the SPB out serv.
        /// </summary>
        /// <value>
        /// The SPB out serv.
        /// </value>
        public string SpbOutServ { get; set; }



        /// <summary>
        /// Gets or sets the SPB identifier.
        /// </summary>
        /// <value>
        /// The SPB identifier.
        /// </value>
        public string SpbId { get; set; }



        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>
        public string SpaId { get; set; }



        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }


        /// <summary>
        /// Gets or sets the type of the PLK identifier off hire.
        /// </summary>
        /// <value>
        /// The type of the PLK identifier off hire.
        /// </value>
        public string PlkIdOffHireType { get; set; }


        /// <summary>
        /// Gets or sets the type of the PLK identifier break in passage.
        /// </summary>
        /// <value>
        /// The type of the PLK identifier break in passage.
        /// </value>
        public string PlkIdBreakInPassageType { get; set; }


        /// <summary>
        /// Gets or sets the pla identifier.
        /// </summary>
        /// <value>
        /// The pla identifier.
        /// </value>
        public string PlaId { get; set; }

        /// <summary>
        /// Gets or sets the out distance.
        /// </summary>
        /// <value>
        /// The out distance.
        /// </value>
        public float? OutDistance { get; set; }

        /// <summary>
        /// Gets or sets the lsfo.
        /// </summary>
        /// <value>
        /// The lsfo.
        /// </value>
        public float? Lsfo { get; set; }

        /// <summary>
        /// Gets or sets the LNG.
        /// </summary>
        /// <value>
        /// The LNG.
        /// </value>
        public decimal? Lng { get; set; }

        /// <summary>
        /// Gets or sets the is out of service.
        /// </summary>
        /// <value>
        /// The is out of service.
        /// </value>
        public byte? IsOutOfService { get; set; }

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the go.
        /// </summary>
        /// <value>
        /// The go.
        /// </value>
        public float? Go { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public DateTime? From { get; set; }

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
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }


        /// <summary>
        /// Gets or sets the type of the activity type name.
        /// </summary>
        /// <value>
        /// The type of the activity type name.
        /// </value>
        public string ActivityTypeNameType { get; set; }



        /// <summary>
        /// Gets or sets the activity type name identifier.
        /// </summary>
        /// <value>
        /// The activity type name identifier.
        /// </value>
        public string ActivityTypeNameId { get; set; }


        /// <summary>
        /// Gets or sets the name of the activity type.
        /// </summary>
        /// <value>
        /// The name of the activity type.
        /// </value>
        public string ActivityTypeName { get; set; }

        /// <summary>
        /// Gets or sets the break time.
        /// </summary>
        /// <value>
        /// The break time.
        /// </value>
        public TimeSpan? BreakTime { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public DateTime? To { get; set; }



        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the break in passage type.
        /// </summary>
        /// <value>
        /// The name of the break in passage type.
        /// </value>
        public string BreakInPassageTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the off hire type.
        /// </summary>
        /// <value>
        /// The name of the off hire type.
        /// </value>
        public string OffHireTypeName { get; set; }

        /// <summary>
        /// Gets or sets the SPB lngcargo.
        /// </summary>
        /// <value>
        /// The SPB lngcargo.
        /// </value>
        public decimal? SpbLngcargo { get; set; }

    }
}
