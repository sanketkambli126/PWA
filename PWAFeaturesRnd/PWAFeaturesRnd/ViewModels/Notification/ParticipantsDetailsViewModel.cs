namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// Participants Details View Model
    /// </summary>
    public class ParticipantsDetailsViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the short name of the user.
        /// </summary>
        /// <value>
        /// The short name of the user.
        /// </value>
        public string UserShortName { get; set; }

        #endregion
    }
}
