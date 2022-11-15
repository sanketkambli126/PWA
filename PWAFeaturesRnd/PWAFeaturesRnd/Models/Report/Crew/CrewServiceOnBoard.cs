using System;
using System.Collections.Generic;
using System.Text;

namespace PWAFeaturesRnd.Models.Report.Crew
{
	/// <summary>
	///   <br />Used for getting Crew Current Service Details
	/// </summary>
	public class CrewServiceOnBoard
    {
		/// <summary>Gets or sets the actual contract days.</summary>
		/// <value>The actual contract days.</value>
		public int ActualContractDays { get; set; }

		/// <summary>Gets or sets the budgeted rank description.</summary>
		/// <value>The budgeted rank description.</value>
		public string BudgetedRankDescription { get; set; }

		/// <summary>Gets or sets the contract company.</summary>
		/// <value>The contract company.</value>
		public string ContractCompany { get; set; }

		/// <summary>Gets or sets the name of the contract company.</summary>
		/// <value>The name of the contract company.</value>
		public string ContractCompanyName { get; set; }

		/// <summary>Gets or sets the contract end date.</summary>
		/// <value>The contract end date.</value>
		public Nullable<System.DateTime> ContractEndDate { get; set; }

		/// <summary>Gets or sets the contract start date.</summary>
		/// <value>The contract start date.</value>
		public Nullable<System.DateTime> ContractStartDate { get; set; }

		/// <summary>Gets or sets the type of the contract.</summary>
		/// <value>The type of the contract.</value>
		public string ContractType { get; set; }

		/// <summary>Gets or sets the contract type description.</summary>
		/// <value>The contract type description.</value>
		public string ContractTypeDescription { get; set; }

		/// <summary>Gets or sets the coordinating office.</summary>
		/// <value>The coordinating office.</value>
		public string CoordinatingOffice { get; set; }

		/// <summary>Gets or sets the crew service detail extensions.</summary>
		/// <value>The crew service detail extensions.</value>
		public List<CrewServiceDetailExtension> CrewServiceDetailExtensions { get; set; }

		/// <summary>Gets or sets the crew service identifier.</summary>
		/// <value>The crew service identifier.</value>
		public string CrewServiceId { get; set; }

		/// <summary>Gets or sets the crew service notes.</summary>
		/// <value>The crew service notes.</value>
		public List<CrewServiceNote> CrewServiceNotes { get; set; }

		/// <summary>Gets or sets the extension.</summary>
		/// <value>The extension.</value>
		public int Extension { get; set; }

		/// <summary>Gets or sets the extension unit.</summary>
		/// <value>The extension unit.</value>
		public string ExtensionUnit { get; set; }

		/// <summary>Gets or sets the initial length of the contract.</summary>
		/// <value>The initial length of the contract.</value>
		public int InitialContractLength { get; set; }

		/// <summary>Gets or sets the initial contract length unit.</summary>
		/// <value>The initial contract length unit.</value>
		public string InitialContractLengthUnit { get; set; }

		/// <summary>Gets or sets the joining port country identifier.</summary>
		/// <value>The joining port country identifier.</value>
		public string JoiningPortCountryId { get; set; }

		/// <summary>Gets or sets the joining port identifier.</summary>
		/// <value>The joining port identifier.</value>
		public string JoiningPortId { get; set; }

		/// <summary>Gets or sets the name of the joining port.</summary>
		/// <value>The name of the joining port.</value>
		public string JoiningPortName { get; set; }

		/// <summary>Gets or sets the notes.</summary>
		/// <value>The notes.</value>
		public string Notes { get; set; }

		/// <summary>Gets or sets the overlap days.</summary>
		/// <value>The overlap days.</value>
		public int OverlapDays { get; set; }

		/// <summary>Gets or sets the sign off date.</summary>
		/// <value>The sign off date.</value>
		public DateTime? SignOffDate { get; set; }

		/// <summary>Gets or sets the sign on date.</summary>
		/// <value>The sign on date.</value>
		public Nullable<System.DateTime> SignOnDate { get; set; }

		/// <summary>Gets or sets the transit contract end date.</summary>
		/// <value>The transit contract end date.</value>
		public Nullable<System.DateTime> TransitContractEndDate { get; set; }

		/// <summary>Gets or sets the transit contract start date.</summary>
		/// <value>The transit contract start date.</value>
		public Nullable<System.DateTime> TransitContractStartDate { get; set; }

		/// <summary>Gets or sets the transit days.</summary>
		/// <value>The transit days.</value>
		public int TransitDays { get; set; }

		/// <summary>Gets or sets the transit end date.</summary>
		/// <value>The transit end date.</value>
		public Nullable<System.DateTime> TransitEndDate { get; set; }

		/// <summary>Gets or sets the transit identifier.</summary>
		/// <value>The transit identifier.</value>
		public string TransitId { get; set; }

		/// <summary>Gets or sets the transit start date.</summary>
		/// <value>The transit start date.</value>
		public Nullable<System.DateTime> TransitStartDate { get; set; }

		/// <summary>Gets or sets the travel days.</summary>
		/// <value>The travel days.</value>
		public int TravelDays { get; set; }

		/// <summary>Gets or sets the name of the vessel.</summary>
		/// <value>The name of the vessel.</value>
		public string VesselName { get; set; }

		/// <summary>Gets or sets the name of the client.</summary>
		/// <value>The name of the client.</value>
		public string ClientName { get; set; }
	}
}
