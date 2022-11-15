using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.ViewModels.Crew;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class CrewListController : AuthenticatedController
    {
        #region Private Properties

        /// <summary>
        /// The crew client
        /// </summary>
        private readonly CrewClient _crewClient;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CrewListController"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="provider">The provider.</param>
        public CrewListController(CrewClient client, IDataProtectionProvider provider)
        {
            _crewClient = client;
            _provider = provider;
        }

        #endregion

        #region List Methods

        /// <summary>
        /// Lists the specified crew list.
        /// </summary>
        /// <param name="CrewList">The crew list.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult List(string CrewList, string VesselId)
        {
            CrewListViewModel crewVm = new CrewListViewModel();
            string data = _provider.CreateProtector("CrewList").Unprotect(CrewList);
            crewVm = Newtonsoft.Json.JsonConvert.DeserializeObject<CrewListViewModel>(data);
            crewVm.EncryptedVesselId = VesselId;
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            crewVm.VesselName = decreptedString.Split(Constants.Separator)[1];
            return View(crewVm);
        }

        /// <summary>
        /// Gets the crew list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCrewList(CrewListViewModel input)
        {
            _crewClient.AccessToken = GetAccessToken();

            List<CrewListDetailViewModel> response = await _crewClient.GetCrewList(input);
            return new JsonResult(response);
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
            string crewURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
            return Json(Url.Action("List", new { CrewList = crewURL, VesselId = crew.EncryptedVesselId }));
        }

        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }


    }
}