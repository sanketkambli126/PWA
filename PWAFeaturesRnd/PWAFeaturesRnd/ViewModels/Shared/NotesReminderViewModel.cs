using System;

namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// Notes Reminder ViewModel
    /// </summary>
    public class NotesReminderViewModel
    {
        /// <summary>
        /// Gets or sets the reminder identifier.
        /// </summary>
        /// <value>
        /// The reminder identifier.
        /// </value>
        public long ReminderId { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public long NoteId { get; set; }

        /// <summary>
        /// Gets or sets the cronexpression.
        /// </summary>
        /// <value>
        /// The cronexpression.
        /// </value>
        public string Cronexpression { get; set; }

        /// <summary>
        /// Gets or sets the crondescription.
        /// </summary>
        /// <value>
        /// The crondescription.
        /// </value>
        public string Crondescription { get; set; }

        /// <summary>
        /// Gets or sets the execution date time.
        /// </summary>
        /// <value>
        /// The execution date time.
        /// </value>
        public DateTime? ExecutionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the is cronexp scheduled.
        /// </summary>
        /// <value>
        /// The is cronexp scheduled.
        /// </value>
        public bool? IsCronexpScheduled { get; set; }

        /// <summary>
        /// Gets or sets the is reminder expired.
        /// </summary>
        /// <value>
        /// The is reminder expired.
        /// </value>
        public bool? IsReminderExpired { get; set; }

        /// <summary>
        /// Gets or sets the is single time event.
        /// </summary>
        /// <value>
        /// The is single time event.
        /// </value>
        public bool? IsSingleTimeEvent { get; set; }

        /// <summary>
        /// Gets or sets the expected execution time.
        /// </summary>
        /// <value>
        /// The expected execution time.
        /// </value>
        public DateTime? ExpectedExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets the reminder feature status.
        /// </summary>
        /// <value>
        /// The reminder feature status.
        /// </value>
        public int ReminderFeatureStatus { get; set; }

        /// <summary>
        /// Gets or sets the is alert dismissed.
        /// </summary>
        /// <value>
        /// The is alert dismissed.
        /// </value>
        public bool? IsAlertDismissed { get; set; }
    }
}
