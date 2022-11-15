using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// This is the class for the OBCrewListRequest
    /// </summary>
    public class OBCrewListRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>        
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>        
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>        
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unsync crew required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is unsync crew required; otherwise, <c>false</c>.
        /// </value>        
        public bool IsUnsyncCrewRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is document detail required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is document detail required; otherwise, <c>false</c>.
        /// </value>        
        public bool IsDocumentDetailRequired { get; set; }

        /// <summary>
        /// Gets or sets the department ids.
        /// </summary>
        /// <value>
        /// The department ids.
        /// </value>
        public List<string> DepartmentIds { get; set; }
                
        /// <summary>
        /// Gets or sets the rank category ids.
        /// </summary>
        /// <value>
        /// The rank category ids.
        /// </value>
        public List<string> RankCategoryIds { get; set; }

        /// <summary>
        /// Gets or sets the overdue to date.
        /// </summary>
        /// <value>
        /// The overdue to date.
        /// </value>
        public DateTime? OverdueToDate { get; set; }

        /// <summary>
        /// Gets or sets the unplanned to date.
        /// </summary>
        /// <value>
        /// The unplanned to date.
        /// </value>
        public DateTime? UnplannedToDate { get; set; }
               
        /// <summary>
        /// Gets or sets the stage filter.
        /// </summary>
        /// <value>
        /// The stage filter.
        /// </value>
        public string StageFilter { get; set; }

        /// <summary>
		/// Gets or sets the crew change date.
		/// </summary>
		/// <value>
		/// The crew change date.
		/// </value>
		public DateTime? CrewChangeDate { get; set; }
    }
}
