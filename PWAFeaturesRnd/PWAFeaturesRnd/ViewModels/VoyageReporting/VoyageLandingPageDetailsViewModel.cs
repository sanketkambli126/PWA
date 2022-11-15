using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Report.VoyageReporting;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// 
    /// </summary>
    public class VoyageLandingPageDetailsViewModel
    {
        #region Private Properties

        /// <summary>
        /// The provier
        /// </summary>
        private IDataProtectionProvider _provier;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sea passage event.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sea passage event; otherwise, <c>false</c>.
        /// </value>
        public bool IsSeaPassageEvent { get; set; }

        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        /// <value>
        /// The request URL.
        /// </value>
        public string RequestURL { get; set; }

        /// <summary>
        /// Gets or sets from request URL.
        /// </summary>
        /// <value>
        /// From request URL.
        /// </value>
        public string FromRequestURL { get; set; }

        /// <summary>
        /// Converts to requesturl.
        /// </summary>
        /// <value>
        /// To request URL.
        /// </value>
        public string ToRequestURL { get; set; }

        /// <summary>
        /// The FromAgentRequestURL
        /// </summary>
        public string FromAgentRequestURL { get; set; }

        /// <summary>
        /// The ToAgentRequestURL
        /// </summary>
        public string ToAgentRequestURL { get; set; }

        /// <summary>
        /// Gets or sets from header section.
        /// </summary>
        /// <value>
        /// From header section.
        /// </value>
        public string FromHeaderSection { get; set; }

        /// <summary>
        /// Gets or sets from faop value.
        /// </summary>
        /// <value>
        /// From faop value.
        /// </value>
        public string FromFAOPValue { get; set; }

        /// <summary>
        /// Gets or sets the name of from port.
        /// </summary>
        /// <value>
        /// The name of from port.
        /// </value>
        public string FromPortName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has from port alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has from port alert; otherwise, <c>false</c>.
        /// </value>
        public bool HasFromPortAlert { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public string FromDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is agent available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is agent available; otherwise, <c>false</c>.
        /// </value>
        public bool IsAgentAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is from to visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is from to visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsFromToVisible { get; set; }

        /// <summary>
        /// Converts to headersection.
        /// </summary>
        /// <value>
        /// To header section.
        /// </value>
        public string ToHeaderSection { get; set; }

        /// <summary>
        /// Converts to eospvalue.
        /// </summary>
        /// <value>
        /// To eosp value.
        /// </value>
        public string ToEOSPValue { get; set; }

        /// <summary>
        /// Converts to portname.
        /// </summary>
        /// <value>
        /// The name of to port.
        /// </value>
        public string ToPortName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is to port alert visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is to port alert visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsToPortAlertVisible { get; set; }

        /// <summary>
        /// Converts to portdate.
        /// </summary>
        /// <value>
        /// To port date.
        /// </value>
        public string ToPortDate { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>
        public decimal TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the distance travelled.
        /// </summary>
        /// <value>
        /// The distance travelled.
        /// </value>
        public decimal DistanceTravelled { get; set; }

        /// <summary>
        /// Gets or sets the bad weather details.
        /// </summary>
        /// <value>
        /// The bad weather details.
        /// </value>
        public List<VoyageActivityBadWeatherDetailViewModel> BadWeatherDetails { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string POS_ID { get; set; }

        /// <summary>
        /// Gets or sets the is delay.
        /// </summary>
        /// <value>
        /// The is delay.
        /// </value>
        public bool? IsDelay { get; set; }

        /// <summary>
        /// Gets or sets the is port bad weather.
        /// </summary>
        /// <value>
        /// The is port bad weather.
        /// </value>
        public bool? IsPortBadWeather { get; set; }

        /// <summary>
        /// Gets or sets the pla identifier.
        /// </summary>
        /// <value>
        /// The pla identifier.
        /// </value>
        public string PLA_ID { get; set; }

        /// <summary>
        /// Gets or sets the last event position.
        /// </summary>
        /// <value>
        /// The last event position.
        /// </value>
        public string LastEventPosition { get; set; }

        /// <summary>
        /// Gets or sets the remaining value.
        /// </summary>
        /// <value>
        /// The remaining value.
        /// </value>
        public decimal RemainingValue { get; set; }

        /// <summary>
        /// Gets or sets the progress bar end status.
        /// </summary>
        /// <value>
        /// The progress bar end status.
        /// </value>
        public string ProgressBarEndStatus { get; set; }

        /// <summary>
        /// Gets or sets the eosp date header.
        /// </summary>
        /// <value>
        /// The eosp date header.
        /// </value>
        public string EospDateHeader { get; set; }

        /// <summary>
        /// Gets or sets the faop date header.
        /// </summary>
        /// <value>
        /// The faop date header.
        /// </value>
        public string FaopDateHeader { get; set; }

        /// <summary>
        /// Gets or sets the berth date header.
        /// </summary>
        /// <value>
        /// The berth date header.
        /// </value>
        public string BerthDateHeader { get; set; }

        /// <summary>
        /// Gets or sets the un berth date header.
        /// </summary>
        /// <value>
        /// The un berth date header.
        /// </value>
        public string UnBerthDateHeader { get; set; }

        /// <summary>
        /// Gets or sets the eosp date.
        /// </summary>
        /// <value>
        /// The eosp date.
        /// </value>
        public string EospDate { get; set; }

        /// <summary>
        /// Gets or sets the berth date.
        /// </summary>
        /// <value>
        /// The berth date.
        /// </value>
        public string BerthDate { get; set; }

        /// <summary>
        /// Gets or sets the un berth date.
        /// </summary>
        /// <value>
        /// The un berth date.
        /// </value>
        public string UnBerthDate { get; set; }

        /// <summary>
        /// Gets or sets the faop date.
        /// </summary>
        /// <value>
        /// The faop date.
        /// </value>
        public string FaopDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the charter.
        /// </summary>
        /// <value>
        /// The type of the charter.
        /// </value>
        public string CharterType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is no charter.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is no charter; otherwise, <c>false</c>.
        /// </value>
        public bool IsNoCharter { get; set; }

        /// <summary>
		/// Gets or sets the name of the charter.
		/// </summary>
		/// <value>
		/// The name of the charter.
		/// </value>
		public string CharterName { get; set; }

        /// <summary>
        /// Gets or sets the charter number.
        /// </summary>
        /// <value>
        /// The charter number.
        /// </value>
        public string CharterNumber { get; set; }


        /// <summary>
        /// The next activity id
        /// </summary>
        public string NextActivityId { get; set; }

        /// <summary>
        /// The next activity id
        /// </summary>
        public string PreviousActivityId { get; set; }

        /// <summary>
        /// Gets or sets the voyage number.
        /// </summary>
        /// <value>
        /// The voyage number.
        /// </value>
        public string VoyageNumber { get; set; }

        /// <summary>
        /// Gets or sets the full name of the port.
        /// </summary>
        /// <value>
        /// The full name of the port.
        /// </value>
        public string PortFullName { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the unlocode.
        /// </summary>
        /// <value>
        /// The unlocode.
        /// </value>
        public string Unlocode { get; set; }

        /// <summary>
        /// Gets or sets the full latitude.
        /// </summary>
        /// <value>
        /// The full latitude.
        /// </value>
        public string FullLatitude { get; set; }

        /// <summary>
        /// Gets or sets the full longitude.
        /// </summary>
        /// <value>
        /// The full longitude.
        /// </value>
        public string FullLongitude { get; set; }

        /// <summary>
        /// Gets or sets the is key hub port.
        /// </summary>
        /// <value>
        /// The is key hub port.
        /// </value>
        public string IsKeyHubPort { get; set; }
             
        /// <summary>
        /// Gets or sets the ActivityName.
        /// </summary>
        /// <value>
        /// The ActivityName.
        /// </value>
        public string ActivityName { get; set; }

        /// <summary>
        /// Gets or sets the last updated event date.
        /// </summary>
        /// <value>
        /// The last updated event date.
        /// </value>
        public string LastUpdatedEventDate { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VoyageLandingPageDetailsViewModel" /> class.
        /// </summary>
        public VoyageLandingPageDetailsViewModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoyageLandingPageDetailsViewModel" /> class.
        /// </summary>
        /// <param name="voyage">The voyage.</param>
        /// <param name="provider">The provider.</param>
        public VoyageLandingPageDetailsViewModel(VoyageLandingPageDetails voyage, IDataProtectionProvider provider)
        {
            _provier = provider;

            IsSeaPassageEvent = !string.IsNullOrWhiteSpace(voyage.POS_ID) && voyage.PLA_ID == "SP";
            //voyage.PLA_ID == "SP"; -- was used prev

            IsFromToVisible = !string.IsNullOrWhiteSpace(voyage.POS_ID);

            //from header section
            FromHeaderSection = IsSeaPassageEvent ? "From" : voyage.ActivityName;
            FromFAOPValue = IsSeaPassageEvent ? "FAOP" : "EOSP";
            FromPortName = voyage.FromCntCode + " - " + voyage.FromPortName;
            HasFromPortAlert = voyage.HasFromPortAlert.GetValueOrDefault();
            FromDate = voyage.FromDate.HasValue ? voyage.FromDate.Value.ToString("dd MMM yyyy HH:mm") : "";
            IsAgentAvailable = voyage.IsAgentAvailable.GetValueOrDefault();

            //to header section 
            ToHeaderSection = IsSeaPassageEvent ? "To" : "NEXT PORT";
            PositionListDateStatus? eospStauts = GetPositionListDateStatus(voyage.EospStatus);

            EospDateHeader = GetDateHeader(eospStauts, PortEventType.EOSP);
            FaopDateHeader = GetDateHeader(GetPositionListDateStatus(voyage.FaopStatus), PortEventType.FAOP);
            BerthDateHeader = GetDateHeader(GetPositionListDateStatus(voyage.BerthStatus), PortEventType.BERTHED);
            UnBerthDateHeader = GetDateHeader(GetPositionListDateStatus(voyage.UnBerthStatus), PortEventType.UNBERTHED);

            EospDate = voyage.EospDate.HasValue ? voyage.EospDate.Value.ToString(Constants.DateTime24HrFormat) : "-";
            FaopDate = voyage.FaopDate.HasValue ? voyage.FaopDate.Value.ToString(Constants.DateTime24HrFormat) : "-";
            BerthDate = voyage.BerthDate.HasValue ? voyage.BerthDate.Value.ToString(Constants.DateTime24HrFormat) : "-";
            UnBerthDate = voyage.UnBerthDate.HasValue ? voyage.UnBerthDate.Value.ToString(Constants.DateTime24HrFormat) : "-";

            CharterType = voyage.CharterType;
            IsNoCharter = voyage.CharterType == "NC" || string.IsNullOrWhiteSpace(voyage.CharterType);

            if (IsSeaPassageEvent && eospStauts.HasValue && eospStauts.Value == PositionListDateStatus.ACT)
            {
                ToEOSPValue = PortEventType.EOSP.ToString();
            }
            else if (IsSeaPassageEvent && (!eospStauts.HasValue || eospStauts.Value == PositionListDateStatus.EST))
            {
                ToEOSPValue = Constants.ETA;
            }

            string portNameFirstPart = IsSeaPassageEvent ? voyage.ToCntCode : voyage.NextCntCode;
            string portNameSecondPart = IsSeaPassageEvent ? voyage.ToPortName : voyage.NextPortName;
            ToPortName = portNameFirstPart + " - " + portNameSecondPart;
            IsToPortAlertVisible = (voyage.HasToPortAlert.HasValue && voyage.HasToPortAlert.Value) || (voyage.HasNextPortAlert.HasValue && voyage.HasNextPortAlert.Value);

            ToPortDate = IsSeaPassageEvent ? (voyage.ToDate.HasValue ? voyage.ToDate.Value.ToString("dd MMM yyyy HH:mm") : "") : (voyage.NextPortDate.HasValue ? voyage.NextPortDate.Value.ToString("dd MMM yyyy HH:mm") : "");

            //addition prop
            POS_ID = voyage.POS_ID;
            IsDelay = voyage.IsDelay;
            IsPortBadWeather = voyage.IsPortBadWeather;
            PLA_ID = voyage.PLA_ID;
            NextActivityId = voyage.NextActivityId;
            PreviousActivityId = voyage.PreviousActivityId;
            ActivityName = voyage.ActivityName;
            
            //progress bar section
            DistanceTravelled = voyage.DistanceTravelled;
            TotalDistance = voyage.TotalDistance;
            LastEventPosition = voyage.LantitudeDegree + "°, " + voyage.LantitudeMinute + "' " + voyage.LantitudeDirection + " " + voyage.LongitudeDegree + "°, " + voyage.LongitudeMinute + "' " + voyage.LongitudeDirection;

            RemainingValue = TotalDistance - DistanceTravelled;

            ProgressBarEndStatus = "AT SEA" + "\n" + LastEventPosition + "\n" + RemainingValue + " nm remaining";

            if (IsSeaPassageEvent)
            {
                BadWeatherDetails = voyage != null && voyage.WeatherDetail != null && voyage.WeatherDetail.Any() ? voyage.WeatherDetail.Where(obj => obj.IsBreakInPassage || obj.BadWeatherAlert).Select(obj => new VoyageActivityBadWeatherDetailViewModel(obj, PLA_ID, _provier)).ToList() : null;

                if (voyage != null)
                {
                    voyage.TotalDistance = voyage.EospStatus == EnumsHelper.GetKeyValue(PositionListDateStatus.ACT) ? DistanceTravelled : voyage.TotalDistance;
                }
            }
            else if (!string.IsNullOrWhiteSpace(POS_ID))
            {
                List<VoyageActivityBadWeatherDetailViewModel> badWeatherDetails = new List<VoyageActivityBadWeatherDetailViewModel>();
                if (IsDelay == true)
                {
                    badWeatherDetails.Add(new VoyageActivityBadWeatherDetailViewModel(
                        new VoyageActivityBadWeatherDetail
                        {
                            DistanceOnPassage = 0,
                            DistanceLog = 0,
                            IsBreakInPassage = IsDelay.GetValueOrDefault()
                        }, PLA_ID, _provier)
                    {
                        PosId = POS_ID,
                        PlaId = PLA_ID
                    });
                }
                if (IsPortBadWeather == true)
                {
                    badWeatherDetails.Add(new VoyageActivityBadWeatherDetailViewModel(new VoyageActivityBadWeatherDetail
                    {
                        DistanceOnPassage = 0,
                        DistanceLog = IsDelay == true ? 5 : 0,
                        BadWeatherAlert = IsPortBadWeather.GetValueOrDefault()
                    }, PLA_ID, _provier)
                    {
                        PosId = POS_ID,
                        PlaId = PLA_ID,
                    });
                }
                BadWeatherDetails = badWeatherDetails;
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// Gets the position list date status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        private PositionListDateStatus? GetPositionListDateStatus(string status)
        {
            if (!string.IsNullOrWhiteSpace(status))
            {
                return (PositionListDateStatus)Enum.Parse(typeof(PositionListDateStatus), EnumsHelper.GetEnumItemFromKeyValue(typeof(PositionListDateStatus), status));
            }
            else
            {
                return default(PositionListDateStatus?);
            }
        }

        /// <summary>
        /// Gets the date header.
        /// </summary>
        /// <param name="dateStatus">The date status.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <returns>
        /// header name
        /// </returns>
        private string GetDateHeader(PositionListDateStatus? dateStatus, PortEventType eventType)
        {
            switch (eventType)
            {
                case PortEventType.BERTHED:
                    switch (dateStatus)
                    {
                        case PositionListDateStatus.ACT:
                            return Constants.BTHD;
                        case PositionListDateStatus.EST:
                            return Constants.ETB;
                        default:
                            return Constants.ETB;
                    }
                case PortEventType.EOSP:
                    switch (dateStatus)
                    {
                        case PositionListDateStatus.ACT:
                            return Constants.EOSP;
                        case PositionListDateStatus.EST:
                            return Constants.ETA;
                        default:
                            return Constants.ETA;
                    }
                case PortEventType.FAOP:
                    switch (dateStatus)
                    {
                        case PositionListDateStatus.ACT:
                            return Constants.FAOP;
                        case PositionListDateStatus.EST:
                            return Constants.ETS;
                        default:
                            return Constants.ETS;
                    }
                case PortEventType.UNBERTHED:
                    switch (dateStatus)
                    {
                        case PositionListDateStatus.ACT:
                            return Constants.UNBTHD;
                        case PositionListDateStatus.EST:
                            return Constants.ETDBRTH;
                        default:
                            return Constants.ETDBRTH;
                    }
                default:
                    break;
            }
            return null;
        }

        #endregion
    }
}
