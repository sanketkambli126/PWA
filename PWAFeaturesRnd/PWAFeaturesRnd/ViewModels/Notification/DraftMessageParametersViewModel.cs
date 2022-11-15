using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Notification
{
	/// <summary>
	/// The draft message parameters view model
	/// </summary>
	public class DraftMessageParametersViewModel
    {
		/// <summary>
		/// Gets or sets the category identifier.
		/// </summary>
		/// <value>
		/// The category identifier.
		/// </value>
		public int CategoryId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the vessel imo number.
        /// </summary>
        /// <value>
        /// The vessel imo number.
        /// </value>
        public string VesselIMONumber { get; set; }

		/// <summary>
		/// Gets or sets the channel identifier.
		/// </summary>
		/// <value>
		/// The channel identifier.
		/// </value>
		public int ChannelId { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is general.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is general; otherwise, <c>false</c>.
		/// </value>
		public bool IsGeneralCat { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is save as draft.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is save as draft; otherwise, <c>false</c>.
		/// </value>
		public bool IsSaveAsDraft { get; set; }

		/// <summary>
		/// Gets or sets the message description.
		/// </summary>
		/// <value>
		/// The message description.
		/// </value>
		public string MessageDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is attachment.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is attachment; otherwise, <c>false</c>.
		/// </value>
		public bool IsAttachment { get; set; }

		/// <summary>
		/// Gets or sets the attachments.
		/// </summary>
		/// <value>
		/// The attachments.
		/// </value>
		public List<AttachmentViewModel> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        /// <value>
        /// The participants.
        /// </value>
        public List<DraftChannelSubscriptionViewModel> Participants { get; set; }
	}
}
