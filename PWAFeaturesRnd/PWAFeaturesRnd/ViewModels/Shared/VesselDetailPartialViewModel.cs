using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// VesselDetailPartialViewModel
    /// </summary>
    public class VesselDetailPartialViewModel
    {
        /// <summary>
        /// Gets or sets the drop down identifier.
        /// </summary>
        /// <value>
        /// The drop down identifier.
        /// </value>
        public string DropDownId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show commercial.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show commercial; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowCommercial { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show haz occ.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show haz occ; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowHazOcc { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show crewing.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show crewing; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowCrewing { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show environment.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show environment; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowEnvironment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show financials.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show financials; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowFinancials { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show certificates.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show certificates; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowCertificates { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show defects.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show defects; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowDefects { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show PMS.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show PMS; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowPMS { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show procurement.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show procurement; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowProcurement { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can show inspections and ratings.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show inspections and ratings; otherwise, <c>false</c>.
		/// </value>
		public bool CanShowInspectionsAndRatings { get; set; }

		/// <summary>
		/// Gets or sets a value IsSeaPassage.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can show sea passage ; otherwise, <c>false</c>.
		/// </value>
		public bool IsSeaPassage { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

	}
}
