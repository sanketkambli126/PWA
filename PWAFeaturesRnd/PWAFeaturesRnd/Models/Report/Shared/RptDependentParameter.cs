using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	public class RptDependentParameter
	{
		/// <summary>
		/// Gets or sets the dependent on identifier.
		/// </summary>
		/// <value>
		/// The dependent on identifier.
		/// </value>
		public string DependentOnId { get; set; }

		/// <summary>
		/// Gets or sets the dependent on property.
		/// </summary>
		/// <value>
		/// The dependent on property.
		/// </value>
		public string DependentOnProperty { get; set; }

		/// <summary>
		/// Gets or sets the DPD identifier.
		/// </summary>
		/// <value>
		/// The DPD identifier.
		/// </value>
		public string DpdId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the parameter identifier.
		/// </summary>
		/// <value>
		/// The parameter identifier.
		/// </value>
		public string ParameterId { get; set; }
	}
}
