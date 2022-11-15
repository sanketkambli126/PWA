using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Certificate
{
	public class VesselCertificateAuditLogDetail
	{
        /// <summary>
        /// Gets or sets the certificate identifier.
        /// </summary>
        /// <value>
        /// The certificate identifier.
        /// </value>
        public string CertificateId { get; set; }

        /// <summary>
        /// Gets or sets the certificate extended number.
        /// </summary>
        /// <value>
        /// The certificate extended number.
        /// </value>
        public int? CertificateExtendedNumber { get; set; }


        /// <summary>
        /// Gets or sets the log date local.
        /// </summary>
        /// <value>
        /// The log date local.
        /// </value>
        public DateTime LogDateLocal { get; set; }


        /// <summary>
        /// Gets or sets the log date UTC.
        /// </summary>
        /// <value>
        /// The log date UTC.
        /// </value>
        public DateTime? LogDateUTC { get; set; }


        /// <summary>
        /// Gets or sets the vessel certificate identifier.
        /// </summary>
        /// <value>
        /// The vessel certificate identifier.
        /// </value>
        public string VesselCertificateId { get; set; }


        /// <summary>
        /// Gets or sets the vessel certificate parent identifier.
        /// </summary>
        /// <value>
        /// The vessel certificate parent identifier.
        /// </value>
        public string VesselCertificateParentId { get; set; }


        /// <summary>
        /// Gets or sets the name of the vessel certificate.
        /// </summary>
        /// <value>
        /// The name of the vessel certificate.
        /// </value>
        public string VesselCertificateName { get; set; }


        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        public string Event { get; set; }


        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }


        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }


        /// <summary>
        /// Gets or sets the name of the updated by.
        /// </summary>
        /// <value>
        /// The name of the updated by.
        /// </value>
        public string UpdatedByName { get; set; }


        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public string UserRole { get; set; }
	}
}
