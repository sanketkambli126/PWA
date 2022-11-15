using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.ViewModels.Common;

namespace PWAFeaturesRnd.Controllers
{
	/// <summary>
	/// Common Controller
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
	public class CommonController : AuthenticatedController
	{
		#region Properties

		/// <summary>
		/// The provider
		/// </summary>
		private IDataProtectionProvider _provider;

		/// <summary>
		/// The document client
		/// </summary>
		private DocumentClient _documentClient;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="CommonController" /> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <param name="documentClient">The document client.</param>
		public CommonController(IDataProtectionProvider provider, DocumentClient documentClient)
		{
			_provider = provider;
			_documentClient = documentClient;
		}

		#endregion

		#region Actions

		/// <summary>
		/// Gets the vessel name from URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <returns></returns>
		public IActionResult GetVesselNameFromUrl(string url)
		{
			string vesselName = string.Empty;
			if (!string.IsNullOrEmpty(url))
			{
				Uri uri = new Uri(url);
				var parameters = HttpUtility.ParseQueryString(uri.Query);
				var vesselIdParameter = parameters.Get("VesselId");

				var vesselDecryptedString = CommonUtil.GetDecryptedVessel(_provider, vesselIdParameter);
				if (!string.IsNullOrWhiteSpace(vesselDecryptedString))
				{
					vesselName = CommonUtil.GetVesselNameFromDisplayName(vesselDecryptedString.Split(Constants.Separator)[1]);
				}
			}
			return new JsonResult(vesselName);
		}

		/// <summary>
		/// Updates the selected tab.
		/// </summary>
		/// <param name="pageKey">The page key.</param>
		/// <param name="selectedTab">The selected tab.</param>
		/// <returns></returns>
		public IActionResult UpdateSelectedTab(string pageKey, string selectedTab)
		{
			SetSelectedTab(pageKey, selectedTab);
			return new JsonResult(true);
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

		public IActionResult Error()
        {
			return View();
        }

		/// <summary>
		/// Get jwt access token
		/// </summary>
		/// <returns></returns>
		public IActionResult GetJwtAccessToken()
		{
			string token = GetAccessToken();
			return new JsonResult(token);
		}

		/// <summary>
		/// Get access details
		/// </summary>
		/// <returns></returns>
		public IActionResult GetAccessDetails()
		{
			return new JsonResult(new { token = GetAccessToken(), emailId = Request.Cookies["EmailId"] });
		}
		#endregion
	}
}
