using System;
using PWAFeaturesRnd.Models.Report.Crew;

namespace PWAFeaturesRnd.ViewModels.Crew
{
    /// <summary>
    /// Medical Sign Off Response View Model
    /// </summary>
    public class MedicalSignOffResponseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>
        public string RankId { get; set; }

        /// <summary>
        /// Gets or sets the rank description.
        /// </summary>
        /// <value>
        /// The rank description.
        /// </value>
        public string RankDescription { get; set; }

        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the nationality.
        /// </summary>
        /// <value>
        /// The nationality.
        /// </value>
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the onboard days.
        /// </summary>
        /// <value>
        /// The onboard days.
        /// </value>
        public int? OnboardDays { get; set; }

        /// <summary>
        /// Gets or sets the on board days display.
        /// </summary>
        /// <value>
        /// The on board days display.
        /// </value>
        public string OnBoardDaysDisplay { get; set; }

        /// <summary>
        /// Gets or sets the sign on.
        /// </summary>
        /// <value>
        /// The sign on.
        /// </value>
        public DateTime? SignOn { get; set; }

        /// <summary>
        /// Gets or sets the sign off.
        /// </summary>
        /// <value>
        /// The sign off.
        /// </value>
        public DateTime? SignOff { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the port off.
        /// </summary>
        /// <value>
        /// The port off.
        /// </value>
        public string PortOff { get; set; }

        /// <summary>
        /// Gets or sets the country off.
        /// </summary>
        /// <value>
        /// The country off.
        /// </value>
        public string CountryOff { get; set; }

        /// <summary>
        /// Gets or sets the current status description.
        /// </summary>
        /// <value>
        /// The current status description.
        /// </value>
        public string CurrentStatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the status end date.
        /// </summary>
        /// <value>
        /// The status end date.
        /// </value>
        public DateTime? StatusEndDate { get; set; }

        /// <summary>
        /// Gets or sets the full name of the crew.
        /// </summary>
        /// <value>
        /// The full name of the crew.
        /// </value>
        public string CrewFullName { get; set; }

        /// <summary>
        /// Gets or sets the encrypted crew identifier.
        /// </summary>
        /// <value>
        /// The encrypted crew identifier.
        /// </value>
        public string EncryptedCrewId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is crew name visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is crew name visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsCrewNameVisible { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalSignOffResponseViewModel"/> class.
        /// </summary>
        public MedicalSignOffResponseViewModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalSignOffResponseViewModel"/> class.
        /// </summary>
        /// <param name="Entity">The entity.</param>
        public MedicalSignOffResponseViewModel(MedicalSignOffResponse Entity)
        {
            if (Entity != null)
            {
                RankDescription = Entity.RankDescription ?? "";
                RankId = Entity.RankId ?? "";
                CrewFullName = SetCrewFullName(Entity);
                Nationality = Entity.Nationality ?? "";
                OnboardDays = Entity.OnboardDays.GetValueOrDefault();
                OnBoardDaysDisplay = Entity.OnboardDays.HasValue ? (Entity.OnboardDays + " D") : "-";
                SignOn = Entity.SignOn;
                SignOff = Entity.SignOff;
                Reason = Entity.Reason ?? "";
                PortOff = Entity.PortOff ?? "";
                CountryOff = Entity.CountryOff ?? "";
                CurrentStatusDescription = Entity.CurrentStatusDescription ?? "";
                StatusEndDate = Entity.StatusEndDate;
                CrewId = Entity.CrewId;
                IsCrewNameVisible = !string.IsNullOrWhiteSpace(CrewFullName) ? true : false;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sets the full name of the crew.
        /// </summary>
        /// <param name="crew">The crew.</param>
        /// <returns></returns>
        public string SetCrewFullName(MedicalSignOffResponse crew)
        {
            string CrewFullName = "";

            if (string.IsNullOrWhiteSpace(crew.CrewSurname))
            {
                CrewFullName = !string.IsNullOrWhiteSpace(crew.CrewForename) ? crew.CrewForename + " " + crew.CrewMiddleName : crew.CrewMiddleName;
            }
            else
            {
                CrewFullName = crew.CrewSurname + ", " + (!string.IsNullOrWhiteSpace(crew.CrewForename) ? crew.CrewForename + " " + crew.CrewMiddleName : crew.CrewMiddleName);
            }
            return CrewFullName;
        }


        #endregion
    }
}
