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
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.Models.Report.Defect;
using PWAFeaturesRnd.Models.Report.InspectionManager;
using PWAFeaturesRnd.Models.Report.PlannedMaintenance;
using PWAFeaturesRnd.Models.Report.VoyageReporting;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.Defect;
using PWAFeaturesRnd.ViewModels.Inspection;
using PWAFeaturesRnd.ViewModels.PlannedMaintenance;
using PWAFeaturesRnd.ViewModels.VoyageReporting;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class MarineWCFClient : BaseHttpClient
    {
        #region Properties

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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MarineWCFClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public MarineWCFClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider, IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
        {
            client.BaseAddress = new Uri(AppSettings.MarineWCFApiUrl);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        #endregion

        #region Inspection

        /// <summary>
        /// Gets the inspection details by inspection.
        /// </summary>
        /// <param name="inspectionId">The inspection identifier.</param>
        /// <returns></returns>
        public async Task<InspectionDetailsViewModel> GetInspectionDetailsByInspection(string inspectionId)
        {
            InspectionDetailsViewModel inspectionDetails = new InspectionDetailsViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/GetInspectionByInspectionId/" + inspectionId));
            Inspection inspection = await GetAsync<Inspection>(requestUrl);
            if (inspection != null)
            {
                inspectionDetails.IsClosed = inspection.DateClosed.HasValue;
                inspectionDetails.IsDeleted = inspection.IsDeleted.GetValueOrDefault();
                inspectionDetails.IalIdReportStatus = inspection.IalIdReportStatus; //Report tab related
                inspectionDetails.TplId = inspection.TplId; //Report tab related
                inspectionDetails.IsSupportingDocumentVisible = inspection.InspectionTypeRequireDocument == true ? true : false; //Supporting doc
                inspectionDetails.DateClosed = inspection.DateClosed;
                inspectionDetails.InspectionTypeId = inspection.InspectionTypeId;
            }
            return inspectionDetails;
        }

        /// <summary>
        /// Gets the inspection by inspection identifier.
        /// </summary>
        /// <param name="inspectionId">The inspection identifier.</param>
        /// <returns></returns>
        public async Task<InspectionDetailsViewModel> GetInspectionByInspectionId(string inspectionId)
        {
            InspectionDetailsViewModel inspectionDetails = new InspectionDetailsViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/GetInspectionByInspectionId/" + inspectionId));
            Inspection inspection = await GetAsync<Inspection>(requestUrl);
            if (inspection != null)
            {
                inspectionDetails.IsClosed = inspection.DateClosed.HasValue;
                inspectionDetails.IsDeleted = inspection.IsDeleted.GetValueOrDefault();
                inspectionDetails.IalIdReportStatus = inspection.IalIdReportStatus; //Report tab related
                inspectionDetails.TplId = inspection.TplId; //Report tab related
                inspectionDetails.IsSupportingDocumentVisible = inspection.InspectionTypeRequireDocument == true ? true : false; //Supporting doc
                inspectionDetails.DateClosed = inspection.DateClosed;
                inspectionDetails.InspectionTypeId = inspection.InspectionTypeId;
            }

            List<InspectionFindingLiteViewModel> findingDetails = new List<InspectionFindingLiteViewModel>();

            if (inspectionDetails != null && !inspectionDetails.DateClosed.HasValue)
            {
                findingDetails = await GetInspectionFindingsByInspectionId(inspectionId);
                if (findingDetails != null)
                {
                    int total = findingDetails.Count;
                    int complete = findingDetails.Where(x => x.DateCleared != null).Count();
                    inspectionDetails.IsInspectionClomplete = total == complete;
                    inspectionDetails.AllFindingsCleared = total == complete;
                }
                else
                {
                    inspectionDetails.IsInspectionClomplete = false;
                    inspectionDetails.AllFindingsCleared = false;
                }
            }

            return inspectionDetails;
        }

        /// <summary>
        /// Gets the inspection findings by inspection identifier.
        /// </summary>
        /// <param name="inspectionId">The inspection identifier.</param>
        /// <returns></returns>
        public async Task<List<InspectionFindingLiteViewModel>> GetInspectionFindingsByInspectionId(string inspectionId)
        {
            List<InspectionFindingLiteViewModel> findingDetailsVM = new List<InspectionFindingLiteViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/GetInspectionFindingsByInspectionId/" + inspectionId));
            List<InspectionFindingLite> findings = await GetAsync<List<InspectionFindingLite>>(requestUrl);
            if (findings != null && findings.Any())
            {
                foreach (InspectionFindingLite item in findings)
                {
                    InspectionFindingLiteViewModel findingVM = new InspectionFindingLiteViewModel();
                    findingVM.DateCleared = item.DateCleared;
                    findingDetailsVM.Add(findingVM);
                }
            }

            return findingDetailsVM;
        }

        /// <summary>
        /// Gets the type of the omv inspection action.
        /// </summary>
        /// <param name="isApplicableForInspectionType">Type of the is applicable for inspection.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> GetOMVInspectionActionType(bool? isApplicableForInspectionType)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/GetOMVInspectionActionType/" + isApplicableForInspectionType));
            List<Lookup> result = await GetAsync<List<Lookup>>(requestUrl);
            return result;
        }

        /// <summary>
        /// Gets the screening detail for omv inspection.
        /// </summary>
        /// <param name="inspectionId">The inspection identifier.</param>
        /// <returns></returns>
        public async Task<InspectionOmvScreeningLiteViewModel> GetScreeningDetailForOmvInspection(string inspectionId)
        {
            InspectionOmvScreeningLiteViewModel screenings = new InspectionOmvScreeningLiteViewModel();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/GetScreeningDetailForOmvInspection/" + inspectionId));
            InspectionOmvScreeningLite response = await GetAsync<InspectionOmvScreeningLite>(requestUrl);

            if (response != null)
            {
                screenings.ScreeningTypeId = response.ScreeningTypeId;
            }
            return screenings;
        }

        /// <summary>
        /// Posts the getinspection question answer details.
        /// </summary>
        /// <param name="inspectionReqVM">The inspection req vm.</param>
        /// <returns></returns>
        public async Task<List<MarineQuestionAnswerDetailResponseViewModel>> PostGetinspectionQuestionAnswerDetails(InspectionQuestionAnswerDetailRequestViewModel inspectionReqVM)
        {
            List<MarineQuestionAnswerDetailResponseViewModel> QuestionsList = new List<MarineQuestionAnswerDetailResponseViewModel>();
            InspectionQuestionAnswerDetailRequest request = new InspectionQuestionAnswerDetailRequest()
            {
                VesselId = inspectionReqVM.VesselId,
                VrpId = inspectionReqVM.InspectionTypeId,
                VstId = inspectionReqVM.InspectionId
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/PostGetinspectionQuestionAnswerDetails"));
            List<MarineQuestionAnswerDetailResponse> response = await PostAsync<List<MarineQuestionAnswerDetailResponse>>(requestUrl, CreateHttpContent(request));
            if (response != null && response.Any())
            {
                foreach (MarineQuestionAnswerDetailResponse item in response)
                {
                    MarineQuestionAnswerDetailResponseViewModel question = new MarineQuestionAnswerDetailResponseViewModel();
                    question.IsValid = IsAllQuestionsValid(item);
                    QuestionsList.Add(question);
                }
            }
            return QuestionsList;
        }

        /// <summary>
        /// Determines whether [is all questions valid] [the specified question].
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns>
        ///   <c>true</c> if [is all questions valid] [the specified question]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAllQuestionsValid(MarineQuestionAnswerDetailResponse question)
        {
            bool isValid = true;
            if (question.IsCommentRequired && string.IsNullOrWhiteSpace(question.Comment))
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            if ((question.IalIdAnswerType == EnumsHelper.GetKeyValue(QuestionAnswerType.Date) && question.DateAnswer == null) && question.IsRequired)
            {
                isValid = false;
            }
            else
            {
                isValid = true; ;
            }

            if ((question.IalIdAnswerType == EnumsHelper.GetKeyValue(QuestionAnswerType.Text) && string.IsNullOrWhiteSpace(question.TextAnswer)) && question.IsRequired)
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            if ((question.IalIdAnswerType == EnumsHelper.GetKeyValue(QuestionAnswerType.Numeric) && question.NumericAnswer == null) && question.IsRequired)
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            if ((question.IalIdAnswerType == EnumsHelper.GetKeyValue(QuestionAnswerType.YesNo) && question.BooleanAnswer == null) && question.IsRequired)
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            UserDefinedListLibraryResponseViewModel selectedUserDefinedListValue = new UserDefinedListLibraryResponseViewModel();
            MarineQuestionListAnswerResponse _userDefinedListAnswerResponse = null;
            if (question.UserDefinedListAnswer != null && question.UserDefinedListAnswer.Any())
            {
                _userDefinedListAnswerResponse = question.UserDefinedListAnswer.FirstOrDefault();
            }

            //Need to show logic and test this particular condition
            Task<List<UserDefinedListLibraryResponseViewModel>> userDefinedListValues = null;
            if (_userDefinedListAnswerResponse != null)
            {
                userDefinedListValues = GetUserDefinedListValues(question.QalId);
            }

            selectedUserDefinedListValue = userDefinedListValues != null && userDefinedListValues.Result != null ? userDefinedListValues.Result.FirstOrDefault(x => x.QalId == _userDefinedListAnswerResponse.QalId) : null;
            if ((question.IalIdAnswerType == EnumsHelper.GetKeyValue(QuestionAnswerType.UserDefinedList) && selectedUserDefinedListValue == null) && question.IsRequired)
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Gets the user defined list values.
        /// </summary>
        /// <param name="qalId">The qal identifier.</param>
        /// <returns></returns>
        public async Task<List<UserDefinedListLibraryResponseViewModel>> GetUserDefinedListValues(string qalId)
        {
            List<UserDefinedListLibraryResponseViewModel> userDefinedListValues = new List<UserDefinedListLibraryResponseViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/GetUserDefinedListValues/" + qalId));
            List<UserDefinedListLibraryResponse> response = await GetAsync<List<UserDefinedListLibraryResponse>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (UserDefinedListLibraryResponse item in response)
                {
                    UserDefinedListLibraryResponseViewModel userResponse = new UserDefinedListLibraryResponseViewModel();
                    userResponse.QalId = item.QalId;
                    userDefinedListValues.Add(userResponse);
                }
            }
            return userDefinedListValues;
        }

        /// <summary>
        /// Posts the save inspection.
        /// </summary>
        /// <param name="inspectionId">The inspection identifier.</param>
        /// <returns></returns>
        public async Task<InspectionClosureSuccessViewModel> PostSaveInspection(string inspectionId)
        {
            InspectionClosureSuccessViewModel successResponse = new InspectionClosureSuccessViewModel();
            Inspection request = new Inspection();
            //Service call is done here because same object is sent for save
            Uri inputRequestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/GetInspectionByInspectionId/" + inspectionId));
            request = await GetAsync<Inspection>(inputRequestUrl);

            List<Lookup> InspectionStatuses = await GetOMVInspectionActionType(false);
            InspectionOmvScreeningLiteViewModel screenings = await GetScreeningDetailForOmvInspection(inspectionId);

            Lookup InspectionStatus = new Lookup();
            if (screenings != null && InspectionStatuses != null && InspectionStatuses.Any())
            {
                string screeningTypeId = screenings.ScreeningTypeId;
                InspectionStatus = InspectionStatuses.Where(x => x.Identifier == screeningTypeId).FirstOrDefault();
            }
            else
            {
                InspectionStatus = null;
            }

            if (request != null && request.InspectionTypeId == EnumsHelper.GetKeyValue(InspectionTypes.OilMajorVetting)
            && (InspectionStatus == null || InspectionStatus != null && InspectionStatus.Identifier != EnumsHelper.GetKeyValue<InspectionScreeningStatus>(InspectionScreeningStatus.Accepted)
            && InspectionStatus.Identifier != EnumsHelper.GetKeyValue<InspectionScreeningStatus>(InspectionScreeningStatus.Rejected)))
            {
                successResponse.Message = "Inspection cannot be closed in Pending state.";
                successResponse.OperationResult = false;
                return successResponse;
            }

            if (request != null && !string.IsNullOrWhiteSpace(request.IalIdReportStatus) && !string.IsNullOrWhiteSpace(request.TplId)
                && request.IalIdReportStatus != EnumsHelper.GetKeyValue(InspectionManagerReportStatus.ReportComplete))
            {
                successResponse.OperationResult = false;
                successResponse.Message = "This type of inspection can only be closed in Shipsure application.";
                return successResponse;
            }

            request.DateClosed = DateTime.Now;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Inspection/SaveInspection"));
            UpdateResponse<Inspection> response = await PostAsync<UpdateResponse<Inspection>>(requestUrl, CreateHttpContent(request));

            if (response.OperationSuccess)
            {
                successResponse.OperationResult = true;
                successResponse.Message = "Inspection closed successfully.";
            }
            else
            {
                successResponse.OperationResult = false;
                successResponse.Message = "Save Inspection operation failed.";
            }
            return successResponse;
        }

        #endregion

        #region Defect

        #region Defect closure action

        /// <summary>
        /// Closes the defect action.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <param name="refetchAfterSave">if set to <c>true</c> [refetch after save].</param>
        /// <returns></returns>
        public async Task<bool> CloseDefectAction(DefectClosureRequestViewModel inputRequest, bool refetchAfterSave)
        {
            Uri requestUrlForInput = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/DefectManager/GetDefectReportWOForEdit/" + inputRequest.DecryptedDWOId));
            DefectReportWorkOrder response = await GetAsync<DefectReportWorkOrder>(requestUrlForInput);
            if (response != null)
            {
                response.IsOffHire = inputRequest.IsOffHireRequired;
                response.ActualTime = inputRequest.ActualTimeDay;
                response.OffHireHours = inputRequest.OffHireHours;
                response.OffHireMins = inputRequest.OffHireMins;
                response.PlkIdOffHireType = inputRequest.OffhireTypeId;
                response.ImpactId = inputRequest.ImpactId;
                response.IsRegulatoryAuthority = inputRequest.IsRegulatoryAuthority;
                response.DispensationInPlace = inputRequest.DispensationInPlace;
                response.IsGasFree = inputRequest.IsGasFree;
                response.OffHireReason = inputRequest.OffHireReason;
            }

            var input = new Dictionary<string, object>()
                {
                    {"request", response },
                    {"refetchAfterSave",refetchAfterSave }
                };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/DefectManager/CloseDefectWorkOrder"));
            bool isSaveSuccessful = await PostAsync<bool>(requestUrl, CreateHttpContent(input));
            return isSaveSuccessful;
        }

        /// <summary>
        /// Posts the get component hierarchy.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<string> PostGetComponentHierarchy(DefectComponentHeirarchyRequestViewModel request)
        {
            string SystemAreaPath = string.Empty;

            var input = new Dictionary<string, object>()
                {
                    {"vesselId", request.VesselId },
                    {"componentId", request.ComponentId },
                    {"systemAreaId", request.SystemAreaId }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/DefectManager/PostGetComponentHierarchy"));
            List<DefectComponentHeirarchy> response = await PostAsync<List<DefectComponentHeirarchy>>(requestUrl, CreateHttpContent(input));
            if (response != null && response.Any())
            {
                SystemAreaPath = string.Join(" > ", response.OrderByDescending(x => x.Index).Select(x => x.Name));
            }

            return SystemAreaPath;
        }

        /// <summary>
        /// Posts the get defect work order attribute.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DefectWorkOrderAttributeViewModel>> PostGetDefectWorkOrderAttribute()
        {
            List<DefectWorkOrderAttributeViewModel> filters = new List<DefectWorkOrderAttributeViewModel>();
            List<DefectAttribute> filterRequest = new List<DefectAttribute>();
            filterRequest.Add(DefectAttribute.DefectImpact);
            filterRequest.Add(DefectAttribute.OffHirePeriod);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/DefectManager/PostGetDefectWorkOrderAttribute"));
            List<DefectWorkOrderAttribute> response = await PostAsync<List<DefectWorkOrderAttribute>>(requestUrl, CreateHttpContent(filterRequest));
            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    DefectWorkOrderAttributeViewModel statusItem = new DefectWorkOrderAttributeViewModel();
                    statusItem.AttributeName = item.AttributeName ?? "";
                    statusItem.DalId = item.DalId ?? "";
                    statusItem.LookupCode = item.LookupCode ?? "";
                    filters.Add(statusItem);
                }
            }

            return filters;
        }

        /// <summary>
        /// Gets the defect report wo for edit.
        /// </summary>
        /// <param name="dwoId">The dwo identifier.</param>
        /// <returns></returns>
        public async Task<DefectReportWorkOrderViewModel> GetDefectReportWOForEdit(string dwoId)
        {
            DefectReportWorkOrderViewModel DefectWorkOrderDetail = new DefectReportWorkOrderViewModel();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/DefectManager/GetDefectReportWOForEdit/" + dwoId));
            DefectReportWorkOrder response = await GetAsync<DefectReportWorkOrder>(requestUrl);
            if (response != null)
            {
                DefectWorkOrderDetail.IsOffHireRequired = response.IsOffHire.GetValueOrDefault();
                DefectWorkOrderDetail.ActualTime = response.ActualTime ?? 0;
                DefectWorkOrderDetail.OffHireHours = response.OffHireHours ?? 0;
                DefectWorkOrderDetail.OffHireMins = response.OffHireMins ?? 0;
                DefectWorkOrderDetail.OffhireTypeId = response.PlkIdOffHireType;
                DefectWorkOrderDetail.ImpactId = response.ImpactId;
                DefectWorkOrderDetail.IsRegulatoryAuthority = response.IsRegulatoryAuthority;
                DefectWorkOrderDetail.DispensationInPlace = response.DispensationInPlace;
                DefectWorkOrderDetail.IsGasFree = response.IsGasFree;
                DefectWorkOrderDetail.OffHireReason = response.OffHireReason ?? "";
            }

            return DefectWorkOrderDetail;
        }

        /// <summary>
        /// Posts the get reported defect wo for preview.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="dwoId">The dwo identifier.</param>
        /// <returns></returns>
        public async Task<PreviewReportedDefectWorkOrderViewModel> PostGetReportedDefectWOForPreview(string vesselId, string dwoId)
        {
            PreviewReportedDefectWorkOrderViewModel DefectDetails = new PreviewReportedDefectWorkOrderViewModel();
            var input = new Dictionary<string, object>()
                {
                    {"vesselId", vesselId },
                    {"dwoId", dwoId }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/DefectManager/PostGetReportedDefectWOForPreview"));
            PreviewReportedDefectWorkOrder response = await PostAsync<PreviewReportedDefectWorkOrder>(requestUrl, CreateHttpContent(input));

            if (response != null)
            {
                DefectDetails.DefectName = response.DefectName;
                DefectDetails.SystemAreaName = response.SystemAreaName;
                DefectDetails.CompletedOn = response.CompletedOn.HasValue ? response.CompletedOn.Value.ToString(Constants.DateFormat) : ""; ;
                DefectDetails.DueDate = response.DueDate.HasValue ? response.DueDate.Value.ToString(Constants.DateFormat) : "";
                DefectDetails.ConditionAfterWorkDone = response.ConditionAfterWorkDone;
                DefectDetails.ConditionBeforeWorkDone = response.ConditionBeforeWorkDone;
                DefectDetails.SymptomsObeserved = SetSymptoms(response.Symptoms);
                DefectDetails.IsAdditionalJobReported = response.IsAdditionalJobReported ? "Yes" : "No";
                DefectDetails.PartsUsed = SetPartsUsed(response);
                DefectDetails.RemarkAndFindings = response.RemarkAndFindings ?? "";
                //TODO:
                //Need to ask pallavi why show other symptoms and other observed symptoms is binded if value for them is not fetched and assigned in SS
            }

            return DefectDetails;
        }

        /// <summary>
        /// Sets the parts used.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private List<DefectReportWorkOrderPartsUsedViewModel> SetPartsUsed(PreviewReportedDefectWorkOrder result)
        {
            List<DefectReportWorkOrderPartsUsedViewModel> PartsUsed = new List<DefectReportWorkOrderPartsUsedViewModel>();
            if (result.PartsUsed != null && result.PartsUsed.Any())
            {
                foreach (DefectReportWorkOrderPartsUsed item in result.PartsUsed)
                {
                    DefectReportWorkOrderPartsUsedViewModel part = new DefectReportWorkOrderPartsUsedViewModel();
                    part.PartName = item.PartName ?? "";
                    part.MakerReferenceNumber = item.MakerReferenceNumber ?? "";
                    part.QuantityUsed = item.QuantityUsed;
                    PartsUsed.Add(part);
                }
            }

            return PartsUsed;
        }

        /// <summary>
        /// Sets the symptoms.
        /// </summary>
        /// <param name="WorkOrderSymptoms">The work order symptoms.</param>
        /// <returns></returns>
        private string SetSymptoms(List<DefectReportWorkOrderSymptom> WorkOrderSymptoms)
        {
            return (string.Join(", ", WorkOrderSymptoms.Select(symptom => symptom.SymptomDescription).ToList()));
            //ShowOtherSymptoms = WorkOrderSymptoms.Any(x => x.PwsId == EnumsHelper.GetKeyValue(.Other));

            //if (ShowOtherSymptoms)
            //{
            //    //OtherSymptomsObserved = WorkOrderSymptoms.Where(x => x.PwsId == EnumsHelper.GetKeyValue(WorkOrderSymptom.Other)).FirstOrDefault().SymptomComment;
            //}
        }

        /// <summary>
        /// Gets the wo detail for report defect.
        /// </summary>
        /// <param name="dwoId">The dwo identifier.</param>
        /// <returns></returns>
        public async Task<DefectDetailViewModel> GetWODetailForReportDefect(string dwoId)
        {
            DefectDetailViewModel defectHeader = new DefectDetailViewModel();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/DefectManager/GetWODetailForReportDefect/" + dwoId));
            DefectDetail response = await GetAsync<DefectDetail>(requestUrl);

            if (response != null && response.HeaderDetail != null)
            {
                defectHeader.DefectName = response.HeaderDetail.DefectName ?? "";
                defectHeader.DefectNumber = response.HeaderDetail.DefectNumber ?? "";
                defectHeader.Category = response.HeaderDetail.Category ?? "";
                defectHeader.StaffType = response.HeaderDetail.StaffType ?? "";
                defectHeader.SiteType = response.HeaderDetail.SiteType ?? "";
                defectHeader.Priority = response.HeaderDetail.Priority ?? "";
                defectHeader.DueDate = response.DueDate.HasValue ? response.DueDate.Value.ToString(Constants.DateFormat) : "";
                if (!string.IsNullOrWhiteSpace(response.HeaderDetail.PtrId))
                {
                    defectHeader.ComponentId = response.HeaderDetail.PtrId;
                }
                else
                {
                    defectHeader.ComponentId = response.HeaderDetail.ParentComponentId;
                    defectHeader.SystemAreaId = response.HeaderDetail.PgrId;
                }
            }

            return defectHeader;
        }

        /// <summary>
        /// Posts the get position attribute lookup.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> PostGetPosAttributeLookup()
        {
            List<PosAttributeLookupCode> input = new List<PosAttributeLookupCode>() { PosAttributeLookupCode.OffHireApplicable };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/DefectManager/PostGetPosAttributeLookup"));
            List<Lookup> response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(input));

            return response;
        }

        #endregion


        #endregion

        #region SeaPassageCall
        /// <summary>
        /// Posts the get sea passage noon details report.
        /// </summary>
        /// <param name="requestViewModel">The request view model.</param>
        /// <returns></returns>
        public async Task<NoonReportDetailsViewModel> PostGetSeaPassageNoonDetailsReport(SeaPassageReportRequestViewModel requestViewModel)
        {
            string vesselId = GetVesselId(requestViewModel.vesselId);
            string vesselName = CommonUtil.GetVesselDisplayName(_provider, requestViewModel.vesselId);

            NoonReportDetailsViewModel result = new NoonReportDetailsViewModel();
            result.SeaPassageBreaks = new List<SeaPassageBreakViewModel>();
            var value = new Dictionary<string, object>()
            {
                { "posId", requestViewModel.PosId},
                { "spaId", requestViewModel.SpaId }
            };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetNoonReportDetails"));
            NoonReportDetails response = await PostAsync<NoonReportDetails>(requestUrl, CreateHttpContent(value));
            if (response != null)
            {
                #region Loockup/Services
                List<Lookup> UTCList = await GetUtcListForDropdown();
                List<Lookup> SecurityLevel = await GetSecurityLevel();
                var SynopsisTypes = new List<Lookup>();
                var CurrentTypes = new List<Lookup>();
                var vesselDirections = await GetDirectionLookup();
                var swellHeights = await GetSwellHeightLookup();
                var swellLengths = await GetSwellLengthLookup();
                var wavePositions = await GetWavePositionLookup();
                var windForces = await GetWindForceLookup();
                var barMovements = await GetBarMovementLookups();
                var pitchAndRolls = await GetPitchAndRollLookup();
                var RouteChangedReasons = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.RouteChangeReason });
                var offHireBreakInTypes = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.BreakInPassageType, PosAttributeLookupCode.OffHireApplicable });
                await GetSynopsisAndCurrntTypes(SynopsisTypes, CurrentTypes);
                List<PosActivityType> posActivityTypes = await GetPosActivityType(EnumsHelper.GetKeyValue(PosActivityTypeLookupCode.SeaB));

                PreviousSeaPassageDetails previousSeaPassageDetails = await GetPreviousNoonReportDetail(requestViewModel.SpaId, requestViewModel.PosId, vesselId);
                #endregion

                #region Details
                NoonReportNavigationViewModel noonReportNvaigation = new NoonReportNavigationViewModel();
                noonReportNvaigation.PreviousDate = previousSeaPassageDetails.SpaDate;
                noonReportNvaigation.Date = response.NoonReportNvaigation.Date;
                noonReportNvaigation.IsNoon = response.NoonReportNvaigation.PlaId == EnumsHelper.GetKeyValue(SeaPassageActivityType.NOON);
                noonReportNvaigation.SpaId = response.NoonReportNvaigation.SpaId;

                //Details
                result.Date = GetFormattedDateTimeSting(response.NoonReportNvaigation.Date);
                result.SelectedStartUTC = UTCList != null && UTCList.Any() ? UTCList.Where(x => x.Identifier == response.NoonReportNvaigation.TimeZoneClock.ToString()).Select(x => x.Description).FirstOrDefault() : "-";
                result.CrossedIDLDays = GetCrossedIDLDays(noonReportNvaigation).ToString();
                result.SelectedStartUTC = GetDefaultValue(result.SelectedStartUTC);
                result.IsNoonIncomplete = requestViewModel.IsNoonIncomplete;

                result.NewShipTime = response.NoonReportNvaigation.NewShipTime == null ? Constants.DashForEmpty : GetDecimalDefaultValue(response.NoonReportNvaigation.NewShipTime);
                result.SpaRouteChangedYes = response.NoonReportNvaigation.SpaRouteChanged;
                result.PlkIdRouteChangeReason = RouteChangedReasons != null && RouteChangedReasons.Any() ? RouteChangedReasons.Where(x => x.Identifier == response.NoonReportNvaigation.PlkIdRouteChangeReason).Select(x => x.Description).FirstOrDefault() : "-";
                result.VesselName = vesselName;
                result.SecurityLevel = SecurityLevel != null && SecurityLevel.Any() ? SecurityLevel.Where(x => x.Identifier == response.NoonReportNvaigation.SpaSecurity).Select(x => x.Description).FirstOrDefault() : "-";
                result.CanShowIDL = requestViewModel.IsIdl;
                #endregion

                #region Navigation

                //Navigation
                noonReportNvaigation.EtaDate = GetFormattedDateTimeSting(response.NoonReportNvaigation.EtaDate); // show if IsNoon =true

                noonReportNvaigation.SpaNewLat1 = response.NoonReportNvaigation.SpaNewLat1;
                noonReportNvaigation.SpaNewLat2 = response.NoonReportNvaigation.SpaNewLat2;
                noonReportNvaigation.Lat3 = CreateLongitude(noonReportNvaigation.SpaNewLat1, noonReportNvaigation.SpaNewLat2, response.NoonReportNvaigation.Lat3);

                noonReportNvaigation.SpaNewLong1 = response.NoonReportNvaigation.SpaNewLong1;
                noonReportNvaigation.SpaNewLong2 = response.NoonReportNvaigation.SpaNewLong2;
                noonReportNvaigation.Long3 = CreateLongitude(noonReportNvaigation.SpaNewLong1, noonReportNvaigation.SpaNewLong2, response.NoonReportNvaigation.Long3);
                noonReportNvaigation.Course = GetFloatDefaultValue(response.NoonReportNvaigation.Course);

                noonReportNvaigation.DisToGr = GetFloatDefaultValue(response.NoonReportNvaigation.DisToGr);
                noonReportNvaigation.DistLog = GetFloatDefaultValue(response.NoonReportNvaigation.DistLog);
                noonReportNvaigation.SpaDisByECDIS = GetDecimalDefaultValue(response.NoonReportNvaigation.SpaDisByECDIS);
                noonReportNvaigation.DistGo = GetFloatDefaultValue(response.NoonReportNvaigation.DistGo);

                noonReportNvaigation.CharterSpeed = GetFloatDefaultValue(previousSeaPassageDetails.CharterSpeed);
                noonReportNvaigation.Speed = GetFloatDefaultValue(response.NoonReportNvaigation.Speed);
                noonReportNvaigation.SteamTime = GetDefaultValue(response.NoonReportNvaigation.SteamTime);
                noonReportNvaigation.SpeedOverGround = GetDecimalDefaultValue(response.NoonReportNvaigation.SpeedOverGround);

                float? ActualPrevDistanceTravelled = previousSeaPassageDetails.DistanceTravelled.HasValue ? previousSeaPassageDetails.DistanceTravelled.GetValueOrDefault() : default(float?);
                noonReportNvaigation.TotalDistance = CalculateTotalDistanceOnPassage(response.NoonReportNvaigation.DisToGr, ActualPrevDistanceTravelled);
                noonReportNvaigation.TotalTimeSailed = UpdatedTotalTimeSailed(response.NoonReportNvaigation.SteamD, previousSeaPassageDetails.TotalTimeSailedD);
                noonReportNvaigation.TotalTimeSailedD = (previousSeaPassageDetails.TotalTimeSailedD ?? 0) + (response.NoonReportNvaigation.SteamD ?? 0);
                noonReportNvaigation.TotalAvgSpeed = CalculateTotalAvgSpeed(noonReportNvaigation.TotalTimeSailedD, noonReportNvaigation.TotalDistance);

                noonReportNvaigation.EcaEntryDate = GetFormattedDateTimeSting(response.NoonReportNvaigation.EcaEntryDate);
                noonReportNvaigation.EcaExitDate = GetFormattedDateTimeSting(response.NoonReportNvaigation.EcaExitDate);
                noonReportNvaigation.SpaFuelChangeOverStartDateTime = GetFormattedDateTimeSting(response.NoonReportNvaigation.SpaFuelChangeOverStartDateTime);
                noonReportNvaigation.SpaFuelChangeOverEndDateTime = GetFormattedDateTimeSting(response.NoonReportNvaigation.SpaFuelChangeOverEndDateTime);

                noonReportNvaigation.BreakTime = GetDefaultValue(response.NoonReportNvaigation.BreakTime);
                noonReportNvaigation.BreakDist = GetFloatDefaultValue(OnGetTotalBreakDist(response.SeaPassageBreaks));
                noonReportNvaigation.OutServTime = GetDefaultValue(response.NoonReportNvaigation.OutServTime);
                noonReportNvaigation.SpaComment = GetDefaultValue(response.NoonReportNvaigation.SpaComment);
                result.NoonReportNvaigation = noonReportNvaigation;
                #endregion

                #region Noon Synopsis
                result.NoonSynopsis = new List<SeapassageNoonSynopsisViewModel>();
                if (response.NoonSynopsis != null && response.NoonSynopsis.Any())
                {
                    foreach (var item in response.NoonSynopsis)
                    {
                        SeapassageNoonSynopsisViewModel _noonSynopsis = new SeapassageNoonSynopsisViewModel
                        {
                            FromDate = GetFormattedDateTimeSting(item.FromDate),
                            ToDate = GetFormattedDateTimeSting(item.ToDate),
                            TimeDifference = item.TimeDifference,
                            SelectedSynopsisType = SynopsisTypes != null && SynopsisTypes.Any() ? SynopsisTypes.Where(x => x.Identifier == item.TypeId).Select(x => x.Description).FirstOrDefault() : "-",
                            AvgSpeed = item.AvgSpeed,
                            PlkIdSignificantCurrent = CurrentTypes != null && CurrentTypes.Any() ? CurrentTypes.Where(x => x.Identifier == item.PlkIdSignificantCurrent).Select(x => x.Description).FirstOrDefault() : "-",
                            Effect = item.Effect,
                            Comment = item.Comment
                        };

                        result.NoonSynopsis.Add(_noonSynopsis);
                    }
                }
                #endregion

                #region SeaPassageBreaks
                if (response.SeaPassageBreaks != null && response.SeaPassageBreaks.Any())
                {
                    foreach (var item in response.SeaPassageBreaks)
                    {
                        SeaPassageBreakViewModel _seaPassageBreak = new SeaPassageBreakViewModel();
                        _seaPassageBreak.PlkIdBreakInPassageType = item.PlkIdBreakInPassageType;
                        _seaPassageBreak.Type = offHireBreakInTypes != null && offHireBreakInTypes.Any() ? offHireBreakInTypes.Where(x => x.Identifier == item.PlkIdBreakInPassageType).Select(x => x.Description).FirstOrDefault() : "-";
                        _seaPassageBreak.From = item.From;
                        _seaPassageBreak.To = item.To;
                        _seaPassageBreak.BreakTimeD = item.SpbOutServ;
                        _seaPassageBreak.BreakTime = GetTimeSpanFromString(item.SpbOutServ);
                        _seaPassageBreak.Reason = posActivityTypes != null && posActivityTypes.Any() ? posActivityTypes.Where(x => x.Id == item.PlaId).Select(x => x.Description).FirstOrDefault() : "-";
                        _seaPassageBreak.Fo = item.Fo;
                        _seaPassageBreak.Lsfo = item.Lsfo;
                        _seaPassageBreak.Do = item.Do;
                        _seaPassageBreak.Go = item.Go;
                        _seaPassageBreak.Lng = item.Lng;
                        _seaPassageBreak.SpbLngcargo = item.SpbLngcargo;
                        _seaPassageBreak.OutDistance = item.OutDistance;
                        _seaPassageBreak.IsOutOfService = item.IsOutOfService;
                        _seaPassageBreak.OffHireType = offHireBreakInTypes != null && offHireBreakInTypes.Any() ? offHireBreakInTypes.Where(x => x.Identifier == item.PlkIdOffHireType).Select(x => x.Description).FirstOrDefault() : "-";
                        _seaPassageBreak.Comments = item.Comments;

                        result.SeaPassageBreaks.Add(_seaPassageBreak);
                    }
                }

                TimeSpan stoppageTime = GetStoppageTimeFromBreaks(result.SeaPassageBreaks);
                string businessTime = CalculateBusinessTime(GetTimeSpanFromString(response.NoonReportNvaigation.SteamTime) + stoppageTime);
                result.BusinessTime = businessTime;
                #endregion

                #region NoonReportWeather
                NoonReportWeatherViewModel NoonReportWeather = new NoonReportWeatherViewModel();
                NoonReportWeather.SwellDir = vesselDirections != null && vesselDirections.Any() ? vesselDirections.Where(x => x.Id == response.NoonReportWeather.SwellDir).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SwellHeig = swellHeights != null && swellHeights.Any() ? swellHeights.Where(x => x.Id == response.NoonReportWeather.SwellHeig).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SwellLen = swellLengths != null && swellLengths.Any() ? swellLengths.Where(x => x.Id == response.NoonReportWeather.SwellLen).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SpaSeaTemp = response.NoonReportWeather.SpaSeaTemp;
                NoonReportWeather.SpaSeaDir = vesselDirections != null && vesselDirections.Any() ? vesselDirections.Where(x => x.Id == response.NoonReportWeather.SpaSeaDir).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SpaSeaHeig = swellHeights != null && swellHeights.Any() ? swellHeights.Where(x => x.Id == response.NoonReportWeather.SpaSeaHeig).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SpaSeaState = wavePositions != null && wavePositions.Any() ? wavePositions.Where(x => x.Identifier == response.NoonReportWeather.SpaSeaState).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.WindDir = vesselDirections != null && vesselDirections.Any() ? vesselDirections.Where(x => x.Id == response.NoonReportWeather.WindDir).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.WindSpeed = response.NoonReportWeather.WindSpeed;
                NoonReportWeather.SpaTrueWindDirection = response.NoonReportWeather.SpaTrueWindDirection;
                NoonReportWeather.SpaTrueWindSpeed = response.NoonReportWeather.SpaTrueWindSpeed;
                NoonReportWeather.WindForce = windForces != null && windForces.Any() ? windForces.Where(x => x.Id == response.NoonReportWeather.WindForce).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.WaveDir = wavePositions != null && wavePositions.Any() ? wavePositions.Where(x => x.Identifier == response.NoonReportWeather.WaveDir).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SpaCurrent = vesselDirections != null && vesselDirections.Any() ? vesselDirections.Where(x => x.Id == response.NoonReportWeather.SpaCurrent).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SpaAirTemp = response.NoonReportWeather.SpaAirTemp;
                NoonReportWeather.SpaBarMovement = barMovements != null && barMovements.Any() ? barMovements.Where(x => x.Identifier == response.NoonReportWeather.SpaBarMovement).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SpaPitchRoll = pitchAndRolls != null && pitchAndRolls.Any() ? pitchAndRolls.Where(x => x.Id == response.NoonReportWeather.SpaPitchRoll).Select(x => x.Description).FirstOrDefault() : "-";
                NoonReportWeather.SpaBadWeatherHrs = response.NoonReportWeather.SpaBadWeatherHrs == null ? Constants.DashForEmpty : Convert.ToInt32(response.NoonReportWeather.SpaBadWeatherHrs).ToString();
                NoonReportWeather.SpaBadWeatherDist = response.NoonReportWeather.SpaBadWeatherDist == null ? Constants.DashForEmpty : Convert.ToInt32(response.NoonReportWeather.SpaBadWeatherDist).ToString();
                NoonReportWeather.SpaBarPressure = response.NoonReportWeather.SpaBarPressure;
                //NoonReportWeather.SpeedOverGround = response.NoonReportWeather.SpeedOverGround;
                //NoonReportWeather.Description = response.NoonReportWeather.Description;
                //NoonReportWeather.WindDirInDegs = response.NoonReportWeather.WindDirInDegs;
                NoonReportWeather.SpaTrueWindDirection = response.NoonReportWeather.SpaTrueWindDirection;
                result.NoonReportWeather = NoonReportWeather;
                #endregion

                #region NoonReportVesselDraft
                //noonReportVesselDraft
                NoonReportVesselDraftViewModel noonReportVesselDraft = new NoonReportVesselDraftViewModel();
                noonReportVesselDraft.SpaDrftFwdD = GetFloatDefaultValue(response.NoonReportVesselDraft.SpaDrftFwdD);
                noonReportVesselDraft.SpaDraftMidD = GetDecimalDefaultValue(response.NoonReportVesselDraft.SpaDraftMidD);
                noonReportVesselDraft.SpaDrftAftD = GetFloatDefaultValue(response.NoonReportVesselDraft.SpaDrftAftD);
                noonReportVesselDraft.SpaDraftMeanD = GetDecimalDefaultValue(response.NoonReportVesselDraft.SpaDraftMeanD);
                result.NoonReportVesselDraft = noonReportVesselDraft;
                #endregion

                #region NoonpowerOutput
                //Power Output
                NoonReportSlipAndPowerOutputViewModel noonpowerOutput = new NoonReportSlipAndPowerOutputViewModel();
                var VesselMainEngineMCR = previousSeaPassageDetails != null ? previousSeaPassageDetails.MCR : null;
                noonpowerOutput.PowerOutputMe = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.PowerOutputMe);
                noonpowerOutput.MainEngineLoad = GetDecimalDefaultValue(response.NoonReportSlipAndPowerOutput.MainEngineLoad);
                noonpowerOutput.MainEngineMCR = GetFloatDefaultValue(SetMainEngineMCR(response.NoonReportSlipAndPowerOutput.PowerOutputMe, VesselMainEngineMCR));
                noonpowerOutput.PowerOutputDieselEng = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.PowerOutputDieselEng);
                noonpowerOutput.PowerOutputTurbGen = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.PowerOutputTurbGen);
                noonpowerOutput.PowerOutputShaftGen = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.PowerOutputShaftGen);
                noonpowerOutput.CargoReefer = GetDecimalDefaultValue(response.NoonReportSlipAndPowerOutput.CargoReefer);
                noonpowerOutput.SpaSfoc = GetDecimalDefaultValue(response.NoonReportSlipAndPowerOutput.SpaSfoc);
                #endregion

                #region NoonSlips
                //slips
                noonpowerOutput.TotRev2 = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.TotRev2);
                noonpowerOutput.TotRev1 = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.TotRev1);
                noonpowerOutput.CounterRollover = (response.NoonReportSlipAndPowerOutput.CounterRollover ?? false) ? Constants.Yes : Constants.No;
                noonpowerOutput.CounterRolloverAt = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.CounterRolloverAt);
                noonpowerOutput.TrueSlip = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.TrueSlip);
                noonpowerOutput.ObsSlip = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.ObsSlip);
                noonpowerOutput.Rpm = GetFloatDefaultValue(response.NoonReportSlipAndPowerOutput.Rpm);
                result.NoonReportSlipAndPowerOutput = noonpowerOutput;
                #endregion

                #region FreshWater
                //freshWater
                var freshWater = response.NoonReportFreshWaterLubeOil;
                ROBDetailsViewModel freshWaterDomestic = new ROBDetailsViewModel();


                freshWaterDomestic.TankCapacity = (decimal?)previousSeaPassageDetails.FWDomesticCapacity;
                freshWaterDomestic.PreviousROB = previousSeaPassageDetails.FwRob.HasValue ? previousSeaPassageDetails.FwRob : 0;
                freshWaterDomestic.ProductionQty = (decimal?)freshWater.SpaFwGener;
                freshWaterDomestic.ConsumptionQty = (decimal?)freshWater.SpaFwCons;
                freshWaterDomestic.ROB = (!freshWater.SpaFwRob.HasValue) ? freshWaterDomestic.PreviousROB : freshWater.SpaFwRob;
                freshWaterDomestic.Percentage = CalculateROBPercentage(freshWaterDomestic.TankCapacity, freshWaterDomestic.ROB);
                result.FreshWaterDomestic = freshWaterDomestic;

                ROBDetailsViewModel freshWaterTechnical = new ROBDetailsViewModel();
                freshWaterTechnical.TankCapacity = (decimal?)previousSeaPassageDetails.FWTechnicalCapacity;
                freshWaterTechnical.PreviousROB = previousSeaPassageDetails.FwTech.HasValue ? (decimal?)previousSeaPassageDetails.FwTech : 0;
                freshWaterTechnical.ProductionQty = (decimal?)freshWater.SpaFwTechGener;
                freshWaterTechnical.ConsumptionQty = (decimal?)freshWater.SpaFwTechCons;
                freshWaterTechnical.ROB = (!freshWater.SpaFwTechRob.HasValue) ? freshWaterTechnical.PreviousROB : (decimal?)freshWater.SpaFwTechRob;
                freshWaterTechnical.Percentage = CalculateROBPercentage(freshWaterTechnical.TankCapacity, freshWaterTechnical.ROB);
                result.FreshWaterTechnical = freshWaterTechnical;
                #endregion

                bool isEntityCreated = string.IsNullOrWhiteSpace(freshWater.SpaId);
                #region lubeOil
                //lubeOil
                ROBDetailsViewModel lubeOilClo = new ROBDetailsViewModel();
                lubeOilClo.TankCapacity = (decimal?)previousSeaPassageDetails.CLOLuboilCapacity;
                lubeOilClo.PreviousROB = previousSeaPassageDetails.ClyLubeOilRob.GetValueOrDefault();
                lubeOilClo.ProductionQty = (freshWater.SpaClyLubeOilProduction);
                lubeOilClo.ConsumptionQty = (freshWater.SpaClyLubeOilConsumption);
                lubeOilClo.ROB = (!freshWater.SpaClyLubeOilRob.HasValue) ? lubeOilClo.PreviousROB : freshWater.SpaClyLubeOilRob;
                lubeOilClo.Percentage = CalculateROBPercentage(lubeOilClo.TankCapacity, lubeOilClo.ROB);
                result.LubeOilClo = lubeOilClo;

                ROBDetailsViewModel lubeOilCrankCase = new ROBDetailsViewModel();
                lubeOilCrankCase.TankCapacity = (decimal?)previousSeaPassageDetails.CrankLuboilCapacity;
                lubeOilCrankCase.PreviousROB = previousSeaPassageDetails.CrankCaseRob.GetValueOrDefault();
                lubeOilCrankCase.ProductionQty = (freshWater.SpaCrankCaseProdcution);
                lubeOilCrankCase.ConsumptionQty = (freshWater.SpaCrankCaseConsumption);
                lubeOilCrankCase.ROB = !freshWater.SpaCrankCaseRob.HasValue ? lubeOilCrankCase.PreviousROB : freshWater.SpaCrankCaseRob;
                lubeOilCrankCase.Percentage = CalculateROBPercentage(lubeOilCrankCase.TankCapacity, lubeOilCrankCase.ROB);
                result.LubeOilCrankCase = lubeOilCrankCase;

                ROBDetailsViewModel lubeOilAux = new ROBDetailsViewModel();
                lubeOilAux.TankCapacity = (decimal?)previousSeaPassageDetails.AuxLuboilCapacity;
                lubeOilAux.PreviousROB = previousSeaPassageDetails.AuxLubOilRob.GetValueOrDefault();
                lubeOilAux.ProductionQty = (freshWater.SpaAuxLubeOilProduction);
                lubeOilAux.ConsumptionQty = (freshWater.SpaAuxLubeOilConsumption);
                lubeOilAux.ROB = !freshWater.SpaAuxLubeOilRob.HasValue ? lubeOilAux.PreviousROB : freshWater.SpaAuxLubeOilRob;
                lubeOilAux.Percentage = CalculateROBPercentage(lubeOilAux.TankCapacity, lubeOilAux.ROB);
                result.LubeOilAux = lubeOilAux;

                ROBDetailsViewModel lubeOilGeneral = new ROBDetailsViewModel();
                lubeOilGeneral.TankCapacity = (decimal?)previousSeaPassageDetails.GeneralLuboilCapacity;
                lubeOilGeneral.PreviousROB = previousSeaPassageDetails.GeneralLubOilRob.GetValueOrDefault();
                lubeOilGeneral.ProductionQty = freshWater.SpaGeneralLubeOilProduction;
                lubeOilGeneral.ConsumptionQty = freshWater.SpaGeneralLubeOilConsumption;
                lubeOilGeneral.ROB = !freshWater.SpaGeneralLubeOilRob.HasValue ? lubeOilGeneral.PreviousROB : freshWater.SpaGeneralLubeOilRob;
                lubeOilGeneral.Percentage = CalculateROBPercentage(lubeOilGeneral.TankCapacity, lubeOilGeneral.ROB);
                result.LubeOilGeneral = lubeOilGeneral;
                #endregion

                #region Waster Rob
                //Waster Rob
                var wasterRob = response.NoonReportConsumptionCapacityRob;
                ROBDetailsViewModel wasterRobSludge = new ROBDetailsViewModel();
                wasterRobSludge.TankCapacity = isEntityCreated ? (decimal?)previousSeaPassageDetails.SludgeCapacity : wasterRob.SpaCapacitySludge.HasValue ? wasterRob.SpaCapacitySludge : (decimal?)previousSeaPassageDetails.SludgeCapacity;
                wasterRobSludge.PreviousROB = previousSeaPassageDetails.SludgeRob.HasValue ? previousSeaPassageDetails.SludgeRob : 0;
                wasterRobSludge.ProductionQty = wasterRob.SpaSludgeProductionQty;
                wasterRobSludge.ConsumptionQty = ((previousSeaPassageDetails.SludgeRob.HasValue ? previousSeaPassageDetails.SludgeRob : 0) + wasterRob.SpaSludgeProductionQty.GetValueOrDefault()) - wasterRob.SpaRobsludge;
                wasterRobSludge.ConsumptionQty = wasterRobSludge.ConsumptionQty.GetValueOrDefault() <= 0 ? default(decimal?) : wasterRobSludge.ConsumptionQty;
                wasterRobSludge.ROB = (isEntityCreated || !wasterRob.SpaRobsludge.HasValue) ? wasterRobSludge.PreviousROB : wasterRob.SpaRobsludge;
                wasterRobSludge.Percentage = CalculateROBPercentage(wasterRobSludge.TankCapacity, wasterRobSludge.ROB);
                result.WasterRobSludge = wasterRobSludge;

                ROBDetailsViewModel wasterRobBilge = new ROBDetailsViewModel();
                wasterRobBilge.TankCapacity = isEntityCreated ? (decimal?)previousSeaPassageDetails.BilgeCapacity : wasterRob.SpaCapacityBilge.HasValue ? wasterRob.SpaCapacityBilge : (decimal?)previousSeaPassageDetails.BilgeCapacity;
                wasterRobBilge.PreviousROB = previousSeaPassageDetails.BilgeRob.HasValue ? previousSeaPassageDetails.BilgeRob : 0;
                wasterRobBilge.ProductionQty = wasterRob.SpaBilgeProductionQty;
                wasterRobBilge.ConsumptionQty = ((previousSeaPassageDetails.BilgeRob.HasValue ? previousSeaPassageDetails.BilgeRob : 0) + wasterRob.SpaBilgeProductionQty.GetValueOrDefault()) - wasterRob.SpaRobbilge;
                wasterRobBilge.ConsumptionQty = wasterRobBilge.ConsumptionQty.GetValueOrDefault() <= 0 ? default(decimal?) : wasterRobBilge.ConsumptionQty;
                wasterRobBilge.ROB = (isEntityCreated || !wasterRob.SpaRobbilge.HasValue) ? wasterRobBilge.PreviousROB : wasterRob.SpaRobbilge;
                wasterRobBilge.Percentage = CalculateROBPercentage(wasterRobBilge.TankCapacity, wasterRobBilge.ROB);
                result.WasterRobBilge = wasterRobBilge;

                ROBDetailsViewModel wasterRobSlops = new ROBDetailsViewModel();
                wasterRobSlops.TankCapacity = isEntityCreated ? (decimal?)previousSeaPassageDetails.SlopCapacity : wasterRob.SpaCapacitySlops.HasValue ? wasterRob.SpaCapacitySlops : (decimal?)previousSeaPassageDetails.SlopCapacity;
                wasterRobSlops.PreviousROB = previousSeaPassageDetails.SlopRob.HasValue ? previousSeaPassageDetails.SlopRob : 0;
                wasterRobSlops.ProductionQty = wasterRob.SpaSlopProductionQty;
                wasterRobSlops.ConsumptionQty = ((previousSeaPassageDetails.SlopRob.HasValue ? previousSeaPassageDetails.SlopRob : 0) + wasterRob.SpaSlopProductionQty.GetValueOrDefault()) - wasterRob.SpaRobslops;
                wasterRobSlops.ConsumptionQty = wasterRobSlops.ConsumptionQty.GetValueOrDefault() <= 0 ? default(decimal?) : wasterRobSlops.ConsumptionQty;
                wasterRobSlops.ROB = (isEntityCreated || !wasterRob.SpaRobslops.HasValue) ? wasterRobSlops.PreviousROB : wasterRob.SpaRobslops;
                wasterRobSlops.Percentage = CalculateROBPercentage(wasterRobSlops.TankCapacity, wasterRobSlops.ROB);
                result.WasterRobSlops = wasterRobSlops;

                ROBDetailsViewModel wasterRobSewage = new ROBDetailsViewModel();
                wasterRobSewage.TankCapacity = isEntityCreated ? (decimal?)previousSeaPassageDetails.SewageCapacity : wasterRob.SpaCapacitySewage.HasValue ? wasterRob.SpaCapacitySewage : (decimal?)previousSeaPassageDetails.SewageCapacity;
                wasterRobSewage.PreviousROB = previousSeaPassageDetails.SewageRob.HasValue ? previousSeaPassageDetails.SewageRob : 0;
                wasterRobSewage.ProductionQty = wasterRob.SpaSewageProductionQty;
                wasterRobSewage.ConsumptionQty = ((previousSeaPassageDetails.SewageRob.HasValue ? previousSeaPassageDetails.SewageRob : 0) + wasterRob.SpaSewageProductionQty.GetValueOrDefault()) - wasterRob.SpaRobsewage;
                wasterRobSewage.ConsumptionQty = wasterRobSewage.ConsumptionQty.GetValueOrDefault() <= 0 ? default(decimal?) : wasterRobSewage.ConsumptionQty;
                wasterRobSewage.ROB = (isEntityCreated || !wasterRob.SpaRobslops.HasValue) ? wasterRobSewage.PreviousROB : wasterRob.SpaRobsewage;
                wasterRobSewage.Percentage = CalculateROBPercentage(wasterRobSewage.TankCapacity, wasterRobSewage.ROB);
                result.WasterRobSewage = wasterRobSewage;
                #endregion

                #region Running hours Power developed
                //Running hours/ Power developed
                result.VoyageRunningHourList = new List<VoyageRunningHourViewModel>();
                if (response.VoyageRunningHourList != null && response.VoyageRunningHourList.Any())
                {
                    foreach (var item in response.VoyageRunningHourList)
                    {
                        VoyageRunningHourViewModel _voyageRunningHour = new VoyageRunningHourViewModel();
                        _voyageRunningHour.PartName = item.PartName;
                        _voyageRunningHour.Previous = item.Previous;
                        _voyageRunningHour.Daily = item.Daily;
                        _voyageRunningHour.Total = item.Total;
                        _voyageRunningHour.PowerOutput = item.PowerOutput;
                        result.VoyageRunningHourList.Add(_voyageRunningHour);
                    }
                }
                #endregion

                #region Fuel ROB
                //Fuel ROB
                result.FuelRobItems = new List<FuelROBDetailsViewModel>();

                var fuelRob = response.FuelRob;
                FuelROBDetailsViewModel foFuelROB = new FuelROBDetailsViewModel();
                foFuelROB.Title = Constants.FO;
                foFuelROB.TankCapacity = previousSeaPassageDetails != null ? previousSeaPassageDetails.FoCapacity : null;
                foFuelROB.PreviousROB = previousSeaPassageDetails != null && previousSeaPassageDetails.Fo.HasValue ? previousSeaPassageDetails.Fo : 0.0f;
                foFuelROB.IsTypeCollapsed = foFuelROB.PreviousROB.GetValueOrDefault() <= 0;
                foFuelROB.Sulphur = GetFloatDefaultValue(fuelRob.FoSulphur);
                foFuelROB.Boiler = GetFloatDefaultValue(fuelRob.FoBoiler);
                foFuelROB.BreakConsumption = GetFloatDefaultValue(fuelRob.FoMainEngineBreak);
                foFuelROB.Cargo = GetFloatDefaultValue(fuelRob.FoCargoHeat);
                foFuelROB.DGConsumption = GetFloatDefaultValue(fuelRob.FoConsumption);
                foFuelROB.MEConsumption = GetFloatDefaultValue(fuelRob.FoMainEngine);
                foFuelROB.DeBallast = GetFloatDefaultValue(fuelRob.SpaFoDeball);
                foFuelROB.IGS = GetFloatDefaultValue(fuelRob.SpaFoInert);
                foFuelROB.TankCleaning = GetFloatDefaultValue(fuelRob.SpaFoTnkClean);
                foFuelROB.Other = GetFloatDefaultValue(fuelRob.FoOther);
                foFuelROB.TotalROB = isEntityCreated ? foFuelROB.PreviousROB : fuelRob.FoRob;
                foFuelROB.Percentage = CalculateFuelRobPercentage(foFuelROB.TotalROB, foFuelROB.TankCapacity);
                foFuelROB.FreeCapacity = foFuelROB.TankCapacity.GetValueOrDefault() - foFuelROB.TotalROB.GetValueOrDefault();
                foFuelROB.TotalConsumption = CalculateConsumptionQty(foFuelROB);

                FuelROBDetailsViewModel lsfoFuelROB = new FuelROBDetailsViewModel();
                lsfoFuelROB.Title = Constants.LSFO;
                lsfoFuelROB.TankCapacity = previousSeaPassageDetails != null ? previousSeaPassageDetails.LsfoCapacity : null;
                lsfoFuelROB.PreviousROB = previousSeaPassageDetails != null && previousSeaPassageDetails.Lsfo.HasValue ? previousSeaPassageDetails.Lsfo : 0.0f;
                lsfoFuelROB.IsTypeCollapsed = lsfoFuelROB.PreviousROB.GetValueOrDefault() <= 0;
                lsfoFuelROB.Sulphur = GetFloatDefaultValue(fuelRob.LsfoSulphur);
                lsfoFuelROB.Boiler = GetFloatDefaultValue(fuelRob.LsfoBoiler);
                lsfoFuelROB.BreakConsumption = GetFloatDefaultValue(fuelRob.LsfoMainEngineBreak);
                lsfoFuelROB.Cargo = GetFloatDefaultValue(fuelRob.LsfoCargoHeat);
                lsfoFuelROB.DGConsumption = GetFloatDefaultValue(fuelRob.LsfoConsumption);
                lsfoFuelROB.MEConsumption = GetFloatDefaultValue(fuelRob.LsfoMainEngine);
                lsfoFuelROB.DeBallast = GetFloatDefaultValue((float?)fuelRob.SpaLsfoDeball);
                lsfoFuelROB.IGS = GetFloatDefaultValue((float?)fuelRob.SpaLsfoInert);
                lsfoFuelROB.TankCleaning = GetFloatDefaultValue((float?)fuelRob.SpaLsfoTnkClean);
                lsfoFuelROB.Other = GetFloatDefaultValue(fuelRob.LsfoOther);
                lsfoFuelROB.TotalROB = isEntityCreated ? lsfoFuelROB.PreviousROB : fuelRob.LsfoRob;
                lsfoFuelROB.Percentage = CalculateFuelRobPercentage(lsfoFuelROB.TotalROB, lsfoFuelROB.TankCapacity);
                lsfoFuelROB.FreeCapacity = lsfoFuelROB.TankCapacity.GetValueOrDefault() - lsfoFuelROB.TotalROB.GetValueOrDefault();
                lsfoFuelROB.TotalConsumption = CalculateConsumptionQty(lsfoFuelROB);

                FuelROBDetailsViewModel doFuelROB = new FuelROBDetailsViewModel();
                doFuelROB.Title = Constants.DO;
                doFuelROB.TankCapacity = previousSeaPassageDetails.DoCapacity;
                doFuelROB.PreviousROB = previousSeaPassageDetails.Do.HasValue ? previousSeaPassageDetails.Do : 0.0f;
                doFuelROB.IsTypeCollapsed = doFuelROB.PreviousROB.GetValueOrDefault() <= 0;
                doFuelROB.Sulphur = GetFloatDefaultValue(fuelRob.DoSulphur);
                doFuelROB.Boiler = GetFloatDefaultValue(fuelRob.DoBoiler);
                doFuelROB.BreakConsumption = GetFloatDefaultValue(fuelRob.DoMainEngineBreak);
                doFuelROB.Cargo = GetFloatDefaultValue(fuelRob.DoCargoHeat);
                doFuelROB.DGConsumption = GetFloatDefaultValue(fuelRob.DoConsumption);
                doFuelROB.MEConsumption = GetFloatDefaultValue(fuelRob.DoMainEngine);
                doFuelROB.DeBallast = GetFloatDefaultValue(fuelRob.SpaDoDeball);
                doFuelROB.IGS = GetFloatDefaultValue(fuelRob.SpaDoInert);
                doFuelROB.TankCleaning = GetFloatDefaultValue(fuelRob.SpaDoTnkClean);
                doFuelROB.Other = GetFloatDefaultValue(fuelRob.DoOther);
                doFuelROB.TotalROB = isEntityCreated ? doFuelROB.PreviousROB : fuelRob.DoRob;
                doFuelROB.Percentage = CalculateFuelRobPercentage(doFuelROB.TotalROB, doFuelROB.TankCapacity);
                doFuelROB.FreeCapacity = doFuelROB.TankCapacity.GetValueOrDefault() - doFuelROB.TotalROB.GetValueOrDefault();
                doFuelROB.TotalConsumption = CalculateConsumptionQty(doFuelROB);

                FuelROBDetailsViewModel goFuelROB = new FuelROBDetailsViewModel();
                goFuelROB.Title = Constants.GO;
                goFuelROB.TankCapacity = previousSeaPassageDetails.GoCapacity;
                goFuelROB.PreviousROB = previousSeaPassageDetails.Go.HasValue ? previousSeaPassageDetails.Go : 0.0f;
                goFuelROB.IsTypeCollapsed = goFuelROB.PreviousROB.GetValueOrDefault() <= 0;
                goFuelROB.Sulphur = GetFloatDefaultValue(fuelRob.GoSulphur);
                goFuelROB.Boiler = GetFloatDefaultValue(fuelRob.GoBoiler);
                goFuelROB.BreakConsumption = GetFloatDefaultValue(fuelRob.GoMainEngineBreak);
                goFuelROB.Cargo = GetFloatDefaultValue(fuelRob.GoCargoHeat);
                goFuelROB.DGConsumption = GetFloatDefaultValue(fuelRob.GoConsumption);
                goFuelROB.MEConsumption = GetFloatDefaultValue(fuelRob.GoMainEngine);
                goFuelROB.DeBallast = GetFloatDefaultValue((float?)fuelRob.SpaGoDeball);
                goFuelROB.IGS = GetFloatDefaultValue((float?)fuelRob.SpaGoInert);
                goFuelROB.TankCleaning = GetFloatDefaultValue((float?)fuelRob.SpaGoTnkClean);
                goFuelROB.Other = GetFloatDefaultValue(fuelRob.GoOther);
                goFuelROB.TotalROB = isEntityCreated ? goFuelROB.PreviousROB : fuelRob.GoRob;
                goFuelROB.Percentage = CalculateFuelRobPercentage(goFuelROB.TotalROB, goFuelROB.TankCapacity);
                goFuelROB.FreeCapacity = goFuelROB.TankCapacity.GetValueOrDefault() - goFuelROB.TotalROB.GetValueOrDefault();
                goFuelROB.TotalConsumption = CalculateConsumptionQty(goFuelROB);

                FuelROBDetailsViewModel lngBunkerFuelROB = new FuelROBDetailsViewModel();
                lngBunkerFuelROB.Title = Constants.BunkerLNG;
                lngBunkerFuelROB.TankCapacity = previousSeaPassageDetails.LngCapacity;
                lngBunkerFuelROB.PreviousROB = previousSeaPassageDetails.Lng.HasValue ? (float?)previousSeaPassageDetails.Lng : 0.0f;
                lngBunkerFuelROB.IsTypeCollapsed = lngBunkerFuelROB.PreviousROB.GetValueOrDefault() <= 0;
                lngBunkerFuelROB.Boiler = GetFloatDefaultValue((float?)fuelRob.LngBoiler);
                lngBunkerFuelROB.BreakConsumption = GetFloatDefaultValue((float?)fuelRob.LngMainEngineBreak);
                lngBunkerFuelROB.Cargo = GetFloatDefaultValue((float?)fuelRob.LngCargoHeat);
                lngBunkerFuelROB.DGConsumption = GetFloatDefaultValue((float?)fuelRob.LngConsumption);
                lngBunkerFuelROB.MEConsumption = GetFloatDefaultValue((float?)fuelRob.LngMainEngine);
                lngBunkerFuelROB.DeBallast = GetFloatDefaultValue((float?)fuelRob.SpaLngDeball);
                lngBunkerFuelROB.IGS = GetFloatDefaultValue((float?)fuelRob.SpaLngInert);
                lngBunkerFuelROB.TankCleaning = GetFloatDefaultValue((float?)fuelRob.SpaLngTnkClean);
                lngBunkerFuelROB.Other = GetFloatDefaultValue((float?)fuelRob.LngOther);
                lngBunkerFuelROB.TotalROB = isEntityCreated ? lngBunkerFuelROB.PreviousROB : (float?)fuelRob.LngRob;
                lngBunkerFuelROB.IsSulphurReadOnly = true;
                lngBunkerFuelROB.Percentage = CalculateFuelRobPercentage(lngBunkerFuelROB.TotalROB, lngBunkerFuelROB.TankCapacity);
                lngBunkerFuelROB.FreeCapacity = lngBunkerFuelROB.TankCapacity.GetValueOrDefault() - lngBunkerFuelROB.TotalROB.GetValueOrDefault();
                lngBunkerFuelROB.TotalConsumption = CalculateConsumptionQty(lngBunkerFuelROB);

                FuelROBDetailsViewModel lngCargoFuelROB = new FuelROBDetailsViewModel();
                lngCargoFuelROB.Title = Constants.CargoLNG;
                lngCargoFuelROB.Boiler = GetFloatDefaultValue((float?)fuelRob.LngCargoBoiler);
                lngCargoFuelROB.BreakConsumption = GetFloatDefaultValue((float?)fuelRob.LngCargoMainEngineBreak);
                lngCargoFuelROB.Cargo = GetFloatDefaultValue((float?)fuelRob.LngCargoHeat);
                lngCargoFuelROB.DGConsumption = GetFloatDefaultValue((float?)fuelRob.LngCargoConsumption);
                lngCargoFuelROB.MEConsumption = GetFloatDefaultValue((float?)fuelRob.LngCargoMainEngine);
                lngCargoFuelROB.DeBallast = GetFloatDefaultValue((float?)fuelRob.SpaLngCargoDeball);
                lngCargoFuelROB.IGS = GetFloatDefaultValue((float?)fuelRob.SpaLngCargoInert);
                lngCargoFuelROB.TankCleaning = GetFloatDefaultValue((float?)fuelRob.SpaLngCargoTnkClean);
                lngCargoFuelROB.Other = GetFloatDefaultValue((float?)fuelRob.LngCargoOther);
                lngCargoFuelROB.IsSulphurReadOnly = true;
                lngCargoFuelROB.Percentage = CalculateFuelRobPercentage(lngCargoFuelROB.TotalROB, lngCargoFuelROB.TankCapacity);
                lngCargoFuelROB.FreeCapacity = lngCargoFuelROB.TankCapacity.GetValueOrDefault() - lngCargoFuelROB.TotalROB.GetValueOrDefault();
                lngCargoFuelROB.TotalConsumption = CalculateConsumptionQty(lngCargoFuelROB);

                result.FuelRobItems.AddRange(new List<FuelROBDetailsViewModel>
                {
                    foFuelROB,
                    lsfoFuelROB,
                    doFuelROB,
                    goFuelROB,
                    lngBunkerFuelROB,
                    lngCargoFuelROB
                });
                #endregion

                #region Noon Comments
                result.NoonComments = new List<NoonReportCommentViewModel>();
                if (response.NoonComments != null && response.NoonComments.Any())
                {
                    var voyageAttributes = new List<VoyageAttributeLookupCode> { VoyageAttributeLookupCode.NoonCommentCategory };
                    var VoyageAttributtes = await GetVoyageAttributes(voyageAttributes);
                    //var CategoryReasons = await GetNoonCommentCategoryReasons("");
                    foreach (var item in response.NoonComments)
                    {
                        //var CategoryReasons = await GetNoonCommentCategoryReasons(item.PlkIdCommentCategory);
                        NoonReportCommentViewModel comment = new NoonReportCommentViewModel();
                        comment.Type = VoyageAttributtes != null && VoyageAttributtes.Any() ? VoyageAttributtes.Where(x => x.PlkId == item.PlkIdCommentCategory).Select(x => x.Name).FirstOrDefault() : "-";
                        comment.Reason = "";//CategoryReasons != null && CategoryReasons.Any() ? CategoryReasons.Where(x => x.Identifier == item.NcrId).Select(x => x.Description).FirstOrDefault() : ""; 
                        comment.Comment = item.Comment;
                        result.NoonComments.Add(comment);
                    }
                }
                #endregion


                DateTime? fromDate = OnWeather24HrsGetPreviousDate(noonReportNvaigation, previousSeaPassageDetails);
                DateTime? toDate = GetCurrentDate(noonReportNvaigation);

                result.NoonReport24Weather = Get24HweatherList(response.NoonReport24Weather, windForces, vesselDirections, fromDate, toDate);

                var IsApplicable = result.NoonReport24Weather != null && result.NoonReport24Weather.Any();
                if (IsApplicable)
                {
                    var TotalHoursDetails = result.NoonReport24Weather.Aggregate(TimeSpan.Zero, (value, obj) => value + (obj.PhwRecordedAt - obj.PhwRecordedFrom).GetValueOrDefault());
                    result.Weather24HoursLabel = string.Format(Constants.Weather24HoursLabel, TotalHoursDetails.TotalHours.ToString("N2"));
                }
                else
                {
                    var TotalHoursDetails = TimeSpan.FromHours(20);
                    result.Weather24HoursLabel = string.Format(Constants.Weather24HoursLabel, TotalHoursDetails.TotalHours);
                }
            }
            return result;
        }

        /// <summary>
        /// Calculates the consumption qty.
        /// </summary>
        /// <param name="fuel">The fuel.</param>
        /// <returns></returns>
        private float? CalculateConsumptionQty(FuelROBDetailsViewModel fuel)
        {
            float? MEConsumption = null
            , BreakConsumption = null
            , Cargo = null
            , Boiler = null
            , DGConsumption = null
            , Other = null
            , DeBallast = null
            , IGS = null
            , TankCleaning = null;

            if (fuel.MEConsumption != Constants.DashForEmpty)
                MEConsumption = float.Parse(fuel.MEConsumption);
            if (fuel.BreakConsumption != Constants.DashForEmpty)
                BreakConsumption = float.Parse(fuel.BreakConsumption);
            if (fuel.Cargo != Constants.DashForEmpty)
                Cargo = float.Parse(fuel.Cargo);
            if (fuel.Boiler != Constants.DashForEmpty)
                Boiler = float.Parse(fuel.Boiler);
            if (fuel.DGConsumption != Constants.DashForEmpty)
                DGConsumption = float.Parse(fuel.DGConsumption);
            if (fuel.Other != Constants.DashForEmpty)
                Other = float.Parse(fuel.Other);
            if (fuel.DeBallast != Constants.DashForEmpty)
                DeBallast = float.Parse(fuel.DeBallast);
            if (fuel.IGS != Constants.DashForEmpty)
                IGS = float.Parse(fuel.IGS);
            if (fuel.TankCleaning != Constants.DashForEmpty)
                TankCleaning = float.Parse(fuel.TankCleaning);

            float? ConsumptionQty = (MEConsumption.HasValue || BreakConsumption.HasValue || Cargo.HasValue || Boiler.HasValue || DGConsumption.HasValue || Other.HasValue || DeBallast.HasValue || IGS.HasValue || TankCleaning.HasValue) ?
                             (MEConsumption.GetValueOrDefault() + BreakConsumption.GetValueOrDefault() + Cargo.GetValueOrDefault() + Boiler.GetValueOrDefault() + DGConsumption.GetValueOrDefault() + Other.GetValueOrDefault() + DeBallast.GetValueOrDefault() + IGS.GetValueOrDefault() + TankCleaning.GetValueOrDefault()) : default(float?);
            return ConsumptionQty;
        }

        /// <summary>
        /// Calculates the fuel rob percentage.
        /// </summary>
        /// <param name="TotalROB">The total rob.</param>
        /// <param name="TankCapacity">The tank capacity.</param>
        /// <returns></returns>
        private double CalculateFuelRobPercentage(float? TotalROB, float? TankCapacity)
        {
            var Percentage = TotalROB.HasValue && TankCapacity.GetValueOrDefault() != 0 ? (TotalROB.Value * 100) / TankCapacity.Value : default(double?);
            return (Percentage ?? 0);
        }

        /// <summary>
        /// Calculates the rob percentage.
        /// </summary>
        /// <param name="TankCapacity">The tank capacity.</param>
        /// <param name="ROB">The rob.</param>
        /// <returns></returns>
        private string CalculateROBPercentage(decimal? TankCapacity, decimal? ROB)
        {
            var Percentage = ROB.HasValue && TankCapacity.GetValueOrDefault() != 0 ? (double?)((ROB.Value * 100) / TankCapacity.Value) : default(double?);
            return (Percentage ?? 0).ToString("0.00");
        }

        /// <summary>
        /// Sets the main engine MCR.
        /// </summary>
        /// <param name="PowerOutputMe">The power output me.</param>
        /// <param name="VesselMainEngineMCR">The vessel main engine MCR.</param>
        /// <returns></returns>
        private float SetMainEngineMCR(float? PowerOutputMe, decimal? VesselMainEngineMCR)
        {
            float? MainEngineMCR = null;
            if (PowerOutputMe.HasValue && VesselMainEngineMCR.HasValue && VesselMainEngineMCR != 0)
            {
                MainEngineMCR = (PowerOutputMe / (float)VesselMainEngineMCR.Value) * 100;
            }

            return (MainEngineMCR ?? 0);
        }

        /// <summary>
        /// Gets the route changed reason list.
        /// </summary>
        /// <param name="attributeList">The attribute list.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> GetRouteChangedReasonList(List<PosAttributeLookupCode> attributeList)
        {
            Uri requestsUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetPosAttributeLookup"));
            List<Lookup> response = await PostAsync<List<Lookup>>(requestsUrl, CreateHttpContent(attributeList));
            return response;
        }

        /// <summary>
        /// Gets the synopsis and currnt types.
        /// </summary>
        /// <param name="SynopsisTypes">The synopsis types.</param>
        /// <param name="CurrentTypes">The current types.</param>
        public async Task GetSynopsisAndCurrntTypes(List<Lookup> SynopsisTypes, List<Lookup> CurrentTypes)
        {
            var attributeList = new List<PosAttributeLookupCode>() { PosAttributeLookupCode.NoonSynopsisType, PosAttributeLookupCode.SignificantCurrent };
            Uri requestsUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetPosAttributeLookup"));
            List<Lookup> _attributeLookupTypes = await PostAsync<List<Lookup>>(requestsUrl, CreateHttpContent(attributeList));

            if (_attributeLookupTypes != null && _attributeLookupTypes.Any())
            {
                _attributeLookupTypes.ForEach(type =>
                {
                    if (type.LongDescription == EnumsHelper.GetDescription(PosAttributeLookupCode.NoonSynopsisType))
                    {
                        SynopsisTypes.Add(type);
                    }
                    else if (type.LongDescription == EnumsHelper.GetDescription(PosAttributeLookupCode.SignificantCurrent))
                    {
                        CurrentTypes.Add(type);
                    }
                });
            }
        }

        /// <summary>
        /// Gets the voyage attributes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<List<VoyageAttributeLookup>> GetVoyageAttributes(List<VoyageAttributeLookupCode> value)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetVoyageAttributes"));
            List<VoyageAttributeLookup> response = await PostAsync<List<VoyageAttributeLookup>>(requestUrl, CreateHttpContent(value));
            return response;
        }

        /// <summary>
        /// Gets the type of the position activity.
        /// </summary>
        /// <param name="posActivityType">Type of the position activity.</param>
        /// <returns></returns>
        public async Task<List<PosActivityType>> GetPosActivityType(string posActivityType)
        {
            string queryString = "posActivityType=" + posActivityType;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetPosActivityType"), queryString);
            List<PosActivityType> response = await GetAsync<List<PosActivityType>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the noon comment category reasons.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> GetNoonCommentCategoryReasons(string value)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetNoonCommentCategoryReasons"));
            List<Lookup> response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(value));
            return response;
        }

        /// <summary>
        /// Gets the UTC list for dropdown.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> GetUtcListForDropdown()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetUtcListForDropdown"));
            List<Lookup> UtcList = await GetAsync<List<Lookup>>(requestUrl);
            return UtcList;
        }

        /// <summary>
        /// Gets the direction lookup.
        /// </summary>
        /// <returns></returns>
        public async Task<List<VesselDirection>> GetDirectionLookup()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetDirectionLookup"));
            List<VesselDirection> response = await GetAsync<List<VesselDirection>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the swell height lookup.
        /// </summary>
        /// <returns></returns>
        public async Task<List<SwellHeight>> GetSwellHeightLookup()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetSwellHeightLookup"));
            List<SwellHeight> response = await GetAsync<List<SwellHeight>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the swell length lookup.
        /// </summary>
        /// <returns></returns>
        public async Task<List<SwellLength>> GetSwellLengthLookup()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetSwellLengthLookup"));
            List<SwellLength> response = await GetAsync<List<SwellLength>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the wave position lookup.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> GetWavePositionLookup()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetWavePositionLookup"));
            List<Lookup> response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the wind force lookup.
        /// </summary>
        /// <returns></returns>
        public async Task<List<WindForce>> GetWindForceLookup()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetWindForceLookup"));
            List<WindForce> response = await GetAsync<List<WindForce>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the bar movement lookups.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> GetBarMovementLookups()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetBarMovementLookups"));
            List<Lookup> response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the pitch and roll lookup.
        /// </summary>
        /// <returns></returns>
        public async Task<List<PitchAndRoll>> GetPitchAndRollLookup()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetPitchAndRollLookup"));
            List<PitchAndRoll> response = await GetAsync<List<PitchAndRoll>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the security level.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> GetSecurityLevel()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetSecurityLevel"));
            List<Lookup> securityList = await GetAsync<List<Lookup>>(requestUrl);
            return securityList;
        }

        /// <summary>
        /// Gets the previous noon report detail.
        /// </summary>
        /// <param name="spaId">The spa identifier.</param>
        /// <param name="posId">The position identifier.</param>
        /// <param name="vesId">The ves identifier.</param>
        /// <returns></returns>
        public async Task<PreviousSeaPassageDetails> GetPreviousNoonReportDetail(string spaId, string posId, string vesId)
        {
            var value = new Dictionary<string, object>()
            {
                { "spaId", spaId },
                { "posId", posId },
                { "vesId", vesId },
            };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetPreviousNoonReportDetail"));
            PreviousSeaPassageDetails response = await PostAsync<PreviousSeaPassageDetails>(requestUrl, CreateHttpContent(value));
            if (response == null)
            {
                response = new PreviousSeaPassageDetails();
            }

            return response;
        }

        /// <summary>
        /// Called when [get total break dist].
        /// </summary>
        /// <param name="seaPassageBreaks">The sea passage breaks.</param>
        /// <returns></returns>
        private float OnGetTotalBreakDist(List<SeaPassageBreak> seaPassageBreaks)
        {
            if (seaPassageBreaks != null && seaPassageBreaks.Any())
            {
                return seaPassageBreaks.
                     Where(obj => obj.IsDeleted != true).
                     Aggregate(0.0f, (dist, breakDetail) => dist + breakDetail.OutDistance.GetValueOrDefault());
            }
            return 0.0f;
        }

        /// <summary>
        /// Calculates the total distance on passage.
        /// </summary>
        /// <param name="DisToGr">The dis to gr.</param>
        /// <param name="ActualPrevDistanceTravelled">The actual previous distance travelled.</param>
        /// <returns></returns>
        public float CalculateTotalDistanceOnPassage(float? DisToGr, float? ActualPrevDistanceTravelled)
        {
            return DisToGr.GetValueOrDefault() + ActualPrevDistanceTravelled.GetValueOrDefault();
        }

        /// <summary>
        /// Updateds the total time sailed.
        /// </summary>
        /// <param name="steamTime">The steam time.</param>
        /// <param name="prevTotalSailedTime">The previous total sailed time.</param>
        /// <returns></returns>
        public string UpdatedTotalTimeSailed(float? steamTime, float? prevTotalSailedTime)
        {
            TimeSpan time = TimeSpan.FromHours((prevTotalSailedTime ?? 0) + (steamTime ?? 0));
            return GetFormattedTimeString(time);
        }

        /// <summary>
        /// Calculates the total average speed.
        /// </summary>
        /// <param name="TotalTimeSailedD">The total time sailed d.</param>
        /// <param name="TotalDistance">The total distance.</param>
        /// <returns></returns>
        private string CalculateTotalAvgSpeed(float? TotalTimeSailedD, float? TotalDistance)
        {
            if (TotalDistance.HasValue && TotalTimeSailedD.HasValue && TotalTimeSailedD.Value != 0)
            {
                return string.Format("{0:0.00}", TotalDistance.Value / TotalTimeSailedD.Value);
            }
            else
            {
                return "0.00";
            }
        }

        /// <summary>
        /// Gets the time span from string.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static TimeSpan GetTimeSpanFromString(string time)
        {
            if (!string.IsNullOrEmpty(time))
            {
                string[] input = time.Split(':');
                if (input.Length == 2)
                {
                    return TimeSpan.FromHours(Convert.ToDouble(input[0])).Add(TimeSpan.FromMinutes(Convert.ToDouble(input[1])));
                }
            }
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Gets the stoppage time from breaks.
        /// </summary>
        /// <param name="BreakInPassage">The break in passage.</param>
        /// <returns></returns>
        private TimeSpan GetStoppageTimeFromBreaks(List<SeaPassageBreakViewModel> BreakInPassage)
        {
            if (BreakInPassage != null)
            {
                return
                      BreakInPassage.
                      Where(obj => obj.IsDeleted != true && !string.IsNullOrWhiteSpace(obj.PlkIdBreakInPassageType) && obj.PlkIdBreakInPassageType.Equals(EnumsHelper.GetKeyValue(BreakInPassageType.BreakInPassage))).
                      Aggregate(TimeSpan.Zero, (breakTime, breakDetail) => breakTime + breakDetail.BreakTime.GetValueOrDefault(TimeSpan.Zero));
            }
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Calculates the business time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public string CalculateBusinessTime(TimeSpan? time)
        {
            return GetFormattedTimeString(time);
        }

        /// <summary>
        /// Gets the formatted time string.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static string GetFormattedTimeString(TimeSpan? time)
        {
            return time.HasValue ? ((int)time.Value.TotalHours).ToString("0#") + ":" + time.Value.Minutes.ToString("0#") : "0:00";
        }

        /// <summary>
        /// Gets the formatted date sting.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        private string GetFormattedDateSting(DateTime? dateTime)
        {
            return (dateTime.HasValue) ? dateTime.Value.ToString(Constants.DateFormat) : "-";
        }

        /// <summary>
        /// Gets the formatted date time sting.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        private string GetFormattedDateTimeSting(DateTime? dateTime)
        {
            return (dateTime.HasValue) ? dateTime.Value.ToString(Constants.DateTime24HrFormat) : "-";
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        private string GetDefaultValue(string val)
        {
            return string.IsNullOrWhiteSpace(val) ? "-" : val;
        }

        /// <summary>
        /// Gets the decimal default value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        private string GetDecimalDefaultValue(decimal? val)
        {
            if (val.HasValue)
            {
                return string.Format("{0:0.00}", val);
            }
            else
            {
                return Constants.DashForEmpty;
            }
        }

        /// <summary>
        /// Gets the float default value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        private string GetFloatDefaultValue(float? val)
        {
            if (val.HasValue)
            {
                return string.Format("{0:0.00}", val);
            }
            else
            {
                return Constants.DashForEmpty;
            }
        }

        /// <summary>
        /// Gets the vessel identifier.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        private string GetVesselId(string encryptedVesselDetail)
        {
            if (!string.IsNullOrWhiteSpace(encryptedVesselDetail))
            {
                string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
                return decryptedString.Split(Constants.Separator)[0];
            }
            return null;
        }

        /// <summary>
        /// Creates the longitude.
        /// </summary>
        /// <param name="LongDegree">The long degree.</param>
        /// <param name="LongMin">The long minimum.</param>
        /// <param name="LongIndicator">The long indicator.</param>
        /// <returns></returns>
        private string CreateLongitude(decimal? LongDegree, decimal? LongMin, string LongIndicator)
        {
            string longitude = "";
            if (LongDegree.HasValue)
            {
                longitude += LongDegree.Value.ToString("0.00").Replace(".00", String.Empty);
                longitude += "°, ";
            }
            if (LongMin.HasValue)
            {
                longitude += LongMin.Value.ToString("0.00");
                longitude += "' ";
                longitude += LongIndicator;
            }
            return longitude;
        }

        /// <summary>
        /// Updates the work order status.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="refetchAfterSave">if set to <c>true</c> [refetch after save].</param>
        /// <returns></returns>
        public async Task<bool> UpdateWorkOrderStatus(WorkOrderStatusUpdateRequest request, bool refetchAfterSave)
        {
            var input = new Dictionary<string, object>()
                {
                    {"request", request },
                    {"refetchAfterSave",refetchAfterSave }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/PlannedMaintenance/UpdateWorkOrderStatus"));
            bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(input));
            return response;
        }

        /// <summary>
        /// Posts the get sea passage faop details report.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <param name="spaId">The spa identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<FaopDetailViewModel> PostGetSeaPassageFAOPDetailsReport(string posId, string spaId, string encryptedVesselId)
        {
            string vesselId = GetVesselId(encryptedVesselId);
            string vesselName = CommonUtil.GetVesselDisplayName(_provider, encryptedVesselId);

            FaopDetailViewModel FaopDetails = new FaopDetailViewModel();
            FaopDetails.VesId = encryptedVesselId;
            FaopDetails.VesselName = vesselName;
            var value = new Dictionary<string, object>()
            {
                { "posId", posId },
                { "spaId", spaId },
                { "vesselId", vesselId}
            };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetFaopDetail"));
            SeaPassageDetail response = await PostAsync<SeaPassageDetail>(requestUrl, CreateHttpContent(value));

            if (response != null)
            {
                List<Lookup> SecurityLevel = await GetSecurityLevel();
                var FaopPrevDetails = await PostGetPrevFaopDetailsReport(posId, spaId, encryptedVesselId);

                FullAwayOnPassageROBViewModel FuelRob;
                FullAwayOnPassageROBViewModel WasteRob;
                FullAwayOnPassageROBViewModel LubeOilRob;
                FullAwayOnPassageROBViewModel FreshWaterRob;

                FaopDetails.FuelList = new List<FullAwayOnPassageROBViewModel>();
                FaopDetails.WasteList = new List<FullAwayOnPassageROBViewModel>();
                FaopDetails.FreshWaterList = new List<FullAwayOnPassageROBViewModel>();
                FaopDetails.LubeOilList = new List<FullAwayOnPassageROBViewModel>();

                FaopDetails.SpaDate = GetFormattedDateTimeSting(response.PosSeaPassageEntity.SpaDate);
                FaopDetails.LatDirection = CreateLongitude(response.PosSeaPassageEntity.LatDegree, response.PosSeaPassageEntity.LatMinute, response.PosSeaPassageEntity.LatDirection);
                FaopDetails.LongDirection = CreateLongitude(response.PosSeaPassageEntity.LongDegree, response.PosSeaPassageEntity.LongMinute, response.PosSeaPassageEntity.LongDirection);
                FaopDetails.SpaSecurity = SecurityLevel != null && SecurityLevel.Any() ? SecurityLevel.Where(x => x.Identifier == response.PosSeaPassageEntity.SpaSecurity).Select(x => x.Description).FirstOrDefault() : "-";
                FaopDetails.Dwt = GetDecimalDefaultValue(response.PosSeaPassageEntity.Dwt);
                FaopDetails.TotRev2 = GetDecimalDefaultValue((decimal?)response.PosSeaPassageEntity.TotRev2);
                FaopDetails.SpaRouteChanged = response.PosSeaPassageEntity.SpaRouteChanged == true ? Constants.Yes : Constants.No;
                FaopDetails.IsSpaRouteChanged = response.PosSeaPassageEntity.SpaRouteChanged;

                if (FaopPrevDetails != null)
                {
                    FaopDetails.RobPrevAuxLubeOil = FaopPrevDetails.AuxLubOilRob;
                    FaopDetails.RobPrevClo = FaopPrevDetails.ClyLubeOilRob;
                    FaopDetails.RobPrevCrankCase = FaopPrevDetails.CrankCaseRob;
                    FaopDetails.RobPrevDo = FaopPrevDetails.DoRob;
                    FaopDetails.RobPrevDomestic = FaopPrevDetails.FwRob;
                    FaopDetails.RobPrevFo = FaopPrevDetails.FoRob;
                    FaopDetails.RobPrevGo = FaopPrevDetails.GoRob;
                    FaopDetails.RobPrevLng = FaopPrevDetails.LngRob;
                    FaopDetails.RobPrevLsfo = FaopPrevDetails.LsfoRob;
                    FaopDetails.RobPrevTechnical = FaopPrevDetails.FwTech;
                    FaopDetails.RobPrevSludge = FaopPrevDetails.SludgeRob;
                    FaopDetails.RobPrevBilge = FaopPrevDetails.BilgeRob;
                    FaopDetails.RobPrevSlops = FaopPrevDetails.SlopRob;
                    FaopDetails.RobPrevSewage = FaopPrevDetails.SewageRob;
                    FaopDetails.RobPrevGeneralLubeOil = FaopPrevDetails.GeneralLubOilRob;

                    FuelRob = new FullAwayOnPassageROBViewModel();
                    FuelRob.Title = Constants.Previous;
                    FuelRob.RobFo = (decimal?)FaopDetails.RobPrevFo;
                    FuelRob.RobLsfo = (decimal?)FaopDetails.RobPrevLsfo;
                    FuelRob.RobDo = (decimal?)FaopDetails.RobPrevDo;
                    FuelRob.RobGo = (decimal?)FaopDetails.RobPrevGo;
                    FuelRob.RobLng = (decimal?)FaopDetails.RobPrevLng;
                    FaopDetails.FuelList.Add(FuelRob);

                    WasteRob = new FullAwayOnPassageROBViewModel();
                    WasteRob.Title = Constants.Previous;
                    WasteRob.RobSludge = (decimal?)FaopDetails.RobPrevSludge;
                    WasteRob.RobBilge = (decimal?)FaopDetails.RobPrevBilge;
                    WasteRob.RobSlops = (decimal?)FaopDetails.RobPrevSlops;
                    WasteRob.RobSewage = (decimal?)FaopDetails.RobPrevSewage;
                    FaopDetails.WasteList.Add(WasteRob);

                    LubeOilRob = new FullAwayOnPassageROBViewModel();
                    LubeOilRob.Title = Constants.Previous;
                    LubeOilRob.RobClo = (decimal?)FaopDetails.RobPrevClo;
                    LubeOilRob.RobCrankCase = (decimal?)FaopDetails.RobPrevCrankCase;
                    LubeOilRob.RobAuxLubeOil = (decimal?)FaopDetails.RobPrevAuxLubeOil;
                    LubeOilRob.SpaGeneralLubeOilRob = (decimal?)FaopDetails.RobPrevGeneralLubeOil;
                    FaopDetails.LubeOilList.Add(LubeOilRob);

                    FreshWaterRob = new FullAwayOnPassageROBViewModel();
                    FreshWaterRob.Title = Constants.Previous;
                    FreshWaterRob.RobDomestic = (decimal?)FaopDetails.RobPrevDomestic;
                    FreshWaterRob.RobTechnical = (decimal?)FaopDetails.RobPrevTechnical;
                    FaopDetails.FreshWaterList.Add(FreshWaterRob);
                }

                var _faopDetails = response.PosSeaPassageEntity;
                if (_faopDetails != null)
                {
                    FaopDetails.RobAuxLubeOil = SetCurrentROB((decimal?)FaopDetails.RobPrevAuxLubeOil, _faopDetails.RobAuxLubeOil);
                    FaopDetails.RobClo = SetCurrentROB((decimal?)FaopDetails.RobPrevClo, _faopDetails.RobClo);
                    FaopDetails.RobCrankCase = SetCurrentROB((decimal?)FaopDetails.RobPrevCrankCase, _faopDetails.RobCrankCase);
                    FaopDetails.RobDo = SetCurrentROB((decimal?)FaopDetails.RobPrevDo, (decimal?)_faopDetails.RobDo);
                    FaopDetails.RobDomestic = SetCurrentROB((decimal?)FaopDetails.RobPrevDomestic, _faopDetails.RobDomestic);
                    FaopDetails.RobFo = SetCurrentROB((decimal?)FaopDetails.RobPrevFo, (decimal?)_faopDetails.RobFo);
                    FaopDetails.RobGo = SetCurrentROB((decimal?)FaopDetails.RobPrevGo, (decimal?)_faopDetails.RobGo);
                    FaopDetails.RobLng = SetCurrentROB((decimal?)FaopDetails.RobPrevLng, _faopDetails.RobLng);
                    FaopDetails.RobLngCargo = 0.0M;
                    FaopDetails.RobLsfo = SetCurrentROB((decimal?)FaopDetails.RobPrevLsfo, (decimal?)_faopDetails.RobLsfo);
                    FaopDetails.RobTechnical = SetCurrentROB((decimal?)FaopDetails.RobPrevTechnical, (decimal?)_faopDetails.RobTechnical);
                    FaopDetails.RobSludge = SetCurrentROB((decimal?)FaopDetails.RobPrevSludge, _faopDetails.RobSludge);
                    FaopDetails.RobBilge = SetCurrentROB((decimal?)FaopDetails.RobPrevBilge, _faopDetails.RobBilge);
                    FaopDetails.RobSlops = SetCurrentROB((decimal?)FaopDetails.RobPrevSlops, _faopDetails.RobSlops);
                    FaopDetails.RobSewage = SetCurrentROB((decimal?)FaopDetails.RobPrevSewage, _faopDetails.RobSewage);
                    FaopDetails.SpaGeneralLubeOilRob = SetCurrentROB((decimal?)FaopDetails.RobPrevGeneralLubeOil, _faopDetails.SpaGeneralLubeOilRob);

                    FuelRob = new FullAwayOnPassageROBViewModel();
                    FuelRob.Title = Constants.Current;
                    FuelRob.RobFo = SetCurrentROB((decimal?)FaopDetails.RobPrevFo, (decimal?)_faopDetails.RobFo);
                    FuelRob.RobLsfo = SetCurrentROB((decimal?)FaopDetails.RobPrevLsfo, (decimal?)_faopDetails.RobLsfo);
                    FuelRob.RobDo = SetCurrentROB((decimal?)FaopDetails.RobPrevDo, (decimal?)_faopDetails.RobDo);
                    FuelRob.RobGo = SetCurrentROB((decimal?)FaopDetails.RobPrevGo, (decimal?)_faopDetails.RobGo);
                    FuelRob.RobLng = SetCurrentROB((decimal?)FaopDetails.RobPrevLng, _faopDetails.RobLng);
                    FaopDetails.FuelList.Add(FuelRob);

                    WasteRob = new FullAwayOnPassageROBViewModel();
                    WasteRob.Title = Constants.Current;
                    WasteRob.RobSludge = SetCurrentROB((decimal?)FaopDetails.RobPrevSludge, _faopDetails.RobSludge);
                    WasteRob.RobBilge = SetCurrentROB((decimal?)FaopDetails.RobPrevBilge, _faopDetails.RobBilge);
                    WasteRob.RobSlops = SetCurrentROB((decimal?)FaopDetails.RobPrevSlops, _faopDetails.RobSlops);
                    WasteRob.RobSewage = SetCurrentROB((decimal?)FaopDetails.RobPrevSewage, _faopDetails.RobSewage);
                    FaopDetails.WasteList.Add(WasteRob);

                    LubeOilRob = new FullAwayOnPassageROBViewModel();
                    LubeOilRob.Title = Constants.Current;
                    LubeOilRob.RobClo = SetCurrentROB((decimal?)FaopDetails.RobPrevClo, _faopDetails.RobClo);
                    LubeOilRob.RobCrankCase = SetCurrentROB((decimal?)FaopDetails.RobPrevCrankCase, _faopDetails.RobCrankCase);
                    LubeOilRob.RobAuxLubeOil = SetCurrentROB((decimal?)FaopDetails.RobPrevAuxLubeOil, _faopDetails.RobAuxLubeOil);
                    LubeOilRob.SpaGeneralLubeOilRob = SetCurrentROB((decimal?)FaopDetails.RobPrevGeneralLubeOil, _faopDetails.SpaGeneralLubeOilRob);
                    FaopDetails.LubeOilList.Add(LubeOilRob);

                    FreshWaterRob = new FullAwayOnPassageROBViewModel();
                    FreshWaterRob.Title = Constants.Current;
                    FreshWaterRob.RobDomestic = SetCurrentROB((decimal?)FaopDetails.RobPrevDomestic, _faopDetails.RobDomestic);
                    FreshWaterRob.RobTechnical = SetCurrentROB((decimal?)FaopDetails.RobPrevTechnical, (decimal?)_faopDetails.RobTechnical);
                    FaopDetails.FreshWaterList.Add(FreshWaterRob);

                    FaopDetails.SpaCargoBallastList = new List<FullAwayOnPassageROBViewModel>();
                    var spaCargoBllast = new FullAwayOnPassageROBViewModel();
                    spaCargoBllast.Title = "Cargo";
                    spaCargoBllast.SpaCargoBallastQty = _faopDetails.SpaCargoQty;
                    FaopDetails.SpaCargoBallastList.Add(spaCargoBllast);

                    spaCargoBllast = new FullAwayOnPassageROBViewModel();
                    spaCargoBllast.Title = "Ballast";
                    spaCargoBllast.SpaCargoBallastQty = _faopDetails.SpaBallastQty;
                    FaopDetails.SpaCargoBallastList.Add(spaCargoBllast);

                }
                if (FaopPrevDetails != null)
                {
                    FaopDetails.VoyageRunningHourList = new List<VoyageRunningHourViewModel>();
                    if (FaopPrevDetails.VoyageRunningHourList != null && FaopPrevDetails.VoyageRunningHourList.Any())
                    {
                        foreach (var item in FaopPrevDetails.VoyageRunningHourList)
                        {
                            VoyageRunningHourViewModel VoyageRunning = new VoyageRunningHourViewModel();
                            VoyageRunning.PartName = item.PartName;
                            VoyageRunning.Previous = item.Previous;
                            VoyageRunning.Total = item.Total;

                            FaopDetails.VoyageRunningHourList.Add(VoyageRunning);
                        }
                    }
                }

                if (FaopDetails != null && string.IsNullOrWhiteSpace(FaopDetails.SpaId) && FaopPrevDetails != null)
                {
                    FaopDetails.Fore = FaopPrevDetails.FwdDraft;
                    FaopDetails.Mid = (decimal?)FaopPrevDetails.MidDraft;
                    FaopDetails.Aft = FaopPrevDetails.AftDraft;
                    FaopDetails.MeanDraft = FaopDetails.Fore.HasValue || FaopDetails.Aft.HasValue ? (decimal?)Math.Round(((FaopDetails.Fore.GetValueOrDefault() + FaopPrevDetails.AftDraft.GetValueOrDefault()) / 2), 2, MidpointRounding.ToEven) : default(decimal?);
                }

                FaopDetails.Comments = GetDefaultValue(response.PosSeaPassageEntity.Comments);

            }
            return FaopDetails;
        }

        /// <summary>
        /// Posts the get previous faop details report.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <param name="spaId">The spa identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<VoyageReportPrevFaopDetail> PostGetPrevFaopDetailsReport(string posId, string spaId, string encryptedVesselId)
        {
            string vesselId = GetVesselId(encryptedVesselId);
            var value = new Dictionary<string, object>()
            {
                { "posId", posId },
                { "spaId", spaId },
                { "vesselId", vesselId}
            };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetPrevFaopDetails"));
            VoyageReportPrevFaopDetail FaopPrevDetails = await PostAsync<VoyageReportPrevFaopDetail>(requestUrl, CreateHttpContent(value));

            return FaopPrevDetails;
        }

        /// <summary>
        /// Sets the current rob.
        /// </summary>
        /// <param name="previousValue">The previous value.</param>
        /// <param name="currentValue">The current value.</param>
        /// <returns></returns>
        private decimal? SetCurrentROB(decimal? previousValue, decimal? currentValue)
        {
            if (previousValue.HasValue && previousValue != 0)
            {
                return previousValue;
            }
            return currentValue;
        }

        /// <summary>
        /// Get24s the hweather list.
        /// </summary>
        /// <param name="listOfWeathers">The list of weathers.</param>
        /// <param name="windForces">The wind forces.</param>
        /// <param name="vesselDirections">The vessel directions.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public List<NoonReport24HweatherViewModel> Get24HweatherList(List<NoonReport24Hweather> listOfWeathers, List<WindForce> windForces, List<VesselDirection> vesselDirections
           , DateTime? fromDate, DateTime? toDate)
        {
            var NoonReport24HweatherList = new List<NoonReport24HweatherViewModel>();
            if (fromDate.HasValue && toDate.HasValue)
            {
                listOfWeathers = GetFiltered24HweatherList(listOfWeathers, fromDate.Value, toDate.Value);
            }
            if (listOfWeathers != null && listOfWeathers.Any())
            {
                //NoonReport24HweatherList.AddRange(listOfWeathers.Select(obj => new NoonReport24HweatherViewModel(obj,)));
                foreach (var item in listOfWeathers)
                {
                    NoonReport24HweatherViewModel NoonReport24Hweather = new NoonReport24HweatherViewModel();
                    NoonReport24Hweather.PhwRecordedAt = item.PhwRecordedAt;
                    NoonReport24Hweather.PhwWindSpeed = item.PhwWindSpeed;
                    NoonReport24Hweather.PhwWindForce = windForces != null && windForces.Any() ? windForces.Where(x => x.Id == item.PhwWindForce).Select(x => x.Description).FirstOrDefault() : "-";
                    NoonReport24Hweather.PhwWindDir = vesselDirections != null && vesselDirections.Any() ? vesselDirections.Where(x => x.Id == item.PhwWindDir).Select(x => x.Description).FirstOrDefault() : "-";
                    NoonReport24Hweather.PdrIdSwellDirection = vesselDirections != null && vesselDirections.Any() ? vesselDirections.Where(x => x.Id == item.PdrIdSwellDirection).Select(x => x.Description).FirstOrDefault() : "-";
                    NoonReport24Hweather.WavId = vesselDirections != null && vesselDirections.Any() ? vesselDirections.Where(x => x.Id == item.WavId).Select(x => x.Description).FirstOrDefault() : "-";
                    NoonReport24HweatherList.Add(NoonReport24Hweather);
                }

                if (fromDate.HasValue)
                {
                    DateTime previousDateTime = fromDate.Value;
                    NoonReport24HweatherList.ToList().ForEach(obj =>
                    {
                        obj.PhwRecordedFrom = previousDateTime;
                        if (obj.PhwRecordedAt.HasValue)
                        {
                            previousDateTime = obj.PhwRecordedAt.Value;
                        }
                    });
                }

            }

            return NoonReport24HweatherList;
        }

        /// <summary>
        /// Gets the filtered24 hweather list.
        /// </summary>
        /// <param name="listOfWeathers">The list of weathers.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        private List<NoonReport24Hweather> GetFiltered24HweatherList(List<NoonReport24Hweather> listOfWeathers, DateTime fromDate, DateTime toDate)
        {
            const int spanHours = 4; // as we want to divide time slot in 4 hours span we initialise it with 4
            const int noonHour = 12; // variable to indicate mid of day i.e. 12pm
            const int totalHourPerDay = 24;

            double startHours = Math.Floor(fromDate.TimeOfDay.TotalHours / spanHours) * spanHours;
            int noOfValidElement = GetMaxValidSlotCount(fromDate.Date.AddHours(startHours), toDate, spanHours);
            if (noOfValidElement > 0)
            {
                List<NoonReport24Hweather> list = new List<NoonReport24Hweather>();
                List<double> filteredHourList = new List<double>();
                for (int i = 0; i < noOfValidElement; i++)
                {
                    double hours = startHours + spanHours;
                    hours = hours == totalHourPerDay ? 0 : hours;
                    filteredHourList.Add(hours);
                    startHours = hours;
                }
                foreach (double hr in filteredHourList)
                {
                    NoonReport24Hweather weatherDetail = null;
                    if (listOfWeathers != null && listOfWeathers.Any())
                    {
                        weatherDetail = listOfWeathers.Where(obj => obj.PhwRecordedAt.HasValue).FirstOrDefault(obj => obj.PhwRecordedAt.Value.TimeOfDay.Hours == hr);
                        if (weatherDetail != null)
                        {
                            weatherDetail.PhwRecordedAt = hr <= noonHour ? toDate.Date.AddHours(hr) : toDate.Date.AddDays(-1).AddHours(hr);
                        }
                    }
                    if (weatherDetail == null)
                    {
                        weatherDetail = new NoonReport24Hweather
                        {
                            PhwRecordedAt = hr <= noonHour ? toDate.Date.AddHours(hr) : toDate.Date.AddDays(-1).AddHours(hr),
                        };
                    }
                    if (weatherDetail != null)
                    {
                        list.Add(weatherDetail);
                        if (noOfValidElement == list.Count)
                        {
                            break;
                        }
                    }
                }
                return list.Any() ? list : null;
            }
            return null;
        }

        /// <summary>
        /// Gets the maximum valid slot count.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="spanHours">The span hours.</param>
        /// <returns></returns>
        private int GetMaxValidSlotCount(DateTime fromDate, DateTime toDate, int spanHours)
        {
            TimeSpan diff = toDate - fromDate;
            return ((int)Math.Ceiling(diff.TotalHours / spanHours) - 1);
        }

        /// <summary>
        /// Gets the crossed IDL days.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        private int? GetCrossedIDLDays(NoonReportNavigationViewModel viewModel)
        {
            int? CrossedIDLDays = null;
            if (viewModel.IsNoon && !string.IsNullOrWhiteSpace(viewModel.SpaId))
            {
                if (viewModel.Date.HasValue)
                {
                    DateTime acualNoonDate = viewModel.PreviousDate.Value;
                    acualNoonDate = (acualNoonDate.TimeOfDay < TimeSpan.FromHours(12)) ? acualNoonDate.Date.AddHours(12) : acualNoonDate.Date.AddDays(1).AddHours(12);
                    CrossedIDLDays = (viewModel.Date.Value - acualNoonDate).Days;

                    viewModel.IsCrossedIDL = CrossedIDLDays > 0 ? true : false;
                }
            }
            return CrossedIDLDays;
        }

        /// <summary>
        /// Called when [weather24 HRS get previous date].
        /// </summary>
        /// <param name="_navigation">The navigation.</param>
        /// <param name="_previousSeaPassageDetails">The previous sea passage details.</param>
        /// <returns></returns>
        private DateTime? OnWeather24HrsGetPreviousDate(NoonReportNavigationViewModel _navigation, PreviousSeaPassageDetails _previousSeaPassageDetails)
        {
            if (_navigation != null && _previousSeaPassageDetails != null)
            {
                return (_navigation.IsCrossedIDL && _navigation.Date.HasValue) ? _navigation.Date.Value.AddDays(-1) : _previousSeaPassageDetails.SpaDate;
            }
            return default(DateTime?);
        }

        /// <summary>
        /// Gets the current date.
        /// </summary>
        /// <param name="_navigation">The navigation.</param>
        /// <returns></returns>
        private DateTime? GetCurrentDate(NoonReportNavigationViewModel _navigation)
        {
            return _navigation != null ? _navigation.Date : default(DateTime?);
        }

        /// <summary>
        /// Gets the change in destination.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <param name="spaId">The spa identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<ChangeInDestinationViewModel> PostGetChangeInDestination(string posId, string spaId, string encryptedVesselId)
        {
            ChangeInDestinationViewModel ChangeInDestinationDetail = new ChangeInDestinationViewModel();
            ChangeInDestination result = new ChangeInDestination();

            string vesselId = GetVesselId(encryptedVesselId);
            string vesselName = CommonUtil.GetVesselDisplayName(_provider, encryptedVesselId);

            if (string.IsNullOrWhiteSpace(spaId))
            {
                result = await GetNewChangeInDestinationAsync(posId);
            }
            else
            {
                result = await GetChangeInDestinationAsync(spaId, posId);
            }

            if (result != null)
            {
                List<Lookup> SecurityLevel = await GetSecurityLevel();

                ChangeInDestinationDetail.SpaDate = GetFormattedDateTimeSting(result.SpaDate);
                ChangeInDestinationDetail.SpaLat3 = CreateLongitude(result.SpaNewLat1, result.SpaNewLat2, result.SpaLat3);
                ChangeInDestinationDetail.SpaLong3 = CreateLongitude(result.SpaNewLong1, result.SpaNewLong2, result.SpaLong3);
                ChangeInDestinationDetail.SpaSecurity = SecurityLevel != null && SecurityLevel.Any() ? SecurityLevel.Where(x => x.Identifier == result.SpaSecurity).Select(x => x.Description).FirstOrDefault() : "-";
                ChangeInDestinationDetail.OldPortPrtName = result.OldPortPrtName;
                ChangeInDestinationDetail.PoslistPosDistGo = result.PoslistPosDistGo;
                ChangeInDestinationDetail.OldPortCntId = result.OldPortCntId;
                ChangeInDestinationDetail.PoslistPosDate3 = GetFormattedDateSting(result.PoslistPosDate3);
                ChangeInDestinationDetail.SpaPortNew = result.NewPort1CntId + " - " + result.NewPort1PrtName;
                ChangeInDestinationDetail.SpaDistGo = result.SpaDistGo;
                ChangeInDestinationDetail.NewPort1CntId = result.Port1 != null ? result.Port1.CountryName : Constants.DashForEmpty;
                ChangeInDestinationDetail.SpaEtaDate = GetFormattedDateSting(result.SpaEtaDate);
                ChangeInDestinationDetail.SpaComment = result.SpaComment;
                ChangeInDestinationDetail.VesselName = vesselName;
            }

            return ChangeInDestinationDetail;
        }

        /// <summary>
        /// Gets the change in destination asynchronous.
        /// </summary>
        /// <param name="spaId">The spa identifier.</param>
        /// <param name="posId">The position identifier.</param>
        /// <returns></returns>
        private async Task<ChangeInDestination> GetChangeInDestinationAsync(string spaId, string posId)
        {
            var value = new Dictionary<string, object>()
            {
                { "spaId", spaId },
                { "posId", posId },
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetChangeInDestination"));
            ChangeInDestination response = await PostAsync<ChangeInDestination>(requestUrl, CreateHttpContent(value));

            return response;
        }

        /// <summary>
        /// Gets the new change in destination asynchronous.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <returns></returns>
        private async Task<ChangeInDestination> GetNewChangeInDestinationAsync(string posId)
        {
            var value = new Dictionary<string, object>()
            {
                { "posId", posId },
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetNewChangeInDestination"));
            ChangeInDestination response = await PostAsync<ChangeInDestination>(requestUrl, CreateHttpContent(value));

            return response;
        }
        #endregion

        #region Planned Maintenance

        /// <summary>
        /// Gets the wo reschedule header detail.
        /// </summary>
        /// <param name="workOrderId">The work order identifier.</param>
        /// <returns></returns>
        public async Task<WorkOrderRescheduleHeaderDetailViewModel> GetWORescheduleHeaderDetail(string workOrderId, string vesselId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/PlannedMaintenance/GetWORescheduleHeaderDetail/" + workOrderId));

            WorkOrderRescheduleHeaderDetail response = await GetAsync<WorkOrderRescheduleHeaderDetail>(requestUrl);

            WorkOrderRescheduleHeaderDetailViewModel result = new WorkOrderRescheduleHeaderDetailViewModel(response);

            if (response != null)
            {
                RescheduleRequestViewModel request = new RescheduleRequestViewModel()
                {
                    PorId = response.PorId,
                    PorRequestId = response.PorRequestId,
                    RescheduleStatus = response.RescheduleStatusId,
                    VesselId = vesselId,
                    WorkOrderId = workOrderId,
                    MlaIdSource = EnumsHelper.GetKeyValue(RiskAssessmentSource.RescheduleWorkOrder)
                };
                result.ProcessRescheduleWoUrl = CommonUtil.GetEncryptedURL<RescheduleRequestViewModel>(_provider, Constants.RescheduleRequestEncryptionText, request);
            }

            return result;
        }

        /// <summary>
        /// Gets the reschedule work order for edit.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<RescheduleWorkOrderDetailViewModel> GetRescheduleWorkOrderForEdit(RescheduleRequestViewModel input)
        {
            RescheduleRequest request = new RescheduleRequest()
            {
                PorId = input.PorId,
                PorRequestId = input.PorRequestId,
                RescheduleStatus = input.RescheduleStatus,
                VesselId = input.VesselId,
                WorkOrderId = input.WorkOrderId
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/PlannedMaintenance/GetRescheduleWorkOrderForEdit"));
            RescheduleWorkOrderDetail response = await PostAsync<RescheduleWorkOrderDetail>(requestUrl, CreateHttpContent(request));

            RescheduleWorkOrderDetailViewModel result = null;

            if (response != null)
            {
                result = new RescheduleWorkOrderDetailViewModel()
                {
                    WorkOrderReasonDescription = response.WorkOrderReasonDescription,
                    ExtendedBy = Convert.ToInt32(response.ExtendedBy),
                    RequestedBy = response.RequestedBy,
                    RequestedOn = response.RequestedOn.ToString(Constants.DateFormat),
                    RequesterRoleDescription = response.RequesterRoleDescription ?? "",
                    ReasonForReschedule = response.RequesterComment ?? "",
                    IsRiskAssessmentMapped = response.IsRiskAssessmentMapped,
                    RiskAssessmentMappedComment = response.RiskAssessmentMappedComment,
                    IsJobHistoryLinked = response.IsJobHistoryLinked,
                    JobHistoryLinkedComment = response.JobHistoryLinkedComment,
                    IsSupportingWOCreated = response.IsSupportingWOCreated,
                    SupportingWOCreatedComment = response.SupportingWOCreatedComment
                };

                result.AssociatedJobs = GetAssociatedJobs(response.WorkOrdersToReschedule);
                result.SupportingJobsHistory = GetSupportingJobHistory(response.MappedWorkOrder);
                result.SupportingWorkOrders = GetSupportingWorkOrders(response.SupportingWorkOrders);
            }

            return result;
        }

        /// <summary>
        /// Gets the associated jobs.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private List<RescheduleMappedWODetailViewModel> GetAssociatedJobs(List<RescheduleMappedWODetail> data)
        {
            List<RescheduleMappedWODetailViewModel> transformedData = new List<RescheduleMappedWODetailViewModel>();

            if (data != null && data.Any())
            {
                RescheduleMappedWODetailViewModel obj = null;
                foreach (var item in data)
                {
                    obj = new RescheduleMappedWODetailViewModel
                    {
                        Type = item.JobTypeShortCode,
                        DueDate = item.OriginalDueDate,
                        ComponentName = item.ComponentName,
                        JobName = item.JobName,
                        Status = item.WorkOrderStatusShortCode,
                        Interval = item.IntervalValue + " " + item.IntervalType,
                        Responsibility = item.ResponsibilityShortCode
                    };
                    transformedData.Add(obj);
                }
            }
            return transformedData;
        }

        /// <summary>
        /// Gets the supporting job history.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private List<RescheduleLinkedWorkOrderViewModel> GetSupportingJobHistory(List<RescheduleLinkedWorkOrder> data)
        {
            List<RescheduleLinkedWorkOrderViewModel> transformedData = new List<RescheduleLinkedWorkOrderViewModel>();

            if (data != null && data.Any())
            {
                RescheduleLinkedWorkOrderViewModel obj = null;

                foreach (var item in data)
                {
                    obj = new RescheduleLinkedWorkOrderViewModel
                    {
                        DoneDate = item.DoneDate,
                        ClosedDate = item.ClosedDate,
                        Department = item.DepartmentShortCode,
                        Name = item.JobName,
                        Type = item.JobTypeShortCode,
                        ComponentName = item.ComponentName,
                        Interval = item.IntervalValue + " " + item.IntervalType,
                        Responsibility = item.RankDescription
                    };
                    transformedData.Add(obj);
                }
            }
            return transformedData;
        }

        /// <summary>
        /// Gets the supporting work orders.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private List<UnplannedWorkOrderDetailViewModel> GetSupportingWorkOrders(List<UnplannedWorkOrderDetail> data)
        {
            List<UnplannedWorkOrderDetailViewModel> transformedData = new List<UnplannedWorkOrderDetailViewModel>();

            if (data != null && data.Any())
            {
                UnplannedWorkOrderDetailViewModel obj = null;

                foreach (var item in data)
                {
                    obj = new UnplannedWorkOrderDetailViewModel
                    {
                        JobName = item.WorkOrderName,
                        Type = item.JobTypeShortCode,
                        DueDate = item.DueDate,
                        Responsibility = item.ResponsibleRankDescription
                    };
                    transformedData.Add(obj);
                }

            }

            return transformedData;
        }

        /// <summary>
        /// Gets the reschedule work order rules.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<PMSRescheduleRulesResponseViewModel> GetRescheduleWorkOrderRules(WorkOrderHeaderDetailViewModel input)
        {
            PMSRescheduleRulesRequest request = new PMSRescheduleRulesRequest()
            {
                IntervalTypeId = input.IntervalTypeId,
                IntervalValue = input.IntervalValue,
                IsCritical = input.IsCritical,
                JobIntervalTypeId = input.JobIntervalTypeId,
                VesselId = input.VesselId,
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/PlannedMaintenance/GetRescheduleWorkOrderRules"));
            PMSRescheduleRulesResponse response = await PostAsync<PMSRescheduleRulesResponse>(requestUrl, CreateHttpContent(request));

            PMSRescheduleRulesResponseViewModel result = new PMSRescheduleRulesResponseViewModel();

            if (response != null)
            {
                result.ExtendedDaysNote = GetExtendedDaysNote(request.IntervalValue, request.IntervalTypeId, Convert.ToInt32(response.PercentValue), Convert.ToInt32(response.MonthsValue));
                result.MaximumCounterExtensionValue = Convert.ToInt32(request.IntervalValue) * Convert.ToInt32(response.PercentValue) / 100;
                result.MaximumIntervalDays = ConvertWeeksAndMonthsToDays(request.IntervalValue, request.IntervalTypeId) * Convert.ToInt32(response.PercentValue) / 100;
            }

            return result;
        }

        /// <summary>
        /// Gets the extended days note.
        /// </summary>
        /// <param name="intervalValue">The interval value.</param>
        /// <param name="intervalTypeId">The interval type identifier.</param>
        /// <param name="maxCounterExtensionPercentage">The maximum counter extension percentage.</param>
        /// <param name="maxCounterExtensionMonths">The maximum counter extension months.</param>
        /// <returns></returns>
        private string GetExtendedDaysNote(int? intervalValue, string intervalTypeId, int maxCounterExtensionPercentage, int maxCounterExtensionMonths)
        {
            string extendedDaysNote = String.Empty;
            if (!((intervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.RunningHours)) || (intervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Revolutions)) || (intervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Events))))
            {
                int maximumIntervalDays = ConvertWeeksAndMonthsToDays(intervalValue, intervalTypeId) * maxCounterExtensionPercentage / 100;
                extendedDaysNote = string.Format("You can only reschedule a work order for {0} days beyond its due date.", maximumIntervalDays);
            }
            else
            {
                extendedDaysNote = string.Format("Reschedule date is calculated based on {0}% of the interval or {1} months max whichever is the lower.", maxCounterExtensionPercentage, maxCounterExtensionMonths);
            }
            return extendedDaysNote;
        }

        /// <summary>
        /// Converts the weeks and months to days.
        /// </summary>
        /// <param name="intervalValue">The interval value.</param>
        /// <param name="intervalTypeId">The interval type identifier.</param>
        /// <returns></returns>
        private int ConvertWeeksAndMonthsToDays(int? intervalValue, string intervalTypeId)
        {
            if (intervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Month) || string.IsNullOrWhiteSpace(intervalTypeId))
            {
                return Convert.ToInt32(intervalValue) * 30;
            }
            else if (intervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Week))
            {
                return Convert.ToInt32(intervalValue) * 7;
            }

            return 0;
        }

        /// <summary>
        /// Updates the wo reschedule status.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="inputValue">The input value.</param>
        /// <param name="refetchAfterSave">if set to <c>true</c> [refetch after save].</param>
        /// <returns></returns>
        public async Task<bool> UpdateWORescheduleStatus(RescheduleRequestViewModel input, RescheduleWorkOrderDetailViewModel inputValue, bool refetchAfterSave)
        {
            RescheduleRequest request = new RescheduleRequest()
            {
                PorId = input.PorId,
                PorRequestId = input.PorRequestId,
                RescheduleStatus = input.RescheduleStatus,
                VesselId = input.VesselId,
                WorkOrderId = input.WorkOrderId
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/PlannedMaintenance/GetRescheduleWorkOrderForEdit"));
            RescheduleWorkOrderDetail response = await PostAsync<RescheduleWorkOrderDetail>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                if (inputValue.IsApprove)
                {
                    response.RescheduleStatusId = EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Approved);
                    response.ApproverComment = inputValue.Comment;
                    response.RevisitComment = null;
                }
                else if (inputValue.IsReject)
                {
                    response.RescheduleStatusId = EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Rejected);
                    response.ApproverComment = inputValue.Comment;
                    response.RevisitComment = null;
                }
                else if (inputValue.IsRevise)
                {
                    response.RescheduleStatusId = EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Revised);
                    response.ApproverComment = null;
                    response.RevisitComment = inputValue.Comment;
                }

                response.IsRiskAssessmentMapped = inputValue.IsRiskAssessmentMapped;
                response.RiskAssessmentMappedComment = inputValue.RiskAssessmentMappedComment;
                response.IsJobHistoryLinked = inputValue.IsJobHistoryLinked;
                response.JobHistoryLinkedComment = inputValue.JobHistoryLinkedComment;
                response.IsSupportingWOCreated = inputValue.IsSupportingWOCreated;
                response.SupportingWOCreatedComment = inputValue.SupportingWOCreatedComment;
                response.ApprovedExtendedBy = inputValue.ApprovedExtendedBy;

                bool isCounterBased = (response.PjiId == EnumsHelper.GetKeyValue(JobIntervalType.RunningHours)) || (response.PjiId == EnumsHelper.GetKeyValue(JobIntervalType.Revolutions)) || (response.PjiId == EnumsHelper.GetKeyValue(JobIntervalType.Events));

                if (isCounterBased)
                {
                    response.RescheduledInterval = (response.OriginalInterval ?? 0) + (response.ApprovedExtendedBy ?? 0);
                    response.RequestedInterval = (response.OriginalInterval ?? 0) + response.ExtendedBy;
                }
                else
                {
                    response.RescheduledInterval = (response.ApprovedExtendedBy ?? 0);
                    response.RequestedInterval = response.ExtendedBy;
                }
            }

            var inputRequest = new Dictionary<string, object>()
                {
                    {"request", response },
                    {"refetchAfterSave",refetchAfterSave }
                };
            Uri saveRequestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/PlannedMaintenance/UpdateWORescheduleStatus"));
            bool isSaveSuccessful = await PostAsync<bool>(saveRequestUrl, CreateHttpContent(inputRequest));
            return isSaveSuccessful;
        }

        #endregion

        #region JSA

        /// <summary>
        /// Gets the mapped risk assessment detail.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<HazardDetailViewModel>> GetMappedRiskAssessmentDetail(RescheduleRequestViewModel request)
        {
            var input = new Dictionary<string, object>()
            {
                {"vesselId", request.VesselId },
                {"sourceId", request.PorRequestId },
                {"mlaIdSource", request.MlaIdSource }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/JSA/GetMappedRiskAssessmentDetail"));
            MappedRiskAssessmentDetail response = await PostAsync<MappedRiskAssessmentDetail>(requestUrl, CreateHttpContent(input));

            List<HazardDetailViewModel> result = new List<HazardDetailViewModel>();

            if (response != null)
            {
                HazardDetailViewModel hazardDetailsVM = null;
                if (response.RiskAssessments != null && response.RiskAssessments.Any())
                {
                    foreach (var parent in response.RiskAssessments)
                    {
                        for (int i = 0; i < parent.HazardList.Count; i++)
                        {
                            HazardDetail hazardElement = parent.HazardList[i];

                            hazardDetailsVM = new HazardDetailViewModel()
                            {
                                RiskArea = parent.Originator + " \\ " + parent.RefNumber + " " + parent.WorkActivityDescription,
                                Description = hazardElement.Description,
                                LikelihoodDescription = hazardElement.LikelihoodDescription,
                                SeverityDescription = hazardElement.SeverityDescription,
                                RiskFactorDescription = hazardElement.RiskFactorDescription,
                                HazardNumber = i + 1
                            };

                            if (!string.IsNullOrWhiteSpace(hazardElement.LikelihoodDescription))
                            {
                                hazardDetailsVM.LikelihoodColor = GetColorBrush(Convert.ToInt16(hazardElement.LikelihoodDescription.Substring(0, 1)));
                            }
                            else
                            {
                                hazardDetailsVM.LikelihoodColor = "Good";
                            }

                            if (!string.IsNullOrWhiteSpace(hazardElement.SeverityDescription))
                            {
                                hazardDetailsVM.SeverityColor = GetColorBrush(Convert.ToInt16(hazardElement.SeverityDescription.Substring(0, 1)));
                            }
                            else
                            {
                                hazardDetailsVM.SeverityColor = "Good";
                            }

                            if (!string.IsNullOrWhiteSpace(hazardElement.RiskFactorDescription))
                            {
                                hazardDetailsVM.RiskFactorColor = GetColorBrush(Convert.ToInt16(hazardElement.RiskFactorDescription.Substring(0, 1)));
                            }
                            else
                            {
                                hazardDetailsVM.RiskFactorColor = "Good";
                            }

                            if (!string.IsNullOrWhiteSpace(hazardElement.InitialRiskFactorDescription))
                            {
                                hazardDetailsVM.InitialRiskColor = GetColorBrush(Convert.ToInt32(hazardElement.InitialRiskFactorDescription.Substring(0, 1)));
                            }

                            if (!string.IsNullOrWhiteSpace(hazardElement.InitialRiskFactorDescription) && !string.IsNullOrWhiteSpace(hazardElement.RiskFactorDescription) && !string.IsNullOrWhiteSpace(hazardElement.RghId))
                            {
                                hazardDetailsVM.IsInitialRiskVisible = Convert.ToInt32(hazardElement.InitialRiskFactorDescription.Substring(0, 1)) != Convert.ToInt32(hazardElement.RiskFactorDescription.Substring(0, 1));
                            }
                            else
                            {
                                hazardDetailsVM.IsInitialRiskVisible = false;
                            }

                            hazardDetailsVM.InitialRiskFactorDescription = "Initial Risk " + hazardElement.InitialRiskFactorDescription;


                            result.Add(hazardDetailsVM);
                        }
                    }
                }
                if (response.AdditionalHazardList != null && response.AdditionalHazardList.Any())
                {
                    foreach (var item in response.AdditionalHazardList)
                    {
                        hazardDetailsVM = new HazardDetailViewModel()
                        {
                            RiskArea = Constants.AdditionalJobHazards,
                            Description = item.Description,
                            LikelihoodDescription = item.LikelihoodDescription,
                            SeverityDescription = item.SeverityDescription,
                            RiskFactorDescription = item.RiskFactorDescription,
                            HazardNumber = item.HazardNumber
                        };

                        if (!string.IsNullOrWhiteSpace(item.LikelihoodDescription))
                        {
                            hazardDetailsVM.LikelihoodColor = GetColorBrush(Convert.ToInt16(item.LikelihoodDescription.Substring(0, 1)));
                        }
                        else
                        {
                            hazardDetailsVM.LikelihoodColor = "Good";
                        }

                        if (!string.IsNullOrWhiteSpace(item.SeverityDescription))
                        {
                            hazardDetailsVM.SeverityColor = GetColorBrush(Convert.ToInt16(item.SeverityDescription.Substring(0, 1)));
                        }
                        else
                        {
                            hazardDetailsVM.SeverityColor = "Good";
                        }

                        if (!string.IsNullOrWhiteSpace(item.RiskFactorDescription))
                        {
                            hazardDetailsVM.RiskFactorColor = GetColorBrush(Convert.ToInt16(item.RiskFactorDescription.Substring(0, 1)));
                        }
                        else
                        {
                            hazardDetailsVM.RiskFactorColor = "Good";
                        }

                        result.Add(hazardDetailsVM);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the color brush.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetColorBrush(int count)
        {
            switch (count)
            {
                case 1:
                case 2: return "Good";
                case 3:
                case 4: return "Warning";
                case 5:
                case 6: return "Critical";
                default: return "Good";
            }
        }

        #endregion

        /// <summary>
        /// Gets the port event and rob details.
        /// </summary>
        /// <param name="psfId">The PSF identifier.</param>
        /// <returns></returns>
        /// AddEditPortEventDelayDetailsView
        public async Task<PortEventDetailsViewModel> GetPortEventAndRobDetails(PortEventRobSummaryRequestViewModel input)
        {
            PortEventDetailsViewModel result = new PortEventDetailsViewModel();
            var response = await GetPortEventDetailsAsync(input.PsfId);
            if (response != null)
            {
                result = await SetPortEventHeaderDetails(response);
                result.IsEospFaopModeEnable = false;
                result.IsConsumptionModeEnable = true;
                result.FuelRobBreakDown = await SetFuelRobDetailsForPortCall(input, response.FuelRobBreakDown, result);
                result.LubOilBreakDown = await SetLubeOilDetailsForPortCall(input, response.LubOilBreakDown, result);
            }
            return result;
        }

        /// <summary>
        /// The add edit eosp view
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PortEventDetailsViewModel> GetPortEospEventAndRobDetails(PortEventRobSummaryRequestViewModel input)
        {
            PortEventDetailsViewModel result = new PortEventDetailsViewModel();
            var response = await GetPortEventDetailsAsync(input.PsfId);
            if (response != null)
            {
                result = await SetPortEventHeaderDetails(response);
                result.IsEospFaopModeEnable = true;
                result.IsConsumptionModeEnable = false;

                SetVesselDraft(result, response);
                result.FuelRobBreakDown = await SetFuelRobDetailsForPortCall(input, response.FuelRobBreakDown, result);
                result.FreshWaterRobBreakDown = await SetFreshWaterRobDetails(input, response.FreshWaterRobBreakDown, result);
                result.LubOilBreakDown = await SetLubeOilDetailsForPortCall(input, response.LubOilBreakDown, result);
                result.WasteRobBreakDown = await SetWasteRobDetails(input, response.WasteRobBreakDown, result);
                result.BallastDetails = await SetBallastDetails(GetBallastTemplate(), response.BallastDetails);
                List<PortEngineRunningHours> runningHourList = await GetRunningHourListAsync(input, response);
                result.EngineRunningHours = SetRunningHourList(runningHourList);
            }
            return result;
        }

        /// <summary>
        /// The set portevent header details
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<PortEventDetailsViewModel> SetPortEventHeaderDetails(PortEventDetails response)
        {
            PortEventDetailsViewModel result = new PortEventDetailsViewModel();
            List<Lookup> SecurityLevelList = await GetSecurityLevel();
            List<Lookup> OffHireTypesList = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.OffHireApplicable });
            result.FromDate = response.FromDate;
            result.ToDate = response.ToDate;
            result.TotalDistance = response.TotalDistance;
            result.SecurityLevel = SecurityLevelList != null && SecurityLevelList.Any() ? SecurityLevelList.Where(x => x.Identifier == response.SecurityLevel).Select(x => x.Description).FirstOrDefault() : Constants.DashForEmpty;
            result.IsOffHire = response.IsOffHire;
            result.OffHireType = OffHireTypesList != null && OffHireTypesList.Any() ? OffHireTypesList.Where(x => x.Identifier == response.PlkIdOffHireType).Select(x => x.Description).FirstOrDefault() : Constants.DashForEmpty;
            result.IsLopIssued = response.IsLopIssued;
            result.Duration = response.Duration;
            result.Comment = response.Comment;

            return result;
        }

        /// <summary>
        /// The GetPortEventDetailsAsync
        /// </summary>
        /// <param name="PsfId"></param>
        /// <returns></returns>
        private async Task<PortEventDetails> GetPortEventDetailsAsync(string PsfId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetPortEventAndRobDetails/" + PsfId));
            PortEventDetails response = await GetAsync<PortEventDetails>(requestUrl);

            return response;
        }

        /// <summary>
        /// Sets the fuel rob details for port call.
        /// </summary>
        /// <param name="fuelRobTemplate">The fuel rob template.</param>
        /// <param name="fuelRobDetails">The fuel rob details.</param>
        /// <returns></returns>
        private async Task<List<PortCallFuelRobDetailsViewModel>> SetFuelRobDetailsForPortCall(PortEventRobSummaryRequestViewModel input, List<PortEventAttributeDetail> fuelRobDetails, PortEventDetailsViewModel result)
        {
            List<PortCallFuelRobDetailsViewModel> FuelRobList = new List<PortCallFuelRobDetailsViewModel>();

            PortEventRobSummaryRequest request = new PortEventRobSummaryRequest();
            request.PosId = input.PosId;
            request.PpfId = input.PpfId;
            request.PsfId = input.PsfId;
            request.VesselId = GetVesselId(input.EncryptedVesselDetail);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetFuelSummaryForPortEvent"));
            List<PortEventAttributeDetail> fuelRobTemplate = await PostAsync<List<PortEventAttributeDetail>>(requestUrl, CreateHttpContent(request));

            if (fuelRobDetails != null && fuelRobDetails.Any() && fuelRobTemplate != null && fuelRobTemplate.Any())
            {
                List<Lookup> _fuelTypes = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.FuelType });
                List<Lookup> _consumptionCategory = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.ConsumptionCategory });
                var RobDetailsData = new List<PortEventAttributeDetailViewModel>();

                List<IGrouping<string, PortEventAttributeDetail>> groupByRobTypeIdList = null;
                // MAPPING DATA IN TEMPLATE FOR EDIT TO INITIALISE ROB DETAILS
                if (fuelRobTemplate != null && fuelRobTemplate.Any() && fuelRobDetails != null && fuelRobDetails.Any())
                {
                    fuelRobTemplate.ForEach(template =>
                    {
                        PortEventAttributeDetail entity = fuelRobDetails.FirstOrDefault(currentDetail => template.AttributeId == currentDetail.AttributeId && template.RobTypeId == currentDetail.RobTypeId);
                        if (entity != null)
                        {
                            template.PpeId = entity.PpeId;
                            template.Value = template.AttributeId == EnumsHelper.GetKeyValue(ConsumptionCategoryAttribute.PreviousROB) || template.AttributeId == EnumsHelper.GetKeyValue(ConsumptionCategoryAttribute.TotalROB) ? (entity.Value ?? 0) : entity.Value;
                        }
                        else
                        {
                            template.Value = (template.AttributeId == EnumsHelper.GetKeyValue(ConsumptionCategoryAttribute.PreviousROB) || (template.AttributeId == EnumsHelper.GetKeyValue(ConsumptionCategoryAttribute.TotalROB) && template.IsReadOnly)) ? (template.Value ?? 0) : template.Value;
                        }

                    });
                    groupByRobTypeIdList = fuelRobTemplate.GroupBy(obj => obj.RobTypeId).ToList();
                }

                Dictionary<string, string> fuelTypeTitleDescById = _fuelTypes != null ? _fuelTypes.ToDictionary(fuelType => fuelType.Identifier, fuelType => fuelType.Description) : new Dictionary<string, string>();

                if (groupByRobTypeIdList != null)
                {
                    foreach (string robTypeId in fuelTypeTitleDescById.Keys)
                    {
                        PortCallFuelRobDetailsViewModel FuelRobModel = new PortCallFuelRobDetailsViewModel();
                        FuelRobModel.RobDetails = new List<PortEventAttributeDetailViewModel>();
                        IGrouping<string, PortEventAttributeDetail> item = groupByRobTypeIdList.FirstOrDefault(obj => obj.Key == robTypeId);

                        if (item != null)
                        {
                            var sortedList = item.OrderBy(x => x.SortIndex).ToList();
                            if (result.IsEospFaopModeEnable && !result.IsConsumptionModeEnable)
                            {
                                PortEventAttributeDetail tankCapacity = sortedList.FirstOrDefault(obj => obj.AttributeId == EnumsHelper.GetKeyValue(LubOilROBBreakDown.TankCapacity));
                                if (tankCapacity != null)
                                {
                                    sortedList.Remove(tankCapacity);
                                }
                            }

                            foreach (var portEvent in sortedList)
                            {
                                PortEventAttributeDetailViewModel portAtrribute = new PortEventAttributeDetailViewModel();
                                portAtrribute.RobTypeId = portEvent.AttributeId;
                                portAtrribute.PpeId = portEvent.PpeId;
                                portAtrribute.AttributeId = portEvent.AttributeId;
                                portAtrribute.Value = portEvent.Value;
                                portAtrribute.IsReadOnly = portEvent.IsReadOnly;
                                portAtrribute.IsViewOnly = portEvent.IsViewOnly;
                                portAtrribute.SortIndex = portEvent.SortIndex;
                                portAtrribute.IsDeleted = portEvent.IsDeleted;
                                portAtrribute.EventDate = portEvent.EventDate;
                                portAtrribute.PpfId = portEvent.PpfId;

                                FuelRobModel.RobDetails.Add(portAtrribute);
                            }
                            FuelRobModel.Title = fuelTypeTitleDescById.ContainsKey(item.Key) ? fuelTypeTitleDescById[item.Key] : null;

                            FuelRobList.Add(FuelRobModel);
                        }
                    }
                }

                if (FuelRobList != null && FuelRobList.Any())
                {
                    List<string> nameList = new List<string> { string.Empty };

                    nameList.AddRange(FuelRobList.First().RobDetails.Select(obj => GetConsumptionCategoryAttributeDescription(obj.AttributeId, _consumptionCategory)).ToList());

                    result.FuelRobBreakDownNameList = nameList;

                    FuelRobList.ToList().ForEach(obj =>
                    {
                        obj.ListOfFuel = null;
                        SetROBMismatchValue(obj);
                    });
                }
            }
            return FuelRobList;
        }

        /// <summary>
        /// Sets the lube oil details for port call.
        /// </summary>
        /// <param name="lubeOilTemplate">The lube oil template.</param>
        /// <param name="lubeOilDetails">The lube oil details.</param>
        /// <returns></returns>
        private async Task<List<PortCallFuelRobDetailsViewModel>> SetLubeOilDetailsForPortCall(PortEventRobSummaryRequestViewModel input, List<PortEventAttributeDetail> lubeOilDetails, PortEventDetailsViewModel result)
        {
            List<PortCallFuelRobDetailsViewModel> FuelRobList = new List<PortCallFuelRobDetailsViewModel>();

            PortEventRobSummaryRequest request = new PortEventRobSummaryRequest();
            request.PosId = input.PosId;
            request.PpfId = input.PpfId;
            request.PsfId = input.PsfId;
            request.VesselId = GetVesselId(input.EncryptedVesselDetail);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetLubOilRobForPortEvent"));
            List<PortEventAttributeDetail> lubeOilTemplate = await PostAsync<List<PortEventAttributeDetail>>(requestUrl, CreateHttpContent(request));

            if (lubeOilTemplate != null && lubeOilTemplate.Any() && lubeOilDetails != null && lubeOilDetails.Any())
            {
                List<Lookup> _lubeOilTypes = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.LubOilType });
                List<Lookup> _consumptionCategory = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.LubOilROBBreakDown });

                List<IGrouping<string, PortEventAttributeDetail>> groupByRobTypeIdList = null;

                // MAPPING DATA IN TEMPLATE FOR EDIT TO INITIALISE ROB DETAILS

                lubeOilTemplate.ForEach(template =>
                {
                    PortEventAttributeDetail entity = lubeOilDetails.FirstOrDefault(currentDetail => template.AttributeId == currentDetail.AttributeId && template.RobTypeId == currentDetail.RobTypeId);
                    if (entity != null)
                    {
                        template.PpeId = entity.PpeId;
                        template.Value = (template.AttributeId == EnumsHelper.GetKeyValue(LubOilROBBreakDown.PreviousROB) || template.AttributeId == EnumsHelper.GetKeyValue(LubOilROBBreakDown.CurrentRob)) ? (entity.Value ?? 0) : entity.Value;
                    }
                    else
                    {
                        template.Value = (template.AttributeId == EnumsHelper.GetKeyValue(LubOilROBBreakDown.PreviousROB) || (template.AttributeId == EnumsHelper.GetKeyValue(LubOilROBBreakDown.CurrentRob) && template.IsReadOnly)) ? (template.Value ?? 0) : template.Value;
                    }
                });
                groupByRobTypeIdList = lubeOilTemplate.GroupBy(obj => obj.RobTypeId).ToList();

                Dictionary<string, string> fuelTypeTitleById = _lubeOilTypes != null ? _lubeOilTypes.ToDictionary(fuelType => fuelType.Identifier, fuelType => fuelType.Description) : new Dictionary<string, string>();

                if (groupByRobTypeIdList != null)
                {
                    foreach (string robTypeId in fuelTypeTitleById.Keys)
                    {
                        PortCallFuelRobDetailsViewModel FuelRobModel = new PortCallFuelRobDetailsViewModel();
                        FuelRobModel.RobDetails = new List<PortEventAttributeDetailViewModel>();
                        IGrouping<string, PortEventAttributeDetail> item = groupByRobTypeIdList.FirstOrDefault(obj => obj.Key == robTypeId);

                        if (item != null)
                        {
                            var sortedList = item.OrderBy(x => x.SortIndex).ToList();
                            if (result.IsEospFaopModeEnable && !result.IsConsumptionModeEnable)
                            {
                                PortEventAttributeDetail tankCapacity = sortedList.FirstOrDefault(obj => obj.AttributeId == EnumsHelper.GetKeyValue(LubOilROBBreakDown.TankCapacity));
                                if (tankCapacity != null)
                                {
                                    sortedList.Remove(tankCapacity);
                                }
                            }

                            foreach (var portEvent in sortedList)
                            {
                                PortEventAttributeDetailViewModel portAtrribute = new PortEventAttributeDetailViewModel();
                                portAtrribute.RobTypeId = portEvent.AttributeId;
                                portAtrribute.PpeId = portEvent.PpeId;
                                portAtrribute.AttributeId = portEvent.AttributeId;
                                portAtrribute.Value = portEvent.Value;
                                portAtrribute.IsReadOnly = portEvent.IsReadOnly;
                                portAtrribute.IsViewOnly = portEvent.IsViewOnly;
                                portAtrribute.SortIndex = portEvent.SortIndex;
                                portAtrribute.IsDeleted = portEvent.IsDeleted;
                                portAtrribute.EventDate = portEvent.EventDate;
                                portAtrribute.PpfId = portEvent.PpfId;
                                FuelRobModel.RobDetails.Add(portAtrribute);
                            }
                            FuelRobModel.Title = fuelTypeTitleById.ContainsKey(item.Key) ? fuelTypeTitleById[item.Key] : null;

                            FuelRobList.Add(FuelRobModel);
                        }
                    }

                    if (FuelRobList != null && FuelRobList.Any())
                    {
                        List<string> nameList = new List<string> { string.Empty };

                        nameList.AddRange(FuelRobList.First().RobDetails.Select(obj => GetConsumptionCategoryAttributeDescription(obj.AttributeId, _consumptionCategory)).ToList());

                        result.LubOilBreakDownNameList = nameList;
                    }
                }
            }
            return FuelRobList;
        }

        /// <summary>
        /// Gets the consumption category attribute description.
        /// </summary>
        /// <param name="attributeId">The attribute identifier.</param>
        /// <param name="_consumptionCategoryAttributes">The consumption category attributes.</param>
        /// <returns></returns>
        private string GetConsumptionCategoryAttributeDescription(string attributeId, List<Lookup> _consumptionCategoryAttributes)
        {
            return _consumptionCategoryAttributes != null && _consumptionCategoryAttributes.Any(breakDown => Equals(breakDown.Identifier, attributeId)) ?
                    _consumptionCategoryAttributes.First(breakDown => Equals(breakDown.Identifier, attributeId)).Description : null;
        }

        /// <summary>
        /// Sets the rob mismatch value.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void SetROBMismatchValue(PortCallFuelRobDetailsViewModel obj)
        {
            PortEventAttributeDetailViewModel eospDetail = obj.RobDetails != null ? obj.RobDetails.FirstOrDefault(x => x.AttributeId == EnumsHelper.GetKeyValue(ConsumptionCategoryAttribute.EOSP)) : null;
            PortEventAttributeDetailViewModel faopDetail = obj.RobDetails != null ? obj.RobDetails.FirstOrDefault(x => x.AttributeId == EnumsHelper.GetKeyValue(ConsumptionCategoryAttribute.TotalROB)) : null;
            if (eospDetail != null)
            {
                bool bunkerDetailNotExists = faopDetail != null && (obj.ListOfFuel == null || CheckBunkerDetailNotExists(faopDetail.RobTypeId, obj));

                obj.IsRobMismatch = bunkerDetailNotExists && eospDetail != null && faopDetail != null && faopDetail.Value.GetValueOrDefault() > eospDetail.Value.GetValueOrDefault();
                obj.IsRobMismatchDetailsAdded = !bunkerDetailNotExists && eospDetail != null && faopDetail != null && faopDetail.Value.GetValueOrDefault() > eospDetail.Value.GetValueOrDefault();
            }
        }

        /// <summary>
        /// Checks the bunker detail not exists.
        /// </summary>
        /// <param name="robTypeId">The rob type identifier.</param>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        private bool CheckBunkerDetailNotExists(string robTypeId, PortCallFuelRobDetailsViewModel obj)
        {
            obj.ROBDetailsReason = null;
            if (obj.ListOfFuel.Any(x => x == robTypeId))
            {
                obj.ROBDetailsReason = "Added Bunker Tank(s) Quantity Survey Event";
                return false;
            }
            else
            {
                obj.ROBDetailsReason = "Added bunkering quantity in Bunkering Tab.";
                if (robTypeId == EnumsHelper.GetKeyValue(FuelType.FO))
                {
                    return !obj.ListOfFuel.Any(x => x == EnumsHelper.GetDescription(OillGradeTypeSpecificationForVoyage.FO));
                }
                else if (robTypeId == EnumsHelper.GetKeyValue(FuelType.LSFO))
                {
                    return !obj.ListOfFuel.Any(x => x == EnumsHelper.GetDescription(OillGradeTypeSpecificationForVoyage.LSFO));
                }
                else if (robTypeId == EnumsHelper.GetKeyValue(FuelType.DO))
                {
                    return !obj.ListOfFuel.Any(x => x == EnumsHelper.GetKeyValue(OillGradeTypeSpecificationForVoyage.DO));
                }
                else if (robTypeId == EnumsHelper.GetKeyValue(FuelType.GO))
                {
                    return !obj.ListOfFuel.Any(x => x == EnumsHelper.GetKeyValue(OillGradeTypeSpecificationForVoyage.GO));
                }
                else if (robTypeId == EnumsHelper.GetKeyValue(FuelType.LNG))
                {
                    return !obj.ListOfFuel.Any(x => x == EnumsHelper.GetKeyValue(OillGradeTypeSpecificationForVoyage.LNG));
                }
            }

            return true;
        }

        /// <summary>
        /// The Set Vessel Draft
        /// </summary>
        /// <param name="result"></param>
        /// <param name="portEventDetails"></param>
        private void SetVesselDraft(PortEventDetailsViewModel result, PortEventDetails portEventDetails)
        {
            result.AftDraft = portEventDetails.AftDraft;
            result.ForwardDraft = portEventDetails.ForwardDraft;
            result.MidDraft = portEventDetails.MidDraft;
            result.MeanDraft = portEventDetails.MeanDraft;
        }

        /// <summary>
        /// The set fresh water rob details
        /// </summary>
        /// <param name="input"></param>
        /// <param name="freshWaterRobDetails"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task<List<PortCallFuelRobDetailsViewModel>> SetFreshWaterRobDetails(PortEventRobSummaryRequestViewModel input, List<PortEventAttributeDetail> freshWaterRobDetails, PortEventDetailsViewModel result)
        {
            List<PortCallFuelRobDetailsViewModel> freshWaterRobList = new List<PortCallFuelRobDetailsViewModel>();

            PortEventRobSummaryRequest request = new PortEventRobSummaryRequest();
            request.PosId = input.PosId;
            request.PpfId = input.PpfId;
            request.PsfId = input.PsfId;
            request.VesselId = GetVesselId(input.EncryptedVesselDetail);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetFeshWaterRobForPortEvent"));
            List<PortEventAttributeDetail> freshWaterRobTemplate = await PostAsync<List<PortEventAttributeDetail>>(requestUrl, CreateHttpContent(request));

            if (freshWaterRobTemplate != null && freshWaterRobTemplate.Any() && freshWaterRobDetails != null && freshWaterRobDetails.Any())
            {
                List<Lookup> _freshWaterTypes = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.FreshWaterType });
                List<Lookup> _consumptionCategory = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.FreshWaterROBBreakDown });

                List<IGrouping<string, PortEventAttributeDetail>> groupByRobTypeIdList = null;
                // MAPPING DATA IN TEMPLATE FOR EDIT TO INITIALISE ROB DETAILS
                if (freshWaterRobTemplate != null && freshWaterRobTemplate.Any() && freshWaterRobDetails != null && freshWaterRobDetails.Any())
                {
                    freshWaterRobTemplate.ForEach(template =>
                    {
                        PortEventAttributeDetail entity = freshWaterRobDetails.FirstOrDefault(currentDetail => template.AttributeId == currentDetail.AttributeId && template.RobTypeId == currentDetail.RobTypeId);
                        if (entity != null)
                        {
                            template.PpeId = entity.PpeId;
                            template.Value = (template.AttributeId == EnumsHelper.GetKeyValue(FreshWaterROBBreakDown.PreviousROB) || template.AttributeId == EnumsHelper.GetKeyValue(FreshWaterROBBreakDown.CurrentRob)) ? (entity.Value ?? 0) : entity.Value;
                        }
                        else
                        {
                            template.Value = (template.AttributeId == EnumsHelper.GetKeyValue(FreshWaterROBBreakDown.PreviousROB) || (template.AttributeId == EnumsHelper.GetKeyValue(FreshWaterROBBreakDown.CurrentRob) && template.IsReadOnly)) ? (template.Value ?? 0) : template.Value;
                        }
                    });
                    groupByRobTypeIdList = freshWaterRobTemplate.GroupBy(obj => obj.RobTypeId).ToList();
                }

                Dictionary<string, string> fuelTypeTitleById = _freshWaterTypes != null ?
                                                                   _freshWaterTypes.ToDictionary(fwType => fwType.Identifier, fwType => fwType.Description) :
                                                                   new Dictionary<string, string>();
                if (groupByRobTypeIdList != null)
                {
                    foreach (string robTypeId in fuelTypeTitleById.Keys)
                    {
                        PortCallFuelRobDetailsViewModel FuelRobModel = new PortCallFuelRobDetailsViewModel();
                        FuelRobModel.RobDetails = new List<PortEventAttributeDetailViewModel>();
                        IGrouping<string, PortEventAttributeDetail> item = groupByRobTypeIdList.FirstOrDefault(obj => obj.Key == robTypeId);

                        if (item != null)
                        {
                            var sortedList = item.OrderBy(x => x.SortIndex).ToList();
                            if (result.IsEospFaopModeEnable || result.IsConsumptionModeEnable)
                            {
                                PortEventAttributeDetail tankCapacity = sortedList.FirstOrDefault(obj => obj.AttributeId == EnumsHelper.GetKeyValue(LubOilROBBreakDown.TankCapacity));
                                if (tankCapacity != null)
                                {
                                    sortedList.Remove(tankCapacity);
                                }
                            }
                            foreach (var portEvent in sortedList)
                            {
                                PortEventAttributeDetailViewModel portAtrribute = new PortEventAttributeDetailViewModel();
                                portAtrribute.RobTypeId = portEvent.AttributeId;
                                portAtrribute.PpeId = portEvent.PpeId;
                                portAtrribute.AttributeId = portEvent.AttributeId;
                                portAtrribute.Value = portEvent.Value;
                                portAtrribute.IsReadOnly = portEvent.IsReadOnly;
                                portAtrribute.IsViewOnly = portEvent.IsViewOnly;
                                portAtrribute.SortIndex = portEvent.SortIndex;
                                portAtrribute.IsDeleted = portEvent.IsDeleted;
                                portAtrribute.EventDate = portEvent.EventDate;
                                portAtrribute.PpfId = portEvent.PpfId;

                                FuelRobModel.RobDetails.Add(portAtrribute);
                            }
                            FuelRobModel.Title = fuelTypeTitleById.ContainsKey(item.Key) ? fuelTypeTitleById[item.Key] : null;

                            freshWaterRobList.Add(FuelRobModel);
                        }
                    }
                }
                if (freshWaterRobList != null && freshWaterRobList.Any())
                {
                    List<string> nameList = new List<string> { string.Empty };

                    nameList.AddRange(freshWaterRobList.First().RobDetails.Select(obj => GetConsumptionCategoryAttributeDescription(obj.AttributeId, _consumptionCategory)).ToList());

                    result.FreshWaterBreakDownNameList = nameList;
                }
            }

            return freshWaterRobList;
        }

        public async Task<List<PortCallFuelRobDetailsViewModel>> SetWasteRobDetails(PortEventRobSummaryRequestViewModel input, List<PortEventAttributeDetail> wasteRobDetails, PortEventDetailsViewModel result)
        {
            List<PortCallFuelRobDetailsViewModel> wasteRobList = new List<PortCallFuelRobDetailsViewModel>();

            PortEventRobSummaryRequest request = new PortEventRobSummaryRequest();
            request.PosId = input.PosId;
            request.PpfId = input.PpfId;
            request.PsfId = input.PsfId;
            request.VesselId = GetVesselId(input.EncryptedVesselDetail);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetWasteRobForPortEvent"));
            List<PortEventAttributeDetail> wasteRobTemplate = await PostAsync<List<PortEventAttributeDetail>>(requestUrl, CreateHttpContent(request));

            if (wasteRobTemplate != null && wasteRobTemplate.Any() && wasteRobDetails != null && wasteRobDetails.Any())
            {
                List<Lookup> _wasteTypes = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.WasteType });
                List<Lookup> _consumptionCategory = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.WasteROBBreakDown });

                List<IGrouping<string, PortEventAttributeDetail>> groupByRobTypeIdList = null;
                // MAPPING DATA IN TEMPLATE FOR EDIT TO INITIALISE ROB DETAILS
                if (wasteRobTemplate != null && wasteRobTemplate.Any() && wasteRobDetails != null && wasteRobDetails.Any())
                {
                    wasteRobTemplate.ForEach(template =>
                    {
                        PortEventAttributeDetail entity = wasteRobDetails.FirstOrDefault(currentDetail => template.AttributeId == currentDetail.AttributeId && template.RobTypeId == currentDetail.RobTypeId);
                        if (entity != null)
                        {
                            template.PpeId = entity.PpeId;
                            template.Value = (template.AttributeId == EnumsHelper.GetKeyValue(WasteROBBreakDown.PreviousROB) || template.AttributeId == EnumsHelper.GetKeyValue(WasteROBBreakDown.CurrentRob)) ? (entity.Value ?? 0) : entity.Value;
                        }
                        else
                        {
                            template.Value = (template.AttributeId == EnumsHelper.GetKeyValue(WasteROBBreakDown.PreviousROB) || (template.AttributeId == EnumsHelper.GetKeyValue(WasteROBBreakDown.CurrentRob) && template.IsReadOnly)) ? (template.Value ?? 0) : template.Value;
                        }
                    });
                    groupByRobTypeIdList = wasteRobTemplate.GroupBy(obj => obj.RobTypeId).ToList();
                }
                Dictionary<string, string> fuelTypeTitleById = _wasteTypes != null ? _wasteTypes.ToDictionary(fuelType => fuelType.Identifier, fuelType => fuelType.Description) : new Dictionary<string, string>();

                if (groupByRobTypeIdList != null)
                {

                    foreach (string robTypeId in fuelTypeTitleById.Keys)
                    {
                        PortCallFuelRobDetailsViewModel FuelRobModel = new PortCallFuelRobDetailsViewModel();
                        FuelRobModel.RobDetails = new List<PortEventAttributeDetailViewModel>();
                        IGrouping<string, PortEventAttributeDetail> item = groupByRobTypeIdList.FirstOrDefault(obj => obj.Key == robTypeId);

                        if (item != null)
                        {

                            var sortedList = item.OrderBy(x => x.SortIndex).ToList();
                            if (result.IsEospFaopModeEnable && !result.IsConsumptionModeEnable)
                            {
                                PortEventAttributeDetail tankCapacity = sortedList.FirstOrDefault(obj => obj.AttributeId == EnumsHelper.GetKeyValue(LubOilROBBreakDown.TankCapacity));
                                if (tankCapacity != null)
                                {
                                    sortedList.Remove(tankCapacity);
                                }
                            }

                            foreach (var portEvent in sortedList)
                            {
                                PortEventAttributeDetailViewModel portAtrribute = new PortEventAttributeDetailViewModel();
                                portAtrribute.RobTypeId = portEvent.AttributeId;
                                portAtrribute.PpeId = portEvent.PpeId;
                                portAtrribute.AttributeId = portEvent.AttributeId;
                                portAtrribute.Value = portEvent.Value;
                                portAtrribute.IsReadOnly = portEvent.IsReadOnly;
                                portAtrribute.IsViewOnly = portEvent.IsViewOnly;
                                portAtrribute.SortIndex = portEvent.SortIndex;
                                portAtrribute.IsDeleted = portEvent.IsDeleted;
                                portAtrribute.EventDate = portEvent.EventDate;
                                portAtrribute.PpfId = portEvent.PpfId;

                                FuelRobModel.RobDetails.Add(portAtrribute);
                            }
                            FuelRobModel.Title = fuelTypeTitleById.ContainsKey(item.Key) ? fuelTypeTitleById[item.Key] : null;

                            wasteRobList.Add(FuelRobModel);
                        }
                    }
                }

                if (wasteRobList != null && wasteRobList.Any())
                {
                    List<string> nameList = new List<string> { string.Empty };

                    nameList.AddRange(wasteRobList.First().RobDetails.Select(obj => GetConsumptionCategoryAttributeDescription(obj.AttributeId, _consumptionCategory)).ToList());

                    result.WasteRobBreakDownNameList = nameList;
                }
            }

            return wasteRobList;
        }

        /// <summary>
        /// Gets the ballast template.
        /// </summary>
        /// <returns> List<PortEventAttributeDetail> </returns>
        private List<PortEventAttributeDetail> GetBallastTemplate()
        {
            List<PortEventAttributeDetail> ballastTemplateList = new List<PortEventAttributeDetail>();
            foreach (string ballastTypeId in Enum.GetValues(typeof(BallastType)).OfType<BallastType>().Select(attribute => EnumsHelper.GetKeyValue(attribute)))
            {
                ballastTemplateList.Add(new PortEventAttributeDetail()
                {
                    RobTypeId = ballastTypeId,
                    AttributeId = EnumsHelper.GetKeyValue(BallastBreakDownAttribute.FinalQty)
                });
            }
            return ballastTemplateList;
        }

        /// <summary>
        /// The set ballast details
        /// </summary>
        /// <param name="eventDetailsTemplate"></param>
        /// <param name="eventListDetails"></param>
        /// <returns></returns>
        private async Task<List<BallastDetailViewModel>> SetBallastDetails(List<PortEventAttributeDetail> eventDetailsTemplate, List<PortEventAttributeDetail> eventListDetails)
        {
            if (eventDetailsTemplate != null && eventDetailsTemplate.Any() && eventListDetails != null && eventListDetails.Any())
            {
                eventDetailsTemplate.ForEach(template =>
                {
                    PortEventAttributeDetail entity = eventListDetails.FirstOrDefault(currentDetail => template.AttributeId == currentDetail.AttributeId && template.RobTypeId == currentDetail.RobTypeId);
                    if (entity != null)
                    {
                        template.PpeId = entity.PpeId;
                        template.Value = entity.Value;
                    }
                });
            }
            List<Lookup> _ballastWaterType = await GetRouteChangedReasonList(new List<PosAttributeLookupCode>() { PosAttributeLookupCode.BallastWaterType });
            List<BallastDetailViewModel> BallastRobList = new List<BallastDetailViewModel>();
            if (eventDetailsTemplate != null && eventDetailsTemplate.Any())
            {
                foreach (var portEvent in eventDetailsTemplate)
                {
                    BallastDetailViewModel BallastRob = new BallastDetailViewModel();
                    BallastRob.Title = GetBallastTypeDescription(portEvent.RobTypeId, _ballastWaterType);
                    BallastRob.IsEospFaopModeEnable = true;

                    PortEventAttributeDetailViewModel portAtrribute = new PortEventAttributeDetailViewModel();
                    portAtrribute.RobTypeId = portEvent.AttributeId;
                    portAtrribute.PpeId = portEvent.PpeId;
                    portAtrribute.AttributeId = portEvent.AttributeId;
                    portAtrribute.Value = portEvent.Value;
                    portAtrribute.IsReadOnly = portEvent.IsReadOnly;
                    portAtrribute.IsViewOnly = portEvent.IsViewOnly;
                    portAtrribute.SortIndex = portEvent.SortIndex;
                    portAtrribute.IsDeleted = portEvent.IsDeleted;
                    portAtrribute.EventDate = portEvent.EventDate;
                    portAtrribute.PpfId = portEvent.PpfId;

                    BallastRob.RobDetails = portAtrribute;

                    BallastRobList.Add(BallastRob);
                }
            }

            return BallastRobList;
        }

        /// <summary>
        /// The get ballast type description
        /// </summary>
        /// <param name="robTypeId"></param>
        /// <param name="_ballastTypes"></param>
        /// <returns></returns>
        private string GetBallastTypeDescription(string robTypeId, List<Lookup> _ballastTypes)
        {
            return _ballastTypes != null && _ballastTypes.Any(breakDown => Equals(breakDown.Identifier, robTypeId)) ?
                    _ballastTypes.First(breakDown => Equals(breakDown.Identifier, robTypeId)).Description : null;
        }

        /// <summary>
        /// The GetRunningHourListAsync
        /// </summary>
        /// <param name="input"></param>
        /// <param name="portEventDetails"></param>
        /// <returns></returns>
        private async Task<List<PortEngineRunningHours>> GetRunningHourListAsync(PortEventRobSummaryRequestViewModel input, PortEventDetails portEventDetails)
        {
            List<PortEngineRunningHours> runningHrsList = new List<PortEngineRunningHours>();
            var value = new Dictionary<string, object>()
            {
                { "PosId", input.PosId },
                { "PsfId", input.PsfId },
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetPrevRunningHrsForPortEvent"));
            List<PortEngineRunningHours> RunningHours = await PostAsync<List<PortEngineRunningHours>>(requestUrl, CreateHttpContent(value));

            // for Edit : mapping previous data on saved running hrs details
            if (portEventDetails != null && !string.IsNullOrWhiteSpace(portEventDetails.PsfId) &&
                portEventDetails != null && portEventDetails.EngineRunningHours != null && portEventDetails.EngineRunningHours.Any())
            {
                portEventDetails.EngineRunningHours.ForEach(obj =>
                {
                    PortEngineRunningHours prev = RunningHours != null && RunningHours.Any() ? RunningHours.FirstOrDefault(prevHrs => prevHrs.PartName == obj.PartName) : null;
                    obj.PreviousHours = prev != null ? prev.PreviousHours.GetValueOrDefault() : default(decimal);
                    obj.Total = obj.Total.GetValueOrDefault();
                    obj.Date = portEventDetails.FromDate;
                });
                runningHrsList = portEventDetails.EngineRunningHours;
            }

            return runningHrsList;
        }

        /// <summary>
        /// the SetRunningHourList
        /// </summary>
        /// <param name="runningHourList"></param>
        /// <returns></returns>
        private List<PortEngineRunningHoursViewModel> SetRunningHourList(List<PortEngineRunningHours> runningHourList)
        {
            List<PortEngineRunningHoursViewModel> RunningHoursList = new List<PortEngineRunningHoursViewModel>();
            if (runningHourList != null && runningHourList.Any())
            {
                foreach (var item in runningHourList)
                {
                    PortEngineRunningHoursViewModel RunningHours = new PortEngineRunningHoursViewModel();
                    RunningHours.PartName = item.PartName;
                    RunningHours.PreviousHours = item.PreviousHours;
                    RunningHours.Total = item.Total;
                    RunningHours.Date = item.Date;
                    RunningHoursList.Add(RunningHours);
                }
            }
            return RunningHoursList;
        }

        /// <summary>
        /// Get Vessel Communications
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<VesselCommunicationDetailViewModel>> GetVesselCommunications(string vesselId)
        {
            List<VesselCommunicationDetailViewModel> response = new List<VesselCommunicationDetailViewModel>();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
            string VesselId = decreptedString.Split(Constants.Separator)[0];
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/vessel/GetVesselCommunications/" + VesselId));
            List<VesselCommunicationDetail> result = await GetAsync<List<VesselCommunicationDetail>>(requestUrl);

            if (result != null && result.Any())
            {
                foreach (VesselCommunicationDetail item in result)
                {
                    response.Add(new VesselCommunicationDetailViewModel()
                    {
                        ComId = item.ComId,
                        ComDeleted = item.ComDeleted,
                        ComDesc = !string.IsNullOrWhiteSpace(item.ComDesc) ? item.ComDesc : "",
                        ComExpiryDate = item.ComExpiryDate,
                        ComNumber = item.ComNumber,
                        ComPrimaryContact = item.ComPrimaryContact,
                        PrimaryContact = item.ComPrimaryContact.GetValueOrDefault() ? "Yes" : "No",
                        ComProvider = item.ComProvider,
                        ComStartDate = item.ComStartDate,
                        CtyId = item.CtyId,
                        VesId = item.VesId,
                        CmpName = !string.IsNullOrWhiteSpace(item.CmpName) ? item.CmpName : "",
                        CtyName = !string.IsNullOrWhiteSpace(item.CtyName) ? item.CtyName : "",
                        IsEmail = !string.IsNullOrWhiteSpace(item.ComNumber) && item.ComNumber.Contains("@"),
                    });
                }
            }
            return response;
        }

        /// <summary>
        /// The add edit Manoeruving view
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PortEventDetailsViewModel> GetPortEventManoeuvringDetails(PortEventRobSummaryRequestViewModel input)
        {
            PortEventDetailsViewModel result = new PortEventDetailsViewModel();
            var response = await GetPortEventDetailsAsync(input.PsfId);
            if (response != null)
            {
                result = await SetPortEventHeaderDetails(response);
                result.IsEospFaopModeEnable = false;
                result.IsConsumptionModeEnable = false;
                result.IsInBound = response.IsInBound;
                SetVesselDraft(result, response);
                result.FuelRobBreakDown = await SetFuelRobDetailsForPortCall(input, response.FuelRobBreakDown, result);
                result.IsConsumptionModeEnable = true;
                result.FreshWaterRobBreakDown = await SetFreshWaterRobDetails(input, response.FreshWaterRobBreakDown, result);
                result.LubOilBreakDown = await SetLubeOilDetailsForPortCall(input, response.LubOilBreakDown, result);
                result.WasteRobBreakDown = await SetWasteRobDetails(input, response.WasteRobBreakDown, result);
                List<PortEngineRunningHours> runningHourList = await GetRunningHourListAsync(input, response);
                result.EngineRunningHours = SetRunningHourList(runningHourList);
            }
            return result;
        }

        /// <summary>
        /// Gets the system area.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<string>> GetJSAControlRights(string jobId)
        {
            string queryString = jobId;
            List<string> response = new List<string>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/JSA/GetJSAControlRights/" + jobId));
            response = await GetAsync<List<string>>(requestUrl);

            return response;
        }

        /// <summary>
        /// Changes the jsa status.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="remark">The remark.</param>
        /// <param name="jsaStatus">The jsa status.</param>
        /// <returns></returns>
        public async Task<bool> ChangeJSAStatus(string jobId, string remark, JSAStatus jsaStatus)
        {
            bool isJSAStatusChanged = false;
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("jobId", jobId);
            keyValues.Add("remark", remark);
            keyValues.Add("status", jsaStatus);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/JSA/ChangeJSAStatus"));
            isJSAStatusChanged = await PostAsync<bool>(requestUrl, CreateHttpContent(keyValues));

            return isJSAStatusChanged;
        }

        /// <summary>
        /// Changes the jsa office comments.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public async Task<bool> ChangeJSAOfficeComments(string jobId, string comment)
        {
            bool isCommentsAdded = false;
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("jobId", jobId);
            keyValues.Add("officeComments", comment);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/JSA/ChangeJSAOfficeComments"));
            isCommentsAdded = await PostAsync<bool>(requestUrl, CreateHttpContent(keyValues));

            return isCommentsAdded;
        }
    }
}
