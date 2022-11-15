using System;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// This class is used to get and save the unsync crew document detail.
    /// </summary>
    public class UnsyncCrewDocumentDetail
    {
        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>        
        public string DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>        
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the CRW identifier.
        /// </summary>
        /// <value>
        /// The CRW identifier.
        /// </value>        
        public string CrwId { get; set; }

        /// <summary>
        /// Gets or sets the CRW identifier tp.
        /// </summary>
        /// <value>
        /// The CRW identifier tp.
        /// </value>        
        public string CrwIdTp { get; set; }

        /// <summary>
        /// Gets or sets the type of the obl identifier document.
        /// </summary>
        /// <value>
        /// The type of the obl identifier document.
        /// </value>        
        public string OblIdDocumentType { get; set; }

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>        
        public string DocumentType { get; set; }

        /// <summary>
        /// Gets or sets the count identifier.
        /// </summary>
        /// <value>
        /// The count identifier.
        /// </value>        
        public string CntId { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>        
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        /// <value>
        /// The document number.
        /// </value>        
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary document.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is primary document; otherwise, <c>false</c>.
        /// </value>        
        public bool IsPrimaryDocument { get; set; }

        /// <summary>
        /// Gets or sets the issued date.
        /// </summary>
        /// <value>
        /// The issued date.
        /// </value>        
        public DateTime? IssuedDate { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>        
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>        
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>        
        public Guid UniqueIdentifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sycn crew document.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sycn crew document; otherwise, <c>false</c>.
        /// </value>        
        public bool IsSycnCrewDocument { get; set; }
    }
}
