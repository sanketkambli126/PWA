using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Vessel
{
	/// <summary>
	/// Logs And Possible Work Flows Response
	/// </summary>
	public class LogsAndPossibleWorkFlowsResponse
	{
		/// <summary>
		/// Gets or sets the possible workflow details.
		/// </summary>
		/// <value>
		/// The possible workflow details.
		/// </value>
		public List<PossibleWorkflowDetails> PossibleWorkflowDetails { get; set; }

		/// <summary>
		/// Gets or sets the previous logs details.
		/// </summary>
		/// <value>
		/// The previous logs details.
		/// </value>
		public List<PreviousLogsDetails> PreviousLogsDetails { get; set; }

	}
}
