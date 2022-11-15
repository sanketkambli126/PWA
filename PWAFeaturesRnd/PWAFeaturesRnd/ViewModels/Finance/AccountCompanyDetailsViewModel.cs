using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Finance
{
	public class AccountCompanyDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the IsPOReceivedOnly
        /// </summary>
        /// <value>
		/// The is po received only.
		/// </value>
        public bool IsPOReceivedOnly { get; set; }

        /// <summary>
		/// Gets or sets the accounting financial years.
		/// </summary>
		/// <value>
		/// The accounting financial years.
		/// </value>
		public List<AccountingFinancialYearDetailsViewModel> AccountingFinancialYears { get; set; }

        /// <summary>
        /// Gets or sets the base currency.
        /// </summary>
        public string BaseCoyCurr { get; set; }

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
		/// Gets or sets the CHH identifier.
		/// </summary>
		/// <value>
		/// The CHH identifier.
		/// </value>
		public string ChhId { get; set; }

		/// <summary>
		/// Gets or sets the type of the vessel management office.
		/// </summary>
		/// <value>
		/// The type of the vessel management office.
		/// </value>
		public string VesselManagementOfficeType { get; set; }
	}
}
