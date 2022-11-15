using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// Notes Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class NotesController : AuthenticatedController
    {
        #region Private Variable

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// The shared client
        /// </summary>
        private NotificationClient _notificationClient;

        /// <summary>
        /// The shared client
        /// </summary>
        private SharedClient _sharedClient;

        /// <summary>
        /// The notification document client
        /// </summary>
        private NotificationDocumentClient _notificationDocumentClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="notificationClient">The notification client.</param>
        /// <param name="sharedClient">The shared client.</param>
        public NotesController(IDataProtectionProvider provider, NotificationClient notificationClient, SharedClient sharedClient, NotificationDocumentClient notificationDocumentClient)
        {
            _provider = provider;
            _notificationClient = notificationClient;
            _sharedClient = sharedClient;
            _notificationDocumentClient = notificationDocumentClient;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Gets the notes list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetNotesList(NotesRequestViewModel request)
        {
            _notificationClient.AccessToken = GetAccessToken();

            DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
            pageRequest.Length = 40;
            pageRequest.Start = (pageRequest.Length * (request.PageNumber - 1)) + 1;
            pageRequest.Columns = new List<Column>();
            pageRequest.Columns.Add(new Column() { Name = "NoteTitle" });

            pageRequest.Order = new List<Order>();
            pageRequest.Order.Add(new Order()
            {
                Column = 0,
                Dir = "asc"
            });

            List<NotesListViewModel> notes = new List<NotesListViewModel>();
            bool hasNextScroll = false;
            int timeZoneOffSet = 0;
            string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
            Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);

            PagedResult<PagedList<NotesListViewModel>> response = await _notificationClient.GetNotesList(pageRequest, request, timeZoneOffSet);
            if (response != null && response.Results != null && response.Results.Any())
            {
                notes = response.Results;
                hasNextScroll = response.HasNext;
            }
            
            return new JsonResult(new { data = notes, hasNextScroll = hasNextScroll, totalCount = response.TotalCount });
        }

        /// <summary>
        /// Saves the note details.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<IActionResult> SaveNoteDetails(CreateNoteRequestViewModel inputRequest)
        {
            _notificationClient.AccessToken = GetAccessToken();
            long response = await _notificationClient.SaveNoteDetails(inputRequest);
            return new JsonResult(response);
        }

        /// <summary>
        /// Notes the details.
        /// </summary>
        /// <param name="encryptedNoteId">The encrypted note identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> NoteDetails(string encryptedNoteId)
        {
            string stringNoteId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.NoteIdEncryptionText, encryptedNoteId);
            long noteId = long.Parse(stringNoteId);

            _notificationClient.AccessToken = GetAccessToken();
            int timeZoneOffSet = 0;
            string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
            Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
            var result = await _notificationClient.GetNoteDetailsById(noteId, timeZoneOffSet);
            return new JsonResult(result);
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="encryptedNoteId">The encrypted note identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeleteNote(string encryptedNoteId)
        {
            string stringNoteId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.NoteIdEncryptionText, encryptedNoteId);
            long noteId = long.Parse(stringNoteId);

            _notificationClient.AccessToken = GetAccessToken();
            var result = await _notificationClient.DeleteNoteById(noteId);
            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the notes area list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetNotesAreaList()
        {
            _notificationClient.AccessToken = GetAccessToken();
            List<MessageCategoryDtoViewModel> result = await _notificationClient.GetMessageCategories(AppSettings.ApplicationId);
            return new JsonResult(result);
        }

        /// <summary>
        /// Navigates to note record details.
        /// </summary>
        /// <param name="encryptedNoteId">The encrypted note identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> NavigateToNoteRecordDetails(string encryptedNoteId, string encryptedVesselId)
        {
            string stringNoteId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.NoteIdEncryptionText, encryptedNoteId);
            int noteId = Convert.ToInt32(stringNoteId);
            string vesselId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.VesselIdEncryptionText, encryptedVesselId);

            if (!String.IsNullOrWhiteSpace(stringNoteId) && !String.IsNullOrWhiteSpace(vesselId))
            {
                _notificationClient.AccessToken = GetAccessToken();
                _sharedClient.AccessToken = GetAccessToken();
                NoteAdditionalDetailsResponse response = await _notificationClient.GetNoteAdditionalDetails(noteId, AppSettings.ApplicationId);
                
                if (response != null)
                {
                    string urlDetails = null;

                    if (!string.IsNullOrWhiteSpace(response.NavigationParameters))
                    {
                        urlDetails = JsonConvert.DeserializeObject<NavigationParameters>(response.NavigationParameters).URL;
                    }
                    List<string> vesselIds = new List<string>() { vesselId };
                    List<VesselInfoViewModel> vesselDetails = await _sharedClient.GetVesselsName(vesselIds);
                    VesselInfoViewModel vesselDetail = vesselDetails.FirstOrDefault();

                    string encryptedVesselUrl = CommonUtil.GetEncryptedVesselDetails(_provider, vesselDetail.VesselId, vesselDetail.VesselName, vesselDetail.CoyId);
                    string encryptedRequest = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationRecordDetailsEncKey, JsonConvert.DeserializeObject<ContextParameter>(response.ContextParams));

                    if (response != null)
                    {
                        return new JsonResult(urlDetails + encryptedRequest + "&VesselId=" + encryptedVesselUrl);
                    }
                }                

                return new JsonResult(response);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Updates the note status.
        /// </summary>
        /// <param name="encryptedNoteId">The encrypted note identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateNoteStatus(string encryptedNoteId, int status)
        {
            string stringNoteId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.NoteIdEncryptionText, encryptedNoteId);
            long noteId = long.Parse(stringNoteId);

            _notificationClient.AccessToken = GetAccessToken();
            var result = await _notificationClient.UpdateNoteStatus(noteId, status);
            return new JsonResult(result);
        }

        /// <summary>
        /// Marks the job as done.
        /// </summary>
        /// <param name="reminderVM">The reminder vm.</param>
        /// <returns></returns>
        public async Task<IActionResult> MarkJobAsDone(NotesReminderViewModel reminderVM)
		{
			_notificationClient.AccessToken = GetAccessToken();
			var result = await _notificationClient.MarkJobAsDone(reminderVM);
			return new JsonResult(result);
		}

        /// <summary>
        /// Updates the reminder status.
        /// </summary>
        /// <param name="reminderVM">The reminder vm.</param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateReminderStatus(NotesReminderViewModel reminderVM)
		{
			_notificationClient.AccessToken = GetAccessToken();
			var result = await _notificationClient.UpdateReminderStatus(reminderVM);
			return new JsonResult(result);
		}

        /// <summary>
        /// Dismiss Reminder Without EncryptedKey
        /// </summary>
        /// <param name="reminderId"></param>
        /// <returns></returns>
        public async Task<IActionResult> DismissReminderWithoutEncryptedKey(NotesReminderViewModel reminderVM) 
        {
            _notificationClient.AccessToken = GetAccessToken();
            if (reminderVM != null)
            {
                bool result = await _notificationClient.ReminderAlertDismissed(reminderVM.ReminderId);
                return new JsonResult(result);
            }
            return new JsonResult(false);            
        }

        /// <summary>
        /// Reminders the alert dismissed.
        /// </summary>
        /// <param name="reminderId">The reminder identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> ReminderAlertDismissed(string encryptedReminderId)
        {
            _notificationClient.AccessToken = GetAccessToken();
            string stringReminderId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.ReminderIdEncryptionText, encryptedReminderId);
            long reminderId = long.Parse(stringReminderId);
            bool result = await _notificationClient.ReminderAlertDismissed(reminderId);
            return new JsonResult(result);
        }

        /// <summary>
        /// Downloads the attachment.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadAttachment(AttachmentViewModel request)
        {
            _notificationDocumentClient.AccessToken = GetAccessToken();
            DownloadDocumentRequest documentRequest = new DownloadDocumentRequest { DocumentId = request.EttId };
            var result = await _notificationDocumentClient.DownloadDocument(documentRequest);
            string byteString = (result.DocumentByteStream != null && result.DocumentByteStream.Length > 0) ? Convert.ToBase64String(result.DocumentByteStream) : null;
            string fileName = !string.IsNullOrWhiteSpace(request.Description) ? request.Description : result.FileName.Split('.')[0];
            string extension = EnumsHelper.GetEnumNameFromKeyValue(typeof(DocumentFileType), request.FileExtension);
            return new JsonResult(new { filename = fileName, bytes = byteString, fileType = extension, status = result.Status });
        }

        /// <summary>
        /// Deletes the attachment.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteAttachment(AttachmentViewModel request)
        {
            _notificationDocumentClient.AccessToken = GetAccessToken();
            DownloadDocumentRequest documentRequest = new DownloadDocumentRequest
            {
                DocumentId = request.EttId
            };
            var result = await _notificationDocumentClient.DeleteDocument(documentRequest);
            return new JsonResult(true);
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> UploadFile(UploadAttachmentRequestViewModel request)
        {
            _notificationDocumentClient.AccessToken = GetAccessToken();
            request.LoginUserName = HttpContext.Session.GetString(Constants.UserNameSessionKey);
            UploadAttachmentResponse result = await _notificationDocumentClient.UploadFile(request);
            return new JsonResult(new { Response = result.IsOperationSuccess, Data = result });
        }

        #endregion

    }
}
