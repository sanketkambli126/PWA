using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// ComponentCategoryTreeRequestViewModel
    /// </summary>
    public class ComponentCategoryTreeRequestViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

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
        /// Gets or sets a value indicating whether this instance is component click.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is component click; otherwise, <c>false</c>.
        /// </value>
        public bool IsComponentClick { get; set; }

        /// <summary>
        /// Gets or sets the functional area.
        /// </summary>
        /// <value>
        /// The functional area.
        /// </value>
        public SystemAreaFunctionalType? FunctionalArea { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        public string ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the alternate template identifier.
        /// </summary>
        /// <value>
        /// The alternate template identifier.
        /// </value>
        public string AlternateTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the parent component identifier.
        /// </summary>
        /// <value>
        /// The parent component identifier.
        /// </value>
        public string ParentComponentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can add component.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can add component; otherwise, <c>false</c>.
        /// </value>
        public bool CanAddComponent { get; set; }

        /// <summary>
        /// Gets or sets the component identifier.
        /// </summary>
        /// <value>
        /// The component identifier.
        /// </value>
        public string RequestComponentId { get; set; }
    }
}
