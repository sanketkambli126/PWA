using System;
using System.Collections.Generic;
using System.Text;

namespace PWAFeaturesRnd.Models.Report.Crew
{
	/// <summary>
	///   <br /> class composed in crew onboard
	/// </summary>
	public class CrewServiceDetailExtension
    {

		/// <summary>Gets or sets a value indicating whether [ext agreed by seafarer].</summary>
		/// <value>
		///   <c>true</c> if [ext agreed by seafarer]; otherwise, <c>false</c>.</value>
		public bool ExtAgreedBySeafarer { get; set; }

		/// <summary>Gets or sets the ext extended by.</summary>
		/// <value>The ext extended by.</value>
		public string ExtExtendedBy { get; set; }

		/// <summary>Gets or sets the ext extended on.</summary>
		/// <value>The ext extended on.</value>
		public DateTime? ExtExtendedOn { get; set; }

		/// <summary>Gets or sets the ext extension reason.</summary>
		/// <value>The ext extension reason.</value>
		public int ExtExtensionReason { get; set; }

		/// <summary>Gets or sets the set extension valid until.</summary>
		/// <value>The set extension valid until.</value>
		public DateTime? SetExtensionValidUntil { get; set; }

		/// <summary>Gets or sets a value indicating whether [set is extension approved].</summary>
		/// <value>
		///   <c>true</c> if [set is extension approved]; otherwise, <c>false</c>.</value>
		public bool SetIsExtensionApproved { get; set; }
	}
}
