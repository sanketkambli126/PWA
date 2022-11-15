using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Crew
{
	/// <summary>
	/// Crew CV Request View Model
	/// </summary>
	public class CrewCvRequestViewModel
	{
		/// <summary>
		/// Gets or sets the encrypted crew identifier.
		/// </summary>
		/// <value>
		/// The encrypted crew identifier.
		/// </value>
		public string EncryptedCrewId { get; set; }

		/// <summary>
		/// Gets or sets the type of the report format.
		/// </summary>
		/// <value>
		/// The type of the report format.
		/// </value>
		public FormatTypes ReportFormatType { get; set; }

		/// <summary>
		/// Gets or sets the crew rank.
		/// </summary>
		/// <value>
		/// The crew rank.
		/// </value>
		public string CrewRank { get; set; }

		/// <summary>
		/// Gets or sets the name of the crew.
		/// </summary>
		/// <value>
		/// The name of the crew.
		/// </value>
		public string CrewName { get; set; }

		/// <summary>
		/// Gets or sets the report format.
		/// </summary>
		/// <value>
		/// The report format.
		/// </value>
		public string ReportFormat { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include address.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include address; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludeAddress { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include contact.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include contact; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludeContact { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include picture.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include picture; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludePicture { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include summary of competence.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include summary of competence; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludeSummaryOfCompetence { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include on shore history.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include on shore history; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludeOnShoreHistory { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include service details.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include service details; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludeServiceDetails { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include documents.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include documents; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludeDocuments { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include notes.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include notes; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludeNotes { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include medical records.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include medical records; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncludeMedicalRecords { get; set; }
	}
}
