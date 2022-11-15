using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
	/// <summary>
	/// 
	/// </summary>
	public class IncidentAction
	{
		/// <summary>
		/// Gets or sets the action date.
		/// </summary>
		/// <value>
		/// The action date.
		/// </value>
		public System.DateTime ActionDate { get; set; }

		/// <summary>
		/// Gets or sets the action perf by.
		/// </summary>
		/// <value>
		/// The action perf by.
		/// </value>
		public System.String ActionPerfBy { get; set; }

		/// <summary>
		/// Gets or sets the name of the action perf by.
		/// </summary>
		/// <value>
		/// The name of the action perf by.
		/// </value>
		public System.String ActionPerfByName { get; set; }

		/// <summary>
		/// Gets or sets the action taken.
		/// </summary>
		/// <value>
		/// The action taken.
		/// </value>
		public System.String ActionTaken { get; set; }

		/// <summary>
		/// Gets or sets the action taken specify.
		/// </summary>
		/// <value>
		/// The action taken specify.
		/// </value>
		public System.String ActionTakenSpecify { get; set; }

		/// <summary>
		/// Gets or sets the action to be taken.
		/// </summary>
		/// <value>
		/// The action to be taken.
		/// </value>
		public System.String ActionToBeTaken { get; set; }

		/// <summary>
		/// Gets or sets the assigned to.
		/// </summary>
		/// <value>
		/// The assigned to.
		/// </value>
		public System.String AssignedTo { get; set; }

		/// <summary>
		/// Gets or sets the name of the assign to.
		/// </summary>
		/// <value>
		/// The name of the assign to.
		/// </value>
		public System.String AssignToName { get; set; }

		/// <summary>
		/// Gets or sets the closure.
		/// </summary>
		/// <value>
		/// The closure.
		/// </value>
		public Nullable<System.Boolean> Closure { get; set; }

		/// <summary>
		/// Gets or sets the closure date.
		/// </summary>
		/// <value>
		/// The closure date.
		/// </value>
		public Nullable<System.DateTime> ClosureDate { get; set; }

		/// <summary>
		/// Gets or sets the corr action desc.
		/// </summary>
		/// <value>
		/// The corr action desc.
		/// </value>
		public System.String CorrActionDesc { get; set; }

		/// <summary>
		/// Gets or sets the created date.
		/// </summary>
		/// <value>
		/// The created date.
		/// </value>
		public Nullable<System.DateTime> CreatedDate { get; set; }

		/// <summary>
		/// Gets or sets the deadline.
		/// </summary>
		/// <value>
		/// The deadline.
		/// </value>
		public Nullable<System.DateTime> Deadline { get; set; }

		/// <summary>
		/// Gets or sets the deleted.
		/// </summary>
		/// <value>
		/// The deleted.
		/// </value>
		public Nullable<System.Boolean> Deleted { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public System.String Description { get; set; }

		/// <summary>
		/// Gets or sets the iad identifier.
		/// </summary>
		/// <value>
		/// The iad identifier.
		/// </value>
		public System.String IadId { get; set; }

		/// <summary>
		/// Gets or sets the iae identifier.
		/// </summary>
		/// <value>
		/// The iae identifier.
		/// </value>
		public System.String IaeId { get; set; }

		/// <summary>
		/// Gets or sets the number.
		/// </summary>
		/// <value>
		/// The number.
		/// </value>
		public System.String Number { get; set; }

		/// <summary>
		/// Gets or sets the preventative.
		/// </summary>
		/// <value>
		/// The preventative.
		/// </value>
		public Nullable<System.Boolean> Preventative { get; set; }

		/// <summary>
		/// Gets or sets the recommendation identifier.
		/// </summary>
		/// <value>
		/// The recommendation identifier.
		/// </value>
		public System.String RecommendationId { get; set; }

		/// <summary>
		/// Gets or sets the specify.
		/// </summary>
		/// <value>
		/// The specify.
		/// </value>
		public System.String Specify { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public System.String Status { get; set; }

		/// <summary>
		/// Gets or sets the imr identifier.
		/// </summary>
		/// <value>
		/// The imr identifier.
		/// </value>
		public string ImrId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [closure locked].
		/// </summary>
		/// <value>
		///   <c>true</c> if [closure locked]; otherwise, <c>false</c>.
		/// </value>
		public bool ClosureLocked { get; set; }

		/// <summary>
		/// Gets or sets the status identifier.
		/// </summary>
		/// <value>
		/// The status identifier.
		/// </value>
		public System.String StatusId { get; set; }
	}
}
