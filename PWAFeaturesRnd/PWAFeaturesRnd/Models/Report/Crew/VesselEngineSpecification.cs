using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Crew
{
	/// <summary>
	/// Custom contract to hold vessel engine specification
	/// </summary>
	public class VesselEngineSpecification
	{
		/// <summary>
		/// Gets or sets the maker.
		/// </summary>
		/// <value>
		/// The maker.
		/// </value>
		public string Maker { get; set; }

		/// <summary>
		/// Gets or sets the engine type.
		/// </summary>
		/// <value>
		/// The engine type.
		/// </value>
		public string EngineType { get; set; }

		/// <summary>
		/// Gets or sets the engine power.
		/// </summary>
		/// <value>
		/// The engine power.
		/// </value>
		public int? EnginePower { get; set; }

		/// <summary>
		/// Gets or sets the engine power unit.
		/// </summary>
		/// <value>
		/// The engine power unit.
		/// </value>
		public string EnginePowerUnit { get; set; }
	}
}
