using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// Charter Detail View Model
	/// </summary>
	public class CharterDetailViewModel
	{
		/// <summary>
		/// Gets or sets the name of the charter.
		/// </summary>
		/// <value>
		/// The name of the charter.
		/// </value>
		public string CharterName { get; set; }

		/// <summary>
		/// Gets or sets the voyage number.
		/// </summary>
		/// <value>
		/// The voyage number.
		/// </value>
		public string VoyageNumber { get; set; }

		/// <summary>
		/// Gets or sets the charter number.
		/// </summary>
		/// <value>
		/// The charter number.
		/// </value>
		public string CharterNumber { get; set; }

		/// <summary>
		/// Gets or sets the trade.
		/// </summary>
		/// <value>
		/// The trade.
		/// </value>
		public string Trade { get; set; }

		/// <summary>
		/// Gets or sets the charter requirements list.
		/// </summary>
		/// <value>
		/// The charter requirements list.
		/// </value>
		public List<CharterRequirementsViewModel> CharterRequirementsList { get; set; }
	}
}
