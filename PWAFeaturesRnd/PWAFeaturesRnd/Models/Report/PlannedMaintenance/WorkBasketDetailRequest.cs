using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Work Basket Detail Request
	/// </summary>
	public class WorkBasketDetailRequest
	{
		/// <summary>
		/// Gets or sets the show in range work orders.
		/// </summary>
		/// <value>
		/// The show in range work orders.
		/// </value>
		public bool? ShowInRangeWorkOrders { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [calculate recurrence].
		/// </summary>
		/// <value>
		///   <c>true</c> if [calculate recurrence]; otherwise, <c>false</c>.
		/// </value>
		public bool CalculateRecurrence { get; set; }

		/// <summary>
		/// Gets or sets the show rounds work order.
		/// </summary>
		/// <value>
		/// The show rounds work order.
		/// </value>
		public bool? ShowRoundsWorkOrder { get; set; }

		/// <summary>
		/// Gets or sets the show standing work orders.
		/// </summary>
		/// <value>
		/// The show standing work orders.
		/// </value>
		public bool? ShowStandingWorkOrders { get; set; }

		/// <summary>
		/// Gets or sets the show reschedule work orders.
		/// </summary>
		/// <value>
		/// The show reschedule work orders.
		/// </value>
		public bool? ShowRescheduleWorkOrders { get; set; }

		/// <summary>
		/// Gets or sets the show jsa required work orders.
		/// </summary>
		/// <value>
		/// The show jsa required work orders.
		/// </value>
		public bool? ShowJSARequiredWorkOrders { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show all schedule task].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show all schedule task]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowAllScheduleTask { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is recurrence required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is recurrence required; otherwise, <c>false</c>.
		/// </value>
		public bool IsRecurrenceRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show previous months overdue].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show previous months overdue]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowPreviousMonthsOverdue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show current month overdue].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show current month overdue]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowCurrentMonthOverdue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show over due].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show over due]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowOverDue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show due].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show due]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowDue { get; set; }

		/// <summary>
		/// Gets or sets the office approval.
		/// </summary>
		/// <value>
		/// The office approval.
		/// </value>
		public int OfficeApproval { get; set; }

		/// <summary>
		/// Gets or sets the criticality.
		/// </summary>
		/// <value>
		/// The criticality.
		/// </value>
		public int Criticality { get; set; }

		/// <summary>
		/// Gets or sets the job type ids.
		/// </summary>
		/// <value>
		/// The job type ids.
		/// </value>
		public List<string> JobTypeIds { get; set; }

		/// <summary>
		/// Gets or sets the work order status ids.
		/// </summary>
		/// <value>
		/// The work order status ids.
		/// </value>
		public List<string> WorkOrderStatusIds { get; set; }

		/// <summary>
		/// Gets or sets the responsibility ids.
		/// </summary>
		/// <value>
		/// The responsibility ids.
		/// </value>
		public List<string> ResponsibilityIds { get; set; }

		/// <summary>
		/// Gets or sets the department ids.
		/// </summary>
		/// <value>
		/// The department ids.
		/// </value>
		public List<string> DepartmentIds { get; set; }

		/// <summary>
		/// Gets or sets the parent component identifier.
		/// </summary>
		/// <value>
		/// The parent component identifier.
		/// </value>
		public string ParentComponentId { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the category identifier.
		/// </summary>
		/// <value>
		/// The category identifier.
		/// </value>
		public string CategoryId { get; set; }

		/// <summary>
		/// Gets or sets the top system area identifier.
		/// </summary>
		/// <value>
		/// The top system area identifier.
		/// </value>
		public string TopSystemAreaId { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the job interval type ids.
		/// </summary>
		/// <value>
		/// The job interval type ids.
		/// </value>
		public List<string> JobIntervalTypeIds { get; set; }

		/// <summary>
		/// Gets or sets the reschedule type ids.
		/// </summary>
		/// <value>
		/// The reschedule type ids.
		/// </value>
		public List<string> RescheduleTypeIds { get; set; }

		/// <summary>
		/// Gets or sets the exclude work order status ids.
		/// </summary>
		/// <value>
		/// The exclude work order status ids.
		/// </value>
		public List<string> ExcludeWorkOrderStatusIds { get; set; }
	}
}
