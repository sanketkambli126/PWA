using System;
using System.Linq;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Models.Report.VoyageReporting;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// Voyage Activity Report View Model
    /// </summary>
    public class VoyageActivityReportViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public VoyageActivityReport Entity { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity.
        /// </summary>
        /// <value>
        /// The name of the activity.
        /// </value>
        public string ActivityName { get; set; }

        /// <summary>
        /// Gets or sets from status.
        /// </summary>
        /// <value>
        /// From status.
        /// </value>
        public string FromStatus { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets from port.
        /// </summary>
        /// <value>
        /// From port.
        /// </value>
        public string FromPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has from port alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has from port alert; otherwise, <c>false</c>.
        /// </value>
        public bool HasFromPortAlert { get; set; }

        /// <summary>
        /// Converts to status.
        /// </summary>
        /// <value>
        /// To status.
        /// </value>
        public string ToStatus { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Converts to port.
        /// </summary>
        /// <value>
        /// To port.
        /// </value>
        public string ToPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has to port alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has to port alert; otherwise, <c>false</c>.
        /// </value>
        public bool HasToPortAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sea passage event.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sea passage event; otherwise, <c>false</c>.
        /// </value>
        public bool IsSeaPassageEvent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has port agent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has port agent; otherwise, <c>false</c>.
        /// </value>
        public bool HasPortAgent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is off hire.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
        /// </value>
        public bool IsOffHire { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is delay.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is delay; otherwise, <c>false</c>.
        /// </value>
        public bool IsDelay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bad weather.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is bad weather; otherwise, <c>false</c>.
        /// </value>
        public bool IsBadWeather { get; set; }

        /// <summary>
        /// Gets or sets the charter number.
        /// </summary>
        /// <value>
        /// The charter number.
        /// </value>
        public string CharterNumber { get; set; }

        /// <summary>
        /// Gets or sets the voyage number.
        /// </summary>
        /// <value>
        /// The voyage number.
        /// </value>
        public string VoyageNumber { get; set; }

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
        /// Converts FromAgentRequestURL.
        /// </summary>
        /// <value>
        /// FromAgentRequestURL
        /// </value>
        public string FromAgentRequestURL { get; set; }

        /// <summary>
        /// Converts ToAgentRequestURL.
        /// </summary>
        /// <value>
        /// ToAgentRequestURL.
        /// </value>
        public string ToAgentRequestURL { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel detail.
        /// </summary>
        /// <value>
        /// The encrypted vessel detail.
        /// </value>
        public string EncryptedVesselDetail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is vessel loaded flag.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vessel loaded flag; otherwise, <c>false</c>.
        /// </value>
        public bool IsVesselLoadedFlag { get; set; }

        /// <summary>
        /// Gets or sets the activity description.
        /// </summary>
        /// <value>
        /// The activity description.
        /// </value>
        public string ActivityDescription { get; set; }

        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        /// <value>
        /// The activity identifier.
        /// </value>
        public string ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the last report event date.
        /// </summary>
        /// <value>
        /// The last report event date.
        /// </value>
        public DateTime? LastReportEventDate { get; set; }


        #region FromPortDetails

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string FromPortCountryName { get; set; }

        /// <summary>
        /// Gets or sets the un locode.
        /// </summary>
        /// <value>
        /// The un locode.
        /// </value>
        public string FromPortUNLocode { get; set; }

        /// <summary>
        /// Gets or sets the lat degree.
        /// </summary>
        /// <value>
        /// The lat degree.
        /// </value>
        public string FromPortLat { get; set; }

        /// <summary>
        /// Gets or sets the long degree.
        /// </summary>
        /// <value>
        /// The long degree.
        /// </value>
        public string FromPortLong { get; set; }


        /// <summary>
        /// Gets or sets the is key hub port.
        /// </summary>
        /// <value>
        /// The is key hub port.
        /// </value>
        public string FromPortIsKeyHubPort { get; set; }

        /// <summary>
        /// Gets or sets from port identifier.
        /// </summary>
        /// <value>
        /// From port identifier.
        /// </value>
        public string FromPortId { get; set; }

        #endregion

        #region ToPortDetail

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string ToPortCountryName { get; set; }

        /// <summary>
        /// Gets or sets the un locode.
        /// </summary>
        /// <value>
        /// The un locode.
        /// </value>
        public string ToPortUNLocode { get; set; }

        /// <summary>
        /// Gets or sets the lat degree.
        /// </summary>
        /// <value>
        /// The lat degree.
        /// </value>
        public string ToPortLat { get; set; }

        /// <summary>
        /// Gets or sets the long degree.
        /// </summary>
        /// <value>
        /// The long degree.
        /// </value>
        public string ToPortLong { get; set; }

        /// <summary>
        /// Gets or sets the is key hub port.
        /// </summary>
        /// <value>
        /// The is key hub port.
        /// </value>
        public string ToPortIsKeyHubPort { get; set; }

        /// <summary>
        /// Converts to portid.
        /// </summary>
        /// <value>
        /// To port identifier.
        /// </value>
        public string ToPortId { get; set; }


        #endregion

        /// <summary>
        /// Gets or sets the position date1.
        /// </summary>
        /// <value>
        /// The position date1.
        /// </value>
        public string EospDate { get; set; }
        /// <summary>
        /// Gets or sets the position date2.
        /// </summary>
        /// <value>
        /// The position date2.
        /// </value>
        public string BerthedDate { get; set; }
        /// <summary>
        /// Gets or sets the position date3.
        /// </summary>
        /// <value>
        /// The position date3.
        /// </value>
        public string UnBerthedDate { get; set; }
        /// <summary>
        /// Gets or sets the position date4.
        /// </summary>
        /// <value>
        /// The position date4.
        /// </value>
        public string FaopDate { get; set; }

        /// <summary>
        /// Gets or sets the position status1.
        /// </summary>
        /// <value>
        /// The position status1.
        /// </value>
        public string EospStatus { get; set; }
        /// <summary>
        /// Gets or sets the position status2.
        /// </summary>
        /// <value>
        /// The position status2.
        /// </value>
        public string BerthStatus { get; set; }
        /// <summary>
        /// Gets or sets the position status3.
        /// </summary>
        /// <value>
        /// The position status3.
        /// </value>
        public string UnBerthStatus { get; set; }
        /// <summary>
        /// Gets or sets the position status4.
        /// </summary>
        /// <value>
        /// The position status4.
        /// </value>
        public string FaopStatus { get; set; }


        /// <summary>
        /// Gets or sets the name of the agent1.
        /// </summary>
        /// <value>
        /// The name of the agent1.
        /// </value>
        public string Agent1Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent2.
        /// </summary>
        /// <value>
        /// The name of the agent2.
        /// </value>
        public string Agent2Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent3.
        /// </summary>
        /// <value>
        /// The name of the agent3.
        /// </value>
        public string Agent3Name { get; set; }

        /// <summary>
        /// Gets or sets the agent count.
        /// </summary>
        /// <value>
        /// The agent count.
        /// </value>
        public int AgentCount { get; set; }

        /// <summary>
        /// Gets or sets the type of the agent1.
        /// </summary>
        /// <value>
        /// The type of the agent1.
        /// </value>
        public string Agent1Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the agent2.
        /// </summary>
        /// <value>
        /// The type of the agent2.
        /// </value>
        public string Agent2Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the agent3.
        /// </summary>
        /// <value>
        /// The type of the agent3.
        /// </value>
        public string Agent3Type { get; set; }

        /// <summary>
        /// Gets or sets the channel count.
        /// </summary>
        /// <value>
        /// The channel count.
        /// </value>
        public int ChannelCount { get; set; }

        /// <summary>
        /// Gets or sets the notes count.
        /// </summary>
        /// <value>
        /// The notes count.
        /// </value>
        public int NotesCount { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VoyageActivityReportViewModel"/> class.
        /// </summary>
        public VoyageActivityReportViewModel()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoyageActivityReportViewModel"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public VoyageActivityReportViewModel(VoyageActivityReport entity)
        {
            Entity = entity;
            if (Entity != null && Entity.Events != null && Entity.Events.Any())
            {
                PositionListPreviewEvent fromEvent = Entity.IsSeaPassageEvent ?
                                                     Entity.Events[0] : (Entity.Events.Any(obj => !obj.IsEstimatedEventDate) ?
                                                     Entity.Events.Where(obj => !obj.IsEstimatedEventDate).OrderBy(obj => obj.EventDate).LastOrDefault() : Entity.Events.Where(obj => obj.EventDate.HasValue).OrderBy(obj => obj.EventDate).FirstOrDefault());
                PositionListPreviewEvent toEvent = Entity.IsSeaPassageEvent ? Entity.Events[1] : null;

                FromStatus = GetStatus(fromEvent, Entity.IsSeaPassageEvent);
                ToStatus = Entity.IsSeaPassageEvent ? GetStatus(toEvent, Entity.IsSeaPassageEvent) : null;

                FromDate = fromEvent != null ? fromEvent.EventDate : Entity.LastReportEventDate;
                ToDate = toEvent != null ? toEvent.EventDate : null;

            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="reportEvent">The report event.</param>
        /// <param name="isSeaPassage">if set to <c>true</c> [is sea passage].</param>
        /// <returns></returns>
        private string GetStatus(PositionListPreviewEvent reportEvent, bool isSeaPassage)
        {
            if (reportEvent != null)
            {
                switch (reportEvent.Status)
                {
                    case PositionListEventStatus.VoyageArrived: return reportEvent.IsEstimatedEventDate ? Constants.ETA : Constants.EOSP;
                    case PositionListEventStatus.VoyageDeparted: return reportEvent.IsEstimatedEventDate ? (isSeaPassage ? Constants.ETD : Constants.ETS) : Constants.FAOP;
                    case PositionListEventStatus.BerthArrived: return reportEvent.IsEstimatedEventDate ? Constants.ETB : Constants.BTHD;
                    case PositionListEventStatus.BerthDeparted: return reportEvent.IsEstimatedEventDate ? Constants.ETDBRTH : Constants.UNBTHD;
                }
            }

            if (reportEvent == null && !isSeaPassage)
            {
                return Constants.ETA;
            }
            return null;
        }

        #endregion
    }
}
