using System;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Contract for Crew Documents
    /// </summary>
    class CrewDocumentDetail
    {
        /// <summary>
        /// Gets or sets the CRD external provider.
        /// </summary>
        /// <value>
        /// The CRD external provider.
        /// </value>
        public string CrdExternalProvider { get; set; }
        /// <summary>
        /// Gets or sets the CRD updated by.
        /// </summary>
        /// <value>
        /// The CRD updated by.
        /// </value>
        public string CrdUpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the CRD superseded by.
        /// </summary>
        /// <value>
        /// The CRD superseded by.
        /// </value>
        public string CrdSupersededBy { get; set; }
        /// <summary>
        /// Gets or sets the CRD status.
        /// </summary>
        /// <value>
        /// The CRD status.
        /// </value>
        public string CrdStatus { get; set; }
        /// <summary>
        /// Gets or sets the CRD score.
        /// </summary>
        /// <value>
        /// The CRD score.
        /// </value>
        public string CrdScore { get; set; }
        /// <summary>
        /// Gets or sets the CRD reviewed on.
        /// </summary>
        /// <value>
        /// The CRD reviewed on.
        /// </value>
        public DateTime? CrdReviewedOn { get; set; }
        /// <summary>
        /// Gets or sets the CRD reviewed by.
        /// </summary>
        /// <value>
        /// The CRD reviewed by.
        /// </value>
        public string CrdReviewedBy { get; set; }
        /// <summary>
        /// Gets or sets the CRD revalid.
        /// </summary>
        /// <value>
        /// The CRD revalid.
        /// </value>
        public DateTime? CrdRevalid { get; set; }
        /// <summary>
        /// Gets or sets the CRD place.
        /// </summary>
        /// <value>
        /// The CRD place.
        /// </value>
        public string CrdPlace { get; set; }
        /// <summary>
        /// Gets or sets the CRD number.
        /// </summary>
        /// <value>
        /// The CRD number.
        /// </value>
        public string CrdNumber { get; set; }
        /// <summary>
        /// Gets or sets the CRD issued.
        /// </summary>
        /// <value>
        /// The CRD issued.
        /// </value>
        public DateTime? CrdIssued { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [CRD is primary document].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [CRD is primary document]; otherwise, <c>false</c>.
        /// </value>
        public bool CrdIsPrimaryDocument { get; set; }
        /// <summary>
        /// Gets or sets the cad identifier.
        /// </summary>
        /// <value>
        /// The cad identifier.
        /// </value>
        public string CadId { get; set; }
        /// <summary>
        /// Gets or sets the cda identifier.
        /// </summary>
        /// <value>
        /// The cda identifier.
        /// </value>
        public string CdaId { get; set; }
        /// <summary>
        /// Gets or sets the CRD grade.
        /// </summary>
        /// <value>
        /// The CRD grade.
        /// </value>
        public string CrdGrade { get; set; }
        /// <summary>
        /// Gets or sets the CRD add financial data.
        /// </summary>
        /// <value>
        /// The CRD add financial data.
        /// </value>
        public bool? CrdAddFinancialData { get; set; }
        /// <summary>
        /// Gets or sets the CRD cancelled.
        /// </summary>
        /// <value>
        /// The CRD cancelled.
        /// </value>
        public byte CrdCancelled { get; set; }
        /// <summary>
        /// Gets or sets the CRD client costing.
        /// </summary>
        /// <value>
        /// The CRD client costing.
        /// </value>
        public string CrdClientCosting { get; set; }
        /// <summary>
        /// Gets or sets the CRD comments.
        /// </summary>
        /// <value>
        /// The CRD comments.
        /// </value>
        public string CrdComments { get; set; }
        /// <summary>
        /// Gets or sets the CRD cost.
        /// </summary>
        /// <value>
        /// The CRD cost.
        /// </value>
        public double? CrdCost { get; set; }
        /// <summary>
        /// Gets or sets the CRD cost currency.
        /// </summary>
        /// <value>
        /// The CRD cost currency.
        /// </value>
        public string CrdCostCurrency { get; set; }
        /// <summary>
        /// Gets or sets the CRD country.
        /// </summary>
        /// <value>
        /// The CRD country.
        /// </value>
        public string CrdCountry { get; set; }
        /// <summary>
        /// Gets or sets the CRD cra status.
        /// </summary>
        /// <value>
        /// The CRD cra status.
        /// </value>
        public int? CrdCraStatus { get; set; }
        /// <summary>
        /// Gets or sets the CRD created by.
        /// </summary>
        /// <value>
        /// The CRD created by.
        /// </value>
        public string CrdCreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the CRD identifier.
        /// </summary>
        /// <value>
        /// The CRD identifier.
        /// </value>
        public string CrdId { get; set; }
        /// <summary>
        /// Gets or sets the CRD expenses.
        /// </summary>
        /// <value>
        /// The CRD expenses.
        /// </value>
        public double? CrdExpenses { get; set; }
        /// <summary>
        /// Gets or sets the CRD expenses currency.
        /// </summary>
        /// <value>
        /// The CRD expenses currency.
        /// </value>
        public string CrdExpensesCurrency { get; set; }
        /// <summary>
        /// Gets or sets the CRD expiry.
        /// </summary>
        /// <value>
        /// The CRD expiry.
        /// </value>
        public DateTime? CrdExpiry { get; set; }
        /// <summary>
        /// Gets or sets the CRD external provided.
        /// </summary>
        /// <value>
        /// The CRD external provided.
        /// </value>
        public bool? CrdExternalProvided { get; set; }
        /// <summary>
        /// Gets or sets the CRD approved by.
        /// </summary>
        /// <value>
        /// The CRD approved by.
        /// </value>
        public string CrdApprovedBy { get; set; }
        /// <summary>
        /// Gets or sets the CRD created on.
        /// </summary>
        /// <value>
        /// The CRD created on.
        /// </value>
        public DateTime? CrdCreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the CRW identifier.
        /// </summary>
        /// <value>
        /// The CRW identifier.
        /// </value>
        public string CrwId { get; set; }
        /// <summary>
        /// Gets or sets the no of attachments.
        /// </summary>
        /// <value>
        /// The no of attachments.
        /// </value>
        public int NoOfAttachments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [skip validations].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [skip validations]; otherwise, <c>false</c>.
        /// </value>
        public bool SkipValidations { get; set; }

        /// <summary>
        /// Gets or sets the archieved expired document.
        /// </summary>
        /// <value>
        /// The archieved expired document.
        /// </value>
        /// Note: This property is used at View-Model side. To Remove arceived document from list after saving document same as expired and same country and type
        public bool? ArchievedExpiredDocument { get; set; }

        /// <summary>
        /// Gets or sets the name of the created by.
        /// </summary>
        /// <value>
        /// The name of the created by.
        /// </value>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [updated by name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [updated by name]; otherwise, <c>false</c>.
        /// </value>
        public string UpdatedByName { get; set; }

        /// <summary>
        /// Gets or sets the type of the crew document.
        /// </summary>
        /// <value>
        /// The type of the crew document.
        /// </value>
        public CrewDocumentTypeDetail CrewDocumentType { get; set; }

        /// <summary>
        /// Gets or sets the crew document status.
        /// </summary>
        /// <value>
        /// The crew document status.
        /// </value>
        public CrewDocumentStatusDetail CrewDocumentStatus { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public string DocId { get; set; }
    }
}
