using Microsoft.AspNetCore.DataProtection;
using System;
using PWAFeaturesRnd.Models.Report.VoyageReporting;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// 
    /// </summary>
    public class VoyageActivityBadWeatherDetailViewModel
    {
        #region Private Properties

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;


        #endregion

        #region Constants

        /// <summary>
        /// The activty type sea passage
        /// </summary>
        private const string ActivtyTypeSeaPassage = "SP";

        #endregion

        #region Model Properties

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
		public string VES_ID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bad weather alert].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [bad weather alert]; otherwise, <c>false</c>.
        /// </value>
        public bool BadWeatherAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bad weather].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [bad weather]; otherwise, <c>false</c>.
        /// </value>
        public bool BadWeather { get; set; }

        /// <summary>
        /// Gets or sets the distance log.
        /// </summary>
        /// <value>
        /// The distance log.
        /// </value>
        public decimal? DistanceLog { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is break in passage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is break in passage; otherwise, <c>false</c>.
        /// </value>
        public bool IsBreakInPassage { get; set; }

        /// <summary>
        /// Gets or sets the distance on passage.
        /// </summary>
        /// <value>
        /// The distance on passage.
        /// </value>
        public decimal? DistanceOnPassage { get; set; }

        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>
        public string SpaId { get; set; }

        /// <summary>
        /// Gets or sets the spa date.
        /// </summary>
        /// <value>
        /// The spa date.
        /// </value>
        public DateTime? SpaDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is IDL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is IDL; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdl { get; set; }

        /// <summary>
        /// Gets or sets the voyage reporting modal request URL.
        /// </summary>
        /// <value>
        /// The voyage reporting modal request URL.
        /// </value>
        public string VoyageReportingModalRequestURL { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the pla identifier.
        /// </summary>
        /// <value>
        /// The pla identifier.
        /// </value>
        public string PlaId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is break and bad weather alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is break and bad weather alert; otherwise, <c>false</c>.
        /// </value>
        public bool IsBreakAndBadWeatherAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is only bad weather alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is only bad weather alert; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnlyBadWeatherAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is only break alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is only break alert; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnlyBreakAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is only port bad weather alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is only port bad weather alert; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnlyPortBadWeatherAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is only delay alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is only delay alert; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnlyDelayAlert { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public decimal? Distance { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VoyageActivityBadWeatherDetailViewModel"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public VoyageActivityBadWeatherDetailViewModel(IDataProtectionProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoyageActivityBadWeatherDetailViewModel"/> class.
        /// </summary>
        /// <param name="Entity">The entity.</param>
        /// <param name="Pla_Id">The pla identifier.</param>
        /// <param name="provider">The provider.</param>
        public VoyageActivityBadWeatherDetailViewModel(VoyageActivityBadWeatherDetail Entity, string Pla_Id, IDataProtectionProvider provider)
        {
            _provider = provider;

            PlaId = Pla_Id;
            Distance = (Entity.DistanceLog - (Entity.DistanceOnPassage / 2));

            VES_ID = Entity.VES_ID;
            BadWeatherAlert = Entity.BadWeatherAlert;
            BadWeather = Entity.BadWeather;
            DistanceLog = Entity.DistanceLog;
            IsIdl = Entity.IsIdl;
            SpaDate = Entity.SpaDate;
            SpaId = Entity.SpaId;
            DistanceOnPassage = Entity.DistanceOnPassage;
            IsBreakInPassage = Entity.IsBreakInPassage;

            IsBreakAndBadWeatherAlert = (string.IsNullOrWhiteSpace(PlaId) || Equals(PlaId, ActivtyTypeSeaPassage)) && IsBreakInPassage && BadWeatherAlert;
            IsOnlyBreakAlert = (string.IsNullOrWhiteSpace(PlaId) || Equals(PlaId, ActivtyTypeSeaPassage)) && IsBreakInPassage && !BadWeatherAlert;
            IsOnlyBadWeatherAlert = (string.IsNullOrWhiteSpace(PlaId) || Equals(PlaId, ActivtyTypeSeaPassage)) && BadWeatherAlert && !IsBreakInPassage;
            IsOnlyPortBadWeatherAlert = !string.IsNullOrWhiteSpace(PlaId) && !Equals(PlaId, ActivtyTypeSeaPassage) && BadWeatherAlert && !IsBreakInPassage;
            IsOnlyDelayAlert = !string.IsNullOrWhiteSpace(PlaId) && !Equals(PlaId, ActivtyTypeSeaPassage) && IsBreakInPassage && !BadWeatherAlert;

            //bad weather request url
            VoyageReportingModalRequestViewModel voyageReportingRequestVM = new VoyageReportingModalRequestViewModel();
            voyageReportingRequestVM.IsBreakInPassage = Entity.IsBreakInPassage;
            voyageReportingRequestVM.SpaId = SpaId;
            voyageReportingRequestVM.BadWeatherAlert = Entity.BadWeatherAlert;

            VoyageReportingModalRequestURL = _provider.CreateProtector("VoyageReportingModalURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequestVM));
        }

        #endregion

        #region Method

        #endregion
    }
}
