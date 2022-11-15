using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PWAFeaturesRnd.Common.Converter;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report;

namespace PWAFeaturesRnd.ViewModels.Report
{
	public class GeneralLedgerViewModel
	{
		/// <summary>
		/// Gets or sets the year list.
		/// </summary>
		/// <value>
		/// The year list.
		/// </value>
		public List<LookUp> YearList { get; set; }

		/// <summary>
		/// Gets or sets the account list.
		/// </summary>
		/// <value>
		/// The account list.
		/// </value>
		public List<LookUp> AccountList { get; set; }

		/// <summary>
		/// Gets or sets the general ledger list.
		/// </summary>
		/// <value>
		/// The general ledger list.
		/// </value>
		public List<GeneralLedgerDetail> GeneralLedgerList { get; set; }

		/// <summary>
		/// Gets or sets the opening balance date.
		/// </summary>
		/// <value>
		/// The opening balance date.
		/// </value>
		public DateTime OpeningBalanceDate { get; set; }

		/// <summary>
		/// Gets or sets the opening balance.
		/// </summary>
		/// <value>
		/// The opening balance.
		/// </value>
		public decimal OpeningBalance { get; set; }

		/// <summary>
		/// Gets or sets the total debts.
		/// </summary>
		/// <value>
		/// The total debts.
		/// </value>
		public decimal TotalDebts { get; set; }

		/// <summary>
		/// Gets or sets the total credits.
		/// </summary>
		/// <value>
		/// The total credits.
		/// </value>
		public decimal TotalCredits { get; set; }

		/// <summary>
		/// Gets or sets the closing balance date.
		/// </summary>
		/// <value>
		/// The closing balance date.
		/// </value>
		public DateTime ClosingBalanceDate { get; set; }

		/// <summary>
		/// Gets or sets the closing balance.
		/// </summary>
		/// <value>
		/// The closing balance.
		/// </value>
		public decimal ClosingBalance { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Gets or sets the financial year start date.
		/// </summary>
		/// <value>
		/// The financial year start date.
		/// </value>
		public DateTime FinancialYearStartDate { get; set; }

		/// <summary>
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets the account identifier to.
		/// </summary>
		/// <value>
		/// The account identifier to.
		/// </value>
		public string AccountIdTo { get; set; }

		/// <summary>
		/// Gets or sets the account identifier from.
		/// </summary>
		/// <value>
		/// The account identifier from.
		/// </value>
		public string AccountIdFrom { get; set; }

		/// <summary>
		/// Gets or sets the account ids.
		/// </summary>
		/// <value>
		/// The account ids.
		/// </value>
		public string AccountIds { get; set; }

		/// <summary>
		/// Gets or sets the account identifier.
		/// </summary>
		/// <value>
		/// The account identifier.
		/// </value>
		public string AccountId { get; set; }
		/// <summary>
		/// Gets or sets the name of the account.
		/// </summary>
		/// <value>
		/// The name of the account.
		/// </value>
		public string AccountName { get; set; }

		/// <summary>
		/// Gets or sets the base coy curr.
		/// </summary>
		/// <value>
		/// The base coy curr.
		/// </value>
		public string BaseCoyCurr { get; set; }

		/// <summary>
		/// Gets or sets the CHH identifier.
		/// </summary>
		/// <value>
		/// The CHH identifier.
		/// </value>
		public string ChhId { get; set; }

		/// <summary>
		/// Gets or sets the minimum start date.
		/// </summary>
		/// <value>
		/// The minimum start date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime MinStartDate { get; set; }
		/// <summary>
		/// Gets or sets the maximum end date.
		/// </summary>
		/// <value>
		/// The maximum end date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime MaxEndDate { get; set; }
		/// <summary>
		/// Gets or sets the coy fin start date.
		/// </summary>
		/// <value>
		/// The coy fin start date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime? CoyFinStartDate { get; set; }
		/// <summary>
		/// Gets or sets the coy fin end date.
		/// </summary>
		/// <value>
		/// The coy fin end date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime? CoyFinEndDate { get; set; }

		/// <summary>
		/// Gets or sets the fin start date.
		/// </summary>
		/// <value>
		/// The fin start date.
		/// </value>
		public int finStartDate { get; set; }
		/// <summary>
		/// Gets or sets the fin start month.
		/// </summary>
		/// <value>
		/// The fin start month.
		/// </value>
		public int finStartMonth { get; set; }
		/// <summary>
		/// Gets or sets the fin end date.
		/// </summary>
		/// <value>
		/// The fin end date.
		/// </value>
		public int finEndDate { get; set; }
		/// <summary>
		/// Gets or sets the fin end month.
		/// </summary>
		/// <value>
		/// The fin end month.
		/// </value>
		public int finEndMonth { get; set; }
		/// <summary>
		/// Gets or sets the fin period.
		/// </summary>
		/// <value>
		/// The fin period.
		/// </value>
		public int finPeriod { get; set; }

		/// <summary>
		/// Gets or sets the type of the vessel management office.
		/// </summary>
		/// <value>
		/// The type of the vessel management office.
		/// </value>
		public string VesselManagementOfficeType { get; set; }

		/// <summary>
		/// Gets or sets active tab calss. eg. tab-1 ,tab-2.
		/// </summary>
		/// <value>
		/// The Active Mobile Tab Classe text field.
		/// </value>
		public string ActiveMobileTabClass { get; set; }
	}
}
