using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
    public class CostAnalysisTransactionResponseExportViewModel
    {
        [DisplayName("Text")]
        public string Text { get; set; }

        [DisplayName("Date")]
        public string Date { get; set; }

        [DisplayName("Order No.")]
        public string OrderNumber { get; set; }

        [DisplayName("Supplier")]
        public string Supplier { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

        [DisplayName("Reference")]
        public string Reference { get; set; }

        [DisplayName("Amount")]
        public string Amount { get; set; }
    }
}
