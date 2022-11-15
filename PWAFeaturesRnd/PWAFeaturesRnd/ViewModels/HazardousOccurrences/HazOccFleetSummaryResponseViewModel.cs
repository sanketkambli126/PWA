using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
	/// <summary>
	/// HazOcc Fleet Summary Response
	/// </summary>
	public class HazOccFleetSummaryResponseViewModel
	{
		/// <summary>
		/// Gets or sets the serious incidents count.
		/// </summary>
		/// <value>
		/// The serious incidents count.
		/// </value>
		public int SeriousIncidentsCount { get; set; }

		/// <summary>
		/// Gets or sets the serious incidents priority.
		/// </summary>
		/// <value>
		/// The serious incidents priority.
		/// </value>
		public int SeriousIncidentsPriority { get; set; }

		/// <summary>
		/// Gets or sets the serious incidents information.
		/// </summary>
		/// <value>
		/// The serious incidents information.
		/// </value>
		public string SeriousIncidentsInfo { get; set; }

		/// <summary>
		/// Gets or sets the ltif count.
		/// </summary>
		/// <value>
		/// The ltif count.
		/// </value>
		public int LtifCount { get; set; }

		/// <summary>
		/// Gets or sets the ltif priority.
		/// </summary>
		/// <value>
		/// The ltif priority.
		/// </value>
		public int LtifPriority { get; set; }

		/// <summary>
		/// Gets or sets the ltif information.
		/// </summary>
		/// <value>
		/// The ltif information.
		/// </value>
		public string LtifInfo { get; set; }

		/// <summary>
		/// Gets or sets the oil spill count.
		/// </summary>
		/// <value>
		/// The oil spill count.
		/// </value>
		public int OilSpillCount { get; set; }

		/// <summary>
		/// Gets or sets the oil spill priority.
		/// </summary>
		/// <value>
		/// The oil spill priority.
		/// </value>
		public int OilSpillPriority { get; set; }

		/// <summary>
		/// Gets or sets the oil spill information.
		/// </summary>
		/// <value>
		/// The oil spill information.
		/// </value>
		public string OilSpillInfo { get; set; }
	}
}
