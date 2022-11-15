using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// Document Detail Request
	/// </summary>
	public class DocumentDetailRequest
	{
		/// <summary>
		/// Gets or sets the SSM identifier.
		/// To pass single sub module id.
		/// </summary>
		/// <value>
		/// The SSM identifier.
		/// </value>
		public string SsmId { get; set; }

		/// <summary>
		/// Gets or sets the sub modules.
		/// To pass multiple sub module ids.
		/// </summary>
		/// <value>
		/// The sub modules.
		/// </value>
		public List<string> SubModules { get; set; }

		/// <summary>
		/// Gets or sets the DCT identifier.
		/// </summary>
		/// <value>
		/// The DCT identifier.
		/// </value>
		public string DctId { get; set; }

		/// <summary>
		/// Gets or sets the source identifier.
		/// To pass single document source id.
		/// </summary>
		/// <value>
		/// The source identifier.
		/// </value>
		public string SourceId { get; set; }

		/// <summary>
		/// Gets or sets the source ids.
		/// To pass multiple document source ids.
		/// </summary>
		/// <value>
		/// The source ids.
		/// </value>
		public List<string> DocumentSourceIds { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is vesel view.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is vesel view; otherwise, <c>false</c>.
		/// </value>
		public bool IsVeselView { get; set; }
	}
}
