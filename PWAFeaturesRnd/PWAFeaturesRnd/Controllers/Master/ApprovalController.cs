using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.ViewModels.Approval;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.Defect;
using PWAFeaturesRnd.ViewModels.Inspection;
using PWAFeaturesRnd.ViewModels.JSA;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// Approval Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ApprovalController : AuthenticatedController
	{
		#region Private Properties

		/// <summary>
		/// The provider
		/// </summary>
		private IDataProtectionProvider _provider;

		/// <summary>
		/// The shared client
		/// </summary>
		private readonly SharedClient _sharedClient;

		/// <summary>
		/// The notification client
		/// </summary>
		private readonly PurchasingClient _purchasingClient;

		/// <summary>
		/// The marine client
		/// </summary>
		private readonly MarineClient _marineClient;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="ApprovalController" /> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <param name="sharedClient">The shared client.</param>
		/// <param name="purchasingClient">The purchasing client.</param>
		/// <param name="marineClient"> The marine client</param>
		public ApprovalController(IDataProtectionProvider provider, SharedClient sharedClient, PurchasingClient purchasingClient, MarineClient marineClient)
		{
			_provider = provider;
			_sharedClient = sharedClient;
			_purchasingClient = purchasingClient;
			_marineClient = marineClient;
		}

		#endregion

		#region Views

		/// <summary>
		/// Lists the specified approval reuqest.
		/// </summary>
		/// <param name="ApprovalReuqest">The approval reuqest.</param>
		/// <returns></returns>
		public async Task<IActionResult> List(string FleetRequest, string ApprovalReuqest)
		{
			ApprovalListViewModel requestViewModel = new ApprovalListViewModel();

			if (!string.IsNullOrWhiteSpace(ApprovalReuqest))
			{
				requestViewModel = CommonUtil.GetDecryptedRequest<ApprovalListViewModel>(_provider, Constants.ApprovalEncryptionText, ApprovalReuqest);
			}

			if (!string.IsNullOrWhiteSpace(FleetRequest))
			{
				requestViewModel.DashboardParameter = CommonUtil.GetDecryptedFleetRequest(_provider, FleetRequest);
				requestViewModel.ActiveMobileTabClass = SetTab(Constants.ApprovalListPageKey, requestViewModel.ActiveMobileTabClass, Constants.Tab1);
			}

			ApprovalListViewModel sessionViewModel = null;
			string sessionData = GetApprovalFilter();

			if (!String.IsNullOrWhiteSpace(sessionData))
			{
				sessionViewModel = CommonUtil.GetDecryptedRequest<ApprovalListViewModel>(_provider, Constants.ApprovalEncryptionText, sessionData);
				
				if (sessionViewModel != null)
				{
					requestViewModel.HeaderNodeShortCode = sessionViewModel.HeaderNodeShortCode;
					requestViewModel.NodeShortCode = sessionViewModel.NodeShortCode;
					requestViewModel.ActiveMobileTabClass = SetTab(Constants.ApprovalListPageKey, sessionViewModel.ActiveMobileTabClass, Constants.Tab1);
					if (sessionViewModel.DashboardParameter != null && string.IsNullOrWhiteSpace(FleetRequest))
					{
						requestViewModel.DashboardParameter = sessionViewModel.DashboardParameter;
					}
				}
			}
			
			if (requestViewModel.DashboardParameter == null)
			{
				NavigationTreeViewModel node = null;
				NavigationTreeViewModel navigationTreeVM = SetNavigationTreeViewModel();
				navigationTreeVM.AllowFleetSelection = true;
				
				_sharedClient.AccessToken = GetAccessToken();
				List<NavigationTreeViewModel> response = await _sharedClient.GetNavigationTreeTopLevel(navigationTreeVM);


				if (response != null && response.Any())
				{
					if (navigationTreeVM.UserType == UserType.Internal)
					{
						node = response.FirstOrDefault(x => x.UserMenuItemType == UserMenuItemType.MyResponsibilities);
					}
					
					if(node == null)
					{
						node = response.FirstOrDefault();
					}

					requestViewModel.DashboardParameter = new DashboardParameter
					{
						MenuType = node.UserMenuTypeShortCode,
						Title = node.Title
					};
				}
			}
			string encryptedObject = CommonUtil.GetEncryptedURL(_provider, Constants.ApprovalEncryptionText, requestViewModel);

			SetApprovalFilter(encryptedObject);
			requestViewModel.ActiveMobileTabClass = SetTab(Constants.ApprovalListPageKey, requestViewModel.ActiveMobileTabClass, Constants.Tab1);
			return View(requestViewModel);
		}

		#endregion

		#region Actions

		/// <summary>
		/// Sets the approval filters.
		/// </summary>
		/// <param name="headerNode">The header node.</param>
		/// <param name="shortCode">The short code.</param>
		/// <returns></returns>
		public IActionResult SetApprovalFilters(string headerNode, string shortCode)
		{
			ApprovalListViewModel sessionViewModel = new ApprovalListViewModel();

			string sessionData = GetApprovalFilter();
			if (!String.IsNullOrWhiteSpace(sessionData))
			{
				sessionViewModel = CommonUtil.GetDecryptedRequest<ApprovalListViewModel>(_provider, Constants.ApprovalEncryptionText, sessionData);
				sessionViewModel.HeaderNodeShortCode = headerNode;
				sessionViewModel.NodeShortCode = shortCode;
				sessionViewModel.ActiveMobileTabClass = Constants.Tab2;
			}

			string encryptedObject = CommonUtil.GetEncryptedURL(_provider, Constants.ApprovalEncryptionText, sessionViewModel);

			SetApprovalFilter(encryptedObject);
			return new JsonResult(new { data = sessionViewModel });
		}

		/// <summary>
		/// The Get Approval Summary method
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetApprovalSummary(ApprovalSummaryRequestViewModel request)
		{
			_sharedClient.AccessToken = GetAccessToken();
			List<ApprovalSummaryResponseViewModel> response = await _sharedClient.GetApprovalSummary(request);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the approval purchase order.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetApprovalPurchaseOrder(ApprovalPurchaseOrderRequestViewModel request)
		{
			_purchasingClient.AccessToken = GetAccessToken();
			string vesselId = String.Empty;

			if (!string.IsNullOrWhiteSpace(request.VesselId))
			{
				string decryptedVesselId = _provider.CreateProtector("Vessel").Unprotect(request.VesselId);
				vesselId = decryptedVesselId.Split(Constants.Separator)[0];
			}

			request.VesselId = vesselId;
			List<ApprovalPurchaseOrderResponseViewModel> response = await _purchasingClient.GetApprovalPurchaseOrder(request);
			return new JsonResult(new { data = response });
		}
        /// <summary>
        /// Gets the approval purchase order list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetApprovalPurchaseOrderList(DataTablePageRequest<string> pageRequest, ApprovalPurchaseOrderRequestViewModel input)
		{
			List<ApprovalPurchaseOrderResponseViewModel> result = new List<ApprovalPurchaseOrderResponseViewModel>();
			_purchasingClient.AccessToken = GetAccessToken();
			string vesselId = String.Empty;

			if (!string.IsNullOrWhiteSpace(input.VesselId))
			{
				string decryptedVesselId = _provider.CreateProtector("Vessel").Unprotect(input.VesselId);
				vesselId = decryptedVesselId.Split(Constants.Separator)[0];
			}

			input.VesselId = vesselId;
			DataTablePageResponse<List<ApprovalPurchaseOrderResponseViewModel>> response = await _purchasingClient.GetApprovalPurchaseOrderList(pageRequest, input);

			return new JsonResult(new DataTablePageResponse<List<ApprovalPurchaseOrderResponseViewModel>>
			{
				Draw = pageRequest.Draw,
				RecordsFiltered = response.RecordsFiltered,
				Data = response.Data,
				RecordsTotal = response.RecordsTotal
			});
		}

		/// <summary>
		/// Gets the defect pending closures.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetDefectPendingClosures(DefectListViewModel input)
		{
			_marineClient.AccessToken = GetAccessToken();

			input.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.Completed); // To get completed status
			List<DefectWorkBasketResponseViewModel> response = await _marineClient.GetDefectList(input);

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the Panned Maintenance pending closures.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetPMSPendingClosures(ApprovalPmsRequestViewModel input)
		{
			_marineClient.AccessToken = GetAccessToken();

			input.WorkOrderStatusIds = new List<string> { EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder) }; // Pending closure
			List<ApprovalPmsResponseViewModel> response = await _marineClient.GetPmsPendingApprovalList(input);

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the Panned Maintenance pending Reschedule Requests.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetPMSRescheduleRequests(ApprovalPmsRescheduleRequestViewModel input)
		{
			_marineClient.AccessToken = GetAccessToken();

			List<ApprovalPmsRescheduleResponseViewModel> response = await _marineClient.GetPmsRescheduleRequestApprovallList(input);

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the Inspection & Audit pending closures.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetInspectionAuditPendingClosure(ApprovalInspectionAuditRequestViewModel input)
		{
			InspectionRequestViewModel inspectionRequestViewModel = new InspectionRequestViewModel();
			inspectionRequestViewModel.VesselId = input.EncryptedVesselId;
			inspectionRequestViewModel.FleetId = input.FleetId;
			inspectionRequestViewModel.MenuType = input.MenuType;
			_marineClient.AccessToken = GetAccessToken();

			InspectionTypeDetailRequestViewModel inspectionTypeInput = new InspectionTypeDetailRequestViewModel();
			inspectionTypeInput.Ves_Id = inspectionRequestViewModel.VesselId;

            if (!string.IsNullOrWhiteSpace(inspectionTypeInput.Ves_Id))
            {
				string vesselId = _provider.CreateProtector("Vessel").Unprotect(inspectionRequestViewModel.VesselId);
				inspectionRequestViewModel.VesselId = vesselId.Split(Constants.Separator)[0];
				inspectionRequestViewModel.VesselName = vesselId.Split(Constants.Separator)[1];
            }

            List<InspectionTypeDetailViewModel> inspectionTypeList = await _marineClient.GetInspectionTypeWithVesselTypeFilter(inspectionTypeInput);

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();

			if (!String.IsNullOrWhiteSpace(input.NodeType) && input.NodeType.Equals(EnumsHelper.GetKeyValue(ApprovalInspectionAuditNodes.InspectionPendingClosure)))
            {
				List<string> typeInspection = inspectionTypeList.Where(x => x.IsAuditType == false).Select(x => x.InspectionTypeId).ToList();
				inspectionRequestViewModel.InspectionTypeIds = typeInspection;
				inspectionRequestViewModel.InspectionType = InspectionDashboardType.PendingClosureByOfficeType;
			}

			if (!String.IsNullOrWhiteSpace(input.NodeType) && input.NodeType.Equals(EnumsHelper.GetKeyValue(ApprovalInspectionAuditNodes.AuditPendingClosure)))
			{
				List<string> typeAudit = inspectionTypeList.Where(x => x.IsAuditType == true).Select(x => x.InspectionTypeId).ToList();
				inspectionRequestViewModel.InspectionTypeIds = typeAudit;
				inspectionRequestViewModel.InspectionType = InspectionDashboardType.AuditPendingClosureByOfficeType;
			}

			inspectionRequestViewModel.InspectionFilter = InspectionsFilter.Complete;
			inspectionRequestViewModel.IsInspection = true;
			inspectionRequestViewModel.IsPendingClosure = true;
			
			DataTablePageResponse<List<VesselInspectionViewModel>> response = await _marineClient.GetVesselInspectionDetailList(pageRequest, inspectionRequestViewModel);

			return new JsonResult(new { data = response.Data });
		}
		/// <summary>
		/// Gets the JSA pending approvals.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetJSAPendingApprovals(ApprovalJSARequestViewModel input)
		{
			JSAListViewModel requestVm = new JSAListViewModel();
			requestVm.EncryptedVesselId = input.EncryptedVesselId;
			requestVm.IsSearchClicked = false;
			requestVm.StageName = EnumsHelper.GetKeyValue(JSAStage.PendingOfficeApproval);

			_marineClient.AccessToken = GetAccessToken();
			List<JsaJobDetailResponseViewModel> result = await _marineClient.GetJSAList(requestVm);

			return new JsonResult((new { data = result }));
		}

        /// <summary>
        /// Gets the jsa pending approvals list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJSAPendingApprovalsList(DataTablePageRequest<string> pageRequest, ApprovalJSARequestViewModel input)
		{
			_marineClient.AccessToken = GetAccessToken();

            DataTablePageResponse<List<JsaJobDetailResponseViewModel>> response = await _marineClient.GetJSAListPaged(pageRequest, input);

			return new JsonResult(new DataTablePageResponse<List<JsaJobDetailResponseViewModel>>
			{
				Draw = pageRequest.Draw,
				RecordsFiltered = response.RecordsFiltered,
				Data = response.Data,
				RecordsTotal = response.RecordsTotal
			});
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// Sets the navigation TreeView model.
		/// </summary>
		/// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
		[NonAction]
		private NavigationTreeViewModel SetNavigationTreeViewModel()
		{
			NavigationTreeViewModel navigationTreeViewModel = new NavigationTreeViewModel();
			navigationTreeViewModel.Exclusion = new List<UserMenuItemType>();

			navigationTreeViewModel.UserType = (UserType)Enum.Parse(typeof(UserType), HttpContext.Session.GetString("UserType"));
			if (navigationTreeViewModel.UserType == UserType.Client)
			{
				navigationTreeViewModel.PreloadUserFleet = false;
				navigationTreeViewModel.Exclusion.AddRange(new List<UserMenuItemType> { UserMenuItemType.MyResponsibilities, UserMenuItemType.MyFleets, UserMenuItemType.MyOffices, UserMenuItemType.MotoMoco });
			}
			else
			{
				navigationTreeViewModel.PreloadUserFleet = true;
				navigationTreeViewModel.Exclusion.AddRange(new List<UserMenuItemType> { UserMenuItemType.MotoMoco });
			}

			return navigationTreeViewModel;
		}

		#endregion
	}
}
