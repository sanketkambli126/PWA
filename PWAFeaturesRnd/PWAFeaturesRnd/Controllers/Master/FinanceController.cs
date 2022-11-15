using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.ExportToExcel;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report;
using PWAFeaturesRnd.Models.Report.Finance;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.Models.Report.VesselManagement;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.ExportToExcel;
using PWAFeaturesRnd.ViewModels.Finance;
using PWAFeaturesRnd.ViewModels.Report;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class FinanceController : AuthenticatedController
    {
        /// <summary>
        /// The finance client
        /// </summary>
        private readonly FinanceClient _financeClient;

        /// <summary>
        /// The marine client
        /// </summary>
        private readonly MarineClient _marineClient;

        /// <summary>
        /// The shared client
        /// </summary>
        private readonly SharedClient _sharedClient;

        /// <summary>
        /// The document client
        /// </summary>
        private readonly DocumentClient _documentClient;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseOrderController" /> class.
        /// </summary>
        /// <param name="_financeRequestClient">The finance request client.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="marineClient">The marine client.</param>
        /// <param name="sharedClient">The shared client.</param>
        /// <param name="documentClient">The document client.</param>
        public FinanceController(FinanceClient _financeRequestClient, IDataProtectionProvider provider, MarineClient marineClient, SharedClient sharedClient, DocumentClient documentClient)
        {
            _provider = provider;
            _financeClient = _financeRequestClient;
            _marineClient = marineClient;
            _sharedClient = sharedClient;
            _documentClient = documentClient;
        }

        /// <summary>
        /// Lists the specified operation cost request URL.
        /// </summary>
        /// <param name="OperationCostRequestUrl">The operation cost request URL.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <returns></returns>
        public async Task<IActionResult> List(string OperationCostRequestUrl, string VesselId, bool IsVesselChanged)
        {
            OperatingCostBarChartRequest operationCostRequest = new OperatingCostBarChartRequest();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);

            string data = _provider.CreateProtector("OperationCostURL").Unprotect(OperationCostRequestUrl);
            operationCostRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<OperatingCostBarChartRequest>(data);

            string oldVesselId = string.Empty;
            string oldVesselCoyId = string.Empty;
            if (!string.IsNullOrEmpty(operationCostRequest.VesselId))
            {
                string decreptedOldVesselString = _provider.CreateProtector("Vessel").Unprotect(operationCostRequest.VesselId);
                oldVesselId = decreptedOldVesselString.Split(Constants.Separator)[0];
                oldVesselCoyId = decreptedOldVesselString.Split(Constants.Separator)[2];
            }
            else
            {
                oldVesselId = decreptedString.Split(Constants.Separator)[0];
                oldVesselCoyId = decreptedString.Split(Constants.Separator)[2];
            }

            string NewVesselId = decreptedString.Split(Constants.Separator)[0];
            string NewVesselCoyId = decreptedString.Split(Constants.Separator)[2];

            OperationCostDrillDownViewModel operationCostVM = new OperationCostDrillDownViewModel();

            if ((!string.IsNullOrWhiteSpace(oldVesselId) && !string.IsNullOrWhiteSpace(NewVesselId) && oldVesselId.Equals(NewVesselId)
                && !string.IsNullOrWhiteSpace(oldVesselCoyId) && !string.IsNullOrWhiteSpace(NewVesselCoyId) && oldVesselCoyId.Equals(NewVesselCoyId)) && !IsVesselChanged)
            {
                operationCostVM.VesselId = VesselId;
                operationCostVM.VesselName = decreptedString.Split(Constants.Separator)[1];
                operationCostVM.AccountId = operationCostRequest.AccountId;
                operationCostVM.AccountLevel = operationCostRequest.AccountLevel;
                operationCostVM.CoyId = decreptedString.Split(Constants.Separator)[2];
                operationCostVM.ReportDefinitionType = operationCostRequest.ReportDefinitionType;
                operationCostVM.Parent2AccAndDesc = operationCostRequest.Parent2AccAndDesc;
                operationCostVM.Parent3AccAndDesc = operationCostRequest.Parent3AccAndDesc;
                operationCostVM.Parent1AccAndDesc = operationCostRequest.Parent1AccAndDesc;
                operationCostVM.ToDate = operationCostRequest.ToDate;
            }
            else
            {
                operationCostVM.VesselId = VesselId;
                operationCostVM.VesselName = decreptedString.Split(Constants.Separator)[1];
                operationCostVM.AccountId = null;
                operationCostVM.AccountLevel = -1;
                operationCostVM.CoyId = NewVesselCoyId;
                operationCostVM.ReportDefinitionType = operationCostRequest.ReportDefinitionType;
                operationCostVM.Parent2AccAndDesc = null;
                operationCostVM.Parent3AccAndDesc = null;
                operationCostVM.Parent1AccAndDesc = null;
                operationCostVM.ToDate = operationCostRequest.ToDate;
            }

            if (operationCostVM.AccountLevel != -1)
            {
                operationCostVM = PreviousStageNavigation(operationCostVM);
            }
            else
            {
                operationCostVM.CurrentStageTitle = "Total Budget";
            }

            operationCostVM.MonthList = GetMonthList();
            operationCostVM.YearList = await GetYearList(!string.IsNullOrWhiteSpace(NewVesselCoyId) ? NewVesselCoyId : oldVesselCoyId, !string.IsNullOrWhiteSpace(NewVesselId) ? NewVesselId : oldVesselId);
            operationCostVM.SelectedMonth = operationCostRequest.ToDate.HasValue ? operationCostRequest.ToDate.Value.Month.ToString() : "1";
            operationCostVM.SelectedYear = operationCostRequest.ToDate.HasValue ? operationCostRequest.ToDate.Value.Year.ToString() : "2020";
            if (operationCostRequest.ToDate.HasValue)
            {
                DateTime date = operationCostRequest.ToDate.Value;
                string dateSuffix = (date.Day % 10 == 1 && date.Day % 100 != 11) ? "st"
                                    : (date.Day % 10 == 2 && date.Day % 100 != 12) ? "nd"
                                    : (date.Day % 10 == 3 && date.Day % 100 != 13) ? "rd"
                                    : "th";

                operationCostVM.HeaderToDate = string.Format("{0:dd}{1} {0:MMM} {0:yyyy}", date, dateSuffix);
            }
            else
            {
                operationCostVM.HeaderToDate = "";
            }
            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey);
            string OperationCostRequestData = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(operationCostVM));
            if (operationCostVM.AccountLevel != -1)
            {
                //previous is actually the current stage name
                //but the previous stage url is proper previous url
                //client said breadcrumb is not needed on finance, hence the code is commented
                //List<Tuple<string, string>> stack = UpdateStack(operationCostVM.PreviousStageName, OperationCostRequestUrl);
                //operationCostVM.NavigationBreadcrumbs = stack;

                SetSessionDetail(operationCostVM.PreviousStageName, pageKey, OperationCostRequestData);
            }
            else
            {
                SetSessionDetail(pageKey, null, OperationCostRequestUrl);
                RemoveSessionFilter(_provider, EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey), null, decreptedString.Split(Constants.Separator)[0]);
                //create stack in session
                //CreateStackInSession(OperationCostRequestUrl);

                //operationCostVM.NavigationBreadcrumbs = new List<Tuple<string, string>> { };
            }

            operationCostVM.ActiveMobileTabClass = SetTab(pageKey, operationCostVM.ActiveMobileTabClass, Constants.Tab1);

            return View(operationCostVM);
        }

        /// <summary>
        /// Creates the stack in session.
        /// </summary>
        private void CreateStackInSession(string value)
        {
            string stackKey = EnumsHelper.GetKeyValue(NavigationPageKey.FinanceStackKey);

            //stack object is a list datastructure but it is operated stack-like (FIFO)
            List<Tuple<string, string>> stack = new List<Tuple<string, string>>();

            //add current list page in the stack
            stack.Add(new Tuple<string, string>(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey), value));

            //this is to put the stack with stackKey in the session 
            SaveStack(stack, stackKey);
        }

        /// <summary>
        /// Adds the into stack.
        /// </summary>
        /// <param name="stageName">Name of the stage.</param>
        /// <param name="value">The value.</param>
        private List<Tuple<string, string>> UpdateStack(string stageName,string value)
        {
            string stackKey = EnumsHelper.GetKeyValue(NavigationPageKey.FinanceStackKey);
            List<Tuple<string, string>> stack = GetStack(stackKey);

            //if current page is already present in the stack,
            //then it means it was already visited and we need to update it
            //so we pop all the pages after we have visited the current page
            //we also remove the current page with old url
            //and add the current page with the updated current url
            if (ContainsInStack(stageName, stack))
            {
                Tuple<string, string> current = stack.ElementAt(stack.Count-1);
                stack.RemoveAt(stack.Count - 1);

                //remove all the pages visited after the current stage
                while (!current.Item1.Equals(stageName) && stack.Count > 0)
                {
                    //removing session
                    GetSourceURL(current.Item1);

                    current = stack.ElementAt(stack.Count - 1);
                    stack.RemoveAt(stack.Count - 1);
                };
            }

            //add updated page in the stack
            stack.Add(new Tuple<string, string>(stageName, value));
            SaveStack(stack, stackKey);
            return stack;
        }

        /// <summary>
        /// Determines whether [contains in stack] [the specified stage name].
        /// </summary>
        /// <param name="stageName">Name of the stage.</param>
        /// <param name="stack">The stack.</param>
        /// <returns>
        ///   <c>true</c> if [contains in stack] [the specified stage name]; otherwise, <c>false</c>.
        /// </returns>
        private bool ContainsInStack(string stageName, List<Tuple<string, string>> stack)
        {
            return stack.Any(x => x.Item1.Equals(stageName));
        }

        /// <summary>
        /// Gets the stack.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private List<Tuple<string,string>> GetStack(string key)
        {
            List<Tuple<string, string>> stack = CommonUtil.GetSessionObject<List<Tuple<string, string>>>(HttpContext.Session, key);
            if(stack == null)
            {
                return new List<Tuple<string,string>>();
            }
            return stack;
        }

        /// <summary>
        /// Saves the stack.
        /// </summary>
        /// <param name="stack">The stack.</param>
        /// <param name="key">The key.</param>
        private void SaveStack(List<Tuple<string,string>> stack,string key)
        {
            HttpContext.Session.SetSessionObject(key, stack);
        }

        /// <summary>
        /// Gets the operation cost list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOperationCostList(OperationCostDrillDownViewModel request)
        {
            _financeClient.AccessToken = GetAccessToken();
            OperatinCostViewModel response = await _financeClient.PostGetRCDrillDownDetailsByLevel(request);
            return new JsonResult(new { operatingCostList = response.OperatingCostList, budget = response.Budget, variance = response.Variance, total = response.Total, actual = response.Actual, accurals = response.Accurals, previousStageNavigation = response.PreviousOperationCostUrl, encryptedVesselId = response.EncryptedVesselId });
        }

        /// <summary>
        /// Gets the operation cost header details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOperationCostHeaderDetails(OperationCostDrillDownViewModel request)
        {
            _financeClient.AccessToken = GetAccessToken();
            RunningCostRecalReportHeaderDetailsViewModel response = await _financeClient.PostGetRunningCostRecalReportHeaderDetails(request);
            return new JsonResult(response);
        }

        /// <summary>
        /// Sets the page parameter.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> SetPageParameter(OperationCostDrillDownViewModel input)
        {
            _financeClient.AccessToken = GetAccessToken();

            if (!input.IsTransactionLevel)
            {
                OperatingCostBarChartRequest request = new OperatingCostBarChartRequest();
                DateTime endOfMonth = new DateTime(Convert.ToInt32(input.SelectedYear), Convert.ToInt32(input.SelectedMonth), 1).AddMonths(1).AddDays(-1);
                request.ToDate = endOfMonth;
                request.CoyId = input.CoyId;
                request.VesselId = input.VesselId;
                request.AccountId = input.AccountId;
                request.AccountLevel = input.AccountLevel;
                request.Parent1AccAndDesc = input.Parent1AccAndDesc;
                request.Parent2AccAndDesc = input.Parent2AccAndDesc;
                request.Parent3AccAndDesc = input.Parent3AccAndDesc;
                request.ReportDefinitionType = input.ReportDefinitionType;
                request.VesselId = input.VesselId;
                string OperationCostRequestUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey), OperationCostRequestUrl, input.VesselId);
                return Json(Url.Action("List", new { OperationCostRequestUrl = OperationCostRequestUrl, VesselId = input.VesselId }));
            }
            else
            {
                OperatingCostBarChartRequest request = new OperatingCostBarChartRequest();
                string previousStageData = _provider.CreateProtector("OperationCostURL").Unprotect(input.PreviousStageUrl);
                request = Newtonsoft.Json.JsonConvert.DeserializeObject<OperatingCostBarChartRequest>(previousStageData);
                DateTime endOfMonth = new DateTime(Convert.ToInt32(input.SelectedYear), Convert.ToInt32(input.SelectedMonth), 1).AddMonths(1).AddDays(-1);
                request.ToDate = endOfMonth;

                GLTransactionFilter transactionRequest = new GLTransactionFilter();
                string transactionData = _provider.CreateProtector("TransactionCostRequestUrl").Unprotect(input.TransactionRequestUrl);
                transactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<GLTransactionFilter>(transactionData);
                transactionRequest.ToDate = endOfMonth;


                OperationCostDrillDownViewModel inputRequest = new OperationCostDrillDownViewModel();
                inputRequest.ToDate = endOfMonth;
                inputRequest.AccountLevel = request.AccountLevel;
                inputRequest.ReportDefinitionType = request.ReportDefinitionType;
                inputRequest.CoyId = request.CoyId;
                inputRequest.AccountId = request.AccountId;
                inputRequest.Parent2AccAndDesc = request.Parent2AccAndDesc;
                inputRequest.Parent3AccAndDesc = request.Parent3AccAndDesc;
                inputRequest.VesselId = input.VesselId;
                OperatinCostViewModel response = await _financeClient.PostGetRCDrillDownDetailsByLevel(inputRequest);

                var previousNodeSelected = response != null && response.OperatingCostList != null ? response.OperatingCostList.Where(x => x.AccountId == transactionRequest.AccountCode).FirstOrDefault() : null;

                double localBudgetValue = 0;
                double localTotalValue = 0;
                double localVarianceValue = 0;
                double localActualValue = 0;
                double localAccuralsValue = 0;
                if (previousNodeSelected != null)
                {
                    localAccuralsValue = previousNodeSelected.Accurals;
                    localActualValue = previousNodeSelected.Actual;
                    localBudgetValue = previousNodeSelected.Budget;
                    localTotalValue = previousNodeSelected.Total;
                    localVarianceValue = previousNodeSelected.Variance;
                }

                transactionRequest.Total = localTotalValue;
                transactionRequest.Variance = localVarianceValue;
                transactionRequest.Budget = localBudgetValue;
                transactionRequest.Accurals = localAccuralsValue;
                transactionRequest.Actual = localActualValue;
                transactionRequest.FromDate = new DateTime(endOfMonth.Year, 1, 1);
                string operationCostUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                string transactionRequestUrl = _provider.CreateProtector("TransactionCostRequestUrl").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(transactionRequest));

                SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey), operationCostUrl, input.VesselId);
                return Json(Url.Action("Transaction", new { PreviousRequest = operationCostUrl, TransactionUrl = transactionRequestUrl, VesselId = input.VesselId }));
            }
        }

        /// <summary>
        /// Previouses the stage navigation.
        /// </summary>
        /// <param name="operationCostVM">The operation cost vm.</param>
        /// <returns></returns>
        public OperationCostDrillDownViewModel PreviousStageNavigation(OperationCostDrillDownViewModel operationCostVM)
        {
            OperatingCostBarChartRequest previousStageRequest = new OperatingCostBarChartRequest();

            if (operationCostVM != null && operationCostVM.AccountLevel != -1)
            {
                if (operationCostVM.AccountLevel == 1)
                {
                    previousStageRequest.VesselId = operationCostVM.VesselId;
                    previousStageRequest.AccountId = operationCostVM.Parent2AccAndDesc;
                    previousStageRequest.AccountLevel = 2;
                    previousStageRequest.CoyId = operationCostVM.CoyId;
                    previousStageRequest.ReportDefinitionType = operationCostVM.ReportDefinitionType;
                    previousStageRequest.Parent2AccAndDesc = null;
                    previousStageRequest.Parent3AccAndDesc = operationCostVM.Parent3AccAndDesc;
                    previousStageRequest.Parent1AccAndDesc = null;
                    previousStageRequest.ToDate = operationCostVM.ToDate;

                    operationCostVM.PreviousStageUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(previousStageRequest));

                    int index = operationCostVM.AccountId.IndexOf(Constants.OperationCostDrillDownDelimiter) + 1;
                    string stageName = operationCostVM.AccountId.Substring(index);

                    operationCostVM.PreviousStageName = "Back To "+stageName;
                }
                else if (operationCostVM.AccountLevel == 2)
                {
                    previousStageRequest.VesselId = operationCostVM.VesselId;
                    previousStageRequest.AccountId = operationCostVM.Parent3AccAndDesc;
                    previousStageRequest.AccountLevel = 3;
                    previousStageRequest.CoyId = operationCostVM.CoyId;
                    previousStageRequest.ReportDefinitionType = operationCostVM.ReportDefinitionType;
                    previousStageRequest.Parent2AccAndDesc = null;
                    previousStageRequest.Parent3AccAndDesc = null;
                    previousStageRequest.Parent1AccAndDesc = null;
                    previousStageRequest.ToDate = operationCostVM.ToDate;

                    operationCostVM.PreviousStageUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(previousStageRequest));
                    int index = operationCostVM.AccountId.IndexOf(Constants.OperationCostDrillDownDelimiter) + 1;
                    string stageName = operationCostVM.AccountId.Substring(index);

                    operationCostVM.PreviousStageName = "Back To "+stageName;
                }
                else if (operationCostVM.AccountLevel == 3)
                {
                    previousStageRequest.VesselId = operationCostVM.VesselId;
                    previousStageRequest.AccountId = null;
                    previousStageRequest.AccountLevel = -1;
                    previousStageRequest.CoyId = operationCostVM.CoyId;
                    previousStageRequest.ReportDefinitionType = operationCostVM.ReportDefinitionType;
                    previousStageRequest.Parent2AccAndDesc = null;
                    previousStageRequest.Parent3AccAndDesc = null;
                    previousStageRequest.Parent1AccAndDesc = null;
                    previousStageRequest.ToDate = operationCostVM.ToDate;

                    operationCostVM.PreviousStageUrl = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(previousStageRequest));

                    int index = operationCostVM.AccountId.IndexOf(Constants.OperationCostDrillDownDelimiter) + 1;
                    string stageName = operationCostVM.AccountId.Substring(index);
                    operationCostVM.PreviousStageName = "Back To "+stageName;

                }
                operationCostVM.CurrentStageTitle = operationCostVM.AccountId.Replace(Constants.OperationCostDrillDownDelimiter, " -");
            }
            return operationCostVM;
        }

        /// <summary>
        /// Gets the transaction details.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="TransactionCostRequestUrl">The transaction cost request URL.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetTransactionDetails(DataTablePageRequest<string> pageRequest, string TransactionCostRequestUrl)
        {
            _financeClient.AccessToken = GetAccessToken();

            GLTransactionFilter searchFilter = new GLTransactionFilter();
            string data = _provider.CreateProtector("TransactionCostRequestUrl").Unprotect(TransactionCostRequestUrl);
            searchFilter = Newtonsoft.Json.JsonConvert.DeserializeObject<GLTransactionFilter>(data);

            DataTablePageResponse<List<CostAnalysisTransactionResponseViewModel>> response = await _financeClient.PostGetTransactionsByAccountCodePaged(pageRequest, searchFilter);

            return new JsonResult(new DataTablePageResponse<List<CostAnalysisTransactionResponseViewModel>>
            {
                Draw = pageRequest.Draw,
                RecordsFiltered = response.RecordsFiltered,
                Data = response.Data,
                RecordsTotal = response.RecordsTotal
            });
        }

        /// <summary>
        /// Gets the transaction details export to excel.
        /// </summary>
        /// <param name="TransactionCostRequestUrl">The transaction cost request URL.</param>
        /// <param name="CurrencyForExportToExcel">The currency for export to excel.</param>
        /// <param name="CurrentStageTitle">The current stage title.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetTransactionDetailsExportToExcel(string TransactionCostRequestUrl, string CurrencyForExportToExcel, string CurrentStageTitle)
        {
            _financeClient.AccessToken = GetAccessToken();
            GLTransactionFilter searchFilter = new GLTransactionFilter();
            string data = _provider.CreateProtector("TransactionCostRequestUrl").Unprotect(TransactionCostRequestUrl);
            searchFilter = Newtonsoft.Json.JsonConvert.DeserializeObject<GLTransactionFilter>(data);
            List<CostAnalysisTransactionResponse> response = await _financeClient.PostGetTransactionsByAccountCodePagedExportToExcel(searchFilter);

            ExportToExcelRequest request = new ExportToExcelRequest();

            request.FileName = "Transaction Details";
            request.Title = "Transaction Details";
            request.Summary = "Vessel : " + searchFilter.VesselName + "\nDate : " + DateTime.Now.ToString("dd MMM yyyy") + "\nAccount : " + CurrentStageTitle + "\nBudget : " + searchFilter.Budget + "\nAccrual : " + searchFilter.Accurals + "\nActual : " + searchFilter.Actual + "\nTotal : " + searchFilter.Total + "\nVariance : " + searchFilter.Variance + "\nCurrency : " + CurrencyForExportToExcel;
            request.SummaryRowCount = 9;
            request.ColumnCount = 7;
            if (response != null && response.Any())
            {
                List<CostAnalysisTransactionResponseExportViewModel> result = new List<CostAnalysisTransactionResponseExportViewModel>();

                response.ForEach(x =>
                    result.Add(new CostAnalysisTransactionResponseExportViewModel
                    {
                        Text = x.Text,
                        Date = x.TransactionDate.ToString("dd MMM yyyy"),
                        OrderNumber = x.OrderNo,
                        Supplier = x.Supplier,
                        Type = x.JournalType,
                        Reference = x.Reference,
                        Amount = string.Format(Constants.TwoDecimal_NumberFormat, x.Amount)
                    })
                );

                return ExportToExcel<CostAnalysisTransactionResponseExportViewModel>(result, request);
            }
            return new JsonResult(new { status = "Ok" });
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<FinanceReportViewModel> lstResult = new List<FinanceReportViewModel>();
            FinanceReportViewModel financeReportViewModel = new FinanceReportViewModel();
            financeReportViewModel.AccountCode = "50";
            financeReportViewModel.AccountName = "CREW";
            financeReportViewModel.Total = 558724;
            financeReportViewModel.Budget = 797567;
            financeReportViewModel.Variance = 238843;

            lstResult.Add(financeReportViewModel);

            financeReportViewModel = new FinanceReportViewModel();
            financeReportViewModel.AccountCode = "52";
            financeReportViewModel.AccountName = "INSURANCE";
            financeReportViewModel.Total = 0;
            financeReportViewModel.Budget = 0;
            financeReportViewModel.Variance = 0;

            lstResult.Add(financeReportViewModel);

            financeReportViewModel = new FinanceReportViewModel();
            financeReportViewModel.AccountCode = "54";
            financeReportViewModel.AccountName = "TOTAL TECHNICAL";
            financeReportViewModel.Total = 281068;
            financeReportViewModel.Budget = 441079;
            financeReportViewModel.Variance = 160011;

            lstResult.Add(financeReportViewModel);

            financeReportViewModel = new FinanceReportViewModel();
            financeReportViewModel.AccountCode = "582";
            financeReportViewModel.AccountName = "MANAGEMENT FEES";
            financeReportViewModel.Total = 72500;
            financeReportViewModel.Budget = 75656;
            financeReportViewModel.Variance = 3156;

            lstResult.Add(financeReportViewModel);

            return View(lstResult);
        }

        /// <summary>
        /// Grids the view.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult GridView(string id)
        {
            ViewBag.id = id ?? "0";
            return View();
        }

        /// <summary>
        /// Transactions the specified previous request.
        /// </summary>
        /// <param name="PreviousRequest">The previous request.</param>
        /// <param name="TransactionUrl">The transaction URL.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <returns></returns>
        public async Task<IActionResult> Transaction(string PreviousRequest, string TransactionUrl, string VesselId, bool IsVesselChanged)
        {
            OperationCostDrillDownViewModel transactionDetailsVM = new OperationCostDrillDownViewModel();

            OperatingCostBarChartRequest operationCostRequest = new OperatingCostBarChartRequest();
            string previousData = _provider.CreateProtector("OperationCostURL").Unprotect(PreviousRequest);
            operationCostRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<OperatingCostBarChartRequest>(previousData);

            GLTransactionFilter filters = new GLTransactionFilter();
            string currentData = _provider.CreateProtector("TransactionCostRequestUrl").Unprotect(TransactionUrl);
            filters = Newtonsoft.Json.JsonConvert.DeserializeObject<GLTransactionFilter>(currentData);

            string decreptedNewVessel = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            string newVesselId = decreptedNewVessel.Split(Constants.Separator)[0];

            string decryptedOldVessel = _provider.CreateProtector("Vessel").Unprotect(operationCostRequest.VesselId);
            string oldVesselId = decryptedOldVessel.Split(Constants.Separator)[0];

            if (newVesselId != oldVesselId || IsVesselChanged)
            {
                OperationCostDrillDownViewModel operationCostVM = new OperationCostDrillDownViewModel();
                operationCostVM.VesselId = VesselId;
                operationCostVM.VesselName = decreptedNewVessel.Split(Constants.Separator)[1];
                operationCostVM.AccountId = null;
                operationCostVM.AccountLevel = -1;
                operationCostVM.CoyId = decreptedNewVessel.Split(Constants.Separator)[2];
                operationCostVM.ReportDefinitionType = operationCostRequest.ReportDefinitionType;
                operationCostVM.ToDate = operationCostRequest.ToDate;

                string operationCostURL = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(operationCostVM));
                return RedirectToAction("List", new { OperationCostRequestUrl = operationCostURL, VesselId = VesselId });
            }

            transactionDetailsVM.PreviousStageUrl = PreviousRequest;
            transactionDetailsVM.TransactionRequestUrl = TransactionUrl;
            transactionDetailsVM.VesselId = VesselId;
            transactionDetailsVM.VesselName = decreptedNewVessel.Split(Constants.Separator)[1];
            transactionDetailsVM.CoyId = decreptedNewVessel.Split(Constants.Separator)[2];
            transactionDetailsVM.AccountCode = filters.AccountCode;

            DateTime date = filters.ToDate;
            string dateSuffix = (date.Day % 10 == 1 && date.Day % 100 != 11) ? "st"
                                : (date.Day % 10 == 2 && date.Day % 100 != 12) ? "nd"
                                : (date.Day % 10 == 3 && date.Day % 100 != 13) ? "rd"
                                : "th";

            transactionDetailsVM.TransactionToDate = string.Format("{0:dd}{1} {0:MMM} {0:yyyy}", date, dateSuffix);
            transactionDetailsVM.PreviousStageName = "Back To " + operationCostRequest.LabelName;
            transactionDetailsVM.Accurals = filters.Accurals;
            transactionDetailsVM.Actual = filters.Actual;
            transactionDetailsVM.Budget = filters.Budget;
            transactionDetailsVM.Variance = filters.Variance;
            transactionDetailsVM.Total = filters.Total;
            transactionDetailsVM.ToDate = filters.ToDate;
            transactionDetailsVM.ReportDefinitionType = operationCostRequest.ReportDefinitionType;
            transactionDetailsVM.CurrentStageTitle = filters.AccountCode + " - " + operationCostRequest.LabelName;
            transactionDetailsVM.MonthList = GetMonthList();
            transactionDetailsVM.YearList = await GetYearList(decreptedNewVessel.Split(Constants.Separator)[2], newVesselId ?? oldVesselId);
            transactionDetailsVM.SelectedMonth = operationCostRequest.ToDate.HasValue ? operationCostRequest.ToDate.Value.Month.ToString() : "1";
            transactionDetailsVM.SelectedYear = operationCostRequest.ToDate.HasValue ? operationCostRequest.ToDate.Value.Year.ToString() : "2020";

            var transactionData = _provider.CreateProtector("TransactionCostRequestUrl").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(transactionDetailsVM));
            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.FinanceTransactionPageKey);
            SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey), transactionData);

            transactionDetailsVM.ActiveMobileTabClass = SetTab(pageKey, transactionDetailsVM.ActiveMobileTabClass, Constants.Tab1);
            return View(transactionDetailsVM);
        }

        /// <summary>
        /// Gets the general ledger list.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetGeneralLedgerList(GeneralLedgerTransactionRequest inputRequest)
        {
            _financeClient.AccessToken = GetAccessToken();
            List<GeneralLedgerTransactionResponseViewModel> response = await _financeClient.PostGetGeneralLedgerTransactionsQuery(inputRequest);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the month list.
        /// </summary>
        /// <returns></returns>
        public List<LookUp> GetMonthList()
        {
            List<LookUp> MonthList = new List<LookUp>();

            LookUp janMonth = new LookUp();
            janMonth.Description = "Jan";
            janMonth.Identifier = "1";
            MonthList.Add(janMonth);

            LookUp febMonth = new LookUp();
            febMonth.Description = "Feb";
            febMonth.Identifier = "2";
            MonthList.Add(febMonth);

            LookUp marchMonth = new LookUp();
            marchMonth.Description = "Mar";
            marchMonth.Identifier = "3";
            MonthList.Add(marchMonth);

            LookUp aprilMonth = new LookUp();
            aprilMonth.Description = "Apr";
            aprilMonth.Identifier = "4";
            MonthList.Add(aprilMonth);

            LookUp mayMonth = new LookUp();
            mayMonth.Description = "May";
            mayMonth.Identifier = "5";
            MonthList.Add(mayMonth);

            LookUp juneMonth = new LookUp();
            juneMonth.Description = "Jun";
            juneMonth.Identifier = "6";
            MonthList.Add(juneMonth);

            LookUp julyMonth = new LookUp();
            julyMonth.Description = "Jul";
            julyMonth.Identifier = "7";
            MonthList.Add(julyMonth);

            LookUp augMonth = new LookUp();
            augMonth.Description = "Aug";
            augMonth.Identifier = "8";
            MonthList.Add(augMonth);

            LookUp septMonth = new LookUp();
            septMonth.Description = "Sept";
            septMonth.Identifier = "9";
            MonthList.Add(septMonth);

            LookUp octMonth = new LookUp();
            octMonth.Description = "Oct";
            octMonth.Identifier = "10";
            MonthList.Add(octMonth);

            LookUp novMonth = new LookUp();
            novMonth.Description = "Nov";
            novMonth.Identifier = "11";
            MonthList.Add(novMonth);

            LookUp decMonth = new LookUp();
            decMonth.Description = "Dec";
            decMonth.Identifier = "12";
            MonthList.Add(decMonth);

            return MonthList;
        }

        /// <summary>
        /// Gets the year list.
        /// </summary>
        /// <param name="coyId">The coy identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<LookUp>> GetYearList(string coyId, string vesselId)
        {
            List<LookUp> YearList = new List<LookUp>();

            var currentYear = DateTime.Now.Year;
            var previousTenYears = currentYear - 10;

            _marineClient.AccessToken = GetAccessToken();
            List<VesselManagementTypeDetail> response = await _marineClient.PostGetVesselManagementSummary(vesselId);

            VesselManagementTypeDetail obj = response.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.AccountingCompanyId) && x.AccountingCompanyId.Equals(coyId));

            int year = currentYear;

            if (obj != null)
            {
                year = obj.ManagementStartDate.HasValue ? obj.ManagementStartDate.Value.Year < previousTenYears ? previousTenYears : obj.ManagementStartDate.Value.Year : previousTenYears;
            }

            for (int yearCounter = currentYear; yearCounter >= year; yearCounter--)
            {
                LookUp yearObj = new LookUp();
                yearObj.Identifier = yearCounter.ToString();
                yearObj.Description = yearCounter.ToString();
                YearList.Add(yearObj);
            }

            return YearList;
        }

        /// <summary>
        /// Gets the account code list paged.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="q">The q.</param>
        /// <param name="_type">The type.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task<JsonResult> GetAccountCodeListPaged(string term, string q, string _type, int page)
        {
            _financeClient.AccessToken = GetAccessToken();

            ChartAccountDetailRequest request = new ChartAccountDetailRequest();
            request.SearchParameter = term;
            request.ChartHeaderId = "GLAS00000164";
            request.ChdPosting = "P";

            DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
            pageRequest.Length = 100;
            pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
            pageRequest.Columns = new List<Column>();
            pageRequest.Columns.Add(new Column() { Name = "AccountCodeId" });
            pageRequest.Columns.Add(new Column() { Name = "AccountCode" });
            pageRequest.Columns.Add(new Column() { Name = "Currency" });
            pageRequest.Columns.Add(new Column() { Name = "AccountType" });

            pageRequest.Order = new List<Order>();
            pageRequest.Order.Add(new Order()
            {
                Column = 0,
                Dir = "asc"
            });
            Select2ResponseViewModel<List<ChartAccountDetailResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<ChartAccountDetailResponseViewModel>>();
            select2ResponseViewModel.Results = new List<ChartAccountDetailResponseViewModel>();
            DataTablePageResponse<List<ChartAccountDetailResponseViewModel>> response = new DataTablePageResponse<List<ChartAccountDetailResponseViewModel>>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                response = await _financeClient.GetAccountCodeListPaged(pageRequest, request);
            }
            select2ResponseViewModel.Results = response.Data;
            select2ResponseViewModel.Pagination = new Pagination();
            select2ResponseViewModel.Pagination.More = response.RecordsTotal > (pageRequest.Length * page);

            return new JsonResult(select2ResponseViewModel);
        }

        #region Document Download - Transaction


        /// <summary>
        /// Gets the document list.
        /// </summary>
        /// <param name="invoiceDocumentId">The invoice document identifier.</param>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="voucherNo">The voucher no.</param>
        /// <param name="documentCount">The document count.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDocumentList(string invoiceDocumentId, string accountingCompanyId, string voucherNo, int documentCount)
        {
            List<CloudDocumentSearchResponseViewModel> VoucherDocuments = new List<CloudDocumentSearchResponseViewModel>();

            if (!string.IsNullOrEmpty(invoiceDocumentId))
            {
                VoucherDocuments.Add(new CloudDocumentSearchResponseViewModel()
                {
                    CloudDocumentIdentifier = invoiceDocumentId,
                    DocumentDescription = "",
                    MatchedId = ""
                });
            }
            else if (documentCount > 0)
            {
                VoucherDocuments = await GetVoucherDocuments(accountingCompanyId, voucherNo);
            }

            return new JsonResult(new { data = VoucherDocuments });
        }

        /// <summary>
        /// Gets the voucher documents.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="voucherNo">The voucher no.</param>
        /// <returns></returns>
        private async Task<List<CloudDocumentSearchResponseViewModel>> GetVoucherDocuments(string accountingCompanyId, string voucherNo)
        {
            List<CloudDocumentSearchResponseViewModel> voucherDocs = new List<CloudDocumentSearchResponseViewModel>();
            List<CloudDocumentSearchResponseViewModel> cloudDocumentList = new List<CloudDocumentSearchResponseViewModel>();

            cloudDocumentList = await GetCloudDocumentList(accountingCompanyId, voucherNo);

            List<InvoiceDocumentResponseViewModel> invDocuments = await GetDocumentsForOldSystemVoucher(accountingCompanyId, voucherNo);

            if (cloudDocumentList != null && cloudDocumentList.Any())
            {
                voucherDocs.AddRange(cloudDocumentList);
            }
            if (invDocuments != null && invDocuments.Any())
            {
                foreach (InvoiceDocumentResponseViewModel doc in invDocuments)
                {
                    voucherDocs.Add(new CloudDocumentSearchResponseViewModel()
                    {
                        CloudDocumentIdentifier = doc.InvoiceDocumentId,
                        CoyId = doc.AccountingCompanyId,
                        DocumentDescription = "",
                        MatchedId = ""
                    });
                }
            }

            return voucherDocs;
        }

        /// <summary>
        /// Gets the cloud document list.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="voucherNo">The voucher no.</param>
        /// <returns></returns>
        private async Task<List<CloudDocumentSearchResponseViewModel>> GetCloudDocumentList(string accountingCompanyId, string voucherNo)
        {
            //CloudDocumentSearch -- SS Name
            _sharedClient.AccessToken = GetAccessToken();
            List<CloudDocumentSearchResponseViewModel> cloudDocList = new List<CloudDocumentSearchResponseViewModel>();
            CloudDocumentSearchRequest request = new CloudDocumentSearchRequest();
            request.MatchedIdentifiers = new List<string> { voucherNo };
            request.DocumentCategories = new List<DocumentCategory>() { DocumentCategory.GeneralLedgerTransaction };
            request.Status = CloudUploadStatus.InCloud;
            request.CloudDocumentFilterField = EnumsHelper.GetKeyValue(CloudDocumentField.CoyId);
            request.FieldValue = accountingCompanyId;
            request.FieldInfo = new FieldInfo(CloudDocumentField.CoyId);
            cloudDocList = await _sharedClient.PostCloudDocumentSearch(request);
            return cloudDocList;
        }

        /// <summary>
        /// Gets the documents for old system voucher.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="voucherNo">The voucher no.</param>
        /// <returns></returns>
        private async Task<List<InvoiceDocumentResponseViewModel>> GetDocumentsForOldSystemVoucher(string accountingCompanyId, string voucherNo)
        {
            //GetInvDocByCoyIdAndVoucherNo -- SS name
            _financeClient.AccessToken = GetAccessToken();
            List<InvoiceDocumentResponseViewModel> documents = new List<InvoiceDocumentResponseViewModel>();
            documents = await _financeClient.PostGetInvDocByCoyIdAndVoucherNo(accountingCompanyId, voucherNo);
            return documents;
        }

        /// <summary>
        /// Downloads the document.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadDocument(string input)
        {
            _documentClient.AccessToken = GetAccessToken();
            CloudDocumentRequestViewModel request = Newtonsoft.Json.JsonConvert.DeserializeObject<CloudDocumentRequestViewModel>(input);
            if (!string.IsNullOrWhiteSpace(request.MatchedId))
            {
                //New Cloud document download
                //SS - GetCloudEntityDocument 
                CloudDocumentDownloadRequest inputRequest = new CloudDocumentDownloadRequest
                {
                    DocumentCategory = request.DocumentCategory.GetValueOrDefault(),
                    Identifier = request.CloudDocumentIdentifier,
                    FileName = (request.DocumentSizeInBytes == 0) ? request.DocumentFilename : null,
                    DocumentFileType = request.FileType.GetValueOrDefault()

                };
                Stream result = await _documentClient.DownloadDocument(inputRequest);

                byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
                string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
                return new JsonResult(new { filename = request.DocumentDescription, bytes = byteString, fileType = EnumsHelper.GetDescription(request.FileType.GetValueOrDefault()) });
            }
            else
            {
                //Old system
                //SS -> GetInvoiceDocument;
                //DownloadInvoiceDocument
                if (!string.IsNullOrWhiteSpace(request.CloudDocumentIdentifier))
                {
                    DownloadResponseViewModel result = await _documentClient.DownloadInvoiceDocument(request.CloudDocumentIdentifier);
                    if (result != null)
                    {
                        byte[] byteData = (result != null && result.DocumentStream != null) ? CommonUtil.ConvertStreamToByte(result.DocumentStream) : null;
                        string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
                        return new JsonResult(new { filename = request.CloudDocumentIdentifier, bytes = byteString, fileType = result.MediaType });
                    }
                }
                return new JsonResult(new { filename = request.CloudDocumentIdentifier, bytes = "" });
            }
        }

        #endregion
        /// <summary>
        /// Prints the sire report asynchronous.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<JsonResult> ExportToExcelReport(OperationCostDrillDownViewModel input)
        {
            _sharedClient.AccessToken = GetAccessToken();
            _financeClient.AccessToken = GetAccessToken();

            ReportLight reportRequest = new ReportLight();
            DateTime endOfMonth = new DateTime(Convert.ToInt32(input.SelectedYear), Convert.ToInt32(input.SelectedMonth), 1).AddMonths(1).AddDays(-1);

            var responceCompanyDetails = await _financeClient.GetSelectedAccountingCompanyDetails(input.CoyId);
            bool IsPOReceivedOnly = responceCompanyDetails.IsPOReceivedOnly;

            if (IsPOReceivedOnly)
            {
                reportRequest = await _sharedClient.GetReportLightByFilename(EnumsHelper.GetKeyValue(ReportMaster.FinanceOperationalCostDrillDownInternalExcelReport));
            }
            else
            {
                reportRequest = await _sharedClient.GetReportLightByFilename(EnumsHelper.GetKeyValue(ReportMaster.FinanceOperationalCostDrillDownPeriodExcelReport));
            }

            reportRequest.ReportFormat = ReportExportTypes.Excel;

            if (reportRequest != null)
            {
                foreach (var p in reportRequest.ReportParameters)
                {
                    if (p.ParameterName.Contains("@COY_ID")) { p.ValueToSet = new List<object>() { input.CoyId }; }
                    if (p.ParameterName.Contains("@ChartType")) { p.ValueToSet = new List<object>() { input.ReportDefinitionType.ToString() }; }
                    if (p.ParameterName.Contains("@GenerationDate")) { p.ValueToSet = new List<object>() { endOfMonth }; }
                }

                var reportRequestId = await _sharedClient.InitiateReportRequest(reportRequest);

                if (reportRequestId != null && reportRequestId != string.Empty)
                {
                    return new JsonResult(new { message = Messages.ReportGenerationSuccessMessage, success = true });
                }
                else
                {
                    return new JsonResult(new { message = Messages.RequestFailureMessage, success = false });
                }
            }
            else
            {
                return new JsonResult(new { message = Messages.NoDetailsFound, success = false });
            }
        }

        /// <summary>
        /// Gets the transaction details.
        /// </summary>
        /// <param name="TransactionCostRequestUrl">TransactionCostRequestUrl.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVeselTransactionDetails(string TransactionCostRequestUrl)
        {
            _financeClient.AccessToken = GetAccessToken();

            GLTransactionFilter searchFilter = new GLTransactionFilter();
            string data = _provider.CreateProtector("TransactionCostRequestUrl").Unprotect(TransactionCostRequestUrl);
            searchFilter = Newtonsoft.Json.JsonConvert.DeserializeObject<GLTransactionFilter>(data);

            List<VesselCostAnalysisTransactionResponseViewModel> response = await _financeClient.GetVesselCostAnalysisTransactions(searchFilter);

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the transaction details export to excel.
        /// </summary>
        /// <param name="TransactionCostRequestUrl">The transaction cost request URL.</param>
        /// <param name="CurrencyForExportToExcel">The currency for export to excel.</param>
        /// <param name="CurrentStageTitle">The current stage title.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVeselTransactionDetailsExportToExcel(string TransactionCostRequestUrl, string CurrencyForExportToExcel, string CurrentStageTitle)
        {
            _financeClient.AccessToken = GetAccessToken();
            GLTransactionFilter searchFilter = new GLTransactionFilter();
            string data = _provider.CreateProtector("TransactionCostRequestUrl").Unprotect(TransactionCostRequestUrl);
            searchFilter = Newtonsoft.Json.JsonConvert.DeserializeObject<GLTransactionFilter>(data);
            List<VesselCostAnalysisTransactionResponse> response = await _financeClient.GetVesselCostAnalysisTransactionsExportToExcel(searchFilter);

            ExportToExcelRequest request = new ExportToExcelRequest();

            request.FileName = "Transaction Details";
            request.Title = "Transaction Details";
            request.Summary = "Vessel : " + searchFilter.VesselName + "\nDate : " + DateTime.Now.ToString(Constants.DateFormat) + "\nAccount : " + CurrentStageTitle + "\nBudget : " + searchFilter.Budget + "\nAccrual : " + searchFilter.Accurals + "\nActual : " + searchFilter.Actual + "\nTotal : " + searchFilter.Total + "\nVariance : " + searchFilter.Variance + "\nCurrency : " + CurrencyForExportToExcel;
            request.SummaryRowCount = 9;
            request.ColumnCount = 7;
            if (response != null && response.Any())
            {
                List<VesselCostAnalysisTransactionResponseExportViewModel> result = new List<VesselCostAnalysisTransactionResponseExportViewModel>();

                response.ForEach(x =>
                    result.Add(new VesselCostAnalysisTransactionResponseExportViewModel
                    {
                        VoucherNo = x.Voucher,
                        Type = x.Type,
                        TransactionDate = x.TransactionDate.ToString(Constants.DateFormat),
                        Currency = x.Currency,
                        Amount = string.Format(Constants.TwoDecimal_NumberFormat, x.Amount),
                        AmountBase = string.Format(Constants.TwoDecimal_NumberFormat, x.AmountBase),
                        Order = x.Order
                    })
                );

                return ExportToExcel<VesselCostAnalysisTransactionResponseExportViewModel>(result, request);
            }
            return new JsonResult(new { status = "Ok" });
        }

        #region Genral Ledger - Details Account Balances
        /// <summary>
        /// Generals the ledger.
        /// </summary>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GeneralLedger(string VesselId, bool IsVesselChanged)
        {
            _financeClient.AccessToken = GetAccessToken();
            GeneralLedgerViewModel generalLedgerViewModel = new GeneralLedgerViewModel();
            DateTime todayDate = DateTime.Today;
            string decreptedNewVessel = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            generalLedgerViewModel.VesselId = VesselId;
            generalLedgerViewModel.VesselName = decreptedNewVessel.Split(Constants.Separator)[1];
            generalLedgerViewModel.CoyId = decreptedNewVessel.Split(Constants.Separator)[2];

            if (IsVesselChanged)
            {
                var FinanceListData = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey));
                string financeListData = _provider.CreateProtector("OperationCostURL").Unprotect(FinanceListData);
                var financeListVM = Newtonsoft.Json.JsonConvert.DeserializeObject<OperatingCostBarChartRequest>(financeListData);

                OperatingCostBarChartRequest request = new OperatingCostBarChartRequest();
                request.AccountId = null;
                request.AccountLevel = -1;
                request.CoyId = decreptedNewVessel.Split(Constants.Separator)[2];
                request.ReportDefinitionType = ReportDefinitionType.S;
                request.MonthsDuration = Constants.VarianceMonthsDuration;
                request.VariancePercentageFirstXMonthLimit = Constants.VariancePercentageFirstXMonthLimit;
                request.VariancecPercentageSecondXMonthLimit = Constants.VariancecPercentageSecondXMonthLimit;
                request.ToDate = financeListVM.ToDate;
                string operationCostURL = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));

                return RedirectToAction("List", new { OperationCostRequestUrl = operationCostURL, VesselId = VesselId, IsVesselChanged = IsVesselChanged });
            }
            var response = await _financeClient.GetSelectedAccountingCompanyDetails(generalLedgerViewModel.CoyId);
            generalLedgerViewModel.BaseCoyCurr = response.BaseCoyCurr;
            generalLedgerViewModel.FromDate = response.CoyFinYearStart;
            generalLedgerViewModel.ToDate = todayDate;
            generalLedgerViewModel.ChhId = response.ChhId;
            generalLedgerViewModel.MinStartDate = new DateTime(1998, response != null && response.CoyFinYearStart.HasValue ? response.CoyFinYearStart.Value.Month : 1, response != null && response.CoyFinYearStart.HasValue ? response.CoyFinYearStart.Value.Day : 1);
            generalLedgerViewModel.MaxEndDate = DateTime.Now.AddMonths(18);

            generalLedgerViewModel.CoyFinStartDate = response.CoyFinYearStart;
            generalLedgerViewModel.CoyFinEndDate = response.CoyFinYearEnd;

            generalLedgerViewModel.finStartDate = response.CoyFinYearStart != null && response.CoyFinYearStart.HasValue ? response.CoyFinYearStart.Value.Day : 1;
            generalLedgerViewModel.finEndDate = response.CoyFinYearEnd != null && response.CoyFinYearEnd.HasValue ? response.CoyFinYearEnd.Value.Day : 1;

            generalLedgerViewModel.finStartMonth = response.CoyFinYearStart != null && response.CoyFinYearStart.HasValue ? response.CoyFinYearStart.Value.Month : 1;
            generalLedgerViewModel.finEndMonth = response.CoyFinYearEnd != null && response.CoyFinYearEnd.HasValue ? response.CoyFinYearEnd.Value.Month : 12;
            generalLedgerViewModel.VesselManagementOfficeType = response.VesselManagementOfficeType;

            var generalLedgerData = _provider.CreateProtector("FinanceGeneralLedger").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(generalLedgerViewModel));

            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.FinanceGeneralLedgerPageKey);
            SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey), generalLedgerData);

            var SessionData = GetSessionFilter(pageKey);
            string FinanceGeneralLedgerData = _provider.CreateProtector("FinanceGeneralLedger").Unprotect(SessionData);
            var deserializeVM = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralLedgerViewModel>(FinanceGeneralLedgerData);

            generalLedgerViewModel.FromDate = deserializeVM.FromDate;
            generalLedgerViewModel.ToDate = deserializeVM.ToDate;
            generalLedgerViewModel.AccountId = deserializeVM.AccountId;
            generalLedgerViewModel.AccountName = deserializeVM.AccountName;
            generalLedgerViewModel.FinancialYearStartDate = deserializeVM.FinancialYearStartDate;
            generalLedgerViewModel.ChhId = deserializeVM.ChhId;
            generalLedgerViewModel.BaseCoyCurr = deserializeVM.BaseCoyCurr;
            generalLedgerViewModel.finPeriod = deserializeVM.finPeriod;

            generalLedgerViewModel.ActiveMobileTabClass = SetTab(pageKey, generalLedgerViewModel.ActiveMobileTabClass, Constants.Tab1);
            return View(generalLedgerViewModel);
        }

        /// <summary>
        /// Generals the summary details.
        /// </summary>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSummaryDetails(string VesselId)
        {
            _financeClient.AccessToken = GetAccessToken();
            string decreptedNewVessel = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            string CoyId = decreptedNewVessel.Split(Constants.Separator)[2];
            var response = await _financeClient.GetAccCompanyAdditionalInfo(CoyId);

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the transaction details.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="TransactionCostRequestUrl">The transaction cost request URL.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetAccountBalanceForAccCompany(GLTransactionFilter input)
        {
            _financeClient.AccessToken = GetAccessToken();
            GLTransactionFilter searchFilter = GetRequestObjectForGeneralLedgerList(input);

            List<AccountBalanceDetailViewModel> response = await _financeClient.PostGetAccountBalanceForAccCompany(searchFilter);

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the request object for general ledger list.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [NonAction]
        private GLTransactionFilter GetRequestObjectForGeneralLedgerList(GLTransactionFilter input)
        {
            GLTransactionFilter searchFilter = new GLTransactionFilter();

            string decreptedNewVessel = _provider.CreateProtector("Vessel").Unprotect(input.VesselId);
            searchFilter.VesselId = decreptedNewVessel.Split(Constants.Separator)[0];
            searchFilter.AccountingCompanyId = decreptedNewVessel.Split(Constants.Separator)[2];
            searchFilter.FromDate = input.FromDate;
            searchFilter.ToDate = input.ToDate;
            searchFilter.CoyType = EnumsHelper.GetKeyValue(VesselEntityType.Vessel);
            searchFilter.AccountCode = input.AccountCode;
            searchFilter.BaseCoyCurr = input.BaseCoyCurr;
            searchFilter.AccountingCompanyChhId = input.AccountingCompanyChhId;
            searchFilter.FinancialYearStartDate = input.FinancialYearStartDate;
            return searchFilter;
        }

        /// <summary>
        /// Get the financial years
        /// </summary>
        /// <param name="accountingCompanyId"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetFinancialYears(string accountingCompanyId)
        {
            _financeClient.AccessToken = GetAccessToken();
            GLTransactionFilter searchFilter = new GLTransactionFilter();
            var response = await _financeClient.GetSelectedAccountingCompanyDetails(accountingCompanyId);

            return new JsonResult(new { response.AccountingFinancialYears });
        }

        /// <summary>
		/// Gets the selected account code.
		/// </summary>
		/// <param name="inputText">The input text.</param>
		/// <param name="accountId">The account identifier.</param>
		/// <returns></returns>
		public async Task<ActionResult> GetSelectedAccountCode(string inputText, string accountId, string ChhId)
        {
            _financeClient.AccessToken = GetAccessToken();
            List<ChartAccountDetailResponseViewModel> response = null;
            ChartAccountDetailRequest request = new ChartAccountDetailRequest();
            ChartAccountDetailResponseViewModel SelectedCompany = null;

            request.SearchParameter = inputText;
            request.ChartHeaderId = ChhId;
            request.IsSearchDescriptionWithContains = true;
            request.ChdPosting = EnumsHelper.GetKeyValue(AccountType.Posting);

            if (!string.IsNullOrWhiteSpace(inputText))
            {
                response = await _financeClient.GetAccountCodes(request);

                if (response != null && response.Any())
                {
                    SelectedCompany = response.Where(x => x.AccountId == accountId).FirstOrDefault();
                }
            }

            return new JsonResult(SelectedCompany);
        }

        /// <summary>
        /// Gets the account code list paged.
        /// </summary>
        /// <param name="inputText">The inputText.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task<JsonResult> GetAccountCodesListPaged(string inputText, int page, string ChhId)
        {
            _financeClient.AccessToken = GetAccessToken();
            DataTablePageResponse<List<ChartAccountDetailResponseViewModel>> response = new DataTablePageResponse<List<ChartAccountDetailResponseViewModel>>();

            ChartAccountDetailRequest request = new ChartAccountDetailRequest();
            request.SearchParameter = inputText;
            request.ChartHeaderId = ChhId;
            request.IsSearchDescriptionWithContains = true;
            request.ChdPosting = EnumsHelper.GetKeyValue(AccountType.Posting);

            DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
            pageRequest.Length = 10;
            pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
            pageRequest.Columns = new List<Column>();
            pageRequest.Columns.Add(new Column() { Name = "AccountCodeId" });

            pageRequest.Order = new List<Order>();
            pageRequest.Order.Add(new Order()
            {
                Column = 0,
                Dir = "asc"
            });

            if (!string.IsNullOrWhiteSpace(inputText))
            {
                response = await _financeClient.GetAccountCodeListPaged(pageRequest, request);
            }

            return new JsonResult(new { data = response.Data });
        }

        /// <summary>
        /// Exports to excel general ledger.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> ExportToExcelGeneralLedger(GeneralLedgerExportRequest input)
        {
            _financeClient.AccessToken = GetAccessToken();
            GLTransactionFilter filter = new GLTransactionFilter
            {
                AccountCode = input.AccountCode,
                AccountingCompanyChhId = input.AccountingCompanyChhId,
                FinancialYearStartDate = input.FinancialYearStartDate,
                FromDate = input.FromDate,
                ToDate = input.ToDate,
                VesselId = input.VesselId,
                BaseCoyCurr = input.BaseCoyCurr
            };

            GLTransactionFilter searchFilter = GetRequestObjectForGeneralLedgerList(filter);

            if(input != null)
            {
                input.VesselName = CommonUtil.GetVesselDisplayName(_provider,input.VesselId);
            }

            List<AccountBalanceDetailExportViewModel> response = await _financeClient.ExportToExcelGeneralLedgerList(searchFilter);

            ExportToExcelRequest request = new ExportToExcelRequest();
            request.FileName = "General Ledger";
            request.Title = "General Ledger";
            string summary = "Vessel : " + input.VesselName;
            int summaryRowCount = 1;

            summary += "\nFrom Date : " + input.FromDate.ToString("dd MMM yyyy");
            summaryRowCount++;
            summary += "\nTo Date : " + input.ToDate.ToString("dd MMM yyyy");
            summaryRowCount++;
            if (!string.IsNullOrWhiteSpace(input.AccountName))
            {
                summary += "\nAccount Name : " + input.AccountName;
                summaryRowCount++;
            }


            request.Summary = summary;
            request.SummaryRowCount = summaryRowCount + 2; //things were looking packed , hences added 2 extra for spacing
            request.ColumnCount = typeof(AccountBalanceDetailExportViewModel).GetProperties().Count();

            return ExportToExcel(response, request);
        }


        #endregion

        #region General Ledger - Transaction

        /// <summary>
        /// Generals the ledger transaction.
        /// </summary>
        /// <param name="transactionURL">The transaction URL.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsNavigatedFromAccountList">if set to <c>true</c> [is navigated from account list].</param>
        /// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <returns></returns>
        public async Task<IActionResult> GeneralLedgerTransaction(string transactionURL, string VesselId, bool IsVesselChanged)
        {
            _financeClient.AccessToken = GetAccessToken();
            GLTransactionViewModel transactionVM = new GLTransactionViewModel();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            string coyId = decreptedString.Split(Constants.Separator)[2];

            if (IsVesselChanged)
            {
                var FinanceListData = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey));
                string financeListData = _provider.CreateProtector("OperationCostURL").Unprotect(FinanceListData);
                var financeListVM = Newtonsoft.Json.JsonConvert.DeserializeObject<OperatingCostBarChartRequest>(financeListData);

                OperatingCostBarChartRequest request = new OperatingCostBarChartRequest();
                request.AccountId = null;
                request.AccountLevel = -1;
                request.CoyId = coyId;
                request.ReportDefinitionType = ReportDefinitionType.S;
                request.MonthsDuration = Constants.VarianceMonthsDuration;
                request.VariancePercentageFirstXMonthLimit = Constants.VariancePercentageFirstXMonthLimit;
                request.VariancecPercentageSecondXMonthLimit = Constants.VariancecPercentageSecondXMonthLimit;
                request.ToDate = financeListVM.ToDate;
                string operationCostURL = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                
                return RedirectToAction("List", new { OperationCostRequestUrl = operationCostURL, VesselId = VesselId, IsVesselChanged = IsVesselChanged });
            }

            if (!string.IsNullOrWhiteSpace(transactionURL))
            {
                string data = _provider.CreateProtector("GLTransaction").Unprotect(transactionURL);
                transactionVM = Newtonsoft.Json.JsonConvert.DeserializeObject<GLTransactionViewModel>(data);
            }
            
            var response = await _financeClient.GetSelectedAccountingCompanyDetails(coyId);

            transactionVM.BaseCoyCurr = response.BaseCoyCurr;
            transactionVM.EncryptedVesselId = VesselId;
            transactionVM.VesselName = decreptedString.Split(Constants.Separator)[1];

            transactionVM.MinStartDate = new DateTime(1998, response != null && response.CoyFinYearStart.HasValue ? response.CoyFinYearStart.Value.Month : 1, response != null && response.CoyFinYearStart.HasValue ? response.CoyFinYearStart.Value.Day : 1);
            transactionVM.MaxEndDate = DateTime.Now.AddMonths(18);

            transactionVM.CoyFinStartDate = response.CoyFinYearStart;
            transactionVM.CoyFinEndDate = response.CoyFinYearEnd;

            transactionVM.finStartDate = response.CoyFinYearStart != null && response.CoyFinYearStart.HasValue ? response.CoyFinYearStart.Value.Day : 1;
            transactionVM.finEndDate = response.CoyFinYearEnd != null && response.CoyFinYearEnd.HasValue ? response.CoyFinYearEnd.Value.Day : 1;

            transactionVM.finStartMonth = response.CoyFinYearStart != null && response.CoyFinYearStart.HasValue ? response.CoyFinYearStart.Value.Month : 1;
            transactionVM.finEndMonth = response.CoyFinYearEnd != null && response.CoyFinYearEnd.HasValue ? response.CoyFinYearEnd.Value.Month : 12;
            transactionVM.VesselManagementOfficeType = response.VesselManagementOfficeType;

            string generalLedgerTransaction = _provider.CreateProtector("GLTransaction").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(transactionVM));

            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.FinanceGeneralLedgerTransactionPageKey);
            SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey), generalLedgerTransaction);
            var SessionData = GetSessionFilter(pageKey);
            string transactionData = _provider.CreateProtector("GLTransaction").Unprotect(SessionData);
            var deserializeVM = Newtonsoft.Json.JsonConvert.DeserializeObject<GLTransactionViewModel>(transactionData);

            transactionVM.FromDate = deserializeVM.FromDate;
            transactionVM.ToDate = deserializeVM.ToDate;
            transactionVM.AccountCode = deserializeVM.AccountCode;
            transactionVM.AccountingCompanyId = deserializeVM.AccountingCompanyId;
            transactionVM.FinancialYearStartDate = deserializeVM.FinancialYearStartDate;
            transactionVM.ChhId = deserializeVM.ChhId;
            transactionVM.AccountName = deserializeVM.AccountName;
            transactionVM.finPeriod = deserializeVM.finPeriod;
            transactionVM.AccountNameDescription = deserializeVM.AccountNameDescription;

            transactionVM.ActiveMobileTabClass = SetTab(pageKey, transactionVM.ActiveMobileTabClass, Constants.Tab1);
            return View(transactionVM);
        }

        /// <summary>
        /// Gets the general ledger transaction list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetGeneralLedgerTransactionList(GLTransactionFilterViewModel filters)
        {
            _financeClient.AccessToken = GetAccessToken();
            List<CostAnalysisTransactionResponseViewModel> response = await _financeClient.GeneralLedgerTransactionList(filters);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Sets the general ledger transaction page parameter.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public IActionResult SetGeneralLedgerTransactionPageParameter(GLTransactionViewModel input)
        {
            if (input != null)
            {
                string generalLedgerTransaction = _provider.CreateProtector("GLTransaction").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
                SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceGeneralLedgerTransactionPageKey), generalLedgerTransaction, input.EncryptedVesselId);
                return new JsonResult(new { data = true });
            }
            else
            {
                return new JsonResult(new { data = false });
            }
        }

        /// <summary>
        /// Sets the general ledger page parameter.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public IActionResult SetGeneralLedgerPageParameter(GeneralLedgerViewModel input)
        {
            if (input != null)
            {
                string generalLedger = _provider.CreateProtector("FinanceGeneralLedger").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
                SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceGeneralLedgerPageKey), generalLedger, input.VesselId);

                return new JsonResult(new { data = true });
            }
            else
            {
                return new JsonResult(new { data = false });
            }
        }

        /// <summary>
        /// Gets the finance source URL.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns></returns>
        public IActionResult GetFinanceSourceURL(string pageKey)
        {
            if (!string.IsNullOrWhiteSpace(pageKey))
            {
               return GetSourceURL(pageKey);
            }
            else
            {
                //clearing the stack if the user is leaving the list page
                //HttpContext.Session.Remove(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceStackKey));
                return GetSourceURL(EnumsHelper.GetKeyValue(NavigationPageKey.FinanceListPageKey));
            }
        }
        #endregion
    }
}