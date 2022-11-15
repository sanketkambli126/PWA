using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// Defect Dashboard Response
	/// </summary>
	public class DefectDashboardResponse
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the vessel description.
		/// </summary>
		/// <value>
		/// The vessel description.
		/// </value>
		public string VesselDescription { get; set; }

		/// <summary>
		/// Gets or sets the vessel age.
		/// </summary>
		/// <value>
		/// The vessel age.
		/// </value>
		public DateTime? VesselBuiltDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is next position available.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is next position available; otherwise, <c>false</c>.
		/// </value>
		public bool IsNextPositionAvailable { get; set; }

		/// <summary>
		/// Gets or sets the due count.
		/// </summary>
		/// <value>
		/// The due count.
		/// </value>
		public int? DueCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue count.
		/// </summary>
		/// <value>
		/// The overdue count.
		/// </value>
		public int? OverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the defect rescheduled count.
		/// </summary>
		/// <value>
		/// The defect rescheduled count.
		/// </value>
		public int? DefectRescheduledCount { get; set; }

		/// <summary>
		/// Gets or sets the open defect count.
		/// </summary>
		/// <value>
		/// The open defect count.
		/// </value>
		public int? OpenDefectCount { get; set; }

		/// <summary>
		/// Gets or sets the closed defect count.
		/// </summary>
		/// <value>
		/// The closed defect count.
		/// </value>
		public int? ClosedDefectCount { get; set; }

		/// <summary>
		/// Gets or sets the off hire required count.
		/// </summary>
		/// <value>
		/// The off hire required count.
		/// </value>
		public int? OffHireRequiredCount { get; set; }

		/// <summary>
		/// Gets or sets the order count.
		/// </summary>
		/// <value>
		/// The order count.
		/// </value>
		public int? OrderCount { get; set; }

		/// <summary>
		/// Gets or sets the open work order.
		/// </summary>
		/// <value>
		/// The open work order.
		/// </value>
		public List<DefectOpenWorkOrder> OpenWorkOrder { get; set; }

		/// <summary>
		/// Gets or sets the category detail.
		/// </summary>
		/// <value>
		/// The category detail.
		/// </value>
		public List<DefectCategory> CategoryDetail { get; set; }

		/// <summary>
		/// Gets or sets the system area detail.
		/// </summary>
		/// <value>
		/// The system area detail.
		/// </value>
		public List<DefectSystemArea> SystemAreaDetail { get; set; }

		/// <summary>
		/// Gets or sets all defect count.
		/// </summary>
		/// <value>
		/// All defect count.
		/// </value>
		public int? AllDefectCount { get; set; }
	}
}
