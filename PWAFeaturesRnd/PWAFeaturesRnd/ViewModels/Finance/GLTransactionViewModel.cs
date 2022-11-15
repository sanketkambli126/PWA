using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PWAFeaturesRnd.Common.Converter;

namespace PWAFeaturesRnd.ViewModels.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class GLTransactionViewModel
    {
        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets the account code.
        /// </summary>
        /// <value>
        /// The account code.
        /// </value>
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>
        /// The name of the account.
        /// </value>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the accounting company CHH identifier.
        /// </summary>
        /// <value>
        /// The accounting company CHH identifier.
        /// </value>
        public string ChhId { get; set; }

        /// <summary>
        /// Gets or sets the financial year start date.
        /// </summary>
        /// <value>
        /// The financial year start date.
        /// </value>
        public DateTime FinancialYearStartDate { get; set; }

        /// <summary>
        /// Gets or sets the base coy curr.
        /// </summary>
        /// <value>
        /// The base coy curr.
        /// </value>
        public string BaseCoyCurr { get; set; }

        /// <summary>
        /// Gets or sets the minimum start date.
        /// </summary>
        /// <value>
        /// The minimum start date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime MinStartDate { get; set; }

        /// <summary>
        /// Gets or sets the maximum end date.
        /// </summary>
        /// <value>
        /// The maximum end date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime MaxEndDate { get; set; }

        /// <summary>
        /// Gets or sets the coy fin start date.
        /// </summary>
        /// <value>
        /// The coy fin start date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? CoyFinStartDate { get; set; }

        /// <summary>
        /// Gets or sets the coy fin end date.
        /// </summary>
        /// <value>
        /// The coy fin end date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? CoyFinEndDate { get; set; }

        /// <summary>
        /// Gets or sets the fin start date.
        /// </summary>
        /// <value>
        /// The fin start date.
        /// </value>
        public int finStartDate { get; set; }

        /// <summary>
        /// Gets or sets the fin start month.
        /// </summary>
        /// <value>
        /// The fin start month.
        /// </value>
        public int finStartMonth { get; set; }

        /// <summary>
        /// Gets or sets the fin end date.
        /// </summary>
        /// <value>
        /// The fin end date.
        /// </value>
        public int finEndDate { get; set; }

        /// <summary>
        /// Gets or sets the fin end month.
        /// </summary>
        /// <value>
        /// The fin end month.
        /// </value>
        public int finEndMonth { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel management office.
        /// </summary>
        /// <value>
        /// The type of the vessel management office.
        /// </value>
        public string VesselManagementOfficeType { get; set; }

        /// <summary>
        /// Gets or sets the account name description.
        /// </summary>
        /// <value>
        /// The account name description.
        /// </value>
        public string AccountNameDescription { get; set; }

        /// <summary>
        /// Gets or sets the fin period.
        /// </summary>
        /// <value>
        /// The fin period.
        /// </value>
        public string finPeriod { get; set; }

        /// <summary>
        /// Gets or sets active tab calss. eg. tab-1 ,tab-2.
        /// </summary>
        /// <value>
        /// The Active Mobile Tab Classe text field.
        /// </value>
        public string ActiveMobileTabClass { get; set; }

    }
}
