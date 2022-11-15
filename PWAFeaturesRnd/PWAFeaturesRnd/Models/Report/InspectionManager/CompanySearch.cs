using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Model to hold Company details while searching
    /// </summary>
    public class CompanySearch
    {
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>

        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>

        public string CountryId { get; set; }

        /// <summary>
        /// Gets or sets the type of the commodity.
        /// </summary>
        /// <value>
        /// The type of the commodity.
        /// </value>

        public string CommodityType { get; set; }

        /// <summary>
        /// Gets or sets the type of the contract.
        /// </summary>
        /// <value>
        /// The type of the contract.
        /// </value>

        public string ContractType { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>

        public string CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the company type ids.
        /// </summary>
        /// <value>
        /// The company type ids.
        /// </value>

        public List<string> CompanyTypeIds { get; set; }

        /// <summary>
        /// Gets or sets the type of the supplier.
        /// </summary>
        /// <value>
        /// The type of the supplier.
        /// </value>
        /// ToDo: Kalpesh SupplierType property name needs to change to SupplierContractType

        public SupplierContractType? SupplierType { get; set; }

        /// <summary>
        /// Gets or sets the fetch default currency
        /// </summary>
        /// <value>
        /// The fetch default currency.
        /// </value>

        public bool FetchDefaultCurrency { get; set; }

        /// <summary>
        /// Gets or sets the fetch supplier assessment.
        /// </summary>
        /// <value>
        /// The fetch supplier assessment.
        /// </value>

        public bool FetchSupplierAssessment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fetch default credit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch default credit]; otherwise, <c>false</c>.
        /// </value>

        public bool FetchDefaultCredit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fetch supported currency].
        /// </summary>
        /// <value>
        /// <c>true</c> if [fetch supported currency]; otherwise, <c>false</c>.
        /// </value>

        public bool FetchSupportedCurrency { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [fetch ap enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch ap enabled]; otherwise, <c>false</c>.
        /// </value>

        public bool FetchAPValid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tech vessel.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is tech vessel; otherwise, <c>false</c>.
        /// </value>

        public bool IsTechVessel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is crew vessel.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is crew vessel; otherwise, <c>false</c>.
        /// </value>

        public bool IsCrewVessel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is full text search.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is full text search; otherwise, <c>false</c>.
        /// </value>

        public bool IsFullTextSearch { get; set; }

        /// <summary>
        /// Gets or sets the responsible office identifier.
        /// </summary>
        /// <value>
        /// The responsible office identifier.
        /// </value>

        public string ResponsibleOfficeId { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>

        public string OwnerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fetch only ap enable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch only ap enable]; otherwise, <c>false</c>.
        /// </value>

        public bool FetchOnlyAPEnable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fetch only activated accounting companies].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch only activated accounting companies]; otherwise, <c>false</c>.
        /// </value>

        public bool FetchOnlyActivatedAccountingCompanies { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is agent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is agent; otherwise, <c>false</c>.
        /// </value>

        public bool IsAgent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fetch is certified].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch is certified]; otherwise, <c>false</c>.
        /// </value>

        public bool FetchIsCertified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fetch is supplier compliance].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch is supplier compliance]; otherwise, <c>false</c>.
        /// </value>

        public bool FetchIsSupplierCompliance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is company compliance.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is company compliance; otherwise, <c>false</c>.
        /// </value>

        public bool IsCompanyCompliance { get; set; }

        /// <summary>
        /// Gets or sets the company certified document identifier.
        /// </summary>
        /// <value>
        /// The company certified document identifier.
        /// </value>

        public string CompanyCertifiedDocId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [exclude delete type companies].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exclude delete type companies]; otherwise, <c>false</c>.
        /// </value>

        public bool ExcludeDeleteTypeCompanies { get; set; }

        /// <summary>
        /// Gets or sets the Excluded company type ids.
        /// </summary>
        /// <value>
        /// The Excluded company type ids.
        /// </value>

        public List<string> ExcludedCompanyTypeIds { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>

        public string CompanyId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [fetch intercompany].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch intercompany]; otherwise, <c>false</c>.
        /// </value>

        public bool FetchIntercompany { get; set; }

        /// <summary>
        /// Gets or sets the fetch vetting request.
        /// </summary>
        /// <value>
        /// The fetch vetting request.
        /// </value>

        public bool? FetchVettingRequest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dl agent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dl agent; otherwise, <c>false</c>.
        /// </value>

        public bool IsDlAgent { get; set; }

        public PagedRequest pageRequest { get; set; }
    }
}
