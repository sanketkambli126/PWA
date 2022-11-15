using FluentValidation.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report
{
	public class GeneralLedgerDetail
	{
		public int AccountCode { get; set; }

		public string AccountName { get; set; }

		public DateTime Date { get; set; }

		public string Voucher { get; set; }

		public string Type { get; set; }

		public string Ref { get; set; }

		public string Contra { get; set; }

		public string Voyage { get; set; }

		public string Text { get; set; }

		public decimal OriginalAmount { get; set; }

		public string OriginalCurrency { get; set; }

		public decimal FunctionalAmount { get; set; }

		public decimal FunctionalBalance { get; set; }
	}
}
