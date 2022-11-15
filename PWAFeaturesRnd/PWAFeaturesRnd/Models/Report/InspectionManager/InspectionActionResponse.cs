using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	public class InspectionActionResponse
	{
		/// <summary>
		/// Gets or sets the inspection identifier.
		/// </summary>
		/// <value>
		/// The inspection identifier.
		/// </value>
		public string InspectionId { get; set; }

		/// <summary>
		/// Gets or sets the finding identifier.
		/// </summary>
		/// <value>
		/// The finding identifier.
		/// </value>
		public string InspectionFindingId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the action date.
		/// </summary>
		/// <value>
		/// The action date.
		/// </value>
		public DateTime? ActionDate { get; set; }

		/// <summary>
		/// Gets or sets the action description.
		/// </summary>
		/// <value>
		/// The action description.
		/// </value>
		public string ActionDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is clear.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is clear; otherwise, <c>false</c>.
		/// </value>
		public bool IsClear { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the reported by.
		/// </summary>
		/// <value>
		/// The reported by.
		/// </value>
		public string ReportedBy { get; set; }
	}
}
