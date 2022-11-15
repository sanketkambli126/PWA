using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.ExportToExcel;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.PlannedMaintenance;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.ExportToExcel;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.PlannedMaintenance;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
	public class PlannedMaintenanceController : AuthenticatedController
	{
		/// <summary>
		/// The DocumentClient
		/// </summary>
		private DocumentClient _documentClient;

		/// <summary>
		/// The client
		/// </summary>
		private readonly MarineClient _marineClient;

		/// <summary>
		/// The provider
		/// </summary>
		private IDataProtectionProvider _provider;

		/// <summary>
		/// The SharedClient
		/// </summary>
		private SharedClient _sharedClient;

		/// <summary>
		/// The marine WCF client
		/// </summary>
		private MarineWCFClient _marineWCFClient;

		/// <summary>
		/// The notification client
		/// </summary>
		private readonly NotificationClient _notificationClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlannedMaintenanceController" /> class.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="provider"></param>
		/// <param name="sharedClient"></param>
		/// <param name="documentClient"></param>
		/// <param name="notificationClient"></param>
		/// <param name="marineWCFClient"></param>
		public PlannedMaintenanceController(MarineClient client, IDataProtectionProvider provider, SharedClient sharedClient, DocumentClient documentClient, NotificationClient notificationClient, MarineWCFClient marineWCFClient)
		{
			_marineClient = client;
			_provider = provider;
			_sharedClient = sharedClient;
			_documentClient = documentClient;
			AccessibleModules = new List<string> { EnumsHelper.GetKeyValue(Modules.PMS) };
			_notificationClient = notificationClient;
			_marineWCFClient = marineWCFClient;
		}

		/// <summary>
		/// Lists the specified planned maintenance URL.
		/// </summary>
		/// <param name="PlannedMaintenance">The planned maintenance.</param>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <returns></returns>
		public IActionResult List(string PlannedMaintenance, string VesselId)
		{
			PlannedMaintenanceListViewModel pmsVM = new PlannedMaintenanceListViewModel();

			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);

			pmsVM = CommonUtil.GetDecryptedRequest<PlannedMaintenanceListViewModel>(_provider, Constants.PMSList, PlannedMaintenance);
			pmsVM = GetPMSListRequestObject(pmsVM);
			string EncryptPlannedMaintenance = CommonUtil.GetEncryptedURL(_provider, Constants.PMSList, pmsVM);
			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey);
			SetSessionDetail(pageKey, null, EncryptPlannedMaintenance);
			RemoveSessionFilter(_provider, pageKey, null, decreptedString.Split(Constants.Separator)[0]);

			var SessionData = GetSessionFilter(pageKey);
			string data = _provider.CreateProtector("PMSList").Unprotect(SessionData);
			pmsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceListViewModel>(data);
			pmsVM.EncryptedVesselId = VesselId;
			pmsVM.VesselName = decreptedString.Split(Constants.Separator)[1];
			pmsVM.ActiveMobileTabClass = SetTab(pageKey, pmsVM.ActiveMobileTabClass, Constants.Tab2);

			return View(pmsVM);
		}

		/// <summary>
		/// Gets the PMS header summary.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetPMSHeaderSummary(PlannedMaintenanceListViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			MaintenanceDashboardResponseViewModel response = await _marineClient.PostGetMaintenanceDashboardDetail(request);
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the work basket details list.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkBasketDetailsList(PlannedMaintenanceListViewModel input)
		{
			_marineClient.AccessToken = GetAccessToken();

			List<WorkBasketDetailResponseViewModel> response = await _marineClient.PostGetVesselWorkBasketDetail(input);

			if (response != null && response.Any())
			{
				RecordDiscussionRequestViewModel pmsRecordRequest1 = new RecordDiscussionRequestViewModel();
				pmsRecordRequest1.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.PlannedMaintenance));
				pmsRecordRequest1.ReferenceIds = response.Where(x => !x.IsDefectWorkOrder).Select(x => x.WorkOrderId).ToList();

				_notificationClient.AccessToken = GetAccessToken();
				List<RecordDiscussionResponse> pmsRecordResponse1 = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(pmsRecordRequest1);

				IEnumerable<RecordDiscussionResponse> pmsFilteredRecordResponse = pmsRecordResponse1.Where(x => x.ChannelCount > 0 || x.NotesCount > 0);

				foreach (var item in pmsFilteredRecordResponse)
				{
					WorkBasketDetailResponseViewModel pmsObj = response.FirstOrDefault(x => !x.IsDefectWorkOrder && x.WorkOrderId == item.ReferenceIdentifier);
					if (pmsObj != null)
					{
						NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
						{
							CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.PlannedMaintenance)),
							ReferenceIdentifier = item.ReferenceIdentifier
						};

						pmsObj.ChannelCount = item.ChannelCount;
						pmsObj.NotesCount = item.NotesCount;
						pmsObj.MessageDetailsJSON = JsonConvert.SerializeObject(newMessageDetails);
					}
				}

				RecordDiscussionRequestViewModel defectRecordRequest = new RecordDiscussionRequestViewModel();
				defectRecordRequest.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.DefectWorkOrder));
				defectRecordRequest.ReferenceIds = response.Where(x => x.IsDefectWorkOrder).Select(x => x.DwoId).ToList();

				_notificationClient.AccessToken = GetAccessToken();
				List<RecordDiscussionResponse> defectRecordResponse = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(defectRecordRequest);

				IEnumerable<RecordDiscussionResponse> defectFilteredRecordResponse = defectRecordResponse.Where(x => x.ChannelCount > 0 || x.NotesCount > 0);

				foreach (var item in defectFilteredRecordResponse)
				{
					WorkBasketDetailResponseViewModel defectObj = response.FirstOrDefault(x => x.IsDefectWorkOrder && x.DwoId == item.ReferenceIdentifier);
					if (defectObj != null)
					{
						NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
						{
							CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.DefectWorkOrder)),
							ReferenceIdentifier = item.ReferenceIdentifier
						};

						defectObj.ChannelCount = item.ChannelCount;
						defectObj.NotesCount = item.NotesCount;
						defectObj.MessageDetailsJSON = JsonConvert.SerializeObject(newMessageDetails);
					}
				}
			}

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Sets the page parameter.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public IActionResult SetPageParameter(PlannedMaintenanceListViewModel input)
		{
			PlannedMaintenanceListViewModel request = new PlannedMaintenanceListViewModel();

			request.EncryptedVesselId = input.EncryptedVesselId;
			request.FromDate = input.FromDate;
			request.ToDate = input.ToDate;
			request.StageName = input.StageName;
			request.StatusIds = input.StatusIds;
			request.PriorityIds = input.PriorityIds;
			request.RescheduledIds = input.RescheduledIds;
			request.ResponsibilityIds = input.ResponsibilityIds;
			request.JobTypeIds = input.JobTypeIds;
			request.OtherFilters = input.OtherFilters;
			request.isSearchedClick = input.isSearchedClick;
			request.ComponentTitle = input.ComponentTitle;

			request.SelectedWBStatusIds = input.StatusIds != null && input.StatusIds.Any() ? string.Join(",", input.StatusIds.Select(x => x)) : string.Empty;
			request.SelectedWBPriorityIds = input.PriorityIds != null && input.PriorityIds.Any() ? string.Join(",", input.PriorityIds.Select(x => x)) : string.Empty;
			request.SelectedWBResponsibilityIds = input.ResponsibilityIds != null && input.ResponsibilityIds.Any() ? string.Join(",", input.ResponsibilityIds.Select(x => x)) : string.Empty;
			request.SelectedWBRescheduledIds = input.RescheduledIds != null && input.RescheduledIds.Any() ? string.Join(",", input.RescheduledIds.Select(x => x)) : string.Empty;
			request.SelectedWBJobTypeIds = input.JobTypeIds != null && input.JobTypeIds.Any() ? string.Join(",", input.JobTypeIds.Select(x => x)) : string.Empty;
			request.SelectedOtherFilters = input.OtherFilters != null && input.OtherFilters.Any() ? string.Join(",", input.OtherFilters.Select(x => x)) : string.Empty;

			if (!string.IsNullOrWhiteSpace(input.TopSystemAreaId))
			{
				request.TopSystemAreaId = input.TopSystemAreaId;
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(input.ComponentId))
				{
					request.ComponentId = input.ComponentId;
					request.ParentComponentId = input.ComponentId;
				}
				else
				{
					request.CategoryId = input.CategoryId;
					request.ParentComponentId = input.ParentComponentId;
					request.ComponentId = input.ParentComponentId;
				}
			}

			string plannedMaintenanceUrl = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey), plannedMaintenanceUrl, input.EncryptedVesselId);

			return new JsonResult(new { data = request });
		}

		/// <summary>
		/// Gets the work basket history details list.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkBasketHistoryDetailsList(PlannedMaintenanceListViewModel input)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<WorkHistoryResponseViewModel> response = await _marineClient.PostGetClosedWorkOrderHistory(input);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the work basket history details all list.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkBasketHistoryDetailsAllList(PlannedMaintenanceListViewModel input)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<WorkBasketAllDetailViewModel> response = await _marineClient.PostGetAllWorkBasketList(input);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Details the specified planned maintenance details request URL.
		/// </summary>
		/// <param name="PlannedMaintenanceDetails">The planned maintenance details.</param>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public IActionResult Detail(string PlannedMaintenanceDetails, string VesselId, bool IsVesselChanged, string context)
		{
			PlannedMaintenanceDetailViewModel detailsViewModel = new PlannedMaintenanceDetailViewModel();
			PlannedMaintenanceRequestViewModel requestUrl = new PlannedMaintenanceRequestViewModel();
			PlannedMaintenanceListViewModel pmsListVM = new PlannedMaintenanceListViewModel();

			if (!String.IsNullOrWhiteSpace(PlannedMaintenanceDetails))
			{
				string data = _provider.CreateProtector("PMSDetails").Unprotect(PlannedMaintenanceDetails);
				requestUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
				pmsListVM.ToDate = requestUrl.ToDate;
				pmsListVM.FromDate = requestUrl.FromDate;
				pmsListVM.StageName = requestUrl.StageName;
			}
			else if (!String.IsNullOrWhiteSpace(context))
			{
				ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
				requestUrl.WorkOrderId = contextParameter.PwoId;

				DateTime now = DateTime.Now;
				pmsListVM.FromDate = new DateTime(now.Year, now.Month, 1);
				pmsListVM.ToDate = pmsListVM.FromDate.AddMonths(1).AddDays(-1);
				pmsListVM.StageName = EnumsHelper.GetDescription(PMSDashboardStage.All);
			}

			string plannedMaintenanceListUrl = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(pmsListVM));
			if (IsVesselChanged)
			{
				return RedirectToAction("List", new { PlannedMaintenance = plannedMaintenanceListUrl, VesselId = VesselId });
			}

			detailsViewModel.PlannedMaintainanceListUrl = plannedMaintenanceListUrl;

			_marineClient.AccessToken = GetAccessToken();
			Task<WorkOrderGenericHeaderDetailResponseViewModel> taskResponse = _marineClient.GetWorkOrderGenericHeaderDetails(requestUrl.WorkOrderId);
			WorkOrderGenericHeaderDetailResponseViewModel response = taskResponse.Result ?? new WorkOrderGenericHeaderDetailResponseViewModel();

			if (!String.IsNullOrWhiteSpace(context))
			{
				requestUrl.EncryptedVesselId = VesselId;

				requestUrl.FromDate = pmsListVM.FromDate;
				requestUrl.StageName = pmsListVM.StageName;
				requestUrl.ToDate = pmsListVM.ToDate;

				requestUrl.ComponentId = response.ComponentId;
				requestUrl.ScheduleTaskId = response.ScheduleTaskId;
				requestUrl.WorkOrderId = response.WorkOrderId;
				requestUrl.WorkOrderIndicationId = response.WorkOrderIndicationTypeId;

				if (string.IsNullOrWhiteSpace(response.ScheduleTaskId) && response.WorkOrderIndicationTypeId != EnumsHelper.GetKeyValue(WorkOrderIndicationType.Defect))
				{
					requestUrl.IsSWO = true;
				}

				PlannedMaintenanceDetails = CommonUtil.GetEncryptedURL(_provider, "PMSDetails", requestUrl);
			}

			string decryptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
			string currentVesselId = !String.IsNullOrWhiteSpace(decryptedString) ? decryptedString.Split(Constants.Separator)[0] : string.Empty;
			string currentVesselName = !String.IsNullOrWhiteSpace(decryptedString) ? decryptedString.Split(Constants.Separator)[1] : string.Empty;

			detailsViewModel.VesselName = currentVesselName;
			detailsViewModel.EncryptedVesselId = VesselId;
			detailsViewModel.IsNavigatedFromDone = requestUrl.IsNavigatedFromDone ? true : false;
			detailsViewModel.IsSWO = requestUrl.IsSWO;

			detailsViewModel.PlannedMaintenanceRequestDetailsURL = PlannedMaintenanceDetails;

			string[] contextParams = { requestUrl.WorkOrderId };
			string[] messageParams = { response.JobName };

			detailsViewModel.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.PlannedMaintenance, currentVesselId, CommonUtil.GetVesselNameFromDisplayName(detailsViewModel.VesselName), contextParams, messageParams, requestUrl.WorkOrderId);
			detailsViewModel.IsFromViewRecord = IsFromViewRecordVal(context);
			SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceDetailsPageKey), EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey), PlannedMaintenanceDetails);
			return View(detailsViewModel);
		}

		/// <summary>
		/// Vessels the header details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> VesselHeaderDetails(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			VesselPreviewViewModel previewViewModel = await _marineClient.PostGetVesselHeaderDetail(request.EncryptedVesselId);
			return new JsonResult(previewViewModel);
		}

		/// <summary>
		/// Components the heirarchy details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> ComponentHeirarchyDetails(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<ComponentHierarchyResponseViewModel> response = await _marineClient.PostGetComponentHierarchy(request);
			return new JsonResult(response);
		}

		/// <summary>
		/// Components the details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> ComponentDetails(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			ComponentSearchResponseViewModel response = await _marineClient.PostGetComponentHeaderDetails(request);
			return new JsonResult(response);
		}

		/// <summary>
		/// Works the order details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> WorkOrderDetails(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			WorkOrderHeaderDetailViewModel response = await _marineClient.GetWorkOrderDetails(request);

			if (response.CanProcessRescheduleWO)
			{
				_marineWCFClient.AccessToken = GetAccessToken();
				PMSRescheduleRulesResponseViewModel rulesResponse = await _marineWCFClient.GetRescheduleWorkOrderRules(response);
				response.ExtendedDaysNote = rulesResponse.ExtendedDaysNote;
				response.MaximumCounterExtensionValue = rulesResponse.MaximumCounterExtensionValue;
				response.MaximumIntervalDays = rulesResponse.MaximumIntervalDays;
			}

			return new JsonResult(response);
		}

		/// <summary>
		/// Works the order specifications.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> WorkOrderSpecifications(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			ReportWorkOrderViewModel response = await _marineClient.PostGetWorkOrderSpecification(request);
			return new JsonResult(response);
		}

		/// <summary>
		/// Vessels the guide lines.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> VesselGuideLines(string request)
		{
			_marineClient.AccessToken = GetAccessToken();
			string response = await _marineClient.PostGetVesselJobDescription(request);
			return new JsonResult(response);
		}

		/// <summary>
		/// Spares the part list.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> SparePartList(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			PlannedMaintenanceRequestViewModel detailsVM = GetPlannedMaintenanceRequestFromEncryptedUrl(request.PlannedMaintenanceRequestDetailsURL);
			List<SearchPartResponseViewModel> response = await _marineClient.PostGetWorkOrderRequiredParts(detailsVM.ScheduleTaskId);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Get the Spares part list with encrypted URL.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> SparePartListWithEncryptedUrl(string request)
		{
			_marineClient.AccessToken = GetAccessToken();
			PlannedMaintenanceRequestViewModel detailsVM = GetPlannedMaintenanceRequestFromEncryptedUrl(request);
			List<SearchPartResponseViewModel> response = await _marineClient.PostGetWorkOrderRequiredParts(detailsVM.ScheduleTaskId);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the planned maintenance request from encrypted URL.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		[NonAction]
		private PlannedMaintenanceRequestViewModel GetPlannedMaintenanceRequestFromEncryptedUrl(string request)
		{
			string data = _provider.CreateProtector("PMSDetails").Unprotect(request);
			PlannedMaintenanceRequestViewModel detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
			return detailsVM;
		}

		/// <summary>
		/// Gets the reschedule history details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetRescheduleHistoryDetails(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<RescheduleWorkOrderDetailViewModel> response = await _marineClient.GetRescheduleHistoryLogs(request);
			return new JsonResult(new { data = response });
		}

		#region Done - Section

		/// <summary>
		/// Gets the maintainace history details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetMaintainaceHistoryDetails(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();

			PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
			string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
			detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);

			CertificateReportWorkOrderViewModel certificateReportVM = null;
			ReportWorkOrderViewModel RoundWorkOrderViewModel = null;
			ReportWorkOrderViewModel OtherWorkOrderViewModel = null;
			string Status = string.Empty;
			if (!string.IsNullOrWhiteSpace(detailsVM.DefectWorkOrderId) && !string.IsNullOrWhiteSpace(detailsVM.WorkOrderIndicationId)
					   && detailsVM.WorkOrderIndicationId == EnumsHelper.GetKeyValue(WorkOrderIndicationType.Defect))
			{
				//Defect
				Status = "Defect";

			}
			else if (detailsVM.WorkOrderIndicationId == EnumsHelper.GetKeyValue(WorkOrderIndicationType.Round))
			{
				//Round
				RoundWorkOrderViewModel = await _marineClient.PostGetWorkHistoryDetail(detailsVM.WorkOrderHistoryId);
				Status = "Round";
			}
			else if (detailsVM.WorkOrderIndicationId == EnumsHelper.GetKeyValue(WorkOrderIndicationType.Certificate))
			{
				string decreptedString = _provider.CreateProtector("Vessel").Unprotect(request.EncryptedVesselId);
				string vesselId = decreptedString.Split(Constants.Separator)[0];

				certificateReportVM = await _marineClient.PostGetCertficateWorkOrderHistory(detailsVM.WorkOrderHistoryId, vesselId);
				Status = "Certificate";
			}
			else
			{
				OtherWorkOrderViewModel = await _marineClient.GetWorkOrderHistoryDetails(detailsVM.WorkOrderHistoryId, detailsVM.ScheduleTaskId);
				Status = "Other";
			}

			return new JsonResult(new { certificateReportVM = certificateReportVM, otherWorkOrderViewModel = OtherWorkOrderViewModel, roundWorkOrderViewModel = RoundWorkOrderViewModel, status = Status });
		}

		/// <summary>
		/// Gets the work order history details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkOrderHistoryDetails(string workOrderHistoryId, string scheduleTaskId)
		{
			_marineClient.AccessToken = GetAccessToken();
			ReportWorkOrderViewModel OtherWorkOrderViewModel = await _marineClient.GetWorkOrderHistoryDetails(workOrderHistoryId, scheduleTaskId);
			return new JsonResult(OtherWorkOrderViewModel);
		}


		/// <summary>
		/// Gets the component heirarchy done round.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetComponentHeirarchyDoneRound(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<ComponentHierarchyResponseViewModel> response = await _marineClient.PostGetComponentHierarchyForDoneRound(request);
			return new JsonResult(response);
		}

		#endregion

		/// <summary>
		/// Gets the work basket status tree list.
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkBasketStatusTreeList()
		{
			_marineClient.AccessToken = GetAccessToken();
			List<Lookup> response = await _marineClient.GetWorkBasketStatusTreeList();

			List<TreeViewModel<Lookup>> treeList = new List<TreeViewModel<Lookup>>();
			List<TreeViewModel<Lookup>> childItems = new List<TreeViewModel<Lookup>>();

			TreeViewModel<Lookup> AllOption = new TreeViewModel<Lookup>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<Lookup>>(),
			};

			if (response != null && response.Any())
			{
				var closedStatus = EnumsHelper.GetKeyValue(JobStatus.Closed);
				response = response.Where(x => x.Identifier != closedStatus).ToList();

				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<Lookup>
				{
					Key = y.Identifier,
					Title = y.LongDescription + " (" + y.Description + ")",
					Tooltip = y.Description,
					Expanded = false,
					Checkbox = true,
					Lazy = false,
					Children = null
				}));
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Gets the work basket priority tree list.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetWorkBasketPriorityTreeList()
		{
			List<Lookup> response = new List<Lookup>();

			var list = Enum.GetValues(typeof(WorkBasketPriority)).Cast<WorkBasketPriority>().ToList();
			foreach (var item in list)
			{
				Lookup priority = new Lookup();
				priority.Identifier = EnumsHelper.GetKeyValue(item);
				priority.Description = EnumsHelper.GetKeyValue(item);
				priority.LongDescription = EnumsHelper.GetDescription(item);
				response.Add(priority);
			}

			List<TreeViewModel<Lookup>> treeList = new List<TreeViewModel<Lookup>>();
			List<TreeViewModel<Lookup>> childItems = new List<TreeViewModel<Lookup>>();

			TreeViewModel<Lookup> AllOption = new TreeViewModel<Lookup>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<Lookup>>(),
			};

			if (response != null && response.Any())
			{
				var closedStatus = EnumsHelper.GetKeyValue(JobStatus.Closed);
				response = response.Where(x => x.Identifier != closedStatus).ToList();

				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<Lookup>
				{
					Key = y.Identifier,
					Title = y.LongDescription + " (" + y.Description + ")",
					Tooltip = y.Description,
					Expanded = false,
					Checkbox = true,
					Lazy = false,
					Children = null
				}));
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Gets the work basket rescheduled tree list.
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkBasketRescheduledTreeList()
		{
			_marineClient.AccessToken = GetAccessToken();
			List<string> lookupCodes = new List<string>();
			string attribute = EnumsHelper.GetKeyValue(MaintenanceAttributeLookupCode.RescheduleType);
			lookupCodes.Add(attribute);

			List<MaintenanceAttributeLookupViewModel> response = await _marineClient.GetMaintenanceAttributes(lookupCodes);

			List<TreeViewModel<MaintenanceAttributeLookupViewModel>> treeList = new List<TreeViewModel<MaintenanceAttributeLookupViewModel>>();
			List<TreeViewModel<MaintenanceAttributeLookupViewModel>> childItems = new List<TreeViewModel<MaintenanceAttributeLookupViewModel>>();

			TreeViewModel<MaintenanceAttributeLookupViewModel> AllOption = new TreeViewModel<MaintenanceAttributeLookupViewModel>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<MaintenanceAttributeLookupViewModel>>(),
			};

			if (response != null && response.Any())
			{
				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<MaintenanceAttributeLookupViewModel>
				{
					Key = y.AttributeId,
					Title = y.AttributeDescription,
					Tooltip = y.AttributeDescription,
					Expanded = false,
					Checkbox = true,
					Lazy = false,
					Children = null
				}));
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Gets the work basket responsibility tree list.
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkBasketResponsibilityTreeList(string input)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<ResponsibleRankDetailResponseViewModel> response = await _marineClient.GetVesselResponsibleRanks(input);

			List<string> ResponsibleRankHeader = new List<string>();
			List<ResponsibleRankDetailResponseViewModel> orderStatusList = new List<ResponsibleRankDetailResponseViewModel>();
			List<TreeViewModel<ResponsibleRankDetailResponseViewModel>> treeList = new List<TreeViewModel<ResponsibleRankDetailResponseViewModel>>();

			if (response != null && response.Any())
			{
				ResponsibleRankHeader = response.Select(x => x.DepartmentName).Distinct().ToList();
			}

			TreeViewModel<ResponsibleRankDetailResponseViewModel> AllOption = new TreeViewModel<ResponsibleRankDetailResponseViewModel>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<ResponsibleRankDetailResponseViewModel>>(),
				AdditionalData = new ResponsibleRankDetailResponseViewModel()
				{
					DepartmentId = null
				}
			};

			if (ResponsibleRankHeader != null && ResponsibleRankHeader.Any())
			{
				foreach (string headerName in ResponsibleRankHeader)
				{

					List<TreeViewModel<ResponsibleRankDetailResponseViewModel>> childItems = new List<TreeViewModel<ResponsibleRankDetailResponseViewModel>>();
					if (response.Where(x => x.DepartmentName == headerName).Any())
					{
						var headerResponse = response.Where(x => x.DepartmentName == headerName).FirstOrDefault();
						childItems.AddRange(response.Where(x => x.DepartmentName == headerName).Select(y =>
						new TreeViewModel<ResponsibleRankDetailResponseViewModel>
						{
							Key = y.CrewRankId,
							Title = y.CrewRankDescription + " (" + y.CrewRankShortCode + ")",
							Tooltip = y.CrewRankDescription + " (" + y.CrewRankShortCode + ")",
							Expanded = false,
							Checkbox = true,
							Lazy = false,
							Children = null
						}));

						AllOption.Children.Add(new TreeViewModel<ResponsibleRankDetailResponseViewModel>
						{
							Key = null,
							Title = headerResponse.DepartmentName + " (" + headerResponse.DepartmentShortCode + ")",
							Tooltip = headerResponse.DepartmentName + " (" + headerResponse.DepartmentShortCode + ")",
							Expanded = false,
							Checkbox = true,
							Lazy = false,
							Children = childItems,
						});
					}
				}
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Gets the summary details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetSummaryDetails(PMSDashboardRequestViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			PMSDashboardSummaryViewModel pmsSummary = await _marineClient.PMSDashboardSummary(request);
			return new JsonResult(pmsSummary);
		}

		/// <summary>
		/// Gets the type of the job.
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetJobType()
		{
			_marineClient.AccessToken = GetAccessToken();

			MaintenanceType? maintenanceType = null;
			List<Lookup> response = await _marineClient.GetJobType(maintenanceType);

			List<TreeViewModel<Lookup>> treeList = new List<TreeViewModel<Lookup>>();
			List<TreeViewModel<Lookup>> childItems = new List<TreeViewModel<Lookup>>();

			TreeViewModel<Lookup> AllOption = new TreeViewModel<Lookup>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<Lookup>>(),
			};

			if (response != null && response.Any())
			{
				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<Lookup>
				{
					Key = y.Identifier,
					Title = y.LongDescription + " (" + y.Description + ")",
					Tooltip = y.LongDescription,
					Expanded = false,
					Checkbox = true,
					Lazy = false,
					Children = null
				}));
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Maintenances the history list.
		/// </summary>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <returns></returns>
		public IActionResult MaintenanceHistoryList(string VesselId)
		{
			PlannedMaintenanceListViewModel historyVM = new PlannedMaintenanceListViewModel();
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
			historyVM.FromDate = DateTime.Now.AddDays(-30);
			historyVM.ToDate = DateTime.Now;
			string maintenanceHistory = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(historyVM));
			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.MaintenanceHistoryListPageKey);
			SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey), maintenanceHistory);
			RemoveSessionFilter(_provider, pageKey, null, decreptedString.Split(Constants.Separator)[0]);

			var SessionData = GetSessionFilter(pageKey);
			string data = _provider.CreateProtector("PMSList").Unprotect(SessionData);
			historyVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceListViewModel>(data);
			historyVM.VesselName = decreptedString.Split(Constants.Separator)[1];
			historyVM.EncryptedVesselId = VesselId;
			historyVM.ActiveMobileTabClass = SetTab(pageKey, historyVM.ActiveMobileTabClass, Constants.Tab1);
			return View(historyVM);
		}

		/// <summary>
		/// Maintenances the history details.
		/// </summary>
		/// <param name="PlannedMaintenanceDetails">The planned maintenance details.</param>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <returns></returns>
		public IActionResult MaintenanceHistoryDetails(string PlannedMaintenanceDetails, string VesselId, bool IsVesselChanged)
		{
			PlannedMaintenanceDetailViewModel detailsViewModel = new PlannedMaintenanceDetailViewModel();

			PlannedMaintenanceRequestViewModel requestUrl = new PlannedMaintenanceRequestViewModel();
			string data = _provider.CreateProtector("PMSDetails").Unprotect(PlannedMaintenanceDetails);
			requestUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);

			//vessel change navigation
			PlannedMaintenanceListViewModel pmsListVM = new PlannedMaintenanceListViewModel();
			pmsListVM.ToDate = requestUrl.ToDate;
			pmsListVM.FromDate = requestUrl.FromDate;

			//pmsListVM.EncryptedVesselId = VesselId; //check 

			pmsListVM.StageName = requestUrl.StageName;
			string plannedMaintenanceUrl = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(pmsListVM));
			if (IsVesselChanged)
			{
				return RedirectToAction("MaintenanceHistoryList", new { VesselId = VesselId });
			}

			detailsViewModel.VesselName = decreptedString.Split(Constants.Separator)[1];
			detailsViewModel.EncryptedVesselId = VesselId;
			detailsViewModel.PlannedMaintenanceRequestDetailsURL = PlannedMaintenanceDetails;
			detailsViewModel.PlannedMaintainanceListUrl = plannedMaintenanceUrl;
			detailsViewModel.IsNavigatedFromDone = requestUrl.IsNavigatedFromDone ? true : false;

			SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.MaintenanceHistoryDetailsPageKey), EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey), PlannedMaintenanceDetails);
			return View(detailsViewModel);
		}

		public IActionResult LoadMaintenanceHistoryList(PlannedMaintenanceListViewModel request)
		{
			return Json(Url.Action("MaintenanceHistoryList", new { VesselId = request.EncryptedVesselId }));
		}
		public IActionResult LoadPlannedMaintenance(PlannedMaintenanceListViewModel request)
		{
			PlannedMaintenanceListViewModel input = new PlannedMaintenanceListViewModel();
			input.EncryptedVesselId = request.EncryptedVesselId;
			DateTime now = DateTime.Now;
			input.FromDate = new DateTime(now.Year, now.Month, 1);
			input.ToDate = input.FromDate.AddMonths(1).AddDays(-1);
			input.StageName = EnumsHelper.GetDescription(PMSDashboardStage.Due);
			string pmsURL = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
			return Json(Url.Action("List", new { PlannedMaintenance = pmsURL, VesselId = input.EncryptedVesselId }));
		}
		/// <summary>
		/// Gets the other filters.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetOtherFilters()
		{

			List<Lookup> response = _marineClient.GetPMSOtherFilters();

			List<TreeViewModel<Lookup>> treeList = new List<TreeViewModel<Lookup>>();
			List<TreeViewModel<Lookup>> childItems = new List<TreeViewModel<Lookup>>();

			TreeViewModel<Lookup> AllOption = new TreeViewModel<Lookup>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<Lookup>>(),
			};

			if (response != null && response.Any())
			{
				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<Lookup>
				{
					Key = y.Identifier,
					Title = y.Description,
					Tooltip = y.Description,
					Expanded = false,
					Checkbox = true,
					Lazy = false,
					Children = null
				}));
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Gets the hierarchy tree.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetHierarchyTree(ComponentCategoryTreeRequestViewModel parent)
		{
			_marineClient.AccessToken = GetAccessToken();

			ComponentCategoryTreeRequest listRequest = new ComponentCategoryTreeRequest
			{
				AlternateTemplateId = parent.AlternateTemplateId,
				CategoryId = parent.CategoryId,
				ComponentId = parent.RequestComponentId,
				FunctionalArea = parent.FunctionalArea,
				IsComponentClick = parent.IsComponentClick,
				ModuleId = parent.ModuleId,
				SyaId = parent.SyaId,
				VesselId = parent.VesselId
			};

			string vesselId = _provider.CreateProtector("Vessel").Unprotect(parent.VesselId);
			listRequest.VesselId = vesselId.Split(Constants.Separator)[0];

			List<TreeViewModel<ComponentCategoryTreeResponseViewModel>> treeList = new List<TreeViewModel<ComponentCategoryTreeResponseViewModel>>();
			List<ComponentCategoryTreeResponse> response = await _marineClient.GetPMSVesselTree(listRequest);
			foreach (var item in response)
			{

				TreeViewModel<ComponentCategoryTreeResponseViewModel> child = new TreeViewModel<ComponentCategoryTreeResponseViewModel>
				{
					AdditionalData = new ComponentCategoryTreeResponseViewModel
					{
						SyaId = item.SyaId,
						FunctionalArea = item.FunctionalArea,
						ComponentId = item.ComponentId,
						CanAddComponent = item.CanAddComponent,
						CategoryId = item.CategoryId,
						ChildCategoryComponent = item.ChildCategoryComponent,
						Code = item.Code,
						HasChild = item.HasChild,
						MappedSystemArea = item.MappedSystemArea,
						Name = item.Name,
						NumberOfChildren = item.NumberOfChildren,
						ParentCode = item.ParentCode,
						ParentComponentRequired = item.ParentComponentRequired,
						ParentId = item.ParentId,
						ParentComponentId = parent.ComponentId ?? (!parent.CanAddComponent ? parent.ParentComponentId : null)
					},
					Checkbox = false,
					Expanded = false,
					Key = item.CategoryId,
					Title = item.Name,
					Lazy = true,
					Tooltip = item.Name,
					Children = null
				};
				treeList.Add(child);
			}

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Gets the maintenance history summary.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetMaintenanceHistorySummary(ClosedWorkOrderHistorySummaryRequestViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			ClosedWorkOrderHistorySummaryResponceViewModel Summary = await _marineClient.MaintenanceHistorySummary(request);
			return new JsonResult(Summary);
		}

		/// <summary>
		/// Gets the maintenance history summary details.
		/// </summary>
		/// <param name="protectedUrl">The protected URL.</param>
		/// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
		/// <returns></returns>
		public IActionResult GetMaintenanceHistorySummaryDetails(string protectedUrl, string encryptedVesselId)
		{
			string data = _provider.CreateProtector("PMSList").Unprotect(protectedUrl);
			PlannedMaintenanceListViewModel result = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceListViewModel>(data);
			result = GetPMSHistoryListRequestObject(result);
			result.EncryptedVesselId = encryptedVesselId;
			string MaintenanceHistoryUrlEncrypted = CommonUtil.GetEncryptedURL(_provider, Constants.PMSList, result);
			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.MaintenanceHistoryListPageKey);
			SetSessionFilter(pageKey, MaintenanceHistoryUrlEncrypted, encryptedVesselId);
			
			return new JsonResult(new { data = result });
		}

		/// <summary>
		/// Gets the certificate list.
		/// </summary>
		/// <param name="VesselCertificateLogId">VesselCertificateLogId of the certificate.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkorderHistoryDocuments(PlannedMaintenanceDetailViewModel request)
		{
			_sharedClient.AccessToken = GetAccessToken();
			DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest();

			PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
			string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
			detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
			List<string> WorkOrderHistoryId = new List<string>();
			WorkOrderHistoryId.Add(detailsVM.WorkOrderHistoryId);

			documentDetailRequest.DocumentSourceIds = WorkOrderHistoryId;
			documentDetailRequest.SsmId = EnumsHelper.GetKeyValue(SubModule.WorkOrderHistory);
			documentDetailRequest.DctId = null;

			string input = _provider.CreateProtector("DocumentURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(documentDetailRequest));
			List<DocumentDetail> response = await _sharedClient.PostGetDocumentDetails(input);

			List<DocumentDetailViewModel> result = new List<DocumentDetailViewModel>();
			if (response != null && response.Any())
			{
				response.ForEach(x =>
				{
					result.Add(new DocumentDetailViewModel()
					{

						CreatedOn = x.CreatedOn,
						Type = x.CategoryName,
						Description = x.Description,
						Title = x.Title,
						CanRequestDocument = x.CanRequestDocument,
						IsWebAddressEditable = x.DocumentType != null && x.DocumentType == Convert.ToInt32(EnumsHelper.GetKeyValue(DocumentType.WebAddress)),
						WebAddress = x.WebAddress,
						EttId = x.EttId,
						CloudFileName = x.CloudFileName,
					});
				});
			}
			return new JsonResult(new { data = result });
		}

		/// <summary>
		/// Gets the work order reschedule detail.
		/// </summary>
		/// <param name="workOrderRescheduleId">The work order reschedule identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetWorkOrderRescheduleDetail(string workOrderRescheduleId, string rescheduleRequestTypeId)
		{
			_marineClient.AccessToken = GetAccessToken();
			RescheduleWorkOrderDetailViewModel response = await _marineClient.GetWorkOrderRescheduleDetail(workOrderRescheduleId, rescheduleRequestTypeId);
			return new JsonResult(response);
		}

		/// <summary>
		/// Downloads the document.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> DownloadDocument(string input)
		{
			_documentClient.AccessToken = GetAccessToken();
			CloudDocumentDownloadRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<CloudDocumentDownloadRequest>(input);
			request.DocumentCategory = DocumentCategory.WorkOrderHistory;
			request.DocumentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(request.FileName)).FirstOrDefault();
			var result = await _documentClient.DownloadDocument(request);
			byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
			string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
			return new JsonResult(new { filename = request.FileName, bytes = byteString, fileType = EnumsHelper.GetDescription(request.DocumentFileType) });
		}

		/// <summary>
		/// Gets the PMS summary details.
		/// </summary>
		/// <param name="plannedMaintenanceUrl">The planned maintenance URL.</param>
		/// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
		/// <returns></returns>
		public JsonResult GetPMSSummaryDetails(string plannedMaintenanceUrl, string encryptedVesselId)
		{
			string data = _provider.CreateProtector("PMSList").Unprotect(plannedMaintenanceUrl);
			PlannedMaintenanceListViewModel result = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceListViewModel>(data);
			result = GetPMSListRequestObject(result);
			result.EncryptedVesselId = encryptedVesselId;

			string plannedMaintenanceUrlEncrypted = CommonUtil.GetEncryptedURL(_provider, Constants.PMSList, result);

			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey), plannedMaintenanceUrlEncrypted, encryptedVesselId);

			return new JsonResult(new { data = result });
		}

		/// <summary>
		/// Sets the page parameter PMS history.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public IActionResult SetPageParameterPMSHistory(PlannedMaintenanceListViewModel input)
		{
			PlannedMaintenanceListViewModel request = new PlannedMaintenanceListViewModel();
			request.EncryptedVesselId = input.EncryptedVesselId;
			request.FromDate = input.FromDate;
			request.ToDate = input.ToDate;
			request.StageName = input.StageName;
			request.RescheduledIds = input.RescheduledIds;
			request.ResponsibilityIds = input.ResponsibilityIds;
			request.JobTypeIds = input.JobTypeIds;
			request.OtherFilters = input.OtherFilters;
			request.isSearchedClick = input.isSearchedClick;
			request.IsCritical = input.IsCritical;
			request.ComponentTitle = input.ComponentTitle;

			request.SelectedWBResponsibilityIds = input.ResponsibilityIds != null && input.ResponsibilityIds.Any() ? string.Join(",", input.ResponsibilityIds.Select(x => x)) : string.Empty;
			request.SelectedWBRescheduledIds = input.RescheduledIds != null && input.RescheduledIds.Any() ? string.Join(",", input.RescheduledIds.Select(x => x)) : string.Empty;
			request.SelectedWBJobTypeIds = input.JobTypeIds != null && input.JobTypeIds.Any() ? string.Join(",", input.JobTypeIds.Select(x => x)) : string.Empty;
			request.SelectedOtherFilters = input.OtherFilters != null && input.OtherFilters.Any() ? string.Join(",", input.OtherFilters.Select(x => x)) : string.Empty;
			request.TopSystemAreaId = input.TopSystemAreaId;
			request.ComponentId = input.ComponentId;
			request.ParentComponentId = input.ComponentId;
			request.CategoryId = input.CategoryId;

			string plannedMaintenanceUrl = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.MaintenanceHistoryListPageKey), plannedMaintenanceUrl, input.EncryptedVesselId);

			return new JsonResult(new { data = request });
		}

		/// <summary>
		/// Gets the unplanned wo specification.
		/// </summary>
		/// <param name="workOrderId">The work order identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetUnplannedWOSpecification(PlannedMaintenanceDetailViewModel request)
		{
			_marineClient.AccessToken = GetAccessToken();
			UnplannedWorkOrderDetailViewModel response = await _marineClient.GetUnplannedWOSpecification(request);
			return new JsonResult(response);
		}


		/// <summary>
		/// Exports to excel PMS.
		/// </summary>
		/// <param name="pmsRequest">The PMS request.</param>
		/// <returns></returns>
		public async Task<IActionResult> ExportToExcelPMSList(PlannedMaintenanceListViewModel pmsRequest)
		{
			_marineClient.AccessToken = GetAccessToken();
			if (pmsRequest != null)
			{
				string vesselId = _provider.CreateProtector("Vessel").Unprotect(pmsRequest.EncryptedVesselId);
				pmsRequest.VesselName = vesselId.Split(Constants.Separator)[1];
			}
			List<WorkBasketDetailExportViewModel> response = await _marineClient.ExportToExcelPMSList(pmsRequest);

			ExportToExcelRequest request = new ExportToExcelRequest();
			request.FileName = "Planned Maintenance";
			request.Title = "Planned Maintenance";
			string summary = "Vessel : " + pmsRequest.VesselName;
			int summaryRowCount = 1;

			string stageName = string.Empty;
			if (pmsRequest.isSearchedClick)
			{
				summary += "\nFrom Date : " + pmsRequest.FromDate.ToString("dd MMM yyyy");
				summaryRowCount++;
				summary += "\nTo Date : " + pmsRequest.ToDate.ToString("dd MMM yyyy");
				summaryRowCount++;

				if (!string.IsNullOrWhiteSpace(pmsRequest.ComponentTitle))
				{
					summary += "\nComponent : " + pmsRequest.ComponentTitle;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(pmsRequest.StatusTitles))
				{
					if (pmsRequest.StatusTitles.Split(",")[0].Contains(Constants.All))
					{
						pmsRequest.StatusTitles = Constants.All;
					}
					summary += "\nStatus : " + pmsRequest.StatusTitles;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(pmsRequest.PriorityTitles))
				{
					if (pmsRequest.PriorityTitles.Split(",")[0].Contains(Constants.All))
					{
						pmsRequest.PriorityTitles = Constants.All;
					}
					summary += "\nPriority : " + pmsRequest.PriorityTitles;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(pmsRequest.ResponsiblityTitles))
				{
					if (pmsRequest.ResponsiblityTitles.Split(",")[0].Contains(Constants.All))
					{
						pmsRequest.ResponsiblityTitles = Constants.All;
					}
					summary += "\nResponsibility : " + pmsRequest.ResponsiblityTitles;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(pmsRequest.RescheduleTitles))
				{
					if (pmsRequest.RescheduleTitles.Split(",")[0].Contains(Constants.All))
					{
						pmsRequest.RescheduleTitles = Constants.All;
					}
					summary += "\nReschedule : " + pmsRequest.RescheduleTitles;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(pmsRequest.JobTypeTitles))
				{
					if (pmsRequest.JobTypeTitles.Split(",")[0].Contains(Constants.All))
					{
						pmsRequest.JobTypeTitles = Constants.All;
					}
					summary += "\nJob Types : " + pmsRequest.JobTypeTitles;
					summaryRowCount++;
				}
				if (!string.IsNullOrWhiteSpace(pmsRequest.OtherFilterTitles))
				{
					if (pmsRequest.OtherFilterTitles.Split(",")[0].Contains(Constants.All))
					{
						pmsRequest.OtherFilterTitles = Constants.All;
					}
					summary += "\nOther Filters : " + pmsRequest.OtherFilterTitles;
					summaryRowCount++;
				}
			}
			else
			{
				summary += "\nStage : " + EnumsHelper.GetEnumNameFromKeyValue(typeof(PMSDashboardStage), pmsRequest.StageName);
				summaryRowCount++;
			}

			request.Summary = summary;
			request.SummaryRowCount = summaryRowCount;
			request.ColumnCount = typeof(WorkBasketDetailExportViewModel).GetProperties().Count();

			return ExportToExcel(response, request);
		}

		/// <summary>
		/// Gets the PMS list request object.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		[NonAction]
		private PlannedMaintenanceListViewModel GetPMSListRequestObject(PlannedMaintenanceListViewModel request)
		{
			if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.Due))
			{
				request.FromDate = request.FromDate;
				request.ToDate = request.ToDate;
				request.OtherFilters = new List<string>() { EnumsHelper.GetKeyValue(PMSOtherFilter.Due) };
			}
			else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.CriticalDue))
			{
				request.FromDate = request.FromDate;
				request.ToDate = request.ToDate;
				request.OtherFilters = new List<string>() { EnumsHelper.GetKeyValue(PMSOtherFilter.Due) };
				request.PriorityIds = new List<string>() { EnumsHelper.GetKeyValue(WorkBasketPriority.Critical) };
			}
			else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.Overdue))
			{
				request.FromDate = request.FromDate;
				request.ToDate = request.ToDate;
				request.OtherFilters = new List<string>() { EnumsHelper.GetKeyValue(PMSOtherFilter.OverduePriorMonth) };
				request.StatusIds = new List<string>() {
														 EnumsHelper.GetKeyValue(JobStatus.WorkOrder),
														 EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder),
														 EnumsHelper.GetKeyValue(JobStatus.RescheduleRequested),
														 EnumsHelper.GetKeyValue(JobStatus.ReOpenedWorkOrder),
														 EnumsHelper.GetKeyValue(JobStatus.ShipsWorkOrder),
														 EnumsHelper.GetKeyValue(JobStatus.DefectWorkOrder)
														};
			}
			else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.CriticalOverdue))
			{
				request.FromDate = request.FromDate;
				request.ToDate = request.ToDate;
				request.OtherFilters = new List<string>() { EnumsHelper.GetKeyValue(PMSOtherFilter.OverduePriorMonth) };
				request.PriorityIds = new List<string>() { EnumsHelper.GetKeyValue(WorkBasketPriority.Critical) };
			}
			else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.Critical))
			{
				request.FromDate = request.FromDate;
				request.ToDate = request.ToDate;
				request.PriorityIds = new List<string>() { (EnumsHelper.GetKeyValue(WorkBasketPriority.Critical)) };
			}
			else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.Completed))
			{
				request.FromDate = request.FromDate;
				request.ToDate = request.ToDate;
				request.StatusIds = new List<string>() { (EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder)) };
			}
			else if (request.StageName == EnumsHelper.GetKeyValue(PMSDashboardStage.All))
			{
				request.FromDate = request.FromDate;
				request.ToDate = request.ToDate;
				request.OtherFilters = new List<string>() { EnumsHelper.GetKeyValue(PMSOtherFilter.OverduePriorMonth) ,
															EnumsHelper.GetKeyValue(PMSOtherFilter.Due),
															EnumsHelper.GetKeyValue(PMSOtherFilter.OverdueCurrentMonth)};
			}
			else if (request.StageName == EnumsHelper.GetKeyValue(PMSDashboardStage.ReqReschedule))
			{
				request.FromDate = request.FromDate;
				request.ToDate = request.ToDate;
				request.StatusIds = new List<string>() { (EnumsHelper.GetKeyValue(JobStatus.RescheduleRequested)) };
			}

			if (request.OtherFilters != null && request.OtherFilters.Any())
			{
				request.SelectedOtherFilters = string.Join(',', request.OtherFilters);
			}

			if (request.PriorityIds != null && request.PriorityIds.Any())
			{
				request.SelectedWBPriorityIds = string.Join(',', request.PriorityIds);
			}

			if (request.StatusIds != null && request.StatusIds.Any())
			{
				request.SelectedWBStatusIds = string.Join(',', request.StatusIds);
			}

			if (request.RescheduledIds != null && request.RescheduledIds.Any())
			{
				request.SelectedWBRescheduledIds = string.Join(',', request.RescheduledIds);
			}

			return request;
		}


		/// <summary>
		/// Gets the PMS history list request object.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		[NonAction]
		private PlannedMaintenanceListViewModel GetPMSHistoryListRequestObject(PlannedMaintenanceListViewModel request)
		{
			if (request.StageName == EnumsHelper.GetKeyValue(ClosedWorkOrderHistoryStage.OverhaulCount))
			{
				request.RescheduledIds = new List<string>();
				request.JobTypeIds = new List<string>();
				request.JobTypeIds.Add(EnumsHelper.GetKeyValue(JobClassType.Overhaul));
			}
			else if (request.StageName == EnumsHelper.GetKeyValue(ClosedWorkOrderHistoryStage.RescheduledCount))
			{
				request.RescheduledIds = new List<string>();
				request.JobTypeIds = new List<string>();
				request.RescheduledIds.Add(EnumsHelper.GetKeyValue(RescheduleType.Reschedule));
			}

			if (request.RescheduledIds != null && request.RescheduledIds.Any())
			{
				request.SelectedWBJobTypeIds = string.Empty;
				request.SelectedWBRescheduledIds = string.Join(',', request.RescheduledIds);
			}

			if (request.JobTypeIds != null && request.JobTypeIds.Any())
			{
				request.SelectedWBRescheduledIds = string.Empty;
				request.SelectedWBJobTypeIds = string.Join(',', request.JobTypeIds);
			}

			return request;
		}

		/// <summary>
		/// Exports to excel maintenance history list.
		/// </summary>
		/// <param name="maintenanceHistoryRequest">The PMS request.</param>
		/// <returns></returns>
		public async Task<IActionResult> ExportToExcelMaintenanceHistoryList(PlannedMaintenanceListViewModel maintenanceHistoryRequest)
		{
			_marineClient.AccessToken = GetAccessToken();
			if (maintenanceHistoryRequest != null)
			{
				string vesselId = _provider.CreateProtector("Vessel").Unprotect(maintenanceHistoryRequest.EncryptedVesselId);
				maintenanceHistoryRequest.VesselName = vesselId.Split(Constants.Separator)[1];
			}
			List<WorkHistoryExportViewModel> response = await _marineClient.ExportToExcelMaintenanceHistoryList(maintenanceHistoryRequest);

			ExportToExcelRequest request = new ExportToExcelRequest();
			request.FileName = "Maintenance History List";
			request.Title = "Maintenance History List";
			string summary = "Vessel : " + maintenanceHistoryRequest.VesselName;
			int summaryRowCount = 1;

			string stageName = string.Empty;
			if (maintenanceHistoryRequest.isSearchedClick)
			{
				summary += "\nFrom Date : " + maintenanceHistoryRequest.FromDate.ToString("dd MMM yyyy");
				summaryRowCount++;
				summary += "\nTo Date : " + maintenanceHistoryRequest.ToDate.ToString("dd MMM yyyy");
				summaryRowCount++;



				if (!string.IsNullOrWhiteSpace(maintenanceHistoryRequest.ComponentTitle))
				{
					summary += "\nComponent : " + maintenanceHistoryRequest.ComponentTitle;
					summaryRowCount++;
				}

				if (maintenanceHistoryRequest.IsCritical.HasValue)
				{
					summary += "\nCritical : " + (maintenanceHistoryRequest.IsCritical.Value ? Constants.Yes : Constants.No);
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(maintenanceHistoryRequest.ResponsiblityTitles))
				{
					if (maintenanceHistoryRequest.ResponsiblityTitles.Split(",")[0].Contains(Constants.All))
					{
						maintenanceHistoryRequest.ResponsiblityTitles = Constants.All;
					}
					summary += "\nResponsibility : " + maintenanceHistoryRequest.ResponsiblityTitles;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(maintenanceHistoryRequest.RescheduleTitles))
				{
					if (maintenanceHistoryRequest.RescheduleTitles.Split(",")[0].Contains(Constants.All))
					{
						maintenanceHistoryRequest.RescheduleTitles = Constants.All;
					}
					summary += "\nReschedule : " + maintenanceHistoryRequest.RescheduleTitles;
					summaryRowCount++;
				}

				if (!string.IsNullOrWhiteSpace(maintenanceHistoryRequest.JobTypeTitles))
				{
					if (maintenanceHistoryRequest.JobTypeTitles.Split(",")[0].Contains(Constants.All))
					{
						maintenanceHistoryRequest.JobTypeTitles = Constants.All;
					}
					summary += "\nJob Types : " + maintenanceHistoryRequest.JobTypeTitles;
					summaryRowCount++;
				}
			}
			else
			{
				if (string.IsNullOrWhiteSpace(maintenanceHistoryRequest.StageName))
				{
					summary += "\nStage : " + Constants.All;
				}
				else
				{
					summary += "\nStage : " + EnumsHelper.GetEnumNameFromKeyValue(typeof(ClosedWorkOrderHistoryStage), maintenanceHistoryRequest.StageName ?? Constants.All);
				}

				summaryRowCount++;
			}

			request.Summary = summary;
			request.SummaryRowCount = summaryRowCount;
			request.ColumnCount = typeof(WorkHistoryExportViewModel).GetProperties().Count();

			return ExportToExcel(response, request);
		}

		/// <summary>
		/// Closes the work order.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<JsonResult> CloseWorkOrder(PlannedMaintenanceDetailViewModel input)
		{
			_marineWCFClient.AccessToken = GetAccessToken();

			string data = _provider.CreateProtector("PMSDetails").Unprotect(input.PlannedMaintenanceRequestDetailsURL);
			PlannedMaintenanceRequestViewModel detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);

			WorkOrderStatusUpdateRequest request = new WorkOrderStatusUpdateRequest
			{
				JobStatus = JobStatus.Closed
			};
			request.WorkOrderIds = new List<string>() { detailsVM.WorkOrderId };

			bool response = await _marineWCFClient.UpdateWorkOrderStatus(request, false);

			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the PMS source URL.
		/// </summary>
		/// <param name="pageKey">The page key.</param>
		/// <returns></returns>
		public JsonResult GetPmsSourceUrl(string pageKey)
		{
			string sourceUrl = GetSourceURLString(pageKey);

			var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey));
			if (session != null)
			{
				PlannedMaintenanceListViewModel pmsVM = new PlannedMaintenanceListViewModel();
				var SessionData = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey));
				string data = _provider.CreateProtector("PMSList").Unprotect(SessionData);
				pmsVM = JsonConvert.DeserializeObject<PlannedMaintenanceListViewModel>(data);
				pmsVM.GridSubTitle = Constants.PMSCompleted;
				pmsVM.OtherFilters = null;
				pmsVM.SelectedOtherFilters = null;
				pmsVM.SelectedWBStatusIds = EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder);
				pmsVM.StatusIds = new List<string> { EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder) };
				pmsVM.StageName = EnumsHelper.GetDescription(PMSDashboardStage.Completed);
				string EncryptPlannedMaintenance = CommonUtil.GetEncryptedURL(_provider, Constants.PMSList, pmsVM);
				SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey), EncryptPlannedMaintenance, pmsVM.EncryptedVesselId);
			}
			return new JsonResult(sourceUrl);
		}

		/// <summary>
		/// Gets the wo reschedule header detail.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetWORescheduleHeaderDetail(PlannedMaintenanceDetailViewModel request)
		{
			string vesselId = CommonUtil.GetDecryptedVesselId(_provider, request.EncryptedVesselId);
			PlannedMaintenanceRequestViewModel detailsVM = CommonUtil.GetDecryptedRequest<PlannedMaintenanceRequestViewModel>(_provider, "PMSDetails", request.PlannedMaintenanceRequestDetailsURL);

			_marineWCFClient.AccessToken = GetAccessToken();
			WorkOrderRescheduleHeaderDetailViewModel response = await _marineWCFClient.GetWORescheduleHeaderDetail(detailsVM.WorkOrderId, vesselId);

			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the reschedule work order for edit.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetRescheduleWorkOrderForEdit(string input)
		{
			RescheduleRequestViewModel request = CommonUtil.GetDecryptedRequest<RescheduleRequestViewModel>(_provider, Constants.RescheduleRequestEncryptionText, input);

			_marineWCFClient.AccessToken = GetAccessToken();
			RescheduleWorkOrderDetailViewModel response = await _marineWCFClient.GetRescheduleWorkOrderForEdit(request);

			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the mapped risk assessment detail.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetMappedRiskAssessmentDetail(string input)
		{
			RescheduleRequestViewModel request = CommonUtil.GetDecryptedRequest<RescheduleRequestViewModel>(_provider, Constants.RescheduleRequestEncryptionText, input);

			_marineWCFClient.AccessToken = GetAccessToken();
			List<HazardDetailViewModel> response = await _marineWCFClient.GetMappedRiskAssessmentDetail(request);

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the process reschedule wo attachments.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetProcessRescheduleWoAttachments(string input)
		{
			List<DocumentDetailViewModel> result = new List<DocumentDetailViewModel>();
			
			RescheduleRequestViewModel request = CommonUtil.GetDecryptedRequest<RescheduleRequestViewModel>(_provider, Constants.RescheduleRequestEncryptionText, input);

			DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest();
			documentDetailRequest.SourceId = request.PorRequestId;
			documentDetailRequest.SsmId = EnumsHelper.GetKeyValue(SubModule.RescheduleWorkOrder);
			string encryptedDocumentRequest =  CommonUtil.GetEncryptedURL(_provider, Constants.DocumentUrlEncryptionText, documentDetailRequest);

			_sharedClient.AccessToken = GetAccessToken();
			List<DocumentDetail> response = await _sharedClient.PostGetDocumentDetails(encryptedDocumentRequest);
			
			if (response != null && response.Any())
			{
				foreach (DocumentDetail item in response)
				{
					DocumentDetailViewModel viewModel = new DocumentDetailViewModel();
					viewModel.CreatedOn = item.CreatedOn;
					viewModel.Type = item.CategoryName;
					viewModel.Description = !string.IsNullOrEmpty(item.Description) ? item.Description : "";
					viewModel.Title = item.Title;
					viewModel.CanRequestDocument = item.CanRequestDocument;
					viewModel.IsWebAddressEditable = item.DocumentType != null && item.DocumentType == Convert.ToInt32(EnumsHelper.GetKeyValue(DocumentType.WebAddress));
					viewModel.WebAddress = CommonUtil.GetExecutableWebAddress(item.WebAddress);
					viewModel.EttId = item.EttId;
					viewModel.CloudFileName = item.CloudFileName;
					if (item.SsmId == EnumsHelper.GetKeyValue(SubModule.RescheduleWorkOrder))
					{
						viewModel.DocumentCategory = DocumentCategory.RescheduleWorkOrder;
					}
					result.Add(viewModel);
				}
			}
			return new JsonResult(new { data = result });
		}

		/// <summary>
		/// Processes the wo.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="inputRequest">The input request.</param>
		/// <returns></returns>
		public async Task<JsonResult> ProcessRescheduleRequest(string input, RescheduleWorkOrderDetailViewModel inputRequest)
		{
			RescheduleRequestViewModel request = CommonUtil.GetDecryptedRequest<RescheduleRequestViewModel>(_provider, Constants.RescheduleRequestEncryptionText, input);

			_marineWCFClient.AccessToken = GetAccessToken();
			bool response = await _marineWCFClient.UpdateWORescheduleStatus(request, inputRequest, false);

			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the process reschedule request source URL.
		/// </summary>
		/// <param name="pageKey">The page key.</param>
		/// <returns></returns>
		public JsonResult GetProcessRescheduleRequestSourceUrl(string pageKey)
		{
			string sourceUrl = GetSourceURLString(pageKey);

			var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey));
			if (session != null)
			{
				PlannedMaintenanceListViewModel pmsVM = new PlannedMaintenanceListViewModel();
				var SessionData = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey));
				string data = _provider.CreateProtector("PMSList").Unprotect(SessionData);
				pmsVM = JsonConvert.DeserializeObject<PlannedMaintenanceListViewModel>(data);
				pmsVM.GridSubTitle = Constants.PMSReqReschedule;
				pmsVM.OtherFilters = null;
				pmsVM.SelectedOtherFilters = null;
				pmsVM.SelectedWBStatusIds = EnumsHelper.GetKeyValue(JobStatus.RescheduleRequested);
				pmsVM.StatusIds = new List<string> { EnumsHelper.GetKeyValue(JobStatus.RescheduleRequested) };
				pmsVM.StageName = EnumsHelper.GetDescription(PMSDashboardStage.ReqReschedule);
				string EncryptPlannedMaintenance = CommonUtil.GetEncryptedURL(_provider, Constants.PMSList, pmsVM);
				SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey), EncryptPlannedMaintenance, pmsVM.EncryptedVesselId);
			}
			return new JsonResult(sourceUrl);
		}

	}
}