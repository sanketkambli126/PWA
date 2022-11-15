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
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Defect;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Defect;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.PlannedMaintenance;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class DefectController : AuthenticatedController
	{
        #region Constructor

        /// <summary>
        /// The client
        /// </summary>
        private readonly MarineClient _marineClient;

        /// <summary>
        /// The shared client
        /// </summary>
        private readonly SharedClient _sharedClient;

        /// <summary>
        /// The DocumentClient
        /// </summary>
        private DocumentClient _documentClient;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// The notification client
        /// </summary>
        private readonly NotificationClient _notificationClient;

        /// <summary>
        /// The marine WCF client
        /// </summary>
        private MarineWCFClient _marineWCFClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefectController" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="sharedClient">The shared client.</param>
        /// <param name="documentClient">The document client.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="notificationClient">The notification client.</param>
        /// <param name="marineWCFClient">The marine WCF client.</param>
        public DefectController(MarineClient client, SharedClient sharedClient, DocumentClient documentClient, IDataProtectionProvider provider, NotificationClient notificationClient, MarineWCFClient marineWCFClient)
		{
			_marineClient = client;
			_provider = provider;
			_sharedClient = sharedClient;
			_documentClient = documentClient;
			_notificationClient = notificationClient;
			_marineWCFClient = marineWCFClient;
		}

        #endregion

        #region List Methods

        /// <summary>
        /// Lists the specified defect request.
        /// </summary>
        /// <param name="DefectRequest">The defect request.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsViewMore">if set to <c>true</c> [is view more].</param>
        /// <returns></returns>
        public IActionResult List(string DefectRequest, string VesselId)
		{
			DefectListViewModel viewModel = new DefectListViewModel();

			DefectListViewModel defectModel = CommonUtil.GetDecryptedRequest<DefectListViewModel>(_provider, "DefectList", DefectRequest);
			defectModel = GetDefectFilterDetails(defectModel);
			string EncryptedRequest = CommonUtil.GetEncryptedURL(_provider, "DefectList", defectModel);
			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey);
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
			SetSessionDetail(pageKey, null, EncryptedRequest);
			RemoveSessionFilter(_provider, pageKey, null, decreptedString.Split(Constants.Separator)[0]);
			viewModel = SetDefectListViewModel(GetSessionFilter(pageKey));
			viewModel.VesselName = decreptedString.Split(Constants.Separator)[1];
			viewModel.EncryptedVesselId = VesselId;
			viewModel.ActiveMobileTabClass = SetTab(pageKey, viewModel.ActiveMobileTabClass, Constants.Tab2);
			return View(viewModel);
		}

        /// <summary>
        /// Sets the defect ListView model.
        /// </summary>
        /// <param name="defectListURL">The defect list URL.</param>
        /// <returns></returns>
        [NonAction]
		private DefectListViewModel SetDefectListViewModel(string defectListURL)
		{
			DefectListViewModel defectVM = new DefectListViewModel();
			string data = _provider.CreateProtector("DefectList").Unprotect(defectListURL);
			defectVM = Newtonsoft.Json.JsonConvert.DeserializeObject<DefectListViewModel>(data);
			return defectVM;
		}

        /// <summary>
        /// Maintains the filter parameters.
        /// </summary>
        /// <returns>
        /// PurchaseOrderRequestViewModel
        /// </returns>
        public IActionResult MaintainFilterParameters()
		{
			string DefectManagerFilter = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey));
			string vesselId = GetSessionVesselFilter(EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey));

			if (!string.IsNullOrWhiteSpace(DefectManagerFilter) && !string.IsNullOrWhiteSpace(vesselId))
			{
				string decryptedViewModel = _provider.CreateProtector("DefectList").Unprotect(DefectManagerFilter);
				DefectListViewModel viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DefectListViewModel>(decryptedViewModel);
				return new JsonResult(new { data = viewModel, isTempDataExist = true });
			}
			else
			{
				return new JsonResult(new { data = string.Empty, isTempDataExist = false });
			}
		}

        /// <summary>
        /// Sets the page parameter.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public IActionResult SetPageParameter(DefectListViewModel inputRequest)
		{
			DefectListViewModel viewModel = new DefectListViewModel();
			viewModel.IsSearchClicked = inputRequest.IsSearchClicked;
			viewModel.EncryptedVesselId = inputRequest.EncryptedVesselId;
			viewModel.ToDate = inputRequest.ToDate;
			viewModel.FromDate = inputRequest.FromDate;
			viewModel.SelectedPlannedFor = GetListOfString(inputRequest.SelectedPlannedFor);
			viewModel.SelectedStatus = GetListOfString(inputRequest.SelectedStatus);
			viewModel.SelectedSystemArea = GetListOfString(inputRequest.SelectedSystemArea);
			viewModel.DefectTitle = inputRequest.DefectTitle;
			viewModel.SelectedCriticalStatus = inputRequest.SelectedCriticalStatus;
			viewModel.SelectedDueStatus = inputRequest.SelectedDueStatus;
			viewModel.SelectedPlannedForIds = GetCommaSeparatedString(inputRequest.SelectedPlannedFor);
			viewModel.SelectedStatusIds = GetCommaSeparatedString(inputRequest.SelectedStatus);
			viewModel.SelectedSystemAreaIds = GetCommaSeparatedString(inputRequest.SelectedSystemArea);
			viewModel.StageName = inputRequest.StageName;
			viewModel.ActiveMobileTabClass = inputRequest.ActiveMobileTabClass;
			string defectURL = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(viewModel));
			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey), defectURL, inputRequest.EncryptedVesselId);

			return new JsonResult(new { data = viewModel });
		}

        /// <summary>
        /// Gets the list of string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [NonAction]
		private List<string> GetListOfString(List<string> input)
		{
			if (input != null && input.Any() && !string.IsNullOrWhiteSpace(input.FirstOrDefault()))
			{
				return input[0].Split(',').ToList();
			}
			else
			{
				return new List<string>();
			}

		}

        /// <summary>
        /// Returns the comma separated string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [NonAction]
		private string GetCommaSeparatedString(List<string> input)
		{
			return input != null && input.Any() ? string.Join(",", input) : "";
		}

        /// <summary>
        /// Sets the summary filter in temporary data.
        /// </summary>
        /// <param name="defectUrl">The defect URL.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult SetSummaryFilterInTempData(string defectUrl, string vesselId)
		{
			string data = _provider.CreateProtector("DefectList").Unprotect(defectUrl);
			DefectListViewModel viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DefectListViewModel>(data);
			viewModel = GetDefectFilterDetails(viewModel);
			viewModel.EncryptedVesselId = vesselId;

			string defectURL = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(viewModel));
			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey), defectURL, vesselId);
			return new JsonResult(new { data = viewModel });
		}

        /// <summary>
        /// Gets the order stage.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDefectCriticalList()
		{
			List<Lookup> CriticalList = new List<Lookup>();

			List<DefectCriticalStatus> OrderStageList = _marineClient.GetDefectCriticalList();

			foreach (DefectCriticalStatus stage in OrderStageList)
			{
				CriticalList.Add(new Lookup() { Identifier = EnumsHelper.GetKeyValue(stage), Description = EnumsHelper.GetDescription(stage) });
			}
			return new JsonResult(CriticalList);
		}

        /// <summary>
        /// Gets the defect due list.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDefectDueList()
		{
			List<Lookup> DueList = new List<Lookup>();

			List<DefectDueStatus> OrderStageList = _marineClient.GetDefectDueStatus();

			foreach (DefectDueStatus stage in OrderStageList)
			{
				DueList.Add(new Lookup() { Identifier = EnumsHelper.GetKeyValue(stage), Description = EnumsHelper.GetDescription(stage) });
			}
			return new JsonResult(DueList);
		}

        /// <summary>
        /// Gets the defect work basket list.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectWorkBasketList(DefectListViewModel input)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<DefectWorkBasketResponseViewModel> response = await _marineClient.GetDefectList(input);

			if (response != null && response.Any())
			{
				RecordDiscussionRequestViewModel recordRequest = new RecordDiscussionRequestViewModel();
				recordRequest.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.DefectWorkOrder));
				recordRequest.ReferenceIds = response.Select(x => x.DefectWorkOrderId).ToList();

				_notificationClient.AccessToken = GetAccessToken();
				List<RecordDiscussionResponse> recordResponse = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(recordRequest);

				IEnumerable<RecordDiscussionResponse> filteredRecordResponse = recordResponse.Where(x => x.ChannelCount > 0 || x.NotesCount > 0);

				foreach (var item in filteredRecordResponse)
				{
					DefectWorkBasketResponseViewModel defectObj = response.FirstOrDefault(x => x.DefectWorkOrderId == item.ReferenceIdentifier);
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
        /// Posts the get defect work order status list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PostGetDefectWorkOrderStatusList()
		{
			_marineClient.AccessToken = GetAccessToken();
			List<DefectWorkOrderAttributeViewModel> response = await _marineClient.PostGetDefectWorkOrderStatusList();
			return new JsonResult(new { data = response });
		}

        /// <summary>
        /// Posts the get defect work order planned for list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PostGetDefectWorkOrderPlannedForList()
		{
			_marineClient.AccessToken = GetAccessToken();
			List<DefectWorkOrderAttributeViewModel> response = await _marineClient.PostGetDefectWorkOrderPlannedForList();
			return new JsonResult(new { data = response });
		}

        /// <summary>
        /// Gets the system area.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetSystemArea()
		{
			_marineClient.AccessToken = GetAccessToken();
			List<Lookup> response = await _marineClient.GetSystemArea();
			return new JsonResult(new { data = response });
		}

        /// <summary>
        /// Gets the defect manager summary details.
        /// </summary>
        /// <param name="EncryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectManagerSummaryDetails(string EncryptedVesselId)
		{
			_marineClient.AccessToken = GetAccessToken();
			DefectSummaryResponseViewModel response = await _marineClient.GetDefectDashboarSummarydDetail(EncryptedVesselId);
			return new JsonResult(response);
		}

        /// <summary>
        /// Gets the system area tree list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetSystemAreaTreeList()
		{
			_marineClient.AccessToken = GetAccessToken();
			List<Lookup> response = await _marineClient.GetSystemArea();

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
        /// Gets the defect workorder planned for tree list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectWorkOrderPlannedForTreeList()
		{
			_marineClient.AccessToken = GetAccessToken();
			List<DefectWorkOrderAttributeViewModel> response = await _marineClient.PostGetDefectWorkOrderPlannedForList();

			List<TreeViewModel<DefectWorkOrderAttributeViewModel>> treeList = new List<TreeViewModel<DefectWorkOrderAttributeViewModel>>();
			List<TreeViewModel<DefectWorkOrderAttributeViewModel>> childItems = new List<TreeViewModel<DefectWorkOrderAttributeViewModel>>();

			TreeViewModel<DefectWorkOrderAttributeViewModel> AllOption = new TreeViewModel<DefectWorkOrderAttributeViewModel>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<DefectWorkOrderAttributeViewModel>>(),
			};

			if (response != null && response.Any())
			{
				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<DefectWorkOrderAttributeViewModel>
				{
					Key = y.DalId,
					Title = y.AttributeName,
					Tooltip = y.AttributeName,
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
        /// Gets the defect workorder status tree list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectWorkOrderStatusTreeList()
		{
			_marineClient.AccessToken = GetAccessToken();
			List<DefectWorkOrderAttributeViewModel> response = await _marineClient.PostGetDefectWorkOrderStatusList();

			List<TreeViewModel<DefectWorkOrderAttributeViewModel>> treeList = new List<TreeViewModel<DefectWorkOrderAttributeViewModel>>();
			List<TreeViewModel<DefectWorkOrderAttributeViewModel>> childItems = new List<TreeViewModel<DefectWorkOrderAttributeViewModel>>();

			TreeViewModel<DefectWorkOrderAttributeViewModel> AllOption = new TreeViewModel<DefectWorkOrderAttributeViewModel>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<DefectWorkOrderAttributeViewModel>>(),
			};

			if (response != null && response.Any())
			{
				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<DefectWorkOrderAttributeViewModel>
				{
					Key = y.DalId,
					Title = y.AttributeName,
					Tooltip = y.AttributeName,
					Expanded = false,
					Checkbox = true,
					Lazy = false,
					Children = null
				}));
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

        #region Attachments

        /// <summary>
        /// Gets the defect documents.
        /// </summary>
        /// <param name="DefectWorkOrderIds">The defect work order ids.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectDocuments(List<string> DefectWorkOrderIds)
		{
			_sharedClient.AccessToken = GetAccessToken();
			DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest();
			documentDetailRequest.DocumentSourceIds = DefectWorkOrderIds;
			documentDetailRequest.SubModules = new List<string>() { EnumsHelper.GetKeyValue(SubModule.DefectReportWorkOrder), EnumsHelper.GetKeyValue(SubModule.DefectWorkOrder) };

			string input = _provider.CreateProtector("DocumentURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(documentDetailRequest));
			List<DocumentDetail> response = await _sharedClient.PostGetDocumentDetails(input);
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
					viewModel.IsWebAddressEditable = item.DocumentType != null && item.DocumentType == Convert.ToInt32(EnumsHelper.GetKeyValue(DocumentType.WebAddress));
					viewModel.WebAddress = CommonUtil.GetExecutableWebAddress(item.WebAddress);
					viewModel.EttId = item.EttId;
					viewModel.CloudFileName = item.CloudFileName;
					if (item.SsmId == EnumsHelper.GetKeyValue(SubModule.DefectReportWorkOrder))
					{
						viewModel.DocumentCategory = DocumentCategory.DefectReportWorkOrder;
					}
					else if (item.SsmId == EnumsHelper.GetKeyValue(SubModule.DefectWorkOrder))
					{
						viewModel.DocumentCategory = DocumentCategory.DefectWorkOrder;
					}

					result.Add(viewModel);
				}
			}

			return new JsonResult(new { data = result });
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

			request.DocumentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(request.FileName)).FirstOrDefault();
			var result = await _documentClient.DownloadDocument(request);
			byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
			string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
			return new JsonResult(new { filename = request.FileName, bytes = byteString, fileType = EnumsHelper.GetDescription(request.DocumentFileType) });
		}

        /// <summary>
        /// Gets the defect documents details.
        /// </summary>
        /// <param name="DefectWorkOrderId">The defect work order identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectDocumentsDetails(string DefectWorkOrderId)
		{
			_sharedClient.AccessToken = GetAccessToken();
			string decryptedDWOId = _provider.CreateProtector("DefectDwoId").Unprotect(DefectWorkOrderId);
			DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest();
			documentDetailRequest.DocumentSourceIds = new List<string>() { decryptedDWOId };
			documentDetailRequest.SubModules = new List<string>() { EnumsHelper.GetKeyValue(SubModule.DefectReportWorkOrder), EnumsHelper.GetKeyValue(SubModule.DefectWorkOrder) };

			string input = _provider.CreateProtector("DocumentURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(documentDetailRequest));
			List<DocumentDetail> response = await _sharedClient.PostGetDocumentDetails(input);
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
					viewModel.IsWebAddressEditable = item.DocumentType != null && item.DocumentType == Convert.ToInt32(EnumsHelper.GetKeyValue(DocumentType.WebAddress));
					viewModel.EttId = item.EttId;
					viewModel.CloudFileName = item.CloudFileName;
					if (item.SsmId == EnumsHelper.GetKeyValue(SubModule.DefectReportWorkOrder))
					{
						viewModel.DocumentCategory = DocumentCategory.DefectReportWorkOrder;
					}
					else if (item.SsmId == EnumsHelper.GetKeyValue(SubModule.DefectWorkOrder))
					{
						viewModel.DocumentCategory = DocumentCategory.DefectWorkOrder;
					}
					viewModel.WebAddress = CommonUtil.GetExecutableWebAddress(item.WebAddress);

					result.Add(viewModel);
				}
			}

			return new JsonResult(new { data = result });
		}

        #endregion

        #region Report Executor

        /// <summary>
        /// Exports to excel defect list.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<JsonResult> ExportToExcelDefectList(DefectListViewModel input)
		{
			_sharedClient.AccessToken = GetAccessToken();
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input.EncryptedVesselId);
			string VesselId_decrept = decreptedString.Split(Constants.Separator)[0];
			string Status = string.Empty;
			string Type = string.Empty;
			string DefectSystemAreaIds = string.Empty;

			DefectWorkBasketRequest request = new DefectWorkBasketRequest();
			ReportLight reportRequest = await _sharedClient.GetReportLightByFilename(EnumsHelper.GetKeyValue(ReportMaster.MarineDefectManagerListingReport));

			if (reportRequest != null)
			{
				reportRequest.FriendlyFileName = Constants.VesselDefectListFileName;
				reportRequest.ReportFormat = ReportExportTypes.Excel;

				if (input.IsSearchClicked)
				{
					request.FromDate = input.FromDate;
					request.ToDate = input.ToDate;

					if (input.SelectedCriticalStatus == EnumsHelper.GetKeyValue(DefectCriticalStatus.OnlyCritical))
					{
						request.IsCritical = true;
					}
					else if (input.SelectedCriticalStatus == EnumsHelper.GetKeyValue(DefectCriticalStatus.All))
					{
						request.IsCritical = false;
					}

					if (input.SelectedDueStatus == EnumsHelper.GetKeyValue(DefectDueStatus.All))
					{
						request.IsDue = true;
						request.IsOverdue = true;
					}
					else if (input.SelectedDueStatus == EnumsHelper.GetKeyValue(DefectDueStatus.Due))
					{
						request.IsDue = true;
					}
					else if (input.SelectedDueStatus == EnumsHelper.GetKeyValue(DefectDueStatus.Overdue))
					{
						request.IsOverdue = true;
					}
					Status = input.SelectedStatus != null ? string.Join(",", input.SelectedStatus) : "";
					Type = input.SelectedPlannedFor != null ? string.Join(",", input.SelectedPlannedFor) : "";
					DefectSystemAreaIds = input.SelectedSystemArea != null ? string.Join(",", input.SelectedSystemArea) : "";
				}
				else
				{
					request.FromDate = null;
					request.ToDate = null;
					request.IsDue = true;
					request.IsOverdue = true;
					if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.ClosedDefect))
					{
						request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Close) };
						request.ToDate = DateTime.Now;
						request.FromDate = request.ToDate.Value.Date.AddMonths(-12);
					}
					else if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.OpenDefect))
					{
						request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
					}
					else if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Overdue))
					{
						request.IsDue = false;
						request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
					}
					else if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.OffHire))
					{
						request.IsOffHire = true;
						request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
					}
					else if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Layover))
					{
						request.Type = new List<string>() { "GLAS00000009" };
						request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
					}
					else if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Drydock))
					{
						request.Type = new List<string>() { "GLAS00000010" };
						request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
					}
					else if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.TechnicalDefect))
					{
						request.AddedInDamageForm = true;
						request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
					}
					else if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.GuaranteeClaim))
					{
						request.GuaranteeClaimRequired = true;
						request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
					}
					else if (input.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Completed))
					{
						input.SelectedStatus = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed) };
					}

					Status = request.Status != null ? string.Join(",", request.Status) : "";
					Type = request.Type != null ? string.Join(",", request.Type) : "";
				}

				foreach (var reportParameter in reportRequest.ReportParameters)
				{
					if (reportParameter.ParameterName.Contains("@VesselId"))
					{
						reportParameter.ValueToSet = new List<object>() { VesselId_decrept };
					}
					if (reportParameter.ParameterName.Contains("@TopSystemArea"))
					{
						reportParameter.ValueToSet = new List<object>() { string.Empty };
					}
					if (reportParameter.ParameterName.Contains("@FromDate"))
					{
						reportParameter.ValueToSet = new List<object>() { request.FromDate };
					}
					if (reportParameter.ParameterName.Contains("@ToDate"))
					{
						reportParameter.ValueToSet = new List<object>() { request.ToDate };
					}
					if (reportParameter.ParameterName.Contains("@PgrId"))
					{
						reportParameter.ValueToSet = new List<object>() { string.Empty };
					}
					if (reportParameter.ParameterName.Contains("@PtrId"))
					{
						reportParameter.ValueToSet = new List<object>() { string.Empty };
					}
					if (reportParameter.ParameterName.Contains("@ShowDue"))
					{
						reportParameter.ValueToSet = new List<object>() { request.IsDue };
					}
					if (reportParameter.ParameterName.Contains("@ShowOverdue"))
					{
						reportParameter.ValueToSet = new List<object>() { request.IsOverdue };
					}
					if (reportParameter.ParameterName.Contains("@IsCritical"))
					{
						reportParameter.ValueToSet = new List<object>() { request.IsCritical };
					}
					if (reportParameter.ParameterName.Contains("@GuaranteeClaimRequired"))
					{
						reportParameter.ValueToSet = new List<object>() { request.GuaranteeClaimRequired };
					}
					if (reportParameter.ParameterName.Contains("@AddInDamageForm"))
					{
						reportParameter.ValueToSet = new List<object>() { request.AddedInDamageForm };
					}
					if (reportParameter.ParameterName.Contains("@IsOffHire"))
					{
						reportParameter.ValueToSet = new List<object>() { request.IsOffHire };
					}
					if (reportParameter.ParameterName.Contains("@PriorityIds"))
					{
						reportParameter.ValueToSet = new List<object>() { string.Empty };
					}
					if (reportParameter.ParameterName.Contains("@StatusIds"))
					{
						reportParameter.ValueToSet = new List<object>() { Status };
					}
					if (reportParameter.ParameterName.Contains("@TypeIds"))
					{
						reportParameter.ValueToSet = new List<object>() { Type };
					}
					if (reportParameter.ParameterName.Contains("@CategoryIds"))
					{
						reportParameter.ValueToSet = new List<object>() { string.Empty };
					}
					if (reportParameter.ParameterName.Equals("@SystemAreaIds"))
					{
						reportParameter.ValueToSet = new List<object>() { DefectSystemAreaIds };
					}
					if (reportParameter.ParameterName.Contains("@PageNumber"))
					{
						reportParameter.ValueToSet = new List<object>() { 1 };
					}
					if (reportParameter.ParameterName.Contains("@PageSize"))
					{
						reportParameter.ValueToSet = new List<object>() { 999 };
					}
					if (reportParameter.ParameterName.Equals("@SystemArea"))
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

        #endregion

        #endregion

        #region Details Method

        /// <summary>
        /// Detailses this instance.
        /// </summary>
        /// <param name="DefectDetails">The defect details.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IActionResult Details(string DefectDetails, string VesselId, bool IsVesselChanged, string context)
		{
			if (IsVesselChanged)
			{
				DefectListViewModel defectListVM = new DefectListViewModel();
				defectListVM.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.OpenDefect);
				string defectListUrl = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(defectListVM));

				return RedirectToAction("List", new { DefectRequest = defectListUrl, VesselId = VesselId });
			}

			DefectDetailsViewModel defectViewModel = new DefectDetailsViewModel();

			if (!string.IsNullOrWhiteSpace(DefectDetails))
			{
				string data = _provider.CreateProtector("DefectDetails").Unprotect(DefectDetails);
				defectViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DefectDetailsViewModel>(data);
			}

			if (!string.IsNullOrWhiteSpace(context))
			{
				ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
				defectViewModel.DefectWorkOrderId = contextParameter.DwoId;
			}

			string decryptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
			string currentVesselId = !String.IsNullOrWhiteSpace(decryptedString) ? decryptedString.Split(Constants.Separator)[0] : string.Empty;
			string currentVesselName = !String.IsNullOrWhiteSpace(decryptedString) ? decryptedString.Split(Constants.Separator)[1] : string.Empty;

			defectViewModel.EncryptedVesselId = VesselId;
			defectViewModel.VesselName = currentVesselName;
			defectViewModel.EncryptedDWOId = _provider.CreateProtector("DefectDwoId").Protect(defectViewModel.DefectWorkOrderId);

			_marineClient.AccessToken = GetAccessToken();
			Task<DefectWorkOrderViewModel> taskResponse = _marineClient.GetDefectWorkOrderHeaderDetail(defectViewModel.DefectWorkOrderId);
			DefectWorkOrderViewModel response = taskResponse.Result ?? new DefectWorkOrderViewModel();

			defectViewModel.IsGuaranteeClaimCode = response.IsGuaranteeClaimCode;
			defectViewModel.IsCompletedOrClosed = response.IsCompletedOrClosed;
			defectViewModel.IsStatusCompleted = response.IsStatusCompleted;

			string[] contextParams = { defectViewModel.DefectWorkOrderId };
			string[] messageParams = { response.DefectNumber, response.DefectName };

			defectViewModel.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.DefectWorkOrder, currentVesselId, CommonUtil.GetVesselNameFromDisplayName(defectViewModel.VesselName), contextParams, messageParams, defectViewModel.DefectWorkOrderId);
			defectViewModel.IsFromViewRecord = IsFromViewRecordVal(context);
			SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.DefectDetailsPageKey), EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey), DefectDetails);
			return View(defectViewModel);
		}

        /// <summary>
        /// Gets the defect work order for edit.
        /// </summary>
        /// <param name="EncryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectWorkOrderForEdit(string EncryptedDWOId)
		{
			_marineClient.AccessToken = GetAccessToken();
			DefectWorkOrderViewModel response = await _marineClient.PostGetDefectWorkOrderForEdit(EncryptedDWOId);
			return new JsonResult(response);
		}

        /// <summary>
        /// Gets the defect report wo summary.
        /// </summary>
        /// <param name="EncryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectReportWOSummary(string EncryptedDWOId)
		{
			_marineClient.AccessToken = GetAccessToken();
			DefectReportWorkOrderSummaryViewModel response = await _marineClient.PostGetDefectReportWOSummary(EncryptedDWOId);
			return new JsonResult(response);
		}

        /// <summary>
        /// Gets the defect reschedule log.
        /// </summary>
        /// <param name="EncryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectRescheduleLog(string EncryptedDWOId)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<RescheduleDefectWorkOrderViewModel> response = await _marineClient.GetDefectWORescheduleLog(EncryptedDWOId);
			return new JsonResult(new { data = response });
		}

        /// <summary>
        /// Gets the requisition.
        /// </summary>
        /// <param name="EncryptedDWOId">The encrypted dwo identifier.</param>
        /// <param name="EncryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRequisition(string EncryptedDWOId, string EncryptedVesselId)
		{
			_marineClient.AccessToken = GetAccessToken();
			List<DefectRequisitionViewModel> response = await _marineClient.GetMappedRequisition(EncryptedDWOId, EncryptedVesselId);
			return new JsonResult(new { data = response });
		}

        /// <summary>
        /// Downloads the defect detail report.
        /// </summary>
        /// <param name="dwoId">The dwo identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns>
        /// message
        /// </returns>
        public async Task<JsonResult> DownloadDefectDetailReport(string dwoId, string vesselId)
		{
			_sharedClient.AccessToken = GetAccessToken();
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
			string VesselId_decrept = decreptedString.Split(Constants.Separator)[0];

			string dwo = _provider.CreateProtector("DefectDwoId").Unprotect(dwoId);

			ReportLight reportRequest = await _sharedClient.GetReportLightByFilename(EnumsHelper.GetKeyValue(ReportMaster.MarineDefectManagerDetailsReport));

			if (reportRequest != null)
			{
				reportRequest.ReportFormat = ReportExportTypes.PDF;

				foreach (var reportParameter in reportRequest.ReportParameters)
				{
					if (reportParameter.ParameterName.Equals("@sVES_ID"))
					{
						reportParameter.ValueToSet = new List<object>() { VesselId_decrept };
					}
					if (reportParameter.ParameterName.Equals("@Dwo_Id"))
					{
						reportParameter.ValueToSet = new List<object>() { dwo };
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
        /// Closes the selected defect.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<JsonResult> CloseSelectedDefect(DefectClosureRequestViewModel inputRequest)
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			inputRequest.DecryptedDWOId = _provider.CreateProtector("DefectDwoId").Unprotect(inputRequest.EncryptedDWOId);			
			bool response = await _marineWCFClient.CloseDefectAction(inputRequest, true);
			return new JsonResult(response);
		}


        #region Defect Closure Action

        /// <summary>
        /// Gets the drop down details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		public async Task<IActionResult> GetDropDownDetails()
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			List<DefectWorkOrderAttributeViewModel> ImpactList = null;
			List<DefectWorkOrderAttributeViewModel> OffHirePeriodList = null;
			List<DefectWorkOrderAttributeViewModel> filters = await _marineWCFClient.PostGetDefectWorkOrderAttribute();
			if (filters != null && filters.Any())
			{
				if (filters.Any(x => !string.IsNullOrWhiteSpace(x.LookupCode) && x.LookupCode.Equals(EnumsHelper.GetDescription(DefectAttribute.DefectImpact).ToString())))
				{
					ImpactList = filters.Where(x => x.LookupCode.Equals(EnumsHelper.GetDescription(DefectAttribute.DefectImpact).ToString())).ToList();
				}

				if (filters.Any(x => !string.IsNullOrWhiteSpace(x.LookupCode) && x.LookupCode.Equals(EnumsHelper.GetDescription(DefectAttribute.OffHirePeriod).ToString())))
				{
					OffHirePeriodList = filters.Where(x => x.LookupCode.Equals(EnumsHelper.GetDescription(DefectAttribute.OffHirePeriod).ToString())).ToList();
				}
			}

			return new JsonResult(new { impactList = ImpactList, offHirePeriodList = OffHirePeriodList });
		}

        /// <summary>
        /// Posts the get component hierarchy.
        /// </summary>
        /// <param name="requestVM">The request vm.</param>
        /// <returns></returns>
        public async Task<IActionResult> PostGetComponentHierarchy(DefectComponentHeirarchyRequestViewModel requestVM)
		{
			_marineWCFClient.AccessToken = GetAccessToken();

			string SystemAreaPath = string.Empty;
			if (requestVM != null)
			{
				if (!string.IsNullOrWhiteSpace(requestVM.VesselId))
				{
					string decryptedVesslId = CommonUtil.GetDecryptedVessel(_provider, requestVM.VesselId);
					requestVM.VesselId = !string.IsNullOrWhiteSpace(decryptedVesslId) ? decryptedVesslId.Split(Constants.Separator)[0] : string.Empty;
				}
				SystemAreaPath = await _marineWCFClient.PostGetComponentHierarchy(requestVM);
			}

			return new JsonResult(SystemAreaPath);
		}

        /// <summary>
        /// Gets the defect report wo for edit.
        /// </summary>
        /// <param name="encryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectReportWOForEdit(string encryptedDWOId)
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			string defectWorkOrderId = _provider.CreateProtector("DefectDwoId").Unprotect(encryptedDWOId);
			DefectReportWorkOrderViewModel defectDetails = await _marineWCFClient.GetDefectReportWOForEdit(defectWorkOrderId);
			return new JsonResult(defectDetails);
		}

        /// <summary>
        /// Posts the get reported defect wo for preview.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <param name="encryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> PostGetReportedDefectWOForPreview(string encryptedVesselId, string encryptedDWOId)
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			string defectWorkOrderId = _provider.CreateProtector("DefectDwoId").Unprotect(encryptedDWOId);
			string decryptedVesseld = CommonUtil.GetDecryptedVessel(_provider, encryptedVesselId);
			string vesselId = !string.IsNullOrWhiteSpace(decryptedVesseld) ? decryptedVesseld.Split(Constants.Separator)[0] : string.Empty;
			PreviewReportedDefectWorkOrderViewModel defectWO = await _marineWCFClient.PostGetReportedDefectWOForPreview(vesselId, defectWorkOrderId);
			return new JsonResult(defectWO);
		}

        /// <summary>
        /// Gets the wo detail for report defect.
        /// </summary>
        /// <param name="encryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetWODetailForReportDefect(string encryptedDWOId)
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			string defectWorkOrderId = _provider.CreateProtector("DefectDwoId").Unprotect(encryptedDWOId);
			DefectDetailViewModel defectWO = await _marineWCFClient.GetWODetailForReportDefect(defectWorkOrderId);
			return new JsonResult(defectWO);
		}

        /// <summary>
        /// Posts the get position attribute lookup.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PostGetPosAttributeLookup()
		{
			_marineWCFClient.AccessToken = GetAccessToken();
			List<Lookup> OffHireTypeList = await _marineWCFClient.PostGetPosAttributeLookup();
			return new JsonResult(OffHireTypeList);
		}

        #endregion

        #endregion


        /// <summary>
        /// Gets the defect filter details.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        [NonAction]
		private DefectListViewModel GetDefectFilterDetails(DefectListViewModel filters)
		{
			filters.FromDate = null;
			filters.ToDate = null;

			if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.ClosedDefect))
			{
				filters.SelectedStatus = new List<string>() { "GLAS00000031" };
				filters.ToDate = DateTime.Now;
				filters.FromDate = filters.ToDate.Value.Date.AddMonths(-12);
			}
			else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.OpenDefect))
			{
				filters.SelectedStatus = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
			}
			else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Overdue))
			{
				filters.SelectedDueStatus = EnumsHelper.GetKeyValue(DefectDueStatus.Overdue);
				filters.SelectedStatus = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
			}
			else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Layover))
			{
				filters.SelectedPlannedFor = new List<string>() { "GLAS00000009" };
				filters.SelectedStatus = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
			}
			else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Drydock))
			{
				filters.SelectedPlannedFor = new List<string>() { "GLAS00000010" };
				filters.SelectedStatus = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
			}
			else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Completed))
			{
				filters.SelectedStatus = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed) };
			}

			if (filters.SelectedPlannedFor != null && filters.SelectedPlannedFor.Any())
			{
				filters.SelectedPlannedForIds = string.Join(',', filters.SelectedPlannedFor);
			}

			if (filters.SelectedStatus != null && filters.SelectedStatus.Any())
			{
				filters.SelectedStatusIds = string.Join(',', filters.SelectedStatus);
			}

			return filters;
		}

        /// <summary>
        /// Gets the defect approval succes URL.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns></returns>
        public JsonResult GetDefectApprovalSuccesUrl(string pageKey)
		{
			string sourceUrl = GetSourceURLString(pageKey);

			//PMS
			if (CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey)) != null)
			{
				PlannedMaintenanceListViewModel pmsVM = new PlannedMaintenanceListViewModel();
				var SessionData = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey));
				string data = _provider.CreateProtector("PMSList").Unprotect(SessionData);
				pmsVM = JsonConvert.DeserializeObject<PlannedMaintenanceListViewModel>(data);
				pmsVM.GridSubTitle = Constants.PMSCompleted;
				pmsVM.StageName = EnumsHelper.GetDescription(PMSDashboardStage.Completed);
				pmsVM.OtherFilters = null;
				pmsVM.SelectedOtherFilters = null;
				pmsVM.SelectedWBStatusIds = EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder);
				pmsVM.StatusIds = new List<string> { EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder) };
				string EncryptPlannedMaintenance = CommonUtil.GetEncryptedURL(_provider, Constants.PMSList, pmsVM);
				SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.PlannedMaintenanceListPageKey), EncryptPlannedMaintenance, pmsVM.EncryptedVesselId);
			}
			else if (CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey)) != null)
			{
				DefectListViewModel defectVM = SetDefectListViewModel(GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey)));
				defectVM.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.Completed);
				defectVM.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.Completed);
				defectVM.SelectedStatus = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed) };
				defectVM.SelectedStatusIds = EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed);
				string encryptedRequest = CommonUtil.GetEncryptedURL(_provider, "DefectList", defectVM);
				SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.DefectListPageKey), encryptedRequest, defectVM.EncryptedVesselId);
			}

			return new JsonResult(sourceUrl);
		}
	}
}