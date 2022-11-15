namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectHeaderDetail
    {
        /// <summary>
        /// Gets or sets the parent component identifier.
        /// </summary>
        /// <value>
        /// The parent component identifier.
        /// </value>
        public string ParentComponentId { get; set; }

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
        /// Gets or sets the defect number.
        /// </summary>
        /// <value>
        /// The defect number.
        /// </value>
        public string DefectNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the defect.
        /// </summary>
        /// <value>
        /// The name of the defect.
        /// </value>
        public string DefectName { get; set; }

        /// <summary>
        /// Gets or sets the type of the staff.
        /// </summary>
        /// <value>
        /// The type of the staff.
        /// </value>
        public string StaffType { get; set; }

        /// <summary>
        /// Gets or sets the type of the site.
        /// </summary>
        /// <value>
        /// The type of the site.
        /// </value>
        public string SiteType { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }
    }
}
