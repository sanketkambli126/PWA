using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
    public class VesselCostAnalysisTransactionResponseExportViewModel
    {
        [DisplayName("Voucher")]
        public string VoucherNo { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

        [DisplayName("Transaction Date")]
        public string TransactionDate { get; set; }

        [DisplayName("Currency")]
        public string Currency { get; set; }

        [DisplayName("Amount")]
        public string Amount { get; set; }

        [DisplayName("Amount Base")]
        public string AmountBase { get; set; }

        [DisplayName("Order")]
        public string Order { get; set; }
    }
}
