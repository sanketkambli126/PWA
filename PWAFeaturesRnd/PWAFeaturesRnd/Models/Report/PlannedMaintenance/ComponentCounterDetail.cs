using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// ComponentCounterDetail
	/// </summary>
	public class ComponentCounterDetail
	{
		/// <summary>
		/// Gets or sets the total running hours.
		/// </summary>
		/// <value>
		/// The total running hours.
		/// </value>
		public int? TotalRunningHours { get; set; }

		/// <summary>
		/// Gets or sets the running hours.
		/// </summary>
		/// <value>
		/// The running hours.
		/// </value>
		public int? RunningHours { get; set; }

		/// <summary>
		/// Gets or sets the reading date.
		/// </summary>
		/// <value>
		/// The reading date.
		/// </value>
		public DateTime? ReadingDate { get; set; }

		/// <summary>
		/// Gets or sets the forecast average.
		/// </summary>
		/// <value>
		/// The forecast average.
		/// </value>
		public int? ForecastAverage { get; set; }

		/// <summary>
		/// Gets or sets the start run time.
		/// </summary>
		/// <value>
		/// The start run time.
		/// </value>
		public int? StartRunTime { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Gets or sets the interval type identifier.
		/// </summary>
		/// <value>
		/// The interval type identifier.
		/// </value>
		public string IntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the type of the interval.
		/// </summary>
		/// <value>
		/// The type of the interval.
		/// </value>
		public string IntervalType { get; set; }

		/// <summary>
		/// Gets or sets the actual average.
		/// </summary>
		/// <value>
		/// The actual average.
		/// </value>
		public int? ActualAverage { get; set; }

		/// <summary>
		/// Gets or sets the component heirarchy.
		/// </summary>
		/// <value>
		/// The component heirarchy.
		/// </value>
		public string ComponentHeirarchy { get; set; }

		/// <summary>
		/// Gets or sets the parent total reading.
		/// </summary>
		/// <value>
		/// The parent total reading.
		/// </value>
		public int? ParentTotalReading { get; set; }

		/// <summary>
		/// Gets or sets the parent average.
		/// </summary>
		/// <value>
		/// The parent average.
		/// </value>
		public int? ParentAverage { get; set; }

		/// <summary>
		/// Gets or sets the name of the parent component.
		/// </summary>
		/// <value>
		/// The name of the parent component.
		/// </value>
		public string ParentComponentName { get; set; }

		/// <summary>
		/// Gets or sets the parent component run time identifier.
		/// </summary>
		/// <value>
		/// The parent component run time identifier.
		/// </value>
		public string ParentComponentRunTimeId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the component run time identifier.
		/// </summary>
		/// <value>
		/// The component run time identifier.
		/// </value>
		public string ComponentRunTimeId { get; set; }

		/// <summary>
		/// Gets or sets the parent reading date.
		/// </summary>
		/// <value>
		/// The parent reading date.
		/// </value>
		public DateTime? ParentReadingDate { get; set; }

		/// <summary>
		/// Gets or sets the component history identifier.
		/// </summary>
		/// <value>
		/// The component history identifier.
		/// </value>
		public string ComponentHistoryId { get; set; }
	}
}
