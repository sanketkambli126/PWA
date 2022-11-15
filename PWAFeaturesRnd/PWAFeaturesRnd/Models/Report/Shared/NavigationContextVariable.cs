using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	public class NavigationContextVariable
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the NCV identifier.
		/// </summary>
		/// <value>
		/// The NCV identifier.
		/// </value>
		public string NcvId { get; set; }

		/// <summary>
		/// Gets or sets the name of the variable.
		/// </summary>
		/// <value>
		/// The name of the variable.
		/// </value>
		public string VariableName { get; set; }
	}
}
