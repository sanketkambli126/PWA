using System;

namespace PWAFeaturesRnd.ViewModels.Certificate
{
    /// <summary>
    /// The certificatedetailsresponseviewmodel
    /// </summary>
    public class CertificateDetailsResponseViewModel
    {
        /// <summary>
        /// Gets or sets the vessel certificate identifier.
        /// </summary>
        /// <value>
        /// The vessel certificate identifier.
        /// </value>
        public string VesselCertificateId { get; set; }
        /// <summary>
        /// Gets or sets the annotation.
        /// </summary>
        /// <value>
        /// The annotation.
        /// </value>
        public string Annotation { get; set; }
        /// <summary>
        /// Gets or sets the name of the certificate.
        /// </summary>
        /// <value>
        /// The name of the certificate.
        /// </value>
        public string CertificateFullName { get; set; }
        /// <summary>
        /// Gets or sets the vessel.
        /// </summary>
        /// <value>
        /// The vessel.
        /// </value>
        public string Vessel { get; set; }
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }        
    }
}
