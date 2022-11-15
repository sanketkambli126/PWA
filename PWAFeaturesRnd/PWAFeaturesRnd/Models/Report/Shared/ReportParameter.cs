using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	public class ReportParameter
	{
		/// <summary>
		/// Gets or sets the value to set.
		/// </summary>
		/// <value>
		/// The value to set.
		/// </value>
		public List<object> ValueToSet { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is parameter of base type.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is parameter of base type; otherwise, <c>false</c>.
		/// </value>
		public bool IsParameterOfBaseType { get; set; }

		/// <summary>
		/// Gets or sets the name of the control.
		/// </summary>
		/// <value>
		/// The name of the control.
		/// </value>
		public string ControlName { get; set; }

		/// <summary>
		/// Gets or sets the default value.
		/// </summary>
		/// <value>
		/// The default value.
		/// </value>
		public string DefaultValue { get; set; }

		/// <summary>
		/// Gets or sets the display sequence.
		/// </summary>
		/// <value>
		/// The display sequence.
		/// </value>
		public int DisplaySequence { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ReportParameter"/> is hidden.
		/// </summary>
		/// <value>
		///   <c>true</c> if hidden; otherwise, <c>false</c>.
		/// </value>
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets the type of the input control.
		/// </summary>
		/// <value>
		/// The type of the input control.
		/// </value>
		public InputControlType InputControlType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is nullable.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
		/// </value>
		public bool IsNullable { get; set; }

		/// <summary>
		/// Gets or sets the json parameters.
		/// </summary>
		/// <value>
		/// The json parameters.
		/// </value>
		public string JsonParameters { get; set; }

		/// <summary>
		/// Gets or sets the navigation context variable.
		/// </summary>
		/// <value>
		/// The navigation context variable.
		/// </value>
		public NavigationContextVariable NavigationContextVariable { get; set; }

		/// <summary>
		/// Gets or sets the NCV identifier.
		/// </summary>
		/// <value>
		/// The NCV identifier.
		/// </value>
		public string NcvId { get; set; }

		/// <summary>
		/// Gets or sets the name of the parameter.
		/// </summary>
		/// <value>
		/// The name of the parameter.
		/// </value>
		public string ParameterName { get; set; }

		/// <summary>
		/// Gets or sets the RPT dependent parameters.
		/// </summary>
		/// <value>
		/// The RPT dependent parameters.
		/// </value>
		public List<RptDependentParameter> RptDependentParameters { get; set; }

		/// <summary>
		/// Gets or sets the RRP identifier.
		/// </summary>
		/// <value>
		/// The RRP identifier.
		/// </value>
		public string RrpId { get; set; }

		/// <summary>
		/// Gets or sets the RRT identifier.
		/// </summary>
		/// <value>
		/// The RRT identifier.
		/// </value>
		public string RrtId { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the value set mode.
		/// </summary>
		/// <value>
		/// The value set mode.
		/// </value>
		public ValueSetMode ValueSetMode { get; set; }
	}
}
