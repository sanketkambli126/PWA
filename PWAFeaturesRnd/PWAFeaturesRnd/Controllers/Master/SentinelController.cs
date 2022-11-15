using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Sentinel;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// Sentinel Controller
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class SentinelController : AuthenticatedController
    {
        #region Private Properties

        /// <summary>
        /// The provider
        /// </summary>
        private readonly IDataProtectionProvider _provider;

        /// <summary>
        /// The h seq manager dashboard client
        /// </summary>
        private readonly HSEQManagerDashboardClient _hSEQManagerDashboardClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SentinelController"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="hSEQManagerDashboardClient">The h seq manager dashboard client.</param>
        public SentinelController(IDataProtectionProvider provider, HSEQManagerDashboardClient hSEQManagerDashboardClient)
        {
            _provider = provider;
            _hSEQManagerDashboardClient = hSEQManagerDashboardClient;
        }

        #endregion

        #region Views

        /// <summary>
        /// Vessels the detail.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> VesselDetail(string VesselId)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            SentinelDashboardDetailRequestViewModel input = new SentinelDashboardDetailRequestViewModel();
            input.EncryptedVesselId = VesselId;

            input.VesselId = CommonUtil.GetDecryptedVesselId(_provider, input.EncryptedVesselId);
            input.GetCategoryGraphDetails = true;

            SentinelDashboardDetailViewModel response = await _hSEQManagerDashboardClient.GetSentinelDashboardDetail(input);

            return View(response);
        }

        /// <summary>
        /// Offices the list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OfficeList()
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            SentinelDashboardDetailRequestViewModel input = new SentinelDashboardDetailRequestViewModel();

            input.UserId = Request.Cookies["UserId"];
            input.MenuType = "O";

            List<OfficeViewDetailResponseViewModel> officeList = await _hSEQManagerDashboardClient.GetSentinelDashboardOfficeView(input);
            return View(officeList);
        }

        /// <summary>
        /// Vessels the list.
        /// </summary>
        /// <param name="FleetRequest">The fleet request.</param>
        /// <returns></returns>
        public async Task<IActionResult> VesselList(string FleetRequest)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            SentinelVesselListViewModel response = new SentinelVesselListViewModel();

            if (!String.IsNullOrWhiteSpace(FleetRequest))
            {
                DashboardParameter fleetRequest = CommonUtil.GetDecryptedFleetRequest(_provider, FleetRequest);
                ModelDimensionVesselValueRequestViewModel input = new ModelDimensionVesselValueRequestViewModel();
                input.UserId = Request.Cookies["UserId"];
                input.MenuType = fleetRequest.MenuType;
                input.FleetId = fleetRequest.FleetId;

                response.Title = fleetRequest.Title;
                //response.VesselList = await _hSEQManagerDashboardClient.GetSentinelFleetVesselDetail(input);
            }

            return View(response);
        }

        /// <summary>
        /// Fleets the vessel list.
        /// </summary>
        /// <param name="FleetRequest">The fleet request.</param>
        /// <param name="VesselFilters">The vessel filters.</param>
        /// <returns></returns>
        public async Task<IActionResult> FleetVesselList(string FleetRequest, string VesselFilters)
		{ 
			SentinelVesselListViewModel response = new SentinelVesselListViewModel();
			FleetVesselDetailRequestViewModel input = new FleetVesselDetailRequestViewModel();

			if (!String.IsNullOrWhiteSpace(FleetRequest))
			{
				DashboardParameter decryptedFleetRequest = CommonUtil.GetDecryptedFleetRequest(_provider, FleetRequest);
				
				input.UserId = Request.Cookies["UserId"];
				input.MenuType = decryptedFleetRequest.MenuType;
				input.FleetId = decryptedFleetRequest.FleetId;
				response.Title = decryptedFleetRequest.Title;
				if (String.IsNullOrWhiteSpace(decryptedFleetRequest.FleetId))
				{
					response.IsVLOfficeSearchAvailable = true;
				}

			}
			if (!String.IsNullOrWhiteSpace(VesselFilters))
			{
				FleetVesselDetailRequestViewModel decryptedFilters = CommonUtil.GetDecryptedRequest<FleetVesselDetailRequestViewModel>(_provider, Constants.VesselList, VesselFilters);
				input.ModelDimensionId = decryptedFilters.ModelDimensionId;
				input.OverrideDimensionId = decryptedFilters.OverrideDimensionId;
				input.ColorStatus = decryptedFilters.ColorStatus;
				input.BiggestMoverRange = decryptedFilters.BiggestMoverRange;

				response.VesselListRequest = decryptedFilters;
			}

			PagedRequest pagedRequest = new PagedRequest()
			{
				PageNumber = 1,
				PageSize = 20
			};

			response.VesselList = await GetVesselList(input, pagedRequest);
			response.FleetRequest = FleetRequest;
			return View(response);
		}

        #endregion

        #region Action

        /// <summary>
        /// Categories the details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCategoryDetails(CategoryOverrideDetailRequestViewModel input)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            input.VesselId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.VesselId, input.EncryptedVesselId);
            List<VesselModelChildValueDetailResponseViewModel> categoryDetails = await _hSEQManagerDashboardClient.GetSentinelCategoryAndFactorDetail(input);
            return PartialView("categoryDetailPartialView", categoryDetails);
        }

		/// <summary>
		/// Gets the active overrides.
		/// </summary>
		/// <param name="FleetId">The fleet identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetActiveOverrides(string fleetRequest)
		{
			_hSEQManagerDashboardClient.AccessToken = GetAccessToken();
			SentinelDashboardDetailRequestViewModel input = new SentinelDashboardDetailRequestViewModel();
			DashboardParameter decryptedFleetRequest = CommonUtil.GetDecryptedFleetRequest(_provider, fleetRequest);
			input.UserId = Request.Cookies["UserId"];
			input.MenuType = decryptedFleetRequest.MenuType;
			input.FleetId = decryptedFleetRequest.FleetId;
			List<OverrideDimensionVesselResponseViewModel> activeOverridesList = await _hSEQManagerDashboardClient.GetSentinelFleetWiseOverrideVesselCount(input);
			foreach (var item in activeOverridesList)
			{
				item.FleetRequest = fleetRequest;
			}

            return PartialView("activeOverridesPartialView", activeOverridesList);
        }

        /// <summary>
        /// Gets the biggest movers.
        /// </summary>
        /// <param name="fleetRequest">The fleet request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetBiggestMovers(string fleetRequest)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            SentinelDashboardDetailRequestViewModel input = new SentinelDashboardDetailRequestViewModel();
            DashboardParameter decryptedFleetRequest = CommonUtil.GetDecryptedFleetRequest(_provider, fleetRequest);
            input.UserId = Request.Cookies["UserId"];
            input.MenuType = decryptedFleetRequest.MenuType;
            input.FleetId = decryptedFleetRequest.FleetId;

			List<ValueDifferenceRangeResponseViewModel> result = await _hSEQManagerDashboardClient.GetValueDifferenceRange(input);
			foreach(var item in result)
            {
				item.FleetRequest = fleetRequest;
            }

            return PartialView("biggestMoversPartialView", result);
        }

        /// <summary>
        /// Gets the category and overrides tree.
        /// </summary>
        /// <param name="fleetRequest">The fleet request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCategoryAndOverridesTree(string fleetRequest)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            ModelDimensionRequestViewModel input = new ModelDimensionRequestViewModel();
            DashboardParameter decryptedFleetRequest = CommonUtil.GetDecryptedFleetRequest(_provider, fleetRequest);
            input.UserId = Request.Cookies["UserId"];
            input.MenuType = decryptedFleetRequest.MenuType;
            input.FleetId = decryptedFleetRequest.FleetId;
            input.DimensionLevelString = "0~1~2";

            List<TreeViewModel<ModelDimensionResponseViewModel>> result = await _hSEQManagerDashboardClient.GetSentinelModelDimension(input);

            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the biggest movers tree.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetBiggestMoversTree()
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();

            List<TreeViewModel<BiggestMoverRangeResponseViewModel>> result = await _hSEQManagerDashboardClient.GetBiggestMoverRangeTree();

            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the filtered results.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetFilteredResults(string fleetRequest, FleetVesselDetailRequestViewModel input)
        {
            DashboardParameter decryptedFleetRequest = CommonUtil.GetDecryptedFleetRequest(_provider, fleetRequest);
            input.UserId = Request.Cookies["UserId"];
            input.MenuType = decryptedFleetRequest.MenuType;
            input.FleetId = decryptedFleetRequest.FleetId;

            PagedRequest pagedRequest = new PagedRequest()
            {
                PageNumber = input.PageNumber,
                PageSize = 20
            };

            var VesselList = await GetVesselList(input, pagedRequest);
            return PartialView("modelDimensionVesselContainerPartialView", VesselList);
        }

        /// <summary>
        /// Gets the Fleet list result.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns></returns>
        public async Task<IActionResult> GetFleetListResults(string fleetRequest, OfficeFleetVesselCountRequestViewModel input)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            DashboardParameter decryptedFleetRequest = CommonUtil.GetDecryptedFleetRequest(_provider, fleetRequest);
            input.UserId = Request.Cookies["UserId"];
            input.MenuType = decryptedFleetRequest.MenuType;
            input.FleetId = decryptedFleetRequest.FleetId;
            var fleetList = await _hSEQManagerDashboardClient.GetFleetList(input);
            return PartialView("modelDimensionFleetContainerPartialView", fleetList);
        }

        /// <summary>
        /// Gets the fleet vessel summary.
        /// </summary>
        /// <param name="fleetRequest">The fleet request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetFleetVesselSummary(string fleetRequest)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            OfficeFleetVesselCountRequestViewModel input = new OfficeFleetVesselCountRequestViewModel();
            DashboardParameter decryptedFleetRequest = CommonUtil.GetDecryptedFleetRequest(_provider, fleetRequest);
            input.UserId = Request.Cookies["UserId"];
            input.MenuType = decryptedFleetRequest.MenuType;
            input.FleetId = decryptedFleetRequest.FleetId;

            var result = await _hSEQManagerDashboardClient.GetFleetVesselSummary(input);
            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the vessel score graph.
        /// </summary>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVesselScoreGraph(string VesselId)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            List<VesselSentinelValueViewModel> result = await _hSEQManagerDashboardClient.GetSentinelVesselHistoryGraphDetail(VesselId);

            return new JsonResult(result);
        }

        #endregion

        #region Non Action Private Methods 

        /// <summary>
        /// Gets the vessel list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="pagedRequest">The paged request.</param>
        /// <returns></returns>
        [NonAction]
        private async Task<PagedResponse<List<FleetVesselDetailResponseViewModel>>> GetVesselList(FleetVesselDetailRequestViewModel request, PagedRequest pagedRequest)
        {
            _hSEQManagerDashboardClient.AccessToken = GetAccessToken();
            PagedResponse<List<FleetVesselDetailResponseViewModel>> result = await _hSEQManagerDashboardClient.GetVesselList(request, pagedRequest);
            return result;
        }

        #endregion
       
    }
}
