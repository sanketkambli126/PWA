using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Data contract for crew treatment detail.
    /// </summary>
    public class CrewTreatmentDetail
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the medical manager identifier.
        /// </summary>
        /// <value>
        /// The medical manager identifier.
        /// </value>
        public string MedicalManagerId { get; set; }

        /// <summary>
        /// Gets or sets the ida identifier.
        /// </summary>
        /// <value>
        /// The ida identifier.
        /// </value>
        public string IdaId { get; set; }

        /// <summary>
        /// Gets or sets the injury detail.
        /// </summary>
        /// <value>
        /// The injury detail.
        /// </value>
        public string InjuryDetail { get; set; }

        /// <summary>
        /// Gets or sets the injury treatment.
        /// </summary>
        /// <value>
        /// The injury treatment.
        /// </value>
        public string InjuryTreatment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is crew off signed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is crew off signed; otherwise, <c>false</c>.
        /// </value>
        public bool IsCrewOffSigned { get; set; }

        /// <summary>
        /// Gets or sets the off signed date.
        /// </summary>
        /// <value>
        /// The off signed date.
        /// </value>
        public DateTime? OffSignedDate { get; set; }

        /// <summary>
        /// Gets or sets the off signed port identifier.
        /// </summary>
        /// <value>
        /// The off signed port identifier.
        /// </value>

        public string OffSignedPortId { get; set; }

        /// <summary>
        /// Gets or sets the name of the off signed port.
        /// </summary>
        /// <value>
        /// The name of the off signed port.
        /// </value>

        public string OffSignedPortName { get; set; }

        /// <summary>
        /// Gets or sets the off signed country code.
        /// </summary>
        /// <value>
        /// The off signed country code.
        /// </value>

        public string OffSignedCountryCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is crew treated ashore.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is crew treated ashore; otherwise, <c>false</c>.
        /// </value>

        public bool IsCrewTreatedAshore { get; set; }

        /// <summary>
        /// Gets or sets the treatment date.
        /// </summary>
        /// <value>
        /// The treatment date.
        /// </value>

        public DateTime? TreatmentDate { get; set; }

        /// <summary>
        /// Gets or sets the treatment port identifier.
        /// </summary>
        /// <value>
        /// The treatment port identifier.
        /// </value>

        public string TreatmentPortId { get; set; }

        /// <summary>
        /// Gets or sets the name of the treatment port.
        /// </summary>
        /// <value>
        /// The name of the treatment port.
        /// </value>

        public string TreatmentPortName { get; set; }

        /// <summary>
        /// Gets or sets the treatment country code.
        /// </summary>
        /// <value>
        /// The treatment country code.
        /// </value>

        public string TreatmentCountryCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [drug alcohol tested].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [drug alcohol tested]; otherwise, <c>false</c>.
        /// </value>

        public bool DrugAlcoholTested { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [test result].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [test result]; otherwise, <c>false</c>.
        /// </value>

        public bool TestResult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has crew resumed work.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has crew resumed work; otherwise, <c>false</c>.
        /// </value>

        public bool HasCrewResumedWork { get; set; }

        /// <summary>
        /// Gets or sets the resumed date.
        /// </summary>
        /// <value>
        /// The resumed date.
        /// </value>
        public DateTime? ResumedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hours complaint].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hours complaint]; otherwise, <c>false</c>.
        /// </value>
        public bool HoursComplaint { get; set; }

        /// <summary>
        /// Gets or sets the number of days off.
        /// </summary>
        /// <value>
        /// The number of days off.
        /// </value>
        public decimal? NumberOfDaysOff { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [first aid given on board].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [first aid given on board]; otherwise, <c>false</c>.
        /// </value>
        public bool FirstAidGivenOnBoard { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the dependent detail.
        /// </summary>
        /// <value>
        /// The dependent detail.
        /// </value>
        public string DependentDetail { get; set; }
    }
}
