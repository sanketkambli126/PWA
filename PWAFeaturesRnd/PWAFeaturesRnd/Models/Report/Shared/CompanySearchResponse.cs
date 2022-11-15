using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// Company Search Response
	/// </summary>
	public class CompanySearchResponse
	{
		/// <summary>
		/// Gets or sets a value indicating whether this instance is company certified.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is company certified; otherwise, <c>false</c>.
		/// </value>
		public bool IsCompanyCertified { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is agent.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is agent; otherwise, <c>false</c>.
		/// </value>
		public bool IsAgent { get; set; }

		/// <summary>
		/// Gets or sets the management end date.
		/// </summary>
		/// <value>
		/// The management end date.
		/// </value>
		public DateTime? ManagementEndDate { get; set; }

		/// <summary>
		/// Gets or sets the accounting company identifier.
		/// </summary>
		/// <value>
		/// The accounting company identifier.
		/// </value>
		public string AccountingCompanyId { get; set; }

		/// <summary>
		/// Gets or sets the type of the management.
		/// </summary>
		/// <value>
		/// The type of the management.
		/// </value>
		public string ManagementType { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the is ap valid.
		/// </summary>
		/// <value>
		/// The is ap valid.
		/// </value>
		public bool? IsAPValid { get; set; }

		/// <summary>
		/// Gets or sets the no of invoices.
		/// </summary>
		/// <value>
		/// The no of invoices.
		/// </value>
		public int? NoOfInvoices { get; set; }

		/// <summary>
		/// Gets or sets the credit days.
		/// </summary>
		/// <value>
		/// The credit days.
		/// </value>
		public int? CreditDays { get; set; }

		/// <summary>
		/// Gets or sets the post code.
		/// </summary>
		/// <value>
		/// The post code.
		/// </value>
		public string PostCode { get; set; }

		/// <summary>
		/// Gets or sets the web address.
		/// </summary>
		/// <value>
		/// The web address.
		/// </value>
		public string WebAddress { get; set; }

		/// <summary>
		/// Gets or sets the telephone.
		/// </summary>
		/// <value>
		/// The telephone.
		/// </value>
		public string Telephone { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is company compliance.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is company compliance; otherwise, <c>false</c>.
		/// </value>
		public bool IsCompanyCompliance { get; set; }

		/// <summary>
		/// Gets or sets the fax.
		/// </summary>
		/// <value>
		/// The fax.
		/// </value>
		public string Fax { get; set; }

		/// <summary>
		/// Gets or sets the supplier currencies.
		/// </summary>
		/// <value>
		/// The supplier currencies.
		/// </value>
		public List<string> SupplierCurrencies { get; set; }

		/// <summary>
		/// Gets or sets the type of the supplier.
		/// </summary>
		/// <value>
		/// The type of the supplier.
		/// </value>
		public string SupplierType { get; set; }

		/// <summary>
		/// Gets or sets the supplier rating.
		/// </summary>
		/// <value>
		/// The supplier rating.
		/// </value>
		public SupplierRating? SupplierRating { get; set; }

		/// <summary>
		/// Gets or sets the country identifier.
		/// </summary>
		/// <value>
		/// The country identifier.
		/// </value>
		public string CountryId { get; set; }

		/// <summary>
		/// Gets or sets the is marcas.
		/// </summary>
		/// <value>
		/// The is marcas.
		/// </value>
		public bool? IsMARCAS { get; set; }

		/// <summary>
		/// Gets or sets the town.
		/// </summary>
		/// <value>
		/// The town.
		/// </value>
		public string Town { get; set; }

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public string State { get; set; }

		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value>
		/// The currency.
		/// </value>
		public string Currency { get; set; }

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>
		/// The country.
		/// </value>
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>
		/// The address.
		/// </value>
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the name of the company.
		/// </summary>
		/// <value>
		/// The name of the company.
		/// </value>
		public string CompanyName { get; set; }

		/// <summary>
		/// Gets or sets the company identifier.
		/// </summary>
		/// <value>
		/// The company identifier.
		/// </value>
		public string CompanyId { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the company compliance document identifier.
		/// </summary>
		/// <value>
		/// The company compliance document identifier.
		/// </value>
		public string CompanyComplianceDocId { get; set; }

	}
}
