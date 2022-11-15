using System;

namespace PWAFeaturesRnd.Models.Report.VesselManagement
{
	/// <summary>
	/// Onboard Senior Officer
	/// </summary>
	public class OnboardSeniorOfficer
	{
		/// <summary>
		/// Gets or sets the crew identifier.
		/// </summary>
		/// <value>
		/// The crew identifier.
		/// </value>
		public string CrewId { get; set; }

		/// <summary>
		/// Gets or sets the PCN number.
		/// </summary>
		/// <value>
		/// The PCN number.
		/// </value>
		public string PCNNumber { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the rank.
		/// </summary>
		/// <value>
		/// The rank.
		/// </value>
		public string Rank { get; set; }

		/// <summary>
		/// Gets or sets the rank description.
		/// </summary>
		/// <value>
		/// The rank description.
		/// </value>
		public string RankDescription { get; set; }

		/// <summary>
		/// Gets or sets the name of the fore.
		/// </summary>
		/// <value>
		/// The name of the fore.
		/// </value>
		public string ForeName { get; set; }

		/// <summary>
		/// Gets or sets the name of the sur.
		/// </summary>
		/// <value>
		/// The name of the sur.
		/// </value>
		public string SurName { get; set; }

		/// <summary>
		/// Gets or sets the name of the middle.
		/// </summary>
		/// <value>
		/// The name of the middle.
		/// </value>
		public string MiddleName { get; set; }

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <value>
		/// The full name.
		/// </value>
		public string FullName { get; }

		/// <summary>
		/// Gets or sets the joining date.
		/// </summary>
		/// <value>
		/// The joining date.
		/// </value>
		public DateTime? JoiningDate { get; set; }

		/// <summary>
		/// Gets or sets the relief due date.
		/// </summary>
		/// <value>
		/// The relief due date.
		/// </value>
		public DateTime? ReliefDueDate { get; set; }
	}
}
