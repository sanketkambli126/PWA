using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	public class ReportLight
	{
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReportLight" /> is contextual.
        /// </summary>
        /// <value>
        ///   <c>true</c> if contextual; otherwise, <c>false</c>.
        /// </value>
        public bool Contextual { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display profile.
        /// </summary>
        /// <value>
        /// The display profile.
        /// </value>
        public int DisplayProfile { get; set; }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the friendly identifier.
        /// </summary>
        /// <value>
        /// The friendly identifier.
        /// </value>
        public string FriendlyId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the report group identifier.
        /// </summary>
        /// <value>
        /// The report group identifier.
        /// </value>
        public string ReportGroupId { get; set; }

        /// <summary>
        /// Gets or sets the report parameters.
        /// </summary>
        /// <value>
        /// The report parameters.
        /// </value>
        public List<ReportParameter> ReportParameters { get; set; }

        /// <summary>
        /// Gets or sets the ret identifier.
        /// </summary>
        /// <value>
        /// The ret identifier.
        /// </value>
        public string RetId { get; set; }

        /// <summary>
        /// Gets or sets the RPT identifier.
        /// </summary>
        /// <value>
        /// The RPT identifier.
        /// </value>
        public string RptId { get; set; }

        /// <summary>
        /// Gets or sets the spname.
        /// </summary>
        /// <value>
        /// The spname.
        /// </value>
        public string Spname { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the report format.
        /// </summary>
        /// <value>
        /// The report format.
        /// </value>
        public ReportExportTypes ReportFormat { get; set; }

        /// <summary>
        /// Gets or sets the email recipients.
        /// </summary>
        /// <value>
        /// The email recipients.
        /// </value>
        public List<string> EmailRecipients { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is scheduled report.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is scheduled report; otherwise, <c>false</c>.
        /// </value>
        public bool IsScheduledReport { get; set; }

        /// <summary>
        /// Gets or sets the scheduled report identifier.
        /// </summary>
        /// <value>
        /// The scheduled report identifier.
        /// </value>
        public string ScheduledReportId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [for email only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [for email only]; otherwise, <c>false</c>.
        /// </value>
        public bool ForEmailOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [send individual emails].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [send individual emails]; otherwise, <c>false</c>.
        /// </value>
        public bool SendIndividualEmails { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        /// <value>
        /// The email subject.
        /// </value>
        public string EmailSubject { get; set; }

        /// <summary>
        /// Gets or sets the email body.
        /// </summary>
        /// <value>
        /// The email body.
        /// </value>
        public string EmailBody { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        /// <value>
        /// The BCC.
        /// </value>
        public string Bcc { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly file.
        /// </summary>
        /// <value>
        /// The name of the friendly file.
        /// </value>
        public string FriendlyFileName { get; set; }

        /// <summary>
        /// Gets or sets the delimiter.
        /// </summary>
        /// <value>
        /// The delimiter.
        /// </value>
        public string Delimiter { get; set; }

        /// <summary>
        /// Gets or sets the separator text.
        /// </summary>
        /// <value>
        /// The separator text.
        /// </value>
        public string SeparatorText { get; set; }

        /// <summary>
        /// Gets or sets the type of the export.
        /// </summary>
        /// <value>
        /// The type of the export.
        /// </value>
        public string ExportType { get; set; }

    }
}
