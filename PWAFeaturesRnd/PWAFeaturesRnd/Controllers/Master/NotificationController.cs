using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// Notification Controller
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.BaseController" />
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class NotificationController : BaseController
	{
        /// <summary>
        /// The client
        /// </summary>
        private readonly NotificationClient _client;

        /// <summary>
        /// The shared client
        /// </summary>
        private readonly SharedClient _sharedClient;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// The notification document client
        /// </summary>
        private NotificationDocumentClient _notificationDocumentClient;

		/// <summary>
		/// The ticket store
		/// </summary>
		private ITicketStore _ticketStore;

		/// <summary>
		/// Initializes a new instance of the <see cref="NotificationController" /> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="sharedClient">The shared client.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="notificationDocumentClient">The notification document client.</param>
		/// <param name="ticketStore"></param>
		public NotificationController(NotificationClient client, SharedClient sharedClient, IDataProtectionProvider provider, NotificationDocumentClient notificationDocumentClient, ITicketStore ticketStore)
		{
			_client = client;
			_provider = provider;
			_sharedClient = sharedClient;
			_notificationDocumentClient = notificationDocumentClient;
			_ticketStore = ticketStore;
		}


		/// <summary>
		/// Indexes the specified vessel identifier.
		/// </summary>
		/// <param name="urlParameter">The URL parameter.</param>
		/// <param name="clearSessionStorage"></param>
		/// <param name="isFilterChange"></param>
		/// <returns></returns>
		public async Task<IActionResult> Index(string urlParameter, string clearSessionStorage, bool isFilterChange)
		{		
			
			NotificationSearchRequest searchParameter = new NotificationSearchRequest();
			if (!string.IsNullOrWhiteSpace(urlParameter))
			{
				searchParameter = JsonConvert.DeserializeObject<NotificationSearchRequest>(urlParameter);
			}

			NotificationViewModel viewModel = new NotificationViewModel();
			CookieOptions option = new CookieOptions();
				option.Expires = DateTime.Now.AddMonths(6);
			if (!string.IsNullOrWhiteSpace(searchParameter.NotificationJwtToken))
            {
                string key = StoredNoticationToken(searchParameter.NotificationJwtToken, _ticketStore);
				
				Response.Cookies.Append("RetrievingKey", key, option);
				Response.Cookies.Append("NotificationUserId", searchParameter.UserId, option);
				Response.Cookies.Append("NotificationUserEmailId", searchParameter.UserEmailId, option);
				Response.Cookies.Append("NotificationApplicationId", searchParameter.ApplicationId.ToString(), option);
                Response.Cookies.Append("SignalRURL", AppSettings.SignalRUrl, option);
                Response.Cookies.Append("NotificationRoles", "CRRA00000001,FNRA00000017,FNRA00000019,FNRA00000022,FNRA00000023,FNRA00000024,FNRA00000026,GLAS00000001,GLAS00000002,GLAS00000003,GLAS00000008,GLAS00000012,GLAS00000025,GLAS00000038,GLAS00000094,GLAS00000095,GLAS00000122,GLAS00000125,GLAS00000145,MRRA00000001,PUMA00000003,PURA00000003,-1", option);

                if (!string.IsNullOrWhiteSpace(searchParameter.NavigationURL))
                {
                    Response.Cookies.Append("NotificationNavigationURL", searchParameter.NavigationURL, option);
                }
                searchParameter.SearchText = !string.IsNullOrWhiteSpace(searchParameter.SearchText) ? searchParameter.SearchText : string.Empty;
                searchParameter.ChannelId = !string.IsNullOrWhiteSpace(searchParameter.SearchText) ? 0 : searchParameter.ChannelId;
                
                viewModel.SearchText = searchParameter.SearchText;
                viewModel.OpenCreateNewChannel = searchParameter.OpenCreateNewChannel;
                _client.AccessToken = searchParameter.NotificationJwtToken;
                ChannelRequest channelRequest = new ChannelRequest();
                channelRequest.SearchText = searchParameter.SearchText;
                channelRequest.ChannelId = searchParameter.ChannelId;
                channelRequest.ReferenceIdentifier = searchParameter.ReferenceIdentifier;
                if (!String.IsNullOrWhiteSpace(searchParameter.MessageDetailsJSON))
                {
                    viewModel.NewMessageDetails = JsonConvert.DeserializeObject<NewMessageParametersViewModel>(searchParameter.MessageDetailsJSON);
                    channelRequest.CategoryId = viewModel.NewMessageDetails.CategoryId;
                    channelRequest.ContextPayload = viewModel.NewMessageDetails.ContextPayload;
                    channelRequest.ReferenceIdentifier = viewModel.NewMessageDetails.ReferenceIdentifier;
                }

                int timeZoneOffSet = 0;

                //TODO: Replace Cookies with session

                //int test = GetSessionIntValue(Constants.TimeZoneDiffSessionKey);
                //var testTempDate = TempData[Constants.TimeZoneDiffSessionKey];
                //var testValue = CommonUtil.GetSessionObject<int?>(HttpContext.Session, Constants.TimeZoneDiffSessionKey);
                viewModel.ClearSessionStorage = Convert.ToBoolean(clearSessionStorage);
                viewModel.SessionStorageDetails = SetSessionStorageDetail(_provider, channelRequest);
                string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
                Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
                viewModel.ChannelList = new List<NotificationChannelViewModel>(); //viewModel.ChannelList = await _client.GetChannelList(channelRequest, timeZoneOffSet);

                viewModel.IsSearchClicked = searchParameter.IsSearchClicked;
            }
            else
            {
				//If token is not provoded, then we avoid api calling and intialize channel list for blank
				viewModel.ChannelList = new List<NotificationChannelViewModel>();
				viewModel.ClearSessionStorage = Convert.ToBoolean(clearSessionStorage);

			}
			viewModel.IsFilterChange = isFilterChange;
			return View("Notification", viewModel);
		}        

        /// <summary>
        /// Gets the channel messages.
        /// </summary>
        /// <param name="ChannelId">The channel identifier.</param>
        /// <param name="IsScrolled">if set to <c>true</c> [is scrolled].</param>
        /// <param name="PageNumber">The page number.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetChannelMessages(string ChannelId, bool IsScrolled, int PageNumber)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 10;
			pageRequest.Start = (pageRequest.Length * (PageNumber - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "ParticipantName" });

			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});

			List<NotificationUserMessageViewModel> channelMessage = new List<NotificationUserMessageViewModel>();
			bool hasNextScroll = false;
			//List<NotificationUserMessageViewModel> unreadMessage = await _client.GetNewUnreadMessages(ChannelId);
			//int timeZoneOffSet = GetSessionIntValue(Constants.TimeZoneDiffSessionKey);
			int timeZoneOffSet = 0;
			string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
			Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
			PagedResult<PagedList<NotificationUserMessageViewModel>> response = await _client.GetChannelMessages(pageRequest, ChannelId, timeZoneOffSet);
			if (response != null && response.Results != null && response.Results.Any())
			{
				if (IsScrolled)
				{
					channelMessage.AddRange(response.Results.OrderByDescending(x => x.CreatedOnUtc).ToList());
				}
				else
				{
					channelMessage.AddRange(response.Results.OrderBy(x => x.CreatedOnUtc).ToList());
				}
				hasNextScroll = response.HasNext;
			}

			//unread message
			//if (PageNumber == 1 && IsScrolled == false)
			//{
			//    if (unreadMessage != null && unreadMessage.Any())
			//    {
			//        channelMessage.AddRange(unreadMessage);
			//    }
			//}

			return new JsonResult(new { data = channelMessage, hasNextScroll = hasNextScroll });
		}

        /// <summary>
        /// Sends the notification.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task<IActionResult> SendNotification(NotificationSendRequest data)
		{
			data.SourceAppId = Convert.ToInt32(Request.Cookies["NotificationApplicationId"]);

			if (data.AttachmentList != null && data.AttachmentList.Any())
			{
				data.IsAttachment = 1;
			}
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			int response = await _client.SendNotification(data);
			return new JsonResult(response);
		}

        /// <summary>
        /// Gets the channel participants.
        /// </summary>
        /// <param name="ChannelId">The channel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetChannelParticipants(string ChannelId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);

			List<NotificationChannelSubscriptionViewModel> response = await _client.GetChannelParticipants(ChannelId);

			if (response != null && response.Any())
			{
				var result = response.Where(p => p.SSUserId != null).GroupBy(p => p.SSUserId).Select(grp => grp.FirstOrDefault().SSUserId);
				List<UserRoleDetailViewModel> userRoleName = new List<UserRoleDetailViewModel>();
				if (result != null && result.Any())
				{
					userRoleName = await _sharedClient.GetUsersPrimaryRoleName(result.ToList());
				}

				foreach (var item in response)
				{
					item.UserRoleDescription = userRoleName.Any(x => x.UserId == item.SSUserId) ? userRoleName.Where(x => x.UserId == item.SSUserId).FirstOrDefault().RoleName : string.Empty;
				}
			}
			return new JsonResult(response);
		}

        /// <summary>
        /// Gets the new channel messages.
        /// </summary>
        /// <param name="ChannelId">The channel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetNewChannelMessages(string ChannelId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			//int timeZoneOffSet = GetSessionIntValue(Constants.TimeZoneDiffSessionKey);
			int timeZoneOffSet = 0;
			string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
			Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
			List<NotificationUserMessageViewModel> response = await _client.GetNewChannelMessages(ChannelId, timeZoneOffSet);
			return new JsonResult(response);
		}

        /// <summary>
        /// Gets the channel detail.
        /// </summary>
        /// <param name="ChannelId">The channel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetChannelDetail(string ChannelId)
        {
            NotificationChannelViewModel response = await GetChannelDetailAsync(ChannelId);
            return new JsonResult(response);
        }

		/// <summary>
		/// Get Channel Detail Async
		/// </summary>
		/// <param name="ChannelId"></param>
		/// <returns></returns>
		private async Task<NotificationChannelViewModel> GetChannelDetailAsync(string ChannelId)
        {
            _client.AccessToken = await GetNotificationAccessToken(_ticketStore);
            _sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
            //int timeZoneOffSet = GetSessionIntValue(Constants.TimeZoneDiffSessionKey);
            int timeZoneOffSet = 0;
            string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
            Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
            NotificationChannelViewModel response = await _client.GetChannelDetail(ChannelId, timeZoneOffSet);
            if (response != null)
            {
                List<VesselInfoViewModel> vesselList = new List<VesselInfoViewModel>();
                if (!string.IsNullOrWhiteSpace(response.VesselId))
                {
                    var result = new List<string>() { response.VesselId };
                    vesselList = await _sharedClient.GetVesselsName(result.ToList());
                    if (vesselList != null && vesselList.Any())
                    {
                        response.VesselName = vesselList.FirstOrDefault().VesselName ?? string.Empty;
                        response.VesselIMONumber = vesselList.FirstOrDefault().IMONumber ?? string.Empty;
                    }
                    else
                    {
                        response.VesselName = string.Empty;
                        response.VesselIMONumber = string.Empty;
                    }
                }
                else
                {
                    response.VesselName = string.Empty;
                    response.VesselIMONumber = string.Empty;
                }
            }

            return response;
        }

        /// <summary>
        /// Gets the channel header details.
        /// </summary>
        /// <param name="ChannelId">The channel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetChannelHeaderDetails(string ChannelId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);			
			int timeZoneOffSet = 0;
			string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
			Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
			NotificationChannelViewModel response = await _client.GetChannelDetail(ChannelId, timeZoneOffSet);			
			return new JsonResult(response);
		}


		/// <summary>
		/// Gets the participants list paged.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <param name="page">The page.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="tempSelectedUsers">The Temporary Selected Users.</param>
		/// <returns></returns>
		public async Task<JsonResult> GetParticipantsListPaged(string term, int page, string vesselId, string tempSelectedUsers)
		{
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			string userId = Request.Cookies["NotificationUserId"];
			List<string> tempSelectedUsersList = string.IsNullOrEmpty(tempSelectedUsers) ? new List<string>() : tempSelectedUsers.Split(',').ToList();
			
			Select2ResponseViewModel<List<ParticipantsDetailsViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<ParticipantsDetailsViewModel>>();
			select2ResponseViewModel.Results = new List<ParticipantsDetailsViewModel>();

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 10;
			pageRequest.Start = (pageRequest.Length * (page - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "ParticipantName" });

			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});

			DataTablePageResponse<List<ParticipantsDetailsViewModel>> response = new DataTablePageResponse<List<ParticipantsDetailsViewModel>>();

			response = await _sharedClient.GetParticipants(pageRequest, term, vesselId, userId, tempSelectedUsersList);

			select2ResponseViewModel.Results = response.Data;
			select2ResponseViewModel.Pagination = new Pagination();
			select2ResponseViewModel.Pagination.More = response.RecordsTotal > (pageRequest.Length * page);

			return new JsonResult(select2ResponseViewModel);
		}

        /// <summary>
        /// Creates the notification channel.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> CreateNotificationChannel(CreateChannelRequestDtoViewModel request)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);

			if (request.AttachmentList != null && request.AttachmentList.Any())
			{
				request.IsAttachment = 1;
			}

			bool isValidate = request.IsSaveAsDraft ? true : ValidateCreateNotificationChannel(request);
			int response = 0;
			if (isValidate)
			{
				request.SsUserId = Request.Cookies["NotificationUserId"];
				NotificationChannelSubscription sender = new NotificationChannelSubscription();
				sender.SSUserId = Request.Cookies["NotificationUserId"];
				sender.Username = HttpContext.Session.GetString(Constants.UserNameSessionKey);
				int applicationId = Convert.ToInt32(Request.Cookies["NotificationApplicationId"]);
				response = await _client.CreateChannel(request, sender,applicationId);
			}
			return new JsonResult(response);
		}

        /// <summary>
        /// Validates the create notification channel.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private bool ValidateCreateNotificationChannel(CreateChannelRequestDtoViewModel request)
		{
			if (string.IsNullOrWhiteSpace(request.Title))
			{
				return false;
			}

			if (string.IsNullOrWhiteSpace(request.InitialMsg))
			{
				return false;
			}

			if (!(request.Subscribers != null && request.Subscribers.Any()))
			{
				return false;
			}
			return true;
		}

        /// <summary>
        /// Gets the area list.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAreaList()
		{
			List<Lookup> areaList = new List<Lookup>();
			areaList = _client.GetAreaList();
			return new JsonResult(areaList);
		}

        /// <summary>
        /// Gets the unread channel count.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetReadMessageParticipants(int messageId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			List<NotificationChannelSubscriptionViewModel> result = await _client.GetReadMessageParticipants(messageId);
			return new JsonResult(new { data = result, readby = result.Count });
		}

        /// <summary>
        /// Gets the last read delivered message for channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetLastReadDeliveredMessageForChannel(int channelId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			List<LastReadMessageResponseViewModel> result = await _client.GetLastReadDeliveredMessageForChannel(channelId);
			return new JsonResult(result);
		}

        /// <summary>
        /// Gets my last all read MSG for channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetMyLastAllReadMsgForChannel(int channelId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			ChannelMessageViewModel result = await _client.GetMyLastAllReadMsgForChannel(channelId);
			return new JsonResult(result);
		}

        /// <summary>
        /// Gets the user primary role.
        /// </summary>
        /// <param name="userIds">The user ids.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetUserPrimaryRole(List<string> userIds)
		{
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);

			List<UserRoleDetailViewModel> userRoleName = new List<UserRoleDetailViewModel>();
			if (userIds != null && userIds.Any())
			{
				userRoleName = await _sharedClient.GetUsersPrimaryRoleName(userIds);
			}
			return new JsonResult(userRoleName);
		}


        /// <summary>
        /// Gets the vessel responsibilities.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVesselResponsibilities(string vesselId)
		{
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			string userId = Request.Cookies["NotificationUserId"];
			List<ParticipantsDetailsViewModel> result = await _sharedClient.GetVesselResponsibilities(vesselId, userId);
			return new JsonResult(result);
		}

        /// <summary>
        /// Adds the new participants to channel.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> AddNewParticipantsToChannel(CreateChannelRequestDtoViewModel request)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);

			bool response = false;

			if (request.Subscribers != null && request.Subscribers.Any())
			{
				response = await _client.AddNewParticipantsToChannel(request);
				if (response)
				{
					return new JsonResult(new { Response = "success", Message = "User(s) added successfully." });
				}
				else
				{
					return new JsonResult(new { Response = "error", Message = "Failed to add user(s)." });
				}
			}
			else
			{
				return new JsonResult(new { Response = "error", Message = "Please select a user(s)." });
			}
		}

        /// <summary>
        /// Signals the r test.
        /// </summary>
        /// <returns></returns>
        public IActionResult SignalRTest()
		{
			return View();
		}

        /// <summary>
        /// Gets the channel message test.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetChannelMessageTest(string message)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			bool result = await _client.GetChannelMessagesTest(message);
			return new JsonResult(result);
		}

        /// <summary>
        /// Downloads the attachment.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadAttachment(AttachmentViewModel request)
		{
			_notificationDocumentClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			DownloadDocumentRequest documentRequest = new DownloadDocumentRequest { DocumentId = request.EttId };
			var result = await _notificationDocumentClient.DownloadDocument(documentRequest);
			string byteString = (result.DocumentByteStream != null && result.DocumentByteStream.Length > 0) ? Convert.ToBase64String(result.DocumentByteStream) : null;
			string fileName = !string.IsNullOrWhiteSpace(request.Description) ? request.Description : result.FileName.Split('.')[0];
			string extension = EnumsHelper.GetEnumNameFromKeyValue(typeof(DocumentFileType), request.FileExtension);
			return new JsonResult(new { filename = fileName, bytes = byteString, fileType = extension, status = result.Status });
		}

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> UploadFile(UploadAttachmentRequestViewModel request)
		{
			_notificationDocumentClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			request.LoginUserName = HttpContext.Session.GetString(Constants.UserNameSessionKey);
			UploadAttachmentResponse result = await _notificationDocumentClient.UploadFile(request);
			return new JsonResult(new { Response = result.IsOperationSuccess, Data = result });
		}


        /// <summary>
        /// Notifications the mobile chat detail.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public IActionResult NotificationMobileChatDetail(string request)
		{
			NotificationMobileChatDetailViewModel model = CommonUtil.GetDecryptedRequest<NotificationMobileChatDetailViewModel>(_provider, Constants.NotificationMobileChatDetailEncKey, request);
			model.SessionStorageDetails = SetSessionStorageDetail(_provider, model);
			return View(model);
		}

		/// <summary>
		/// Notification mobile chat details
		/// </summary>
		/// <param name="urlParameter"></param>
		/// <param name="isFilterChange"></param>
		/// <returns></returns>
		public IActionResult NotificationMobileChatDetails(string urlParameter, bool isFilterChange)
		{
			NotificationSearchRequest searchParameter = new NotificationSearchRequest();
			NotificationMobileChatDetailViewModel model = new NotificationMobileChatDetailViewModel();
			if (!string.IsNullOrWhiteSpace(urlParameter))
			{
				searchParameter = JsonConvert.DeserializeObject<NotificationSearchRequest>(urlParameter);
			}

			CookieOptions option = new CookieOptions();
			option.Expires = DateTime.Now.AddMonths(6);
			if (!string.IsNullOrWhiteSpace(searchParameter.NotificationJwtToken))
			{
				string key = StoredNoticationToken(searchParameter.NotificationJwtToken, _ticketStore);
				Response.Cookies.Append("RetrievingKey", key, option);
				Response.Cookies.Append("NotificationUserId", searchParameter.UserId, option);
				Response.Cookies.Append("NotificationUserEmailId", searchParameter.UserEmailId, option);
				Response.Cookies.Append("NotificationApplicationId", searchParameter.ApplicationId.ToString(), option);
				Response.Cookies.Append("SignalRURL", AppSettings.SignalRUrl, option);
				//ToDo : Need to change the hard cord to read from claims of jwt
				Response.Cookies.Append("NotificationRoles", "CRRA00000001,FNRA00000017,FNRA00000019,FNRA00000022,FNRA00000023,FNRA00000024,FNRA00000026,GLAS00000001,GLAS00000002,GLAS00000003,GLAS00000008,GLAS00000012,GLAS00000025,GLAS00000038,GLAS00000094,GLAS00000095,GLAS00000122,GLAS00000125,GLAS00000145,MRRA00000001,PUMA00000003,PURA00000003,-1", option);

				if (!string.IsNullOrWhiteSpace(searchParameter.NavigationURL))
				{
					Response.Cookies.Append("NotificationNavigationURL", searchParameter.NavigationURL, option);
				}
			}
			model.ChannelId = searchParameter.ChannelId;
			model.IsFromOtherSource = true;
			model.SessionStorageDetails = SetSessionStorageDetail(_provider, model);
			model.IsFilterChange = isFilterChange;
			return View("NotificationMobileChatDetail", model);
		}

		/// <summary>
		/// Get channel detailss.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetChannelDetails(string channelId)
		{
			NotificationChannelViewModel response = await GetChannelDetailAsync(channelId);
			NotificationMobileChatDetailViewModel model = new NotificationMobileChatDetailViewModel();
			model.ChannelId = channelId != null ? Convert.ToInt32(channelId) : 0;
			if (response != null)
			{
				model.VesselId = response.VesselId;
				model.VesselName = response.VesselName;
				model.VesselIMONumber = response.VesselIMONumber;
				model.ActiveParticipantsCount = response.ActiveParticipantsCount;
				model.ParticipantCount = response.ActiveParticipantsCount;
				model.Title = response.Title;

				bool isGeneral = response.CategoryId.ToString() == EnumsHelper.GetKeyValue(MessageCategoryEnum.General);
				model.IsGeneralCat = isGeneral;
				NotificationMobileInfoViewModel infoPage = new NotificationMobileInfoViewModel();
				infoPage.ChannelId = model.ChannelId;
				infoPage.VesIMONumber = model.VesselIMONumber;
				infoPage.VesselName = model.VesselName;
				infoPage.VesselId = model.VesselId;
				infoPage.IsGeneralCat = isGeneral;
				infoPage.IsInfoPageLoaded = true;
				model.NotificationMobileInfoURL = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationMobileInfoEncKey, infoPage);

				NotificationMobileInfoViewModel particiapntURL = new NotificationMobileInfoViewModel();
				particiapntURL.ChannelId = model.ChannelId;
				particiapntURL.VesIMONumber = model.VesselIMONumber;
				particiapntURL.VesselName = model.VesselName;
				particiapntURL.VesselId = model.VesselId;
				particiapntURL.IsGeneralCat = isGeneral;
				particiapntURL.IsInfoPageLoaded = false;
				model.NotificationMobileParticipantURL = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationMobileInfoEncKey, particiapntURL);
			}
			return new JsonResult(model);
		}

		/// <summary>
		/// Notification mobile chat discussion
		/// </summary>
		/// <param name="urlParameter"></param>
		/// <param name="isFilterChange"></param>
		/// <returns></returns>
		public async Task<IActionResult> NotificationMobileChatDiscussion(string urlParameter, bool isFilterChange)
		{
			NotificationSearchRequest searchParameter = new NotificationSearchRequest();
			NotificationMobileDiscussionViewModel viewModel = new NotificationMobileDiscussionViewModel();
			if (!string.IsNullOrWhiteSpace(urlParameter))
			{
				searchParameter = JsonConvert.DeserializeObject<NotificationSearchRequest>(urlParameter);
			}

			CookieOptions option = new CookieOptions();
			option.Expires = DateTime.Now.AddMonths(6);
			if (!string.IsNullOrWhiteSpace(searchParameter.NotificationJwtToken))
			{
				string key = StoredNoticationToken(searchParameter.NotificationJwtToken, _ticketStore);
				Response.Cookies.Append("RetrievingKey", key, option);
				Response.Cookies.Append("NotificationUserId", searchParameter.UserId, option);
				Response.Cookies.Append("NotificationUserEmailId", searchParameter.UserEmailId, option);
				Response.Cookies.Append("NotificationApplicationId", searchParameter.ApplicationId.ToString(), option);
				Response.Cookies.Append("SignalRURL", AppSettings.SignalRUrl, option);
				//ToDo : Need to change the hard cord to read from claims of jwt
				Response.Cookies.Append("NotificationRoles", "CRRA00000001,FNRA00000017,FNRA00000019,FNRA00000022,FNRA00000023,FNRA00000024,FNRA00000026,GLAS00000001,GLAS00000002,GLAS00000003,GLAS00000008,GLAS00000012,GLAS00000025,GLAS00000038,GLAS00000094,GLAS00000095,GLAS00000122,GLAS00000125,GLAS00000145,MRRA00000001,PUMA00000003,PURA00000003,-1", option);

				if (!string.IsNullOrWhiteSpace(searchParameter.NavigationURL))
				{
					Response.Cookies.Append("NotificationNavigationURL", searchParameter.NavigationURL, option);
				}
			}

			var response = await GetChannelDetailDraftAsync(searchParameter.ChannelId.ToString());
			if(response != null)
            {
				viewModel.NewMessageDetails = new NewMessageParametersViewModel();
				viewModel.NewMessageDetails.ChannelId = response.ChannelId;
				viewModel.NewMessageDetails.CategoryId = response.CategoryId;
				viewModel.NewMessageDetails.VesselId = response.VesselId;
				viewModel.NewMessageDetails.VesselName = response.VesselName;
				viewModel.NewMessageDetails.IsGeneralCat = response.IsGeneralCat;
				viewModel.NewMessageDetails.IsSaveAsDraft = response.IsSaveAsDraft;
            }
			viewModel.IsFromOtherSource = true;
			viewModel.SessionStorageDetails = SetSessionStorageDetail(_provider, viewModel);
			viewModel.IsFilterChange = isFilterChange;
			return View("NotificationMobileDiscussion", viewModel);
		}


		/// <summary>
		/// Notifications the mobile information.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public IActionResult NotificationMobileInfo(string request)
		{			
			NotificationMobileInfoViewModel model = CommonUtil.GetDecryptedRequest<NotificationMobileInfoViewModel>(_provider, Constants.NotificationMobileInfoEncKey, request);
			model.SessionStorageDetails = SetSessionStorageDetail(_provider, request);
			return View(model);
		}

        /// <summary>
        /// Notifications the mobile discussion.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public IActionResult NotificationMobileDiscussion(string request)
		{
			NotificationMobileDiscussionViewModel viewModel = new NotificationMobileDiscussionViewModel();
			if (!string.IsNullOrWhiteSpace(request))
            {
				viewModel = CommonUtil.GetDecryptedRequest<NotificationMobileDiscussionViewModel>(_provider, Constants.NotificationMobileDiscussionEncKey, request);
			}			
			viewModel.SessionStorageDetails = SetSessionStorageDetail(_provider, request);
			return View(viewModel);
		}


        /// <summary>
        /// Navigates to create discussion.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <returns></returns>
        public IActionResult NavigateToCreateDiscussion(NotificationMobileDiscussionViewModel details)
		{
			string request = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationMobileDiscussionEncKey, details);
			return Json(Url.Action("NotificationMobileDiscussion", new { request }));
		}

        /// <summary>
        /// Deletes the attachment.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteAttachment(AttachmentViewModel request)
		{
			_notificationDocumentClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			DownloadDocumentRequest documentRequest = new DownloadDocumentRequest
			{
				DocumentId = request.EttId
			};
			var result = await _notificationDocumentClient.DeleteDocument(documentRequest);
			return new JsonResult(true);
		}

        /// <summary>
        /// Gets the message categories.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetMessageCategories()
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			List<MessageCategoryDtoViewModel> result = await _client.GetMessageCategories(Convert.ToInt32(Request.Cookies["NotificationApplicationId"]));
			return new JsonResult(result);
		}

        /// <summary>
        /// Navigates to general message.
        /// </summary>
        /// <param name="isMobile">if set to <c>true</c> [is mobile].</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public IActionResult NavigateToGeneralMessage(bool isMobile, string request)
		{
			if (isMobile)
			{
				return Json(Url.Action("NotificationMobileDiscussion", new { request = request }));
			}
			else
			{
				return Json(Url.Action("", "Notification", new { searchRequest = request }));
			}
		}

        /// <summary>
        /// Navigates to record message.
        /// </summary>
        /// <param name="isMobile">if set to <c>true</c> [is mobile].</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public IActionResult NavigateToRecordMessage(bool isMobile, string request)
		{
			if (isMobile)
			{
				NotificationMobileDiscussionViewModel viewModel = new NotificationMobileDiscussionViewModel
				{
					NewMessageDetails = JsonConvert.DeserializeObject<NewMessageParametersViewModel>(request)
				};
				string encryptedRequest = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationMobileDiscussionEncKey, viewModel);
				return Json(Url.Action(nameof(NotificationMobileDiscussion), new { request = encryptedRequest }));
			}
			else
			{
				return Json(Url.Action("", "Notification", new { searchRequest = request }));
			}
		}

        /// <summary>
        /// Gets the record level details.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRecordLevelDetails(int channelId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			int applicationId = Convert.ToInt32(Request.Cookies["NotificationApplicationId"]);
			RecordLevelDetailsViewModel result = await _client.GetRecordLevelDetails(channelId, applicationId);
			return new JsonResult(result);
		}

        /// <summary>
        /// Gets the channel details.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> NavigateToChannelRecordDetails(int channelId, string vesselId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			int applicationId = Convert.ToInt32(Request.Cookies["NotificationApplicationId"]);
			ChannelAdditionalDetailsResponse response = await _client.GetChannelAdditionalDetails(channelId, applicationId);
			
			if (response != null && applicationId == 2)
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
				string encryptedRequest = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationRecordDetailsEncKey, JsonConvert.DeserializeObject<ContextParameter>(response.ContextPayload));
				//return new JsonResult(new { NavigateURL = Request.Cookies["NotificationNavigationURL"] + urlDetails + encryptedRequest + "&VesselId=" + encryptedVesselUrl, Type = "WindowOpen" });
				return new JsonResult(new { NavigateURL = Request.Cookies["NotificationNavigationURL"] + urlDetails + encryptedRequest + "&VesselId=" + encryptedVesselUrl, Type = "RedirectParent" });

			}
			else
			{
				if(response != null)
                {
					return new JsonResult(new { NavigateURL = response.NavigationParameters, ContextPayload = response.ContextPayload, Type = "External" });
				}				
			}

			return new JsonResult(new { NavigateURL = "", Type = "" });
		}

        /// <summary>
        /// Gets the current user details.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetCurrentUserDetails()
		{
			string userId = Request.Cookies["NotificationUserId"];

			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);

			NotificationUserDetails userDetails = await _client.GetUserDetails(userId);

			List<string> userIds = new List<string> { userId };
			List<UserRoleDetailViewModel> rolesDetails = await _sharedClient.GetUsersPrimaryRoleName(userIds);
			userDetails.PrimaryRoleName = rolesDetails.FirstOrDefault().RoleName;

			return new JsonResult(userDetails);
		}

		/// <summary>
		/// Gets the channel list.
		/// </summary>
		/// <param name="channelRequest"></param>
		/// <param name="sessionDetails"></param>
		/// <returns></returns>
		public async Task<JsonResult> GetChannelList(ChannelRequest channelRequest, string sessionDetails)
		{
			ChannelRequest filter = CommonUtil.GetSessionStorageFilter<ChannelRequest>(_provider, sessionDetails);
			if (filter != null)
			{
				filter.SearchText = channelRequest.SearchText;
				filter.IsSearchClicked = channelRequest.IsSearchClicked;
			}
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);

			//TODO: Replace Cookies with session
			//int test = GetSessionIntValue(Constants.TimeZoneDiffSessionKey);
			//var testTempDate = TempData[Constants.TimeZoneDiffSessionKey];
			//var testValue = CommonUtil.GetSessionObject<int?>(HttpContext.Session, Constants.TimeZoneDiffSessionKey);

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 100;
			pageRequest.Start = (pageRequest.Length * (channelRequest.PageNumber - 1)) + 1;
			pageRequest.Columns = new List<Column>();
			pageRequest.Order = new List<Order>();
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "ChannelTitle" });

			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});

			int timeZoneOffSet = 0;
			string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
			Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
			PagedResult<PagedList<NotificationChannelViewModel>> response = await _client.GetChannelList(pageRequest, filter, timeZoneOffSet);
			List<NotificationChannelViewModel> channelList = new List<NotificationChannelViewModel>();
			bool hasNextScroll = false;
			var total = response != null ? response.TotalCount : 0;
			if (response != null && response.Results != null && response.Results.Any())
			{
				channelList = response.Results;
				hasNextScroll = response.HasNext;
			}
			//hasNextScroll = true;
			return new JsonResult(new { data = channelList, hasNextScroll = hasNextScroll, totalCount = total });
		}

		/// <summary>
		/// Get channel detail by id
		/// </summary>
		/// <param name="ChannelId"></param>
		/// <returns></returns>
		public async Task<JsonResult> GetChannelDetailById(string ChannelId)
		{
			ChannelRequest filter = new ChannelRequest();
			filter.ChannelId = !string.IsNullOrWhiteSpace(ChannelId) ? Convert.ToInt32(ChannelId) : 0;
			filter.PageNumber = 1;
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);

			DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
			pageRequest.Length = 1;
			pageRequest.Start = 0;
			pageRequest.Columns = new List<Column>();
			pageRequest.Order = new List<Order>();
			pageRequest.Columns = new List<Column>();
			pageRequest.Columns.Add(new Column() { Name = "ChannelTitle" });

			pageRequest.Order = new List<Order>();
			pageRequest.Order.Add(new Order()
			{
				Column = 0,
				Dir = "asc"
			});

			int timeZoneOffSet = 0;
			string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
			Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
			PagedResult<PagedList<NotificationChannelViewModel>> response = await _client.GetChannelList(pageRequest, filter, timeZoneOffSet);
			NotificationChannelViewModel channelDetail = new NotificationChannelViewModel();
			
			if (response != null && response.Results != null && response.Results.Any())
			{
				channelDetail = response.Results.FirstOrDefault();
			}
			return new JsonResult(channelDetail);
		}

		/// <summary>
		/// Gets the channel detail draft.
		/// </summary>
		/// <param name="ChannelId">The channel identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetChannelDetailDraft(string ChannelId)
        {            
            DraftMessageParametersViewModel response = await GetChannelDetailDraftAsync(ChannelId);
            return new JsonResult(response);
        }

		/// <summary>
		/// Gets the channel detail draft async.
		/// </summary>
		/// <param name="ChannelId"></param>
		/// <returns></returns>
		private async Task<DraftMessageParametersViewModel> GetChannelDetailDraftAsync(string ChannelId)
        {
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			DraftMessageParametersViewModel response = await _client.GetChannelDetailDraft(ChannelId);
            if (response != null)
            {
                List<VesselInfoViewModel> vesselList = new List<VesselInfoViewModel>();
                if (!string.IsNullOrWhiteSpace(response.VesselId))
                {
                    var result = new List<string>() { response.VesselId };
                    vesselList = await _sharedClient.GetVesselsName(result.ToList());
                    if (vesselList != null && vesselList.Any())
                    {
                        response.VesselName = vesselList.FirstOrDefault().VesselName ?? string.Empty;
                        response.VesselIMONumber = vesselList.FirstOrDefault().IMONumber ?? string.Empty;
                    }
                    else
                    {
                        response.VesselName = string.Empty;
                        response.VesselIMONumber = string.Empty;
                    }
                }
                else
                {
                    response.VesselName = string.Empty;
                    response.VesselIMONumber = string.Empty;
                }

                if (response.Participants != null && response.Participants.Any())
                {
                    var result = response.Participants.Where(p => p.SSUserId != null).GroupBy(p => p.SSUserId).Select(grp => grp.FirstOrDefault().SSUserId);
                    List<UserRoleDetailViewModel> userRoleName = new List<UserRoleDetailViewModel>();
                    if (result != null && result.Any())
                    {
                        userRoleName = await _sharedClient.GetUsersPrimaryRoleName(result.ToList());
                    }

                    foreach (var item in response.Participants)
                    {
                        item.UserRoleDescription = userRoleName.Any(x => x.UserId == item.SSUserId) ? userRoleName.Where(x => x.UserId == item.SSUserId).FirstOrDefault().RoleName : string.Empty;
                    }
                }

            }
            return response;
        }

        /// <summary>
        /// Notifications the create channel.
        /// </summary>
        /// <param name="createRequest">The create request.</param>
        /// <returns></returns>
        public IActionResult NotificationCreateChannel(string createRequest)
		{
			NewMessageParametersViewModel NewMessageDetails = new NewMessageParametersViewModel();
			if (!String.IsNullOrWhiteSpace(createRequest))
			{
				var blob = Convert.FromBase64String(createRequest);
				var json = Encoding.UTF8.GetString(blob);
				NewMessageDetails = JsonConvert.DeserializeObject<NewMessageParametersViewModel>(json);
			}
			NewMessageDetails.IsStandaloneCreateChannel = true;
			CookieOptions option = new CookieOptions();
			option.Expires = DateTime.Now.AddMonths(6);
			string key = StoredNoticationToken(NewMessageDetails.NotificationJwtToken, _ticketStore);
			Response.Cookies.Append("RetrievingKey", key, option);
			Response.Cookies.Append("NotificationUserId", NewMessageDetails.UserId, option);
			Response.Cookies.Append("NotificationUserEmailId", NewMessageDetails.UserEmailId, option);
			Response.Cookies.Append("NotificationApplicationId", NewMessageDetails.ApplicationId.ToString(), option);
			Response.Cookies.Append("SignalRURL", AppSettings.SignalRUrl, option);
			//ToDo : Need to change the hard cord to read from claims of jwt
			Response.Cookies.Append("NotificationRoles", "CRRA00000001,FNRA00000017,FNRA00000019,FNRA00000022,FNRA00000023,FNRA00000024,FNRA00000026,GLAS00000001,GLAS00000002,GLAS00000003,GLAS00000008,GLAS00000012,GLAS00000025,GLAS00000038,GLAS00000094,GLAS00000095,GLAS00000122,GLAS00000125,GLAS00000145,MRRA00000001,PUMA00000003,PURA00000003,-1", option);
			return View(NewMessageDetails);
		}

        /// <summary>
        /// Notifications the container.
        /// </summary>
        /// <returns></returns>
        public IActionResult NotificationContainer()
        {
			return View();
        }

        /// <summary>
        /// Creates the discussion container.
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateDiscussionContainer()
		{
			return View();
		}

        /// <summary>
        /// Gets the unread channel count.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetUnreadChannelCount()
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			int response = await _client.GetUnreadChannelCount();
			return new JsonResult(response);
		}

        /// <summary>
        /// Gets the vessel lookup.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="q">The q.</param>
        /// <param name="type">The type.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task<JsonResult> GetVesselLookup(string term, string q, string type, int page)
		{
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);

			NavigationTreeViewModel navigationTreeVM = new NavigationTreeViewModel();
			SetNavigationTreeViewModel(navigationTreeVM);

			ManagementVesselFilter request = new ManagementVesselFilter();
			request.FetchOnlyActivatedCoys = true;

			if (string.IsNullOrWhiteSpace(q))
			{
				request.FleetMenuType = navigationTreeVM.UserType == UserType.Internal ? Constants.Responsibilities : Constants.Client;
			}
			else
			{
				request.FleetMenuType = null;
				request.FetchOnlyActivatedCoys = null;
			}

			request.VesselName = q;
			request.AccountingCompanyId = q;
			request.FetchOnlyActivatedAccountingCompanies = false;
			request.AvoidCoyIdCheck = null;
			request.IsFetchForAllCompanies = false;
			request.IsFetchForAllLocalCompanies = false;
			request.IsQuickSearch = true;
			request.IsVesselInPurchasingManagement = false;
			request.IsVesselsCurrentlyInManagement = true;

			List<ManagementVesselDetailViewModel> response = await _sharedClient.PostGetManagementVesselLookup(request);
			Select2ResponseViewModel<List<ManagementVesselDetailViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<ManagementVesselDetailViewModel>>();
			select2ResponseViewModel.Results = new List<ManagementVesselDetailViewModel>();
			select2ResponseViewModel.Results = response;
			select2ResponseViewModel.Pagination = new Pagination();

			return new JsonResult(select2ResponseViewModel);

		}

        /// <summary>
        /// Sets the navigation TreeView model.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        [NonAction]
		private void SetNavigationTreeViewModel(NavigationTreeViewModel navigationTreeViewModel)
		{
			navigationTreeViewModel.Exclusion = new List<UserMenuItemType>();

			navigationTreeViewModel.UserType = HttpContext.Session.GetString("UserType") != null ? (UserType)Enum.Parse(typeof(UserType), HttpContext.Session.GetString("UserType")) : UserType.Internal;
			if (navigationTreeViewModel.UserType == UserType.Client)
			{
				navigationTreeViewModel.PreloadUserFleet = false;
				navigationTreeViewModel.Exclusion.AddRange(new List<UserMenuItemType> { UserMenuItemType.MyResponsibilities, UserMenuItemType.MyFleets, UserMenuItemType.MyOffices, UserMenuItemType.MotoMoco });
			}
			else
			{
				navigationTreeViewModel.PreloadUserFleet = true;
				navigationTreeViewModel.Exclusion.AddRange(new List<UserMenuItemType> { UserMenuItemType.MotoMoco });
			}
		}

		/// <summary>
		/// Adds the messaging user if not exists.
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> AddMessagingUserIfNotExists()
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			NotificationChannelSubscription userToBeAdded = new NotificationChannelSubscription();
			userToBeAdded.SSUserId = Request.Cookies["NotificationUserId"];
			userToBeAdded.UsrEmail = Request.Cookies["NotificationUserEmailId"];
			UserViewModel userDetail = await _sharedClient.GetUserDetail();
			if (userDetail != null)
			{
				userToBeAdded.Username = userDetail.UserDisplayName;
			}

			bool response = await _client.AddMessagingUserIfNotExists(userToBeAdded);
			return new JsonResult(response);
		}

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="messageViewModel">The message view model.</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteMessage(ChannelMessageViewModel messageViewModel)
        {
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			bool response = await _client.DeleteMessage(new ChannelMessage 
															{
																ChannelId = messageViewModel.ChannelId,
																Id = messageViewModel.Id,
																MessageDescription = messageViewModel.MessageDescription
															});
			return new JsonResult(response);
		}

		/// <summary>
		/// Deletes the channel by identifier.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> DeleteChannelById(string channelId)
		{
			int intChannelId = Convert.ToInt16(channelId);

			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			int applicationId = Convert.ToInt32(Request.Cookies["NotificationApplicationId"]);
			NotificationChannel response = await _client.DeleteChannelById(intChannelId, applicationId);
			return new JsonResult(response);
		}

        /// <summary>
        /// Edits the message.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task<IActionResult> EditMessage(ChannelMessage data)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);

			if (data.AttachmentList != null && data.AttachmentList.Any())
			{
				data.IsAttachment = 1;
			}
			bool response = await _client.EditMessage(data);
			return new JsonResult(response);
		}

        /// <summary>
        /// Gets the channel message by identifier.
        /// </summary>
        /// <param name="MessageId">The message identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetChannelMessageById(int MessageId)
		{
			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			_sharedClient.AccessToken = await GetNotificationAccessToken(_ticketStore);
			EditMessageViewModel response = await _client.GetChannelMessageById(MessageId);
			return new JsonResult(response);
		}

		/// <summary>
		/// Gets the source URL for notification.
		/// </summary>
		/// <param name="sessionDetails">The page key.</param>
		/// <returns></returns>
		public IActionResult GetSourceURLForNotification(string sessionDetails)
		{
			return new JsonResult(CommonUtil.GetSessionStorageSourceURL(_provider, sessionDetails));
		}

		/// <summary>
		/// Set session storage filter for notification
		/// </summary>
		/// <param name="channelRequest"></param>
		/// <param name="sessionDetails"></param>
		/// <returns></returns>
		public IActionResult SetSessionStorageFilterForNotification(ChannelRequest channelRequest, string sessionDetails)
		{

			ChannelRequest filter = CommonUtil.GetSessionStorageFilter<ChannelRequest>(_provider, sessionDetails);
			if(filter != null)
            {
				filter.SearchText = channelRequest.SearchText;
				filter.IsSearchClicked = channelRequest.IsSearchClicked;
				filter.SelectedChannelId = channelRequest.SelectedChannelId;
            }
			return new JsonResult(CommonUtil.SetSessionStorageFilter(_provider, sessionDetails, filter));
		}
		
		/// <summary>
		/// Get session storage filter for list
		/// </summary>
		/// <param name="sessionDetails"></param>
		/// <returns></returns>
		public IActionResult GetSessionStorageFilterForList(string sessionDetails)
		{
			return new JsonResult(CommonUtil.GetSessionStorageFilter<ChannelRequest>(_provider, sessionDetails));
		}

		/// <summary>
		/// Get session storage filter for chat detail
		/// </summary>
		/// <param name="sessionDetails"></param>
		/// <returns></returns>
		public IActionResult GetSessionStorageFilterForChatDetail(string sessionDetails)
		{
			return new JsonResult(CommonUtil.GetSessionStorageFilter<NotificationMobileChatDetailViewModel>(_provider, sessionDetails));
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

			_client.AccessToken = await GetNotificationAccessToken(_ticketStore);
			int timeZoneOffSet = 0;
			string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
			Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
			var result = await _client.GetNoteDetailsById(noteId, timeZoneOffSet);
			return new JsonResult(result);
		}

		/// <summary>
		/// Set session storage filter selected channel
		/// </summary>
		/// <param name="channelRequest"></param>
		/// <param name="sessionDetails"></param>
		/// <returns></returns>
		public IActionResult SetSessionStorageFilterSelectedChannel(ChannelRequest channelRequest, string sessionDetails)
		{
			ChannelRequest filter = CommonUtil.GetSessionStorageFilter<ChannelRequest>(_provider, sessionDetails);
			if (filter != null)
			{
				filter.SelectedChannelId = channelRequest.SelectedChannelId;
			}
			return new JsonResult(CommonUtil.SetSessionStorageFilter(_provider, sessionDetails, filter));
		}

		/// <summary>
		/// Set session storage filter
		/// </summary>
		/// <param name="sessionDetails"></param>
		/// <param name="newSessionDetails"></param>
		/// <returns></returns>
		public IActionResult SetSessionStorageFilter(string sessionDetails, string newSessionDetails)
		{
			ChannelRequest filter = CommonUtil.GetSessionStorageFilter<ChannelRequest>(_provider, newSessionDetails);

			return new JsonResult(CommonUtil.SetSessionStorageFilter(_provider, sessionDetails, filter));
		}

		/// <summary>
		/// Set session storage source url
		/// </summary>
		/// <param name="sessionDetails"></param>
		/// <param name="newSessionDetails"></param>
		/// <returns></returns>
		public IActionResult SetSessionStorageSourceURL(string sessionDetails, string newSessionDetails)
		{
			string url = CommonUtil.GetSessionStorageSourceURL(_provider, sessionDetails);

			return new JsonResult(CommonUtil.SetSessionStorageSourceURL(_provider, newSessionDetails, url));
		}
	}
}