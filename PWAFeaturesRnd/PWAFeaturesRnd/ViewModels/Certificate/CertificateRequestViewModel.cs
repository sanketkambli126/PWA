using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PWAFeaturesRnd.Common.Converter;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Certificate
{
    /// <summary>
    /// Certificate request view model
    /// </summary>
    public class CertificateRequestViewModel
    {
        /// <summary>
        /// Gets or sets the name of the stage.
        /// </summary>
        /// <value>
        /// The name of the stage.
        /// </value>
        public string StageName { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public UserMenuItemType MenuType { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? ToDate { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether [include window].
		/// </summary>
		/// <value>
		///   <c>true</c> if [include window]; otherwise, <c>false</c>.
		/// </value>
		public bool IncludeWindow { get; set; }

        /// <summary>
        /// Gets or sets the type of the certificate.
        /// </summary>
        /// <value>
        /// The type of the certificate.
        /// </value>
        public string CertificateType { get; set; }

        /// <summary>
        /// Gets or sets the certificate status.
        /// </summary>
        /// <value>
        /// The certificate status.
        /// </value>
        public string CertificateStatus { get; set; }

        /// <summary>
        /// Gets or sets the certificate impact.
        /// </summary>
        /// <value>
        /// The certificate impact.
        /// </value>
        public string CertificateImpact { get; set; }

        /// <summary>
        /// Gets or sets the search keyword.
        /// </summary>
        /// <value>
        /// The search keyword.
        /// </value>
        public string SearchKeyword { get; set; }

        /// <summary>
		/// Gets or sets active tab calss. eg. tab-1 ,tab-2.
		/// </summary>
		/// <value>
		/// The Active Mobile Tab Classe text field.
		/// </value>
        public string ActiveMobileTabClass { get; set; }
    }
}
