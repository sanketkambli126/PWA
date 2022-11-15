using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Used for fetching the documents by crewId
    /// </summary>
    public class CrewScannedDocDetails
    {
        /// <summary>
        /// Gets or sets the appraisal rank short code.
        /// </summary>
        /// <value>
        /// The appraisal rank short code.
        /// </value>
        public string AppraisalRankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the medical date.
        /// </summary>
        /// <value>
        /// The medical date.
        /// </value>
        public DateTime? MedicalDate { get; set; }

        /// <summary>
        /// Gets or sets the medical type description.
        /// </summary>
        /// <value>
        /// The medical type description.
        /// </value>
        public string MedicalTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the uploaded by.
        /// </summary>
        /// <value>
        /// The uploaded by.
        /// </value>
        public string UploadedBy { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail identifier.
        /// </summary>
        /// <value>
        /// The thumbnail identifier.
        /// </value>
        public string ThumbnailIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the name of the formal warning vessel.
        /// </summary>
        /// <value>
        /// The name of the formal warning vessel.
        /// </value>
        public string FormalWarningVesselName { get; set; }

        /// <summary>
        /// Gets or sets the formal warning description.
        /// </summary>
        /// <value>
        /// The formal warning description.
        /// </value>
        public string FormalWarningDescription { get; set; }

        /// <summary>
        /// Gets or sets the dispensation description.
        /// </summary>
        /// <value>
        /// The dispensation description.
        /// </value>
        public string DispensationDescription { get; set; }

        /// <summary>
        /// Gets or sets the name of the debriefing vessel.
        /// </summary>
        /// <value>
        /// The name of the debriefing vessel.
        /// </value>
        public string DebriefingVesselName { get; set; }

        /// <summary>
        /// Gets or sets the name of the appraisal vessel.
        /// </summary>
        /// <value>
        /// The name of the appraisal vessel.
        /// </value>
        public string AppraisalVesselName { get; set; }

        /// <summary>
        /// Gets or sets the debriefing rank short code.
        /// </summary>
        /// <value>
        /// The debriefing rank short code.
        /// </value>
        public string DebriefingRankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the full name of the crew.
        /// </summary>
        /// <value>
        /// The full name of the crew.
        /// </value>
        public string CrewFullName { get; set; }

        /// <summary>
        /// Gets or sets the cloud extension identifier.
        /// </summary>
        /// <value>
        /// The cloud extension identifier.
        /// </value>
        public string CloudExtensionId { get; set; }

        /// <summary>
        /// Gets or sets the attachment sub type identifier.
        /// </summary>
        /// <value>
        /// The attachment sub type identifier.
        /// </value>
        public string AttachmentSubTypeId { get; set; }

        /// <summary>
        /// Gets or sets the downloaded by seafarer.
        /// </summary>
        /// <value>
        /// The downloaded by seafarer.
        /// </value>
        public DateTime? DownloadedBySeafarer { get; set; }

        /// <summary>
        /// Gets or sets the last downloaded by seafarer.
        /// </summary>
        /// <value>
        /// The last downloaded by seafarer.
        /// </value>
        public DateTime? LastDownloadedBySeafarer { get; set; }

        /// <summary>
        /// Gets or sets the read by seafarer.
        /// </summary>
        /// <value>
        /// The read by seafarer.
        /// </value>
        public DateTime? ReadBySeafarer { get; set; }

        /// <summary>
        /// Gets or sets the cloud extension notes.
        /// </summary>
        /// <value>
        /// The cloud extension notes.
        /// </value>
        public string CloudExtensionNotes { get; set; }

        /// <summary>
        /// Gets or sets the type of the ats.
        /// </summary>
        /// <value>
        /// The type of the ats.
        /// </value>
        public string AtsType { get; set; }

        /// <summary>
        /// Gets or sets the LNP description.
        /// </summary>
        /// <value>
        /// The LNP description.
        /// </value>
        public string LnpDescription { get; set; }

        /// <summary>
        /// Gets or sets the name of the allotee.
        /// </summary>
        /// <value>
        /// The name of the allotee.
        /// </value>
        public string AlloteeName { get; set; }

        /// <summary>
        /// Gets or sets the service rank short code.
        /// </summary>
        /// <value>
        /// The service rank short code.
        /// </value>
        public string ServiceRankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the service vessel.
        /// </summary>
        /// <value>
        /// The name of the service vessel.
        /// </value>
        public string ServiceVesselName { get; set; }

        /// <summary>
        /// Gets or sets the note short description.
        /// </summary>
        /// <value>
        /// The note short description.
        /// </value>
        public string NoteShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the scanned document identifier.
        /// </summary>
        /// <value>
        /// The scanned document identifier.
        /// </value>
        public string ScannedDocumentId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the scanned date.
        /// </summary>
        /// <value>
        /// The scanned date.
        /// </value>
        public DateTime ScannedDate { get; set; }

        /// <summary>
        /// Gets or sets the scanned by.
        /// </summary>
        /// <value>
        /// The scanned by.
        /// </value>
        public string ScannedBy { get; set; }

        /// <summary>
        /// Gets or sets the issue date.
        /// </summary>
        /// <value>
        /// The issue date.
        /// </value>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the cloud status.
        /// </summary>
        /// <value>
        /// The cloud status.
        /// </value>
        public CloudUploadStatus? CloudStatus { get; set; }

        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>
        /// The type of the file.
        /// </value>
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the crew document identifier.
        /// </summary>
        /// <value>
        /// The crew document identifier.
        /// </value>
        public string CrewDocumentId { get; set; }

        /// <summary>
        /// Gets or sets the entity reference1.
        /// </summary>
        /// <value>
        /// The entity reference1.
        /// </value>
        public string EntityReference1 { get; set; }

        /// <summary>
        /// Gets or sets the attachment source.
        /// </summary>
        /// <value>
        /// The attachment source.
        /// </value>
        public string AttachmentSource { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        public int FileSize { get; set; }

        /// <summary>
        /// Gets or sets the entity table identifier.
        /// </summary>
        /// <value>
        /// The entity table identifier.
        /// </value>
        public string EntityTableId { get; set; }

        /// <summary>
        /// Gets or sets the entity table enum.
        /// </summary>
        /// <value>
        /// The entity table enum.
        /// </value>
        public DocumentCategory? EntityTableEnum { get; set; }

        /// <summary>
        /// Gets or sets the document type description.
        /// </summary>
        /// <value>
        /// The document type description.
        /// </value>
        public string DocumentTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the document scanned group identifier.
        /// </summary>
        /// <value>
        /// The document scanned group identifier.
        /// </value>
        public string DocumentScannedGroupId { get; set; }

        /// <summary>
        /// Gets or sets the entity reference2.
        /// </summary>
        /// <value>
        /// The entity reference2.
        /// </value>
        public string EntityReference2 { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        /// <value>
        /// The document number.
        /// </value>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the note.
        /// </summary>
        /// <value>
        /// The type of the note.
        /// </value>
        public string NoteType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sent to seafarer.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sent to seafarer; otherwise, <c>false</c>.
        /// </value>
        public bool IsSentToSeafarer { get; set; }

        /// <summary>
        /// Gets or sets the payroll date.
        /// </summary>
        /// <value>
        /// The payroll date.
        /// </value>
        public DateTime? PayrollDate { get; set; }
    }
}
