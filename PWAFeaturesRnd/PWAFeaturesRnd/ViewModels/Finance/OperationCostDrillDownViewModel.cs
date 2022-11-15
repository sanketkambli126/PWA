using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.ViewModels.Finance
{
    /// <summary>
    /// OperationCostDrillDownViewModel
    /// </summary>
    public class OperationCostDrillDownViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the type of the report definition.
        /// </summary>
        /// <value>
        /// The type of the report definition.
        /// </value>
        public ReportDefinitionType ReportDefinitionType { get; set; }

        /// <summary>
        /// Gets or sets the parent3 acc and desc.
        /// </summary>
        /// <value>
        /// The parent3 acc and desc.
        /// </value>
        public string Parent3AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the parent2 acc and desc.
        /// </summary>
        /// <value>
        /// The parent2 acc and desc.
        /// </value>
        public string Parent2AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the account level.
        /// </summary>
        /// <value>
        /// The account level.
        /// </value>
        public int AccountLevel { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the parent1 acc and desc.
        /// </summary>
        /// <value>
        /// The parent1 acc and desc.
        /// </value>
        public string Parent1AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the name of the previous stage.
        /// </summary>
        /// <value>
        /// The name of the previous stage.
        /// </value>
        public string PreviousStageName { get; set; }

        /// <summary>
        /// Gets or sets the previous stage URL.
        /// </summary>
        /// <value>
        /// The previous stage URL.
        /// </value>
        public string PreviousStageUrl { get; set; }

        /// <summary>
        /// Gets or sets the current stage title.
        /// </summary>
        /// <value>
        /// The current stage title.
        /// </value>
        public string CurrentStageTitle { get; set; }

        /// <summary>
        /// Gets or sets the transaction request URL.
        /// </summary>
        /// <value>
        /// The transaction request URL.
        /// </value>
        public string TransactionRequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the account code.
        /// </summary>
        /// <value>
        /// The account code.
        /// </value>
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the transaction to date.
        /// </summary>
        /// <value>
        /// The transaction to date.
        /// </value>
        public string TransactionToDate { get; set; }

        /// <summary>
        /// Gets or sets the actual.
        /// </summary>
        /// <value>
        /// The actual.
        /// </value>
        public double Actual { get; set; }
        /// <summary>
        /// Gets or sets the accurals.
        /// </summary>
        /// <value>
        /// The accurals.
        /// </value>
        public double Accurals { get; set; }
        /// <summary>
        /// Gets or sets the budget.
        /// </summary>
        /// <value>
        /// The budget.
        /// </value>
        public double Budget { get; set; }
        /// <summary>
        /// Gets or sets the variance.
        /// </summary>
        /// <value>
        /// The variance.
        /// </value>
        public double Variance { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets the month list.
        /// </summary>
        /// <value>
        /// The month list.
        /// </value>
        public List<LookUp> MonthList { get; set; }

        /// <summary>
        /// Gets or sets the year list.
        /// </summary>
        /// <value>
        /// The year list.
        /// </value>
        public List<LookUp> YearList { get; set; }

        /// <summary>
        /// Gets or sets the selected month.
        /// </summary>
        /// <value>
        /// The selected month.
        /// </value>
        public string SelectedMonth { get; set; }
        /// <summary>
        /// Gets or sets the selected year.
        /// </summary>
        /// <value>
        /// The selected year.
        /// </value>
        public string SelectedYear { get; set; }

        /// <summary>
        /// Gets or sets the header to date.
        /// </summary>
        /// <value>
        /// The header to date.
        /// </value>
        public string HeaderToDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [transaction level].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [transaction level]; otherwise, <c>false</c>.
        /// </value>
        public bool IsTransactionLevel { get; set; }

        /// <summary>
        /// Gets or sets the breadcrumbs.
        /// </summary>
        /// <value>
        /// The breadcrumbs.
        /// </value>
        public List<Tuple<string, string>> NavigationBreadcrumbs { get; set; }

        /// <summary>
		/// Gets or sets active tab calss. eg. tab-1 ,tab-2.
		/// </summary>
		/// <value>
		/// The Active Mobile Tab Classe text field.
		/// </value>
        public string ActiveMobileTabClass { get; set; }
    }
}
