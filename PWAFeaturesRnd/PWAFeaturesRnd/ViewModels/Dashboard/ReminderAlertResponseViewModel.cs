using System;

namespace PWAFeaturesRnd.ViewModels.Dashboard
{
    public class ReminderAlertResponseViewModel
    {
        /// <summary>
        /// Gets or sets the reminder identifier.
        /// </summary>
        /// <value>
        /// The reminder identifier.
        /// </value>
        public string EncryptedReminderId { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public string NoteId { get; set; }

        /// <summary>
        /// Gets or sets the note title.
        /// </summary>
        /// <value>
        /// The note title.
        /// </value>
        public string NoteTitle { get; set; }

        /// <summary>
        /// Gets or sets the note description.
        /// </summary>
        /// <value>
        /// The note description.
        /// </value>
        public string NoteDescription { get; set; }

        /// <summary>
        /// Gets or sets the is single time event.
        /// </summary>
        /// <value>
        /// The is single time event.
        /// </value>
        public bool? IsSingleTimeEvent { get; set; }

        /// <summary>
        /// Gets or sets the is cron exp scheduled.
        /// </summary>
        /// <value>
        /// The is cron exp scheduled.
        /// </value>
        public bool? IsCronExpScheduled { get; set; }

        /// <summary>
        /// Gets or sets the is reminder expired.
        /// </summary>
        /// <value>
        /// The is reminder expired.
        /// </value>
        public bool? IsReminderExpired { get; set; }

        /// <summary>
        /// Gets or sets the expected execution time.
        /// </summary>
        /// <value>
        /// The expected execution time.
        /// </value>
        public string ExpectedExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets the note created date.
        /// </summary>
        /// <value>
        /// The note created date.
        /// </value>
        public string NoteCreatedDate { get; set; }
    }
}
