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
using PWAFeaturesRnd.Models.Report.Crew;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Crew;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
	/// <summary>
	/// Crew Controller
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
	public class CrewController : AuthenticatedController
	{
		#region Private Properties

		/// <summary>
		/// The crew client
		/// </summary>
		private readonly CrewClient _crewClient;

		/// <summary>
		/// The shared client
		/// </summary>
		private readonly SharedClient _sharedClient;

		/// <summary>
		/// The provider
		/// </summary>
		private IDataProtectionProvider _provider;
		/// <summary>
		/// The document client
		/// </summary>
		private DocumentClient _documentClient;

        /// <summary>
        /// The notification client
        /// </summary>
        private NotificationClient _notificationClient;

		#endregion

		#region Constructor


		/// <summary>
		/// Initializes a new instance of the <see cref="CrewController" /> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="sharedClient">The shared client.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="documentClient">The document client.</param>
		/// <param name="sharedClient">The shared client.</param>
		public CrewController(CrewClient client, IDataProtectionProvider provider, DocumentClient documentClient, SharedClient sharedClient, NotificationClient notificationClient)
		{
			_crewClient = client;
			_sharedClient = sharedClient;
			_provider = provider;
			_documentClient = documentClient;
			_sharedClient = sharedClient;
			_notificationClient = notificationClient;
			AccessibleModules = new List<string> { EnumsHelper.GetKeyValue(Modules.Crewing) };
		}

		#endregion

		#region Crew List Methods

		/// <summary>
		/// Lists the specified crew list.
		/// </summary>
		/// <param name="CrewList">The crew list.</param>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <returns></returns>
		public IActionResult List(string CrewList, string VesselId)
		{
			CrewListViewModel crewVm = new CrewListViewModel();

			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
			string LatestVesselName = decreptedString.Split(Constants.Separator)[1];

			SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey), null, CrewList);
			RemoveSessionFilter(_provider, EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey), null, decreptedString.Split(Constants.Separator)[0]);
			string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey);
			crewVm = SetDefaultValue(GetSessionFilter(pageKey));
			crewVm.EncryptedVesselId = VesselId;
			crewVm.ActiveMobileTabClass = SetTab(pageKey, crewVm.ActiveMobileTabClass, Constants.Tab2);
			crewVm.VesselName = decreptedString.Split(Constants.Separator)[1];

			return View(crewVm);
		}

		/// <summary>
		/// Sets the default value.
		/// </summary>
		/// <param name="crewUrl">The crew URL.</param>
		/// <returns></returns>
		private CrewListViewModel SetDefaultValue(string crewUrl)
		{
			CrewListViewModel crewVm = new CrewListViewModel();
			if (!string.IsNullOrWhiteSpace(crewUrl))
			{
				string data = _provider.CreateProtector("CrewList").Unprotect(crewUrl);
				crewVm = Newtonsoft.Json.JsonConvert.DeserializeObject<CrewListViewModel>(data);
			}
			return crewVm;
		}

		/// <summary>
		/// Gets the crew list.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetCrewList(CrewListViewModel input)
		{
			_crewClient.AccessToken = GetAccessToken();
			List<CrewListDetailViewModel> response = await _crewClient.GetCrewList(input);

			if (response != null && response.Any())
			{
				RecordDiscussionRequestViewModel recordRequest = new RecordDiscussionRequestViewModel();
				recordRequest.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.Crew));
				recordRequest.ReferenceIds = response.Where(x=> !string.IsNullOrWhiteSpace(x.CrewId)).Select(x => x.CrewId).ToList();

				_notificationClient.AccessToken = GetAccessToken();
				List<RecordDiscussionResponse> recordResponse = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(recordRequest);

				IEnumerable<RecordDiscussionResponse> filteredRecordResponse = recordResponse.Where(x => x.ChannelCount > 0 || x.NotesCount > 0);

				foreach (var item in filteredRecordResponse)
				{
					CrewListDetailViewModel defectObj = response.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.CrewId) && x.CrewId == item.ReferenceIdentifier);
					if (defectObj != null)
					{
						NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
						{
							CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.Crew)),
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
		/// <param name="crew">The crew.</param>
		/// <returns></returns>
		public IActionResult SetPageParameter(CrewListViewModel crew)
		{
			CrewListViewModel request = new CrewListViewModel();
			request.EncryptedVesselId = crew.EncryptedVesselId;
			request.ToDate = crew.ToDate;
			request.FromDate = crew.FromDate;
			request.SelectedFilter = crew.SelectedFilter;
			request.SelectedStageFilter = crew.SelectedStageFilter;
			request.SelectedStageName = EnumsHelper.GetEnumNameFromKeyValue(typeof(CrewStageFilter), crew.SelectedStageFilter);
			request.VesselName = CommonUtil.GetVesselDisplayName(_provider,crew.EncryptedVesselId);

			//multi select
			request.SelectedRankIds = crew.SelectedRankIds;
			request.SelectedRankDescriptions = crew.SelectedRankDescriptions;
			request.SelectedDepartmentIds = crew.SelectedDepartmentIds;
			request.SelectedDepartmentDescriptions = crew.SelectedDepartmentDescriptions;
			request.IsSearchClicked = crew.IsSearchClicked;
			 request.ActiveMobileTabClass = crew.ActiveMobileTabClass;
			string crewURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));

			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey), crewURL, crew.EncryptedVesselId);

			//TempData[Constants.CrewListFilter] = crewURL;
			//TempData[Constants.VesselIdFilter] = crew.EncryptedVesselId;

			return new JsonResult(new { data = request });
		}

		/// <summary>
		/// Maintains the filter.
		/// </summary>
		/// <returns>CrewListViewModel</returns>
		public IActionResult MaintainFilter()
		{
			string crewUrl = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey));
			string vesselId = GetSessionVesselFilter(EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey));

			if (!string.IsNullOrWhiteSpace(crewUrl) && !string.IsNullOrWhiteSpace(vesselId))
			{
				CrewListViewModel crewVm = new CrewListViewModel();
				string data = _provider.CreateProtector("CrewList").Unprotect(crewUrl);
				crewVm = Newtonsoft.Json.JsonConvert.DeserializeObject<CrewListViewModel>(data);
				crewVm.EncryptedVesselId = vesselId;
				string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
				crewVm.VesselName = decreptedString.Split(Constants.Separator)[1];
				return new JsonResult(new { data = crewVm, isTempDataExist = true });
			}
			else
			{
				return new JsonResult(new { data = string.Empty, isTempDataExist = false });
			}
		}

		/// <summary>
		/// Sets the summary filters.
		/// </summary>
		/// <param name="crewUrl">The crew URL.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <returns>CrewListViewModel</returns>
		public IActionResult SetSummaryFilters(string crewUrl, string vesselId)
		{
			CrewListViewModel crewVm = new CrewListViewModel();
			string data = _provider.CreateProtector("CrewList").Unprotect(crewUrl);
			crewVm = Newtonsoft.Json.JsonConvert.DeserializeObject<CrewListViewModel>(data);
			crewVm.EncryptedVesselId = vesselId;

			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
			crewVm.VesselName = decreptedString.Split(Constants.Separator)[1];

			string newCrewUrl = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(crewVm));
			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey), newCrewUrl, vesselId);
			return new JsonResult(new { data = crewVm });
		}

		/// <summary>
		/// Loads the crew list.
		/// </summary>
		/// <param name="crew">The crew.</param>
		/// <returns></returns>
		public IActionResult LoadCrewList(CrewListViewModel crew)
		{
			CrewListViewModel request = new CrewListViewModel();
			request.EncryptedVesselId = crew.EncryptedVesselId;
			request.ToDate = DateTime.Now;
			request.FromDate = DateTime.Now;
			request.SelectedFilter = CrewStageFilter.Onboard;
			request.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.Onboard);
			request.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
			string crewURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
			return Json(Url.Action("List", new { CrewList = crewURL, VesselId = crew.EncryptedVesselId }));
		}

		/// <summary>
		/// Gets the rank category.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="q">The q.</param>
		/// <param name="_type">The type.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetRankCategory(string term, string q, string _type, int page)
		{
			_crewClient.AccessToken = GetAccessToken();
			List<RankCategoryViewModel> response = await _crewClient.GetRankCategory();
			//after fetchinig list filter here
			List<RankCategoryViewModel> filteredList = new List<RankCategoryViewModel>();
			if (!string.IsNullOrWhiteSpace(q))
			{
				filteredList = response.Where(x => x.RankName.ToLower().Contains(q.ToLower())).ToList();
			}
			else
			{
				filteredList = response;
			}

			Select2ResponseViewModel<List<RankCategoryViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<RankCategoryViewModel>>();
			select2ResponseViewModel.Results = new List<RankCategoryViewModel>();
			select2ResponseViewModel.Results = filteredList;
			select2ResponseViewModel.Pagination = new Pagination();

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Gets the department list.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="q">The q.</param>
		/// <param name="_type">The type.</param>
		/// <param name="page">The page.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetDepartmentList(string term, string q, string _type, int page)
		{
			_sharedClient.AccessToken = GetAccessToken();
			List<DepartmentViewModel> response = await _sharedClient.PostGetDepartmentList();
			//after fetchinig list filter here
			List<DepartmentViewModel> filteredList = new List<DepartmentViewModel>();
			if (!string.IsNullOrWhiteSpace(q))
			{
				filteredList = response.Where(x => x.DepartmentName.ToLower().Contains(q.ToLower())).ToList();
			}
			else
			{
				filteredList = response;
			}

			Select2ResponseViewModel<List<DepartmentViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<DepartmentViewModel>>();
			select2ResponseViewModel.Results = new List<DepartmentViewModel>();
			select2ResponseViewModel.Results = filteredList;
			select2ResponseViewModel.Pagination = new Pagination();

			return new JsonResult(select2ResponseViewModel);
		}

		/// <summary>
		/// Gets the crew status.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetCrewStatus()
		{
			List<LookUp> response = new List<LookUp>();

			List<CrewStageFilter> crewStatusList = _crewClient.GetCrewStatus();

			foreach (CrewStageFilter stage in crewStatusList)
			{
				response.Add(new LookUp() { Identifier = EnumsHelper.GetKeyValue(stage), Description = EnumsHelper.GetDescription(stage) });
			}
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the crew summary.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetCrewSummary(string input)
		{
			_crewClient.AccessToken = GetAccessToken();
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input);
			CrewSummaryRequest summaryRequest = new CrewSummaryRequest();
			summaryRequest.VesselId = decreptedString.Split(Constants.Separator)[0];
			summaryRequest.FromDate = DateTime.Now.Date.AddMonths(-1);
			summaryRequest.ToDate = DateTime.Now.Date;
			summaryRequest.PastNumberOfDays = 30;
			CrewSummaryResponseViewModel response = await _crewClient.PostGetCrewSummary(summaryRequest);
			return new JsonResult(response);
		}

		/// <summary>
		/// Get Department List
		/// </summary>
		/// <returns>List<TreeViewModel<DepartmentViewModel>></returns>
		public async Task<IActionResult> GetDepartmentTreeList()
		{
			List<TreeViewModel<DepartmentViewModel>> treeList = new List<TreeViewModel<DepartmentViewModel>>();
			List<TreeViewModel<DepartmentViewModel>> childItems = new List<TreeViewModel<DepartmentViewModel>>();

			_sharedClient.AccessToken = GetAccessToken();

			List<DepartmentViewModel> response = await _sharedClient.PostGetDepartmentList();

			TreeViewModel<DepartmentViewModel> AllOption = new TreeViewModel<DepartmentViewModel>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<DepartmentViewModel>>(),
			};

			if (response != null && response.Any())
			{
				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<DepartmentViewModel>
				{
					Key = y.Id,
					Title = y.Text,
					Tooltip = y.Text,
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
		/// Get Rank List
		/// </summary>
		/// <returns>List<TreeViewModel<RankCategoryViewModel>></returns>
		public async Task<IActionResult> GetRankCategoryTreeList()
		{
			List<TreeViewModel<RankCategoryViewModel>> treeList = new List<TreeViewModel<RankCategoryViewModel>>();

			_crewClient.AccessToken = GetAccessToken();
			List<RankCategoryViewModel> response = await _crewClient.GetRankCategory();

			TreeViewModel<RankCategoryViewModel> AllOption = new TreeViewModel<RankCategoryViewModel>
			{
				Title = Constants.All,
				Expanded = true,
				Key = "",
				Checkbox = true,
				Lazy = false,
				Tooltip = Constants.All,
				Children = new List<TreeViewModel<RankCategoryViewModel>>(),
			};

			if (response != null && response.Any())
			{
				AllOption.Children.AddRange(response.Select(y => new TreeViewModel<RankCategoryViewModel>
				{
					Key = y.Id,
					Title = y.Text,
					Tooltip = y.Text,
					Expanded = false,
					Checkbox = true,
					Lazy = false,
					Children = null
				}));
			}
			treeList.Add(AllOption);

			return new JsonResult(treeList);
		}

		#endregion

		#region Medical Signoff

		/// <summary>
		/// Medicals the sign off list.
		/// </summary>
		/// <param name="CrewList">The crew list.</param>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <returns></returns>
		public IActionResult MedicalSignOffList(string CrewList, string VesselId)
		{
			//TempData[Constants.CrewListFilter] = null;
			//TempData[Constants.VesselIdFilter] = null;
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
			CrewListViewModel crewVm = new CrewListViewModel();


			string data = _provider.CreateProtector("CrewList").Unprotect(CrewList);
			crewVm = Newtonsoft.Json.JsonConvert.DeserializeObject<CrewListViewModel>(data);
			crewVm.EncryptedVesselId = VesselId;
			crewVm.VesselName = decreptedString.Split(Constants.Separator)[1];
			SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.MedicalSignOffListPageKey), EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey), CrewList);
			return View(crewVm);
		}

		/// <summary>
		/// Gets the medical sign off list.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetMedicalSignOffList(CrewListViewModel input)
		{
			_crewClient.AccessToken = GetAccessToken();
			List<MedicalSignOffResponseViewModel> response = await _crewClient.GetMedicalSignOffList(input);
			return new JsonResult(new { data = response });
		}

		/// <summary>
		/// Loads the medical sign off.
		/// </summary>
		/// <param name="crew">The crew.</param>
		/// <returns></returns>
		public IActionResult LoadMedicalSignOff(CrewListViewModel crew)
		{
			CrewListViewModel request = new CrewListViewModel();
			request.FromMedicalSignOff = DateTime.Now.AddMonths(-3);
			request.ToMedicalSignOff = DateTime.Now;
			request.EncryptedVesselId = crew.EncryptedVesselId;
			string crewURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
			return Json(Url.Action("MedicalSignOffList", new { CrewList = crewURL, VesselId = crew.EncryptedVesselId }));
		}

		/// <summary>
		/// Sets the page parameter mso.
		/// </summary>
		/// <param name="crew">The crew.</param>
		/// <returns></returns>
		public IActionResult SetPageParameterMSO(CrewListViewModel crew)
		{
			CrewListViewModel request = new CrewListViewModel();
			request.EncryptedVesselId = crew.EncryptedVesselId;
			request.ToMedicalSignOff = crew.ToMedicalSignOff;
			request.FromMedicalSignOff = crew.FromMedicalSignOff;
			string crewURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
			SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.MedicalSignOffListPageKey), crewURL, crew.EncryptedVesselId);
			return Json(Url.Action("MedicalSignOffList", new { CrewList = crewURL, VesselId = crew.EncryptedVesselId }));
		}


		#endregion

		#region Details

		/// <summary>
		/// Indexes the specified crew identifier.
		/// </summary>
		/// <param name="CrewId">The crew identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> Details(string CrewId, string VesselId, bool IsVesselChanged, string Source, string context)
		{			
			if (!string.IsNullOrWhiteSpace(context))
			{
				ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
				string tempCrewId = contextParameter.CrewId;
				CrewId = _provider.CreateProtector("CrewId").Protect(tempCrewId);
				Source = EnumsHelper.GetKeyValue(CrewDetailSource.CrewList);
			}

			if (IsVesselChanged)
			{
				if (Source == EnumsHelper.GetKeyValue(CrewDetailSource.CrewList))
				{
					CrewListViewModel crewVm = new CrewListViewModel();
					crewVm.ToDate = DateTime.Now;
					crewVm.FromDate = DateTime.Now;
					crewVm.SelectedFilter = CrewStageFilter.Onboard;
					string crewListURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(crewVm));
					return RedirectToAction("List", new { CrewList = crewListURL, VesselId = VesselId });
				}
				else if (Source == EnumsHelper.GetKeyValue(CrewDetailSource.MedicalSignOffList))
				{
					CrewListViewModel crewVm = new CrewListViewModel();
					crewVm.ToMedicalSignOff = DateTime.Now;
					crewVm.FromMedicalSignOff = DateTime.Now.AddMonths(-3);
					string crewListURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(crewVm));
					return RedirectToAction("MedicalSignOffList", new { CrewList = crewListURL, VesselId = VesselId });
				}
			}

			string sourceKeyName = string.Empty;
			if (Source == EnumsHelper.GetKeyValue(CrewDetailSource.CrewList))
			{
				sourceKeyName = EnumsHelper.GetKeyValue(NavigationPageKey.CrewListPageKey);
			}
			else if (Source == EnumsHelper.GetKeyValue(CrewDetailSource.MedicalSignOffList))
			{
				sourceKeyName = EnumsHelper.GetKeyValue(NavigationPageKey.MedicalSignOffListPageKey);
			}
						
			_crewClient.AccessToken = GetAccessToken();
			CrewDetailsViewModel detailsViewModel = new CrewDetailsViewModel();
			var decryptedCrewId = _provider.CreateProtector("CrewId").Unprotect(CrewId);		
			CrewHeaderDetailsViewModel result = await _crewClient.GetCrewDetails(decryptedCrewId);
			
			string[] contextParams = { decryptedCrewId };
			string[] messageParams = { result.Name };
			string decreptedString = CommonUtil.GetDecryptedVessel(_provider, VesselId);
			string currentVesselName = string.Empty;
			string currentVesselId = string.Empty;
			if (!string.IsNullOrWhiteSpace(decreptedString))
			{
				currentVesselId = decreptedString.Split(Constants.Separator)[0];
				currentVesselName = CommonUtil.GetVesselNameFromDisplayName(decreptedString.Split(Constants.Separator)[1]);
			}
			
			detailsViewModel.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.Crew, currentVesselId, currentVesselName, contextParams, messageParams, decryptedCrewId);

			SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.CrewDetailsPageKey), sourceKeyName, CrewId);
						
			detailsViewModel.CrewId = CrewId;
			detailsViewModel.VesselId = VesselId;
			detailsViewModel.IsFromViewRecord = IsFromViewRecordVal(context);
			detailsViewModel.ActiveMobileTabClass = SetTab(EnumsHelper.GetKeyValue(NavigationPageKey.CrewDetailsPageKey), detailsViewModel.ActiveMobileTabClass, Constants.DropdownTab1);
			return View(detailsViewModel);
		}

		/// <summary>
		/// Gets the crew details.
		/// </summary>
		/// <param name="identifier">The crew identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetCrewDetails(string identifier)
		{
			var decryptedCrewId = _provider.CreateProtector("CrewId").Unprotect(identifier);
			_crewClient.AccessToken = GetAccessToken();
			CrewHeaderDetailsViewModel result = await _crewClient.PostGetCrewHeaderDetails(decryptedCrewId);
			return new JsonResult(result);
		}

		/// <summary>
		/// Gets the crew current details.
		/// </summary>
		/// <param name="identifier">The crew identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetCrewCurrentDetails(string identifier)
		{
			var decryptedCrewId = _provider.CreateProtector("CrewId").Unprotect(identifier);
			_crewClient.AccessToken = GetAccessToken();
			CrewCurrentDetailsViewModel result = await _crewClient.PostGetCrewCurrentDetailsAsync(decryptedCrewId);
			return new JsonResult(result);
		}

		/// <summary>
		/// Gets the service history.
		/// </summary>
		/// <param name="identifier">The crew identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetServiceHistory(string identifier, bool canIncludeShore)
		{
			var decryptedCrewId = _provider.CreateProtector("CrewId").Unprotect(identifier);
			_crewClient.AccessToken = GetAccessToken();
			List<ServiceHistoryViewModel> results = await _crewClient.PostGetCrewServiceHistory(decryptedCrewId, canIncludeShore);
			return new JsonResult(new { data = results });
		}

		/// <summary>
		/// Gets the certificates and documents.
		/// </summary>
		/// <param name="identifier">The crew identifier.</param>
		/// <returns>list of CertificatesAndDocumentsViewModel</returns>
		public async Task<IActionResult> GetCertificatesAndDocuments(string identifier)
		{
			var decryptedCrewId = _provider.CreateProtector("CrewId").Unprotect(identifier);
			_crewClient.AccessToken = GetAccessToken();
			List<CertificatesAndDocumentsViewModel> results = await _crewClient.PostGetCertificatesAndDocumentsAsync(decryptedCrewId);
			_sharedClient.AccessToken = GetAccessToken();
			List<TableAttribute> covidResponse = await _sharedClient.PostGetCovidListAsync(new List<string>() { EnumsHelper.GetKeyValue(CrewMasterDataTables.CovidDocumentType) });
			if (results != null && covidResponse != null)
			{
				results.ForEach(x =>
				{
					if (covidResponse.Any(t => t.AttributeName.Equals(x.DocId)))
					{
						x.IsCovidVaccineTypeDocument = true;
					}
				});
			}
			return new JsonResult(new { data = results });
		}

		/// <summary>
		/// Posts the get crew image.
		/// </summary>
		/// <param name="identifier">The crew identifier.</param>
		/// <returns>Crew Image</returns>
		public async Task<IActionResult> PostGetCrewImage(string identifier)
		{
			var decryptedCrewId = _provider.CreateProtector("CrewId").Unprotect(identifier);
			_documentClient.AccessToken = GetAccessToken();
			var result = await _documentClient.DownloadCrewImage(decryptedCrewId);
			byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
			var image = byteData != null ? Convert.ToBase64String(byteData) : null;
			return new JsonResult(image);
		}

		/// <summary>
		/// Downloads the document.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>Json bytes</returns>
		public async Task<IActionResult> DownloadDocument(string input)
		{
			_documentClient.AccessToken = GetAccessToken();
			CloudDocumentDownloadRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<CloudDocumentDownloadRequest>(input);
			request.DocumentCategory = DocumentCategory.CrewDocument;
			request.DocumentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(request.FileName)).FirstOrDefault();
			var result = await _documentClient.DownloadDocument(request);
			byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
			string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
			return new JsonResult(new { filename = request.FileName, bytes = byteString, fileType = EnumsHelper.GetDescription(request.DocumentFileType) });
		}

		/// <summary>
		/// Gets the attachments.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		/// <returns>list of AttachmentViewModel</returns>
		public async Task<IActionResult> GetAttachments(string identifier, List<string> matchedIds)
		{
			var decryptedCrewId = _provider.CreateProtector("CrewId").Unprotect(identifier);
			_crewClient.AccessToken = GetAccessToken();
			List<ViewModels.Crew.AttachmentViewModel> results = await _crewClient.PostGetCrewAttachments(decryptedCrewId, matchedIds);
			return new JsonResult(new { data = results });
		}

		/// <summary>
		/// Gets the crew cv details.
		/// </summary>
		/// <returns></returns>
		public IActionResult GetCrewCvDetails()
		{
			List<Lookup> formatList = new List<Lookup>();
			foreach (var format in EnumsHelper.GetValues<FileFormatTypes>())
			{
				formatList.Add(new Lookup() { Identifier = EnumsHelper.GetKeyValue(format), Description = EnumsHelper.GetDescription(format) });
			}

			FormatTypes reportFormatType = FormatTypes.FormatTypeVShips;
			string reportFormatTypeDesc = EnumsHelper.GetDescription(reportFormatType);
			string defaultFormat = EnumsHelper.GetKeyValue(FileFormatTypes.FormatPDF);
			return new JsonResult(new { 
				FormatList = formatList, 
				ReportFormatType = reportFormatType, 
				ReportFormatTypeDesc = reportFormatTypeDesc,
				DefaultFormat = defaultFormat
			});
		}

		/// <summary>
		/// Creates the cv.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<JsonResult> CreateCV(CrewCvRequestViewModel input)
		{
			_sharedClient.AccessToken = GetAccessToken();
			var decryptedCrewId = _provider.CreateProtector("CrewId").Unprotect(input.EncryptedCrewId);

			string reportIdentifier = null;
			if (input.ReportFormatType == FormatTypes.FormatTypeVShips)
			{
				reportIdentifier = EnumsHelper.GetKeyValue(ReportMaster.CrewingCurriculumVitaeVShipsReport);
			}

			ReportLight reportRequest = await _sharedClient.GetReportLightByFilename(reportIdentifier);

			if (reportRequest != null)
			{
				reportRequest.FriendlyFileName = input.CrewRank + "_" + input.CrewName + "_CV";
				if (!String.IsNullOrWhiteSpace(input.ReportFormat))
				{
					if (input.ReportFormat == EnumsHelper.GetKeyValue(FileFormatTypes.FormatPDF))
					{
						reportRequest.ReportFormat = ReportExportTypes.PDF;
					}
					else if (input.ReportFormat == EnumsHelper.GetKeyValue(FileFormatTypes.FormatTypeExcel))
					{
						reportRequest.ReportFormat = ReportExportTypes.Excel;
					}
					else if (input.ReportFormat == EnumsHelper.GetKeyValue(FileFormatTypes.FormatTypeMSWord))
					{
						reportRequest.ReportFormat = ReportExportTypes.Word;
					}
				}
				else
				{
					reportRequest.ReportFormat = ReportExportTypes.PDF;
				}
				foreach (var p in reportRequest.ReportParameters)
				{
					if (p.ParameterName.Contains("@sCRWID"))
					{
						p.ValueToSet = new List<object>() { decryptedCrewId };
					}
					if (p.ParameterName.Contains("DisplayAddress"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeAddress };
					}
					if (p.ParameterName.Contains("DisplayContactDetails"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeContact };
					}
					if (p.ParameterName.Contains("DisplayImage"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludePicture };
					}
					if (p.ParameterName.Contains("DisplaySummaryOfCompetence"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeSummaryOfCompetence };
					}
					if (p.ParameterName.Contains("DisplayOnShoreDetails"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeOnShoreHistory };
					}
					if (p.ParameterName.Contains("DisplayOnBoardDetails"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeServiceDetails };
					}
					if (p.ParameterName.Contains("DisplayIdentificationDocuments"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeDocuments };
					}
					if (p.ParameterName.Contains("DisplayCertificates"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeDocuments };
					}
					if (p.ParameterName.Contains("DisplayGeneralRemarks"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeNotes };
					}
					if (p.ParameterName.Contains("DisplayMedicalHistory"))
					{
						p.ValueToSet = new List<object>() { input.IsIncludeMedicalRecords };
					}
					if (p.ParameterName.Contains("@sUSR_ID"))
					{
						p.ValueToSet = new List<object>() { Request.Cookies["UserId"] };
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
	}
}