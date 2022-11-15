using System;

namespace PWAFeaturesRnd.ViewModels.Crew
{
    /// <summary>
    ///   <br />ViewModel to display certificates and documents
    /// </summary>
    public class CertificatesAndDocumentsViewModel
    {
        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public string DocumentIdentifier { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public string REF { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public string CNT { get; set; }

        /// <summary>
        /// Gets or sets the issued on.
        /// </summary>
        /// <value>
        /// The issued on.
        /// </value>
        public DateTime? IssuedOn { get; set; }

        /// <summary>
        /// Gets or sets the expiry.
        /// </summary>
        /// <value>
        /// The expiry.
        /// </value>
        public DateTime? Expiry { get; set; }

        /// <summary>
        /// Gets or sets the authority.
        /// </summary>
        /// <value>
        /// The authority.
        /// </value>
        public string Authority { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expired.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expired; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expiring.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expiring; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpiring { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is covid vaccine type document.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is covid vaccine type document; otherwise, <c>false</c>.
        /// </value>
        public bool IsCovidVaccineTypeDocument { get; set; }

        /// <summary>
        /// Gets or sets the document count.
        /// </summary>
        /// <value>
        /// The document count.
        /// </value>
        public int DocumentCount { get; set; }
        
        /// <summary>
        /// Gets or sets the CRD identifier.
        /// </summary>
        /// <value>
        /// The CRD identifier.
        /// </value>
        public string CrdId { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public string DocId { get; set; }

        /// <summary>
        /// Gets or sets the CDT description.
        /// </summary>
        /// <value>
        /// The CDT description.
        /// </value>
        public string CdtDescription { get; set; }

    }
}
