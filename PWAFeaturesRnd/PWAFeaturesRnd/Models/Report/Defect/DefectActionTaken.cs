using System;

namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// 
	/// </summary>
	public class DefectActionTaken
	{
		/// <summary>
		/// Gets or sets the dat identifier.
		/// </summary>
		/// <value>
		/// The dat identifier.
		/// </value>
		public string DatId { get; set; }

		/// <summary>
		/// Gets or sets the dwo identifier.
		/// </summary>
		/// <value>
		/// The dwo identifier.
		/// </value>
		public string DwoId { get; set; }

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public string Action { get; set; }
		
		/// <summary>
		/// Gets or sets a value indicating whether is cleared.
		/// </summary>
		/// <value>
		///   <c>true</c> if cleared; otherwise, <c>false</c>.
		/// </value>
		public bool Cleared { get; set; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>
		/// The date.
		/// </value>
		public DateTime? Date { get; set; }

		/// <summary>
		/// Gets or sets the reported by.
		/// </summary>
		/// <value>
		/// The reported by.
		/// </value>
		public string ReportedBy { get; set; }

		/// <summary>
		/// Gets or sets the reported by identifier.
		/// </summary>
		/// <value>
		/// The reported by identifier.
		/// </value>
		public string ReportedById { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is system input.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is system input; otherwise, <c>false</c>.
		/// </value>
		public bool IsSystemInput { get; set; }
	}
}
