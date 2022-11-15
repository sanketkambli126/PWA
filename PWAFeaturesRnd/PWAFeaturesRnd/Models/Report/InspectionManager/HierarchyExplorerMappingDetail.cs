using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Hierarchy Explorer Mapping Detail
    /// </summary>
    public class HierarchyExplorerMappingDetail
    {
        /// <summary>
        /// Gets or sets the hem identifier.
        /// </summary>
        /// <value>
        /// The hem identifier.
        /// </value>

        public string HemId { get; set; }

        /// <summary>
        /// Gets or sets the source identifier reference.
        /// </summary>
        /// <value>
        /// The source identifier reference.
        /// </value>

        public string SourceIdReference { get; set; }

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>

        public string SourceId { get; set; }

        /// <summary>
        /// Gets or sets the PGR identifier.
        /// </summary>
        /// <value>
        /// The PGR identifier.
        /// </value>

        public string PgrId { get; set; }

        /// <summary>
        /// Gets or sets the PTR identifier.
        /// </summary>
        /// <value>
        /// The PTR identifier.
        /// </value>

        public string PtrId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>

        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the maker.
        /// </summary>
        /// <value>
        /// The maker.
        /// </value>

        public string Maker { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>

        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the designer.
        /// </summary>
        /// <value>
        /// The designer.
        /// </value>

        public string Designer { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>

        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the system area.
        /// </summary>
        /// <value>
        /// The system area.
        /// </value>

        public string SystemArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
        /// </value>

        public bool IsCritical { get; set; }

        /// <summary>
        /// Gets or sets the class code.
        /// </summary>
        /// <value>
        /// The class code.
        /// </value>
        public string ClassCode { get; set; }
    }
}
