using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// Data contract for crew doctors report.
    /// </summary>
    public class CrewDoctorsReportViewModel
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
        /// Gets or sets the name of the doctor.
        /// </summary>
        /// <value>
        /// The name of the doctor.
        /// </value>
        public string DoctorName { get; set; }

        /// <summary>
        /// Gets or sets the restriction days.
        /// </summary>
        /// <value>
        /// The restriction days.
        /// </value>
        public int? RestrictionDays { get; set; }

        /// <summary>
        /// Gets or sets the disability days.
        /// </summary>
        /// <value>
        /// The disability days.
        /// </value>
        public int? DisabilityDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hospitalisation required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hospitalisation required]; otherwise, <c>false</c>.
        /// </value>
        public string HospitalisationRequired { get; set; }

        /// <summary>
        /// Gets or sets the symptom description.
        /// </summary>
        /// <value>
        /// The symptom description.
        /// </value>
        public string SymptomDescription { get; set; }

        /// <summary>
        /// Gets or sets the doctor address.
        /// </summary>
        /// <value>
        /// The doctor address.
        /// </value>
        public string DoctorAddress { get; set; }

        /// <summary>
        /// Gets or sets the hospital detail.
        /// </summary>
        /// <value>
        /// The hospital detail.
        /// </value>
        public string HospitalDetail { get; set; }

        /// <summary>
        /// Gets or sets the visits.
        /// </summary>
        /// <value>
        /// The visits.
        /// </value>
        public List<DoctorVisitReportViewModel> Visits { get; set; }
    }
}
