using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.InspectionManager;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Inspection;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
	/// <seealso cref="PWAFeaturesRnd.Controllers.AuthCodeController" />
	public class InspectionController : AuthenticatedController
	{
		/// <summary>
		/// The client
		/// </summary>
		private readonly MarineClient _client;

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
		private readonly NotificationClient _notificationClient;

		/// <summary>
		/// The marine WCF client
		/// </summary>
		private readonly MarineWCFClient _marineWCFClient;

		/// <summary>
		/// The ss marine client
		/// </summary>
		private readonly SSMarineClient _ssMarineClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="InspectionController"/> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="sharedClient">The shared client.</param>
		/// <param name="notificationClient">The notification client.</param>
		/// <param name="marineWCFClient">The marine WCF client.</param>
		/// <param name="ssMarineClient">The ss marine client.</param>
		public InspectionController(MarineClient client, IDataProtectionProvider provider, SharedClient sharedClient, NotificationClient notificationClient, MarineWCFClient marineWCFClient, SSMarineClient ssMarineClient)
		{
			_client = client;
			_provider = provider;
			_sharedClient = sharedClient;
			_notificationClient = notificationClient;
			_marineWCFClient = marineWCFClient;
			_ssMarineClient = ssMarineClient;
		}

		/// <summary>
		/// Lists this instance.
		/// </summary>
		/// <param name="inspection">The inspection.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="IsViewMore">if set to <c>true</c> [is view more].</param>
		/// <returns></returns>
		public async Task<IActionResult> List(string inspection, string vesselId, bool IsViewMore)
		{
			InspectionRequestViewModel inspectionRequestViewModel = new InspectionRequestViewModel();
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
			string LatestVesselName = decreptedString.Split(Constants.Separator)[1];
			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey);
			SetSessionDetail(pageKey, null, inspection);
			RemoveSessionFilter(_provider, pageKey, null, decreptedString.Split(Constants.Separator)[0]);

			_client.AccessToken = GetAccessToken();
			inspectionRequestViewModel = await InspectionListDetails(GetSessionFilter(pageKey), vesselId);
			inspectionRequestViewModel.VesselName = LatestVesselName;
			inspectionRequestViewModel.ActiveMobileTabClass = SetTab(pageKey, inspectionRequestViewModel.ActiveMobileTabClass, Constants.Tab2);
			return View(inspectionRequestViewModel);
		}

		/// <summary>
		/// Sets the default value.
		/// </summary>
		/// <param name="inspectionRequest">The inspection request.</param>
		/// <returns></returns>
		public async Task<IActionResult> SetDefaultValue(InspectionRequestViewModel inspectionRequest)
		{
			InspectionTypeDetailRequestViewModel inspectionTypeInput = new InspectionTypeDetailRequestViewModel();
			_client.AccessToken = GetAccessToken();
			InspectionRequestViewModel request = new InspectionRequestViewModel();
			request.FromDate = inspectionRequest.FromDate;
			request.ToDate = inspectionRequest.ToDate;
			request.InspectionType = inspectionRequest.InspectionType;
			request.InspectionTypeIds = inspectionRequest.InspectionTypeIds;
			request.InspectionFilter = inspectionRequest.InspectionFilter;
			request.IsInspection = inspectionRequest.IsInspection;
			request.InspectionFilter = inspectionRequest.InspectionFilter;
			request.IsOverdue = inspectionRequest.IsOverdue;
			request.IsDue = inspectionRequest.IsDue;
			request.InDays = inspectionRequest.InDays;
			request.IsShowDetained = inspectionRequest.IsShowDetained;
			request.IsSummaryClicked = inspectionRequest.IsSummaryClicked;

			request.IsFindingOutstanding = true;
			request.IsFindingOverdue = true;
			request.IsPendingClosure = true;
			request.IsClosed = true;
			request.IsAllSelected = true;

			inspectionTypeInput.Ves_Id = inspectionRequest.VesselId;
			List<InspectionTypeDetailViewModel> inspectionTypeList = await _client.GetInspectionTypeWithVesselTypeFilter(inspectionTypeInput);

			List<string> typeAll = inspectionTypeList.Select(x => x.InspectionTypeId).ToList();

			if (typeAll != null && typeAll.Any())
			{
				request.strInspectionTypeIds = string.Join(',', typeAll);
			}

			string inspectionURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey), inspectionURL, inspectionRequest.VesselId);

			return new JsonResult(new { data = request, vesselId = inspectionRequest.VesselId });
		}

		/// <summary>
		/// Sets the page parameter.
		/// </summary>
		/// <param name="inspectionRequest">The inspection request.</param>
		/// <returns></returns>
		public IActionResult SetPageParameter(InspectionRequestViewModel inspectionRequest)
		{

			InspectionRequestViewModel request = new InspectionRequestViewModel();
			request.FromDate = inspectionRequest.FromDate;
			request.ToDate = inspectionRequest.ToDate;
			request.InspectionType = inspectionRequest.InspectionType;
			request.InspectionTypeIds = inspectionRequest.InspectionTypeIds;
			request.InspectionFilter = inspectionRequest.InspectionFilter;
			request.IsInspection = inspectionRequest.IsInspection;
			request.InspectionFilter = inspectionRequest.InspectionFilter;
			request.IsOverdue = inspectionRequest.IsOverdue;
			request.IsDue = inspectionRequest.IsDue;
			request.InDays = inspectionRequest.InDays;
			request.IsShowDetained = inspectionRequest.IsShowDetained;
			request.IsSummaryClicked = inspectionRequest.IsSummaryClicked;
			request.Company = inspectionRequest.Company;
			request.CompanyId = inspectionRequest.CompanyId;
			request.Inspector = inspectionRequest.Inspector;
			request.InspectionTypeTextField = inspectionRequest.InspectionTypeTextField;
			request.StatusList = inspectionRequest.StatusList;

			if (inspectionRequest.StatusList != null && inspectionRequest.StatusList.Any())
			{
				request.IsFindingOutstanding = inspectionRequest.StatusList.Any(x => x.Equals(InspectionsFilter.Outstanding.ToString()));
				request.IsFindingOverdue = inspectionRequest.StatusList.Any(x => x.Equals(InspectionsFilter.Overdue.ToString()));
				request.IsPendingClosure = inspectionRequest.StatusList.Any(x => x.Equals(InspectionsFilter.Complete.ToString()));
				request.IsClosed = inspectionRequest.StatusList.Any(x => x.Equals(InspectionsFilter.Closed.ToString()));
				request.IsAllSelected = inspectionRequest.StatusList.Any(x => x.Equals(InspectionsFilter.AllInspections.ToString()));
			}
			else
			{
				request.IsFindingOutstanding = false;
				request.IsFindingOverdue = false;
				request.IsPendingClosure = false;
				request.IsClosed = false;
				request.IsAllSelected = false;
			}

			if (inspectionRequest.InspectionTypeIds != null && inspectionRequest.InspectionTypeIds.Any())
			{
				request.strInspectionTypeIds = string.Join(',', inspectionRequest.InspectionTypeIds);
			}

			string inspectionURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));

			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey), inspectionURL, inspectionRequest.VesselId);

			return new JsonResult(new { data = request, vesselId = inspectionRequest.VesselId });
		}

		/// <summary>
		/// Findingses this instance.
		/// </summary>
		/// <param name="finding">The finding.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public async Task<IActionResult> Findings(string finding, string vesselId, bool IsVesselChanged, string context)
		{

			InspectionFindingRequestViewModel inspectionFindingRequestViewModel = new InspectionFindingRequestViewModel();

			if (!string.IsNullOrWhiteSpace(finding))
			{
				string data = _provider.CreateProtector("Inspection").Unprotect(finding);
				inspectionFindingRequestViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionFindingRequestViewModel>(data);
				inspectionFindingRequestViewModel.InspectionUrl = finding;
			}

			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
			string newVesselId = decreptedString.Split(Constants.Separator)[0];
			inspectionFindingRequestViewModel.VesselName = decreptedString.Split(Constants.Separator)[1];

			if (!string.IsNullOrWhiteSpace(context))
			{
				ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
				inspectionFindingRequestViewModel.InspectionId = contextParameter.InspectionId;
				inspectionFindingRequestViewModel.VesselId = newVesselId;

				_client.AccessToken = GetAccessToken();
				InspectionAndInspectorDetailsViewModel result = await _client.GetInspectionAndInspectorDetailsAsync(inspectionFindingRequestViewModel.InspectionId);
				if (result != null)
				{
					inspectionFindingRequestViewModel.InspectionTypeId = result.InspectionTypeId;
					inspectionFindingRequestViewModel.InspectionName = result.InspectionName;
					inspectionFindingRequestViewModel.OccuredDate = result.EndDate.HasValue ? result.EndDate.Value.ToString("dd MMM yyyy") : result.StartDate.HasValue ? result.StartDate.Value.ToString("dd MMM yyyy") : "";
				}
				inspectionFindingRequestViewModel.InspectionUrl = CommonUtil.GetEncryptedURL(_provider, Constants.InspectionEncryptionKey, inspectionFindingRequestViewModel);
			}

			if (inspectionFindingRequestViewModel.VesselId != newVesselId || IsVesselChanged)
			{
				return RedirectToAction("List", new { Inspection = finding, VesselId = vesselId });
			}

			inspectionFindingRequestViewModel.VesselId = vesselId;

			string typeId = "";
			if (inspectionFindingRequestViewModel.InspectionTypeId != null)
			{
				typeId = inspectionFindingRequestViewModel.InspectionTypeId;
			}

			if (typeId == EnumsHelper.GetKeyValue(InspectionType.PortStateControl))
			{
				inspectionFindingRequestViewModel.IsPscVisible = true;
				inspectionFindingRequestViewModel.IsOMVType = false;
				inspectionFindingRequestViewModel.IsCausesSectionVisible = true;
			}
			else if (typeId == EnumsHelper.GetKeyValue(InspectionType.OilMajorVetting))
			{
				inspectionFindingRequestViewModel.IsPscVisible = false;
				inspectionFindingRequestViewModel.IsOMVType = true;
				inspectionFindingRequestViewModel.IsCausesSectionVisible = false;
			}
			else
			{
				inspectionFindingRequestViewModel.IsPscVisible = false;
				inspectionFindingRequestViewModel.IsCausesSectionVisible = false;
			}
			string[] contextParams = { inspectionFindingRequestViewModel.InspectionId };
			string[] messageParams = { inspectionFindingRequestViewModel.InspectionName, inspectionFindingRequestViewModel.OccuredDate };

			inspectionFindingRequestViewModel.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.Inspection, newVesselId, CommonUtil.GetVesselNameFromDisplayName(inspectionFindingRequestViewModel.VesselName), contextParams, messageParams, inspectionFindingRequestViewModel.InspectionId);
			inspectionFindingRequestViewModel.IsFromViewRecord = IsFromViewRecordVal(context);
			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.InspectionFindingPageKey);
			SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey), finding);
			inspectionFindingRequestViewModel.EncryptedInspectionId = CommonUtil.GetEncryptedURL(_provider, Constants.InspectionIdEncryptionText, inspectionFindingRequestViewModel.InspectionId);
			inspectionFindingRequestViewModel.EncryptedVesselId = vesselId;

			inspectionFindingRequestViewModel.ActiveMobileTabClass = SetTab(pageKey, inspectionFindingRequestViewModel.ActiveMobileTabClass, Constants.Tab1);

			return View(inspectionFindingRequestViewModel);
		}

		/// <summary>
		/// Actionses this instance.
		/// </summary>
		/// <param name="inspectionAction">The inspection action.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public async Task<IActionResult> Actions(string inspectionAction, string vesselId, bool IsVesselChanged, string context)
		{

			InspectionActionRequestViewModel inspectionActionRequestViewModel = new InspectionActionRequestViewModel();
			if (!string.IsNullOrWhiteSpace(inspectionAction))
			{
				string data = _provider.CreateProtector("Inspection").Unprotect(inspectionAction);
				inspectionActionRequestViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionActionRequestViewModel>(data);
				inspectionActionRequestViewModel.InspectionUrl = inspectionAction;
			}

			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
			string newVesselId = decreptedString.Split(Constants.Separator)[0];
			inspectionActionRequestViewModel.VesselName = decreptedString.Split(Constants.Separator)[1];

			if (!string.IsNullOrWhiteSpace(context))
			{
				ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
				inspectionActionRequestViewModel.InspectionFindingId = contextParameter.FindingId;
				inspectionActionRequestViewModel.VesselId = newVesselId;

				_client.AccessToken = GetAccessToken();
				InspectionFindingDetailsViewModel result = await _client.GetInspectionFindingAndCausationDetails(inspectionActionRequestViewModel.InspectionFindingId);
				if (result != null)
				{
					inspectionActionRequestViewModel.InspectionId = result.InspectionId;
					inspectionActionRequestViewModel.VesselReferenceNo = result.VesRef;
					inspectionActionRequestViewModel.InspectionName = result.InspectionName;
				}
				inspectionActionRequestViewModel.InspectionUrl = CommonUtil.GetEncryptedURL(_provider, Constants.InspectionEncryptionKey, inspectionActionRequestViewModel);
			}

			if (inspectionActionRequestViewModel.VesselId != newVesselId || IsVesselChanged)
			{
				return RedirectToAction("List", new { Inspection = inspectionAction, VesselId = vesselId });
			}

			inspectionActionRequestViewModel.VesselId = vesselId;
			string[] contextParams = { inspectionActionRequestViewModel.InspectionFindingId, inspectionActionRequestViewModel.InspectionId };
			string[] messageParams = { inspectionActionRequestViewModel.VesselReferenceNo };
			inspectionActionRequestViewModel.IsFromViewRecord = IsFromViewRecordVal(context);
			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.InspectionActionPageKey);
			inspectionActionRequestViewModel.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.InspectionFinding, newVesselId, CommonUtil.GetVesselNameFromDisplayName(inspectionActionRequestViewModel.VesselName), contextParams, messageParams, inspectionActionRequestViewModel.InspectionFindingId);
			SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey), inspectionAction);
			inspectionActionRequestViewModel.ActiveMobileTabClass = SetTab(pageKey, inspectionActionRequestViewModel.ActiveMobileTabClass, Constants.Tab1);
			return View(inspectionActionRequestViewModel);
		}

		/// <summary>
		/// Gets the inspection findings by inspection identifier.
		/// </summary>
		/// <param name="inspectionFindingRequest">The inspection finding request.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetInspectionFindingsByInspectionId(InspectionFindingRequestViewModel inspectionFindingRequest)
		{
			_client.AccessToken = GetAccessToken();
			List<InspectionFindingResponseViewModel> response = new List<InspectionFindingResponseViewModel>();

			string vesselId = _provider.CreateProtector("Vessel").Unprotect(inspectionFindingRequest.VesselId);
			inspectionFindingRequest.VesselId = vesselId.Split(Constants.Separator)[0];
			inspectionFindingRequest.VesselName = vesselId.Split(Constants.Separator)[1];

			response = await _client.GetInspectionFindingsByInspectionId(inspectionFindingRequest);

			if (response != null && response.Any())
			{
				RecordDiscussionRequestViewModel request1 = new RecordDiscussionRequestViewModel();
				request1.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.InspectionFinding));
				request1.ReferenceIds = response.Select(x => x.InspectionFindingId).ToList();

				_notificationClient.AccessToken = GetAccessToken();
				List<RecordDiscussionResponse> DiscussionAndNotesCountList = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(request1);

				foreach (var item in DiscussionAndNotesCountList.Where(x => x.ChannelCount > 0 || x.NotesCount > 0))
				{
					foreach (var finding in response.Where(x => x.InspectionFindingId == item.ReferenceIdentifier))
					{
						NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
						{
							CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.InspectionFinding)),
							ReferenceIdentifier = item.ReferenceIdentifier
						};

						finding.ChannelCount = item.ChannelCount;
						finding.NotesCount = item.NotesCount;
						finding.MessageDetailsJSON = JsonConvert.SerializeObject(newMessageDetails);
					}
				}
			}

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the finding actions by finding identifier.
		/// </summary>
		/// <param name="inspectionActionRequestViewModel">The inspection action request view model.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetFindingActionsByFindingId(InspectionActionRequestViewModel inspectionActionRequestViewModel)
		{
			_client.AccessToken = GetAccessToken();
			List<InspectionActionResponseViewModel> response = new List<InspectionActionResponseViewModel>();

			response = await _client.GetFindingActionsByFindingId(inspectionActionRequestViewModel);

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the inspection findings count by inspection identifier.
		/// </summary>
		/// <param name="inspectionFindingRequest">The inspection finding request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetInspectionFindingsCountByInspectionId(InspectionFindingRequestViewModel inspectionFindingRequest)
		{
			_client.AccessToken = GetAccessToken();

			string data = _provider.CreateProtector("Inspection").Unprotect(inspectionFindingRequest.InspectionUrl);
			InspectionFindingRequestViewModel inspectionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionFindingRequestViewModel>(data);
			string inspectionId = inspectionRequest.InspectionId;

			InspectionFindingSummaryResponseViewModel response = new InspectionFindingSummaryResponseViewModel();

			if (!string.IsNullOrWhiteSpace(inspectionId))
			{
				response = await _client.GetInspectionFindingsCountByInspectionId(inspectionId);
			}
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the vessel inspection detail list.
		/// </summary>
		/// <param name="pageRequest">The page request.</param>
		/// <param name="inspectionRequestViewModel">The inspection request view model.</param>
		/// <returns></returns>
		public async Task<ActionResult> GetInspectionList(DataTablePageRequest<string> pageRequest, InspectionRequestViewModel inspectionRequestViewModel)
		{
			_client.AccessToken = GetAccessToken();

			string vesselId = _provider.CreateProtector("Vessel").Unprotect(inspectionRequestViewModel.VesselId);
			inspectionRequestViewModel.VesselId = vesselId.Split(Constants.Separator)[0];
			inspectionRequestViewModel.VesselName = vesselId.Split(Constants.Separator)[1];

			if (inspectionRequestViewModel.strInspectionTypeIds != null)
			{
				inspectionRequestViewModel.InspectionTypeIds = inspectionRequestViewModel.strInspectionTypeIds.Split(",").ToList();
			}

			DataTablePageResponse<List<VesselInspectionViewModel>> response = await _client.GetVesselInspectionDetailList(pageRequest, inspectionRequestViewModel);

			if (response.Data != null && response.Data.Any())
			{
				RecordDiscussionRequestViewModel request1 = new RecordDiscussionRequestViewModel();
				request1.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.Inspection));
				request1.ReferenceIds = response.Data.Select(x => x.InspectionId).ToList();

				_notificationClient.AccessToken = GetAccessToken();
				List<RecordDiscussionResponse> DiscussionAndNotesCountList = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(request1);

				foreach (var item in DiscussionAndNotesCountList.Where(x => x.ChannelCount > 0 || x.NotesCount > 0))
				{
					foreach (var inspection in response.Data.Where(x => x.InspectionId == item.ReferenceIdentifier))
					{
						NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
						{
							CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.Inspection)),
							ReferenceIdentifier = item.ReferenceIdentifier
						};

						inspection.ChannelCount = item.ChannelCount;
						inspection.NotesCount = item.NotesCount;
						inspection.MessageDetailsJSON = JsonConvert.SerializeObject(newMessageDetails);
					}
				}
			}


			return new JsonResult(new DataTablePageResponse<List<VesselInspectionViewModel>>
			{
				Draw = pageRequest.Draw,
				RecordsFiltered = response.RecordsFiltered,
				Data = response.Data,
				RecordsTotal = response.RecordsTotal
			});
		}

		/// <summary>
		/// Gets the inspection manager due in days.
		/// </summary>
		/// <returns></returns>
		public async Task<JsonResult> GetInspectionManagerDueInDays()
		{
			_client.AccessToken = GetAccessToken();
			List<InspectionOverviewFilterResponseViewModel> response = new List<InspectionOverviewFilterResponseViewModel>();
			response = await _client.PostGetDueFilter(InspectionManagerOverviewFilter.InspectionManagerDueInDays);

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the planning list.
		/// </summary>
		/// <param name="inspectionOverviewPlanningRequest">The inspection overview planning request.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetPlanningList(InspectionOverviewPlanningRequest inspectionOverviewPlanningRequest)
		{
			_client.AccessToken = GetAccessToken();
			List<InspectionViewModel> response = new List<InspectionViewModel>();

			string vesselId = _provider.CreateProtector("Vessel").Unprotect(inspectionOverviewPlanningRequest.VesselId);

			inspectionOverviewPlanningRequest.VesselId = vesselId.Split(Constants.Separator)[0];
			inspectionOverviewPlanningRequest.VesselName = vesselId.Split(Constants.Separator)[1];

			response = await _client.GetInspectionOverviewPlanningDetail(inspectionOverviewPlanningRequest);

			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Gets the inspection dashboard details.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetInspectionDashboardDetails(InspectionManagerDashboardRequestViewModel request)
		{
			_client.AccessToken = GetAccessToken();
			InspectionManagerDashboardDetailViewModel inspectionDashboard = await _client.PostGetInspectionManagerDashboardDetail(request);
			return new JsonResult(inspectionDashboard);
		}

		/// <summary>
		/// Gets the inspection type tree.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<ActionResult> GetInspectionTypeTree(InspectionTypeDetailRequestViewModel input)
		{
			_client.AccessToken = GetAccessToken();
			List<string> inspectionTypeHeader = new List<string>();

			List<InspectionTypeDetailViewModel> inspectionTypeList = await _client.GetInspectionTypeWithVesselTypeFilter(input);

			List<TreeViewModel<InspectionTypeDetailViewModel>> treeList = new List<TreeViewModel<InspectionTypeDetailViewModel>>();

			TreeViewModel<InspectionTypeDetailViewModel> AllOption = new TreeViewModel<InspectionTypeDetailViewModel>
			{
				Title = "All",
				Expanded = true,
				Key = null,
				Checkbox = true,
				Lazy = false,
				Tooltip = "All",
				Children = new List<TreeViewModel<InspectionTypeDetailViewModel>>(),
				AdditionalData = new InspectionTypeDetailViewModel()
				{
					InspectionTypeId = null
				}
			};

			if (inspectionTypeList != null && inspectionTypeList.Any())
			{
				inspectionTypeHeader = inspectionTypeList.OrderBy(y => y.InspectionHeaderType).Select(x => x.InspectionHeaderType).Distinct().ToList();
			}

			if (inspectionTypeHeader != null && inspectionTypeHeader.Any())
			{
				foreach (string inspectionTypeHeaderName in inspectionTypeHeader)
				{
					string headerId = inspectionTypeHeaderName;

					List<string> typeHeader = null;
					List<TreeViewModel<InspectionTypeDetailViewModel>> childItems = new List<TreeViewModel<InspectionTypeDetailViewModel>>();
					if (inspectionTypeList != null && inspectionTypeList.Any())
					{
						typeHeader = inspectionTypeList.Where(x => x.InspectionHeaderType == headerId).Select(y => y.Type).Distinct().ToList();
					}
					foreach (var typeHeaderName in typeHeader.OrderBy(y => y))
					{
						List<TreeViewModel<InspectionTypeDetailViewModel>> subchildItems = new List<TreeViewModel<InspectionTypeDetailViewModel>>();
						string typeId = typeHeaderName;
						if (inspectionTypeList.Where(x => x.InspectionHeaderType == inspectionTypeHeaderName && x.Type == typeHeaderName).Any())
						{
							subchildItems.AddRange(inspectionTypeList.Where(x => x.InspectionHeaderType == inspectionTypeHeaderName && x.Type == typeHeaderName).Select(y =>
							new TreeViewModel<InspectionTypeDetailViewModel>
							{
								Key = y.InspectionTypeId,
								Title = y.InspectionType,
								Tooltip = y.InspectionType,
								Expanded = false,
								Checkbox = true,
								Lazy = false,
								Children = null,
								AdditionalData = new InspectionTypeDetailViewModel()
								{
									IsAuditType = y.IsAuditType,
									IsInternal = y.IsInternal,
									Type = y.Type
								}
							}));
						}

						childItems.Add(new TreeViewModel<InspectionTypeDetailViewModel>
						{
							Key = "",
							Title = typeHeaderName,
							Tooltip = typeHeaderName,
							Expanded = false,
							Checkbox = true,
							Lazy = false,
							Children = subchildItems,
							AdditionalData = new InspectionTypeDetailViewModel()
							{
								IsAuditType = typeHeaderName == "Audit" ? true : false,
								IsInternal = inspectionTypeHeaderName == "Internal" ? true : false,
								Type = typeHeaderName
							}
						});
					}

					AllOption.Children.Add(new TreeViewModel<InspectionTypeDetailViewModel>
					{
						Key = "",
						Title = inspectionTypeHeaderName,
						Tooltip = inspectionTypeHeaderName,
						Expanded = false,
						Checkbox = true,
						Lazy = false,
						Children = childItems,
						AdditionalData = new InspectionTypeDetailViewModel()
						{
							IsAuditType = false,
							IsInternal = inspectionTypeHeaderName == "Internal" ? true : false,
							Type = null
						}
					});
				}
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		/// <summary>
		/// Posts the get inspection and inspector details.
		/// </summary>
		/// <param name="inspectionUrl">The inspection Url.</param>
		/// <returns>
		/// Task of InspectionAndInspectorDetailsViewModel
		/// </returns>
		public async Task<IActionResult> PostGetInspectionAndInspectorDetails(string inspectionUrl)
		{
			string data = _provider.CreateProtector("Inspection").Unprotect(inspectionUrl);
			InspectionFindingRequestViewModel inspectionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionFindingRequestViewModel>(data);
			string inspectionId = inspectionRequest.InspectionId;
			_client.AccessToken = GetAccessToken();
			InspectionAndInspectorDetailsViewModel result = await _client.GetInspectionAndInspectorDetailsAsync(inspectionId);
			return new JsonResult(result);
		}

		/// <summary>
		/// Posts the get inspection finding details.
		/// </summary>
		/// <param name="inspectionUrl">The inspection Url.</param>
		/// <returns>
		/// Task of InspectionFindingDetailsViewModel
		/// </returns>
		public async Task<IActionResult> PostGetInspectionFindingDetails(string inspectionUrl)
		{
			string data = _provider.CreateProtector("Inspection").Unprotect(inspectionUrl);
			InspectionActionRequestViewModel inspectionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionActionRequestViewModel>(data);
			_client.AccessToken = GetAccessToken();
			InspectionFindingDetailsViewModel result = await _client.GetInspectionFindingAndCausationDetails(inspectionRequest.InspectionFindingId);
			return new JsonResult(result);
		}

		/// <summary>
		/// Gets the company lookup.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="q">The q.</param>
		/// <param name="_type">The type.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetCompanyLookup(string term, string q, string _type, int page)
		{
			_sharedClient.AccessToken = GetAccessToken();

			Select2ResponseViewModel<List<CompanySearchResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<CompanySearchResponseViewModel>>();
			select2ResponseViewModel.Results = new List<CompanySearchResponseViewModel>();
			DataTablePageResponse<List<CompanySearchResponseViewModel>> response = new DataTablePageResponse<List<CompanySearchResponseViewModel>>();

			CompanySearchRequest request = new CompanySearchRequest();
			request.FetchDefaultCurrency = false;
			request.FetchDefaultCredit = false;
			request.CountryId = null;
			request.IsAgent = false;

			request.FetchAPValid = false;
			request.FetchOnlyActivatedAccountingCompanies = false;
			request.IsFullTextSearch = false;
			request.FetchOnlyAPEnable = false;
			request.CompanyName = term;
			var _searchCompanyTypes = new List<string>();
			var CompanyTypes = new List<CompanyTypeEnum>
					{
						CompanyTypeEnum.Broker,
						CompanyTypeEnum.ClassificationSociety,
						CompanyTypeEnum.CrewManagementOffices,
						CompanyTypeEnum.FreightSupplier,
						CompanyTypeEnum.HMLeadUnderwriter,
						CompanyTypeEnum.ManagementOffice,
						CompanyTypeEnum.PANDIClub,
						CompanyTypeEnum.PurchaseSupplier,
						CompanyTypeEnum.ShipManagementOffices,
						CompanyTypeEnum.Supplier,
						CompanyTypeEnum.Warehouse,
						CompanyTypeEnum.YARDS,
						CompanyTypeEnum.BeneficialOwner,
						CompanyTypeEnum.RegisteredOwner,
						CompanyTypeEnum.ShipOwner,
						CompanyTypeEnum.OilMajorVettingCompany,
						CompanyTypeEnum.ThirdPartyAgent,
						CompanyTypeEnum.ThirdPartyInspector
					};
			if (CompanyTypes != null && CompanyTypes.Any())
			{
				_searchCompanyTypes.AddRange(CompanyTypes.Select(x => EnumsHelper.GetKeyValue(x)));
			}

			request.CompanyTypeIds = _searchCompanyTypes;

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
				response = await _sharedClient.PostGetSupplierDetails(pageRequest, request);
			}
			select2ResponseViewModel.Results = response.Data;
			select2ResponseViewModel.Pagination = new Pagination();
			select2ResponseViewModel.Pagination.More = response.RecordsTotal > (pageRequest.Length * page);

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Binds the inspection dashboard details.
		/// </summary>
		/// <param name="inspection">The inspection.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <returns></returns>
		public async Task<JsonResult> BindInspectionDashboardDetails(string inspection, string vesselId)
		{
			_client.AccessToken = GetAccessToken();
			InspectionRequestViewModel request = await InspectionListDetails(inspection, vesselId);
			string inspectionURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
			request.ActiveMobileTabClass = Constants.Tab2;
			return new JsonResult(new { data = request, vesselId = request.VesselId });
		}

        /// <summary>
        /// Fetch filter data from TempData["InspectionFilter"]
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> MaintainFilter()
		{
			_client.AccessToken = GetAccessToken();
			string referer = Request.Headers["Referer"].ToString();

			InspectionRequestViewModel inspectionRequest = new InspectionRequestViewModel();
			string inspection = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey));
			string vesselId = GetSessionVesselFilter(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey));


			if (!string.IsNullOrWhiteSpace(inspection) && !string.IsNullOrWhiteSpace(vesselId))
			{
				InspectionRequestViewModel request = await InspectionListDetails(inspection, vesselId);

				return new JsonResult(new { data = request, vesselId = request.VesselId, isTempDataExist = true });
			}
			else
			{
				return new JsonResult(new { data = string.Empty, vesselId = string.Empty, isTempDataExist = false });
			}
		}

		/// <summary>
		/// Inspection List Details.
		/// </summary>
		/// <param name="inspection">The inspection.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <returns>
		/// inspectionRequestViewModel
		/// </returns>
		[NonAction]
		private async Task<InspectionRequestViewModel> InspectionListDetails(string inspection, string vesselId)
		{
			InspectionTypeDetailRequestViewModel inspectionTypeInput = new InspectionTypeDetailRequestViewModel();
			InspectionRequestViewModel inspectionRequestViewModel = new InspectionRequestViewModel();

			string data = _provider.CreateProtector("Inspection").Unprotect(inspection);
			inspectionRequestViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionRequestViewModel>(data);
			inspectionRequestViewModel.VesselId = vesselId;

			if (inspectionRequestViewModel.FromDate == null && inspectionRequestViewModel.ToDate == null)
			{
				inspectionRequestViewModel.FromDate = DateTime.Now.AddMonths(-6);
				inspectionRequestViewModel.ToDate = DateTime.Now;
				inspectionRequestViewModel.InspectionType = InspectionDashboardType.AllInspection;
				inspectionRequestViewModel.IsSummaryClicked = true;
			}

			List<InspectionOverviewFilterResponseViewModel> dueInTypeList = await _client.PostGetDueFilter(InspectionManagerOverviewFilter.InspectionManagerDueInDays);
			if (dueInTypeList != null && dueInTypeList.Any())
			{
				InspectionOverviewFilterResponseViewModel detail = dueInTypeList.Any(x => x.IsDefault) ? dueInTypeList.Where(x => x.IsDefault).FirstOrDefault() : dueInTypeList.Any(x => x.Value == 30) ? dueInTypeList.Where(x => x.Value == 30).FirstOrDefault() : dueInTypeList.FirstOrDefault();
				inspectionRequestViewModel.InDays = detail.Value;
			}

			inspectionTypeInput.Ves_Id = vesselId;
			List<InspectionTypeDetailViewModel> inspectionTypeList = await _client.GetInspectionTypeWithVesselTypeFilter(inspectionTypeInput);

			List<string> typeInspection = inspectionTypeList.Where(x => x.IsAuditType == false).Select(x => x.InspectionTypeId).ToList();
			List<string> typeAudit = inspectionTypeList.Where(x => x.IsAuditType == true).Select(x => x.InspectionTypeId).ToList();
			List<string> typeAll = inspectionTypeList.Select(x => x.InspectionTypeId).ToList();

			if (inspectionRequestViewModel.IsSummaryClicked)
			{
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.InspectionPSCType)
				{
					inspectionRequestViewModel.IsDetention = true;
					inspectionRequestViewModel.InspectionTypeIds = inspectionTypeList.Where(x => x.InspectionTypeId == EnumsHelper.GetKeyValue(InspectionType.PortStateControl)).Select(x => x.InspectionTypeId).ToList();
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsClosed = true;
					inspectionRequestViewModel.IsAllSelected = true;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				else if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.OMVRejectionType)
				{
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.InspectionTypeIds = inspectionTypeList.Where(x => x.InspectionTypeId == EnumsHelper.GetKeyValue(InspectionType.OilMajorVetting)).Select(x => x.InspectionTypeId).ToList();
					inspectionRequestViewModel.IsOMVRejection = true;
				}
				else if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.PSCDeficiencyType)
				{
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.InspectionTypeIds = inspectionTypeList.Where(x => x.InspectionTypeId == EnumsHelper.GetKeyValue(InspectionType.PortStateControl)).Select(x => x.InspectionTypeId).ToList();
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsClosed = true;
					inspectionRequestViewModel.IsAllSelected = true;
					inspectionRequestViewModel.IsPSCDeficency = true;
					inspectionRequestViewModel.IsOMVRejection = false;
				}

				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.DetentionType)
				{
					inspectionRequestViewModel.IsShowDetained = true;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsNeverdone = false;
					inspectionRequestViewModel.InspectionTypeIds = inspectionTypeList.Where(x => x.InspectionTypeId == EnumsHelper.GetKeyValue(InspectionType.PortStateControl)).Select(x => x.InspectionTypeId).ToList();
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsClosed = true;
					inspectionRequestViewModel.IsAllSelected = true;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.InspectionOMVType)
				{
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.IsNeverdone = false;
					inspectionRequestViewModel.InspectionTypeIds = inspectionTypeList.Where(x => x.InspectionTypeId == EnumsHelper.GetKeyValue(InspectionType.OilMajorVetting)).Select(x => x.InspectionTypeId).ToList();
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsClosed = true;
					inspectionRequestViewModel.IsAllSelected = true;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.InspectionDueType)
				{
					inspectionRequestViewModel.IsDue = true;
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = true;
					inspectionRequestViewModel.IsInspection = default(bool);
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.InspectionOverdueType)
				{
					inspectionRequestViewModel.IsOverdue = true;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.IsInspection = default(bool);
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.InspectionNeverDoneType)
				{
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.IsNeverdone = true;
					inspectionRequestViewModel.IsInspection = default(bool);
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.InspectionFindingOutstandingType)
				{
					inspectionRequestViewModel.InspectionFilter = InspectionsFilter.Outstanding;
					inspectionRequestViewModel.IsInspection = true;
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.InspectionTypeIds = typeInspection;
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.InspectionFindingOverdueType)
				{
					inspectionRequestViewModel.InspectionFilter = InspectionsFilter.Overdue;
					inspectionRequestViewModel.IsInspection = true;
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.InspectionTypeIds = typeInspection;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.PendingClosureByOfficeType)
				{
					inspectionRequestViewModel.InspectionFilter = InspectionsFilter.Complete;
					inspectionRequestViewModel.IsInspection = true;
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.InspectionTypeIds = typeInspection;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.AuditInspectionFindingOutstandingType)
				{
					inspectionRequestViewModel.InspectionFilter = InspectionsFilter.Outstanding;
					inspectionRequestViewModel.IsInspection = false;
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.InspectionTypeIds = typeAudit;
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.AuditInspectionFindingOverdueType)
				{
					inspectionRequestViewModel.IsInspection = false;
					inspectionRequestViewModel.InspectionFilter = InspectionsFilter.Overdue;
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.InspectionTypeIds = typeAudit;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.AuditPendingClosureByOfficeType)
				{
					inspectionRequestViewModel.InspectionFilter = InspectionsFilter.Complete;
					inspectionRequestViewModel.IsInspection = false;
					inspectionRequestViewModel.IsOverdue = false;
					inspectionRequestViewModel.IsDue = false;
					inspectionRequestViewModel.InspectionTypeIds = typeAudit;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.AllInspection)
				{
					inspectionRequestViewModel.InspectionFilter = InspectionsFilter.AllInspections;
					inspectionRequestViewModel.InDays = 0;
					inspectionRequestViewModel.InspectionTypeIds = null;
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsClosed = true;
					inspectionRequestViewModel.IsAllSelected = true;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.FindingsOutstandingType)
				{
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.FindingsOverdueType)
				{
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.VesselInspectionReport)
				{
					inspectionRequestViewModel.InspectionTypeIds = new List<string>() { EnumsHelper.GetKeyValue(InspectionType.VesselInspectionReport) };
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.OpenOMVFindingsType)
				{
					inspectionRequestViewModel.InDays = 0;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
					inspectionRequestViewModel.InspectionTypeIds = new List<string>() { EnumsHelper.GetKeyValue(InspectionType.OilMajorVetting) };
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsPendingClosure = true;
				}
				else if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.OMVFindingsType)
				{
					inspectionRequestViewModel.InDays = 0;
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
					inspectionRequestViewModel.InspectionTypeIds = new List<string>() { EnumsHelper.GetKeyValue(InspectionType.OilMajorVetting) };
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsClosed = true;
				}
				if (inspectionRequestViewModel.InspectionType == InspectionDashboardType.InspectionPscDeficiencyType)
				{
					inspectionRequestViewModel.IsDetention = false;
					inspectionRequestViewModel.IsOMVRejection = false;
					inspectionRequestViewModel.InspectionTypeIds = inspectionTypeList.Where(x => x.InspectionTypeId == EnumsHelper.GetKeyValue(InspectionType.PortStateControl)).Select(x => x.InspectionTypeId).ToList();
					inspectionRequestViewModel.IsFindingOutstanding = true;
					inspectionRequestViewModel.IsFindingOverdue = true;
					inspectionRequestViewModel.IsPendingClosure = true;
					inspectionRequestViewModel.IsClosed = true;
				}
				if (inspectionRequestViewModel.InspectionTypeIds != null && inspectionRequestViewModel.InspectionTypeIds.Any())
				{
					inspectionRequestViewModel.strInspectionTypeIds = string.Join(',', inspectionRequestViewModel.InspectionTypeIds);
				}
				string inspectionURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(inspectionRequestViewModel));
				SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey), inspectionURL, inspectionRequestViewModel.VesselId);
			}

			return inspectionRequestViewModel;
		}

		/// <summary>
		/// Prints the sire report asynchronous.
		/// </summary>
		/// <param name="inspectionUrl">The inspection URL.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <returns></returns>
		public async Task<JsonResult> ExportToExcelSummeryReport(string inspectionUrl, string vesselId)
		{
			_sharedClient.AccessToken = GetAccessToken();
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
			string VesselId_decrept = decreptedString.Split(Constants.Separator)[0];

			string data = _provider.CreateProtector("Inspection").Unprotect(inspectionUrl);
			InspectionFindingRequestViewModel inspectionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionFindingRequestViewModel>(data);
			string inspectionId_decrept = inspectionRequest.InspectionId;

			ReportLight reportRequest = await _sharedClient.GetReportLightByFilename(EnumsHelper.GetKeyValue(ReportMaster.MarineInspectionShipSecurityAssessmentExcelReport));

			if (reportRequest != null)
			{
				reportRequest.FriendlyFileName = Constants.VesselInspectionReportFileName;
				reportRequest.ReportFormat = ReportExportTypes.Excel;

				foreach (var reportParameter in reportRequest.ReportParameters)
				{
					if (reportParameter.ParameterName.Equals("@sVST_ID"))
					{
						reportParameter.ValueToSet = new List<object>() { inspectionId_decrept };
					}
					if (reportParameter.ParameterName.Equals("@sVES_ID"))
					{
						reportParameter.ValueToSet = new List<object>() { VesselId_decrept };
					}
					if (reportParameter.ParameterName.Equals("@Causes"))
					{
						reportParameter.ValueToSet = new List<object>() { false };
					}
					if (reportParameter.ParameterName.Equals("@Refnos"))
					{
						reportParameter.ValueToSet = new List<object>() { string.Empty };
					}
				}

				var reportRequestId = await _sharedClient.InitiateReportRequest(reportRequest);
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
		/// Routes to inspection using session.
		/// </summary>
		/// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
		/// <returns></returns>
		public IActionResult RouteToInspectionUsingSession(string encryptedVesselId)
		{
			//removing from session
			GetSourceURL(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionFindingPageKey));
			GetSourceURL(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionActionPageKey));

			string defaultObject = CommonUtil.GetEncryptedURL(_provider, Constants.InspectionEncryptionKey, new InspectionRequestViewModel());
			return Json(Url.Action(Constants.ListMethod, new { inspection = defaultObject, vesselId = encryptedVesselId }));
		}

		/// <summary>
		/// Gets the findings report details.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetFindingsReportDetails()
		{
			List<Lookup> formatList = new List<Lookup>();

			var item = FileFormatTypes.FormatPDF;
			formatList.Add(new Lookup() { Identifier = EnumsHelper.GetKeyValue(item), Description = EnumsHelper.GetDescription(item) });

			item = FileFormatTypes.FormatTypeMSWord;
			formatList.Add(new Lookup() { Identifier = EnumsHelper.GetKeyValue(item), Description = EnumsHelper.GetDescription(item) });

			return new JsonResult(new
			{
				FormatList = formatList
			});
		}

		/// <summary>
		/// Downloads the inspection finding report.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<JsonResult> DownloadInspectionFindingReport(InspectionVIRReportRequestViewModel input)
		{
			InspectionVIRReportRequest request = new InspectionVIRReportRequest();

			string data = _provider.CreateProtector("Inspection").Unprotect(input.InspectionUrl);
			InspectionFindingRequestViewModel inspectionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionFindingRequestViewModel>(data);
			request.InspectionId = inspectionRequest.InspectionId;

			string decryptedVesselDetails = _provider.CreateProtector("Vessel").Unprotect(input.EncryptedVesselId);
			request.VesselId = decryptedVesselDetails.Split(Constants.Separator)[0];

			if (!String.IsNullOrWhiteSpace(input.ReportFormat))
			{
				if (input.ReportFormat == EnumsHelper.GetKeyValue(FileFormatTypes.FormatPDF))
				{
					request.ReportExportType = ReportExportTypes.PDF;
				}
				else if (input.ReportFormat == EnumsHelper.GetKeyValue(FileFormatTypes.FormatTypeMSWord))
				{
					request.ReportExportType = ReportExportTypes.Word;
				}
			}
			else
			{
				request.ReportExportType = ReportExportTypes.PDF;
			}

			string result = null;

			if (input.ReportType == EnumsHelper.GetKeyValue(InspectionFindingsReportType.QaDetail))
			{
				result = await PrintQaDetailReport(request);
			}
			else if (input.ReportType == EnumsHelper.GetKeyValue(InspectionFindingsReportType.Detail))
			{
				result = await PrintDetailReport(request);
			}
			else if (input.ReportType == EnumsHelper.GetKeyValue(InspectionFindingsReportType.Summary))
			{
				result = await PrintSummaryReport(request);
			}

			if (result != null && !String.IsNullOrEmpty(result) && !String.IsNullOrWhiteSpace(result))
			{
				return new JsonResult(new { message = Messages.ReportGenerationSuccessMessage, success = true });
			}
			else
			{
				return new JsonResult(new { message = Messages.ReportGenerationErrorMessage, success = false });
			}
		}

		/// <summary>
		/// Prints the qa detail report.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		private async Task<string> PrintQaDetailReport(InspectionVIRReportRequest request)
		{
			_sharedClient.AccessToken = GetAccessToken();
			ReportLight reportRequest = await _sharedClient.GetReportLightByFilename(EnumsHelper.GetKeyValue(ReportMaster.MarineInspectionQuestionAnswerReport));

			if (reportRequest != null)
			{
				reportRequest.ReportFormat = request.ReportExportType;
				foreach (var p in reportRequest.ReportParameters)
				{
					foreach (var reportParameter in reportRequest.ReportParameters)
					{
						if (reportParameter.ParameterName.Contains("@sVesId"))
						{
							reportParameter.ValueToSet = new List<object>() { request.VesselId };
						}
						if (reportParameter.ParameterName.Contains("@sInspectionId"))
						{
							reportParameter.ValueToSet = new List<object>() { request.InspectionId };
						}
					}
				}

				var reportRequestId = await _sharedClient.InitiateReportRequest(reportRequest);
				if (reportRequestId != null && reportRequestId != string.Empty)
				{
					return reportRequestId;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Prints the detail report.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		private async Task<string> PrintDetailReport(InspectionVIRReportRequest request)
		{
			_client.AccessToken = GetAccessToken();
			return await _client.InitiateVesselInspectionReportCall(request);
		}

		/// <summary>
		/// Prints the summary report.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		private async Task<string> PrintSummaryReport(InspectionVIRReportRequest request)
		{
			_client.AccessToken = GetAccessToken();
			return await _client.InitiateVesselInspectionSummaryReportCall(request);
		}

		#region Common Control

		/// <summary>
		/// Gets the company list paged.
		/// </summary>
		/// <param name="inputText">The input text.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetCompanyListPaged(string inputText, int page)
		{
			_sharedClient.AccessToken = GetAccessToken();

			DataTablePageResponse<List<CompanySearchResponseViewModel>> response = new DataTablePageResponse<List<CompanySearchResponseViewModel>>();

			CompanySearchRequest request = new CompanySearchRequest();
			request.FetchDefaultCurrency = false;
			request.FetchDefaultCredit = false;
			request.CountryId = null;
			request.IsAgent = false;

			request.FetchAPValid = false;
			request.FetchOnlyActivatedAccountingCompanies = false;
			request.IsFullTextSearch = false;
			request.FetchOnlyAPEnable = false;
			request.CompanyName = inputText;
			var _searchCompanyTypes = new List<string>();
			var CompanyTypes = new List<CompanyTypeEnum>
					{
						CompanyTypeEnum.Broker,
						CompanyTypeEnum.ClassificationSociety,
						CompanyTypeEnum.CrewManagementOffices,
						CompanyTypeEnum.FreightSupplier,
						CompanyTypeEnum.HMLeadUnderwriter,
						CompanyTypeEnum.ManagementOffice,
						CompanyTypeEnum.PANDIClub,
						CompanyTypeEnum.PurchaseSupplier,
						CompanyTypeEnum.ShipManagementOffices,
						CompanyTypeEnum.Supplier,
						CompanyTypeEnum.Warehouse,
						CompanyTypeEnum.YARDS,
						CompanyTypeEnum.BeneficialOwner,
						CompanyTypeEnum.RegisteredOwner,
						CompanyTypeEnum.ShipOwner,
						CompanyTypeEnum.OilMajorVettingCompany,
						CompanyTypeEnum.ThirdPartyAgent,
						CompanyTypeEnum.ThirdPartyInspector
					};
			if (CompanyTypes != null && CompanyTypes.Any())
			{
				_searchCompanyTypes.AddRange(CompanyTypes.Select(x => EnumsHelper.GetKeyValue(x)));
			}

			request.CompanyTypeIds = _searchCompanyTypes;

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
				response = await _sharedClient.PostGetSupplierDetails(pageRequest, request);
			}

			return new JsonResult(new { data = response.Data });
		}

		/// <summary>
		/// Gets the selected company.
		/// </summary>
		/// <param name="inputText">The input text.</param>
		/// <param name="companyId">The company identifier.</param>
		/// <returns></returns>
		public async Task<ActionResult> GetSelectedCompany(string inputText, string companyId)
		{
			_sharedClient.AccessToken = GetAccessToken();
			List<CompanySearchResponseViewModel> response = null;
			CompanySearchRequest request = new CompanySearchRequest();
			request.FetchDefaultCurrency = false;
			request.FetchDefaultCredit = false;
			request.CountryId = null;
			request.IsAgent = false;

			request.FetchAPValid = false;
			request.FetchOnlyActivatedAccountingCompanies = false;
			request.IsFullTextSearch = false;
			request.FetchOnlyAPEnable = false;
			request.CompanyName = inputText;
			var _searchCompanyTypes = new List<string>();
			var CompanyTypes = new List<CompanyTypeEnum>
					{
						CompanyTypeEnum.Broker,
						CompanyTypeEnum.ClassificationSociety,
						CompanyTypeEnum.CrewManagementOffices,
						CompanyTypeEnum.FreightSupplier,
						CompanyTypeEnum.HMLeadUnderwriter,
						CompanyTypeEnum.ManagementOffice,
						CompanyTypeEnum.PANDIClub,
						CompanyTypeEnum.PurchaseSupplier,
						CompanyTypeEnum.ShipManagementOffices,
						CompanyTypeEnum.Supplier,
						CompanyTypeEnum.Warehouse,
						CompanyTypeEnum.YARDS,
						CompanyTypeEnum.BeneficialOwner,
						CompanyTypeEnum.RegisteredOwner,
						CompanyTypeEnum.ShipOwner,
						CompanyTypeEnum.OilMajorVettingCompany,
						CompanyTypeEnum.ThirdPartyAgent,
						CompanyTypeEnum.ThirdPartyInspector
					};
			if (CompanyTypes != null && CompanyTypes.Any())
			{
				_searchCompanyTypes.AddRange(CompanyTypes.Select(x => EnumsHelper.GetKeyValue(x)));
			}

			request.CompanyTypeIds = _searchCompanyTypes;

			if (!string.IsNullOrWhiteSpace(inputText))
			{
				response = await _sharedClient.GetCompanyList(request);
			}

			var SelectedCompany = response.Where(x => x.CompanyId == companyId).FirstOrDefault();

			return new JsonResult(SelectedCompany);
		}

		#endregion

		#region Marine WCF Client

		#region Inspction closure Action

		/// <summary>
		/// Gets the inspection details on finding.
		/// </summary>
		/// <param name="encryptedInspectionId">The encrypted inspection identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetInspectionDetailsOnFinding(string encryptedInspectionId)
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			InspectionDetailsViewModel inspection = null;
			string inspectionId = null;
			if (!string.IsNullOrWhiteSpace(encryptedInspectionId))
			{
				inspectionId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.InspectionIdEncryptionText, encryptedInspectionId);
				inspection = await _marineWCFClient.GetInspectionDetailsByInspection(inspectionId);
			}
			return new JsonResult(inspection);
		}

		/// <summary>
		/// Gets the inspection details by inspection identifier.
		/// </summary>
		/// <param name="inputRequest">The input request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetInspectionDetailsByInspectionId(InspectionClosureActionCheckViewModel inputRequest)
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			InspectionDetailsViewModel inspection = null;
			string inspectionId = null;
			if (inputRequest != null && !string.IsNullOrWhiteSpace(inputRequest.EncryptedInspectionId))
			{
				inspectionId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.InspectionIdEncryptionText, inputRequest.EncryptedInspectionId);
				inspection = await _marineWCFClient.GetInspectionByInspectionId(inspectionId);
			}
			if (inspection != null)
			{
				if (inspection.IsInspectionClomplete)
				{
					InspectionQuestionAnswerDetailRequestViewModel requestVM = new InspectionQuestionAnswerDetailRequestViewModel();
					requestVM.InspectionTypeId = inspection.InspectionTypeId;
					requestVM.InspectionId = inspectionId;
					string decryptedVesselId = CommonUtil.GetDecryptedVessel(_provider, inputRequest.EncryptedVesselId);
					string vesselId = decryptedVesselId.Split(Constants.Separator)[0];
					requestVM.VesselId = vesselId;
					inspection.IsAllQuestionAndAnsValid = await PostGetinspectionQuestionAnswerDetails(requestVM);
				}
			}
			return new JsonResult(inspection);
		}

		/// <summary>
		/// Gets the inspection closure succes URL.
		/// </summary>
		/// <param name="pageKey">The page key.</param>
		/// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetInspectionClosureSuccesUrl(string pageKey, string encryptedVesselId)
		{
			string sourceUrl = GetSourceURLString(pageKey);

			//inspection
			if (CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey)) != null)
			{
				InspectionRequestViewModel inspection = new InspectionRequestViewModel();
				var SessionData = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey));
				string data = _provider.CreateProtector("Inspection").Unprotect(SessionData);
				inspection = JsonConvert.DeserializeObject<InspectionRequestViewModel>(data);

				_client.AccessToken = GetAccessToken();
				inspection = await InspectionListDetails(GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey)), encryptedVesselId);

				string encryptedInspectionURL = CommonUtil.GetEncryptedURL(_provider, Constants.InspectionEncryptionKey, inspection);
				SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.InspectionListPageKey), encryptedInspectionURL, encryptedVesselId);
			}

			return new JsonResult(sourceUrl);
		}

		/// <summary>
		/// Closes the inspection.
		/// </summary>
		/// <param name="encryptedInspectionId">The encrypted inspection identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> CloseInspection(string encryptedInspectionId)
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			InspectionClosureSuccessViewModel operationResponse = null;
			if (!string.IsNullOrWhiteSpace(encryptedInspectionId))
			{
				string inspectionId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.InspectionIdEncryptionText, encryptedInspectionId);
				operationResponse = await _marineWCFClient.PostSaveInspection(inspectionId);
			}
			return new JsonResult(operationResponse);
		}

		/// <summary>
		/// Posts the getinspection question answer details.
		/// </summary>
		/// <param name="requestVM">The request vm.</param>
		/// <returns></returns>
		[NonAction]
		private async Task<bool> PostGetinspectionQuestionAnswerDetails(InspectionQuestionAnswerDetailRequestViewModel requestVM)
		{
			//Used in purpose for validation in inpsection closure
			_marineWCFClient.AccessToken = GetAccessToken();
			List<MarineQuestionAnswerDetailResponseViewModel> response = null;
			response = await _marineWCFClient.PostGetinspectionQuestionAnswerDetails(requestVM);

			bool isValid = true;
			if (response != null && response.Any())
			{
				isValid = response.All(x => x.IsValid);
			}
			return isValid;
		}

		#endregion

		/// <summary>
		/// Vessels the header details.
		/// </summary>
		/// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> VesselHeaderDetails(string encryptedVesselId)
		{
			_client.AccessToken = GetAccessToken();
			VesselPreviewViewModel previewViewModel = await _client.PostGetVesselHeaderDetail(encryptedVesselId);
			return new JsonResult(previewViewModel);
		}

		#endregion

		#region Vessel Visit Report

		/// <summary>
		/// VVRs the list.
		/// </summary>
		/// <returns></returns>
		public IActionResult VVRList()
		{
			return View("VVR/VVRList");
		}

		public IActionResult VVRFindings()
		{
			return View("VVR/VVRFindings");
		}

		/// <summary>
		/// Gets the type of the department details by inspector.
		/// </summary>
		/// <param name="EncryptedVesselId">The encrypted vessel identifier.</param>
		/// <param name="inspectorEntity">The inspector entity.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetDepartmentDetailsByInspectorType(string EncryptedVesselId, InspectorEntity inspectorEntity)
		{
			_ssMarineClient.AccessToken = GetAccessToken();
			List<Lookup> departments = new List<Lookup>();
			if (inspectorEntity.Equals(InspectorEntity.Office))
			{
				departments = await _ssMarineClient.GetActiveOfficeDepartment();
			}
			else if (inspectorEntity.Equals(InspectorEntity.ShipStaff))
			{
				departments = await _ssMarineClient.GetOnboardDepartmentList(EncryptedVesselId);
			}
			else if (inspectorEntity.Equals(InspectorEntity.ThirdParty))
			{

			}
			return new JsonResult(new { data = departments });
		}

		/// <summary>
		/// Binds the inspection visit details.
		/// </summary>
		/// <returns></returns>
		public async Task<JsonResult> BindInspectionVisitDetails()
		{
			_client.AccessToken = GetAccessToken();
			_sharedClient.AccessToken = GetAccessToken();
			InspectionVisitRequestViewModel inspectionVisitRequestViewModel = await InspectionVisitListDetails();
			UserViewModel userDetail = await _sharedClient.GetUserDetail();
			inspectionVisitRequestViewModel.UserForeName = userDetail.UserForeName;
			inspectionVisitRequestViewModel.UserSurName = userDetail.UserSurName;
			return new JsonResult(inspectionVisitRequestViewModel);
		}

		/// <summary>
		/// Gets the ports lookup.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="q">The q.</param>
		/// <param name="type">The type.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetPortsLookup(string term, string q, string type, int page)
		{
			_ssMarineClient.AccessToken = GetAccessToken();

			SearchPortRequest request = new SearchPortRequest();
			request.PortName = term;
			request.UNLocode = "";
			request.CountryId = "";
			request.pagedRequest = new PagedRequest { PageNumber = page, PageSize = 10 };

			PagedResponse<List<PortDetail>> response = await _ssMarineClient.SearchPortsPaged(request);
			Select2ResponseViewModel<List<PortDetail>> select2ResponseViewModel = new Select2ResponseViewModel<List<PortDetail>>();
			select2ResponseViewModel.Results = new List<PortDetail>();
			select2ResponseViewModel.Results = response.Result;
			select2ResponseViewModel.Pagination = new Pagination();

			return new JsonResult(select2ResponseViewModel);

		}

		/// <summary>
		/// Gets the company search lookup.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="q">The q.</param>
		/// <param name="type">The type.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetCompanySearchLookup(string term, string q, string type, int page)
		{
			_client.AccessToken = GetAccessToken();
			_ssMarineClient.AccessToken = GetAccessToken();
			CompanySearchViewModel request = new CompanySearchViewModel();
			request.CompanyName = term;
			request.CompanyTypeIds = new List<string>();
			request.ExcludedCompanyTypeIds = new List<string>();
			request.pageRequest = new PagedRequest { PageNumber = page, PageSize = 10 };

			PagedResponse<List<CompanySearchResponseViewModel>> response = await _ssMarineClient.SearchCompaniesPaged(request);
			Select2ResponseViewModel<List<CompanySearchResponseViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<CompanySearchResponseViewModel>>();
			select2ResponseViewModel.Results = new List<CompanySearchResponseViewModel>();
			select2ResponseViewModel.Results = response.Result;
			select2ResponseViewModel.Pagination = new Pagination();

			return new JsonResult(select2ResponseViewModel);

		}

		/// <summary>
		/// Saves the inspection visit details.
		/// </summary>
		/// <param name="inspection">The inspection.</param>
		/// <returns></returns>
		public async Task<JsonResult> SaveInspectionVisitDetails(SaveInspectionVisitViewModel saveinspectionvisitviewmodel)
		{
			_ssMarineClient.AccessToken = GetAccessToken();
			saveinspectionvisitviewmodel.MappedQuestions = new List<MarineQuestionAnswerDetailResponse>();
			saveinspectionvisitviewmodel.ScheduleDetails = new List<InspectionScheduleDetail>();
			saveinspectionvisitviewmodel.OfficeReviewerDetail = new List<VesselInspectionOfficeReviewerDetail>();
			UpdateResponse<Inspection> updateResponse = await _ssMarineClient.SaveInspection(saveinspectionvisitviewmodel);
			return new JsonResult(updateResponse);
		}

		/// <summary>
		/// Inspections the visit list details.
		/// </summary>
		/// <returns></returns>
		[NonAction]
		private async Task<InspectionVisitRequestViewModel> InspectionVisitListDetails()
		{
			_ssMarineClient.AccessToken = GetAccessToken();
			InspectionVisitRequestViewModel inspectionVisitRequestViewModel = new InspectionVisitRequestViewModel();
			var departmentList = await _ssMarineClient.GetActiveOfficeDepartment();
			if (departmentList != null)
			{
				inspectionVisitRequestViewModel.DepartmentList = departmentList;
			}

			var operatingList = await _ssMarineClient.GetOperatingTypes();
			if (operatingList != null)
			{
				inspectionVisitRequestViewModel.OperatingList = operatingList;
			}
			inspectionVisitRequestViewModel.ActivityTypesList = await _ssMarineClient.GetPosActivityTypeList(PosActivityTypeLookupCode.Port);
			return inspectionVisitRequestViewModel;
		}

		#endregion
	}
}