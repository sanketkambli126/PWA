using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// The passenger treatment detail
    /// </summary>
    public class PassengerTreatmentDetail
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
        /// Gets or sets the idp identifier.
        /// </summary>
        /// <value>
        /// The idp identifier.
        /// </value>
        public string IdpId { get; set; }

        /// <summary>
        /// Gets or sets the medical manager identifier.
        /// </summary>
        /// <value>
        /// The medical manager identifier.
        /// </value>
        public string MedicalManagerId { get; set; }

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
        /// Gets or sets the disembarked port identifier.
        /// </summary>
        /// <value>
        /// The disembarked port identifier.
        /// </value>
        public string DisembarkedPortId { get; set; }

        /// <summary>
        /// Gets or sets the name of the disembarked port.
        /// </summary>
        /// <value>
        /// The name of the disembarked port.
        /// </value>
        public string DisembarkedPortName { get; set; }

        /// <summary>
        /// Gets or sets the disembarked country code.
        /// </summary>
        /// <value>
        /// The disembarked country code.
        /// </value>
        public string DisembarkedCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the disembarked date.
        /// </summary>
        /// <value>
        /// The disembarked date.
        /// </value>
        public DateTime? DisembarkedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [sent to shore doctor].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sent to shore doctor]; otherwise, <c>false</c>.
        /// </value>
        public bool SentToShoreDoctor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [x rays recommended].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [x rays recommended]; otherwise, <c>false</c>.
        /// </value>
        public bool XRaysRecommended { get; set; }

        /// <summary>
        /// Gets or sets the test result.
        /// </summary>
        /// <value>
        /// The test result.
        /// </value>
        public string TestResult { get; set; }

        /// <summary>
        /// Gets or sets the doctor name and address.
        /// </summary>
        /// <value>
        /// The doctor name and address.
        /// </value>
        public string DoctorNameAndAddress { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the incapacity start date.
        /// </summary>
        /// <value>
        /// The incapacity start date.
        /// </value>
        public DateTime? IncapacityStartDate { get; set; }

        /// <summary>
        /// Gets or sets the incapacity return date.
        /// </summary>
        /// <value>
        /// The incapacity return date.
        /// </value>
        public DateTime? IncapacityReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the visits.
        /// </summary>
        /// <value>
        /// The visits.
        /// </value>
        public List<IncidentVisit> Visits { get; set; }
    }
}
