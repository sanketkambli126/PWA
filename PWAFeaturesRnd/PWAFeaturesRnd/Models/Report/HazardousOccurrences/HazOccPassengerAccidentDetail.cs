using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// The hazocc passenger accident detail
    /// </summary>
    public class HazOccPassengerAccidentDetail
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
        /// Gets or sets the idp identifier.
        /// </summary>
        /// <value>
        /// The idp identifier.
        /// </value>
        public string IdpId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the nationality.
        /// </summary>
        /// <value>
        /// The nationality.
        /// </value>
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the passport number.
        /// </summary>
        /// <value>
        /// The passport number.
        /// </value>
        public string PassportNumber { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the cabin number.
        /// </summary>
        /// <value>
        /// The cabin number.
        /// </value>
        public string CabinNumber { get; set; }

        /// <summary>
        /// Gets or sets the occupation.
        /// </summary>
        /// <value>
        /// The occupation.
        /// </value>
        public string Occupation { get; set; }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        /// <value>
        /// The marital status.
        /// </value>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the telephone number.
        /// </summary>
        /// <value>
        /// The telephone number.
        /// </value>

        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the booking number.
        /// </summary>
        /// <value>
        /// The booking number.
        /// </value>
        public string BookingNumber { get; set; }

        /// <summary>
        /// Gets or sets the embarked in port identifier.
        /// </summary>
        /// <value>
        /// The embarked in port identifier.
        /// </value>

        public string EmbarkedInPortId { get; set; }

        /// <summary>
        /// Gets or sets the name of the embarked in port.
        /// </summary>
        /// <value>
        /// The name of the embarked in port.
        /// </value>
        public string EmbarkedInPortName { get; set; }

        /// <summary>
        /// Gets or sets the embarked in country code.
        /// </summary>
        /// <value>
        /// The embarked in country code.
        /// </value>
        public string EmbarkedInCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the embarked date.
        /// </summary>
        /// <value>
        /// The embarked date.
        /// </value>
        public DateTime? EmbarkedDate { get; set; }

        /// <summary>
        /// Gets or sets the disembarked at port identifier.
        /// </summary>
        /// <value>
        /// The disembarked at port identifier.
        /// </value>
        public string DisembarkedAtPortId { get; set; }

        /// <summary>
        /// Gets or sets the name of the disembarked in port.
        /// </summary>
        /// <value>
        /// The name of the disembarked in port.
        /// </value>
        public string DisembarkedInPortName { get; set; }

        /// <summary>
        /// Gets or sets the disembarked at country code.
        /// </summary>
        /// <value>
        /// The disembarked at country code.
        /// </value>

        public string DisembarkedAtCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the disembarked date.
        /// </summary>
        /// <value>
        /// The disembarked date.
        /// </value>

        public DateTime? DisembarkedDate { get; set; }

        /// <summary>
        /// Gets or sets the travel agent.
        /// </summary>
        /// <value>
        /// The travel agent.
        /// </value>

        public string TravelAgent { get; set; }

        /// <summary>
        /// Gets or sets the first name of the companion.
        /// </summary>
        /// <value>
        /// The first name of the companion.
        /// </value>

        public string CompanionFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the companion.
        /// </summary>
        /// <value>
        /// The last name of the companion.
        /// </value>

        public string CompanionLastName { get; set; }

        /// <summary>
        /// Gets or sets the relationship.
        /// </summary>
        /// <value>
        /// The relationship.
        /// </value>

        public string Relationship { get; set; }

        /// <summary>
        /// Gets or sets the companion nationality.
        /// </summary>
        /// <value>
        /// The companion nationality.
        /// </value>

        public string CompanionNationality { get; set; }

        /// <summary>
        /// Gets or sets the companion telephone number.
        /// </summary>
        /// <value>
        /// The companion telephone number.
        /// </value>

        public string CompanionTelephoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the companion address.
        /// </summary>
        /// <value>
        /// The companion address.
        /// </value>

        public string CompanionAddress { get; set; }

        /// <summary>
        /// Gets or sets the parent report identifier.
        /// </summary>
        /// <value>
        /// The parent report identifier.
        /// </value>

        public string ParentReportId { get; set; }
    }
}
