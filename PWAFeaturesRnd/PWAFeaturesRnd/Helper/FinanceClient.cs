using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Finance;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.ExportToExcel;
using PWAFeaturesRnd.ViewModels.Finance;
using PWAFeaturesRnd.ViewModels.PurchaseOrder;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// Finance client
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class FinanceClient : BaseHttpClient
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public FinanceClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider,IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
        {
            client.BaseAddress = new Uri(AppSettings.FinanceWebApiUrl);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        /// <summary>
        /// Gets the decrypted vessel identifier.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        private string GetDecryptedVesselId(string encryptedVesselDetail)
        {
            if (!string.IsNullOrWhiteSpace(encryptedVesselDetail))
            {
                string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
                return decryptedString.Split(Constants.Separator)[0];
            }
            return null;
        }

        /// <summary>
        /// Gets the encrypted vessel.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="vesselName">Name of the vessel.</param>
        /// <param name="coyId">The coy identifier.</param>
        /// <returns></returns>
        private string GetEncryptedVessel(string vesselId, string vesselName, string coyId)
        {
            string encryptedVessel = _provider.CreateProtector("Vessel").Protect(vesselId + Constants.Separator + vesselName + " - " + coyId + Constants.Separator + coyId);
            return encryptedVessel;
        }

        /// <summary>
        /// Posts the get vessel account budget detail.
        /// </summary>
        /// <param name="AccountCompanyId">The account company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="accountCode">The account code.</param>
        /// <returns></returns>
        public async Task<CompanyVesselBudgetViewModel> PostGetVesselAccountBudgetDetail(string AccountCompanyId, string orderNumber, string accountCode)
        {
            CompanyVesselBudgetViewModel vesselBudgetViewModel = new CompanyVesselBudgetViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/BudgetHeader"));
            CompanyVesselBudget response = new CompanyVesselBudget();

            var request = new Dictionary<string, object>()
            {
                { "accountingCompanyId", AccountCompanyId },
                { "orderNumber", orderNumber },
                { "accountCode", accountCode}
            };

            response = await PostAsync<CompanyVesselBudget>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                vesselBudgetViewModel = new CompanyVesselBudgetViewModel()
                {
                    AccountId = response.AccountId,

                    BudgetActualSpend = string.Format(Constants.TwoDecimal_NumberFormat, response.BudgetActualSpend),
                    BudgetAmountAllocated = string.Format(Constants.TwoDecimal_NumberFormat, response.BudgetAmountAllocated),
                    BudgetCurrencyId = response.BudgetCurrencyId,
                    BudgetDetailId = response.BudgetDetailId,
                    BudgetInvoiceAccrual = string.Format(Constants.TwoDecimal_NumberFormat, response.BudgetInvoiceAccrual),
                    BudgetPurchaseOrderAccrual = string.Format(Constants.TwoDecimal_NumberFormat, response.BudgetPurchaseOrderAccrual),
                    BudgetStartDate = response.BudgetStartDate != DateTime.MinValue ? response.BudgetStartDate.ToString("dd MMM yyyy") : "",
                    BudgetStartEnd = response.BudgetStartEnd != DateTime.MinValue ? response.BudgetStartEnd.ToString("dd MMM yyyy") : "",
                    TotalAccruals = string.Format(Constants.TwoDecimal_NumberFormat, response.TotalAccruals),
                    VesselId = response.VesselId,
                    TotalBudget = string.Format(Constants.TwoDecimal_NumberFormat, (response.TotalAccruals + response.BudgetActualSpend)),
                    VarianceAmount = string.Format(Constants.TwoDecimal_NumberFormat, (response.BudgetAmountAllocated - response.BudgetActualSpend - response.TotalAccruals)),
                    EndDate = response.BudgetStartEnd,
                    StartDate = response.BudgetStartDate,
                    VarianceAmountDecimal = response.BudgetAmountAllocated - response.BudgetActualSpend - response.TotalAccruals,
                    TotalAccrualsDecimal = response.TotalAccruals,
                    BudgetActualSpendDecimal = response.BudgetActualSpend,
                    BudgetAmountAllocatedDecimal = response.BudgetAmountAllocated

                };
            }

            return vesselBudgetViewModel;
        }

        /// <summary>
        /// Posts the get rc drill down details by level.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperatinCostViewModel> PostGetRCDrillDownDetailsByLevel(OperationCostDrillDownViewModel request)
        {
            OperatinCostViewModel operatingCostVM = new OperatinCostViewModel();
            operatingCostVM.OperatingCostList = new List<OperatingCostBarChartResponseViewModel>();

            OperatingCostBarChartRequest requestVM = new OperatingCostBarChartRequest();
            requestVM.ToDate = request.ToDate;
            requestVM.AccountLevel = request.AccountLevel;
            requestVM.ReportDefinitionType = request.ReportDefinitionType;
            requestVM.CoyId = request.CoyId;
            requestVM.AccountId = request.AccountId;
            requestVM.Parent2AccAndDesc = request.Parent2AccAndDesc;
            requestVM.Parent3AccAndDesc = request.Parent3AccAndDesc;

            List<OperatingCostBarChartResponse> response = new List<OperatingCostBarChartResponse>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/RunningCostDrillDown"));

            if (!string.IsNullOrWhiteSpace(requestVM.CoyId) && requestVM.ToDate.HasValue && requestVM.ToDate != DateTime.MinValue)
            {
                response = await PostAsync<List<OperatingCostBarChartResponse>>(requestUrl, CreateHttpContent(requestVM));
            }

            if (response != null && response.Any())
            {
                double localBudgetValue = 0;
                double localTotalValue = 0;
                double localVarianceValue = 0;
                double localActualValue = 0;
                double localAccuralsValue = 0;
                string operationCostUrl = string.Empty;
                string transactionRequestUrl = string.Empty;
                string previousOfTransactionRequestUrl = string.Empty;

                foreach (OperatingCostBarChartResponse item in response)
                {
                    OperatingCostBarChartRequest operationCostRequest = new OperatingCostBarChartRequest();

                    if (request.AccountLevel == -1 && item.Level == 3)
                    {
                        operationCostRequest.ToDate = request.ToDate;
                        operationCostRequest.AccountLevel = 3;
                        operationCostRequest.ReportDefinitionType = request.ReportDefinitionType;
                        operationCostRequest.CoyId = request.CoyId;
                        operationCostRequest.AccountId = item.Parent3AccAndDesc;
                        operationCostRequest.VesselId = request.VesselId;
                        operationCostUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(operationCostRequest));
                    }
                    else if (request.AccountLevel == 3 && item.Level == 2)
                    {
                        operationCostRequest.ToDate = request.ToDate;
                        operationCostRequest.AccountLevel = 2;
                        operationCostRequest.ReportDefinitionType = request.ReportDefinitionType;
                        operationCostRequest.CoyId = request.CoyId;
                        operationCostRequest.AccountId = item.Parent2AccAndDesc;
                        operationCostRequest.Parent3AccAndDesc = item.Parent3AccAndDesc;
                        operationCostRequest.VesselId = request.VesselId;
                        operationCostUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(operationCostRequest));
                    }
                    else if (request.AccountLevel == 2 && item.Level == 1)
                    {
                        operationCostRequest.ToDate = request.ToDate;
                        operationCostRequest.AccountLevel = 1;
                        operationCostRequest.ReportDefinitionType = request.ReportDefinitionType;
                        operationCostRequest.CoyId = request.CoyId;
                        operationCostRequest.AccountId = item.Parent1AccAndDesc;
                        operationCostRequest.Parent3AccAndDesc = item.Parent3AccAndDesc;
                        operationCostRequest.Parent2AccAndDesc = item.Parent2AccAndDesc;
                        operationCostRequest.VesselId = request.VesselId;
                        operationCostUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(operationCostRequest));
                    }
                    else if (item.Level == 0)
                    {
                        GLTransactionFilter filters = new GLTransactionFilter();
                        filters.AccountingCompanyId = request.CoyId;
                        filters.AccountCode = item.AccountId;
                        filters.FromDate = new DateTime(request.ToDate.Value.Year, 1, 1);
                        filters.ToDate = request.ToDate.Value;
                        filters.FinancialYearStartDate = new DateTime(DateTime.Today.Year, 4, 1);//need to ask
                        filters.TransactionState = TransactionState.All;
                        filters.Actual = item.Actual;
                        filters.Accurals = item.Accrual;
                        filters.Budget = item.Budget;
                        filters.Variance = item.Variance;
                        filters.Total = item.Total;
                        filters.VesselId = request.VesselId;
                        filters.VesselName = request.VesselName;
                        transactionRequestUrl = _provider.CreateProtector("TransactionCostRequestUrl").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(filters));
                        requestVM.LabelName = item.Label;
                        requestVM.VesselId = request.VesselId;
                        previousOfTransactionRequestUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(requestVM));
                    }

                    OperatingCostBarChartResponseViewModel operationCost = new OperatingCostBarChartResponseViewModel();
                    operationCost.OperatingCostUrl = operationCostUrl;

                    operationCost.TransactionRequestUrl = transactionRequestUrl;
                    operationCost.PreviousOfTransactionRequestUrl = previousOfTransactionRequestUrl;

                    operationCost.operatingCostVesselId = request.VesselId;
                    operationCost.AccountId = item.AccountId;
                    operationCost.AccountDescription = item.AccountId + (string.IsNullOrWhiteSpace(item.Label) ? "" : (" -" + item.Label));
                    operationCost.Accurals = item.Accrual;
                    operationCost.Actual = item.Actual;
                    operationCost.Total = item.Total;
                    operationCost.Budget = item.Budget;
                    operationCost.Variance = item.Variance;
                    operationCost.DrillDownLevel = item.Level;
                    operationCost.Label = item.Label;
                    operationCost.VariancePercent = item.VariancePercent.ToString();
                    operationCost.IsVariancePercentNegative = item.VariancePercent < 0;
                    operatingCostVM.OperatingCostList.Add(operationCost);

                    localBudgetValue += item.Budget;
                    localTotalValue += item.Total;
                    localVarianceValue += item.Variance;
                    localActualValue += item.Actual;
                    localAccuralsValue += item.Accrual;
                }
                operatingCostVM.Budget = localBudgetValue;
                operatingCostVM.Total = localTotalValue;
                operatingCostVM.Variance = localVarianceValue;
                operatingCostVM.Actual = localActualValue;
                operatingCostVM.Accurals = localAccuralsValue;
            }

            return operatingCostVM;
        }

        /// <summary>
        /// Posts the get running cost recal report header details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<RunningCostRecalReportHeaderDetailsViewModel> PostGetRunningCostRecalReportHeaderDetails(OperationCostDrillDownViewModel request)
        {
            RunningCostRecalReportHeaderDetailsViewModel headerDetails = new RunningCostRecalReportHeaderDetailsViewModel();
            RunningCostRecalReportHeaderDetails response = new RunningCostRecalReportHeaderDetails();

            var value = new Dictionary<string, object>()
            {
                { "coyId", request.CoyId },
                { "reportDateTo", request.ToDate },
                { "reportType",  EnumsHelper.GetKeyValue(request.ReportDefinitionType) }
            };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/RunningCostHeader"));
            response = await PostAsync<RunningCostRecalReportHeaderDetails>(requestUrl, CreateHttpContent(value));

            if (response != null)
            {
                headerDetails.Currency = response.Currency ?? "";
                headerDetails.NumberOfDays = response.NumberOfDays;
                headerDetails.RCFromDateTime = response.RCFromDateTime.ToString("dd MMM yyyy"); ;
                headerDetails.RCToDateTime = response.RCToDateTime.ToString("dd MMM yyyy"); ;
            }

            return headerDetails;
        }

        /// <summary>
        /// Posts the get transactions by account code paged.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<CostAnalysisTransactionResponseViewModel>>> PostGetTransactionsByAccountCodePaged(DataTablePageRequest<string> pageRequest, GLTransactionFilter filters)
        {
            DataTablePageResponse<List<CostAnalysisTransactionResponseViewModel>> result = new DataTablePageResponse<List<CostAnalysisTransactionResponseViewModel>>();
            result.Data = new List<CostAnalysisTransactionResponseViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "filters", filters }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/ConstAnalysisTransaction"));
            PagedResponse<List<CostAnalysisTransactionResponse>> response = null;
            response = await PostAsync<PagedResponse<List<CostAnalysisTransactionResponse>>>(requestUrl, CreateHttpContent(value));
            PurchaseOrderRequestViewModel purchaseOrderUrl = null;

            if (response.Result != null)
            {
                string decreptedString = _provider.CreateProtector("Vessel").Unprotect(filters.VesselId);
                int localCounter = 1;
                foreach (CostAnalysisTransactionResponse item in response.Result)
                {
                    purchaseOrderUrl = new PurchaseOrderRequestViewModel();
                    purchaseOrderUrl.VesselId = decreptedString.Split(Constants.Separator)[0];
                    purchaseOrderUrl.VesselName = decreptedString.Split(Constants.Separator)[1];
                    purchaseOrderUrl.ToDate = DateTime.Now.Date;
                    purchaseOrderUrl.FromDate = DateTime.Now.Date;
                    purchaseOrderUrl.AccountCompanyId = filters.AccountingCompanyId;
                    purchaseOrderUrl.OrderNumber = item.OrderNo;
                    purchaseOrderUrl.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.InProcess);

                    CostAnalysisTransactionResponseViewModel transactionResponse = new CostAnalysisTransactionResponseViewModel();
                    transactionResponse.Amount = item.Amount;
                    transactionResponse.InvoiceDocumentId = !string.IsNullOrWhiteSpace(item.InvoiceDocumentId) ? item.InvoiceDocumentId : "";
                    transactionResponse.JournalType = !string.IsNullOrWhiteSpace(item.JournalType) ? item.JournalType : "";
                    transactionResponse.JournalDesc = !string.IsNullOrWhiteSpace(item.JournalDesc) ? item.JournalDesc : "";
                    transactionResponse.OrderNo = !string.IsNullOrWhiteSpace(item.OrderNo) ? item.OrderNo : "";
                    transactionResponse.Reference = !string.IsNullOrWhiteSpace(item.Reference) ? item.Reference : "";
                    transactionResponse.Supplier = !string.IsNullOrWhiteSpace(item.Supplier) ? item.Supplier : "";
                    transactionResponse.Text = !string.IsNullOrWhiteSpace(item.Text) ? item.Text : "";
                    transactionResponse.TransactionDate = item.TransactionDate.ToString("dd MMM yyyy");
                    transactionResponse.TransactionId = item.TransactionId;
                    transactionResponse.VoucherNo = !string.IsNullOrWhiteSpace(item.VoucherNo) ? item.VoucherNo : "";
                    transactionResponse.PurchaseOrderUrl = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderUrl));
                    transactionResponse.VesselId = filters.VesselId;
                    transactionResponse.DocumentCount = !string.IsNullOrWhiteSpace(item.InvoiceDocumentId) ? 1 : item.DocumentCount;
                    transactionResponse.Counter = localCounter++;
                    transactionResponse.AccountingCompanyId = filters.AccountingCompanyId;
                    result.Data.Add(transactionResponse);
                }
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;
        }

        /// <summary>
        /// Posts the get transactions by account code paged export to excel.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<List<CostAnalysisTransactionResponse>> PostGetTransactionsByAccountCodePagedExportToExcel(GLTransactionFilter filters)
        {
            var value = new Dictionary<string, object>()
            {
                { "filters", filters }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/ConstAnalysisTransaction"));
            List<CostAnalysisTransactionResponse> response = await PostAsyncAutoPaged<CostAnalysisTransactionResponse>(requestUrl, value, 50);

            return response;
        }

        /// <summary>
        /// Posts the get general ledger transactions query.
        /// </summary>
        /// <param name="generalLedgerRequest">The general ledger request.</param>
        /// <returns></returns>
        public async Task<List<GeneralLedgerTransactionResponseViewModel>> PostGetGeneralLedgerTransactionsQuery(GeneralLedgerTransactionRequest generalLedgerRequest)
        {
            List<GeneralLedgerTransactionResponseViewModel> generalLedgerList = new List<GeneralLedgerTransactionResponseViewModel>();
            List<GeneralLedgerTransactionResponse> response = null;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/GeneralLedgerTransactions"));
            response = await PostAsync<List<GeneralLedgerTransactionResponse>>(requestUrl, CreateHttpContent(generalLedgerRequest));
            if (response != null && response.Any())
            {
                foreach (GeneralLedgerTransactionResponse item in response)
                {
                    GeneralLedgerTransactionResponseViewModel generalLedgerVM = new GeneralLedgerTransactionResponseViewModel();
                    generalLedgerVM.AccountCode = item.AccountCode;
                    generalLedgerVM.AccountDesc = item.AccountDesc;
                    generalLedgerVM.AccountName = item.AccountCode + " - " + item.AccountDesc + " - " + item.TransactionOriginalCurrency + " A/C";
                    generalLedgerVM.TransactionDate = item.TransactionDate;
                    generalLedgerVM.VoucherNo = !string.IsNullOrWhiteSpace(item.VoucherNo) ? item.VoucherNo : "";
                    generalLedgerVM.JournalTypeDesc = item.JournalTypeDesc;
                    generalLedgerVM.Ref = !string.IsNullOrWhiteSpace(item.Ref) ? item.Ref : "";
                    generalLedgerVM.Contra = !string.IsNullOrWhiteSpace(item.Contra) ? item.Contra : "";
                    generalLedgerVM.VoyCode = !string.IsNullOrWhiteSpace(item.VoyCode) ? item.VoyCode : "";
                    generalLedgerVM.TransactionText = !string.IsNullOrWhiteSpace(item.TransactionText) ? item.TransactionText : "";
                    generalLedgerVM.OriginalAmount = item.OriginalAmount;
                    generalLedgerVM.TransactionOriginalCurrency = item.TransactionOriginalCurrency;
                    generalLedgerVM.FunctionalAmount = 0;
                    generalLedgerVM.FunctionalBalance = 0;

                    generalLedgerVM.BaseAmount = item.BaseAmount;
                    generalLedgerVM.IsTransactionDebit = item.IsTransactionDebit;
                    generalLedgerVM.JournalTypeSeq = item.JournalTypeSeq;
                    generalLedgerVM.OpeningBalFlag = item.OpeningBalFlag;
                    generalLedgerVM.RecNo = item.RecNo;
                    generalLedgerVM.SortOrder = item.SortOrder;
                    generalLedgerVM.TransactionDateEntered = item.TransactionDateEntered;
                    generalLedgerList.Add(generalLedgerVM);
                }
            }

            return generalLedgerList;
        }

        /// <summary>
        /// Gets the account code list paged.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<ChartAccountDetailResponseViewModel>>> GetAccountCodeListPaged(DataTablePageRequest<string> pageRequest, ChartAccountDetailRequest request)
        {
            DataTablePageResponse<List<ChartAccountDetailResponseViewModel>> result = new DataTablePageResponse<List<ChartAccountDetailResponseViewModel>>();
            result.Data = new List<ChartAccountDetailResponseViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "request", request }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/AccountCodeList"));
            PagedResponse<List<ChartAccountDetailResponse>> response = null;
            response = await PostAsync<PagedResponse<List<ChartAccountDetailResponse>>>(requestUrl, CreateHttpContent(value));

            if (response.Result != null)
            {
                foreach (ChartAccountDetailResponse item in response.Result)
                {
                    ChartAccountDetailResponseViewModel accountDetailResponseViewModel = new ChartAccountDetailResponseViewModel();
                    accountDetailResponseViewModel.AccountId = item.AccountId;
                    accountDetailResponseViewModel.ChartCurrencyId = item.ChartCurrencyId ?? "";
                    accountDetailResponseViewModel.ChartCurrencyType = item.ChartCurrencyType;
                    accountDetailResponseViewModel.ChartDescription = item.ChartDescription;
                    accountDetailResponseViewModel.ChartDetailId = item.ChartDetailId;
                    accountDetailResponseViewModel.ChartDetailParentId = item.ChartDetailParentId;
                    accountDetailResponseViewModel.ChartHeaderId = item.ChartHeaderId;
                    accountDetailResponseViewModel.ChartPosting = item.ChartPosting;
                    accountDetailResponseViewModel.ChartType = item.ChartType;
                    accountDetailResponseViewModel.IsBankAccount = item.IsBankAccount;

                    result.Data.Add(accountDetailResponseViewModel);
                }
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;
        }

        /// <summary>
        /// Gets the operation cost details for vessel.
        /// </summary>
        /// <param name="opexDrillDownRequest">The opex drill down request.</param>
        /// <returns></returns>
        public async Task<OperatingCostBarChartResponseViewModel> GetOperationCostDetailsForVessel(OperationCostDrillDownViewModel opexDrillDownRequest)
        {
            OperatingCostBarChartResponseViewModel opexDetails = new OperatingCostBarChartResponseViewModel();
            OperatingCostBarChartRequest request = new OperatingCostBarChartRequest();
            string decreptedVessel = _provider.CreateProtector("Vessel").Unprotect(opexDrillDownRequest.VesselId);
            var CoyId = decreptedVessel.Split(Constants.Separator)[2];
            request.AccountId = null;
            request.AccountLevel = -1;
            request.CoyId = CoyId;
            request.ReportDefinitionType = ReportDefinitionType.S;
            request.MonthsDuration = Constants.VarianceMonthsDuration;
            request.VariancePercentageFirstXMonthLimit = Constants.VariancePercentageFirstXMonthLimit;
            request.VariancecPercentageSecondXMonthLimit = Constants.VariancecPercentageSecondXMonthLimit;

            var CurrentDate = DateTime.Now;
            var previousDate = CurrentDate.AddDays(-1);
            var nextMonth = previousDate.AddMonths(1);
            var NextMonthDate = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            var requiredDate = NextMonthDate.AddDays(-1);

            request.ToDate = requiredDate;
            OperatingCostBarChartResponse response = null;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/RunningCostSummary"));
            response = await PostAsync<OperatingCostBarChartResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                opexDetails.AccuralsStr = ((decimal)response.Accrual).ToKMB();
                opexDetails.ActualStr = ((decimal)response.Actual).ToKMB();
                opexDetails.BudgetStr = ((decimal)response.Budget).ToKMB();
                opexDetails.Total = response.Total;
                opexDetails.Variance = response.Variance;
                opexDetails.DashboardCostCurrency = response.Currency;

                opexDetails.VariancePercent = response.VariancePercent + "%";
                opexDetails.VarianceKPIPriority = response.VariancePriority;
                opexDetails.BudgetKPIPriority = response.BudgetYTDPriority;
                opexDetails.ActualKPIPriority = response.ActualPriority;
                opexDetails.AccrualKPIPriority = response.AccrualPriority;
            }
            opexDetails.DashboardDrillDownPeriod = requiredDate.ToString("MMM yyyy");

            string operationCostURL = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));

            opexDetails.OpexDashboardUrl = operationCostURL;
            return opexDetails;
        }

        /// <summary>
        /// Posts the get account codes for vessel.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<AccountLookupWrapperViewModel>> PostGetAccountCodesForVessel(SearchAccountCodeRequest request)
        {
            List<AccountLookupWrapperViewModel> accountCodeList = new List<AccountLookupWrapperViewModel>();
            List<Lookup> response = null;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/AccountCodesForVessel"));
            response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(request));
            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    AccountLookupWrapperViewModel localAccountCode = new AccountLookupWrapperViewModel();
                    localAccountCode.Identifier = item.Identifier;
                    localAccountCode.Description = item.Description;
                    accountCodeList.Add(localAccountCode);
                }
            }
            return accountCodeList;
        }

        /// <summary>
        /// Posts the get aux by acc code and coy identifier.
        /// </summary>
        /// <param name="accountingId">The accounting identifier.</param>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <returns></returns>
        public async Task<ApplicableAccountAuxiliariesViewModel> PostGetAuxByAccCodeAndCoyId(string accountingId, string accountingCompanyId)
        {
            ApplicableAccountAuxiliariesViewModel ApplicableAuxFlags = new ApplicableAccountAuxiliariesViewModel();
            ApplicableAccountAuxiliaries response = new ApplicableAccountAuxiliaries();

            var value = new Dictionary<string, object>()
            {
                { "accountingId", accountingId },
                { "accountingCompanyId", accountingCompanyId }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/AuxByAccCodeAndCoyId"));
            response = await PostAsync<ApplicableAccountAuxiliaries>(requestUrl, CreateHttpContent(value));

            if (response != null)
            {
                ApplicableAuxFlags.AuxClaims = response.AuxClaims.GetValueOrDefault();
                ApplicableAuxFlags.AuxGeneral1 = response.AuxGeneral1.GetValueOrDefault();
                ApplicableAuxFlags.AuxGeneral3 = response.AuxGeneral3.GetValueOrDefault();
                ApplicableAuxFlags.AuxNationality = response.AuxNationality.GetValueOrDefault();
                ApplicableAuxFlags.AuxRank = response.AuxRank.GetValueOrDefault();
                ApplicableAuxFlags.AuxSeasonal = response.AuxSeasonal.GetValueOrDefault();
                ApplicableAuxFlags.AuxVessel = response.AuxVessel.GetValueOrDefault();

                ApplicableAuxFlags.IsAuxillaryApplicable = (response != null
              && ((response.AuxClaims ?? false) || (response.AuxGeneral1 ?? false) || (response.AuxGeneral3 ?? false)
              || (response.AuxNationality ?? false) || (response.AuxRank ?? false) || (response.AuxSeasonal ?? false)
              || (response.AuxVessel ?? false)));

            }

            return ApplicableAuxFlags;
        }

        /// <summary>
        /// Posts the get general aux list by type paged.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <param name="auxType">Type of the aux.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<AuxiliaryResponseViewModel>>> PostGetGeneralAuxListByTypePaged(DataTablePageRequest<string> pageRequest, AuxiliarySearchRequestForLookUp searchRequest, string auxType)
        {
            DataTablePageResponse<List<AuxiliaryResponseViewModel>> result = new DataTablePageResponse<List<AuxiliaryResponseViewModel>>();
            result.Data = new List<AuxiliaryResponseViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            PagedResponse<List<AuxiliaryResponse>> response = null;
            AuxCodeType auxCodeType = new AuxCodeType();
            if (auxType == EnumsHelper.GetDescription(AuxCodeType.Seasonal))
            {
                auxCodeType = AuxCodeType.Seasonal;
            }
            else if (auxType == EnumsHelper.GetDescription(AuxCodeType.General1))
            {
                auxCodeType = AuxCodeType.General1;
            }
            else if (auxType == EnumsHelper.GetDescription(AuxCodeType.General3))
            {
                auxCodeType = AuxCodeType.General3;
            }

            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "auxCodeType",auxCodeType},
                { "searchRequest", searchRequest },
                { "isVesselAux", true }

            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/GeneralAuxList"));
            response = await PostAsync<PagedResponse<List<AuxiliaryResponse>>>(requestUrl, CreateHttpContent(value));

            if (response != null && response.Result != null && response.Result.Any())
            {
                foreach (var item in response.Result)
                {
                    AuxiliaryResponseViewModel generalAux = new AuxiliaryResponseViewModel();
                    generalAux.CoyID = item.CoyID;
                    generalAux.Description = item.Description ?? "";
                    generalAux.Id = item.Identifier ?? "";
                    generalAux.Identifier = item.Identifier ?? "";
                    generalAux.ShortCode = item.ShortCode;
                    generalAux.Type = item.Type;
                    generalAux.Text = item.Description ?? "";
                    if (!string.IsNullOrEmpty(item.ShortCode))
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            generalAux.ShortCodeDesc = item.ShortCode + " - " + item.Description;
                        }
                        else
                        {
                            generalAux.ShortCodeDesc = item.ShortCode;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            generalAux.ShortCodeDesc = item.Identifier + " - " + item.Description;
                        }
                        else
                        {
                            generalAux.ShortCodeDesc = item.Identifier;
                        }
                    }
                    result.Data.Add(generalAux);
                }
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;
            return result;
        }

        /// <summary>
        /// Posts the get nationality lookup.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<List<AuxiliaryResponseViewModel>> PostGetNationalityLookup(AuxiliarySearchRequestForLookUp searchRequest)
        {
            List<Lookup> response = new List<Lookup>();
            List<AuxiliaryResponseViewModel> result = new List<AuxiliaryResponseViewModel>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/NationalityLookup"));
            response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(searchRequest));

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    AuxiliaryResponseViewModel NationialityVM = new AuxiliaryResponseViewModel();
                    NationialityVM.Description = item.Description;
                    NationialityVM.Identifier = item.Identifier;
                    NationialityVM.Id = item.Identifier;
                    NationialityVM.Text = item.Description;
                    result.Add(NationialityVM);
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get inv document by coy identifier and voucher no.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="voucherNo">The voucher no.</param>
        /// <returns></returns>
        public async Task<List<InvoiceDocumentResponseViewModel>> PostGetInvDocByCoyIdAndVoucherNo(string accountingCompanyId, string voucherNo)
        {
            List<InvoiceDocumentResponseViewModel> result = new List<InvoiceDocumentResponseViewModel>();
            List<InvoiceDocumentResponse> response = new List<InvoiceDocumentResponse>();
            var value = new Dictionary<string, object>()
            {
                { "accountingCompanyId", accountingCompanyId },
                { "voucherNo", voucherNo}
            };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/InvoiceDocuments"));
            response = await PostAsync<List<InvoiceDocumentResponse>>(requestUrl, CreateHttpContent(value));

            if (response != null && response.Any())
            {
                foreach (InvoiceDocumentResponse item in response)
                {
                    InvoiceDocumentResponseViewModel document = new InvoiceDocumentResponseViewModel();
                    document.AccountingCompanyId = item.AccountingCompanyId;
                    document.InvoiceDocumentId = item.InvoiceDocumentId;
                    document.UpdatedOn = item.UpdatedOn;
                    document.UpdatedBy = item.UpdatedBy;
                    document.VoucherNumber = item.VoucherNumber;
                    result.Add(document);
                }
            }
            return result;
        }

        /// <summary>
        /// Generals the ledger transaction.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<List<CostAnalysisTransactionResponseViewModel>> GeneralLedgerTransactionList(GLTransactionFilterViewModel filterViewModel)
        {
            List<CostAnalysisTransactionResponseViewModel> result = new List<CostAnalysisTransactionResponseViewModel>();

            GLTransactionFilter filters = new GLTransactionFilter();
            filters.ToDate = filterViewModel.ToDate;
            filters.FromDate = filterViewModel.FromDate;
            filters.AccountCode = filterViewModel.AccountCode;
            filters.AccountingCompanyId = filterViewModel.AccountingCompanyId;
            filters.TransactionState = TransactionState.All;
            Dictionary<string, object> value = new Dictionary<string, object>()
            {
                { "filters", filters }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/ConstAnalysisTransaction"));
            List<CostAnalysisTransactionResponse> response = null;
            response = await PostAsyncAutoPaged<CostAnalysisTransactionResponse>(requestUrl, value, 100);
            decimal runningBalanceBase = 0;

            if (response != null && response.Any())
            {
                RunningBalanceRequest input = new RunningBalanceRequest()
                {
                    AccId = filters.AccountCode,
                    ChartId = filterViewModel.AccountingCompanyChhId,
                    CoyId = filters.AccountingCompanyId,
                    FinancialStartDate = filterViewModel.FinancialYearStartDate,
                    FromDate = filters.FromDate,
                    ToDate = filters.ToDate,
                    GLLineDateTran = (response != null && response.Any()) ? response.First().TransactionDate : (DateTime?)null
                };

                RunningBalanceResponseViewModel runningBalance = await GetRunningBalanceUptoTransLine(input);

                if (runningBalance != null)
                {
                    runningBalanceBase = runningBalance.RunningBalanceBase;
                }

                int counter = 0;
                decimal? prevRunningBalance = 0;
                foreach (CostAnalysisTransactionResponse item in response)
                {
                    CostAnalysisTransactionResponseViewModel transactionResponse = new CostAnalysisTransactionResponseViewModel();
                    if (counter == 0)
                    {
                        transactionResponse.RunningBalance = runningBalanceBase + (item.Amount);
                        prevRunningBalance = transactionResponse.RunningBalance;
                    }
                    else
                    {
                        transactionResponse.RunningBalance = (prevRunningBalance.HasValue ? prevRunningBalance.Value : 0) + item.Amount;
                        prevRunningBalance = transactionResponse.RunningBalance;
                    }
                    transactionResponse.TranxDate = item.TransactionDate;
                    transactionResponse.VoucherNo = !string.IsNullOrWhiteSpace(item.VoucherNo) ? item.VoucherNo : "";
                    transactionResponse.JournalType = !string.IsNullOrWhiteSpace(item.JournalType) ? item.JournalType : "";
                    transactionResponse.JournalDesc = !string.IsNullOrWhiteSpace(item.JournalDesc) ? item.JournalDesc : "";
                    transactionResponse.Reference = !string.IsNullOrWhiteSpace(item.Reference) ? item.Reference : "";
                    transactionResponse.Text = !string.IsNullOrWhiteSpace(item.Text) ? item.Text : "";
                    transactionResponse.Currency = item.Currency;
                    transactionResponse.AmountOriginal = item.AmountOriginal;
                    transactionResponse.Amount = item.Amount;
                    transactionResponse.IsOpen = item.IsOpen ? "Yes" : "No";
                    result.Add(transactionResponse);
                    counter++;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the running balance upto trans line.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<RunningBalanceResponseViewModel> GetRunningBalanceUptoTransLine(RunningBalanceRequest request)
        {
            RunningBalanceResponseViewModel result = new RunningBalanceResponseViewModel();
            RunningBalanceResponse response = new RunningBalanceResponse();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/RunningBalanceUptoTransLine"));
            response = await PostAsync<RunningBalanceResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                result.CurrencyType = response.CurrencyType;
                result.DistinctCurrencies = response.DistinctCurrencies;
                result.RunningBalanceBase = response.RunningBalanceBase;
                result.RunningBalanceOrig = response.RunningBalanceOrig;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountingCompanyId"></param>
        /// <returns></returns>
        public async Task<AccountCompanyDetailsViewModel> GetSelectedAccountingCompanyDetails(string accountingCompanyId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/AccountCompanyHeader"));
            var response = await PostAsync<AccountCompanyDetails>(requestUrl, CreateHttpContent(accountingCompanyId));
            AccountCompanyDetailsViewModel accountCompanyDetailsViewModel = new AccountCompanyDetailsViewModel();
            if (response != null)
            {
                List<AccountingFinancialYearDetailsViewModel> accountingFinancialYearList = new List<AccountingFinancialYearDetailsViewModel>();
                if (response.AccountingFinancialYears != null && response.AccountingFinancialYears.Any())
                {
                    foreach (var item in response.AccountingFinancialYears)
                    {
                        AccountingFinancialYearDetailsViewModel accountingFinancialYear = new AccountingFinancialYearDetailsViewModel();
                        accountingFinancialYear.Period = item.Period;
                        accountingFinancialYear.StartDate = item.StartDate;
                        accountingFinancialYear.EndDate = item.EndDate;
                        accountingFinancialYear.DateRange = item.StartDate.ToString(Constants.MonthYearFormat) + " - " + item.EndDate.ToString(Constants.MonthYearFormat);
                        accountingFinancialYearList.Add(accountingFinancialYear);
                    }
                }

                accountCompanyDetailsViewModel.IsPOReceivedOnly = (response.CoyPoreceivedOnly.HasValue && response.CoyPoreceivedOnly == 1) ? true : false;
                accountCompanyDetailsViewModel.AccountingFinancialYears = accountingFinancialYearList;
                accountCompanyDetailsViewModel.BaseCoyCurr = response.CoyCurr;
                accountCompanyDetailsViewModel.CoyCoyType = response.CoyCoyType;
                accountCompanyDetailsViewModel.CoyFinYearStart = response.CoyFinYearStart;
                accountCompanyDetailsViewModel.CoyFinYearEnd = response.CoyFinYearEnd;
                accountCompanyDetailsViewModel.ChhId = response.ChhId;
                accountCompanyDetailsViewModel.VesselManagementOfficeType = response.VesselManagementDetail[0].VesselManagementOfficeType.Description;
            }

            return accountCompanyDetailsViewModel;
        }

        public async Task<List<VesselCostAnalysisTransactionResponseViewModel>> GetVesselCostAnalysisTransactions(GLTransactionFilter filters)
        {
            List<VesselCostAnalysisTransactionResponseViewModel> result = new List<VesselCostAnalysisTransactionResponseViewModel>();
            List<VesselCostAnalysisTransactionResponse> response = new List<VesselCostAnalysisTransactionResponse>();

            var value = new Dictionary<string, object>()
            {
                { "coyId", filters.AccountingCompanyId },
                { "accId", filters.AccountCode},
                { "dateFrom", filters.FromDate},
                { "dateTo", filters.ToDate}
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/VesselCostAnalysisTransactions"));
            response = await PostAsync<List<VesselCostAnalysisTransactionResponse>>(requestUrl, CreateHttpContent(value));

            if (response != null && response.Any())
            {
                int localCounter = 1;
                foreach (VesselCostAnalysisTransactionResponse item in response)
                {
                    VesselCostAnalysisTransactionResponseViewModel objVesselCost = new VesselCostAnalysisTransactionResponseViewModel();
                    objVesselCost.DocumentName = item.DocumentName;
                    objVesselCost.AmountBase = item.AmountBase;
                    objVesselCost.Company = item.Company;
                    objVesselCost.PONumber = item.PONumber;
                    objVesselCost.CloudStatus = item.CloudStatus;
                    objVesselCost.FileType = item.FileType;
                    objVesselCost.InvoiceDocumentId = item.DocumentId;
                    objVesselCost.AmountUsd = item.AmountUsd;
                    objVesselCost.Amount = item.Amount;
                    objVesselCost.Text = item.Text;
                    objVesselCost.Order = item.Order ?? string.Empty;
                    objVesselCost.Reference = item.Reference;
                    objVesselCost.Type = item.Type;
                    objVesselCost.VoucherNo = item.Voucher;
                    objVesselCost.TransactionDate = item.TransactionDate.ToString(Constants.DateFormat);
                    objVesselCost.AccountId = item.AccountId;
                    objVesselCost.Currency = item.Currency;
                    objVesselCost.AccountingCompanyId = filters.AccountingCompanyId;
                    objVesselCost.DocumentCount = !string.IsNullOrWhiteSpace(item.DocumentId) ? 1 : item.DocumentCount;
                    objVesselCost.Counter = localCounter++;
                    result.Add(objVesselCost);
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get transactions by account code paged export to excel.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<List<VesselCostAnalysisTransactionResponse>> GetVesselCostAnalysisTransactionsExportToExcel(GLTransactionFilter filters)
        {
            List<VesselCostAnalysisTransactionResponseViewModel> result = new List<VesselCostAnalysisTransactionResponseViewModel>();
            List<VesselCostAnalysisTransactionResponse> response = new List<VesselCostAnalysisTransactionResponse>();

            var value = new Dictionary<string, object>()
            {
                { "coyId", filters.AccountingCompanyId },
                { "accId", filters.AccountCode},
                { "dateFrom", filters.FromDate},
                { "dateTo", filters.ToDate}
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/VesselCostAnalysisTransactions"));
            response = await PostAsync<List<VesselCostAnalysisTransactionResponse>>(requestUrl, CreateHttpContent(value));

            return response;
        }

        /// <summary>
        /// Post the get account balance for acc company
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<List<AccountBalanceDetailViewModel>> PostGetAccountBalanceForAccCompany(GLTransactionFilter filters)
        {
            List<AccountBalanceDetailViewModel> result = new List<AccountBalanceDetailViewModel>();

            var value = new Dictionary<string, object>()
            {
                { "accountingCompanyID", filters.AccountingCompanyId },
                { "transactionFromDate", filters.FromDate },
                { "transactionToDate", filters.ToDate },
                { "coyType", filters.CoyType },
                { "accId", filters.AccountCode },
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/AccountBalanceForAccCompany"));
            var response = await PostAsyncAutoPaged<AccountBalanceDetail>(requestUrl, value, 20);

            if (response != null && response.Any())
            {
                foreach (AccountBalanceDetail item in response)
                {
                    AccountBalanceDetailViewModel accountBalanceViewModel = GetAccountBalanceViewModelFromResponse(item, filters);

                    //transaction URL
                    GLTransactionViewModel transactionVM = new GLTransactionViewModel();
                    transactionVM.FromDate = filters.FromDate;
                    transactionVM.ToDate = filters.ToDate;
                    transactionVM.AccountCode = item.AccountID;
                    transactionVM.AccountName = item.Description;
                    transactionVM.AccountNameDescription = item.AccountID + " - " + item.Description;
                    transactionVM.AccountingCompanyId = filters.AccountingCompanyId;
                    transactionVM.ChhId = filters.AccountingCompanyChhId;
                    transactionVM.FinancialYearStartDate = filters.FinancialYearStartDate;
                    accountBalanceViewModel.TransactionURL = _provider.CreateProtector("GLTransaction").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(transactionVM));

                    result.Add(accountBalanceViewModel);
                }
            }

            return result;

        }

        /// <summary>
        /// Gets the account balance view model from response.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        private AccountBalanceDetailViewModel GetAccountBalanceViewModelFromResponse(AccountBalanceDetail item, GLTransactionFilter filters)
        {
            AccountBalanceDetailViewModel accountBalanceViewModel = new AccountBalanceDetailViewModel();
            accountBalanceViewModel.AccountCode = item.AccountID;
            accountBalanceViewModel.Description = !string.IsNullOrWhiteSpace(item.Description) ? item.Description : string.Empty;

            accountBalanceViewModel.OriginalBalance = item.OriginalBalance == null ? string.Empty : string.Format(Constants.TwoDecimal_NumberFormat, item.OriginalBalance);
            accountBalanceViewModel.BaseBalanceUSD = item.BaseBalanceUSD == null ? string.Empty : string.Format(Constants.TwoDecimal_NumberFormat, item.BaseBalanceUSD);

            accountBalanceViewModel.AccountType = EnumsHelper.GetEnumItemFromKeyValue(typeof(ChartDetailAccountType), item.AccountType);

            if (item.CurrencyType == null || item.CurrencyType.Equals(EnumsHelper.GetKeyValue(ChartDetailCurrencyType.BaseCurrency)))
            {
                accountBalanceViewModel.CurrencyType = !string.IsNullOrWhiteSpace(filters.BaseCoyCurr) ? filters.BaseCoyCurr : string.Empty;
            }
            else
            {
                accountBalanceViewModel.CurrencyType = !string.IsNullOrWhiteSpace(item.CurrencyID) ? item.CurrencyID : string.Empty;
            }

            if (item.CurrencyType == Constants.MultiCurrencyType)
            {
                accountBalanceViewModel.CurrencyType = Constants.MultiCurrency;
            }

            var auxFieldListst = FillRequiredAuxFieldList(item.ApplicableAccountAuxiliaries, filters.CoyType);
            accountBalanceViewModel.Auxiliaries = auxFieldListst != null && auxFieldListst.Any() ? auxFieldListst.Count().ToString() : string.Empty;
            return accountBalanceViewModel;
        }

        /// <summary>
        /// Gets the account codes.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<ChartAccountDetailResponseViewModel>> GetAccountCodes(ChartAccountDetailRequest request)
        {
            List<ChartAccountDetailResponseViewModel> result = new List<ChartAccountDetailResponseViewModel>();
            List<ChartAccountDetailResponse> response = null;
            var value = new Dictionary<string, object>()
            {
                { "request", request }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/AccountCodeList"));
            response = await PostAsyncAutoPaged<ChartAccountDetailResponse>(requestUrl, value, 100);

            if (response != null && response.Any())
            {
                foreach (ChartAccountDetailResponse item in response.Distinct())
                {
                    ChartAccountDetailResponseViewModel accountDetailResponseViewModel = new ChartAccountDetailResponseViewModel();
                    accountDetailResponseViewModel.AccountId = item.AccountId;
                    accountDetailResponseViewModel.ChartCurrencyId = item.ChartCurrencyId ?? "";
                    accountDetailResponseViewModel.ChartCurrencyType = item.ChartCurrencyType;
                    accountDetailResponseViewModel.ChartDescription = item.ChartDescription;
                    accountDetailResponseViewModel.ChartDetailId = item.ChartDetailId;
                    accountDetailResponseViewModel.ChartDetailParentId = item.ChartDetailParentId;
                    accountDetailResponseViewModel.ChartHeaderId = item.ChartHeaderId;
                    accountDetailResponseViewModel.ChartPosting = item.ChartPosting;
                    accountDetailResponseViewModel.ChartType = item.ChartType;
                    accountDetailResponseViewModel.IsBankAccount = item.IsBankAccount;
                    result.Add(accountDetailResponseViewModel);
                }
            }

            return result;
        }


        /// <summary>
        /// Posts the get genral ledger summary details
        /// </summary>
        /// <param name="coyId"></param>
        /// <returns></returns>
        public async Task<AccountingCompanyDetailViewModel> GetAccCompanyAdditionalInfo(string coyId)
        {
            AccountingCompanyDetailViewModel accountingCompanyDetails = new AccountingCompanyDetailViewModel();
            AccountingCompanyDetail response = new AccountingCompanyDetail();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/AccCompanyAdditionalInfo/" + coyId));
            response = await PostAsync<AccountingCompanyDetail>(requestUrl, CreateHttpContent(coyId));

            if (response != null)
            {
                accountingCompanyDetails.AccountingCompanyType = response.AccountingCompanyType;
                accountingCompanyDetails.Type = "";
                accountingCompanyDetails.VMDOwner = response.VMDOwner;
                accountingCompanyDetails.BaseCurrency = response.BaseCurrency;
                accountingCompanyDetails.FinancialYearStartDate = response.FinancialYearStartDate != null ? Convert.ToDateTime(response.FinancialYearStartDate).ToString(Constants.DateFormat) : string.Empty;
                accountingCompanyDetails.FinancialYearEndDate = response.FinancialYearEndDate != null ? Convert.ToDateTime(response.FinancialYearEndDate).ToString(Constants.DateFormat) : string.Empty;
                accountingCompanyDetails.ManagementStartDate = response.ManagementStartDate != null ? Convert.ToDateTime(response.ManagementStartDate).ToString(Constants.DateFormat) : string.Empty;
                accountingCompanyDetails.GeneralLedgerCutOffDate = response.GeneralLedgerCutOffDate != null ? Convert.ToDateTime(response.GeneralLedgerCutOffDate).ToString(Constants.DateFormat) : string.Empty;
            }

            return accountingCompanyDetails;
        }


        /// <summary>
        /// Fills the required aux field list.
        /// </summary>
        /// <param name="coyType">Type of the coy.</param>
        private List<AccountBalanceAuxViewModel> FillRequiredAuxFieldList(ApplicableAccountAuxiliaries entity, string CoyType)
        {
            var list = new List<AccountBalanceAuxViewModel>();
            if (entity != null)
            {
                if (CoyType == EnumsHelper.GetKeyValue(VesselEntityType.Vessel))
                {
                    if (entity.AuxClaims == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Claims) }); }
                    if (entity.AuxSeasonal == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Seasonal) }); }
                    if (entity.AuxNationality == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Nationality) }); }
                    if (entity.AuxRank == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Rank) }); }
                    if (entity.AuxVessel == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Vessel) }); }
                    if (entity.AuxGeneral1 == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.General1) }); }
                    if (entity.AuxGeneral3 == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.General3) }); }
                }
                else if (CoyType == EnumsHelper.GetKeyValue(VesselEntityType.Entity))
                {
                    if (entity.Aux7 == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Aux7) }); }
                    if (entity.Aux8 == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Aux8) }); }
                    if (entity.Aux9 == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Aux9) }); }
                    if (entity.Expense == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Expense) }); }
                    if (entity.Group == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Group) }); }
                    if (entity.Project == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Project) }); }
                    if (entity.Employee == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Employee) }); }
                    if (entity.Department == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.Department) }); }
                    if (entity.Vessel == true) { list.Add(new AccountBalanceAuxViewModel { RequiredAuxField = EnumsHelper.GetKeyValue(Auxiliary.EntityVessel) }); }
                }
            }
            return list;
        }

        /// <summary>
        /// Gets the over budget details.
        /// </summary>
        /// <param name="requestVM">The request.</param>
        /// <returns></returns>
        public async Task<List<OverBudgetDetailsResponseViewModel>> GetOverBudgetDetails(OverBudgetDetailsRequestViewModel requestVM)
        {
            OverBudgetDetailsRequest request = new OverBudgetDetailsRequest();
            if (requestVM != null)
            {
                request.FleetId = requestVM.FleetId;
                request.MenuType = requestVM.MenuType;

                request.ChartType = EnumsHelper.GetKeyValue(ReportDefinitionType.S);

                string requestVesselId = GetDecryptedVesselId(requestVM.EncryptedVesselId);
                request.VesselIds = !string.IsNullOrWhiteSpace(requestVesselId) ? new List<string>() { requestVesselId } : null;

                var previousMonth = DateTime.Now.AddMonths(-1);
                var toDate = new DateTime(previousMonth.Year, previousMonth.Month, 1);
                request.ToDate = toDate.AddDays(-1);
            }

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/PWAOverBudgetDetails"));
            List<OverBudgetDetailsResponse> response = await PostAsync<List<OverBudgetDetailsResponse>>(requestUrl, CreateHttpContent(request));

            List<OverBudgetDetailsResponseViewModel> result = new List<OverBudgetDetailsResponseViewModel>();
            OperatingCostBarChartRequest opexRequest = null;

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    opexRequest = new OperatingCostBarChartRequest();
                    opexRequest.AccountLevel = -1;
                    opexRequest.CoyId = x.CoyId;
                    opexRequest.ReportDefinitionType = String.IsNullOrWhiteSpace(x.ChartType) ? ReportDefinitionType.S : ((ReportDefinitionType)Enum.Parse(typeof(ReportDefinitionType), (EnumsHelper.GetEnumItemFromKeyValue(typeof(ReportDefinitionType), x.ChartType))));
                    opexRequest.ToDate = x.BudgetToDate;

                    result.Add(new OverBudgetDetailsResponseViewModel()
                    {
                        EncryptedVesselId = GetEncryptedVessel(x.VesselId, x.VesselName, x.CoyId),
                        EncryptedOpexURL = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(opexRequest)),
                        VesselName = x.VesselName,
                        Total = x.UsdTotal,
                        Budget = x.UsdBudget,
                        Variance = x.UsdVariance,
                        BudgetPercenatge = x.UsdBudgetPercenatge
                    });
                });
            }

            return result;
        }

        /// <summary>
        /// Gets the opex fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<OpexFleetSummaryResponseViewModel> GetOpexFleetSummary(OpexFleetSummaryRequestViewModel input)
        {
            OpexFleetSummaryRequest request = new OpexFleetSummaryRequest();
            OpexFleetSummaryResponse response = new OpexFleetSummaryResponse();
            OpexFleetSummaryResponseViewModel fleetSummary = new OpexFleetSummaryResponseViewModel();

            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;

            request.OpexToDate = input.OpexToDate;
            request.BudgetDays = input.BudgetDays;
            request.BudgetPercentageHighLimit = input.BudgetPercentageHighLimit;
            request.BudgetPercentageLowLimit = input.BudgetPercentageLowLimit;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, input.VesselId);
            request.VesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/PWAOpexDashboardFleetSummary"));
            response = await PostAsync<OpexFleetSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                fleetSummary.OpexOverBudgetPercentage = response.OpexOverBudgetPercentage == null
                                                        ? Constants.NotApplicable
                                                        : string.Format(Constants.TwoDecimal_NumberFormat, response.OpexOverBudgetPercentage) + "%";
                fleetSummary.OpexOverBudgetPriority = response.OpexOverBudgetPriority;
                fleetSummary.OpexOverBudgetToDate = string.Format("{0:MMM}-{0:yyyy}", request.OpexToDate);
                fleetSummary.OpexOverBudgetInfo = response.OpexOverBudgetInfo;
            }
            return fleetSummary;
        }


        /// <summary>
        /// Exports to excel general ledger.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<List<AccountBalanceDetailExportViewModel>> ExportToExcelGeneralLedgerList(GLTransactionFilter filters)
        {
            List<AccountBalanceDetailExportViewModel> result = new List<AccountBalanceDetailExportViewModel>();

            var value = new Dictionary<string, object>()
            {
                { "accountingCompanyID", filters.AccountingCompanyId },
                { "transactionFromDate", filters.FromDate },
                { "transactionToDate", filters.ToDate },
                { "coyType", filters.CoyType },
                { "accId", filters.AccountCode },
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Finance/AccountBalanceForAccCompany"));
            var response = await PostAsyncAutoPaged<AccountBalanceDetail>(requestUrl, value, 20);

            if (response != null && response.Any())
            {
                foreach (AccountBalanceDetail item in response)
                {
                    AccountBalanceDetailViewModel accountBalanceViewModel = GetAccountBalanceViewModelFromResponse(item, filters);
                    AccountBalanceDetailExportViewModel accountBalanceExportViewModel = new AccountBalanceDetailExportViewModel
                    {
                        AccountCode = accountBalanceViewModel.AccountCode,
                        AccountType = accountBalanceViewModel.AccountType,
                        Auxiliaries = accountBalanceViewModel.Auxiliaries,
                        BaseBalanceUSD = accountBalanceViewModel.BaseBalanceUSD,
                        CurrencyType = accountBalanceViewModel.CurrencyType,
                        Description = accountBalanceViewModel.Description,
                        OriginalBalance = accountBalanceViewModel.OriginalBalance
                    };

                    result.Add(accountBalanceExportViewModel);
                }
            }

            return result;

        }
    }
}
