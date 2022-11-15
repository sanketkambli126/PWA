using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// Defect Report Work Order Summary ViewModel
    /// </summary>
    public class DefectReportWorkOrderSummaryViewModel
    {
        /// <summary>
        /// Gets or sets the spare parts.
        /// </summary>
        /// <value>
        /// The spare parts.
        /// </value>
        public List<DefectReportWorkOrderPartsUsedViewModel> SpareParts { get; set; }

        /// <summary>
        /// Gets or sets the shore staff.
        /// </summary>
        /// <value>
        /// The shore staff.
        /// </value>
        public List<DefectReportWorkOrderRankViewModel> ShoreStaff { get; set; }

        /// <summary>
        /// Gets or sets the ship staff.
        /// </summary>
        /// <value>
        /// The ship staff.
        /// </value>
        public List<DefectReportWorkOrderRankViewModel> ShipStaff { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is shore staff.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is shore staff; otherwise, <c>false</c>.
        /// </value>
        public bool IsShoreStaff { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ship staff.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ship staff; otherwise, <c>false</c>.
        /// </value>
        public bool IsShipStaff { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; set; }
    }
}
