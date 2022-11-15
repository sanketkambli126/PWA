namespace PWAFeaturesRnd.ViewModels.Dashboard
{
	/// <summary>
	/// 
	/// </summary>
	public class VesselDetailViewModel
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is type identifier required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is type identifier required; otherwise, <c>false</c>.
		/// </value>
		public bool IsTypeIdRequired { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the number.
		/// </summary>
		/// <value>
		/// The number.
		/// </value>
		public decimal Number { get; set; }
		/// <summary>
		/// Gets or sets the office.
		/// </summary>
		/// <value>
		/// The office.
		/// </value>
		public string Office { get; set; }
		/// <summary>
		/// Gets or sets the off hire.
		/// </summary>
		/// <value>
		/// The off hire.
		/// </value>
		public int OffHire { get; set; }
		/// <summary>
		/// Gets or sets the vetting.
		/// </summary>
		/// <value>
		/// The vetting.
		/// </value>
		public int Vetting { get; set; }
		/// <summary>
		/// Gets or sets the serious incident.
		/// </summary>
		/// <value>
		/// The serious incident.
		/// </value>
		public int SeriousIncident { get; set; }
		/// <summary>
		/// Gets or sets the ltif.
		/// </summary>
		/// <value>
		/// The ltif.
		/// </value>
		public int LTIF { get; set; }
		/// <summary>
		/// Gets or sets the PSC detertions.
		/// </summary>
		/// <value>
		/// The PSC detertions.
		/// </value>
		public int PSCDetertions { get; set; }
		/// <summary>
		/// Gets or sets the PSC definition.
		/// </summary>
		/// <value>
		/// The PSC definition.
		/// </value>
		public int PSCDef { get; set; }
		/// <summary>
		/// Gets or sets the week since last insp.
		/// </summary>
		/// <value>
		/// The week since last insp.
		/// </value>
		public int WeekSinceLastInsp { get; set; }
		/// <summary>
		/// Gets or sets the crew over due.
		/// </summary>
		/// <value>
		/// The crew over due.
		/// </value>
		public int CrewOverDue { get; set; }

		public string VoyageReportingRequestUrl { get; set; }
	}
}
