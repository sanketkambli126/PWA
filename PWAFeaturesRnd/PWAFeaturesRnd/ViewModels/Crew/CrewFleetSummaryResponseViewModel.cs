namespace PWAFeaturesRnd.ViewModels.Crew
{
	/// <summary>
	/// Crew Fleet Summary Response VM
	/// </summary>
	public class CrewFleetSummaryResponseViewModel
    {
		/// <summary>
		/// Gets or sets the experience matrix vessel count.
		/// </summary>
		/// <value>
		/// The experience matrix vessel count.
		/// </value>
		public int ExperienceMatrixVesselCount { get; set; }

		/// <summary>
		/// Gets or sets the experience matrix priority.
		/// </summary>
		/// <value>
		/// The experience matrix priority.
		/// </value>
		public int ExperienceMatrixPriority { get; set; }

		/// <summary>
		/// Gets or sets the experience matrix information.
		/// </summary>
		/// <value>
		/// The experience matrix information.
		/// </value>
		public string ExperienceMatrixInfo { get; set; }
	}
}
