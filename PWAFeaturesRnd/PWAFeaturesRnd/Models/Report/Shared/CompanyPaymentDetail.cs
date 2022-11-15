using System;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// CompanyPaymentDetail
	/// </summary>
	public class CompanyPaymentDetail
	{
		/// <summary>
		/// Gets or sets the cop credit days.
		/// </summary>
		/// <value>
		/// The cop credit days.
		/// </value>
		public int? CopCreditDays { get; set; }

		/// <summary>
		/// Gets or sets the cop updated on.
		/// </summary>
		/// <value>
		/// The cop updated on.
		/// </value>
		public DateTime CopUpdatedOn { get; set; }

		/// <summary>
		/// Gets or sets the CMP identifier.
		/// </summary>
		/// <value>
		/// The CMP identifier.
		/// </value>
		public string CmpId { get; set; }

		/// <summary>
		/// Gets or sets the cop updated by.
		/// </summary>
		/// <value>
		/// The cop updated by.
		/// </value>
		public string CopUpdatedBy { get; set; }

		/// <summary>
		/// Gets or sets the cop credit limit.
		/// </summary>
		/// <value>
		/// The cop credit limit.
		/// </value>
		public decimal? CopCreditLimit { get; set; }

		/// <summary>
		/// Gets or sets the cop identifier.
		/// </summary>
		/// <value>
		/// The cop identifier.
		/// </value>
		public string CopId { get; set; }

		/// <summary>
		/// Gets or sets the cop is on hold.
		/// </summary>
		/// <value>
		/// The cop is on hold.
		/// </value>
		public bool? CopIsOnHold { get; set; }

		/// <summary>
		/// Gets or sets the cop loc code.
		/// </summary>
		/// <value>
		/// The cop loc code.
		/// </value>
		public string CopLocCode { get; set; }

		/// <summary>
		/// Gets or sets the cop pay address.
		/// </summary>
		/// <value>
		/// The cop pay address.
		/// </value>
		public string CopPayAddress { get; set; }

		/// <summary>
		/// Gets or sets the cop pay count identifier.
		/// </summary>
		/// <value>
		/// The cop pay count identifier.
		/// </value>
		public string CopPayCntId { get; set; }

		/// <summary>
		/// Gets or sets the cop pay email.
		/// </summary>
		/// <value>
		/// The cop pay email.
		/// </value>
		public string CopPayEmail { get; set; }

		/// <summary>
		/// Gets or sets the cop pay fax.
		/// </summary>
		/// <value>
		/// The cop pay fax.
		/// </value>
		public string CopPayFax { get; set; }

		/// <summary>
		/// Gets or sets the name of the cop pay.
		/// </summary>
		/// <value>
		/// The name of the cop pay.
		/// </value>
		public string CopPayName { get; set; }

		/// <summary>
		/// Gets or sets the cop pay post code.
		/// </summary>
		/// <value>
		/// The cop pay post code.
		/// </value>
		public string CopPayPostCode { get; set; }

		/// <summary>
		/// Gets or sets the cop pay town.
		/// </summary>
		/// <value>
		/// The cop pay town.
		/// </value>
		public string CopPayTown { get; set; }

		/// <summary>
		/// Gets or sets the cop preferred comm.
		/// </summary>
		/// <value>
		/// The cop preferred comm.
		/// </value>
		public string CopPreferredComm { get; set; }

		/// <summary>
		/// Gets or sets the pat identifier hold reason.
		/// </summary>
		/// <value>
		/// The pat identifier hold reason.
		/// </value>
		public string PatIdHoldReason { get; set; }

		/// <summary>
		/// Gets or sets the cop start date.
		/// </summary>
		/// <value>
		/// The cop start date.
		/// </value>
		public DateTime? CopStartDate { get; set; }

		/// <summary>
		/// Gets or sets the rowguid.
		/// </summary>
		/// <value>
		/// The rowguid.
		/// </value>
		public Guid Rowguid { get; set; }

		/// <summary>
		/// Gets or sets the state of the cop.
		/// </summary>
		/// <value>
		/// The state of the cop.
		/// </value>
		public string CopState { get; set; }

	}
}
