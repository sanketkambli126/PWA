namespace PWAFeaturesRnd.ViewModels.Crew
{
	/// <summary>
	/// Crew Experience Matrix Details Response View Model
	/// </summary>
	public class CrewExperienceMatrixDetailsResponseViewModel
	{
		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the name of the department.
		/// </summary>
		/// <value>
		/// The name of the department.
		/// </value>
		public string DepartmentName { get; set; }
	
		/// <summary>
		/// Gets or sets the name of the crew.
		/// </summary>
		/// <value>
		/// The name of the crew.
		/// </value>
		public string CrewName { get; set; }

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
		/// Gets or sets the encrypted vessel identifier.
		/// </summary>
		/// <value>
		/// The encrypted vessel identifier.
		/// </value>
		public string EncryptedVesselId { get; set; }

		/// <summary>
		/// Gets or sets the encrypted crew URL.
		/// </summary>
		/// <value>
		/// The encrypted crew URL.
		/// </value>
		public string EncryptedCrewURL { get; set; }

		/// <summary>
		/// Gets or sets the VMS experience indays.
		/// </summary>
		/// <value>
		/// The VMS experience indays.
		/// </value>
		public int VmsExperienceInDays { get; set; }

		/// <summary>
		/// Gets or sets the experience in years and months.
		/// </summary>
		/// <value>
		/// The experience in years and months.
		/// </value>
		public string ExperienceInYearsAndMonths { get; set; }
	}
}
