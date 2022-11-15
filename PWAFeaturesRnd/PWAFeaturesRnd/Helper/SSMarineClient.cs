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
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.InspectionManager;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.Models.Report.VoyageReporting;
using PWAFeaturesRnd.ViewModels.Inspection;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Helper
{
	/// <summary>
	/// SS Marine Client
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
	public class SSMarineClient : BaseHttpClient
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
		private readonly IDataProtectionProvider _provider;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="SSMarineClient"/> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="confirguration">The confirguration.</param>
		/// <param name="provider">The provider.</param>
		public SSMarineClient(HttpClient client, IConfiguration confirguration, IDataProtectionProvider provider, IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
		{
			client.BaseAddress = new Uri(AppSettings.SSMarineApiUrl);
			_client = client;
			_configuration = confirguration;
			_provider = provider;
		}

        #endregion

        #region Method		

        /// <summary>
        /// Gets the onboard department list.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> GetOnboardDepartmentList(string encryptedVesselId)
        {
			string decreptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselId);
			string VesselId = decreptedString.Split(Constants.Separator)[0];
			string queryString = "vesselId=" + VesselId;
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/api/V1.0/OnboardCrew/GetOnboardDepartmentList"), queryString);
			var response = await GetAsync<List<Lookup>>(requestUrl);
			return response;
		}

		/// <summary>
		/// Gets the inspection type with vessel type filter.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<List<InspectionTypeViewModel>> GetInspectionTypeWithVesselTypeFilter(InspectionTypeDetailRequestViewModel input)
		{
			List<InspectionTypeViewModel> responseVMList = new List<InspectionTypeViewModel>();
			InspectionTypeDetailRequest request = new InspectionTypeDetailRequest();
			if (!String.IsNullOrEmpty(input.Ves_Id))
			{
				request.Ves_Id = CommonUtil.GetDecryptedVesselId(_provider, input.Ves_Id);
			}

			var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/api/V1.0/InspectionManager/GetInspectionTypeList"));
			List<InspectionTypeViewModel> response = await PostAsync<List<InspectionTypeViewModel>>(requestUrl, CreateHttpContent(request));

			if (response != null && response.Any())
			{
				response.ForEach(x =>
				{
					responseVMList.Add(new InspectionTypeViewModel
					{
						VRP_Abbr = x.VRP_Abbr,
						DefaultInterval = x.DefaultInterval,
						InspectionType = x.InspectionType,
						VrpApplicableFor = x.VrpApplicableFor,
						InspectiontypeId = x.InspectiontypeId,
						IsInternalType = x.IsInternalType,
						VrpAbbr = x.VrpAbbr,
						VrpOther1 = x.VrpOther1,
						VrpOther2 = x.VrpOther2,
						VrpType3 = x.VrpType3,
						IsNextDueDateEditable = x.IsNextDueDateEditable,
						IsRequiredNextDueDate = x.IsRequiredNextDueDate,
						InitialInterval = x.InitialInterval,
						HasMappedQuestions = x.HasMappedQuestions
					});
				});
			}

			return responseVMList;
		}

		/// <summary>
		/// Gets the operating types.
		/// </summary>
		/// <returns></returns>
		public async Task<List<Lookup>> GetOperatingTypes()
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/api/V1.0/InspectionManager/GetOperatingTypes"));
			var response = await GetAsync<List<Lookup>>(requestUrl);
			return response;
		}

		/// <summary>
		/// Gets the type of the position activity.
		/// </summary>
		/// <param name="posActivityType">Type of the position activity.</param>
		/// <returns></returns>
		public async Task<List<PosActivityType>> GetPosActivityTypeList(PosActivityTypeLookupCode posActivityType)
		{
			string queryString = "posActivityTypeLookupCode=" + EnumsHelper.GetKeyValue(posActivityType);
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/api/V1.0/VoyageReporting/GetPosActivityTypeList"), queryString);
			List<PosActivityType> response = await GetAsync<List<PosActivityType>>(requestUrl);
			return response;
		}

		/// <summary>
		/// Gets the active office department.
		/// </summary>
		/// <returns></returns>
		public async Task<List<Lookup>> GetActiveOfficeDepartment()
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/api/V1.0/InspectionManager/GetActiveOfficeDepartment"));
			var response = await GetAsync<List<Lookup>>(requestUrl);
			return response;
		}

		/// <summary>
		/// Gets the inspection schedule list.
		/// </summary>
		/// <param name="inspectionScheduleRequest">The inspection schedule request.</param>
		/// <returns></returns>
		public async Task<List<InspectionScheduleDetail>> GetInspectionScheduleList(InspectionScheduleRequest inspectionScheduleRequest)
		{
			var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/api/V1.0/InspectionManager/GetInspectionScheduleList"));
			List<InspectionScheduleDetail> response = await PostAsync<List<InspectionScheduleDetail>>(requestUrl, CreateHttpContent(inspectionScheduleRequest));
			return response;
		}

        /// <summary>
        /// Searches the companies paged.
        /// </summary> 
        /// <param name="companySearchRequest">The company search request.</param>
        /// <returns></returns>
        public async Task<PagedResponse<List<CompanySearchResponseViewModel>>> SearchCompaniesPaged(CompanySearchViewModel companySearchRequestViewModel)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/V1.0/MarineAPI/SearchCompaniesPaged"));

			PagedResponse<List<CompanySearchResponseViewModel>> result = new PagedResponse<List<CompanySearchResponseViewModel>>();
			result.Result = new List<CompanySearchResponseViewModel>();
			PagedResponse<List<CompanySearchResponse>> response = null;

			CompanySearchRequest companySearchRequest=new CompanySearchRequest();
			companySearchRequest.pageRequest = companySearchRequestViewModel.pageRequest;
			companySearchRequest.CompanyName = companySearchRequestViewModel.CompanyName;
			companySearchRequest.CompanyTypeIds = companySearchRequestViewModel.CompanyTypeIds;
			companySearchRequest.ExcludedCompanyTypeIds = companySearchRequestViewModel.ExcludedCompanyTypeIds;
			
			response = await PostAsync<PagedResponse<List<CompanySearchResponse>>>(requestUrl, CreateHttpContent(companySearchRequest));


			if (response.Result != null)
			{
				foreach (CompanySearchResponse item in response.Result)
				{
					CompanySearchResponseViewModel company = new CompanySearchResponseViewModel();
					company.CompanyName = item.CompanyName;
					company.CompanyId = item.CompanyId;
					result.Result.Add(company);
				}
			}

			result.TotalRecords = response.TotalRecords;

			return result;

		}

        /// <summary>
        /// Saves the inspection.
        /// </summary>
        /// <param name="inspection">The inspection.</param>
        /// <returns></returns>
        public async Task<UpdateResponse<Inspection>> SaveInspection(SaveInspectionVisitViewModel saveinspectionvisitviewmodel)
        {
			Inspection inspection = new Inspection();
			inspection.VesselId= saveinspectionvisitviewmodel.VesselId;
			inspection.FromDate=saveinspectionvisitviewmodel.FromDate;
			inspection.EndDate=saveinspectionvisitviewmodel.EndDate;
			inspection.Where=saveinspectionvisitviewmodel.Where;
			inspection.ToPortId=saveinspectionvisitviewmodel.ToPortId;
			inspection.CompanyId=saveinspectionvisitviewmodel.CompanyId;
			inspection.InspectorTitle=saveinspectionvisitviewmodel.InspectorTitle;
			inspection.InspectorSurname=saveinspectionvisitviewmodel.InspectorSurname;
			inspection.InspectorForename=saveinspectionvisitviewmodel.InspectorForename;
			inspection.DepartmentId=saveinspectionvisitviewmodel.DepartmentId;
			inspection.MappedQuestions=saveinspectionvisitviewmodel.MappedQuestions;
			inspection.OfficeReviewerDetail=saveinspectionvisitviewmodel.OfficeReviewerDetail;
			inspection.ScheduleDetails=saveinspectionvisitviewmodel.ScheduleDetails;
			
	        var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/api/V1.0/InspectionManager/SaveInspection"));
			UpdateResponse<Inspection> response = await PostAsync<UpdateResponse<Inspection>>(requestUrl, CreateHttpContent(inspection));
			return response;
		}

		/// <summary>
		/// Searches the ports paged.
		/// </summary>
		/// <param name="searchPortRequest">The search port request.</param>
		/// <returns></returns>
		public async Task<PagedResponse<List<PortDetail>>> SearchPortsPaged(SearchPortRequest searchPortRequest)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/api/V1.0/MarineAPI/SearchPortsPaged"));

			PagedResponse<List<PortDetail>> pagedResponse = await PostAsync<PagedResponse<List<PortDetail>>>(requestUrl, CreateHttpContent(searchPortRequest));

			return pagedResponse;

		}
		#endregion

		#region Private Methods

		#endregion
	}
}
