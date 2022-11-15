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
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Crew;
using PWAFeaturesRnd.ViewModels.Crew;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// Crew Client
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class CrewClient : BaseHttpClient
    {
        #region Private Properties

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
        /// Initializes a new instance of the <see cref="CrewClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public CrewClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider, IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
        {
            client.BaseAddress = new Uri(AppSettings.CrewWebApiUrl);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        #endregion

        #region Dashboard - Methods

        /// <summary>
        /// Posts the get crew summary.
        /// </summary>
        /// <param name="crewRequest">The crew request.</param>
        /// <returns></returns>
        public async Task<CrewSummaryResponseViewModel> PostGetCrewSummary(CrewSummaryRequest crewRequest)
        {
            CrewSummaryResponseViewModel CrewSummary = new CrewSummaryResponseViewModel();
            CrewSummaryResponse response = new CrewSummaryResponse();

            if (crewRequest != null)
            {
                //prop for unplanned and overdue to date
                var localUnplannedBerthDate = DateTime.Now.AddDays(30);
                var unplanneBerthdDate = new DateTime(localUnplannedBerthDate.Year, localUnplannedBerthDate.Month, localUnplannedBerthDate.Day, 23, 59, 59);
                crewRequest.UnplannedToDate = unplanneBerthdDate;

                crewRequest.OverdueToDate = DateTime.Now.AddDays(Constants.CrewOverdueXDays).Date;

                var fromDate = DateTime.Now.AddMonths(-3);
                var medicalSignOffFrom = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);
                crewRequest.FromMedicalSignOff = medicalSignOffFrom;

                var toDate = DateTime.Now;
                var medicalSignOffTo = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59);
                crewRequest.ToMedicalSignOff = medicalSignOffTo;

                //crew change last 7 days
                var crewChangeLast7Days = DateTime.Now.AddDays(Constants.CrewChangeLastXDays);
                crewRequest.CrewChangeFromDate = crewChangeLast7Days;

                crewRequest.MedicalSignOffPriorityLimit = 0;
                crewRequest.OverduePriorityLimit = 0;
                crewRequest.UnplannedBirthPriorityLimit = 0;
            }
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/Summary"));

            response = await PostAsync<CrewSummaryResponse>(requestUrl, CreateHttpContent(crewRequest));

            if (response != null)
            {
                CrewSummary.MedicalSignOffCount = response.MedicalSignOffCount.GetValueOrDefault();
                CrewSummary.OfficerPromNewHireCount = response.OfficerPromNewHireCount.GetValueOrDefault();
                CrewSummary.OnboardCount = response.OnboardCount.GetValueOrDefault();
                CrewSummary.OverdueCount = response.OverdueCount.GetValueOrDefault();
                CrewSummary.Top4SignOnCount = response.SignOnCount.GetValueOrDefault();
                CrewSummary.UnplannedBerthCount = response.UnplannedBerthCount.GetValueOrDefault();
                CrewSummary.CrewChangeCount = response.CrewChangeCount;

                //priorities
                CrewSummary.CrewChangePriority = response.CrewChangePriority;
                CrewSummary.MedicalSignOffPriority = response.MedicalSignOffPriority;
                CrewSummary.OfficerPromNewHirePriority = response.OfficerPromNewHirePriority;
                CrewSummary.OnboardPriority = response.OnboardPriority;
                CrewSummary.OverduePriority = response.OverduePriority;
                CrewSummary.SignOnPriority = response.SignOnPriority;
                CrewSummary.UnplannedBerthPriority = response.UnplannedBerthPriority;


                CrewListViewModel medicalSignOff = new CrewListViewModel();
                medicalSignOff.ToMedicalSignOff = DateTime.Now;
                medicalSignOff.FromMedicalSignOff = DateTime.Now.AddMonths(-3);
                medicalSignOff.ActiveMobileTabClass = Constants.Tab2;
                CrewSummary.MedicalSignOffURL = SetupCrewNavigationURL(medicalSignOff);

                CrewListViewModel officerProm = new CrewListViewModel();
                officerProm.ToDate = DateTime.Now;
                officerProm.FromDate = DateTime.Now;
                officerProm.GridSubTitle = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
                officerProm.SelectedFilter = CrewStageFilter.Onboard;
                officerProm.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.Onboard);
                officerProm.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
                officerProm.ActiveMobileTabClass = Constants.Tab2;
                CrewSummary.OfficerPromNewHireURL = SetupCrewNavigationURL(officerProm);

                CrewListViewModel OnBoard = new CrewListViewModel();
                OnBoard.ToDate = DateTime.Now;
                OnBoard.FromDate = DateTime.Now;
                OnBoard.GridSubTitle = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
                OnBoard.SelectedFilter = CrewStageFilter.Onboard;
                OnBoard.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.Onboard);
                OnBoard.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
                OnBoard.ActiveMobileTabClass = Constants.Tab2;
                CrewSummary.OnboardURL = SetupCrewNavigationURL(OnBoard);

                CrewListViewModel Overdue = new CrewListViewModel();
                Overdue.ToDate = DateTime.Now;
                Overdue.FromDate = DateTime.Now;
                Overdue.GridSubTitle = EnumsHelper.GetDescription(CrewStageFilter.Overdue);
                Overdue.SelectedFilter = CrewStageFilter.Overdue;
                Overdue.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.Overdue);
                Overdue.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.Overdue);
                Overdue.ActiveMobileTabClass = Constants.Tab2;
                CrewSummary.OverdueURL = SetupCrewNavigationURL(Overdue);

                CrewListViewModel Top4SignOn = new CrewListViewModel();
                Top4SignOn.ToDate = DateTime.Now;
                Top4SignOn.FromDate = DateTime.Now;
                Top4SignOn.GridSubTitle = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
                Top4SignOn.SelectedFilter = CrewStageFilter.Onboard;
                Top4SignOn.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.Onboard);
                Top4SignOn.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
                Top4SignOn.ActiveMobileTabClass = Constants.Tab2;
                CrewSummary.Top4SignOnURL = SetupCrewNavigationURL(Top4SignOn);

                CrewListViewModel unplannedBerth = new CrewListViewModel();
                unplannedBerth.ToDate = DateTime.Now;
                unplannedBerth.FromDate = DateTime.Now;
                unplannedBerth.GridSubTitle = EnumsHelper.GetDescription(CrewStageFilter.UnplannedBerth);
                unplannedBerth.SelectedFilter = CrewStageFilter.UnplannedBerth;
                unplannedBerth.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.UnplannedBerth);
                unplannedBerth.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.UnplannedBerth);
                unplannedBerth.ActiveMobileTabClass = Constants.Tab2;
                CrewSummary.UnplannedBerthURL = SetupCrewNavigationURL(unplannedBerth);

                CrewListViewModel viewMore = new CrewListViewModel();
                viewMore.ToDate = DateTime.Now;
                viewMore.FromDate = DateTime.Now;
                viewMore.GridSubTitle = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
                viewMore.SelectedFilter = CrewStageFilter.Onboard;
                viewMore.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.Onboard);
                viewMore.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
                viewMore.ActiveMobileTabClass = Constants.Tab1;
                CrewSummary.ViewMoreURL = SetupCrewNavigationURL(viewMore);

                CrewListViewModel ChangeLast7Days = new CrewListViewModel();
                ChangeLast7Days.ToDate = DateTime.Now;
                ChangeLast7Days.FromDate = DateTime.Now;
                ChangeLast7Days.GridSubTitle = EnumsHelper.GetDescription(CrewStageFilter.ChangeLastXDays);
                ChangeLast7Days.SelectedFilter = CrewStageFilter.Onboard;
                ChangeLast7Days.CrewChangeDate = DateTime.Now.AddDays(Constants.CrewChangeLastXDays);
                ChangeLast7Days.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.ChangeLastXDays);
                ChangeLast7Days.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.ChangeLastXDays);
                ChangeLast7Days.ActiveMobileTabClass = Constants.Tab2;
                CrewSummary.CrewChangeUrl = SetupCrewNavigationURL(ChangeLast7Days);
            }

            return CrewSummary;
        }

        /// <summary>
        /// Gets the crew experience matrix details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<CrewExperienceMatrixDetailsResponseViewModel>> GetCrewExperienceMatrixDetails(CrewExperienceMatrixDetailsRequestViewModel input)
        {
            CrewExperienceMatrixDetailsRequest request = new CrewExperienceMatrixDetailsRequest();
            List<CrewExperienceMatrixDetailsResponseViewModel> result = new List<CrewExperienceMatrixDetailsResponseViewModel>();

            string requestVesselId = GetDecryptedVesselId(input.VesselId);
            request.VesselIds = !string.IsNullOrWhiteSpace(requestVesselId) ? new List<string>() { requestVesselId } : null;
            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/PWACrewExperienceMatrixDetails"));
            List<CrewExperienceMatrixDetailsResponse> response = await PostAsync<List<CrewExperienceMatrixDetailsResponse>>(requestUrl, CreateHttpContent(request));

            CrewListViewModel viewMore = new CrewListViewModel();
            viewMore.ToDate = DateTime.Now;
            viewMore.FromDate = DateTime.Now;
            viewMore.SelectedFilter = CrewStageFilter.Onboard;
            viewMore.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.Onboard);
            viewMore.SelectedStageName = EnumsHelper.GetDescription(CrewStageFilter.Onboard);
            string viewMoreURL = SetupCrewNavigationURL(viewMore);

            if (response != null && response.Any())
            {
                foreach (CrewExperienceMatrixDetailsResponse item in response)
                {
                    CrewExperienceMatrixDetailsResponseViewModel crewExp = new CrewExperienceMatrixDetailsResponseViewModel();
                    crewExp.EncryptedVesselId = GetEncryptedVesselId(item.VesselId, item.VesselName, item.CoyId);
                    crewExp.EncryptedCrewURL = viewMoreURL;
                    crewExp.VesselName = item.VesselName;
                    crewExp.DepartmentName = item.DepartmentName;
                    crewExp.CrewName = item.CrewName;
                    crewExp.RankName = item.RankName;
                    crewExp.VmsExperienceYears = item.VmsExperienceYears;
                    crewExp.VmsExperienceInDays = item.VmsExperienceInDays;

                    var experienceInMonths = item.VmsExperienceInDays > 0 ? item.VmsExperienceInDays / Constants.DaysToMonth : 0;

                    if (experienceInMonths > 0)
                    {
                        string experienceInYearsAndMonths;

                        var experienceInYears = experienceInMonths / 12;
                        var remainingMonths = experienceInMonths % 12;

                        experienceInYearsAndMonths = experienceInYears > 0 ? experienceInYears + "y" : "";
                        experienceInYearsAndMonths += experienceInYears > 0 && remainingMonths > 0 ? " " : "";
                        experienceInYearsAndMonths += remainingMonths > 0 ? remainingMonths + "m" : "";

                        //1 year 3 months duration formatted as 1y 3m
                        crewExp.ExperienceInYearsAndMonths = experienceInYearsAndMonths;
                    }
                    else
                    {
                        crewExp.ExperienceInYearsAndMonths = "-";
                    }

                    result.Add(crewExp);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the crew fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<CrewFleetSummaryResponseViewModel> GetCrewFleetSummary(CrewFleetSummaryRequestViewModel input)
        {
            CrewFleetSummaryRequest request = new CrewFleetSummaryRequest();
            CrewFleetSummaryResponse response = new CrewFleetSummaryResponse();
            CrewFleetSummaryResponseViewModel fleetSummary = new CrewFleetSummaryResponseViewModel();

            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, input.VesselId);
            request.VesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/PWACrewDashboardFleetSummary"));
            response = await PostAsync<CrewFleetSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                fleetSummary.ExperienceMatrixVesselCount = response.ExperienceMatrixVesselCount;
                fleetSummary.ExperienceMatrixPriority = response.ExperienceMatrixPriority;
                fleetSummary.ExperienceMatrixInfo = response.ExperienceMatrixInfo;
            }
            return fleetSummary;
        }

        #endregion

        #region CrewDetails - Methods


        /// <summary>Posts the get crew service history.</summary>
        /// <param name="identifier">The crewId.</param>
        /// <returns>
        ///   ServiceHistoryViewModel
        /// </returns>
        public async Task<List<ServiceHistoryViewModel>> PostGetCrewServiceHistory(string identifier, bool canIncludeShore)
        {
            CrewServiceHistoryRequest request = new CrewServiceHistoryRequest
            {
                CrewId = identifier,
                CanIncludeShore = canIncludeShore

            };
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/CrewServiceHistory"));
            List<CrewServiceHistory> response = await PostAsync<List<CrewServiceHistory>>(requestUrl, CreateHttpContent(request));

            List<ServiceHistoryViewModel> result = new List<ServiceHistoryViewModel>();
            result = new List<ServiceHistoryViewModel>();

            if (response != null)
            {
                response.ForEach(x =>
                {
                    ServiceHistoryViewModel service = new ServiceHistoryViewModel();
                    service.DWT = x.VesselDeadWeightTonnage.GetValueOrDefault();
                    service.Power = x.VesselEngineSpecifications != null ? x.VesselEngineSpecifications.EnginePower : null;
                    service.Days = x.ServiceSeaDays.GetValueOrDefault();
                    service.Engine = x.VesselEngineSpecifications != null ? x.VesselEngineSpecifications.Maker + " " + x.VesselEngineSpecifications.EngineType : "";
                    service.VesselName = x.VesselName ?? "";
                    service.StartDate = x.ServiceStartDate.GetValueOrDefault();
                    service.EndDate = x.ServiceEndDate.GetValueOrDefault();
                    service.Flag = x.VesselFlag ?? "";
                    service.VesselType = x.VesselTypeShortCode ?? "";
                    service.Rank = x.RankShortCode ?? "";
                    service.Status = x.StatusShortCode ?? "";
                    service.StatusId = x.StsId ?? "";
                    service.ServiceActiveStatusId = x.ServiceActiveStatusId;
                    if (x.ServiceStartDate != null && x.ServiceStartDate.Value.Date > DateTime.Today && x.ServiceActiveStatusId == Convert.ToInt32(EnumsHelper.GetKeyValue(CrewServiceActiveStatus.ActualHistorical)))
                    {
                        service.IsFutureOnshoreRecord = true;
                        service.ShortCodeBackGroundColor = "#9D4F9B";
                        service.ShortCodeForegroundColor = "#FFFFFF";
                    }
                    if (x.ServiceActiveStatusId == Convert.ToInt32(EnumsHelper.GetKeyValue(CrewServiceActiveStatus.ActualActive)))
                    {
                        service.ShortCodeBackGroundColor = "#597f53";
                        service.ShortCodeForegroundColor = "#FFFFFF";
                    }
                    SetPlanningStatus(service, x);
                    result.Add(service);
                });
            };
            return result;
        }


        /// <summary>Sets the planning status.</summary>
        /// <param name="service">The serviceHistoryRow of viewmodel.</param>
        /// <param name="x">The serviceHistoryRow from the response</param>
        private void SetPlanningStatus(ServiceHistoryViewModel service, CrewServiceHistory x)
        {
            if (x.PlanningStatusId != null)
            {
                service.PlanningForegroundColor = "#FFFFFF";
                if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Approved))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Approved);
                    service.PlanningStatusDescription = CrewPlanningStatus.Approved.ToString();
                    service.PlanningBackGroundColor = "#22B573"; //green
                }
                else if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Ready))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Ready);
                    service.PlanningStatusDescription = CrewPlanningStatus.Ready.ToString();
                    service.PlanningBackGroundColor = "#606093"; //brushdulllavender
                }
                else if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Planned))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Planned);
                    service.PlanningStatusDescription = CrewPlanningStatus.Planned.ToString();
                    service.PlanningBackGroundColor = "#FBB03B"; //brushyellow
                }
                else if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Proposed))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Proposed);
                    service.PlanningStatusDescription = CrewPlanningStatus.Proposed.ToString();
                    service.PlanningBackGroundColor = "#FF8D1F"; //brushOrane
                }
                else if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Rejected))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Rejected);
                    service.PlanningStatusDescription = CrewPlanningStatus.Rejected.ToString();
                    service.PlanningBackGroundColor = "#FF2905"; //BrushRed
                }
                else if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Released))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Released);
                    service.PlanningStatusDescription = CrewPlanningStatus.Released.ToString();
                    service.PlanningBackGroundColor = "#d3406d"; //BrushDarkPink
                }
                else if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Joined))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Joined);
                    service.PlanningStatusDescription = CrewPlanningStatus.Joined.ToString();
                    service.PlanningBackGroundColor = "#9D4F9B"; //BrushPurple
                }
                else if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Cancelled))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Cancelled);
                    service.PlanningStatusDescription = CrewPlanningStatus.Cancelled.ToString();
                    service.PlanningBackGroundColor = "#808080"; //BrushGray
                }
                else if (x.PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.PlanProposed))
                {
                    service.PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.PlanProposed);
                    service.PlanningStatusDescription = CrewPlanningStatus.PlanProposed.ToString();
                    service.PlanningBackGroundColor = "#d38646"; //BrushOchre
                }
            }
        }

        /// <summary>Posts the get certificates and documents.</summary>
        /// <param name="identifier">The crewId.</param>
        /// <returns>
        ///   <br />CertificatesAndDocumentsViewModel
        /// </returns>
        public async Task<List<CertificatesAndDocumentsViewModel>> PostGetCertificatesAndDocumentsAsync(string identifier)
        {

            Dictionary<string, object> request = new Dictionary<string, object>();
            request.Add("crewId", identifier);
            request.Add("documentId", null);

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/CrewDocsAndCerts"));
            List<CrewDocumentDetail> response = await PostAsync<List<CrewDocumentDetail>>(requestUrl, CreateHttpContent(request));

            List<CertificatesAndDocumentsViewModel> results = new List<CertificatesAndDocumentsViewModel>();

            if (response != null)
            {
                List<CrewDocumentDetail> responseInOrder = response.OrderBy(x => x.CrewDocumentType.CrewDocumentTypeGroup.CdtSeq).ThenBy(x => x.CrewDocumentType.DocSeq).ToList();

                responseInOrder.ForEach(x =>
                {
                    CertificatesAndDocumentsViewModel document = new CertificatesAndDocumentsViewModel();
                    document.Authority = x.CrdApprovedBy;
                    document.Name = x.CrewDocumentType.DocDesc;
                    document.REF = x.CrewDocumentType.DocRef;
                    document.Number = x.CrdNumber;
                    document.CNT = x.CrdCountry;
                    document.DocId = x.DocId;
                    document.IssuedOn = x.CrdIssued;
                    document.Expiry = x.CrdExpiry;
                    document.DocumentCount = x.NoOfAttachments;
                    document.CrdId = x.CrdId;
                    document.CdtDescription = x.CrewDocumentType != null && x.CrewDocumentType.CrewDocumentTypeGroup != null ? x.CrewDocumentType.CrewDocumentTypeGroup.CdtDescription : "";

                    if (document.Expiry < DateTime.Today)
                    {
                        document.IsExpired = true;
                    }
                    else if (document.Expiry >= DateTime.Today && document.Expiry <= DateTime.Today.AddDays(Constants.DocumentAboutToExpireDays))
                    {
                        document.IsExpiring = true;
                    }
                    else
                    {
                        document.IsValid = true;
                    }

                    results.Add(document);
                });
            }
            return results;
        }

        /// <summary>
        /// Posts the get crew attachments.
        /// </summary>
        /// <param name="crewId">The crew identifier.</param>
        /// <param name="matchedId">The matched identifier.</param>
        /// <returns></returns>
        public async Task<List<AttachmentViewModel>> PostGetCrewAttachments(string crewId, List<string> matchedIds)
        {
            Dictionary<string, object> request = new Dictionary<string, object>();
            request.Add("crewId", crewId);
            request.Add("matchedIds", matchedIds);
            request.Add("documentCategoryType", DocumentCategory.CrewDocument);
            request.Add("cloudUploadStatus", null);

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/CrewScannedDocs"));
            List<CrewScannedDocDetails> response = await PostAsync<List<CrewScannedDocDetails>>(requestUrl, CreateHttpContent(request));

            List<AttachmentViewModel> results = new List<AttachmentViewModel>();

            if (response != null)
            {
                response.ForEach(x =>
                {
                    AttachmentViewModel attachment = new AttachmentViewModel();
                    attachment.DocumentSize = x.FileSize;
                    attachment.UploadedBy = x.UploadedBy;
                    attachment.UploadedOn = x.ScannedDate;
                    attachment.Name = x.Description;
                    attachment.FileName = x.FileName;
                    attachment.DocumentId = x.ScannedDocumentId;
                    results.Add(attachment);
                });
            }
            return results;
        }

        /// <summary>Posts the get crew header details.</summary>
        /// <param name="identifier">The crewId.</param>
        /// <returns>
        ///   CrewHeaderDetailsViewModel
        /// </returns>
        public async Task<CrewHeaderDetailsViewModel> PostGetCrewHeaderDetails(string identifier)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/CrewHeaderDetail/" + identifier));
            CrewPreview response = await GetAsync<CrewPreview>(requestUrl);

            CrewHeaderDetailsViewModel result = new CrewHeaderDetailsViewModel();
            if (response != null)
            {
                result.CrewStatus = response.CrewStatus;
                result.Name = response.LastName + ", " + response.FirstName + " " + response.MiddleName;
                result.PCN = response.PCN;
                result.Rank = response.Rank;
                result.Airport = response.NearAirport;
                result.JoinDate = response.JoinedDate.GetValueOrDefault();
            }

            result.Experiences = new List<CrewExperienceInYearsWrapper>();
            CrewExperienceRequest crewExperienceRequest = new CrewExperienceRequest
            {
                CrewId = identifier,
                ExperienceType = ExperienceType.Rank,
                IsOnlyCurrrentExperienceRequired = true
            };

            var expRequestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/GetCrewExperience"));
            List<CrewExperienceDetail> rankDetails = await PostAsync<List<CrewExperienceDetail>>(expRequestUrl, CreateHttpContent(crewExperienceRequest));

            var vesselExpRequestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/CrewVesselExperience/" + identifier));
            List<CrewExperienceDetail> vesselExperienceDetail = await GetAsync<List<CrewExperienceDetail>>(vesselExpRequestUrl);

            if (rankDetails != null && !rankDetails.Any())
            {
                rankDetails.Add(new CrewExperienceDetail() { ExperienceType = ExperienceType.Rank.ToString(), ExperienceInDays = 0 });
            }

            if (rankDetails != null && rankDetails.Any() && vesselExperienceDetail != null && vesselExperienceDetail.Any())
            {
                CrewExperienceDetail crewExperienceDetail = rankDetails.FirstOrDefault();
                if (crewExperienceDetail != null)
                {
                    crewExperienceDetail.ExperienceCode = ExperienceType.Rank.ToString();
                }
                result.RankDetails = new CrewExperienceInYearsWrapper(rankDetails.FirstOrDefault());
                result.Experiences = vesselExperienceDetail.Select(x => new CrewExperienceInYearsWrapper(x)).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gets the crew details.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        public async Task<CrewHeaderDetailsViewModel> GetCrewDetails(string identifier)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/CrewHeaderDetail/" + identifier));
            CrewPreview response = await GetAsync<CrewPreview>(requestUrl);

            CrewHeaderDetailsViewModel result = new CrewHeaderDetailsViewModel();
            if (response != null)
            {
                result.CrewStatus = response.CrewStatus;
                result.Name = response.LastName + ", " + response.FirstName + " " + response.MiddleName;
                result.PCN = response.PCN;
                result.Rank = response.Rank;
                result.Airport = response.NearAirport;
                result.JoinDate = response.JoinedDate.GetValueOrDefault();
            }
            return result;
        }

        /// <summary>Posts the get crew current details asynchronous.</summary>
        /// <param name="identifier">The crewId.</param>
        /// <returns>CrewCurrentDetailsViewModel</returns>
        public async Task<CrewCurrentDetailsViewModel> PostGetCrewCurrentDetailsAsync(string identifier)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/GetCrewCurrentContractDetail/" + identifier));
            CrewServiceOnBoard response = await GetAsync<CrewServiceOnBoard>(requestUrl);
            CrewCurrentDetailsViewModel result = new CrewCurrentDetailsViewModel();
            if (response != null)
            {
                result.Vessel = response.VesselName;
                result.ServiceStart = response.SignOnDate.GetValueOrDefault();
                result.ServiceEnd = response.SignOffDate.GetValueOrDefault();
                result.Rank = response.BudgetedRankDescription;
                result.ContractLength = response.InitialContractLength;
                result.ContractLengthUnit = response.InitialContractLengthUnit;
                result.DaysRem = (response.SignOffDate.GetValueOrDefault().Date - DateTime.Now.Date).Days;
                result.Client = response.ClientName;
            }
            return result;
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Setups the crew navigation URL.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private string SetupCrewNavigationURL(CrewListViewModel input)
        {
            string crewRequestURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
            return crewRequestURL;
        }

        /// <summary>
        /// Resets the reliever details of crew list record.
        /// </summary>
        /// <param name="crewListDetail">The crew list detail.</param>
        private void ResetRelieverDetailsOfCrewListRecord(OBCrewListDetail crewListDetail)
        {
            crewListDetail.RelieverId = null;
            crewListDetail.RelieverFirstName = null;
            crewListDetail.RelieverLastName = null;
            crewListDetail.RelieverMiddleName = null;
        }

        /// <summary>
        /// Gets the encrypted vessel.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="vesselName">Name of the vessel.</param>
        /// <param name="coyId">The coy identifier.</param>
        /// <returns></returns>
        private string GetEncryptedVesselId(string vesselId, string vesselName, string coyId)
        {
            string encryptedVessel = _provider.CreateProtector("Vessel").Protect(vesselId + Constants.Separator + vesselName + " - " + coyId + Constants.Separator + coyId);
            return encryptedVessel;
        }

        /// <summary>
        /// Gets the decrypted vessel identifier.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        private string GetDecryptedVesselId(string encryptedVesselDetail)
        {
            if (!string.IsNullOrWhiteSpace(encryptedVesselDetail))
            {
                string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
                return decryptedString.Split(Constants.Separator)[0];
            }
            return null;
        }
        #endregion

        #region Crew List

        /// <summary>
        /// Gets the crew list.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<List<CrewListDetailViewModel>> GetCrewList(CrewListViewModel inputRequest)
        {
            OBCrewListRequest request = new OBCrewListRequest();
            List<OBCrewListDetail> response = null;
            List<CrewListDetailViewModel> processedCrewList = new List<CrewListDetailViewModel>();

            if (inputRequest != null && !string.IsNullOrWhiteSpace(inputRequest.EncryptedVesselId))
            {
                string vesselId = _provider.CreateProtector("Vessel").Unprotect(inputRequest.EncryptedVesselId);
                request.VesselId = vesselId.Split(Constants.Separator)[0];
                request.FromDate = inputRequest.FromDate;
                request.ToDate = inputRequest.ToDate;
                request.IsUnsyncCrewRequired = false;

                request.StageFilter = inputRequest.SelectedStageFilter;
                if (inputRequest.SelectedStageFilter == EnumsHelper.GetKeyValue(CrewStageFilter.Overdue))
                {
                    request.OverdueToDate = DateTime.Now.AddDays(Constants.CrewOverdueXDays).Date;
                }
                else if (inputRequest.SelectedStageFilter == EnumsHelper.GetKeyValue(CrewStageFilter.UnplannedBerth))
                {
                    var localUnplannedBerthDate = DateTime.Now.AddDays(30);
                    var unplanneBerthdDate = new DateTime(localUnplannedBerthDate.Year, localUnplannedBerthDate.Month, localUnplannedBerthDate.Day, 23, 59, 59);
                    request.UnplannedToDate = unplanneBerthdDate;
                }

                if (inputRequest.IsSearchClicked)
                {
                    if (!string.IsNullOrWhiteSpace(inputRequest.SelectedRankIds))
                    {
                        List<string> requestRankList = new List<string>();
                        requestRankList = inputRequest.SelectedRankIds.Split(',').ToList();
                        request.RankCategoryIds = requestRankList;
                    }

                    if (!string.IsNullOrWhiteSpace(inputRequest.SelectedDepartmentIds))
                    {
                        List<string> requestDepartmentList = new List<string>();
                        requestDepartmentList = inputRequest.SelectedDepartmentIds.Split(',').ToList();
                        request.DepartmentIds = requestDepartmentList;
                    }
                }
                else
                {
                    if (inputRequest.SelectedStageFilter == EnumsHelper.GetKeyValue(CrewStageFilter.ChangeLastXDays))
                    {
                        request.CrewChangeDate = inputRequest.CrewChangeDate;
                    }
                }

            }

            var value = new Dictionary<string, object>()
            {
                { "OBCrewListRequest", request }
            };

            if (request != null && !string.IsNullOrWhiteSpace(request.VesselId))
            {
                var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/GetOnboardCrewList"));
                response = await PostAsyncAutoPaged<OBCrewListDetail>(requestUrl, value, 500);
            }

            //setting serial number
            if (response != null && response.Any())
            {
                int serialNumber = 0;
                foreach (OBCrewListDetail crewListDetail in response.OrderByDescending(x => x.CcaIsOfficer).ThenBy(x => x.RankSequenceNumber).ThenBy(x => x.CrewSignOn))
                {
                    var processedCrewListRecord = processedCrewList.FirstOrDefault(x => x.SvlId != null && x.SvlId.Equals(crewListDetail.SvlId));
                    if (processedCrewListRecord == null)
                    {
                        CrewListDetailViewModel OBCrew = new CrewListDetailViewModel(crewListDetail, ++serialNumber);
                        EncryptCrewId(ref OBCrew, crewListDetail);
                        processedCrewList.Add(OBCrew);
                    }
                    else
                    {
                        //SET ID will be different if both are onboard 
                        if (string.Equals(processedCrewListRecord.SetId, crewListDetail.SetId))
                        {
                            //We have two or more relievers for same berth and same date
                            //So we do not need to display name of the reliever
                            ResetRelieverDetailsOfCrewListRecord(crewListDetail);

                            if (string.Equals(processedCrewListRecord.PlanningStatusId, EnumsHelper.GetKeyValue(CrewPlanningStatus.Approved)) ||
                                string.Equals(processedCrewListRecord.PlanningStatusId, EnumsHelper.GetKeyValue(CrewPlanningStatus.Ready)) ||
                                string.Equals(crewListDetail.PlanningStatusId, EnumsHelper.GetKeyValue(CrewPlanningStatus.Approved)) ||
                                string.Equals(crewListDetail.PlanningStatusId, EnumsHelper.GetKeyValue(CrewPlanningStatus.Ready)))
                            {
                                //Either of the planned records are approved. We mark the processed record as approved, so it is valid for crew change
                                crewListDetail.PlanningStatusId = EnumsHelper.GetKeyValue(CrewPlanningStatus.Approved);
                            }
                        }
                        else
                        {
                            if (crewListDetail.ServiceActiveStatusId == 1)
                            {
                                //Get the active onboard record which is active from before the current record on the same berth. 
                                CrewListDetailViewModel overlappingOnboardRecord = processedCrewList.FirstOrDefault(x => x.SvlId != null && x.SvlId.Equals(crewListDetail.SvlId) && x.ServiceActiveStatusId == 1);

                                if (overlappingOnboardRecord != null)
                                {
                                    //Two diffrent crew are overlapping onboard for same berth
                                    ResetRelieverDetailsOfCrewListRecord(crewListDetail);
                                    //crewListDetail.BerthPlannedRecords = 0;

                                    //display the current crew's name as reliever for previous crew
                                    overlappingOnboardRecord.RelieverId = crewListDetail.CrewId;
                                    overlappingOnboardRecord.RelieverName = overlappingOnboardRecord.SetCrewFullName(crewListDetail);
                                    //overlappingOnboardRecord.IsRelieverStatusReady = false;
                                    overlappingOnboardRecord.PlanningStatusId = EnumsHelper.GetKeyValue(CrewStatus.Overlap);

                                    crewListDetail.SafeManning = false; // Since we dont have to show SM for overlapping records.
                                    int indexOfProcessedRecord = processedCrewList.IndexOf(overlappingOnboardRecord);

                                    int index = processedCrewList.IndexOf(overlappingOnboardRecord);
                                    processedCrewList[index].RelieverId = overlappingOnboardRecord.RelieverId;
                                    processedCrewList[index].RelieverName = overlappingOnboardRecord.RelieverName;
                                    processedCrewList[index].PlanningStatusId = overlappingOnboardRecord.PlanningStatusId;
                                    processedCrewList[index].IsRelieverNameVisible = true;

                                    OBCrewListDetail relieverId = new OBCrewListDetail();
                                    relieverId.RelieverId = overlappingOnboardRecord.RelieverId;

                                    EncryptCrewId(ref overlappingOnboardRecord, relieverId);
                                    processedCrewList[index].EncryptedReliverId = overlappingOnboardRecord.EncryptedReliverId;

                                    //Place the overlapping records one after the another.

                                    CrewListDetailViewModel OBCrewListVM = new CrewListDetailViewModel(crewListDetail, overlappingOnboardRecord.SerialNumber);
                                    EncryptCrewId(ref OBCrewListVM, crewListDetail);
                                    OBCrewListVM.BerthTypeShortCode = EnumsHelper.GetKeyValue(CrewStatus.Overlap);
                                    OBCrewListVM.BerthTypeDescription = EnumsHelper.GetDescription(CrewStatus.Overlap);
                                    OBCrewListVM.BerthColorCode = Constants.ShipsureBrushRed;
                                    processedCrewList.Insert(indexOfProcessedRecord + 1, OBCrewListVM);
                                }
                                else
                                {
                                    //The first onboard crew for the svl
                                    CrewListDetailViewModel obCrew = new CrewListDetailViewModel(crewListDetail, serialNumber);
                                    EncryptCrewId(ref obCrew, crewListDetail);
                                    processedCrewList.Add(obCrew);
                                }
                            }
                            else
                            {
                                //Two diffrent crew are overlapping onboard for same berth
                                ResetRelieverDetailsOfCrewListRecord(crewListDetail);
                                CrewListDetailViewModel obCrew = new CrewListDetailViewModel(crewListDetail, serialNumber);
                                EncryptCrewId(ref obCrew, crewListDetail);
                                processedCrewList.Add(obCrew);
                            }
                        }
                    }
                }
            }

            //removing records for unplanned berth condition
            //if selected stage is unplanned berth then remove records which have reliver name 
            //in case of overlap remove both the entries

            if (inputRequest.SelectedStageFilter == EnumsHelper.GetKeyValue(CrewStageFilter.UnplannedBerth))
            {
                if (processedCrewList != null && processedCrewList.Any())
                {
                    List<string> unplannedSeqNumber = processedCrewList.Where(x => !string.IsNullOrWhiteSpace(x.RelieverName)).Select(x => x.SvlId).ToList();
                    if (unplannedSeqNumber != null && unplannedSeqNumber.Any())
                    {
                        List<CrewListDetailViewModel> unplannedBerthRecords = null;
                        unplannedBerthRecords = processedCrewList.Where(x => !unplannedSeqNumber.Contains(x.SvlId)).ToList();
                        int localSerialNumber = 0;
                        if (unplannedBerthRecords != null && unplannedBerthRecords.Any())
                        {
                            foreach (var item in unplannedBerthRecords)
                            {
                                item.SerialNumber = ++localSerialNumber;
                            }
                        }
                        return unplannedBerthRecords;
                    }
                }
            }

            return processedCrewList;
        }

        /// <summary>
        /// Encrypts the crew identifier.
        /// </summary>
        /// <param name="obCrew">The ob crew.</param>
        /// <param name="crewListDetail">The crew list detail.</param>
        private void EncryptCrewId(ref CrewListDetailViewModel obCrew, OBCrewListDetail crewListDetail)
        {
            if (obCrew != null)
            {
                if (!string.IsNullOrWhiteSpace(crewListDetail.CrewId))
                {
                    obCrew.EncryptedCrewId = _provider.CreateProtector("CrewId").Protect(crewListDetail.CrewId);
                }
                if (!string.IsNullOrWhiteSpace(crewListDetail.RelieverId))
                {
                    obCrew.EncryptedReliverId = _provider.CreateProtector("CrewId").Protect(crewListDetail.RelieverId);
                }
            }

        }

        /// <summary>
        /// Gets the rank category.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RankCategoryViewModel>> GetRankCategory()
        {
            List<RankCategoryViewModel> result = new List<RankCategoryViewModel>();
            List<Lookup> RankList = new List<Lookup>();
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/RankCategory"));

            RankList = await GetAsync<List<Lookup>>(requestUrl);
            if (RankList != null && RankList.Any())
            {
                foreach (Lookup item in RankList)
                {
                    RankCategoryViewModel rank = new RankCategoryViewModel();
                    rank.RankId = item.Identifier;
                    rank.RankName = item.Description;
                    rank.Id = item.Identifier;
                    rank.Text = item.Description;
                    result.Add(rank);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the crew status.
        /// </summary>
        /// <returns></returns>
        public List<CrewStageFilter> GetCrewStatus()
        {
            List<CrewStageFilter> result = new List<CrewStageFilter>();

            foreach (CrewStageFilter val in Enum.GetValues(typeof(CrewStageFilter)))
            {
                result.Add(val);
            }

            return result;
        }

        #endregion

        #region Medical Sign Off List

        /// <summary>
        /// Gets the medical sign off list.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<List<MedicalSignOffResponseViewModel>> GetMedicalSignOffList(CrewListViewModel inputRequest)
        {
            List<MedicalSignOffResponseViewModel> medicalSignOffList = new List<MedicalSignOffResponseViewModel>();
            List<MedicalSignOffResponse> response = new List<MedicalSignOffResponse>();
            MedicalSignOffRequest request = new MedicalSignOffRequest();
            if (inputRequest != null)
            {
                string vesselId = _provider.CreateProtector("Vessel").Unprotect(inputRequest.EncryptedVesselId);
                request.VesselId = vesselId.Split(Constants.Separator)[0];
                request.FromDate = inputRequest.FromMedicalSignOff;
                request.ToDate = inputRequest.ToMedicalSignOff;
            }

            if (request != null && !string.IsNullOrWhiteSpace(request.VesselId))
            {
                var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Crew/MedicalSignOff"));
                response = await PostAsync<List<MedicalSignOffResponse>>(requestUrl, CreateHttpContent(request));
            }

            if (response != null && response.Any())
            {
                foreach (MedicalSignOffResponse item in response)
                {
                    MedicalSignOffResponseViewModel crew = new MedicalSignOffResponseViewModel(item);
                    crew.EncryptedCrewId = _provider.CreateProtector("CrewId").Protect(item.CrewId);
                    medicalSignOffList.Add(crew);
                }
            }
            return medicalSignOffList;
        }

        #endregion

    }
}
