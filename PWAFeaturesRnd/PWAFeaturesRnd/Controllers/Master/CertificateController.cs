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
using PWAFeaturesRnd.Models.Report.Certificate;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Certificate;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.PlannedMaintenance;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CertificateController : AuthenticatedController
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly MarineClient _marineClient;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// The DocumentClient
        /// </summary>
        private DocumentClient _documentClient;

        /// <summary>
        /// The SharedClient
        /// </summary>
        private SharedClient _sharedClient;

		/// <summary>
		/// The notification client
		/// </summary>
		private readonly NotificationClient _notificationClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="PurchaseOrderController" /> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="documentClient">The DocumentClient.</param>
		/// <param name="sharedClient">The SharedClient.</param>
		/// <param name="notificationClient">The notification client.</param>
		public CertificateController(MarineClient client, IDataProtectionProvider provider, DocumentClient documentClient, SharedClient sharedClient, NotificationClient notificationClient)
        {
            _marineClient = client;
            _provider = provider;
            _documentClient = documentClient;
            _sharedClient = sharedClient;
            _notificationClient = notificationClient;
        }

        /// <summary>
        /// Lists the specified certificate request URL.
        /// </summary>
        /// <param name="CertificateRequestUrl">The certificate request URL.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult List(string CertificateRequestUrl, string VesselId, bool IsViewMore = false)
        {

            CertificateRequestViewModel certificateRequestVM = new CertificateRequestViewModel();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);

            certificateRequestVM = CommonUtil.GetDecryptedRequest<CertificateRequestViewModel>(_provider, "CertificateURL", CertificateRequestUrl);
            certificateRequestVM = GetCertificateDashboardDetails(certificateRequestVM);
            string encryptedUrl = CommonUtil.GetEncryptedURL(_provider, "CertificateURL", certificateRequestVM);
            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.CertificateListPageKey);
            SetSessionDetail(pageKey, null, encryptedUrl);
            RemoveSessionFilter(_provider, pageKey, null, decreptedString.Split(Constants.Separator)[0]);

            var SessionData = GetSessionFilter(pageKey);
        
            certificateRequestVM = CommonUtil.GetDecryptedRequest<CertificateRequestViewModel>(_provider, "CertificateURL", SessionData);

            certificateRequestVM.VesselName = decreptedString.Split(Constants.Separator)[1];
            certificateRequestVM.VesselId = VesselId;
            certificateRequestVM.ActiveMobileTabClass = SetTab(pageKey, certificateRequestVM.ActiveMobileTabClass, Constants.Tab2);
            return View(certificateRequestVM);
        }

        /// <summary>
        /// Loads the planned maintenance.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public IActionResult LoadPlannedMaintenance(CertificateRequestViewModel input)
        {
            return Json(Url.Action(Constants.ListMethod, Constants.PlannedMaintenanceController, new { PlannedMaintenance = GetPMSManagedCertificatesNav(input.VesselId), VesselId = input.VesselId }));
        }

        /// <summary>
        /// Gets the PMS managed certificates navigation.
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private string GetPMSManagedCertificatesNav(string encryptedVesselId)
        {
            PlannedMaintenanceListViewModel input = new PlannedMaintenanceListViewModel();
            input.EncryptedVesselId = encryptedVesselId;
            input.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            input.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            input.GridSubTitle = Constants.PMSCertificates;
            input.StageName = EnumsHelper.GetKeyValue(PMSDashboardStage.PMSManagedCertificates);
            input.SelectedWBJobTypeIds = EnumsHelper.GetKeyValue(JobType.Certificate);
            return CommonUtil.GetEncryptedURL(_provider, Constants.PMSList, input);
        }
        /// <summary>
        /// Gets the header summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHeaderSummary(CertificateRequestViewModel input)
        {
            _marineClient.AccessToken = GetAccessToken();
            VesselCertificateSummaryStatResponseViewModel response = await _marineClient.PostGetVesCertSummaryStats(input);
            return new JsonResult(response);
        }

        /// <summary>
        /// Sets the page parameter.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public IActionResult SetPageParameter(CertificateRequestViewModel input)
        {
            CertificateRequestViewModel request = new CertificateRequestViewModel();
            request.StageName = input.StageName;
            request.VesselId = input.VesselId;
            request.VesselName = input.VesselName;
            request.CertificateImpact = input.CertificateImpact;
            request.CertificateStatus = input.CertificateStatus;
            request.CertificateType = input.CertificateType;
            request.IncludeWindow = input.IncludeWindow;
            request.ToDate = input.ToDate;
            request.FromDate = input.FromDate;
            request.SearchKeyword = input.SearchKeyword;

            string cerificateUrl = _provider.CreateProtector("CertificateURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
            SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.CertificateListPageKey), cerificateUrl, input.VesselId);

            return new JsonResult(new { data = request });
        }

        /// <summary>
        /// Gets the certificate list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCertificateList(CertificateRequestViewModel input)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<CertificatePreviewViewModel> response = await _marineClient.PostGetVesselCertificatesPaged(input);
            if (response != null && response.Any())
            {
                RecordDiscussionRequestViewModel request1 = new RecordDiscussionRequestViewModel();
                request1.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.Certificate));
                request1.ReferenceIds = response.Select(x => x.VesselCertificateId).ToList();

                _notificationClient.AccessToken = GetAccessToken();
                List<RecordDiscussionResponse> DiscussionAndNotesCountList = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(request1);

                foreach (var item in DiscussionAndNotesCountList.Where(x => x.ChannelCount > 0 || x.NotesCount > 0))
                {
                    foreach (var certificate in response.Where(x => x.VesselCertificateId == item.ReferenceIdentifier))
                    {
                        NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
                        {
                            CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.Certificate)),
                            ReferenceIdentifier = item.ReferenceIdentifier
                        };

                        certificate.ChannelCount = item.ChannelCount;
                        certificate.NotesCount = item.NotesCount;
                        certificate.MessageDetailsJSON = JsonConvert.SerializeObject(newMessageDetails);
                    }
                }
            }

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the certificate list.
        /// </summary>
        /// <param name="VesselCertificateLogId">VesselCertificateLogId of the certificate.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCertificateDocuments(List<string> VesselCertificateLogIds)
        {
            _sharedClient.AccessToken = GetAccessToken();
            DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest();
            documentDetailRequest.DocumentSourceIds = VesselCertificateLogIds;
            documentDetailRequest.SsmId = EnumsHelper.GetKeyValue(SubModule.VesselCertificate);
            documentDetailRequest.DctId = EnumsHelper.GetKeyValue<DocumentCategoryType>(DocumentCategoryType.Certificates);
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
        /// Gets the certificate list.
        /// </summary>
        /// <param name="documentId">DocumentId of the invoice document.</param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadDocument(string input)
        {
            _documentClient.AccessToken = GetAccessToken();
            CloudDocumentDownloadRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<CloudDocumentDownloadRequest>(input);
            request.DocumentCategory = DocumentCategory.VesselCertificate;
            request.DocumentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(request.FileName)).FirstOrDefault();
            var result = await _documentClient.DownloadDocument(request);
            byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
            string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
            return new JsonResult(new { filename = request.FileName, bytes = byteString, fileType = EnumsHelper.GetDescription(request.DocumentFileType) });
        }

        /// <summary>
        /// Gets the impact values.
        /// </summary>
        /// <returns>List<LookUp></returns>
        public IActionResult GetImpactValues()
        {
            List<LookUp> result = new List<LookUp>();
            List<CertificateImpact> certificateImpacts = _marineClient.GetImpactValues();
            foreach (CertificateImpact impact in certificateImpacts)
            {
                if (EnumsHelper.GetKeyValue(impact) == null)
                {
                    result.Add(new LookUp { Identifier = "", Description = EnumsHelper.GetDescription(impact) });
                }
                else
                {
                    result.Add(new LookUp { Identifier = EnumsHelper.GetKeyValue(impact), Description = EnumsHelper.GetDescription(impact) });
                }
            }
            return new JsonResult(result);
        }
        /// <summary>
        /// Gets the certificate types.
        /// </summary>
        /// <returns>List<LookUp></returns>
        public IActionResult GetCertificateTypes()
        {
            List<LookUp> result = new List<LookUp>();
            List<CertificateType> types = _marineClient.GetCertificateTypes();
            foreach (CertificateType type in types)
            {
                if (EnumsHelper.GetKeyValue(type) == null)
                {
                    result.Add(new LookUp { Identifier = "", Description = "All" });
                }
                else
                {
                    result.Add(new LookUp { Identifier = EnumsHelper.GetKeyValue(type), Description = EnumsHelper.GetDescription(type) });
                }

            }
            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the certificate statuses.
        /// </summary>
        /// <returns>List<LookUp></returns>
        public IActionResult GetCertificateStatuses()
        {
            List<LookUp> result = new List<LookUp>();
            List<VesselCertificateStatus> statuses = _marineClient.GetVesselCertificateStatuses();
            foreach (VesselCertificateStatus status in statuses)
            {
                result.Add(new LookUp { Identifier = EnumsHelper.GetKeyValue(status), Description = EnumsHelper.GetDescription(status) });
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the certificate audit log.
        /// </summary>
        /// <param name="VesselCertificateId">The vessel certificate identifier.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCertificateAuditLog(string VesselCertificateId, string VesselId)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            VesselId = decreptedString.Split(Constants.Separator)[0];
            _sharedClient.AccessToken = GetAccessToken();
            var input = new Dictionary<string, object>()
            {
                { "vesselId", VesselId },
                { "vesselCertificateId", VesselCertificateId}
            };

            List<VesselCertificateAuditLogDetail> response = await _sharedClient.VesselCertificateAuditLog(input);

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the certificate renewal history.
        /// </summary>
        /// <param name="VesselCertificateId">The vessel certificate identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCertificateRenewalHistory(string VesselCertificateId)
        {
            _marineClient.AccessToken = GetAccessToken();
            var input = new Dictionary<string, object>()
                {
                    { "vesselCertificateId", VesselCertificateId },
                    { "fetchDocumentCount", true }
                };
            List<CertificateIssueDetail> response = await _marineClient.GetCertificateIssueLog(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the PMS certificates.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPMSCertificates(string vesselId)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
            vesselId = decreptedString.Split(Constants.Separator)[0];

            _marineClient.AccessToken = GetAccessToken();
            List<WorkBasketAllDetailViewModel> response = await _marineClient.GetPMSCertificates(vesselId);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Bind Certificate Dashboard Details
        /// </summary>
        /// <param name="certificateUrl"></param>
        /// <param name="vesselId"></param>
        /// <returns></returns>
        public IActionResult BindCertificateDashboardDetails(string certificateUrl, string vesselId)
        {
            CertificateRequestViewModel certificateRequestVM = new CertificateRequestViewModel();
            string data = _provider.CreateProtector("CertificateURL").Unprotect(certificateUrl);
            certificateRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<CertificateRequestViewModel>(data);
            certificateRequestVM = GetCertificateDashboardDetails(certificateRequestVM);
            string encryptedUrl = CommonUtil.GetEncryptedURL(_provider,"CertificateURL", certificateRequestVM);

            certificateRequestVM.VesselId = vesselId;
            SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.CertificateListPageKey), encryptedUrl, vesselId);

            return new JsonResult(new { data = certificateRequestVM, vesselId = vesselId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vesselId"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetRequisitionMappedOrders(string vesselId, string vesselCertificateLogId)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<RequisitionOrdersViewModel> response = await _marineClient.GetRequisitionMappedOrders(vesselId, vesselCertificateLogId);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the certificate dashboard details.
        /// </summary>
        /// <param name="certificateRequestVM">The certificate request vm.</param>
        /// <returns></returns>
        public CertificateRequestViewModel GetCertificateDashboardDetails(CertificateRequestViewModel certificateRequestVM)
        {
            if (certificateRequestVM.StageName == EnumsHelper.GetKeyValue(VesselCertificates.TotalActive))
            {
                certificateRequestVM.IncludeWindow = true;
                certificateRequestVM.CertificateImpact = EnumsHelper.GetKeyValue(CertificateImpact.All);
                certificateRequestVM.CertificateStatus = EnumsHelper.GetKeyValue(VesselCertificateStatus.Active); 
            }
            else if (certificateRequestVM.StageName == EnumsHelper.GetKeyValue(VesselCertificates.ExpiringIn30Days))
            {
                certificateRequestVM.CertificateImpact = EnumsHelper.GetKeyValue(CertificateImpact.All);
                certificateRequestVM.FromDate = DateTime.Now.Date;
                certificateRequestVM.ToDate = DateTime.Now.AddDays(Constants.CertificateDueNowRange).Date;
                certificateRequestVM.IncludeWindow = false;
                certificateRequestVM.CertificateStatus = EnumsHelper.GetKeyValue(VesselCertificateStatus.Active);
            }
            return certificateRequestVM;
        }

		/// <summary>
		/// Detailses the specified certificate request.
		/// </summary>
		/// <param name="CertificateRequest">The certificate request.</param>
		/// <param name="VesselId">The vessel identifier.</param>
		/// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public async Task<IActionResult> Details(string CertificateRequest, string VesselId, bool IsVesselChanged, string context)
        {
            _marineClient.AccessToken = GetAccessToken();
            CertificateDetailViewModel certificateDetails = null;
            string vesselCertificateId = string.Empty;
            if (IsVesselChanged)
            {
                CertificateRequestViewModel certificateRequestVM = new CertificateRequestViewModel();
                string certificateListUrl = _provider.CreateProtector("CertificateURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(certificateRequestVM));

                return RedirectToAction("List", new { CertificateRequestUrl = certificateListUrl, VesselId = VesselId });
            }

			if (!string.IsNullOrWhiteSpace(CertificateRequest))
			{
                certificateDetails = CommonUtil.GetDecryptedRequest<CertificateDetailViewModel>(_provider, Constants.CertificatesDetails, CertificateRequest);
                vesselCertificateId = certificateDetails.VesselCertificateId;
            }

            if (!string.IsNullOrWhiteSpace(context))
            {
                ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
                vesselCertificateId = contextParameter.VesselCertificateId;
            }

            if (!string.IsNullOrWhiteSpace(vesselCertificateId))
			{
                certificateDetails = await _marineClient.GetCertificateDetails(vesselCertificateId);
            }
            
			string[] contextParams = { certificateDetails.VesselCertificateId};
			string[] messageParams = { certificateDetails.CertificateNumber, certificateDetails.CertificateName };

			certificateDetails.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.Certificate, certificateDetails.VesselId, certificateDetails.VesselName, contextParams, messageParams, vesselCertificateId);

			SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.CertificateDetailsPageKey), EnumsHelper.GetKeyValue(NavigationPageKey.CertificateListPageKey), CertificateRequest);
            certificateDetails.IsFromViewRecord = IsFromViewRecordVal(context);

            return View(certificateDetails);
        }
    }
}
