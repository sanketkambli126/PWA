using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Vessel
{
	public class LogsAndPossibleWorkFlowsResponseViewModel
	{
		/// <summary>
		/// Gets or sets the previous logs details.
		/// </summary>
		/// <value>
		/// The previous logs details.
		/// </value>
		public List<WorkflowDetailViewModel> PreviousLogsDetails { get; set; }

		/// <summary>
		/// Gets or sets the possible workflow details.
		/// </summary>
		/// <value>
		/// The possible workflow details.
		/// </value>
		public List<WorkflowDetailViewModel> PossibleWorkflowDetails { get; set; }

        /// <summary>
        /// The workflow group count
        /// </summary>
        public int WorkflowGroupCount { get; set; }
	}
}
