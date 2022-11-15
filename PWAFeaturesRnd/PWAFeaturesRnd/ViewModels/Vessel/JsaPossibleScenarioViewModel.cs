using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Vessel
{
	/// <summary>
	/// Jsa Possible Scenario View Model
	/// </summary>
	public class JsaPossibleScenarioViewModel
	{
		/// <summary>
		/// Gets or sets the workflow list.
		/// </summary>
		/// <value>
		/// The workflow list.
		/// </value>
		public List<WorkflowActivityDetailViewModel> WorkflowList { get; set; }

		/// <summary>
		/// Gets or sets the activity list.
		/// </summary>
		/// <value>
		/// The activity list.
		/// </value>
		public List<string> ActivityList { get; set; }
	}
}
