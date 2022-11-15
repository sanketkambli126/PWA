using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.ViewModels.Common
{
	/// <summary>
	/// 
	/// </summary>
	public class Select2ResponseViewModel<T>
	{
		/// <summary>
		/// Gets or sets the results.
		/// </summary>
		/// <value>
		/// The results.
		/// </value>
		public T Results { get; set; }

		/// <summary>
		/// Gets or sets the pagination.
		/// </summary>
		/// <value>
		/// The pagination.
		/// </value>
		public Pagination Pagination { get; set; }
	}
}
