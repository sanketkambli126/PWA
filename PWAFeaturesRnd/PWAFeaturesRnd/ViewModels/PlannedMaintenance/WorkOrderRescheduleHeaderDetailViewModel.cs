using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Report.PlannedMaintenance;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
	/// <summary>
	/// Work Order Reschedule Header Detail View Model
	/// </summary>
	public class WorkOrderRescheduleHeaderDetailViewModel
	{
		#region Properties

		/// <summary>
		/// Gets or sets the reschedule status kpi.
		/// </summary>
		/// <value>
		/// The reschedule status kpi.
		/// </value>
		public string RescheduleStatusKPI { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status.
		/// </summary>
		/// <value>
		/// The reschedule status.
		/// </value>
		public string RescheduleStatus { get; set; }

		/// <summary>
		/// Gets or sets the process reschedule wo URL.
		/// </summary>
		/// <value>
		/// The process reschedule wo URL.
		/// </value>
		public string ProcessRescheduleWoUrl { get; set; }

		/// <summary>
		/// Gets or sets the short name of the interval type.
		/// </summary>
		/// <value>
		/// The short name of the interval type.
		/// </value>
		public string Interval { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the short name of the interval type.
		/// </summary>
		/// <value>
		/// The short name of the interval type.
		/// </value>
		public string IntervalTypeShortName { get; set; }
		
		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkOrderRescheduleHeaderDetailViewModel"/> class.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public WorkOrderRescheduleHeaderDetailViewModel(WorkOrderRescheduleHeaderDetail entity)
		{
			var rescheduleStatusKPI = SetRescheduleStatus(entity.RescheduleStatusId);
			RescheduleStatusKPI = rescheduleStatusKPI.ToString();
			RescheduleStatus = entity.RescheduleStatus;
			Interval = entity.IntervalValue + " " + entity.IntervalTypeShortName;
			IntervalTypeShortName = entity.IntervalTypeShortName.Trim();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Sets the reschedule status.
		/// </summary>
		/// <param name="rescheduleStatusId">The reschedule status identifier.</param>
		/// <returns></returns>
		private KPI SetRescheduleStatus(string rescheduleStatusId)
		{
			//NormalBrush = "{StaticResource BrushOrange}"
			//PreWarningBrush = "{StaticResource BrushPurple}"
			//CrticalBrush = "{StaticResource BrushRed}"
			//GoodBrush = "{StaticResource BrushGreen}"

			if (!string.IsNullOrWhiteSpace(rescheduleStatusId))
			{
				if (rescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Draft) || rescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Pending))
				{
					return KPI.Normal;
				}
				else if (rescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Approved))
				{
					return KPI.Good;
				}
				else if (rescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Revised))
				{
					return KPI.PreWarning;
				}
				else if (rescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Rejected))
				{
					return KPI.Critical;
				}
			}
			return KPI.Normal;
		}

		#endregion
	}
}
