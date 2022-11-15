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
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report.Approval;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;
using PWAFeaturesRnd.ViewModels.Approval;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.PurchaseOrder;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// Purchasing Client
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class PurchasingClient : BaseHttpClient
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
        /// Initializes a new instance of the <see cref="PurchasingClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public PurchasingClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider, IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
        {
            client.BaseAddress = new Uri(AppSettings.PurchasingWebApiUrl);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        /// <summary>
        /// Gets the purchase order list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<PurchasingDetailViewModel>>> GetPurchaseOrderList(DataTablePageRequest<string> pageRequest, PurchaseOrderRequestViewModel inputRequest)
        {
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

            PurchaseOrderRequest orderRequest = new PurchaseOrderRequest();
            orderRequest.OrderStatuses = new List<string>();
            orderRequest.OrderStages = new List<string>();
            if (inputRequest.IsSearchClicked)
            {
                if (inputRequest.strOrderStatusIds != null && inputRequest.strOrderStatusIds != string.Empty)
                {
                    orderRequest.OrderStatuses = inputRequest.strOrderStatusIds.Split(",").ToList();
                    orderRequest.OrderStatuses = orderRequest.OrderStatuses.Where(x => x != "" && x != Constants.All).Distinct().ToList();
                }

                if (inputRequest.ToDate.HasValue)
                {
                    var localToReqDate = new DateTime(inputRequest.ToDate.Value.Year, inputRequest.ToDate.Value.Month, inputRequest.ToDate.Value.Day, 23, 59, 59);
                    orderRequest.ToReqOrdDate = localToReqDate;
                }
                if (inputRequest.FromDate.HasValue)
                {
                    var localFromReqDate = new DateTime(inputRequest.FromDate.Value.Year, inputRequest.FromDate.Value.Month, inputRequest.FromDate.Value.Day, 0, 0, 0);
                    orderRequest.FromReqOrdDate = localFromReqDate;
                }

                if (!string.IsNullOrWhiteSpace(inputRequest.Title))
                {
                    orderRequest.OrderTitle = inputRequest.Title;
                }
                if (!string.IsNullOrWhiteSpace(inputRequest.SearchOrderNumber))
                {
                    orderRequest.OrderNumber = inputRequest.SearchOrderNumber;
                }

                if (!string.IsNullOrWhiteSpace(inputRequest.SupplierId))
                {
                    orderRequest.SupplierId = inputRequest.SupplierId;
                }
            }
            else
            {
                //In Process -RE, EO, EP, TA,TR
                //Ordered - O, DW,OX
                //Dispatched - DA, DV
                //Auth Req - TA, or where ord_authlevel > 0 // Changed on Robert suggestion dated 28th Dec 2020 // Auth. req. to Auth. Enq. and show only TR
                //Recieved(30 Days) - RV, PD(where ord_datereceived in last 30 days)
                if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.InProcess))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.Requisition));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryOutstanding));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryInProgress));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OnHold));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Ordered))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OrderIssued));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToWarehouse));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OrderReadyEx_Works));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.StoredAtWarehouse));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Dispatched))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToAgent));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToVessel));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedInTransit));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedByAgent));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.AuthenticationRequired))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Received30Days))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedByVessel));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.PartOrderReceivedByVessel));

                    orderRequest.ReceivedDate = DateTime.Now.AddDays(-30);
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.AuthorisationRequired))
                {
                    orderRequest.ShowOnlyAuthRequired = inputRequest.ShowOnlyAuthRequired;
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
                    orderRequest.OrderStages.Add(EnumsHelper.GetKeyValue(PurchaseOrderStage.AuthorisedEnquiry));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.TenderAwaitingAuthorization))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation));
                    orderRequest.OrderStages.Add(EnumsHelper.GetKeyValue(PurchaseOrderStage.Enquiry));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Requisitions))
                {
                    orderRequest.OrderStages.Add(EnumsHelper.GetKeyValue(PurchaseOrderStage.Requisition));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.Requisition));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Enquiries))
                {
                    orderRequest.OrderStages.Add(EnumsHelper.GetKeyValue(PurchaseOrderStage.Enquiry));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryInProgress));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryOutstanding));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.OnHold))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OnHold));
                }
            }
            orderRequest.VesselId = inputRequest.VesselId;
            orderRequest.CoyId = inputRequest.AccountCompanyId;

            var value = new Dictionary<string, object>()
            {
                { "request", orderRequest },
                {"pageRequest",pagedRequest }
            };
            
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/OrdersPWA"));
            PagedResponse<List<PurchaseOrderResponse>> response = await PostAsync<PagedResponse<List<PurchaseOrderResponse>>>(requestUrl, CreateHttpContent(value));

            DataTablePageResponse<List<PurchasingDetailViewModel>> result = new DataTablePageResponse<List<PurchasingDetailViewModel>>();
            result.Data = new List<PurchasingDetailViewModel>();

            PurchaseOrderRequestViewModel purchaseOrderUrl = null;

            if (response != null && response.Result != null && response.Result.Any())
            {
                response.Result.ForEach(x =>
                {
                    purchaseOrderUrl = new PurchaseOrderRequestViewModel();
                    purchaseOrderUrl.VesselId = x.VesselId;
                    purchaseOrderUrl.ToDate = inputRequest.ToDate;
                    purchaseOrderUrl.FromDate = inputRequest.FromDate;
                    purchaseOrderUrl.VesselName = x.VesselName;
                    purchaseOrderUrl.AccountCompanyId = x.CoyId;
                    purchaseOrderUrl.OrderNumber = x.OrderNumber;
                    purchaseOrderUrl.POStage = inputRequest.POStage;

                    result.Data.Add(new PurchasingDetailViewModel
                    {
                        PurchaseOrderUrl = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderUrl)),
                        VesselId = CommonUtil.GetEncryptedVessel(_provider, x.VesselId, x.VesselName, x.CoyId),

                        OrderNumber = x.OrderNumber,
                        ProtectedOrderNumber = _provider.CreateProtector("OrderNumber").Protect(x.OrderNumber),
                        AccountingCompanyId = x.CoyId,
                        ProtectedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Protect(x.CoyId),
                        Title = x.OrderName ?? "",
                        IsHighPriority = x.OrderPriority == EnumsHelper.GetKeyValue(PurchaseOrderPriority.Normal) ? false : true,
                        Status = x.StatusId,
                        StatusDescription = x.StatusDescription,
                        DateEntered = x.RequestedDate,
                        Supplier = x.SupplierName ?? "",
                        Cost = x.TotalAmount,
                        OsCost = x.OutstandingAmount,
                        Agent = x.AgentName ?? "",
                        Currency = x.Currency ?? "",
                        DateOrdered = x.OrderDate,
                        Warehouse = x.WarehouseName ?? "",
                        IsHazMaterial = x.IsHazardousMaterial ? "Yes" : "No",
                        ExpectedRecPort = (x.ExpectedReceivedPort ?? ""),
                        ExpctedRecDate = x.ExpectedReceivedDate,
                        IsDamagedItem = x.IsDamagedItems ? "Yes" : "No",
                        IsPoorQuality = x.IsPoorQuality ? "Yes" : "No",
                        IsPoorPackaging = x.IsPoorPackaging ? "Yes" : "No",
                        IsIncorrectItem = x.IsIncorrectItem ? "Yes" : "No",
                        IsCertificateReceived = x.IsCertificateRequired ? (x.IsCertificateReceived ? "Yes" : "No") : "N/A",
                        EncryptedSupplierId = !string.IsNullOrWhiteSpace(x.SupplierId) ? _provider.CreateProtector("SupplierId").Protect(x.SupplierId) : "",
                        EncryptedAgentId = !string.IsNullOrWhiteSpace(x.AgentId) ? _provider.CreateProtector("AgentId").Protect(x.AgentId) : "",
                        EncryptedWarehouseId = !string.IsNullOrWhiteSpace(x.WarehouseId) ? _provider.CreateProtector("WarehouseId").Protect(x.WarehouseId) : "",
                        IsSupplierAdditionalDetailsVisible = !string.IsNullOrWhiteSpace(x.SupplierName) ? true : false,
                        IsWarehouseAdditionalDetailsVisible = !string.IsNullOrWhiteSpace(x.WarehouseName) ? true : false,
                        IsAgentAdditionalDetailsVisible = !string.IsNullOrWhiteSpace(x.AgentName) ? true : false,
                        CurrencyLabel = x.CoyCurrency ?? "",
                        IsFurtherOrderAuthorisationRequired = x.IsFurtherOrderAuthorisationRequired,
                        IsAnyHazardousMaterialInOrderLines = x.IsAnyHazardousMaterialInOrderLines,
                        OrderId = x.OrderId
                    });
                });
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;
        }

        /// <summary>
        /// Exports to excel purchase order list.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<List<PurchaseOrderResponse>> ExportToExcelPurchaseOrderList(PurchaseOrderRequestViewModel inputRequest)
        {
            PurchaseOrderRequest orderRequest = new PurchaseOrderRequest();
            orderRequest.OrderStatuses = new List<string>();
            orderRequest.OrderStages = new List<string>();


            if (inputRequest.IsSearchClicked)
            {
                if (inputRequest.strOrderStatusIds != null && inputRequest.strOrderStatusIds != string.Empty)
                {
                    orderRequest.OrderStatuses = inputRequest.strOrderStatusIds.Split(",").ToList();
                    orderRequest.OrderStatuses = orderRequest.OrderStatuses.Where(x => x != "" && x != Constants.All).Distinct().ToList();
                }

                if (inputRequest.ToDate.HasValue)
                {
                    var localToReqDate = new DateTime(inputRequest.ToDate.Value.Year, inputRequest.ToDate.Value.Month, inputRequest.ToDate.Value.Day, 23, 59, 59);
                    orderRequest.ToReqOrdDate = localToReqDate;
                }
                if (inputRequest.FromDate.HasValue)
                {
                    var localFromReqDate = new DateTime(inputRequest.FromDate.Value.Year, inputRequest.FromDate.Value.Month, inputRequest.FromDate.Value.Day, 0, 0, 0);
                    orderRequest.FromReqOrdDate = localFromReqDate;
                }

                if (!string.IsNullOrWhiteSpace(inputRequest.Title))
                {
                    orderRequest.OrderTitle = inputRequest.Title;
                }
                if (!string.IsNullOrWhiteSpace(inputRequest.SearchOrderNumber))
                {
                    orderRequest.OrderNumber = inputRequest.SearchOrderNumber;
                }

                if (!string.IsNullOrWhiteSpace(inputRequest.SupplierId))
                {
                    orderRequest.SupplierId = inputRequest.SupplierId;
                }

            }
            else
            {
                //In Process -RE, EO, EP, TA,TR
                //Ordered - O, DW,OX
                //Dispatched - DA, DV
                //Auth Req - TA, or where ord_authlevel > 0 // Changed on Robert suggestion dated 28th Dec 2020 // Auth. req. to Auth. Enq. and show only TR
                //Recieved(30 Days) - RV, PD(where ord_datereceived in last 30 days)
                if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.InProcess))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.Requisition));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryOutstanding));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryInProgress));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OnHold));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Ordered))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OrderIssued));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToWarehouse));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OrderReadyEx_Works));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.StoredAtWarehouse));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Dispatched))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToAgent));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.DeliveryToVessel));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedInTransit));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedByAgent));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.AuthenticationRequired))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Received30Days))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedByVessel));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.PartOrderReceivedByVessel));

                    orderRequest.ReceivedDate = DateTime.Now.AddDays(-30);
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.AuthorisationRequired))
                {
                    orderRequest.ShowOnlyAuthRequired = inputRequest.ShowOnlyAuthRequired;
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder));
                    orderRequest.OrderStages.Add(EnumsHelper.GetKeyValue(PurchaseOrderStage.AuthorisedEnquiry));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.TenderAwaitingAuthorization))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation));
                    orderRequest.OrderStages.Add(EnumsHelper.GetKeyValue(PurchaseOrderStage.Enquiry));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Requisitions))
                {
                    orderRequest.OrderStages.Add(EnumsHelper.GetKeyValue(PurchaseOrderStage.Requisition));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.Requisition));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.Enquiries))
                {
                    orderRequest.OrderStages.Add(EnumsHelper.GetKeyValue(PurchaseOrderStage.Enquiry));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryInProgress));
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.EnquiryOutstanding));
                }
                else if (inputRequest.SearchFilter == EnumsHelper.GetKeyValue(PoStagesFilter.OnHold))
                {
                    orderRequest.OrderStatuses.Add(EnumsHelper.GetKeyValue(PurchaseOrderStatus.OnHold));
                }
            }

            orderRequest.VesselId = inputRequest.VesselId;
            orderRequest.CoyId = inputRequest.AccountCompanyId;

            var value = new Dictionary<string, object>()
            {
                { "request", orderRequest }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/OrdersPWA"));
            List<PurchaseOrderResponse> response = await PostAsyncAutoPaged<PurchaseOrderResponse>(requestUrl, value, 50);

            return response;
        }

        /// <summary>
        /// Gets the purchase order order count by vessel.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PurchaseOrderSummaryViewModel> GetPurchaseOrderOrderCountByVessel(PurchasingFilter request)
        {
            request.AwaitingSnrAuthorizationDaysLimit = 4; // default is 4
            request.RequisitionLimit = 5; // default is 5;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/Summary"));
            InventoryManager response = await PostAsync<InventoryManager>(requestUrl, CreateHttpContent(request));
            PurchaseOrderSummaryViewModel result = new PurchaseOrderSummaryViewModel();
            if (response != null)
            {
                result.OrderInProcessCount = response.OrderInProcessCount;
                result.OrderedCount = response.OrderedCount;
                result.OrderDeliveryOnTheWayCount = response.OrderDeliveryOnTheWayCount;
                result.AuthorisationCount = response.AuthorisationCount;
                result.RecievedIn30DaysCount = response.RecievedIn30DaysCount;
                result.AuthRequiredCount = response.AuthRequiredCount;
                result.AwaitingAuthorisationCount = response.AwaitingAuthorisationCount;
                result.RequisitionOlderThanXDaysCount = response.RequisitionOlderThanXDaysCount;
                result.RequisitionCount = response.RequisitionCount;
                result.EnquiriesCount = response.EnquiriesCount;
                result.OnHoldCount = response.OnHoldCount;

                result.IsOrderInProcessUrgent = response.IsOrderInProcessUrgent;
                result.IsOrderedUrgent = response.IsOrderedUrgent;
                result.IsOrderDeliveryOnTheWayUrgent = response.IsOrderDeliveryOnTheWayUrgent;
                result.IsAuthorisationUrgent = response.IsAuthorisationUrgent;
                result.IsRecievedIn30DaysUrgent = response.IsRecievedIn30DaysUrgent;
                result.IsAuthRequiredUrgent = response.IsAuthRequiredUrgent;
                result.IsAwaitingAuthorisationUrgent = response.IsAwaitingAuthorisationUrgent;
                result.IsEnquiriesUrgent = response.IsEnquiriesUrgent;
                result.IsOnHoldUrgent = response.IsOnHoldUrgent;
                result.IsRequisitionUrgent = response.IsRequisitionUrgent;
            }

            PurchaseOrderRequestViewModel overViewRequest = new PurchaseOrderRequestViewModel();
            overViewRequest.FromDate = null;
            overViewRequest.ToDate = null;
            overViewRequest.IsSearchClicked = false;
            overViewRequest.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.InProcess);
            overViewRequest.ActiveMobileTabClass = Constants.Tab1;
            result.OverViewURL = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(overViewRequest));

            result.InProcessURL = SetUpPurchaseOrderUrl(request, PoStagesFilter.InProcess);
            result.OrderURL = SetUpPurchaseOrderUrl(request, PoStagesFilter.Ordered);
            result.DispatchedURL = SetUpPurchaseOrderUrl(request, PoStagesFilter.Dispatched);
            result.AuthEnqURL = SetUpPurchaseOrderUrl(request, PoStagesFilter.AuthenticationRequired);
            result.RecievedURL = SetUpPurchaseOrderUrl(request, PoStagesFilter.Received30Days);
            result.TenderAwaitingAuthURL = SetUpPurchaseOrderUrl(request, PoStagesFilter.TenderAwaitingAuthorization);
            result.RequisitionsURL = SetUpPurchaseOrderUrl(request, PoStagesFilter.Requisitions);
            result.EnquiriesURL = SetUpPurchaseOrderUrl(request, PoStagesFilter.Enquiries);
            result.OnHoldUrl = SetUpPurchaseOrderUrl(request, PoStagesFilter.OnHold);

            PurchaseOrderRequestViewModel poRequest = new PurchaseOrderRequestViewModel();
            poRequest.FromDate = null;
            poRequest.ToDate = null;
            poRequest.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.AuthorisationRequired);
            poRequest.ShowOnlyAuthRequired = true;
            poRequest.ActiveMobileTabClass = Constants.Tab2;
            result.AuthRequiredURL = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(poRequest));

            result.AwaitingSnrAuthPriority = response.AwaitingSnrAuthPriority;
            result.RequisitionPriority = response.RequisitionPriority;
            result.OrderedPriority = response.OrderedPriority;
            result.TenderAwaitingAuthPriority = response.TenderAwaitingAuthPriority;

            return result;
        }

        /// <summary>
        /// Sets up purchase order URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="StageName">Name of the stage.</param>
        /// <returns></returns>
        private string SetUpPurchaseOrderUrl(PurchasingFilter request, PoStagesFilter StageName)
        {
            PurchaseOrderRequestViewModel poRequest = new PurchaseOrderRequestViewModel();
            poRequest.FromDate = null;//request.OrderFromDate.GetValueOrDefault();
            poRequest.ToDate = null;// request.OrderToDate.GetValueOrDefault();
            poRequest.IsSearchClicked = false;
            poRequest.POStage = EnumsHelper.GetKeyValue(StageName);
            poRequest.ActiveMobileTabClass = Constants.Tab2;

            string purchaseOrderURL = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(poRequest));
            return purchaseOrderURL;
        }

        /// <summary>
        /// Gets the open orders by vessel ids.
        /// </summary>
        /// <param name="userMenuItem">The user menu item.</param>
        /// <returns></returns>
        public async Task<OpenOrderSummaryViewModel> GetOpenOrdersByVesselIds(UserMenuItem userMenuItem)
        {
            var value = new Dictionary<string, object>()
            {
                { "userMenuItem", userMenuItem }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/OpenOrder"));
            InventoryManager response = await PostAsync<InventoryManager>(requestUrl, CreateHttpContent(userMenuItem));

            OpenOrderSummaryViewModel openOrderSummaryViewModel = new OpenOrderSummaryViewModel()
            {
                OrderInProcessCount = response.OrderInProcessCount,
                OrderedCount = response.OrderedCount,
                OrderDeliveryOnTheWayCount = response.OrderDeliveryOnTheWayCount,
                AuthorisationCount = response.AuthorisationCount,
                RecievedIn30DaysCount = response.RecievedIn30DaysCount,
            };

            return openOrderSummaryViewModel;
        }

        /// <summary>
        /// Gets the purchase order header detail by coy idand order no.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public async Task<PurchaseOrderDetailViewModel> GetPurchaseOrderHeaderDetailByCoyIdandOrderNo(string accountingCompanyId, string orderNumber)
        {
            var value = new Dictionary<string, object>()
            {
                { "accountingCompanyId", accountingCompanyId },
                { "orderNumber", orderNumber }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/OrderHeader"));

            OrderPreview response = await PostAsync<OrderPreview>(requestUrl, CreateHttpContent(value));

            PurchaseOrderDetailViewModel result = new PurchaseOrderDetailViewModel();
            result = new PurchaseOrderDetailViewModel();
            if (response != null)
            {
                result.OrderStatusName = response.OrderStatusName;
                result.AccountCodeDescription = response.AccountCodeDescription;
                result.AccountId = response.AccountId;
                result.AccountingCompanyId = response.AccountingCompanyId;
                result.ProtectedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Protect(response.AccountingCompanyId);
                result.OrderNumber = response.OrderNumber;
                result.ProtectedOrderNumber = _provider.CreateProtector("OrderNumber").Protect(response.OrderNumber);
                result.OrderStatus = response.OrderStatus;
                result.OrderStage = response.OrderStage;
                result.OrderType = response.OrderType;
                result.Title = response.OrderTitle;
                result.VesselId = response.VesselId;
                result.VesselName = response.VesselName;
                result.OrderPriority = response.OrderPriority;
                result.IsHighPriority = response.OrderPriority == EnumsHelper.GetKeyValue(PurchaseOrderPriority.Normal) ? false : true;
                result.OrderId = response.OrderId;
            }

            return result;
        }

        /// <summary>
        /// Gets the order lines.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<OrderLineViewModel>>> GetOrderLines(DataTablePageRequest<string> pageRequest, string accountingCompanyId, string orderNumber)
        {
            DataTablePageResponse<List<OrderLineViewModel>> result = new DataTablePageResponse<List<OrderLineViewModel>>();
            result.Data = new List<OrderLineViewModel>();

            var value = new Dictionary<string, object>()
            {
                { "accountingCompanyId", accountingCompanyId },
                { "orderNumber", orderNumber }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/OrderLines"));

            List<OrderLineDetail> response = await PostAsyncAutoPaged<OrderLineDetail>(requestUrl, value, 50);

            if (response != null)
            {
                response.ForEach(x =>
                {
                    result.Data.Add(new OrderLineViewModel
                    {
                        Notes = x.Notes,
                        PartName = x.PartName,
                        MakersReference = x.MakersReference,
                        DrawingPosition = x.DrawingPosition,
                        UOM = x.UOM,
                        ROB = x.ROB != null ? x.ROB.ToString() : "",
                        REQ = x.REQ != null ? x.REQ.ToString() : "",
                        ENQ = x.ENQ != null ? x.ENQ.ToString() : "",
                        ORD = x.ORD != null ? x.ORD.ToString() : "",
                        REC = x.REC != null ? x.REC.ToString() : "",
                        ItemNo = x.ItemNo
                    });
                });
            }

            result.RecordsFiltered = response.Count;
            result.RecordsTotal = response.Count;

            return result;
        }

        /// <summary>
        /// Posts the get suppliers.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<SupplierDetailViewModel>>> PostGetSuppliers(DataTablePageRequest<string> pageRequest, SupplierPurchaseOrderRequest request)
        {
            List<SupplierDetailViewModel> supplierList = new List<SupplierDetailViewModel>();

            DataTablePageResponse<List<SupplierDetailViewModel>> result = new DataTablePageResponse<List<SupplierDetailViewModel>>();
            result.Data = new List<SupplierDetailViewModel>();

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/Suppliers"));

            List<SupplierOrderDetail> response = await PostAsync<List<SupplierOrderDetail>>(requestUrl, CreateHttpContent(request));

            PurchaseOrderRequestViewModel requestViewModel = new PurchaseOrderRequestViewModel();
            string data = _provider.CreateProtector("PurchaseOrder").Unprotect(request.PurchaseOrderRequestURL);
            requestViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseOrderRequestViewModel>(data);
            PurchaseOrderRequestViewModel purchaseOrderUrl = null;
            AuthorizeQuoteRequestViewModel authorizeQuoteUrl = null;
            if (response != null)
            {
                response.ForEach(x =>
                {
                    purchaseOrderUrl = new PurchaseOrderRequestViewModel();
                    purchaseOrderUrl.VesselId = requestViewModel.VesselId;
                    purchaseOrderUrl.ToDate = requestViewModel.ToDate;
                    purchaseOrderUrl.FromDate = requestViewModel.FromDate;
                    purchaseOrderUrl.VesselName = requestViewModel.VesselName;
                    purchaseOrderUrl.AccountCompanyId = x.AccountingCompanyId;
                    purchaseOrderUrl.OrderNumber = x.OrderNumber;
                    purchaseOrderUrl.POStage = requestViewModel.POStage;

                    authorizeQuoteUrl = new AuthorizeQuoteRequestViewModel();
                    authorizeQuoteUrl.VesselId = requestViewModel.VesselId;
                    authorizeQuoteUrl.AccountingCompanyId = x.AccountingCompanyId;
                    authorizeQuoteUrl.OrderNumber = x.OrderNumber;
                    authorizeQuoteUrl.PurchaseOrderStage = x.OrderStage;
                    authorizeQuoteUrl.PurchaseOrderStatus = x.OrderStatus;
                    authorizeQuoteUrl.SupplierOrderId = x.SupplierOrderId;
                    authorizeQuoteUrl.SupplierOrderStatus = x.SupplierOrderStatus;

                    supplierList.Add(new SupplierDetailViewModel()
                    {
                        PurchaseOrderRequestUrl = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderUrl)),
                        AuthorizeQuoteRequestUrl = _provider.CreateProtector("AuthorizeQuote").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(authorizeQuoteUrl)),
                        PurchaseOrderRequestVesselId = CommonUtil.GetEncryptedVessel(_provider, purchaseOrderUrl.VesselId, purchaseOrderUrl.VesselName, x.AccountingCompanyId),
                        SupplierOrderId = x.SupplierOrderId,
                        ProtectedSupplierOrderId = _provider.CreateProtector("SupplierOrderId").Protect(x.SupplierOrderId),
                        SupplierName = x.SupplierCompanyName,
                        Country = x.SupplierCountry,
                        SupplierOrderStatus = x.SupplierOrderStatus,
                        SupplierOrderStatusName = EnumsHelper.GetEnumNameFromKeyValue(typeof(PurchaseOrderStatus), x.SupplierOrderStatus),
                        OrderStatus = x.OrderStatus, //Purchase Order - 
                        OrderStage = x.OrderStage, // Purchase Order 
                        IsOrderAuthorised = x.IsOrderAuthorised.HasValue ? x.IsOrderAuthorised.Value : false,
                        IsMarcas = x.IsMarcas,
                        IsContracted = x.IsContracted,
                        IsPreferred = x.IsPreferred,
                        SupplierPreferredReason = x.SupplierPreferredReason,
                        IsCompleteQuote = x.IsCompleteQuote,
                        MaxExWorksDays = x.ExWorkDays.HasValue ? x.ExWorkDays.Value : 0,
                        Total = (x.TotalCost + x.FreightCost) == null ? string.Format(Constants.TwoDecimal_NumberFormat, 0) : string.Format(Constants.TwoDecimal_NumberFormat, x.TotalCost + x.FreightCost),
                        IsBaseTotalNotMatched = Math.Abs(Math.Round(Convert.ToDouble(x.BaseTotalCost), 2) - Math.Round(Convert.ToDouble(x.SupplierOrderLineTotalCost), 2)) > Constants.SupplierBaseTotalMargin,
                        Base = (x.BaseTotalCost + x.BaseFreightCost) == null ? string.Format(Constants.TwoDecimal_NumberFormat, 0) : string.Format(Constants.TwoDecimal_NumberFormat, x.BaseTotalCost + x.BaseFreightCost),
                        Cur = x.SupplierOrderCurrency,
                        RFQIssued = x.RFQIssueDate,
                        BaseCurrency = x.BaseCurrency,
                        GoodsCost = x.TotalCost.HasValue ? string.Format(Constants.TwoDecimal_NumberFormat, x.TotalCost.Value) : string.Format(Constants.TwoDecimal_NumberFormat, 0),
                        IsDiscountApplied = x.HasDiscountApplied,
                        HasNotes = x.HasSupplierNotes,
                        FreightCost = x.FreightCost.HasValue ? string.Format(Constants.TwoDecimal_NumberFormat, x.FreightCost.Value) : string.Format(Constants.TwoDecimal_NumberFormat, 0),
                        SupplierNotes = x.SupplierNotes,
                        PortName = x.PortName,
                        IsValidForEnquiry = x.OrderStage == EnumsHelper.GetKeyValue(PurchaseOrderStage.Enquiry) && x.OrderStatus != EnumsHelper.GetKeyValue(PurchaseOrderStatus.OrderCancelled) && x.OrderStatus != EnumsHelper.GetKeyValue(PurchaseOrderStatus.OnHold),
                        QuoteReceivedDate = x.QuoteReceivedDate,
                        SupplierRating = GetSupplierRating(x.SupplierRating),
                        DocumentCount = x.DocumentCount,
                        SupplierCompanyId = x.SupplierCompanyId
                    });
                });

                result.Data.AddRange(supplierList.Where(x => !string.IsNullOrWhiteSpace(x.SupplierOrderStatus) && x.SupplierOrderStatus.Equals(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder))));
                result.Data.AddRange(supplierList.Where(x => string.IsNullOrWhiteSpace(x.SupplierOrderStatus) || (!string.IsNullOrWhiteSpace(x.SupplierOrderStatus) && !x.SupplierOrderStatus.Equals(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder)))));

                result.RecordsFiltered = response.Count;
                result.RecordsTotal = response.Count;
            }

            return result;
        }

        /// <summary>
        /// Posts the get supplier quote header.
        /// </summary>
        /// <param name="supplierOrderId">The supplier order identifier.</param>
        /// <returns></returns>
        public async Task<ViewQuoteHeaderViewModel> PostGetSupplierQuoteHeader(string supplierOrderId)
        {
            ViewQuoteHeaderViewModel result = new ViewQuoteHeaderViewModel();

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/SupplierQuoteHeader"));

            SupplierOrderQuoteDetail response = await PostAsync<SupplierOrderQuoteDetail>(requestUrl, CreateHttpContent(supplierOrderId));

            if (response != null)
            {
                decimal? costBeforeDiscount = ((response.TotalCost ?? 0) * (100 / (100 - (response.DiscountPercent))));
                decimal costBeforeDiscountFormatted = Math.Round((costBeforeDiscount ?? 0), 2);
                decimal? discountedCost = costBeforeDiscountFormatted - (response.TotalCost ?? 0);
                decimal discountedCostFormatted = Math.Round((discountedCost ?? 0), 2);

                result = new ViewQuoteHeaderViewModel
                {
                    SupplierOrderId = response.SupplierOrderId,
                    ProtectedSupplierOrderId = _provider.CreateProtector("SupplierOrderId").Protect(response.SupplierOrderId),
                    AccountingCompanyId = response.AccountingCompanyId,
                    OrderNumber = response.OrderNumber,
                    SupplierName = response.SupplierName,
                    Currency = response.CurrencyId,
                    SupplierReference = response.SupplierReference,
                    DateReceived = response.DateReceived == null ? "" : response.DateReceived.Value.ToString("dd MMM yyyy"),
                    SupplierOrderPortName = response.SupplierOrderPortName,
                    PortCountryCode = response.PortCountryCode,
                    IsHazardousGoods = response.IsHazardousGoods != null ? response.IsHazardousGoods.Value ? "Yes" : "No" : "",
                    DeliveryCost = string.Format(Constants.TwoDecimal_NumberFormat, response.DeliveryCost != null ? response.DeliveryCost : 0),
                    DiscountPercent = string.Format(Constants.TwoDecimal_NumberFormat, response.DiscountPercent != null ? response.DiscountPercent : 0),
                    IsProformaRequested = response.IsProformaRequested ? "Yes" : "No",
                    ExWorkCountryId = response.ExWorkCountryId,
                    ExWorkLocation = response.ExWorkLocation,
                    MaxExWorkDays = response.MaxExWorkDays.HasValue ? response.MaxExWorkDays.ToString() : "",
                    ValidForDays = ((response.QuoteValidTillDate != null && response.DateReceived != null) ? (response.QuoteValidTillDate.Value - response.DateReceived.Value).TotalDays : 30).ToString(),
                    Cost = string.Format(Constants.TwoDecimal_NumberFormat, costBeforeDiscountFormatted),
                    Discount = string.Format(Constants.TwoDecimal_NumberFormat, discountedCostFormatted),
                    NetCost = string.Format(Constants.TwoDecimal_NumberFormat, response.TotalCost.HasValue ? response.TotalCost : 0),
                    IsCompleteQuote = response.IsCompleteQuote != null ? response.IsCompleteQuote.Value ? "Yes" : "No" : "",
                    QuoteStatus = response.SupplierOrderStatus != null && response.SupplierOrderStatus.Equals(EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingTechnicalInformation)) ? "Awaiting Technical Details" : "Submit For Authorisation",
                    Notes = response.SupplierOrderNotes,
                    ProtectedOrderNumber = _provider.CreateProtector("OrderNumber").Protect(response.OrderNumber),
                    ProtectedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Protect(response.AccountingCompanyId),
                    SparePartTypeId = response.SparePartTypeId
                };
            }
            return result;
        }

        /// <summary>
        /// Posts the get supplier quote order line quote.
        /// </summary>
        /// <param name="supplierOrderId">The supplier order identifier.</param>
        /// <param name="orderLineQuoteType">Type of the order line quote.</param>
        /// <returns></returns>
        public async Task<List<ViewQuoteOrderLineViewModel>> PostGetSupplierQuoteOrderLineQuote(string supplierOrderId, OrderLineQuoteType orderLineQuoteType)
        {
            var value = new Dictionary<string, object>()
            {
                { "supplierOrderId", supplierOrderId },
                { "orderLineQuoteType", orderLineQuoteType }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/SupplierQuoteOrderLine"));

            List<SupplierOrderLineQuoteDetail> response = await PostAsyncAutoPaged<SupplierOrderLineQuoteDetail>(requestUrl, value, 300);

            List<ViewQuoteOrderLineViewModel> result = new List<ViewQuoteOrderLineViewModel>();

            if (response != null)
            {
                response.ForEach(x =>
                {
                    result.Add(new ViewQuoteOrderLineViewModel
                    {
                        PartName = x.PartName,
                        MakersReference = x.MakersReference,
                        QtyEnq = x.QuantityEnquired != null ? x.QuantityEnquired : 0,
                        SupplierQty = x.Units.HasValue ? x.Units.ToString() : "",
                        IsSupplierQtyMismatch = (x.QuantityEnquired ?? 0) != (x.Units ?? 0),
                        UOM = x.UnitOfMeasurement,
                        UnitPrice = x.UnitPrice.HasValue ? x.UnitPrice : 0,
                        DiscountPercent = string.Format(Constants.TwoDecimal_NumberFormat, x.DiscountPercent.HasValue ? x.DiscountPercent : 0),
                        SubTotal = string.Format(Constants.TwoDecimal_NumberFormat, (x.UnitPrice ?? 0) * (x.QuantityEnquired ?? 0) * (100 - (x.DiscountPercent ?? 0)) / 100),
                        ExDays = (x.ExWorkDays.HasValue ? x.ExWorkDays : 1).ToString(),
                        Notes = string.IsNullOrWhiteSpace(x.Notes) ? "" : x.Notes,
                        ItemNo = x.ItemNo,
                        VesselPartId = x.VesselPartId
                    });
                });
            }


            return result;
        }

        /// <summary>
        /// Get spare part type list
        /// </summary>
        /// <returns>
        /// List of type <see cref="SparePartTypeViewModel" />
        /// </returns>
        public async Task<List<SparePartTypeViewModel>> GetSparePartTypeList()
        {
            List<SparePartTypeViewModel> result = new List<SparePartTypeViewModel>();

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/SparePartType"));

            List<SparePartType> response = await GetAsync<List<SparePartType>>(requestUrl);

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    result.Add(new SparePartTypeViewModel
                    {
                        SptCode = x.SptCode,
                        SptDescription = x.SptDescription,
                        SptId = x.SptId,
                        SptName = x.SptName
                    });
                });
            }

            return result;
        }

        /// <summary>
        /// Posts the get purchase order budget list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<BudgetOrderDetailsViewModel>> PostGetPurchaseOrderBudgetList(BudgetOrderDetailRequest request)
        {
            List<BudgetOrderDetailsViewModel> budgetList = new List<BudgetOrderDetailsViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/BudgetList"));
            List<BudgetOrderDetail> response = new List<BudgetOrderDetail>();

            DateTime? startDate = null;
            DateTime? endDate = null;

            if (request.StartDate != DateTime.MinValue)
            {
                startDate = request.StartDate;
            }
            if (request.EndDate != DateTime.MinValue)
            {
                endDate = request.EndDate;
            }

            if (startDate != null && endDate != null)
            {
                response = await PostAsync<List<BudgetOrderDetail>>(requestUrl, CreateHttpContent(request));
            }

            PurchaseOrderRequestViewModel requestViewModel = new PurchaseOrderRequestViewModel();
            string data = _provider.CreateProtector("PurchaseOrder").Unprotect(request.PurchaseOrderRequestUrl);
            requestViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseOrderRequestViewModel>(data);
            PurchaseOrderRequestViewModel purchaseOrderUrl = null;
            if (response != null)
            {
                response.ForEach(x =>
                {
                    purchaseOrderUrl = new PurchaseOrderRequestViewModel();
                    purchaseOrderUrl.VesselId = requestViewModel.VesselId;
                    purchaseOrderUrl.ToDate = requestViewModel.ToDate;
                    purchaseOrderUrl.FromDate = requestViewModel.FromDate;
                    purchaseOrderUrl.VesselName = requestViewModel.VesselName;
                    purchaseOrderUrl.AccountCompanyId = x.AccountingCompanyId;
                    purchaseOrderUrl.OrderNumber = x.OrderNumber;
                    purchaseOrderUrl.POStage = requestViewModel.POStage;

                    budgetList.Add(new BudgetOrderDetailsViewModel
                    {
                        PurchaseOrderRequestUrl = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderUrl)),
                        PurchaseOrderRequestVesselId = CommonUtil.GetEncryptedVessel(_provider, purchaseOrderUrl.VesselId, purchaseOrderUrl.VesselName, x.AccountingCompanyId),
                        AccountingCompanyId = x.AccountingCompanyId,
                        CurrencyId = x.CurrencyId,
                        LocalCost = x.LocalCost == null ? "" : string.Format(Constants.TwoDecimal_NumberFormat, x.LocalCost),
                        OrderDate = x.OrderDate,
                        OrderNumber = x.OrderNumber,
                        OrderTitle = x.OrderTitle,
                        ProtectedOrderNumber = _provider.CreateProtector("OrderNumber").Protect(x.OrderNumber),
                        ProtectedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Protect(x.AccountingCompanyId),
                    });
                });

            }
            return budgetList;
        }

        /// <summary>
        /// Posts the get order invoice delivery detail.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public async Task<OrderInvoiceDeliveryDetailViewModel> PostGetOrderInvoiceDeliveryDetail(string accountingCompanyId, string orderNumber)
        {
            OrderInvoiceDeliveryDetailViewModel deliveryDetailsData = new OrderInvoiceDeliveryDetailViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/DeliveryTimes"));
            OrderInvoiceDeliveryDetail response = new OrderInvoiceDeliveryDetail();
            var request = new Dictionary<string, object>()
            {
                { "accountingCompanyId", accountingCompanyId },
                { "orderNumber", orderNumber }
            };
            response = await PostAsync<OrderInvoiceDeliveryDetail>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                deliveryDetailsData.DeliveryDays = response.DeliveryDays ?? 0;
                deliveryDetailsData.RemainingDeliveryDays = 0;
                if (response.DeliveryDate != null)
                {
                    deliveryDetailsData.RemainingDeliveryDays = Math.Max((response.DeliveryDate.Value.Date - DateTime.Now.Date).Days, 0);
                }
                deliveryDetailsData.DeliveryDate = response.DeliveryDate != null ? response.DeliveryDate.Value.ToString("dd MMM yyyy") : "";
            }

            return deliveryDetailsData;
        }

        /// <summary>
        /// Posts the get order tracker detail.
        /// </summary>
        /// <param name="coyId">The coy identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="orderStatus">The order status.</param>
        /// <returns></returns>
        public async Task<List<OrderStatusViewModel>> PostGetOrderTrackerDetail(string coyId, string orderNumber, string orderStatus)
        {
            OrderTrackerDetailViewModel deliveryDetailsData = new OrderTrackerDetailViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/OrderTracker"));
            OrderTrackerDetail response = new OrderTrackerDetail();
            var request = new Dictionary<string, object>()
            {
                { "coyId", coyId },
                { "orderNumber", orderNumber }
            };
            response = await PostAsync<OrderTrackerDetail>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                deliveryDetailsData.AuthoriseDate = response.AuthoriseDate == null ? "" : response.AuthoriseDate.Value.ToString("dd MMM yyyy");
                deliveryDetailsData.RequestedDate = response.RequestedDate == null ? "" : response.RequestedDate.Value.ToString("dd MMM yyyy");
                deliveryDetailsData.CreatedDate = response.CreatedDate == null ? "" : response.CreatedDate.Value.ToString("dd MMM yyyy");
                deliveryDetailsData.DueForDeliveryDate = response.DueForDeliveryDate == null ? "" : response.DueForDeliveryDate.Value.ToString("dd MMM yyyy");
                deliveryDetailsData.OrderDate = response.OrderDate == null ? "" : response.OrderDate.Value.ToString("dd MMM yyyy");
                deliveryDetailsData.ReceivedDate = response.ReceivedDate == null ? "" : response.ReceivedDate.Value.ToString("dd MMM yyyy");
            }
            List<OrderStatusViewModel> stageList = new List<OrderStatusViewModel>();

            if (response != null)
            {
                stageList.Add(new OrderStatusViewModel()
                {
                    OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.Created),
                    OrderStageDate = deliveryDetailsData.CreatedDate,
                    OrderStage = string.IsNullOrWhiteSpace(deliveryDetailsData.RequestedDate) ? (string.IsNullOrWhiteSpace(deliveryDetailsData.CreatedDate) ? Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted)) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.InProgress))) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.Completed))
                });

                stageList.Add(new OrderStatusViewModel()
                {
                    OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.Requested),
                    OrderStageDate = orderStatus != EnumsHelper.GetKeyValue(PurchaseOrderStatus.DraftOfRequisition) ? deliveryDetailsData.RequestedDate : "",
                    OrderStage = string.IsNullOrWhiteSpace(deliveryDetailsData.AuthoriseDate) ? (string.IsNullOrWhiteSpace(deliveryDetailsData.RequestedDate) ? Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted)) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.InProgress))) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.Completed))
                });

                stageList.Add(new OrderStatusViewModel()
                {
                    OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.Authorised),
                    OrderStageDate = deliveryDetailsData.AuthoriseDate,
                    OrderStage = string.IsNullOrWhiteSpace(deliveryDetailsData.OrderDate) ? (string.IsNullOrWhiteSpace(deliveryDetailsData.AuthoriseDate) ? Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted)) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.InProgress))) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.Completed))
                });

                if (string.IsNullOrWhiteSpace(deliveryDetailsData.OrderDate))
                {
                    stageList.Add(new OrderStatusViewModel()
                    {
                        OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.Ordered),
                        OrderStage = Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted))

                    });

                    stageList.Add(new OrderStatusViewModel()
                    {
                        OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.ExpectedDelivery),
                        OrderStage = Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted))
                    });
                }
                else
                {
                    stageList.Add(new OrderStatusViewModel()
                    {
                        OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.Ordered),
                        OrderStage = string.IsNullOrWhiteSpace(deliveryDetailsData.DueForDeliveryDate) ? (string.IsNullOrWhiteSpace(deliveryDetailsData.OrderDate) ? Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted)) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.InProgress))) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.Completed)),
                        OrderStageDate = deliveryDetailsData.OrderDate
                    });

                    stageList.Add(new OrderStatusViewModel()
                    {
                        OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.ExpectedDelivery),
                        OrderStage = string.IsNullOrWhiteSpace(deliveryDetailsData.ReceivedDate) ? (string.IsNullOrWhiteSpace(deliveryDetailsData.DueForDeliveryDate) ? Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted)) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.InProgress))) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.Completed)),
                        OrderStageDate = deliveryDetailsData.DueForDeliveryDate
                    });
                }
                if (orderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.PartOrderReceivedByVessel))
                {
                    stageList.Add(new OrderStatusViewModel()
                    {
                        OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.PartiallyReceived),
                        OrderStage = string.IsNullOrWhiteSpace(deliveryDetailsData.ReceivedDate) ? Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted)) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.Completed)),
                        OrderStageDate = orderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.PartOrderReceivedByVessel) ? deliveryDetailsData.ReceivedDate : ""
                    });
                }
                else
                {
                    stageList.Add(new OrderStatusViewModel()
                    {
                        OrderStageName = EnumsHelper.GetKeyValue(OrderTracker.Closed),
                        OrderStage = string.IsNullOrWhiteSpace(deliveryDetailsData.ReceivedDate) ? Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.NotCompleted)) : Convert.ToInt32(EnumsHelper.GetKeyValue(TrackerStatus.Completed)),
                        OrderStageDate = orderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.ReceivedByVessel) ? deliveryDetailsData.ReceivedDate : ""
                    });
                }
            }
            return stageList;
        }

        /// <summary>
        /// Posts the get current order delivery detail.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public async Task<OrderDeliveryDetailsViewModel> PostGetCurrentOrderDeliveryDetail(string accountingCompanyId, string orderNumber)
        {
            OrderDeliveryDetailsViewModel deliveryDetails = new OrderDeliveryDetailsViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/DeliveryDetail"));
            OrderDeliveryDetails response = new OrderDeliveryDetails();
            var request = new Dictionary<string, object>()
            {
                { "accountingCompanyId", accountingCompanyId },
                { "orderNumber", orderNumber }
            };
            response = await PostAsync<OrderDeliveryDetails>(requestUrl, CreateHttpContent(request));
            List<PurchaseOrderStatus> _expectedStatusList = new List<PurchaseOrderStatus>() {PurchaseOrderStatus.OrderIssued,
                                                                                                PurchaseOrderStatus.DeliveryToWarehouse,
                                                                                                PurchaseOrderStatus.DeliveryToAgent,
                                                                                                PurchaseOrderStatus.DeliveryToVessel};
            if (response != null)
            {
                deliveryDetails.Address = response.Address;
                deliveryDetails.CompanyName = response.CompanyName;
                deliveryDetails.CompanyType = response.CompanyType != null ? EnumsHelper.GetDescription(response.CompanyType.GetValueOrDefault()) : "";
                deliveryDetails.Country = response.Country;
                deliveryDetails.Email = response.Email;
                deliveryDetails.Fax = response.Fax;
                deliveryDetails.PostalCode = response.PostalCode ?? "";
                deliveryDetails.State = response.State ?? "";
                deliveryDetails.Telephone = response.Telephone;
                deliveryDetails.Town = response.Town ?? "";
                deliveryDetails.Port = response.PortName ?? "";
                deliveryDetails.IsExpectedOrderStatus = (response != null
                                            && (((response.OrderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.OnHold) || response.OrderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.OrderCancelled)) && (_expectedStatusList.Any(x => EnumsHelper.GetKeyValue(x) == response.OrderPreviousStatus) || (response.OrderPreviousStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.FreightOrder)))) || (_expectedStatusList.Any(x => EnumsHelper.GetKeyValue(x) == response.OrderStatus) || (response.OrderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.FreightOrder)))));
            }
            return deliveryDetails;
        }


        /// <summary>
        /// Posts the get summary po components.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public async Task<List<SummaryPurchaseOrderComponentViewModel>> PostGetSummaryPoComponents(string accountingCompanyId, string orderNumber)
        {
            List<SummaryPurchaseOrderComponentViewModel> purchaseOrderComponent = new List<SummaryPurchaseOrderComponentViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/SummaryComponents"));
            List<SummaryPurchaseOrderComponent> response = new List<SummaryPurchaseOrderComponent>();
            var request = new Dictionary<string, object>()
            {
                { "coyId", accountingCompanyId },
                { "orderNo", orderNumber }
            };
            response = await PostAsync<List<SummaryPurchaseOrderComponent>>(requestUrl, CreateHttpContent(request));
            if (response != null && response.Any())
            {
                foreach (SummaryPurchaseOrderComponent item in response)
                {
                    SummaryPurchaseOrderComponentViewModel componentVM = new SummaryPurchaseOrderComponentViewModel();
                    componentVM.ComponentName = string.IsNullOrWhiteSpace(item.ComponentName) ? "" : item.ComponentName;
                    componentVM.CriticalComponent = item.CriticalComponent;
                    componentVM.Designer = String.IsNullOrWhiteSpace(item.Designer) ? "" : item.Designer;
                    componentVM.Maker = string.IsNullOrWhiteSpace(item.Maker) ? "" : item.Maker;
                    componentVM.Serial = string.IsNullOrWhiteSpace(item.Serial) ? "" : item.Serial;
                    componentVM.Type = string.IsNullOrWhiteSpace(item.Type) ? "" : item.Type;
                    purchaseOrderComponent.Add(componentVM);
                }
            }
            return purchaseOrderComponent;
        }

        /// <summary>
        /// Posts the get summary po details.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public async Task<SummaryPurchaseOrderDetailsViewModel> PostGetSummaryPoDetails(string accountingCompanyId, string orderNumber)
        {
            SummaryPurchaseOrderDetailsViewModel summaryOrderDetails = new SummaryPurchaseOrderDetailsViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/SummaryDetail"));
            SummaryPurchaseOrderDetails response = new SummaryPurchaseOrderDetails();
            var request = new Dictionary<string, object>()
            {
                { "accountingCompanyId", accountingCompanyId },
                { "orderNumber", orderNumber }
            };
            response = await PostAsync<SummaryPurchaseOrderDetails>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                summaryOrderDetails.ExpectedDate = response.ExpectedDate == null ? "" : response.ExpectedDate.Value.ToString("dd MMM yyyy");
                summaryOrderDetails.ItemsCount = response.ItemsCount;
                summaryOrderDetails.PortCountry = response.PortCountry;
                summaryOrderDetails.PortName = response.PortName;
            }
            return summaryOrderDetails;
        }

        /// <summary>
        /// Posts the get summary po supplier details.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public async Task<SummaryPurchaseOrderSupplierViewModel> PostGetSummaryPoSupplierDetails(string accountingCompanyId, string orderNumber)
        {
            SummaryPurchaseOrderSupplierViewModel summarySupplierDetails = new SummaryPurchaseOrderSupplierViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/SummarySupplier"));
            SummaryPurchaseOrderSupplier response = new SummaryPurchaseOrderSupplier();
            var request = new Dictionary<string, object>()
            {
                { "coyId", accountingCompanyId },
                { "orderNo", orderNumber }
            };
            response = await PostAsync<SummaryPurchaseOrderSupplier>(requestUrl, CreateHttpContent(request));
            if (response != null)
            {
                summarySupplierDetails.AuthorisedBy = response.AuthorisedBy;
                summarySupplierDetails.CompanyDetails = AssignValueToCompanyDetailsViewModel(response.CompanyDetails);
                summarySupplierDetails.ConfirmationDate = response.ConfirmationDate == null ? "" : response.ConfirmationDate.Value.ToString("dd MMM yyyy");
                summarySupplierDetails.DateAuthorised = response.DateAuthorised == null ? "" : response.DateAuthorised.Value.ToString("dd MMM yyyy");
                summarySupplierDetails.InvoiceChasedDate = response.InvoiceChasedDate == null ? "" : response.InvoiceChasedDate.Value.ToString("dd MMM yyyy");
                summarySupplierDetails.PurchaseOrderChasedDate = response.PurchaseOrderChasedDate == null ? "" : response.PurchaseOrderChasedDate.Value.ToString("dd MMM yyyy");
                summarySupplierDetails.PurchaseOrderId = response.PurchaseOrderId;
                summarySupplierDetails.SupplierReference = response.SupplierReference;
            }
            return summarySupplierDetails;
        }


        /// <summary>
        /// Assigns the value to company details view model.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private CompanyDetailsViewModel AssignValueToCompanyDetailsViewModel(CompanyDetails input)
        {
            CompanyDetailsViewModel companyDetailsVM = new CompanyDetailsViewModel();
            if (input != null)
            {
                companyDetailsVM.CompanyName = input.CompanyName;
                companyDetailsVM.Address = input.Address;
                companyDetailsVM.Town = input.Town;
                companyDetailsVM.State = input.State;
                companyDetailsVM.PostalCode = input.PostalCode;
                companyDetailsVM.Country = input.Country;
                companyDetailsVM.Telephone = input.Telephone;
                companyDetailsVM.Fax = input.Fax;
                companyDetailsVM.Email = input.Email;
            }

            return companyDetailsVM;
        }

        /// <summary>
        /// Posts the get summary po cost.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<SummaryPurchaseOrderCostViewModel> PostGetSummaryPoCost(POSummaryCostDetailsRequest inputRequest)
        {
            SummaryPurchaseOrderCostViewModel costListVM = new SummaryPurchaseOrderCostViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/SummaryCost"));
            SummaryPurchaseOrderCost response = new SummaryPurchaseOrderCost();
            string decryptedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Unprotect(inputRequest.AccountingCompanyId);
            string decryptedOrderNumber = _provider.CreateProtector("OrderNumber").Unprotect(inputRequest.OrderNumber);
            var request = new Dictionary<string, object>()
                {
                    { "coyId", decryptedAccountingCompanyId },
                    { "orderNo", decryptedOrderNumber }
                };
            response = await PostAsync<SummaryPurchaseOrderCost>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                costListVM.Currency = response.Currency;
                costListVM.LastPOCurrencyChangeLog = response.LastPOCurrencyChangeLog;
                costListVM.ShowProposed = response.TotalProposedAmount != null && response.TotalProposedAmount > 0;
                decimal totalAmount = 0;
                decimal totalOutStanding = 0;
                decimal totalProposed = 0;

                costListVM.CostList = new List<SummaryCostViewModel>();
                if (!(inputRequest.OrderStatus == EnumsHelper.GetKeyValue(PurchaseOrderStatus.FreightOrder)))
                {
                    SummaryCostViewModel GoodServices = new SummaryCostViewModel();
                    GoodServices.LabelHeader = "Goods / Service Balance";
                    GoodServices.Amount = string.Format(Constants.TwoDecimal_NumberFormat, response.GoodsServiceAmount.HasValue ? response.GoodsServiceAmount : 0);
                    GoodServices.Outstanding = string.Format(Constants.TwoDecimal_NumberFormat, response.OutstandingGoodsServicesAmount.HasValue ? response.OutstandingGoodsServicesAmount : 0);
                    GoodServices.Proposed = string.Format(Constants.TwoDecimal_NumberFormat, response.ProposedGoodsServiceAmount.HasValue ? response.ProposedGoodsServiceAmount : 0);
                    costListVM.CostList.Add(GoodServices);

                    totalAmount = response.GoodsServiceAmount.HasValue ? response.GoodsServiceAmount.GetValueOrDefault() : 0;
                    totalOutStanding = response.OutstandingGoodsServicesAmount.HasValue ? response.OutstandingGoodsServicesAmount.GetValueOrDefault() : 0;
                    totalProposed = response.ProposedGoodsServiceAmount.HasValue ? response.ProposedGoodsServiceAmount.GetValueOrDefault() : 0;
                }

                SummaryCostViewModel Delivery = new SummaryCostViewModel();
                Delivery.LabelHeader = "Freight / Delivery Balance";
                Delivery.Amount = string.Format(Constants.TwoDecimal_NumberFormat, response.DeliveryAmount.HasValue ? response.DeliveryAmount : 0);
                Delivery.Outstanding = string.Format(Constants.TwoDecimal_NumberFormat, response.OutstandingDeliveryAmount.HasValue ? response.OutstandingDeliveryAmount : 0);
                Delivery.Proposed = string.Format(Constants.TwoDecimal_NumberFormat, response.ProposedDeliveryAmount.HasValue ? response.ProposedDeliveryAmount : 0);
                costListVM.CostList.Add(Delivery);

                if (inputRequest.OrderStage != EnumsHelper.GetKeyValue(PurchaseOrderStage.FreightOrder))
                {
                    totalAmount += response.DeliveryAmount.HasValue ? response.DeliveryAmount.GetValueOrDefault() : 0;
                    totalOutStanding += response.OutstandingDeliveryAmount.HasValue ? response.OutstandingDeliveryAmount.GetValueOrDefault() : 0;
                    totalProposed += response.ProposedDeliveryAmount.HasValue ? response.ProposedDeliveryAmount.GetValueOrDefault() : 0;

                    SummaryCostViewModel total = new SummaryCostViewModel();
                    total.LabelHeader = "Total PO";
                    total.Amount = string.Format(Constants.TwoDecimal_NumberFormat, totalAmount);
                    total.Outstanding = string.Format(Constants.TwoDecimal_NumberFormat, totalOutStanding);
                    total.Proposed = string.Format(Constants.TwoDecimal_NumberFormat, totalProposed);

                    costListVM.CostList.Add(total);
                }
            }
            return costListVM;

        }

        /// <summary>
        /// Posts the get summary po invoicing.
        /// </summary>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public async Task<List<SummaryPurchaseOrderInvoicingViewModel>> PostGetSummaryPoInvoicing(string accountingCompanyId, string orderNumber)
        {
            List<SummaryPurchaseOrderInvoicingViewModel> poInvoiceList = new List<SummaryPurchaseOrderInvoicingViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/SummaryInvoicing"));
            List<SummaryPurchaseOrderInvoicing> response = new List<SummaryPurchaseOrderInvoicing>();
            var request = new Dictionary<string, object>()
            {
                { "coyId", accountingCompanyId },
                { "orderNo", orderNumber }
            };
            response = await PostAsync<List<SummaryPurchaseOrderInvoicing>>(requestUrl, CreateHttpContent(request));
            if (response != null && response.Any())
            {
                foreach (SummaryPurchaseOrderInvoicing item in response)
                {
                    SummaryPurchaseOrderInvoicingViewModel poInvoice = new SummaryPurchaseOrderInvoicingViewModel();
                    poInvoice.Amount = string.Format(Constants.TwoDecimal_NumberFormat, item.Amount);
                    poInvoice.Currency = item.Currency;
                    poInvoice.Status = item.Status;
                    poInvoice.Reference = item.Reference;
                    poInvoice.InvoicePaidDate = item.InvoicePaidDate;
                    poInvoice.InvoiceDate = item.InvoiceDate;
                    poInvoice.StatusCategory = item.StatusCategory;
                    poInvoice.InvoiceDocumentId = item.InvoiceDocumentId;
                    poInvoiceList.Add(poInvoice);
                }
            }
            return poInvoiceList;
        }

        /// <summary>
        /// Gets the order types.
        /// </summary>
        /// <returns></returns>
        public List<PurchaseOrderType> GetOrderTypes()
        {
            List<PurchaseOrderType> orderTypes = new List<PurchaseOrderType>();

            foreach (PurchaseOrderType ordertype in Enum.GetValues(typeof(PurchaseOrderType)))
            {
                orderTypes.Add(ordertype);
            }

            return orderTypes;
        }

        /// <summary>
        /// Gets the order stage.
        /// </summary>
        /// <returns></returns>
        public List<PurchaseOrderStage> GetOrderStage()
        {
            List<PurchaseOrderStage> OrderStageList = new List<PurchaseOrderStage>();
            foreach (PurchaseOrderStage val in Enum.GetValues(typeof(PurchaseOrderStage)))
            {
                OrderStageList.Add(val);
            }

            return OrderStageList;
        }

        /// <summary>
        /// Posts the get sup details for quote authentication.
        /// </summary>
        /// <param name="supplierOrderId">The supplier order identifier.</param>
        /// <returns></returns>
        public async Task<SupplierDetailsForQuoteAuthorizationViewModel> PostGetSupDetailsForQuoteAuth(string supplierOrderId)
        {
            SupplierDetailsForQuoteAuthorizationViewModel result = new SupplierDetailsForQuoteAuthorizationViewModel();
            SupplierDetailsForQuoteAuthorization SupplierDetails = new SupplierDetailsForQuoteAuthorization();

            var request = new Dictionary<string, object>()
            {
                { "supplierOrderId", supplierOrderId },
                { "fetchFurtherAuthorisationRequired", true }
            };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/AuthQuoteSupDetails"));
            SupplierDetails = await PostAsync<SupplierDetailsForQuoteAuthorization>(requestUrl, CreateHttpContent(request));

            if (SupplierDetails != null)
            {
                result.IsSupplierGivenNotes = SupplierDetails != null && (!string.IsNullOrWhiteSpace(SupplierDetails.SupplierNotes) || SupplierDetails.SupplierOrderLineNotes.Any());
                result.IsFeedbackRequired = SupplierDetails.IsFeedbackRequired;
                result.SupplierNotes = GetSupplierNotes(SupplierDetails);
                result.IsStatusTR = SupplierDetails.SupplierOrderStatus.HasValue ? EnumsHelper.GetKeyValue(SupplierDetails.SupplierOrderStatus.Value) == EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingReview) : false;
                if (result.IsStatusTR)
                {
                    result.SupplierOrderStatus = EnumsHelper.GetKeyValue(SupplierDetails.SupplierOrderStatus.Value);
                    result.SupplierOrderStatusWarning = "This order has not been reviewed by the purchasing department";
                }

                result.SupplierName = SupplierDetails.SupplierName ?? "";
                result.PortName = SupplierDetails.Port ?? "";
                result.ExpectedWorkCountry = SupplierDetails.ExpectedWorkCountry ?? "";
                result.ExpectedWorkDays = SupplierDetails.ExpectedWorkDays + " Day(s)";
                result.IsHazardousGoods = SupplierDetails.IsHazardousGoods.GetValueOrDefault() ? "Yes" : "No";
                result.SparePartTypeDetail = SupplierDetails.SparePartTypeCode + (!string.IsNullOrWhiteSpace(SupplierDetails.SparePartTypeName) ? (" - " + SupplierDetails.SparePartTypeName) : "");
                result.FullItemsQuoted = SupplierDetails.IsFullResponse ? "Yes" : "No";
                result.IsFullItemsQuoted = SupplierDetails.IsFullResponse;
                result.IsItemsNotQuoted = SupplierDetails.ItemsNotQuotedCount > 0;
                result.ItemsNotQuotedCount = SupplierDetails.ItemsNotQuotedCount;
                result.IsProformaRequested = SupplierDetails.IsProformaRequested ? "Yes" : "No";
                result.EncryptedSupplierCompanyId = !string.IsNullOrWhiteSpace(SupplierDetails.SupplierCompanyId) ? _provider.CreateProtector("SupplierId").Protect(SupplierDetails.SupplierCompanyId) : "";
                result.QuotedAmount = String.Format(Constants.TwoDecimal_NumberFormat, SupplierDetails.QuoteAmount) + " (" + (SupplierDetails.SupplierOrderCurrrencyId ?? "") + ")";
                result.QuoteAmountInPoVesselCurrency = "+ " + String.Format(Constants.TwoDecimal_NumberFormat, SupplierDetails.QuoteAmountInPoVesselCurrency);
                result.FreightAccrualInPoVesselCurrency = "+ " + String.Format(Constants.TwoDecimal_NumberFormat, SupplierDetails.FreightAccrualInPoVesselCurrency);
                result.QuoteAmountInPoVesselCurrencyDecimal = SupplierDetails.QuoteAmountInPoVesselCurrency;
                result.FreightAccrualInPoVesselCurrencyDecimal = SupplierDetails.FreightAccrualInPoVesselCurrency;
                result.FeedbackSupplierOrderId = SupplierDetails.FeedbackSupplierOrderId;
                result.IsAuthLevelAuthorizationRequired = SupplierDetails.AuthLevel > 0;
                result.IsClientAuthRequired = SupplierDetails.OrdOwnerAuthorise;
            }

            return result;
        }

        /// <summary>
        /// Gets the supplier notes.
        /// </summary>
        /// <param name="SelectedSupplierDetails">The selected supplier details.</param>
        /// <returns></returns>
        private string GetSupplierNotes(SupplierDetailsForQuoteAuthorization SelectedSupplierDetails)
        {
            string SupplierNotes = SelectedSupplierDetails.SupplierNotes != null ? SelectedSupplierDetails.SupplierNotes + "\r\n" : "";
            int count = 0;
            foreach (string supOrderLineNotes in SelectedSupplierDetails.SupplierOrderLineNotes)
            {
                SupplierNotes += (++count) + ". " + supOrderLineNotes + "\r\n";
            }

            return SupplierNotes;
        }

        /// <summary>
        /// Posts the get vessel and office limit.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<VesselAndOfficeLimitViewModel> PostGetVesselAndOfficeLimit(AuthorizeQuoteRequestViewModel inputRequest)
        {
            VesselAndOfficeLimitViewModel result = new VesselAndOfficeLimitViewModel();
            VesselAndOfficeLimit response = new VesselAndOfficeLimit();
            var request = new Dictionary<string, object>()
            {
                { "accountingCompanyId",inputRequest.AccountingCompanyId },
                { "orderNumber", inputRequest.OrderNumber }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/VesselAndOfficeLimit"));
            response = await PostAsync<VesselAndOfficeLimit>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                result.IsVesselLimit = response.VesselLimit.HasValue;
                if (response.VesselLimit.HasValue)
                {
                    result.VesselLimit = ConvertZeroDecimalValue(response.VesselLimit.Value) + " (USD)";
                }
                else
                {
                    result.VesselLimit = "No Limit";
                }

                result.IsOfficeLimit = response.OfficeLimit.HasValue;
                if (response.OfficeLimit.HasValue)
                {
                    result.OfficeLimit = ConvertZeroDecimalValue(response.OfficeLimit.Value) + " (USD)";
                }
                else
                {
                    result.OfficeLimit = "No Limit";
                }

                result.IsLevel1Limit = response.Level1Limit.HasValue;
                if (response.Level1Limit.HasValue)
                {
                    result.Level1Limit = ConvertZeroDecimalValue(response.Level1Limit.Value) + " (USD)";
                }
                else
                {
                    result.Level1Limit = "No Limit";
                }

                result.IsLevel2Limit = response.Level2Limit.HasValue;
                if (response.Level2Limit.HasValue)
                {
                    result.Level2Limit = ConvertZeroDecimalValue(response.Level2Limit.Value) + " (USD)";
                }
                else
                {
                    result.Level2Limit = "No Limit";
                }

                result.IsLevel3Limit = response.Level3Limit.HasValue;
                if (response.Level3Limit.HasValue)
                {
                    result.Level3Limit = ConvertZeroDecimalValue(response.Level3Limit.Value) + " (USD)";
                }
                else
                {
                    result.Level3Limit = "No Limit";
                }

                result.IsClientLimit = response.ClientLimit.HasValue;
                if (response.ClientLimit.HasValue)
                {
                    result.ClientLimit = ConvertZeroDecimalValue(response.ClientLimit.Value) + " (USD)";
                }
                else
                {
                    result.ClientLimit = "No Limit";
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the zero decimal value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private string ConvertZeroDecimalValue(decimal value)
        {
            return String.Format(Constants.TwoDecimal_NumberFormat, value);
            //return value != (decimal)0.00 ? value.ToString("#,#.##") : "0.00";
        }

        /// <summary>
        /// Posts the get order details for quote authentication.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<OrderDetailsForQuoteAuthorizationViewModel> PostGetOrderDetailsForQuoteAuth(AuthorizeQuoteRequestViewModel inputRequest)
        {
            OrderDetailsForQuoteAuthorizationViewModel result = new OrderDetailsForQuoteAuthorizationViewModel();
            OrderDetailsForQuoteAuthorization OrderDetails = new OrderDetailsForQuoteAuthorization();
            var request = new Dictionary<string, object>()
            {
                { "accountingCompanyId",inputRequest.AccountingCompanyId },
                { "orderNumber", inputRequest.OrderNumber }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/AuthQuoteOrderDetails"));
            OrderDetails = await PostAsync<OrderDetailsForQuoteAuthorization>(requestUrl, CreateHttpContent(request));

            if (OrderDetails != null)
            {
                result.OrderTitle = OrderDetails.OrderTitle ?? "";
                result.AccountId = OrderDetails.AccountId;
                result.OrderNumber = OrderDetails.OrderNumber;
                result.AccountingCompanyId = OrderDetails.AccountingCompanyId;
                result.AccountDescription = OrderDetails.AccountDescription;
                result.VesselCurrencyId = OrderDetails.VesselCurrencyId ?? "";
                result.VesselId = OrderDetails.VesselId;
                if (OrderDetails.Auxiliaries != null)
                {
                    result.Auxiliaries = new AuxiliaryDetailViewModel();
                    result.Auxiliaries.Aux7Id = OrderDetails.Auxiliaries.Aux7Id;
                    result.Auxiliaries.Aux7Name = OrderDetails.Auxiliaries.Aux7Name;
                    result.Auxiliaries.Aux8Id = OrderDetails.Auxiliaries.Aux8Id;
                    result.Auxiliaries.Aux8Name = OrderDetails.Auxiliaries.Aux8Name;
                    result.Auxiliaries.Aux9Id = OrderDetails.Auxiliaries.Aux9Id;
                    result.Auxiliaries.Aux9Name = OrderDetails.Auxiliaries.Aux9Name;
                    result.Auxiliaries.ClaimsDescription = OrderDetails.Auxiliaries.ClaimsDescription;
                    result.Auxiliaries.ClaimsId = OrderDetails.Auxiliaries.ClaimsId;
                    result.Auxiliaries.ClaimsShortCode = OrderDetails.Auxiliaries.ClaimsShortCode;
                    result.Auxiliaries.CoyId = OrderDetails.Auxiliaries.CoyId;
                    result.Auxiliaries.CrewRankDescription = OrderDetails.Auxiliaries.CrewRankDescription;
                    result.Auxiliaries.CrewRankId = OrderDetails.Auxiliaries.CrewRankId;
                    result.Auxiliaries.DepartmentId = OrderDetails.Auxiliaries.DepartmentId;
                    result.Auxiliaries.DepartmentName = OrderDetails.Auxiliaries.DepartmentName;
                    result.Auxiliaries.EmployeeId = OrderDetails.Auxiliaries.EmployeeId;
                    result.Auxiliaries.EmployeeName = OrderDetails.Auxiliaries.EmployeeName;
                    result.Auxiliaries.EntityVesselId = OrderDetails.Auxiliaries.EntityVesselId;
                    result.Auxiliaries.EntityVesselName = OrderDetails.Auxiliaries.EntityVesselName;
                    result.Auxiliaries.ExpenseId = OrderDetails.Auxiliaries.ExpenseId;
                    result.Auxiliaries.ExpenseName = OrderDetails.Auxiliaries.ExpenseName;
                    result.Auxiliaries.General1Description = OrderDetails.Auxiliaries.General1Description;
                    result.Auxiliaries.General1Id = OrderDetails.Auxiliaries.General1Id;
                    result.Auxiliaries.General3Description = OrderDetails.Auxiliaries.General3Description;
                    result.Auxiliaries.General3Id = OrderDetails.Auxiliaries.General3Id;
                    result.Auxiliaries.GroupId = OrderDetails.Auxiliaries.GroupId;
                    result.Auxiliaries.GroupName = OrderDetails.Auxiliaries.GroupName;
                    result.Auxiliaries.NationalityDescription = OrderDetails.Auxiliaries.NationalityDescription;
                    result.Auxiliaries.NationalityId = OrderDetails.Auxiliaries.NationalityId;
                    result.Auxiliaries.ProjectName = OrderDetails.Auxiliaries.ProjectName;
                    result.Auxiliaries.ProjectId = OrderDetails.Auxiliaries.ProjectId;
                    result.Auxiliaries.SeasonalDescription = OrderDetails.Auxiliaries.SeasonalDescription;
                    result.Auxiliaries.SeasonalId = OrderDetails.Auxiliaries.SeasonalId;
                    result.Auxiliaries.VesselManagementId = OrderDetails.Auxiliaries.VesselManagementId;
                    result.Auxiliaries.VesselName = OrderDetails.Auxiliaries.VesselName;
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the authorise quote.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> PostAuthoriseQuote(AuthoriseQuoteRequest request)
        {
            bool saveStatus = false;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/AuthoriseQuote"));
            saveStatus = await PostAsync<bool>(requestUrl, CreateHttpContent(request));
            return saveStatus;
        }

        /// <summary>
        /// Posts the get vessel part detail asynchronous.
        /// </summary>
        /// <param name="vesselPartId">The vessel part identifier.</param>
        /// <returns>
        /// PartDetailViewModel
        /// </returns>
        public async Task<PartDetailViewModel> PostGetVesselPartDetailAsync(string vesselPartId)
        {
            string queryString = "vesselPartId=" + vesselPartId;
            PartDetail response = new PartDetail();
            PartDetailViewModel result = new PartDetailViewModel();
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/VesselPartDetail"), queryString);
            response = await PostAsync<PartDetail>(requestUrl, CreateHttpContent(queryString));

            if (response != null)
            {
                result.PartName = response.PartName;
                result.VesselPartId = response.VesselPartId;
                result.MakerReferenceNumber = response.MakerReferenceNumber;
                result.UnitOfMeasurement = response.UnitOfMeasurement;
                result.PlateSheetNumber = response.PlateSheetNumber;
                result.DrawingPosition = response.DrawingPosition;
                result.HarmonizedNumber = response.HarmonizedNumber;
                result.HarmonizedWeight = response.HarmonizedWeight;
                result.IsStockItem = response.IsStockItem;
                result.RecommendedROB = response.RecommendedROB;
                result.TechnicalMinStock = response.TechnicalMinStock;
                result.OperationalMinStock = response.OperationalMinStock;
                result.IsCertificateRequired = response.IsCertificateRequired;
                result.CertificateName = response.CertificateName;
                result.IsDangerousGoods = response.IsDangerousGoods;
                result.DangerousGoodsDescription = response.DangerousGoodsDescription;
                result.Weight = response.Weight;
                result.Volume = response.Volume;
                result.ShelfLifeDuration = response.ShelfLifeDuration;
                result.ShelfLifeDurationUnit = response.ShelfLifeDurationUnit;
                result.VolumeUnit = response.VolumeUnit;
                result.WeightUnit = response.WeightUnit;
            }
            return result;
        }

        /// <summary>
        /// Posts the get order authentication limit detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// OrderAuthorisationLimitDetailViewModel
        /// </returns>
        public async Task<OrderAuthorisationLimitDetailViewModel> PostGetOrderAuthLimitDetail(Dictionary<string, object> input)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/OrderAuthLimitDetail"));
            OrderAuthorisationLimitDetailViewModel result = new OrderAuthorisationLimitDetailViewModel();
            OrderAuthorisationLimitDetail response = new OrderAuthorisationLimitDetail();
            response = await PostAsync<OrderAuthorisationLimitDetail>(requestUrl, CreateHttpContent(input));
            if (response != null)
            {
                result.AuthLevel1Limit = response.AuthLevel1Limit;
                result.ClientLimit = response.ClientLimit;
                result.IsAboveAuthLevel1Limit = response.IsAboveAuthLevel1Limit;
                result.IsAboveClientLimit = response.IsAboveClientLimit;
                result.IsAboveOfficeLimit = response.IsAboveOfficeLimit;
                result.IsAboveVesselLimit = response.IsAboveVesselLimit;
                result.IsContractedAccount = response.IsContractedAccount;
                result.LimitCurrency = response.LimitCurrency;
                result.OfficeLimit = response.OfficeLimit;
                result.VesselLimit = response.VesselLimit;
            }
            return result;
        }

        /// <summary>
        /// Posts the get order authorisers.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<AuthoriserDetailViewModel>> PostGetOrderAuthorisers(Dictionary<string, object> input)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/OrderAuthorisers"));
            List<AuthoriserDetailViewModel> result = new List<AuthoriserDetailViewModel>();
            List<AuthoriserDetail> response = new List<AuthoriserDetail>();
            response = await PostAsync<List<AuthoriserDetail>>(requestUrl, CreateHttpContent(input));
            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    AuthoriserDetailViewModel authoriser = new AuthoriserDetailViewModel();
                    authoriser.AuthorisationDate = x.AuthorisationDate;
                    authoriser.RoleName = x.RoleName;
                    authoriser.UserName = x.UserName;
                    result.Add(authoriser);
                });
            }
            return result;
        }

        /// <summary>
        /// Posts the check order authorisation pending.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// PendingOrderAuthorisationDetailViewModel
        /// </returns>
        public async Task<PendingOrderAuthorisationDetailViewModel> PostCheckOrderAuthorisationPending(Dictionary<string, object> input)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/CheckOrderAuthorisationPending"));
            PendingOrderAuthorisationDetailViewModel result = new PendingOrderAuthorisationDetailViewModel();
            PendingOrderAuthorisationDetail response = new PendingOrderAuthorisationDetail();
            response = await PostAsync<PendingOrderAuthorisationDetail>(requestUrl, CreateHttpContent(input));
            if (response != null)
            {
                result.IsClientAuthorisationProcessed = response.IsClientAuthorisationProcessed;
                result.IsClientAuthorisationRequired = response.IsClientAuthorisationRequired;
                result.IsCurrentUserAuthorisationRequired = response.IsCurrentUserAuthorisationRequired;
                result.IsFurtherOrderAuthorisationRequired = response.IsFurtherOrderAuthorisationRequired;
                result.IsOrderAuthorisationProcessed = response.IsOrderAuthorisationProcessed;
            }
            return result;
        }

        /// <summary>
        /// Posts the authorise order.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// AuthoriserDetailViewModel
        /// </returns>
        public async Task<AuthoriserDetailViewModel> PostAuthoriseOrder(Dictionary<string, object> input)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/AuthoriseOrder"));
            AuthoriserDetailViewModel result = new AuthoriserDetailViewModel();
            AuthoriserDetail response = new AuthoriserDetail();
            response = await PostAsync<AuthoriserDetail>(requestUrl, CreateHttpContent(input));
            if (response != null)
            {
                result.AuthorisationDate = response.AuthorisationDate;
                result.RoleName = response.RoleName;
                result.UserName = response.UserName;
            }
            return result;
        }

        /// <summary>
        /// Posts the client authorise order.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// bool
        /// </returns>
        public async Task<bool> PostClientAuthoriseOrder(Dictionary<string, object> input)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/ClientAuthorisation"));
            bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(input));
            return response;
        }

        private string GetSupplierRating(string rating)
        {
            if (rating == "A")
                return EnumsHelper.GetDescription(SupplierRating.Star5);
            else if (rating == "B")
                return EnumsHelper.GetDescription(SupplierRating.Star4);
            else if (rating == "C")
                return EnumsHelper.GetDescription(SupplierRating.Star3);
            else if (rating == "D")
                return EnumsHelper.GetDescription(SupplierRating.Star2);
            else if (rating == "E")
                return EnumsHelper.GetDescription(SupplierRating.Star1);
            else
                return EnumsHelper.GetDescription(SupplierRating.Default);
        }

        /// <summary>
        /// Gets the approval purchase order.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<ApprovalPurchaseOrderResponseViewModel>> GetApprovalPurchaseOrder(ApprovalPurchaseOrderRequestViewModel input)
        {
            ApprovalPurchaseOrderRequest request = new ApprovalPurchaseOrderRequest()
            {
                FleetId = input.FleetId,
                MenuType = input.MenuType,
                VesselId = input.VesselId
            };

            if (!String.IsNullOrWhiteSpace(input.NodeType) && input.NodeType.Equals(EnumsHelper.GetKeyValue(ApprovalPurchaseOrderNodes.PendingApproval)))
            {
                request.OrderStatuses = new List<string> { EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder) };
                request.ShowOnlyAuthRequired = true;
            }
            else if (!String.IsNullOrWhiteSpace(input.NodeType) && input.NodeType.Equals(EnumsHelper.GetKeyValue(ApprovalPurchaseOrderNodes.TenderAwaitingAuthorization)))
            {
                request.OrderStatuses = new List<string> { EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation) };
                request.OrderStages= new List<string> { EnumsHelper.GetKeyValue(PurchaseOrderStage.Enquiry) };
            }
            
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/GetApprovalPurchaseOrder"));
            List<PurchaseOrderResponse> response = await PostAsync<List<PurchaseOrderResponse>>(requestUrl, CreateHttpContent(request));

            List<ApprovalPurchaseOrderResponseViewModel> result = new List<ApprovalPurchaseOrderResponseViewModel>();
            PurchaseOrderRequestViewModel purchaseOrderUrl = null;

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    purchaseOrderUrl = new PurchaseOrderRequestViewModel();
                    purchaseOrderUrl.VesselId = x.VesselId;
                    purchaseOrderUrl.VesselName = x.VesselName;
                    purchaseOrderUrl.AccountCompanyId = x.CoyId;
                    purchaseOrderUrl.OrderNumber = x.OrderNumber;

                    if (input.NodeType.Equals(EnumsHelper.GetKeyValue(ApprovalPurchaseOrderNodes.TenderAwaitingAuthorization)))
                    {
                        purchaseOrderUrl.ActiveMobileTabClass = Constants.DropdownTab3;
                    }

                    result.Add(new ApprovalPurchaseOrderResponseViewModel
                    {
                        PurchaseOrderUrl = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderUrl)),
                        EncryptedVesselId = CommonUtil.GetEncryptedVessel(_provider, x.VesselId, x.VesselName, x.CoyId),
                        ProtectedOrderNumber = _provider.CreateProtector("OrderNumber").Protect(x.OrderNumber),
                        ProtectedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Protect(x.CoyId),

                        VesselName = x.VesselName,
                        AccountingCompanyId = x.CoyId,
                        OrderNumber = x.OrderNumber,
                        Status = x.StatusId,
                        Title = x.OrderName ?? "",
                        PriorityDescription = x.OrderPriority,
                        Type = x.Type,
                        DateOriginated = x.RequestedDate,
                        AuthLevel = x.AuthLevel,
                        AuthClientLimit = x.AuthClientLimit,
                        AuthLevel1Limit = x.AuthLevel1Limit,
                        AuthOfficeLimit = x.AuthOfficeLimit,
                        AuthVesselLimit = x.AuthVesselLimit,
                        HasClientAuthorised = x.HasClientAuthorised != null ? Convert.ToBoolean(x.HasClientAuthorised) : false,
                    });
                });
            }

            return result;
        }
        
        /// <summary>
        /// Gets the approval purchase order list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<ApprovalPurchaseOrderResponseViewModel>>> GetApprovalPurchaseOrderList(DataTablePageRequest<string> pageRequest, ApprovalPurchaseOrderRequestViewModel inputRequest)
        {
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

            ApprovalPurchaseOrderRequest request = new ApprovalPurchaseOrderRequest()
            {
                FleetId = inputRequest.FleetId,
                MenuType = inputRequest.MenuType,
                VesselId = inputRequest.VesselId
            };


            if (!String.IsNullOrWhiteSpace(inputRequest.NodeType) && inputRequest.NodeType.Equals(EnumsHelper.GetKeyValue(ApprovalPurchaseOrderNodes.PendingApproval)))
            {
                request.OrderStatuses = new List<string> { EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderReadyForOrder) };
                request.ShowOnlyAuthRequired = true;
            }
            else if (!String.IsNullOrWhiteSpace(inputRequest.NodeType) && inputRequest.NodeType.Equals(EnumsHelper.GetKeyValue(ApprovalPurchaseOrderNodes.TenderAwaitingAuthorization)))
            {
                request.OrderStatuses = new List<string> { EnumsHelper.GetKeyValue(PurchaseOrderStatus.TenderAwaitingAuthorisation) };
                request.OrderStages = new List<string> { EnumsHelper.GetKeyValue(PurchaseOrderStage.Enquiry) };
            }
            var value = new Dictionary<string, object>()
            {
                { "request", request },
                {"pageRequest",pagedRequest }
            };
           
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Purchasing/GetApprovalPurchaseOrderPaged"));
            PagedResponse<List<PurchaseOrderResponse>> response = await PostAsync<PagedResponse<List<PurchaseOrderResponse>>>(requestUrl, CreateHttpContent(value));

            DataTablePageResponse<List<ApprovalPurchaseOrderResponseViewModel>> result = new DataTablePageResponse<List<ApprovalPurchaseOrderResponseViewModel>>();

            result.Data = new List<ApprovalPurchaseOrderResponseViewModel>();

            PurchaseOrderRequestViewModel purchaseOrderUrl = null;

            if (response != null && response.Result != null && response.Result.Any())
            {
                response.Result.ForEach(x =>
                {
                    purchaseOrderUrl = new PurchaseOrderRequestViewModel();
                    purchaseOrderUrl.VesselId = x.VesselId;
                    purchaseOrderUrl.VesselName = x.VesselName;
                    purchaseOrderUrl.AccountCompanyId = x.CoyId;
                    purchaseOrderUrl.OrderNumber = x.OrderNumber;

                    if (inputRequest.NodeType.Equals(EnumsHelper.GetKeyValue(ApprovalPurchaseOrderNodes.TenderAwaitingAuthorization)))
                    {
                        purchaseOrderUrl.ActiveMobileTabClass = Constants.DropdownTab3;
                    }

                    result.Data.Add(new ApprovalPurchaseOrderResponseViewModel
                    {
                        PurchaseOrderUrl = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderUrl)),
                        EncryptedVesselId = CommonUtil.GetEncryptedVessel(_provider, x.VesselId, x.VesselName, x.CoyId),
                        ProtectedOrderNumber = _provider.CreateProtector("OrderNumber").Protect(x.OrderNumber),
                        ProtectedAccountingCompanyId = _provider.CreateProtector("AccountingCompanyId").Protect(x.CoyId),

                        VesselName = x.VesselName,
                        AccountingCompanyId = x.CoyId,
                        OrderNumber = x.OrderNumber,
                        Status = x.StatusId,
                        Title = x.OrderName ?? "",
                        PriorityDescription = x.OrderPriority,
                        Type = x.Type,
                        DateOriginated = x.RequestedDate,
                        AuthLevel = x.AuthLevel,
                        AuthClientLimit = x.AuthClientLimit,
                        AuthLevel1Limit = x.AuthLevel1Limit,
                        AuthOfficeLimit = x.AuthOfficeLimit,
                        AuthVesselLimit = x.AuthVesselLimit,
                        HasClientAuthorised = x.HasClientAuthorised != null ? Convert.ToBoolean(x.HasClientAuthorised) : false,
                    });
                });
            }
            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;
        }
    }
}
