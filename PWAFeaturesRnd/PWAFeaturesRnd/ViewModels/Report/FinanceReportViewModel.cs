namespace PWAFeaturesRnd.ViewModels.Report
{
	public class FinanceReportViewModel
	{
		public string AccountCode { get; set; }

		public string AccountName { get; set; }

		public decimal Total { get; set; }

		public decimal Budget { get; set; }

		public decimal Variance { get; set; }

		public string VarianceColor { get { return Total > Budget ? "Red" : "Green"; } }
	}
}
