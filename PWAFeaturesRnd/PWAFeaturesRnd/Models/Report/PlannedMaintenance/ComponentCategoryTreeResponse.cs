using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
    /// <summary>
    /// Tree of Component & Category
    /// </summary>
    public class ComponentCategoryTreeResponse
    {
        /// <summary>
        /// Gets or sets the sya identifier.
        /// </summary>
        /// <value>
        /// The sya identifier.
        /// </value>
        public string SyaId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the component identifier.
        /// </summary>
        /// <value>
        /// The component identifier.
        /// </value>
        public string ComponentId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the child category component.
        /// </summary>
        /// <value>
        /// The child category component.
        /// </value>
        public List<ComponentCategoryTreeResponse> ChildCategoryComponent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can add component.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can add component; otherwise, <c>false</c>.
        /// </value>
        public bool CanAddComponent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [parent component required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [parent component required]; otherwise, <c>false</c>.
        /// </value>
        public bool ParentComponentRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has child.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has child; otherwise, <c>false</c>.
        /// </value>
        public bool HasChild { get; set; }

        /// <summary>
        /// Gets or sets the number of children.
        /// </summary>
        /// <value>
        /// The number of children.
        /// </value>
        public int? NumberOfChildren { get; set; }

        /// <summary>
        /// Gets or sets the functional area.
        /// </summary>
        /// <value>
        /// The functional area.
        /// </value>
        public int? FunctionalArea { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the parent code.
        /// </summary>
        /// <value>
        /// The parent code.
        /// </value>
        public string ParentCode { get; set; }

        /// <summary>
        /// Gets or sets the mapped system area.
        /// </summary>
        /// <value>
        /// The mapped system area.
        /// </value>
        public string MappedSystemArea { get; set; }
    }
}
