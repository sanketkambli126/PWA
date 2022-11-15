namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectComponentHeirarchy
    {
        /// <summary>
        /// Gets or sets the PTR identifier.
        /// </summary>
        /// <value>
        /// The PTR identifier.
        /// </value>
        public string PtrId { get; set; }

        /// <summary>
        /// Gets or sets the PGR identifier.
        /// </summary>
        /// <value>
        /// The PGR identifier.
        /// </value>
        public string PgrId { get; set; }

        /// <summary>
        /// Gets or sets the top system area identifier.
        /// </summary>
        /// <value>
        /// The top system area identifier.
        /// </value>
        public string TopSystemAreaId { get; set; }

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
        /// Gets or sets a value indicating whether this instance is root.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is root; otherwise, <c>false</c>.
        /// </value>
        public bool IsRoot { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dual fuel types applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dual fuel types applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsDualFuelTypesApplicable { get; set; }
    }
}
