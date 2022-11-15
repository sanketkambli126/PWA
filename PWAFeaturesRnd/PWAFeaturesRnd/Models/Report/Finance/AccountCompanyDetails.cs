using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;

namespace PWAFeaturesRnd.Models.Report.Finance
{
	/// <summary>
	/// Account Company  Details
	/// </summary>
	public class AccountCompanyDetails
    {
		/// <summary>
		/// Gets or sets the CHH identifier.
		/// </summary>
		/// <value>
		/// The CHH identifier.
		/// </value>
		public string ChhId { get; set; }

		/// <summary>
		/// Gets or sets the name of the coy CMP.
		/// </summary>
		/// <value>
		/// The name of the coy CMP.
		/// </value>
		public string CoyCmpName { get; set; }

		/// <summary>
		/// Gets or sets the name of the coy.
		/// </summary>
		/// <value>
		/// The name of the coy.
		/// </value>
		public string CoyName { get; set; }

		/// <summary>
		/// Gets or sets the coy curr.
		/// </summary>
		/// <value>
		/// The coy curr.
		/// </value>
		public string CoyCurr { get; set; }

		/// <summary>
		/// Gets or sets the type of the coy coy.
		/// </summary>
		/// <value>
		/// The type of the coy coy.
		/// </value>
		public string CoyCoyType { get; set; }

		/// <summary>
		/// Gets or sets the coy fin year start.
		/// </summary>
		/// <value>
		/// The coy fin year start.
		/// </value>
		public DateTime? CoyFinYearStart { get; set; }

		/// <summary>
		/// Gets or sets the coy fin year end.
		/// </summary>
		/// <value>
		/// The coy fin year end.
		/// </value>
		public DateTime? CoyFinYearEnd { get; set; }

		/// <summary>
		/// Gets or sets the coy local curr.
		/// </summary>
		/// <value>
		/// The coy local curr.
		/// </value>
		public string CoyLocalCurr { get; set; }

		/// <summary>
		/// Gets or sets the coy enable excel.
		/// </summary>
		/// <value>
		/// The coy enable excel.
		/// </value>
		public bool? CoyEnableExcel { get; set; }

		/// <summary>
		/// Gets or sets the coy general ledger cutoff date.
		/// </summary>
		/// <value>
		/// The coy general ledger cutoff date.
		/// </value>
		public DateTime? CoyGeneralLedgerCutoffDate { get; set; }

		/// <summary>
		/// Gets or sets the coy purchase ledger cutoff date.
		/// </summary>
		/// <value>
		/// The coy purchase ledger cutoff date.
		/// </value>
		public DateTime? CoyPurchaseLedgerCutoffDate { get; set; }

		/// <summary>
		/// Gets or sets the coy req approval.
		/// </summary>
		/// <value>
		/// The coy req approval.
		/// </value>
		public bool? CoyReqApproval { get; set; }

		/// <summary>
		/// Gets or sets the coy sales ledger cutoff date.
		/// </summary>
		/// <value>
		/// The coy sales ledger cutoff date.
		/// </value>
		public DateTime? CoySalesLedgerCutoffDate { get; set; }

		/// <summary>
		/// Gets or sets the coy MGMT start.
		/// </summary>
		/// <value>
		/// The coy MGMT start.
		/// </value>
		public DateTime? CoyMgmtStart { get; set; }

		/// <summary>
		/// Gets or sets the coy poreceived only.
		/// </summary>
		/// <value>
		/// The coy poreceived only.
		/// </value>
		public byte? CoyPoreceivedOnly { get; set; }

		/// <summary>
		/// Gets or sets the vessel management detail.
		/// </summary>
		/// <value>
		/// The vessel management detail.
		/// </value>
		public List<VesselManagementDetails> VesselManagementDetail { get; set; }

		/// <summary>
		/// Gets or sets the accounting company status code.
		/// </summary>
		/// <value>
		/// The accounting company status code.
		/// </value>
		public AccountingCompanyStatusCodeDetails AccountingCompanyStatusCode { get; set; }

		/// <summary>
		/// Gets or sets the company.
		/// </summary>
		/// <value>
		/// The company.
		/// </value>
		public CompanyDetails Company { get; set; }

		/// <summary>
		/// Gets or sets the accounting financial years.
		/// </summary>
		/// <value>
		/// The accounting financial years.
		/// </value>
		public List<AccountingFinancialYearDetails> AccountingFinancialYears { get; set; }
	}
}
