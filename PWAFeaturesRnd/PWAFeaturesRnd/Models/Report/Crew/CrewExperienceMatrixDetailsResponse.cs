namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Crew Experience Matrix Details Response
    /// </summary>
    public class CrewExperienceMatrixDetailsResponse
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public string DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>
        /// The name of the department.
        /// </value>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the name of the crew.
        /// </summary>
        /// <value>
        /// The name of the crew.
        /// </value>
        public string CrewName { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>
        public string RankId { get; set; }

        /// <summary>
        /// Gets or sets the name of the rank.
        /// </summary>
        /// <value>
        /// The name of the rank.
        /// </value>
        public string RankName { get; set; }

        /// <summary>
        /// Gets or sets the VMS experience years.
        /// </summary>
        /// <value>
        /// The VMS experience years.
        /// </value>
        public decimal VmsExperienceYears { get; set; }

        /// <summary>
        /// Gets or sets the VMS experience in days.
        /// </summary>
        /// <value>
        /// The VMS experience in days.
        /// </value>
        public int VmsExperienceInDays { get; set; }
    }
}
