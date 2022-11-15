using System;

namespace PWAFeaturesRnd.Models.Report.Crew
{
	/// <summary>
	/// Medical Sign Off Response
	/// </summary>
	public class MedicalSignOffResponse
	{
		/// <summary>
		/// Gets or sets the set identifier.
		/// </summary>
		/// <value>
		/// The set identifier.
		/// </value>
		public string SetId { get; set; }

		/// <summary>
		/// Gets or sets the rank identifier.
		/// </summary>
		/// <value>
		/// The rank identifier.
		/// </value>
		public string RankId { get; set; }

		/// <summary>
		/// Gets or sets the rank description.
		/// </summary>
		/// <value>
		/// The rank description.
		/// </value>
		public string RankDescription { get; set; }

		/// <summary>
		/// Gets or sets the crew identifier.
		/// </summary>
		/// <value>
		/// The crew identifier.
		/// </value>
		public string CrewId { get; set; }

		/// <summary>
		/// Gets or sets the crew forename.
		/// </summary>
		/// <value>
		/// The crew forename.
		/// </value>
		public string CrewForename { get; set; }

		/// <summary>
		/// Gets or sets the name of the crew middle.
		/// </summary>
		/// <value>
		/// The name of the crew middle.
		/// </value>
		public string CrewMiddleName { get; set; }

		/// <summary>
		/// Gets or sets the crew surname.
		/// </summary>
		/// <value>
		/// The crew surname.
		/// </value>
		public string CrewSurname { get; set; }

		/// <summary>
		/// Gets or sets the nat identifier.
		/// </summary>
		/// <value>
		/// The nat identifier.
		/// </value>
		public string NatId { get; set; }

		/// <summary>
		/// Gets or sets the nationality.
		/// </summary>
		/// <value>
		/// The nationality.
		/// </value>
		public string Nationality { get; set; }

		/// <summary>
		/// Gets or sets the onboard days.
		/// </summary>
		/// <value>
		/// The onboard days.
		/// </value>
		public int? OnboardDays { get; set; }

		/// <summary>
		/// Gets or sets the sign on.
		/// </summary>
		/// <value>
		/// The sign on.
		/// </value>
		public DateTime? SignOn { get; set; }

		/// <summary>
		/// Gets or sets the sign off.
		/// </summary>
		/// <value>
		/// The sign off.
		/// </value>
		public DateTime? SignOff { get; set; }

		/// <summary>
		/// Gets or sets the reason.
		/// </summary>
		/// <value>
		/// The reason.
		/// </value>
		public string Reason { get; set; }

		/// <summary>
		/// Gets or sets the port off identifier.
		/// </summary>
		/// <value>
		/// The port off identifier.
		/// </value>
		public string PortOffId { get; set; }

		/// <summary>
		/// Gets or sets the port off.
		/// </summary>
		/// <value>
		/// The port off.
		/// </value>
		public string PortOff { get; set; }

		/// <summary>
		/// Gets or sets the count off identifier.
		/// </summary>
		/// <value>
		/// The count off identifier.
		/// </value>
		public string CntOffId { get; set; }

		/// <summary>
		/// Gets or sets the country off.
		/// </summary>
		/// <value>
		/// The country off.
		/// </value>
		public string CountryOff { get; set; }

		/// <summary>
		/// Gets or sets the status identifier.
		/// </summary>
		/// <value>
		/// The status identifier.
		/// </value>
		public string StatusId { get; set; }

		/// <summary>
		/// Gets or sets the current status description.
		/// </summary>
		/// <value>
		/// The current status description.
		/// </value>
		public string CurrentStatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the status end date.
		/// </summary>
		/// <value>
		/// The status end date.
		/// </value>
		public DateTime? StatusEndDate { get; set; }
	}
}
