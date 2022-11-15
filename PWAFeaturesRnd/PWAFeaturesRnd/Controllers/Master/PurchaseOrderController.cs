using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.DataAttributes;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.ExportToExcel;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.ExportToExcel;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.PurchaseOrder;
using PWAFeaturesRnd.ViewModels.Shared;

/// <summary>
/// 
/// </summary>
namespace PWAFeaturesRnd.Controllers.Master
{
	/// <summary>
	/// Purchase Order Controller
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
	public class PurchaseOrderController : AuthenticatedController
	{
		/// <summary>
		/// The client
		/// </summary>
		private readonly PurchasingClient _client;

		/// <summary>
		/// The finance client
		/// </summary>
		private readonly FinanceClient _financeClient;

		/// <summary>
		/// The shared client
		/// </summary>
		private readonly SharedClient sharedClient;

		/// <summary>
		/// The provider
		/// </summary>
		private IDataProtectionProvider _provider;

		/// <summary>
		/// The DocumentClient
		/// </summary>
		private DocumentClient _documentClient;

		/// <summary>
		/// The notification client
		/// </summary>
		private NotificationClient _notificationClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="PurchaseOrderController" /> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="_financeRequestClient">The finance request client.</param>
		/// <param name="_sharedClient">The shared client.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="documentClient">The document client.</param>
		/// <param name="notificationClient">The notification client.</param>
		public PurchaseOrderController(PurchasingClient client, FinanceClient _financeRequestClient, SharedClient _sharedClient, IDataProtectionProvider provider, DocumentClient documentClient, NotificationClient notificationClient)
		{
			_client = client;
			_provider = provider;
			_financeClient = _financeRequestClient;
			_documentClient = documentClient;
			sharedClient = _sharedClient;
			_notificationClient = notificationClient;
		}

		/// <summary>
		/// Lists this instance.
		/// </summary>
		/// <param name="purchaseOrderRequest">The purchase order request.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <returns></returns>
		public IActionResult List(string purchaseOrderRequest, string vesselId, bool IsVesselChanged)
		{
			string decreptedString = CommonUtil.GetDecryptedVessel(_provider, vesselId);

			PurchaseOrderRequestViewModel poRequestVM = GetPurchaseOrderViewModel(purchaseOrderRequest, vesselId, IsVesselChanged);
			string EncryptedPurchaseOrder = CommonUtil.GetEncryptedURL(_provider, Constants.PurchaseOrderEncryptionText, poRequestVM);

			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey);

			SetSessionDetail(pageKey, null, EncryptedPurchaseOrder);
			RemoveSessionFilter(_provider, pageKey, null, decreptedString.Split(Constants.Separator)[0]);

			var SessionData = GetSessionFilter(pageKey);
			poRequestVM = CommonUtil.GetDecryptedRequest<PurchaseOrderRequestViewModel>(_provider, Constants.PurchaseOrderEncryptionText, SessionData);
			poRequestVM.VesselId = vesselId;
			poRequestVM.VesselName = decreptedString.Split(Constants.Separator)[1];

			poRequestVM.ActiveMobileTabClass = SetTab(pageKey, poRequestVM.ActiveMobileTabClass, Constants.Tab2);
			return View(poRequestVM);
		}

		/// <summary>
		/// Gets the purchase order view model.
		/// </summary>
		/// <param name="purchaseOrderRequest">The purchase order request.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <returns>
		/// PurchaseOrderRequestViewModel
		/// </returns>
		[NonAction]
		public PurchaseOrderRequestViewModel GetPurchaseOrderViewModel(string purchaseOrderRequest, string vesselId, bool IsVesselChanged)
		{
			string decreptedString = CommonUtil.GetDecryptedVessel(_provider, vesselId);

			PurchaseOrderRequestViewModel poRequestVM = new PurchaseOrderRequestViewModel();
			poRequestVM = CommonUtil.GetDecryptedRequest<PurchaseOrderRequestViewModel>(_provider, Constants.PurchaseOrderEncryptionText, purchaseOrderRequest);
			poRequestVM.VesselId = vesselId;
			poRequestVM = GetPurchaseOrderDetails(poRequestVM);
			poRequestVM.VesselName = decreptedString.Split(Constants.Separator)[1];
			poRequestVM.AccountCompanyId = decreptedString.Split(Constants.Separator)[2];
			if (IsVesselChanged)
			{
				poRequestVM.PurchaseOrderTypes = null;
				poRequestVM.SearchOrderNumber = "";
				poRequestVM.Title = "";
				poRequestVM.SelectedOrderStage = null;
				poRequestVM.SelectedStatus = null;
				poRequestVM.IsSearchClicked = false;
				poRequestVM.FromDate = null;
				poRequestVM.ToDate = null;
				poRequestVM.SupplierId = null;
				poRequestVM.SupplierName = null;
			}
			return poRequestVM;
		}


