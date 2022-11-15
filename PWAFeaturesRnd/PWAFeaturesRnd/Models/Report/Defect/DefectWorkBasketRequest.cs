using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// Defect Work Basket Request
	/// </summary>
	public class DefectWorkBasketRequest
	{
		/// <summary>
		/// Gets or sets the fleet identifier.
		/// </summary>
		/// <value>
		/// The fleet identifier.
		/// </value>
		public string FleetId { get; set; }

		/// <summary>
		/// Gets or sets the type of the menu.
		/// </summary>
		/// <value>
		/// The type of the menu.
		/// </value>
		public string MenuType { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Gets or sets to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets the top system area identifier.
		/// </summary>
		/// <value>
		/// The top system area identifier.
		/// </value>
		public string TopSystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is overdue.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is overdue; otherwise, <c>false</c>.
		/// </value>
		public bool IsOverdue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is due.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is due; otherwise, <c>false</c>.
		/// </value>
		public bool IsDue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [added in damage form].
		/// </summary>
		/// <value>
		///   <c>true</c> if [added in damage form]; otherwise, <c>false</c>.
		/// </value>
		public bool AddedInDamageForm { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is off hire.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
		/// </value>
		public bool IsOffHire { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical { get; set; }

		/// <summary>
		/// Gets or sets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public List<string> Priority { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public List<string> Status { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public List<string> Type { get; set; }

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>
		/// The category.
		/// </value>
		public List<string> Category { get; set; }

		/// <summary>
		/// Gets or sets the defect system area identifier.
		/// </summary>
		/// <value>
		/// The defect system area identifier.
		/// </value>
		public List<string> DefectSystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [guarantee claim required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [guarantee claim required]; otherwise, <c>false</c>.
		/// </value>
		public bool GuaranteeClaimRequired { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is awaiting spares.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is awaiting spares; otherwise, <c>false</c>.
		/// </value>
		public bool IsAwaitingSpares { get; set; }
	}
}
