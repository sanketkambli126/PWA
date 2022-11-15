using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Finance
{
	/// <summary>
	/// 
	/// </summary>
    public class AccountingFinancialYearDetails
    {
		/// <summary>
		/// Gets or sets the period.
		/// </summary>
		/// <value>
		/// The period.
		/// </value>
		public int? Period { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>
		/// The end date.
		/// </value>
		public DateTime EndDate { get; set; }
	}
}
