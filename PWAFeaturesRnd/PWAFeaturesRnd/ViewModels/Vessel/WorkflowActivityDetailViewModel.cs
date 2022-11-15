using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Vessel
{
	/// <summary>
	/// WorkflowDetailViewModel
	/// </summary>
	public class WorkflowActivityDetailViewModel
	{
		/// <summary>
		/// Gets or sets the workflow identifier.
		/// </summary>
		/// <value>
		/// The workflow identifier.
		/// </value>
		public string WorkflowName { get; set; }

		/// <summary>
		/// Gets or sets the possible workflow details.
		/// </summary>
		/// <value>
		/// The possible workflow details.
		/// </value>
		public List<WorkflowDetailViewModel> PossibleWorkflowDetails { get; set; }

	}
}