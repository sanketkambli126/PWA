namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Error Log Request in PWA
    /// </summary>
    public class ErrorLogRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the type of the exception.
        /// </summary>
        /// <value>
        /// The type of the exception.
        /// </value>
        public string ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the source application.
        /// </summary>
        /// <value>
        /// The source application.
        /// </value>
        public string SourceApplication { get; set; }

        /// <summary>
        /// Gets or sets the exception category.
        /// </summary>
        /// <value>
        /// The exception category.
        /// </value>
        public string ExceptionCategory { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the inner exception.
        /// </summary>
        /// <value>
        /// The inner exception.
        /// </value>
        public string InnerException { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>
        /// The name of the method.
        /// </value>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the user log in identifier.
        /// </summary>
        /// <value>
        /// The user log in identifier.
        /// </value>
        public string UserLogInId { get; set; }

    }
}
