using Microsoft.AspNetCore.Mvc;
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
using PWAFeaturesRnd.Models.Report.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
	/// <summary>
	/// Permission controller
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
	/// <seealso cref="PWAFeaturesRnd.Controllers.Master.PermissionController" />
	public class PermissionController : AuthenticatedController
	{
		/// <summary>
		/// The shared client
		/// </summary>
		private readonly SharedClient _sharedClient;

		/// <summary>
		/// The document client
		/// </summary>
		private DocumentClient _documentClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionController" /> class.
		/// </summary>
		/// <param name="sharedClient">The shared client.</param>
		/// <param name="documentClient">The document client.</param>
		public PermissionController(SharedClient sharedClient, DocumentClient documentClient)
		{
			_sharedClient = sharedClient;
			_documentClient = documentClient;
		}

		/// <summary>
		/// Posts the get role rights.
		/// </summary>
		/// <param name="controlIds">The control ids.</param>
		/// <returns></returns>
		public async Task<IActionResult> PostGetRoleRights(List<string> controlIds)
		{
			List<ControlPermission> response = new List<ControlPermission>();
			if (controlIds != null && controlIds.Any())
			{
				List<ControlPermission> roleRights = GetRoleRightsStorage(); 
				if (roleRights == null)
				{
					roleRights = new List<ControlPermission>();
				}
				List<string> controlsWithoutPermission = new List<string>();
				foreach (string controlId in controlIds)
				{
					if (!roleRights.Any(x => x.ControlId.ToString().Equals(controlId, StringComparison.InvariantCultureIgnoreCase)))
					{
						controlsWithoutPermission.Add(controlId);
						response.Add(new ControlPermission() { ControlId = new Guid(controlId) });
					}
					else
					{
						response.Add(roleRights.FirstOrDefault(x => x.ControlId.ToString().Equals(controlId, StringComparison.InvariantCultureIgnoreCase)));
					}
				}
				if (controlsWithoutPermission != null && controlsWithoutPermission.Any())
				{
					_sharedClient.AccessToken = GetAccessToken();
					List<ControlPermission> controlsPermission = await _sharedClient.PostGetControlPermissions(controlsWithoutPermission);

					foreach (string control in controlsWithoutPermission)
					{
						if (controlsPermission != null && controlsPermission.Any(x => x.ControlId.ToString().Equals(control, StringComparison.InvariantCultureIgnoreCase)))
						{
							ControlPermission controlpermission = controlsPermission.FirstOrDefault(x => x.ControlId.ToString().Equals(control, StringComparison.InvariantCultureIgnoreCase));
							controlpermission.Permission = controlsPermission.Any(x => x.ControlId.ToString().Equals(control, StringComparison.InvariantCultureIgnoreCase) && x.Permission);
							response.FirstOrDefault(x => x.ControlId.ToString().Equals(control, StringComparison.InvariantCultureIgnoreCase)).Permission = controlpermission.Permission;
							roleRights.Add(controlpermission);
						}
						else
						{
							roleRights.Add(response.FirstOrDefault(x => x.ControlId.ToString().Equals(control, StringComparison.InvariantCultureIgnoreCase)));
						}
					}
				}
				SetRoleRightsStorage(roleRights);
			}
			if(IsDemoMode)
            {
				response.Where(x => x.ControlId.ToString().ToLower() != Constants.DemoControlId.ToLower()).ToList().ForEach(x=>x.Permission = false);
				return new JsonResult(response);
            }
			return new JsonResult(response);
		}

		/// <summary>
		/// Reports the response.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public async Task<IActionResult> ReportResponse(BusinessTaskResponse response)
		{
			if (response != null)
			{
				string reportName = !string.IsNullOrWhiteSpace(response.GeneratedFileName) ? response.GeneratedFileName : response.MessageContent;
				string friendlyFileName = !string.IsNullOrWhiteSpace(response.FriendlyFileName) ? response.FriendlyFileName : reportName;
				DocumentFileType DocumentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(reportName)).FirstOrDefault();

				if (response.Success)
				{
					if (!string.IsNullOrWhiteSpace(reportName)
							&& !reportName.Contains("Scheduled Report")
							&& !reportName.Contains("Cancel Order")
							&& (reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.ExcelXLSX))
								|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Word))
								|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.PDF))
								|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Excel))
								|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.CSV))
								|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Text))
								|| reportName.EndsWith(Common.Constants.ZipExtension, StringComparison.OrdinalIgnoreCase))

					  )
					{
						ReportDownloadHistoryRequest request = new ReportDownloadHistoryRequest()
						{
							FileName = reportName,
							TaskMessageId = response.Identifier,
							DownloadMode = "Auto",
							DownloadedBy = Request.Cookies["UserId"]
						};
						_documentClient.AccessToken = GetAccessToken();
						Stream documentResult = await _documentClient.DownloadReportAndLogHistory(request);

						if (documentResult != null)
						{
							byte[] byteData = CommonUtil.ConvertStreamToByte(documentResult);
							string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
							return new JsonResult(new { filename = friendlyFileName, bytes = byteString, fileType = EnumsHelper.GetDescription(DocumentFileType) });
						}
						else
						{
							return new JsonResult(new { filename = request.FileName, bytes = "", fileType = EnumsHelper.GetDescription(DocumentFileType) });
						}
					}
				}
			}
			return new JsonResult("");
		}

		/// <summary>
		/// Tasks the response.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public async Task<IActionResult> TaskResponse(BusinessTaskResponse response)
		{
			if (response != null)
			{
				string reportName = !string.IsNullOrWhiteSpace(response.GeneratedFileName) ? response.GeneratedFileName : response.MessageContent;
				string friendlyFileName = !string.IsNullOrWhiteSpace(response.FriendlyFileName) ? response.FriendlyFileName : reportName;
				DocumentFileType DocumentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(reportName)).FirstOrDefault();

				if (!string.IsNullOrWhiteSpace(reportName)
				&& (reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.ExcelXLSX))
					|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.ExcelXLSM))
					|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.PDF))
					|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Word))
					|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Excel))
					|| reportName.EndsWith(Constants.ZipExtension, StringComparison.OrdinalIgnoreCase)
					|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.CSV))
					|| reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Text))
					))
				{
					ReportDownloadHistoryRequest request = new ReportDownloadHistoryRequest()
					{
						FileName = reportName,
						TaskMessageId = response.Identifier,
						DownloadMode = "Auto",
						DownloadedBy = Request.Cookies["UserId"]
					};
					_documentClient.AccessToken = GetAccessToken();
					Stream documentResult = await _documentClient.DownloadReportAndLogHistory(request);

					if (documentResult != null)
					{
						byte[] byteData = CommonUtil.ConvertStreamToByte(documentResult);
						string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
						return new JsonResult(new { filename = friendlyFileName, bytes = byteString, fileType = EnumsHelper.GetDescription(DocumentFileType), isFileType = true });
					}
					else
					{
						return new JsonResult(new { filename = request.FileName, bytes = "", fileType = EnumsHelper.GetDescription(DocumentFileType), isFileType = true });
					}
				}
				else
				{
					return new JsonResult(new { isSuccess = response.Success, message = response.MessageContent });
				}
			}
			return new JsonResult("");
		}
	}
}