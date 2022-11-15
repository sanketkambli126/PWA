namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
     /// <summary>
     /// The data contract for passenger doctor's report view model.
     /// </summary>
    public class PassengerDoctorsReportViewModel
    {
        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the medical manager identifier.
        /// </summary>
        /// <value>
        /// The medical manager identifier.
        /// </value>
        public string MedicalManagerId { get; set; }

        /// <summary>
        /// Gets or sets the idp identifier.
        /// </summary>
        /// <value>
        /// The idp identifier.
        /// </value>
        public string IdpId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is first aid administered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is first aid administered; otherwise, <c>false</c>.
        /// </value>
        public string IsFirstAidAdministered { get; set; }

        /// <summary>
        /// Gets or sets the first aid administered by.
        /// </summary>
        /// <value>
        /// The first aid administered by.
        /// </value>
        public string FirstAidAdministeredBy { get; set; }

        /// <summary>
        /// Gets or sets the first aid administered by CRW identifier tp.
        /// </summary>
        /// <value>
        /// The first aid administered by CRW identifier tp.
        /// </value>
        public string FirstAidAdministeredByCrwIdTp { get; set; }

        /// <summary>
        /// Gets or sets the first aid administered by forename.
        /// </summary>
        /// <value>
        /// The first aid administered by forename.
        /// </value>
        public string FirstAidAdministeredByForename { get; set; }

        /// <summary>
        /// Gets or sets the first aid administered by surname.
        /// </summary>
        /// <value>
        /// The first aid administered by surname.
        /// </value>
        public string FirstAidAdministeredBySurname { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is resuscitation equipment available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is resuscitation equipment available; otherwise, <c>false</c>.
        /// </value>
        public string IsResuscitationEquipmentAvailable { get; set; }

        /// <summary>
        /// Gets or sets the equipment location.
        /// </summary>
        /// <value>
        /// The equipment location.
        /// </value>
        public string EquipmentLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [at authorised place].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [at authorised place]; otherwise, <c>false</c>.
        /// </value>
        public string AtAuthorisedPlace { get; set; }

        /// <summary>
        /// Gets or sets the shoes description.
        /// </summary>
        /// <value>
        /// The shoes description.
        /// </value>
        public string ShoesDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has consumed drug or alcohol.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has consumed drug or alcohol; otherwise, <c>false</c>.
        /// </value>
        public string HasConsumedDrugOrAlcohol { get; set; }

        /// <summary>
        /// Gets or sets the alcohol consumed.
        /// </summary>
        /// <value>
        /// The alcohol consumed.
        /// </value>
        public string AlcoholConsumed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has spectacle.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has spectacle; otherwise, <c>false</c>.
        /// </value>
        public string HasSpectacle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [spectacles worn].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [spectacles worn]; otherwise, <c>false</c>.
        /// </value>
        public string SpectaclesWorn { get; set; }

        /// <summary>
        /// Gets or sets the doctors diagnosis.
        /// </summary>
        /// <value>
        /// The doctors diagnosis.
        /// </value>
        public string DoctorsDiagnosis { get; set; }
    }
}