		/// <summary>
		/// Sets the page parameter.
		/// </summary>
		/// <param name="poRequest">The PO request.</param>
		/// <returns></returns>
		public IActionResult SetPageParameter(PurchaseOrderRequestViewModel poRequest)
		{
			PurchaseOrderRequestViewModel request = new PurchaseOrderRequestViewModel();
			request.POStage = poRequest.POStage;
			request.VesselId = poRequest.VesselId;
			request.SearchOrderNumber = poRequest.SearchOrderNumber;
			request.PurchaseOrderTypes = poRequest.PurchaseOrderTypes;
			request.SelectedOrderStage = poRequest.SelectedOrderStage;
			request.ToDate = poRequest.ToDate;
			request.FromDate = poRequest.FromDate;
			request.IsSearchClicked = poRequest.IsSearchClicked;
			request.Title = poRequest.Title;
			request.SupplierName = poRequest.SupplierName;
			request.SupplierId = poRequest.SupplierId;
			request.SelectedStatus = poRequest.SelectedStatus;
			
			if (poRequest.OrderStatusIds != null && poRequest.OrderStatusIds.Any())
			{
				request.strOrderStatusIds = string.Join(',', poRequest.OrderStatusIds);
			}

			string purchaseOrderURL = CommonUtil.GetEncryptedURL(_provider, Constants.PurchaseOrderEncryptionText, request);

			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey), purchaseOrderURL, poRequest.VesselId);

			return new JsonResult(new { data = request });
		}

		/// <summary>
		/// Sets the summary filter in temporary data.
		/// </summary>
		/// <param name="purchaseOrderUrl">The purchase order URL.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <returns>
		/// PurchaseOrderRequestViewModel
		/// </returns>
		public IActionResult SetSummaryFilterInTempData(string purchaseOrderUrl, string vesselId)
		{
			string data = _provider.CreateProtector(Constants.PurchaseOrderEncryptionText).Unprotect(purchaseOrderUrl);
			PurchaseOrderRequestViewModel poRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseOrderRequestViewModel>(data);
			poRequestVM = GetPurchaseOrderDetails(poRequestVM);
			poRequestVM.VesselId = vesselId;

			string purchaseOrderEncrypted = CommonUtil.GetEncryptedURL(_provider, Constants.PurchaseOrderEncryptionText, poRequestVM);
			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey), purchaseOrderEncrypted, vesselId);

			return new JsonResult(new { data = poRequestVM });
		}

		/// <summary>
		/// Gets the order summary.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetOrderSummary(PurchasingFilter request)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedVesselId = _provider.CreateProtector("Vessel").Unprotect(request.VesselId);
			string vesId = decryptedVesselId.Split(Constants.Separator)[0];
			string coyId = decryptedVesselId.Split(Constants.Separator)[2];

			PurchaseOrderSummaryViewModel response = new PurchaseOrderSummaryViewModel();

			if (!string.IsNullOrWhiteSpace(vesId))
			{
				request.VesselId = vesId;
				request.CoyId = coyId;
				response = await _client.GetPurchaseOrderOrderCountByVessel(request);
			}
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the purchase order list.
		/// </summary>
		/// <param name="pageRequest">The page request.</param>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetPurchaseOrderList(DataTablePageRequest<string> pageRequest, PurchaseOrderRequestViewModel input)
		{
			List<PurchasingDetailViewModel> result = new List<PurchasingDetailViewModel>();
			_client.AccessToken = GetAccessToken();
			_notificationClient.AccessToken = GetAccessToken();

			if (input != null)
			{
				string vesselId = _provider.CreateProtector("Vessel").Unprotect(input.VesselId);
				input.VesselId = vesselId.Split(Constants.Separator)[0];
				input.VesselName = vesselId.Split(Constants.Separator)[1];
				input.AccountCompanyId = vesselId.Split(Constants.Separator)[2];
			}
			DataTablePageResponse<List<PurchasingDetailViewModel>> response = await _client.GetPurchaseOrderList(pageRequest, input);

			List<string> OrderIds = new List<string>();
			if (response.Data != null && response.Data.Any())
			{
				RecordDiscussionRequestViewModel request = new RecordDiscussionRequestViewModel();
				request.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.PurchaseOrder));
				request.ReferenceIds = response.Data.Select(x => x.OrderId).ToList();

				List<RecordDiscussionResponse> DiscussionAndNotesCountList = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(request);

				foreach (var item in DiscussionAndNotesCountList.Where(x => x.ChannelCount > 0 || x.NotesCount > 0))
				{
					foreach (var order in response.Data.Where(x => x.OrderId == item.ReferenceIdentifier))
					{
						NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
						{
							CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.PurchaseOrder)),
							ReferenceIdentifier = item.ReferenceIdentifier
						};

						order.ChannelCount = item.ChannelCount;
						order.NotesCount = item.NotesCount;
						order.MessageDetailsJSON = JsonConvert.SerializeObject(newMessageDetails);
					}
				}
			}

			return new JsonResult(new DataTablePageResponse<List<PurchasingDetailViewModel>>
			{
				Draw = pageRequest.Draw,
				RecordsFiltered = response.RecordsFiltered,
				Data = response.Data,
				RecordsTotal = response.RecordsTotal
			});
		}

		/// <summary>
		/// Exports to excel purchase order list.
		/// </summary>
		/// <param name="purchaseRequest">The purchase request.</param>
		/// <returns></returns>
		public async Task<IActionResult> ExportToExcelPurchaseOrderList(PurchaseOrderRequestViewModel purchaseRequest)
		{
			_client.AccessToken = GetAccessToken();
			if (purchaseRequest != null)
			{
				string vesselId = _provider.CreateProtector("Vessel").Unprotect(purchaseRequest.VesselId);
				purchaseRequest.VesselId = vesselId.Split(Constants.Separator)[0];
				purchaseRequest.VesselName = vesselId.Split(Constants.Separator)[1];
				purchaseRequest.AccountCompanyId = vesselId.Split(Constants.Separator)[2];
			}

			List<PurchaseOrderResponse> response = await _client.ExportToExcelPurchaseOrderList(purchaseRequest);

			ExportToExcelRequest request = new ExportToExcelRequest();

			request.FileName = "Purchase Order";
			request.Title = "Purchase Order";

			string summary = "Vessel : " + purchaseRequest.VesselName;
			int summaryRowCount = 1;

			string stageName = string.Empty;
			if (purchaseRequest.IsSearchClicked)
			{
				if (purchaseRequest.FromDate.HasValue)
				{
					summary += "\nFrom Date : " + purchaseRequest.FromDate.Value.ToString("dd MMM yyyy");
					summaryRowCount++;
				}

				if (purchaseRequest.ToDate.HasValue)
				{
					summary += "\nTo Date : " + purchaseRequest.ToDate.Value.ToString("dd MMM yyyy");
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(purchaseRequest.SearchOrderNumber))
				{
					summary += "\nOrder No. : " + purchaseRequest.SearchOrderNumber;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(purchaseRequest.Title))
				{
					summary += "\nOrder Name : " + purchaseRequest.Title;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(purchaseRequest.strOrderStatusIds))
				{
					List<string> orderStatuses = purchaseRequest.strOrderStatusIds.Split(",").ToList();
					orderStatuses = orderStatuses.Where(x => x != "" && x != Constants.All).Distinct().ToList();
					summary += "\nStatus : " + String.Join(",", orderStatuses);
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(purchaseRequest.SupplierName))
				{
					summary += "\nSupplier : " + purchaseRequest.SupplierName;
					summaryRowCount++;
				}
			}
			else
			{
				summary += "\n" + EnumsHelper.GetEnumNameFromKeyValue(typeof(PoStagesFilter), purchaseRequest.POStage) + " Orders";
				summaryRowCount++;
			}

			request.Summary = summary;
			request.SummaryRowCount = summaryRowCount;
			request.ColumnCount = 18;

			List<PurchaseOrderExportViewModel> result = new List<PurchaseOrderExportViewModel>();

			response.ForEach(x =>
				result.Add(new PurchaseOrderExportViewModel
				{
					OrderNumber = x.CoyId + " - " + x.OrderNumber,
					Name = x.OrderName ?? "",
					Status = x.StatusId + (x.IsFurtherOrderAuthorisationRequired ? ", AR" : ""),
					DateEntered = x.RequestedDate == null ? "" : x.RequestedDate.Value.ToString("dd MMM yyyy"),
					DateOrdered = x.OrderDate == null ? "" : x.OrderDate.Value.ToString("dd MMM yyyy"),
					ExpectedReceivedPort = (x.ExpectedReceivedPort ?? ""),
					ExpectedReceivedDate = (x.ExpectedReceivedDate == null ? "" : x.ExpectedReceivedDate.Value.ToString("dd MMM yyyy")),
					Supplier = x.SupplierName ?? "",
					Agent = x.AgentName ?? "",
					Warehouse = x.WarehouseName ?? "",
					IsHazMaterial = x.IsHazardousMaterial ? "Yes" : "No",
					TotalAmount = x.TotalAmount,
					AmountOS = x.OutstandingAmount,
					DamagedItems = x.IsDamagedItems ? "Yes" : "No",
					PoorQuality = x.IsPoorQuality ? "Yes" : "No",
					PoorPackaging = x.IsPoorPackaging ? "Yes" : "No",
					IncorrectItem = x.IsIncorrectItem ? "Yes" : "No",
					CertificateRecieved = x.IsCertificateRequired ? (x.IsCertificateReceived ? "Yes" : "No") : "N/A"
				})
			);

			return ExportToExcel<PurchaseOrderExportViewModel>(result, request);
		}

		/// <summary>
		/// Details the specified accounting company identifier.
		/// </summary>
		/// <param name="PurchaseOrderRequest">The purchase order request.</param>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public async Task<IActionResult> Detail(string PurchaseOrderRequest, string VesselId, bool IsVesselChanged, string context)
		{
			_client.AccessToken = GetAccessToken();
			PurchaseOrderRequestViewModel requestViewModel = new PurchaseOrderRequestViewModel();

			if (!String.IsNullOrWhiteSpace(PurchaseOrderRequest))
			{
				requestViewModel = CommonUtil.GetDecryptedRequest<PurchaseOrderRequestViewModel>(_provider, Constants.PurchaseOrderEncryptionText, PurchaseOrderRequest);
			}

			string decreptedString = CommonUtil.GetDecryptedVessel(_provider, VesselId);

			string newVesselId = decreptedString.Split(Constants.Separator)[0];
			requestViewModel.VesselName = decreptedString.Split(Constants.Separator)[1];
			requestViewModel.AccountCompanyId = decreptedString.Split(Constants.Separator)[2];

			if (!string.IsNullOrWhiteSpace(context))
			{
				ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
				requestViewModel.AccountCompanyId = contextParameter.CoyId;
				requestViewModel.OrderNumber = contextParameter.OrderNo;
				requestViewModel.VesselId = newVesselId;
				PurchaseOrderRequest = CommonUtil.GetEncryptedURL(_provider, Constants.PurchaseOrderEncryptionText, requestViewModel);
			}

			if (requestViewModel.VesselId != newVesselId || IsVesselChanged)
			{
				return RedirectToAction("List", new { purchaseOrderRequest = PurchaseOrderRequest, VesselId = VesselId });
			}

			PurchaseOrderDetailViewModel response = await _client.GetPurchaseOrderHeaderDetailByCoyIdandOrderNo(requestViewModel.AccountCompanyId, requestViewModel.OrderNumber);
			response.PurchaseOrderRequest = PurchaseOrderRequest;
			response.PurchaseOrderVesselId = VesselId;
			var PurchaseOrderEncrypted = CommonUtil.GetEncryptedURL(_provider, Constants.PurchaseOrderEncryptionText, response);
			string[] contextParams = { requestViewModel.AccountCompanyId, requestViewModel.OrderNumber };
			string[] messageParams = { requestViewModel.AccountCompanyId, requestViewModel.OrderNumber };

			response.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.PurchaseOrder, response.VesselId, response.VesselName, contextParams, messageParams, response.OrderId);
			response.IsFromViewRecord = IsFromViewRecordVal(context);

			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementDetailPageKey);
			SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey), PurchaseOrderEncrypted);

			response.ActiveMobileTabClass = SetTab(pageKey, requestViewModel.ActiveMobileTabClass, Constants.DropdownTab1);

			return View(response);
		}

		/// <summary>
		/// Gets the order lines.
		/// </summary>
		/// <param name="pageRequest">The page request.</param>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetOrderLines(DataTablePageRequest<string> pageRequest, string accountingCompanyId, string orderNumber)
		{
			_client.AccessToken = GetAccessToken();

			string dataOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			string dataAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);

			DataTablePageResponse<List<OrderLineViewModel>> response = await _client.GetOrderLines(pageRequest, dataAccountingCompanyId, dataOrderNumber);

			return new JsonResult(new DataTablePageResponse<List<OrderLineViewModel>>
			{
				Draw = pageRequest.Draw,
				RecordsFiltered = response.RecordsFiltered,
				Data = response.Data,
				RecordsTotal = response.RecordsTotal
			});
		}

		/// <summary>
		/// Posts the get open orders by vessel ids.
		/// </summary>
		/// <param name="userMenuItem">The user menu item.</param>
		/// <returns></returns>
		public async Task<ActionResult> PostGetOpenOrdersByVesselIds(UserMenuItem userMenuItem)
		{
			OpenOrderSummaryViewModel result = new OpenOrderSummaryViewModel();
			_client.AccessToken = GetAccessToken();

			result = await _client.GetOpenOrdersByVesselIds(userMenuItem);

			return new JsonResult(result);
		}

		/// <summary>
		/// Posts the get suppliers.
		/// </summary>
		/// <param name="pageRequest">The page request.</param>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetSuppliers(DataTablePageRequest<string> pageRequest, SupplierPurchaseOrderRequest request)
		{
			_client.AccessToken = GetAccessToken();

			request.OrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(request.OrderNumber);
			request.AccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(request.AccountingCompanyId);

			DataTablePageResponse<List<SupplierDetailViewModel>> response = await _client.PostGetSuppliers(pageRequest, request);

			return new JsonResult(new DataTablePageResponse<List<SupplierDetailViewModel>>
			{
				Draw = pageRequest.Draw,
				RecordsFiltered = response.RecordsFiltered,
				Data = response.Data,
				RecordsTotal = response.RecordsTotal
			});
		}

		/// <summary>
		/// Views the quote.
		/// </summary>
		/// <param name="PurchaseOrderRequest">The purchase order request.</param>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <param name="supplierOrderId">The supplier order identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <returns></returns>
		public async Task<IActionResult> ViewQuote(string PurchaseOrderRequest, string VesselId, string supplierOrderId, bool IsVesselChanged)
		{
			_client.AccessToken = GetAccessToken();

			PurchaseOrderRequestViewModel requestViewModel = new PurchaseOrderRequestViewModel();
			string purchaseOrderData = _provider.CreateProtector(Constants.PurchaseOrderEncryptionText).Unprotect(PurchaseOrderRequest);
			requestViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseOrderRequestViewModel>(purchaseOrderData);
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
			string newVesselId = decreptedString.Split(Constants.Separator)[0];
			requestViewModel.VesselName = decreptedString.Split(Constants.Separator)[1];
			requestViewModel.AccountCompanyId = decreptedString.Split(Constants.Separator)[2];
			if (requestViewModel.VesselId != newVesselId || IsVesselChanged)
			{
				return RedirectToAction("List", new { purchaseOrderRequest = PurchaseOrderRequest, VesselId = VesselId });
			}
			string data = _provider.CreateProtector("SupplierOrderId").Unprotect(supplierOrderId);
			ViewQuoteHeaderViewModel response = await _client.PostGetSupplierQuoteHeader(data);
			if (!string.IsNullOrWhiteSpace(response.SparePartTypeId))
			{
				List<SparePartTypeViewModel> sparePartTypeList = await _client.GetSparePartTypeList();
				if (sparePartTypeList != null)
				{
					SparePartTypeViewModel sparePartType = sparePartTypeList.Where(x => x.SptId == response.SparePartTypeId).FirstOrDefault();
					if (sparePartType != null)
					{
						response.SparePartTypeCode = sparePartType.SptCode;
						response.SparePartName = sparePartType.SptName;
					}
				}
			}

			PurchaseOrderRequestViewModel purchaseRequestVM = new PurchaseOrderRequestViewModel();
			purchaseRequestVM.OrderNumber = response.OrderNumber;
			purchaseRequestVM.AccountCompanyId = decreptedString.Split(Constants.Separator)[2];
			purchaseRequestVM.VesselId = decreptedString.Split(Constants.Separator)[0];
			purchaseRequestVM.VesselName = decreptedString.Split(Constants.Separator)[1];

			string encryptedPO = _provider.CreateProtector(Constants.PurchaseOrderEncryptionText).Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseRequestVM));
			response.ViewQuoteURL = encryptedPO;
			response.EncryptedVesselId = VesselId;

			string EncryptedPurchaseOrder = CommonUtil.GetEncryptedURL(_provider, Constants.ViewQuoteHeaderEncryptionText, response);
			SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementViewQuotePageKey), EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey), EncryptedPurchaseOrder);

			return View(response);
		}

		/// <summary>
		/// Posts the get supplier quote order lines.
		/// </summary>
		/// <param name="supplierOrderId">The supplier order identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetSupplierQuoteOrderLines(string supplierOrderId)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedSupplierOrderId = _provider.CreateProtector("SupplierOrderId").Unprotect(supplierOrderId);
			List<ViewQuoteOrderLineViewModel> response = await _client.PostGetSupplierQuoteOrderLineQuote(decryptedSupplierOrderId, OrderLineQuoteType.All);

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Posts the get supplier quote order lines un quoted.
		/// </summary>
		/// <param name="supplierOrderId">The supplier order identifier.</param>
		/// <returns>
		/// List of ViewQuoteOrderLineViewModel
		/// </returns>
		public async Task<IActionResult> PostGetSupplierQuoteOrderLinesUnQuoted(string supplierOrderId)
		{
			_client.AccessToken = GetAccessToken();
			List<ViewQuoteOrderLineViewModel> response = await _client.PostGetSupplierQuoteOrderLineQuote(supplierOrderId, OrderLineQuoteType.UnQuoted);
			return new JsonResult(new { data = response });
		}


		/// <summary>
		/// Posts the get budget tab header details.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <param name="accountCode">The account code.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetBudgetTabHeaderDetails(string accountingCompanyId, string orderNumber, string accountCode)
		{
			_financeClient.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);

			CompanyVesselBudgetViewModel response = new CompanyVesselBudgetViewModel();

			if (!string.IsNullOrWhiteSpace(accountCode))
			{
				response = await _financeClient.PostGetVesselAccountBudgetDetail(decryptedAccountingCompanyId, decryptedOrderNumber, accountCode);
			}
			return new JsonResult(response);

		}

		/// <summary>
		/// Posts the get budget details list.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetBudgetDetailsList(BudgetOrderDetailRequest request)
		{
			_client.AccessToken = GetAccessToken();
			request.AccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(request.AccountingCompanyId);
			List<BudgetOrderDetailsViewModel> response = await _client.PostGetPurchaseOrderBudgetList(request);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Posts the get delivery times.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetDeliveryTimes(string accountingCompanyId, string orderNumber)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			OrderInvoiceDeliveryDetailViewModel response = new OrderInvoiceDeliveryDetailViewModel();
			response = await _client.PostGetOrderInvoiceDeliveryDetail(decryptedAccountingCompanyId, decryptedOrderNumber);
			return new JsonResult(response);
		}

		/// <summary>
		/// Posts the get order tracker details.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <param name="orderStatus">The order status.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetOrderTrackerDetails(string accountingCompanyId, string orderNumber, string orderStatus)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			List<OrderStatusViewModel> response = new List<OrderStatusViewModel>();
			response = await _client.PostGetOrderTrackerDetail(decryptedAccountingCompanyId, decryptedOrderNumber, orderStatus);
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the delivery address details.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetDeliveryAddressDetails(string accountingCompanyId, string orderNumber)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			OrderDeliveryDetailsViewModel response = new OrderDeliveryDetailsViewModel();
			response = await _client.PostGetCurrentOrderDeliveryDetail(decryptedAccountingCompanyId, decryptedOrderNumber);
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the summary component.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetSummaryComponent(string accountingCompanyId, string orderNumber)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			List<SummaryPurchaseOrderComponentViewModel> response = new List<SummaryPurchaseOrderComponentViewModel>();
			response = await _client.PostGetSummaryPoComponents(decryptedAccountingCompanyId, decryptedOrderNumber);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the supplier order details.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetSupplierOrderDetails(string accountingCompanyId, string orderNumber)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			SummaryPurchaseOrderDetailsViewModel response = new SummaryPurchaseOrderDetailsViewModel();
			response = await _client.PostGetSummaryPoDetails(decryptedAccountingCompanyId, decryptedOrderNumber);
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the authorised supplier details.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetAuthorisedSupplierDetails(string accountingCompanyId, string orderNumber)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			SummaryPurchaseOrderSupplierViewModel response = new SummaryPurchaseOrderSupplierViewModel();
			response = await _client.PostGetSummaryPoSupplierDetails(decryptedAccountingCompanyId, decryptedOrderNumber);
			return new JsonResult(response);
		}

		/// <summary>
		/// Posts the get summary po cost.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetSummaryPoCost(POSummaryCostDetailsRequest request)
		{
			_client.AccessToken = GetAccessToken();
			SummaryPurchaseOrderCostViewModel response = new SummaryPurchaseOrderCostViewModel();
			response = await _client.PostGetSummaryPoCost(request);
			return new JsonResult(new { costList = response.CostList, currency = response.Currency, lastPOCurrencyChangeLog = response.LastPOCurrencyChangeLog, showProposed = response.ShowProposed });
		}

		/// <summary>
		/// Posts the get summary po invoicing.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetSummaryPoInvoicing(string accountingCompanyId, string orderNumber)
		{
			_client.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			List<SummaryPurchaseOrderInvoicingViewModel> response = new List<SummaryPurchaseOrderInvoicingViewModel>();
			response = await _client.PostGetSummaryPoInvoicing(decryptedAccountingCompanyId, decryptedOrderNumber);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the order types.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetOrderTypes()
		{
			List<LookUp> orderTypes = new List<LookUp>();

			orderTypes.Add(new LookUp() { Identifier = null, Description = "All" });

			List<PurchaseOrderType> typeList = _client.GetOrderTypes();

			foreach (PurchaseOrderType ordertype in typeList)
			{
				orderTypes.Add(new LookUp() { Identifier = EnumsHelper.GetKeyValue(ordertype), Description = EnumsHelper.GetDescription(ordertype) });
			}
			return new JsonResult(orderTypes);
		}

		/// <summary>
		/// Gets the order stage.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetOrderStage()
		{
			List<LookUp> orderStage = new List<LookUp>();
			orderStage.Add(new LookUp() { Identifier = "", Description = "All" });
			List<PurchaseOrderStage> OrderStageList = _client.GetOrderStage();

			foreach (PurchaseOrderStage stage in OrderStageList)
			{
				if (EnumsHelper.GetKeyValue(stage) != Constants.FreightOrders)
				{
					orderStage.Add(new LookUp() { Identifier = EnumsHelper.GetKeyValue(stage), Description = EnumsHelper.GetDescription(stage) });
				}

			}
			return new JsonResult(orderStage);
		}

		/// <summary>
		/// Gets the order types.
		/// </summary>
		/// <param name="identifier">The document identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> DownloadInvoice(string identifier)
		{
			_documentClient.AccessToken = GetAccessToken();
			var result = await _documentClient.DownloadInvoiceDocument(identifier);
			byte[] byteData = (result != null && result.DocumentStream != null) ? CommonUtil.ConvertStreamToByte(result.DocumentStream) : null;
			string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
			return new JsonResult(new { filename = identifier, bytes = byteString, fileType = result.MediaType });
		}

		/// <summary>
		/// Gets the supplier lookup.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="q">The q.</param>
		/// <param name="_type">The type.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetSupplierLookup(string term, string q, string _type, int page)
		{
			sharedClient.AccessToken = GetAccessToken();

			Select2ResponseViewModel<List<CompanySearchResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<CompanySearchResponseViewModel>>();
			select2ResponseViewModel.Results = new List<CompanySearchResponseViewModel>();
			DataTablePageResponse<List<CompanySearchResponseViewModel>> response = new DataTablePageResponse<List<CompanySearchResponseViewModel>>();

			CompanySearchRequest request = new CompanySearchRequest();
			request.FetchDefaultCurrency = true;
			request.FetchDefaultCredit = false;
			request.CountryId = null;
			request.IsAgent = false;
			request.FetchAPValid = false;
			request.FetchOnlyActivatedAccountingCompanies = false;
			request.IsFullTextSearch = false;
			request.FetchOnlyAPEnable = false;
			request.CompanyName = term;

			request.CompanyTypeIds = new List<string>() { EnumsHelper.GetKeyValue(CompanyTypeEnum.PurchaseSupplier) };

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 100;
			pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "CompanyName" });

			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});


			if (!string.IsNullOrWhiteSpace(term))
			{
				response = await sharedClient.PostGetSupplierDetails(pageRequest, request);
			}
			select2ResponseViewModel.Results = response.Data;
			select2ResponseViewModel.Pagination = new Pagination();
			select2ResponseViewModel.Pagination.More = response.RecordsTotal > (pageRequest.Length * page);

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Gets the address details.
		/// </summary>
		/// <param name="SelectedStatus">The selected status.</param>
		/// <param name="EncryptedId">The encrypted identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetAddressDetails(string SelectedStatus, string EncryptedId)
		{
			sharedClient.AccessToken = GetAccessToken();
			string decryptedAccountingCompanyId = string.Empty;
			if (SelectedStatus == EnumsHelper.GetKeyValue(POTemplateSelection.Agent))
			{
				decryptedAccountingCompanyId = _provider.CreateProtector("AgentId").Unprotect(EncryptedId);
			}
			else if (SelectedStatus == EnumsHelper.GetKeyValue(POTemplateSelection.Warehouse))
			{
				decryptedAccountingCompanyId = _provider.CreateProtector("WarehouseId").Unprotect(EncryptedId);
			}
			else if (SelectedStatus == EnumsHelper.GetKeyValue(POTemplateSelection.Supplier))
			{
				decryptedAccountingCompanyId = _provider.CreateProtector("SupplierId").Unprotect(EncryptedId);
			}

			CompanyDetailViewModel response = await sharedClient.GetCompanyAddressDetails(decryptedAccountingCompanyId);
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the quote authorize wizard.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetQuoteAuthorizeWizard(string request)
		{
			sharedClient.AccessToken = GetAccessToken();
			_financeClient.AccessToken = GetAccessToken();
			_client.AccessToken = GetAccessToken();

			SupplierDetailsForQuoteAuthorizationViewModel response = new SupplierDetailsForQuoteAuthorizationViewModel();
			OrderDetailsForQuoteAuthorizationViewModel OrderDetails = new OrderDetailsForQuoteAuthorizationViewModel();
			AuthorizeQuoteRequestViewModel requestVM = new AuthorizeQuoteRequestViewModel();

			if (!string.IsNullOrWhiteSpace(request))
			{
				string data = _provider.CreateProtector("AuthorizeQuote").Unprotect(request);
				requestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizeQuoteRequestViewModel>(data);
			}

			bool isVesselInManagement = false;
			if (!string.IsNullOrWhiteSpace(requestVM.AccountingCompanyId) && !string.IsNullOrWhiteSpace(requestVM.VesselId))
			{
				requestVM.CheckPrefundingFlag = false;
				isVesselInManagement = await sharedClient.PostCheckVesselIsInPurchasingManagement(requestVM);
			}

			if (isVesselInManagement)
			{
				if (requestVM.PurchaseOrderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation) || requestVM.PurchaseOrderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingReview))
				{
					if (requestVM.SupplierOrderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation) || requestVM.SupplierOrderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingReview))
					{
						if (!string.IsNullOrWhiteSpace(requestVM.SupplierOrderId))
						{
							response = await _client.PostGetSupDetailsForQuoteAuth(requestVM.SupplierOrderId);
							OrderDetails = await _client.PostGetOrderDetailsForQuoteAuth(requestVM);
							return new JsonResult(new { response = true, status = Constants.ToastrSuccess, supplierDetails = response, orderDetails = OrderDetails, isVesselInManagement = isVesselInManagement });
						}
					}
					else
					{
						string errorMessage = string.Format(Constants.SupplierOrderStatusForQuoteAuthorizationIncorrect, requestVM.AccountingCompanyId, requestVM.OrderNumber);
						return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = errorMessage });
					}
				}
				else
				{
					string errorMessage = string.Format(Constants.OrderStatusForQuoteAuthorizationIncorrect, requestVM.AccountingCompanyId, requestVM.OrderNumber);
					return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = errorMessage });
				}
			}
			else
			{
				string errorMessage = Constants.VesselManagementEnd;
				return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = errorMessage });
			}

			return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = "Failed to open authorize quote." });

		}

		/// <summary>
		/// Posts the get order details for quote authentication.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetOrderDetailsForQuoteAuth(string accountingCompanyId, string orderNumber)
		{
			_client.AccessToken = GetAccessToken();
			OrderDetailsForQuoteAuthorizationViewModel OrderDetails = new OrderDetailsForQuoteAuthorizationViewModel();
			AuthorizeQuoteRequestViewModel requestVM = new AuthorizeQuoteRequestViewModel();
			if (!string.IsNullOrWhiteSpace(accountingCompanyId))
			{
				string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
				requestVM.AccountingCompanyId = decryptedAccountingCompanyId;
			}

			if (!string.IsNullOrWhiteSpace(orderNumber))
			{
				string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
				requestVM.OrderNumber = decryptedOrderNumber;
			}

			if (requestVM != null)
			{
				OrderDetails = await _client.PostGetOrderDetailsForQuoteAuth(requestVM);
			}
			return new JsonResult(OrderDetails);
		}

		/// <summary>
		/// Gets the vessel and office limit.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetVesselAndOfficeLimit(string request)
		{
			_client.AccessToken = GetAccessToken();
			VesselAndOfficeLimitViewModel response = new VesselAndOfficeLimitViewModel();
			AuthorizeQuoteRequestViewModel requestVM = new AuthorizeQuoteRequestViewModel();

			if (!string.IsNullOrWhiteSpace(request))
			{
				string data = _provider.CreateProtector("AuthorizeQuote").Unprotect(request);
				requestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizeQuoteRequestViewModel>(data);
			}
			response = await _client.PostGetVesselAndOfficeLimit(requestVM);
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the vessel account budget.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <param name="accountId">The account identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetVesselAccountBudget(string accountingCompanyId, string orderNumber, string accountId)
		{
			_financeClient.AccessToken = GetAccessToken();
			CompanyVesselBudgetViewModel vesselBudget = new CompanyVesselBudgetViewModel();
			if (!string.IsNullOrWhiteSpace(accountingCompanyId) && !string.IsNullOrWhiteSpace(orderNumber) && !string.IsNullOrWhiteSpace(accountId))
			{
				vesselBudget = await _financeClient.PostGetVesselAccountBudgetDetail(accountingCompanyId, orderNumber, accountId);
			}

			return new JsonResult(vesselBudget);
		}

		/// <summary>
		/// Gets the company rejection reasons.
		/// </summary>
		/// <param name="rejectionReasonType">Type of the rejection reason.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetCompanyRejectionReasons(string rejectionReasonType)
		{
			sharedClient.AccessToken = GetAccessToken();

			List<Lookup> justificationReasons = new List<Lookup>();
			if (rejectionReasonType == EnumsHelper.GetKeyValue(ReasonType.BudgetJustification))
			{
				justificationReasons = await sharedClient.PostGetCompanyRejectionReasons(ReasonType.BudgetJustification);
			}
			else if (rejectionReasonType == EnumsHelper.GetKeyValue(ReasonType.Rejection))
			{
				justificationReasons = await sharedClient.PostGetCompanyRejectionReasons(ReasonType.Rejection);
			}

			return new JsonResult(justificationReasons);
		}

		/// <summary>
		/// Gets the vessel account code.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="accountCompanyId">The account company identifier.</param>
		/// <param name="isVesselInPurchasingManagement">if set to <c>true</c> [is vessel in purchasing management].</param>
		/// <returns></returns>
		public async Task<JsonResult> GetVesselAccountCode(string term, string vesselId, string accountCompanyId, bool isVesselInPurchasingManagement)
		{
			_financeClient.AccessToken = GetAccessToken();

			Select2ResponseViewModel<List<AccountLookupWrapperViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<AccountLookupWrapperViewModel>>();
			select2ResponseViewModel.Results = new List<AccountLookupWrapperViewModel>();
			List<AccountLookupWrapperViewModel> response = new List<AccountLookupWrapperViewModel>();

			SearchAccountCodeRequest accountCodeRequest = new SearchAccountCodeRequest();
			accountCodeRequest.VesselId = vesselId;
			accountCodeRequest.AccountingCompanyId = accountCompanyId;
			accountCodeRequest.AccountCode = term;
			accountCodeRequest.AccountName = term;
			accountCodeRequest.IsVesselInPurchasingManagement = isVesselInPurchasingManagement;
			accountCodeRequest.IsQuickSearch = true;
			if (!string.IsNullOrWhiteSpace(term) && !string.IsNullOrWhiteSpace(vesselId) && !string.IsNullOrWhiteSpace(accountCompanyId))
			{
				response = await _financeClient.PostGetAccountCodesForVessel(accountCodeRequest);
			}
			select2ResponseViewModel.Results = response;
			select2ResponseViewModel.Pagination = new Pagination();

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Saves the authentication quote.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> SaveAuthQuote(AuthoriseQuoteRequestViewModel request)
		{
			_client.AccessToken = GetAccessToken();
			AuthoriseQuoteRequest inputRequest = new AuthoriseQuoteRequest();
			bool response = false;
			if (request != null)
			{
				inputRequest.OrderId = request.OrderId;
				inputRequest.SupplierOrderId = request.SupplierOrderId;
				inputRequest.IsFeedbackRequired = request.IsFeedbackRequired;
				inputRequest.FeedbackReasonDescription = request.FeedbackReasonDescription;
				inputRequest.FeedbackComments = request.FeedbackComments;
				inputRequest.FeedbackSupplierOrderId = request.FeedbackSupplierOrderId;
				inputRequest.JustificationComment = request.JustificationComment;
				inputRequest.AccountId = request.AccountId;
				inputRequest.Accruals = request.Accruals;
				inputRequest.JustificationReasonId = !string.IsNullOrWhiteSpace(request.JustificationReasonId) ? Int32.Parse(request.JustificationReasonId) : (int?)null;
				inputRequest.Auxiliaries = request.Auxiliaries;

				response = await _client.PostAuthoriseQuote(inputRequest);
			}
			return new JsonResult(response);
		}

		/// <summary>
		/// Posts the get vessel part detail.
		/// </summary>
		/// <param name="vesselPartId">The vessel part identifier.</param>
		/// <returns>
		/// PartDetailViewModel
		/// </returns>
		public async Task<IActionResult> PostGetVesselPartDetail(string vesselPartId)
		{
			_client.AccessToken = GetAccessToken();
			PartDetailViewModel result = await _client.PostGetVesselPartDetailAsync(vesselPartId);
			return new JsonResult(result);
		}

		/// <summary>
		/// Gets the aux by acc code and coy identifier.
		/// </summary>
		/// <param name="accountId">The account identifier.</param>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetAuxByAccCodeAndCoyId(string accountId, string accountingCompanyId)
		{
			_financeClient.AccessToken = GetAccessToken();
			ApplicableAccountAuxiliariesViewModel ApplicableAuxFlags = await _financeClient.PostGetAuxByAccCodeAndCoyId(accountId, accountingCompanyId);
			return new JsonResult(ApplicableAuxFlags);
		}

		/// <summary>
		/// Gets the insurance claim lookup.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="accountCompanyId">The account company identifier.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetInsuranceClaimLookup(string term, string accountCompanyId, int page)
		{
			sharedClient.AccessToken = GetAccessToken();

			Select2ResponseViewModel<List<AuxiliaryResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<AuxiliaryResponseViewModel>>();
			select2ResponseViewModel.Results = new List<AuxiliaryResponseViewModel>();
			DataTablePageResponse<List<AuxiliaryResponseViewModel>> response = new DataTablePageResponse<List<AuxiliaryResponseViewModel>>();

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 100;
			pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "ShortCode" });
			pageRequest.Columns.Add(new Column() { Name = "Description" });
			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});

			AuxiliarySearchRequestForLookUp request = new AuxiliarySearchRequestForLookUp();
			request.IsQuickSearch = true;
			request.ShortCode = term;
			request.SearchText = term;

			if (!string.IsNullOrWhiteSpace(term) && !string.IsNullOrWhiteSpace(accountCompanyId))
			{
				response = await sharedClient.PostGetInsuranceClaimsByCoyIdPaged(pageRequest, accountCompanyId, request);
			}
			select2ResponseViewModel.Results = response.Data;
			select2ResponseViewModel.Pagination = new Pagination();
			select2ResponseViewModel.Pagination.More = response.RecordsTotal > (pageRequest.Length * page);

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Gets the general aux list by type paged.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="page">The page.</param>
		/// <param name="auxType">Type of the aux.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetGeneralAuxListByTypePaged(string term, int page, string auxType)
		{
			_financeClient.AccessToken = GetAccessToken();

			Select2ResponseViewModel<List<AuxiliaryResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<AuxiliaryResponseViewModel>>();
			select2ResponseViewModel.Results = new List<AuxiliaryResponseViewModel>();
			DataTablePageResponse<List<AuxiliaryResponseViewModel>> response = new DataTablePageResponse<List<AuxiliaryResponseViewModel>>();

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 100;
			pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "ShortCode" });
			pageRequest.Columns.Add(new Column() { Name = "Description" });
			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});

			AuxiliarySearchRequestForLookUp request = new AuxiliarySearchRequestForLookUp();
			request.IsQuickSearch = true;
			request.ShortCode = term;
			request.SearchText = term;

			if (!string.IsNullOrWhiteSpace(term))
			{
				response = await _financeClient.PostGetGeneralAuxListByTypePaged(pageRequest, request, auxType);
			}
			select2ResponseViewModel.Results = response.Data;
			select2ResponseViewModel.Pagination = new Pagination();
			select2ResponseViewModel.Pagination.More = response.RecordsTotal > (pageRequest.Length * page);

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Gets the nationality lookup.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetNationalityLookup(string term)
		{
			_financeClient.AccessToken = GetAccessToken();

			Select2ResponseViewModel<List<AuxiliaryResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<AuxiliaryResponseViewModel>>();
			select2ResponseViewModel.Results = new List<AuxiliaryResponseViewModel>();
			List<AuxiliaryResponseViewModel> response = new List<AuxiliaryResponseViewModel>();

			AuxiliarySearchRequestForLookUp searchRequest = new AuxiliarySearchRequestForLookUp();
			searchRequest.IsQuickSearch = true;
			searchRequest.ShortCode = term;
			searchRequest.SearchText = term;

			if (!string.IsNullOrWhiteSpace(term))
			{
				response = await _financeClient.PostGetNationalityLookup(searchRequest);
			}
			select2ResponseViewModel.Results = response;
			select2ResponseViewModel.Pagination = new Pagination();

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Gets the crew rank lookup.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetCrewRankLookup(string term, int page)
		{
			sharedClient.AccessToken = GetAccessToken();

			Select2ResponseViewModel<List<AuxiliaryResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<AuxiliaryResponseViewModel>>();
			select2ResponseViewModel.Results = new List<AuxiliaryResponseViewModel>();
			DataTablePageResponse<List<AuxiliaryResponseViewModel>> response = new DataTablePageResponse<List<AuxiliaryResponseViewModel>>();

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 100;
			pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "ShortCode" });
			pageRequest.Columns.Add(new Column() { Name = "Description" });
			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});

			if (!string.IsNullOrWhiteSpace(term))
			{
				response = await sharedClient.PostGetCrewRankListForAuxPaged(pageRequest, term);
			}
			select2ResponseViewModel.Results = response.Data;
			select2ResponseViewModel.Pagination = new Pagination();
			select2ResponseViewModel.Pagination.More = response.RecordsTotal > (pageRequest.Length * page);

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Gets the vessel aux lookup.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetVesselAuxLookup(string term, int page)
		{
			sharedClient.AccessToken = GetAccessToken();

			Select2ResponseViewModel<List<AuxiliaryResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<AuxiliaryResponseViewModel>>();
			select2ResponseViewModel.Results = new List<AuxiliaryResponseViewModel>();
			DataTablePageResponse<List<AuxiliaryResponseViewModel>> response = new DataTablePageResponse<List<AuxiliaryResponseViewModel>>();

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 100;
			pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "ShortCode" });
			pageRequest.Columns.Add(new Column() { Name = "Description" });
			pageRequest.Columns.Add(new Column() { Name = "Type" });
			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});

			AuxiliarySearchRequestForLookUp searchRequest = new AuxiliarySearchRequestForLookUp();
			searchRequest.IsQuickSearch = true;
			searchRequest.ShortCode = term;
			searchRequest.SearchText = term;

			if (!string.IsNullOrWhiteSpace(term))
			{
				response = await sharedClient.PostGetVesselAuxListPaged(pageRequest, searchRequest);
			}
			select2ResponseViewModel.Results = response.Data;
			select2ResponseViewModel.Pagination = new Pagination();
			select2ResponseViewModel.Pagination.More = response.RecordsTotal > (pageRequest.Length * page);

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Posts the get authorisation additional information.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns>
		/// OrderAuthorisationLimitDetailViewModel
		/// </returns>
		public async Task<IActionResult> PostGetAuthorisationAdditionalInfo(string accountingCompanyId, string orderNumber)
		{
			Dictionary<string, object> data = new Dictionary<string, object>();
			data.Add("accountingCompanyId", _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId));
			data.Add("orderNumber", _provider.CreateProtector("OrderNumber").Unprotect(orderNumber));
			_client.AccessToken = GetAccessToken();
			OrderAuthorisationLimitDetailViewModel result = await _client.PostGetOrderAuthLimitDetail(data);
			return new JsonResult(result);
		}

		/// <summary>
		/// Posts the get order authorisers.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetOrderAuthorisers(string accountingCompanyId, string orderNumber)
		{
			Dictionary<string, object> data = new Dictionary<string, object>();
			data.Add("accountingCompanyId", _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId));
			data.Add("orderNumber", _provider.CreateProtector("OrderNumber").Unprotect(orderNumber));
			_client.AccessToken = GetAccessToken();
			List<AuthoriserDetailViewModel> result = await _client.PostGetOrderAuthorisers(data);
			return new JsonResult(new { data = result });
		}

		/// <summary>
		/// Posts the check order authorisation pending.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns>
		/// PendingOrderAuthorisationDetailViewModel
		/// </returns>
		public async Task<IActionResult> PostCheckOrderAuthorisationPending(string accountingCompanyId, string orderNumber)
		{
			Dictionary<string, object> data = new Dictionary<string, object>();
			data.Add("accountingCompanyId", _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId));
			data.Add("orderNumber", _provider.CreateProtector("OrderNumber").Unprotect(orderNumber));
			_client.AccessToken = GetAccessToken();
			PendingOrderAuthorisationDetailViewModel result = await _client.PostCheckOrderAuthorisationPending(data);
			return new JsonResult(result);
		}


		/// <summary>
		/// Posts the get authorization rights for purchasing and order authentication.
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> PostGetAuthorizationRightsForPurchasingAndOrderAuth()
		{
			Dictionary<string, object> input = new Dictionary<string, object>();
			input.Add("module", ModuleEnum.Purchasing);
			input.Add("functionality", WorkFlowFunctionalityEnum.OrderAuthorization);
			sharedClient.AccessToken = GetAccessToken();
			List<AuthorizationRightsViewModel> result = await sharedClient.PostGetAuthorizationRights(input);
			return new JsonResult(result);
		}

		/// <summary>
		/// Posts the get current user.
		/// </summary>
		/// <returns>
		/// UserViewModel
		/// </returns>
		public async Task<IActionResult> PostGetCurrentUser()
		{
			sharedClient.AccessToken = GetAccessToken();
			UserViewModel result = await sharedClient.GetCurrentUser();
			return new JsonResult(result);
		}

		/// <summary>
		/// Posts the authorise order.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <returns>
		/// status
		/// </returns>
		public async Task<IActionResult> PostAuthoriseOrder(string accountingCompanyId, string orderNumber, string vesselId)
		{
			sharedClient.AccessToken = GetAccessToken();
			AuthorizeQuoteRequestViewModel requestVM = new AuthorizeQuoteRequestViewModel();
			requestVM.AccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			requestVM.CheckPrefundingFlag = true;
			requestVM.OrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);
			requestVM.VesselId = _provider.CreateProtector("Vessel").Unprotect(vesselId).Split(Constants.Separator)[0];

			bool isVesselInManagement = false;
			if (!string.IsNullOrWhiteSpace(requestVM.AccountingCompanyId) && !string.IsNullOrWhiteSpace(requestVM.VesselId))
			{
				isVesselInManagement = await sharedClient.PostCheckVesselIsInPurchasingManagement(requestVM);
			}
			if (isVesselInManagement)
			{
				Dictionary<string, object> data = new Dictionary<string, object>();
				data.Add("accountingCompanyId", requestVM.AccountingCompanyId);
				data.Add("orderNumber", requestVM.OrderNumber);
				_client.AccessToken = GetAccessToken();
				AuthoriserDetailViewModel result = await _client.PostAuthoriseOrder(data);
				if (result != null)
				{
					return new JsonResult(new { response = true, status = Constants.ToastrSuccess, message = Constants.OrderAuthoriseSuccessMessage });
				}
				else
				{
					return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = Constants.OrderAuthoriseFailedMessage });
				}
			}
			else
			{
				string errorMessage = Constants.VesselManagementEnd;
				return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = errorMessage });
			}
		}

		/// <summary>
		/// Posts the client authorise order.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		/// status
		/// </returns>
		public async Task<IActionResult> PostClientAuthoriseOrder(ClientAuthoriseOrderRequestViewModel input)
		{
			sharedClient.AccessToken = GetAccessToken();
			AuthorizeQuoteRequestViewModel requestVM = new AuthorizeQuoteRequestViewModel();
			requestVM.AccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(input.AccountingCompanyId);
			requestVM.CheckPrefundingFlag = true;
			requestVM.VesselId = _provider.CreateProtector("Vessel").Unprotect(input.VesselId).Split(Constants.Separator)[0];

			bool isVesselInManagement = false;
			if (!string.IsNullOrWhiteSpace(requestVM.AccountingCompanyId) && !string.IsNullOrWhiteSpace(requestVM.VesselId))
			{
				isVesselInManagement = await sharedClient.PostCheckVesselIsInPurchasingManagement(requestVM);
			}
			if (isVesselInManagement)
			{
				Dictionary<string, object> data = new Dictionary<string, object>();
				data.Add("orderId", input.OrderId);
				if (!input.IsApprove)
				{
					data.Add("comment", input.Comment);
				}
				data.Add("isApprove", input.IsApprove);
				_client.AccessToken = GetAccessToken();
				bool result = await _client.PostClientAuthoriseOrder(data);
				if (result)
				{
					if (input.IsApprove)
					{
						return new JsonResult(new { response = true, status = Constants.ToastrSuccess, message = Constants.OrderAuthoriseSuccessMessage });
					}
					else
					{
						return new JsonResult(new { response = true, status = Constants.ToastrSuccess, message = Constants.OrderRejectSuccessMessage });
					}

				}
				else
				{
					if (input.IsApprove)
					{
						return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = Constants.OrderAuthoriseFailedMessage });
					}
					else
					{
						return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = Constants.OrderRejectFailMessage });
					}
				}
			}
			else
			{
				string errorMessage = Constants.VesselManagementEnd;
				return new JsonResult(new { response = false, status = Constants.ToastrValidate, message = errorMessage });
			}
		}

		/// <summary>
		/// Gets the stage and status map.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetStageAndStatusMap()
		{
			Dictionary<string, List<LookUp>> map = new Dictionary<string, List<LookUp>>();
			var stages = Enum.GetValues(typeof(PurchaseOrderStage)).Cast<PurchaseOrderStage>().ToList();
			var allStatus = Enum.GetValues(typeof(PurchaseOrderStatus)).Cast<PurchaseOrderStatus>().
				Where(x => !x.Equals(PurchaseOrderStatus.Default)).
				Select(x => new LookUp
				{
					Identifier = EnumsHelper.GetKeyValue(x),
					Description = EnumsHelper.GetKeyValue(x) + " - " + EnumsHelper.GetDescription(x)
				}).ToList();

			allStatus.Insert(0, new LookUp { Identifier = "", Description = "All" });
			map.Add("", allStatus);
			foreach (var stage in stages)
			{
				var attribute = stage.GetEnumMetadata<PurchaseOrderStage, PurchaseStatusAttribute>();
				if (attribute != null)
				{
					map[EnumsHelper.GetKeyValue(stage)] = attribute.ValidStatusList.
						Select(x => new LookUp
						{
							Identifier = EnumsHelper.GetKeyValue(x),
							Description = EnumsHelper.GetKeyValue(x) + " - " + EnumsHelper.GetDescription(x)
						}).ToList();
					map[EnumsHelper.GetKeyValue(stage)].Insert(0, new LookUp { Identifier = "", Description = "All" });
				}
			}
			return new JsonResult(map);
		}

		/// <summary>
		/// Exports to excel order details report.
		/// </summary>
		/// <param name="accountingCompanyId">The accounting company identifier.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <returns></returns>
		public async Task<JsonResult> ExportToExcelOrderDetailsReport(string accountingCompanyId, string orderNumber)
		{
			sharedClient.AccessToken = GetAccessToken();

			string AccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(accountingCompanyId);
			string OrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(orderNumber);

			ReportLight reportRequest = await sharedClient.GetReportLightByFilename(EnumsHelper.GetKeyValue(ReportMaster.PurchasingOrderDetailExcelReport));

			if (reportRequest != null)
			{
				reportRequest.FriendlyFileName = Constants.OrderDetailsReportFileName;
				reportRequest.ReportFormat = ReportExportTypes.Excel;

				foreach (var reportParameter in reportRequest.ReportParameters)
				{
					if (reportParameter.ParameterName.Equals("@accountingCompanyId"))
					{
						reportParameter.ValueToSet = new List<object>() { AccountingCompanyId };
					}
					if (reportParameter.ParameterName.Equals("@orderNumber"))
					{
						reportParameter.ValueToSet = new List<object>() { OrderNumber };
					}
				}

				var reportRequestId = await sharedClient.InitiateReportRequest(reportRequest);
				if (reportRequestId != null && reportRequestId != string.Empty)
				{
					return new JsonResult(new { message = Messages.ReportGenerationSuccessMessage, success = true });
				}
				else
				{
					return new JsonResult(new { message = Messages.ReportGenerationErrorMessage, success = false });
				}
			}
			else
			{
				return new JsonResult(new { message = Messages.NoDetailsFound, success = false });
			}
		}

		/// <summary>
		/// Gets the order status tree.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetOrderStatusTree()
		{
			List<string> orderStatusHeader = new List<string>();
			List<OrderStausTreeViewModel> orderStatusList = new List<OrderStausTreeViewModel>();
			List<TreeViewModel<OrderStausTreeViewModel>> treeList = new List<TreeViewModel<OrderStausTreeViewModel>>();

			var stages = Enum.GetValues(typeof(PurchaseOrderStage)).Cast<PurchaseOrderStage>().
				Where(x => EnumsHelper.GetKeyValue(x) != EnumsHelper.GetKeyValue(PurchaseOrderStage.FreightOrder)).ToList();
			foreach (var stage in stages)
			{
				var attribute = stage.GetEnumMetadata<PurchaseOrderStage, PurchaseStatusAttribute>();
				if (attribute != null)
				{
					var list = attribute.ValidStatusList.
								Where(x => EnumsHelper.GetKeyValue(x) != EnumsHelper.GetKeyValue(PurchaseOrderStatus.DraftOfRequisition)).ToList();

					foreach (var item in list)
					{
						OrderStausTreeViewModel orderStatus = new OrderStausTreeViewModel();
						orderStatus.OrderStatusId = EnumsHelper.GetKeyValue(item);
						orderStatus.OrderStatus = EnumsHelper.GetKeyValue(item) + " - " + EnumsHelper.GetDescription(item);
						orderStatus.OrderStatusHeader = EnumsHelper.GetKeyValue(item) == Constants.OrderCancelled_ZZ
														|| EnumsHelper.GetKeyValue(item) == Constants.OnHold_OH
														? null : EnumsHelper.GetKeyValue(stage);
						orderStatusList.Add(orderStatus);
					}
				}
			}

			TreeViewModel<OrderStausTreeViewModel> AllOption = new TreeViewModel<OrderStausTreeViewModel>
			{
				Title = "All",
				Expanded = true,
				Key = Constants.All,
				Checkbox = true,
				Lazy = false,
				Tooltip = "All",
				Children = new List<TreeViewModel<OrderStausTreeViewModel>>(),
				AdditionalData = new OrderStausTreeViewModel()
				{
					OrderStatusId = null
				}
			};

			if (orderStatusList != null && orderStatusList.Any())
			{
				foreach (var item in stages)
				{
					orderStatusHeader.Add(EnumsHelper.GetKeyValue(item));
				}
				orderStatusHeader.Add(Constants.OnHold);
				orderStatusHeader.Add(Constants.OrderCancelled);
			}

			if (orderStatusHeader != null && orderStatusHeader.Any())
			{
				foreach (string orderStatusHeaderName in orderStatusHeader)
				{
					string headerId = orderStatusHeaderName;
					List<TreeViewModel<OrderStausTreeViewModel>> childItems = new List<TreeViewModel<OrderStausTreeViewModel>>();
					if (orderStatusList.Where(x => x.OrderStatusHeader == orderStatusHeaderName).Any())
					{
						childItems.AddRange(orderStatusList.Where(x => x.OrderStatusHeader == orderStatusHeaderName).Select(y =>
						new TreeViewModel<OrderStausTreeViewModel>
						{
							Key = y.OrderStatusId,
							Title = y.OrderStatus,
							Tooltip = y.OrderStatus,
							Expanded = false,
							Checkbox = true,
							Lazy = false,
							Children = null,
							AdditionalData = new OrderStausTreeViewModel()
							{
								IsOrderStage = y.IsOrderStage
							}
						}));
					}

					AllOption.Children.Add(new TreeViewModel<OrderStausTreeViewModel>
					{
						Key = orderStatusHeaderName == Constants.OnHold ? Constants.OnHold_OH :
							  orderStatusHeaderName == Constants.OrderCancelled ? Constants.OrderCancelled_ZZ : "",
						Title = orderStatusHeaderName,
						Tooltip = orderStatusHeaderName,
						Expanded = false,
						Checkbox = true,
						Lazy = false,
						Children = childItems,
						AdditionalData = new OrderStausTreeViewModel()
						{
							IsOrderStage = false,
						}
					});
				}
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Gets the selected company.
		/// </summary>
		/// <param name="inputText">The input text.</param>
		/// <param name="companyId">The company identifier.</param>
		/// <returns></returns>
		public async Task<ActionResult> GetSelectedCompany(string inputText, string companyId)
		{
			sharedClient.AccessToken = GetAccessToken();
			List<CompanySearchResponseViewModel> response = null;
			CompanySearchRequest request = new CompanySearchRequest();
			request.FetchDefaultCurrency = true;
			request.FetchDefaultCredit = false;
			request.CountryId = null;
			request.IsAgent = false;

			request.FetchAPValid = false;
			request.FetchOnlyActivatedAccountingCompanies = false;
			request.IsFullTextSearch = false;
			request.FetchOnlyAPEnable = false;
			request.CompanyName = inputText;
			request.CompanyTypeIds = new List<string>() { EnumsHelper.GetKeyValue(CompanyTypeEnum.PurchaseSupplier) };

			if (!string.IsNullOrWhiteSpace(inputText))
			{
				response = await sharedClient.GetCompanyList(request);
			}

			var SelectedCompany = response.Where(x => x.CompanyId == companyId).FirstOrDefault();

			return new JsonResult(SelectedCompany);
		}

		/// <summary>
		/// Gets the company list paged.
		/// </summary>
		/// <param name="inputText">The input text.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetCompanyListPaged(string inputText, int page)
		{
			sharedClient.AccessToken = GetAccessToken();

			DataTablePageResponse<List<CompanySearchResponseViewModel>> response = new DataTablePageResponse<List<CompanySearchResponseViewModel>>();

			CompanySearchRequest request = new CompanySearchRequest();
			request.FetchDefaultCurrency = true;
			request.FetchDefaultCredit = false;
			request.CountryId = null;
			request.IsAgent = false;

			request.FetchAPValid = false;
			request.FetchOnlyActivatedAccountingCompanies = false;
			request.IsFullTextSearch = true;
			request.FetchOnlyAPEnable = false;
			request.CompanyName = inputText;
			request.CompanyTypeIds = new List<string>() { EnumsHelper.GetKeyValue(CompanyTypeEnum.PurchaseSupplier) };

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 10;
			pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "CompanyName" });

			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});
			if (!string.IsNullOrWhiteSpace(inputText))
			{
				response = await sharedClient.PostGetSupplierDetails(pageRequest, request);
			}

			return new JsonResult(new { data = response.Data });
		}

		/// <summary>
		/// Gets the purchase order details.
		/// </summary>
		/// <param name="poRequestVM">The po request vm.</param>
		/// <returns></returns>
		[NonAction]
		private PurchaseOrderRequestViewModel GetPurchaseOrderDetails(PurchaseOrderRequestViewModel poRequestVM)
		{
			poRequestVM.OrderStatusIds = new List<string>();
			string POStage = poRequestVM.POStage;
			if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.InProcess))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.Requisition));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryOutstanding));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryInProgress));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OnHold));
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.Ordered))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OrderIssued));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToWarehouse));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OrderReadyEx_Works));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.StoredAtWarehouse));
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.Dispatched))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToAgent));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToVessel));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedInTransit));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedByAgent));
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.AuthenticationRequired))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.Received30Days))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedByVessel));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.PartOrderReceivedByVessel));

				poRequestVM.FromDate = DateTime.Now.AddDays(-30);
				poRequestVM.ToDate = DateTime.Now;
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.AuthorisationRequired))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.TenderAwaitingAuthorization))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation));
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.Requisitions))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.Requisition));
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.Enquiries))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryInProgress));
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryOutstanding));
			}
			else if (POStage == EnumsHelper.GetKeyValue(PoStagesFilter.OnHold))
			{
				poRequestVM.OrderStatusIds.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OnHold));
			}

			if (poRequestVM.OrderStatusIds != null && poRequestVM.OrderStatusIds.Any())
			{
				poRequestVM.strOrderStatusIds = string.Join(',', poRequestVM.OrderStatusIds);
			}

			return poRequestVM;
		}

		/// <summary>
		/// Gets the supplier documents.
		/// </summary>
		/// <param name="supplierOrderIds">The supplier order ids.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetSupplierDocuments(List<string> supplierOrderIds)
		{
			sharedClient.AccessToken = GetAccessToken();
			DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest();
			documentDetailRequest.DocumentSourceIds = supplierOrderIds;
			documentDetailRequest.SsmId = EnumsHelper.GetKeyValue(SubModule.VesselSupplierQuote);
			string input = _provider.CreateProtector("DocumentURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(documentDetailRequest));
			List<DocumentDetail> response = await sharedClient.PostGetDocumentDetails(input);
			List<DocumentDetailViewModel> result = new List<DocumentDetailViewModel>();

			if (response != null && response.Any())
			{
				foreach (DocumentDetail item in response)
				{
					DocumentDetailViewModel viewModel = new DocumentDetailViewModel();
					viewModel.CreatedOn = item.CreatedOn;
					viewModel.Type = item.CategoryName;
					viewModel.Description = item.Description;
					viewModel.Title = item.Title;
					viewModel.CanRequestDocument = item.CanRequestDocument;
					viewModel.EttId = item.EttId;
					viewModel.CloudFileName = item.CloudFileName;
					viewModel.DocumentCategory = DocumentCategory.VesselSupplierQuote;

					result.Add(viewModel);
				}
			}

			return new JsonResult(new { data = result });
		}

		/// <summary>
		/// Gets the supplier rating breakdown.
		/// </summary>
		/// <param name="SupplierCompanyId">The supplier company identifier.</param>
		/// <param name="SupplierName">Name of the supplier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetSupplierRatingBreakdown(string SupplierCompanyId, string SupplierName)
		{
			sharedClient.AccessToken = GetAccessToken();
			SupplierRatingBreakdownViewModel response = await sharedClient.GetSupplierRatingBreakdown(SupplierCompanyId);
			response.OrderName = SupplierName;

			return PartialView("_SupplierRatingBreakdown", response);
		}

		/// <summary>
		/// GetAuthoriseOrderSuccesUrl
		/// </summary>
		/// <param name="pageKey"></param>
		/// <returns></returns>
		public JsonResult GetAuthoriseOrderSuccesUrl(string pageKey)
		{
			string sourceUrl = GetSourceURLString(pageKey);

			//Purchase Order List
			if (CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey)) != null)
			{
				PurchaseOrderRequestViewModel poRequest = new PurchaseOrderRequestViewModel();
				var SessionData = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey));
				string data = _provider.CreateProtector(Constants.PurchaseOrderEncryptionText).Unprotect(SessionData);
				poRequest = JsonConvert.DeserializeObject<PurchaseOrderRequestViewModel>(data);

				poRequest.OrderStatusIds = new List<string> { EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder) };
				poRequest.strOrderStatusIds = EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder);
				poRequest.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.AuthorisationRequired);
				poRequest.ShowOnlyAuthRequired = true;

				string encryptedPurchaseorderUr = CommonUtil.GetEncryptedURL(_provider, Constants.PurchaseOrderEncryptionText, poRequest);
				SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey), encryptedPurchaseorderUr, poRequest.VesselId);
			}
			else if (CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.ApprovalListPageKey)) != null)
			{

			}

			return new JsonResult(sourceUrl);
		}

		/// <summary>
		/// Gets the authorise quote succes URL.
		/// </summary>
		/// <param name="pageKey">The page key.</param>
		/// <returns></returns>
		public JsonResult GetAuthoriseQuoteSuccesUrl(string pageKey)
		{
			string sourceUrl = GetSourceURLString(pageKey);

			//Purchase Order List
			string procurementListPageKey = EnumsHelper.GetKeyValue(NavigationPageKey.ProcurementListPageKey);
			if (CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, procurementListPageKey) != null)
			{
				PurchaseOrderRequestViewModel poRequest = new PurchaseOrderRequestViewModel();
				var SessionData = GetSessionFilter(procurementListPageKey);
				string data = _provider.CreateProtector(Constants.PurchaseOrderEncryptionText).Unprotect(SessionData);
				poRequest = JsonConvert.DeserializeObject<PurchaseOrderRequestViewModel>(data);

				poRequest.OrderStatusIds = new List<string> { EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation) };
				poRequest.strOrderStatusIds = EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation);
				poRequest.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.TenderAwaitingAuthorization);
				poRequest.ShowOnlyAuthRequired = true;

				string encryptedPurchaseorderUr = CommonUtil.GetEncryptedURL(_provider, Constants.PurchaseOrderEncryptionText, poRequest);
				SetSessionFilter(procurementListPageKey, encryptedPurchaseorderUr, poRequest.VesselId);
			}
			else if (CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.ApprovalListPageKey)) != null)
			{

			}

			return new JsonResult(sourceUrl);
		}
		
	}
}