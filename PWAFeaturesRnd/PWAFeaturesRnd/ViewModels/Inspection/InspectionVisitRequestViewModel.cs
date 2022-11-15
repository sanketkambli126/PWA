using System.Collections.Generic;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.VoyageReporting;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// ViewModel for holding Inspection Visit Request details
    /// </summary>
    public class InspectionVisitRequestViewModel
    {
        /// <summary>
        /// Gets or sets the department list.
        /// </summary>
        /// <value>
        /// The department list.
        /// </value>
        public List<Lookup> DepartmentList { get; set; }

        /// <summary>
        /// Gets or sets the operating list.
        /// </summary>
        /// <value>
        /// The operating list.
        /// </value>
        public List<Lookup> OperatingList { get; set; }

        /// <summary>
        /// Gets or sets the activity types list.
        /// </summary>
        /// <value>
        /// The activity types list.
        /// </value>
        public List<PosActivityType> ActivityTypesList { get; set; }

        /// <summary>
        /// Gets or sets the user title.
        /// </summary>
        /// <value>
        /// The user title.
        /// </value>
        public string UserTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the user fore.
        /// </summary>
        /// <value>
        /// The name of the user fore.
        /// </value>
        public string UserForeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user sur.
        /// </summary>
        /// <value>
        /// The name of the user sur.
        /// </value>
        public string UserSurName { get; set; }
    }
}
