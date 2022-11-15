namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// User Preference Detail ViewModel
    /// </summary>
    public class UserPreferenceDetailViewModel
    {
        /// <summary>
        /// Gets or sets the mapping identifier.
        /// </summary>
        /// <value>
        /// The mapping identifier.
        /// </value>
        public string MappingId { get; set; }

        /// <summary>
        /// Gets or sets the preference identifier.
        /// </summary>
        /// <value>
        /// The preference identifier.
        /// </value>
        public string PreferenceId { get; set; }

        /// <summary>
        /// Gets or sets the preference key.
        /// </summary>
        /// <value>
        /// The preference key.
        /// </value>
        public string PreferenceKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is preferred.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is preferred; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferred { get; set; }
    }
}
