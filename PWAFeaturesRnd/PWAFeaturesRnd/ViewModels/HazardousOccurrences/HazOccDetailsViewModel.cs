using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// 
    /// </summary>
    public class HazOccDetailsViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the incident identifier.
        /// </summary>
        /// <value>
        /// The incident identifier.
        /// </value>
        public string IncidentId { get; set; }

        /// <summary>
        /// Gets or sets the ship reference number.
        /// </summary>
        /// <value>
        /// The ship reference number.
        /// </value>
        public string ShipReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>
        /// The class.
        /// </value>
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>
        /// The type identifier.
        /// </value>
        public string TypeId { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the status kpi.
        /// </summary>
        /// <value>
        /// The status kpi.
        /// </value>
        public int? StatusKPI { get; set; }

        /// <summary>
        /// Gets or sets the submission comment.
        /// </summary>
        /// <value>
        /// The submission comment.
        /// </value>
        public string SubmissionComment { get; set; }
        /// <summary>
        /// Gets or sets the actual severity identifier.
        /// </summary>
        /// <value>
        /// The actual severity identifier.
        /// </value>
        public string ActualSeverityId { get; set; }
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is incident summary.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is incident summary; otherwise, <c>false</c>.
        /// </value>
        public bool IsIncident { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is accident summary.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is accident summary; otherwise, <c>false</c>.
        /// </value>
        public bool IsAccident { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is observation summary.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is observation summary; otherwise, <c>false</c>.
        /// </value>
        public bool IsObservation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is near miss.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is near miss; otherwise, <c>false</c>.
        /// </value>
        public bool IsNearMiss { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is illness.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is illness; otherwise, <c>false</c>.
        /// </value>
        public bool IsIllness { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is crew illness.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is crew illness; otherwise, <c>false</c>.
        /// </value>
        public bool IsCrewIllness { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is passenger illness.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is passenger illness; otherwise, <c>false</c>.
        /// </value>
        public bool IsPassengerIllness { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is third party illness.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is third party illness; otherwise, <c>false</c>.
        /// </value>
        public bool IsThirdPartyIllness { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel.
        /// </summary>
        /// <value>
        /// The type of the vessel.
        /// </value>
        public string VesselType { get; set; }

        /// <summary>
        /// Gets or sets the encrypted identifier.
        /// </summary>
        /// <value>
        /// The encrypted identifier.
        /// </value>
        public string EncryptedIdentifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is redirect from haz occ list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is redirect from haz occ list; otherwise, <c>false</c>.
        /// </value>
        public bool IsRedirectFromHazOccList { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is awaiting completion label visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is awaiting completion label visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsAwaitingCompletionLabelVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show reopen comment].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show reopen comment]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowReopenComment { get; set; }

        /// <summary>
        /// Gets or sets the reopen authorised by.
        /// </summary>
        /// <value>
        /// The reopen authorised by.
        /// </value>
        public string ReopenAuthorisedBy { get; set; }

        /// <summary>
        /// Gets or sets the reopen comments.
        /// </summary>
        /// <value>
        /// The reopen comments.
        /// </value>
        public string ReopenComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is submission comment visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is submission comment visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsSubmissionCommentVisible { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
		public string MessageDetailsJSON { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }
    }
}
