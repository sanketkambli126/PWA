using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Finance
{
    public class VesselManagementDetails
    {
		/// <summary>
		/// Gets or sets the VMD identifier.
		/// </summary>
		/// <value>
		/// The VMD identifier.
		/// </value>
		public string VmdId { get; set; }

		/// <summary>
		/// Gets or sets the VMD manage start.
		/// </summary>
		/// <value>
		/// The VMD manage start.
		/// </value>
		public DateTime VmdManageStart { get; set; }

		/// <summary>
		/// Gets or sets the VMD owner.
		/// </summary>
		/// <value>
		/// The VMD owner.
		/// </value>
		public string VmdOwner { get; set; }

		/// <summary>
		/// Gets or sets the VMD auxcode.
		/// </summary>
		/// <value>
		/// The VMD auxcode.
		/// </value>
		public string VmdAuxcode { get; set; }

		/// <summary>
		/// Gets or sets the name of the VMD vessel.
		/// </summary>
		/// <value>
		/// The name of the VMD vessel.
		/// </value>
		public string VmdVesselName { get; set; }

		/// <summary>
		/// Gets or sets the vmo identifier.
		/// </summary>
		/// <value>
		/// The vmo identifier.
		/// </value>
		public string VmoId { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the type of the vessel management office.
		/// </summary>
		/// <value>
		/// The type of the vessel management office.
		/// </value>
		public VesselManagementOfficeTypeDetails VesselManagementOfficeType { get; set; }
	}
}
