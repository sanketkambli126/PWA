using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Helper
{
	/// <summary>
	/// Notification Client
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
	public class NotificationClient : BaseHttpClient
	{
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

		/// <summary>
		/// Initializes a new instance of the <see cref="NotificationClient" /> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="provider">The provider.</param>
		public NotificationClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider)
			: base(client)
		{
			client.BaseAddress = new Uri(AppSettings.NotificationApiURL);
			_client = client;
			_configuration = configuration;
			_provider = provider;
		}

		/// <summary>
		/// Gets the channel list.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public async Task<PagedResult<PagedList<NotificationChannelViewModel>>> GetChannelList(DataTablePageRequest<string> pageRequest, ChannelRequest inputRequest, int timeZoneOffSet)
		{
			PagedResult<PagedList<NotificationChannelViewModel>> result = new PagedResult<PagedList<NotificationChannelViewModel>>();
			result.Results = new PagedList<NotificationChannelViewModel>();
			PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

			var request = new Dictionary<string, object>()
			{
				{ "pageRequest", pagedRequest },
				{ "request", inputRequest }
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/GetChannelList"));
			var response = await PostAsync<PagedResult<PagedList<NotificationChannel>>>(requestUrl, CreateHttpContent(request));

			if (response != null && response.Results != null && response.Results.Any())
			{
				foreach (NotificationChannel item in response.Results)
				{
					bool isGeneral = item.CatId.GetValueOrDefault().ToString() == EnumsHelper.GetKeyValue(MessageCategoryEnum.General);
					NotificationMobileDiscussionViewModel mobileDiscussion = new NotificationMobileDiscussionViewModel();
					mobileDiscussion.NewMessageDetails = new NewMessageParametersViewModel();
					NewMessageParametersViewModel messageParameters = new NewMessageParametersViewModel();
					messageParameters.ChannelId = item.Id;
					messageParameters.VesselName = item.VesName;
					messageParameters.VesselId = item.VesId;
					messageParameters.IsGeneralCat = isGeneral;
					messageParameters.CategoryId = item.CatId ?? 0;
					messageParameters.IsSaveAsDraft = item.IsSaveAsDraft;
					mobileDiscussion.NewMessageDetails = messageParameters;

					NotificationMobileChatDetailViewModel mobileMessagePage = new NotificationMobileChatDetailViewModel();
					mobileMessagePage.ChannelId = item.Id;
					mobileMessagePage.VesselIMONumber = item.VesIMONumber;
					mobileMessagePage.VesselName = item.VesName;
					mobileMessagePage.Title = item.Title;
					mobileMessagePage.IsOneToOneChat = item.IsOneToOneChat;
					mobileMessagePage.ParticipantCount = item.ParticipantCount;
					mobileMessagePage.VesselId = item.VesId;
					mobileMessagePage.ActiveParticipantsCount = item.ActiveParticipantsCount;

					NotificationMobileInfoViewModel infoPage = new NotificationMobileInfoViewModel();
					infoPage.ChannelId = item.Id;
					infoPage.VesIMONumber = item.VesIMONumber;
					infoPage.VesselName = item.VesName;
					infoPage.VesselId = item.VesId;
					infoPage.IsGeneralCat = isGeneral;
					infoPage.IsInfoPageLoaded = true;
					mobileMessagePage.NotificationMobileInfoURL = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationMobileInfoEncKey, infoPage);

					NotificationMobileInfoViewModel particiapntURL = new NotificationMobileInfoViewModel();
					particiapntURL.ChannelId = item.Id;
					particiapntURL.VesIMONumber = item.VesIMONumber;
					particiapntURL.VesselName = item.VesName;
					particiapntURL.VesselId = item.VesId;
					particiapntURL.IsGeneralCat = isGeneral;
					particiapntURL.IsInfoPageLoaded = false;
					mobileMessagePage.NotificationMobileParticipantURL = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationMobileInfoEncKey, particiapntURL);

					result.Results.Add(new NotificationChannelViewModel()
					{
						ChannelId = item.Id,
						ContextPayload = item.ContextPayload,
						CreatedBy = item.CreatedBy,
						Payload = item.Payload,
						Title = item.Title ?? "",
						RecievedDate = GetLocalConvertedTime(item.ModifiedOn, timeZoneOffSet),
						HasUnreadMessages = item.HasUnreadMessages,
						ClassName = !item.IsSaveAsDraft && item.HasUnreadMessages ? "unread" : "",
						VesselId = item.VesId ?? "",
						IsOneToOneChat = item.IsOneToOneChat,
						VesselIMONumber = item.VesIMONumber ?? "",
						VesselName = item.VesName ?? "",
						NotificationMobileChatDetailURL = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationMobileChatDetailEncKey, mobileMessagePage),
						MobileUnreadChannelStatus = !item.IsSaveAsDraft && item.HasUnreadMessages ? "mobile-link-active" : "",
						IsGeneralCat = isGeneral,
						IsSaveAsDraft = item.IsSaveAsDraft,
						CategoryId = item.CatId ?? 0,
						NotificationMobileDiscussionUrl = CommonUtil.GetEncryptedURL(_provider, Constants.NotificationMobileDiscussionEncKey, mobileDiscussion),
						LastMessageDetails = GetLastSentMessageDetailsForMobile(item.LastSenderName, item.LastSentMessageInChannel),
						LastSentMessageInChannel = string.IsNullOrWhiteSpace(item.LastSentMessageInChannel) ? "" : item.LastSentMessageInChannel.Trim(),
						LastSenderName = item.LastSenderName ?? "",
						IsAttachment = item.IsAttachment,
						TotalRecordCount = item.TotalRecordCount,
						UnreadMessageCount = item.UnreadMessageCount,
						ParticipantsInitials = GetParticipantsInitials(item.ActiveParticipantsCount, item.ParticipantsInitials)
					});
				}
			}

			result.Count = response.Count;
			result.CurrentPage = response.CurrentPage;
			result.PageSize = response.PageSize;
			result.TotalCount = response.TotalCount;
			result.TotalPages = response.TotalPages;
			return result;
		}

		/// <summary>
		/// Gets the last sent message details for mobile.
		/// </summary>
		/// <param name="lastSenderName">Last name of the sender.</param>
		/// <param name="lastSentMessageInChannel">The last sent message in channel.</param>
		/// <returns></returns>
		private string GetLastSentMessageDetailsForMobile(string lastSenderName, string lastSentMessageInChannel)
		{
			string senderName = string.IsNullOrWhiteSpace(lastSenderName) ? "" : lastSenderName.Trim();
			string message = string.IsNullOrWhiteSpace(lastSentMessageInChannel) ? "" : lastSentMessageInChannel.Trim();
			string messageDetails = senderName + " : " + message;
			string trimmedMessage = messageDetails.Length > 80 ? messageDetails.Substring(0, 75) + " ..." : messageDetails;
			return trimmedMessage;
		}

		/// <summary>
		/// Gets the local converted time.
		/// </summary>
		/// <param name="modifiedDate">The modified date.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		private string GetLocalConvertedTime(DateTime? modifiedDate, int timeZoneOffSet)
		{
			string recievedDate = String.Empty;
			if (modifiedDate != null && modifiedDate.GetValueOrDefault() != DateTime.MinValue)
			{
				DateTime localVersion = CommonUtil.ConvertUTCDateToLocal(modifiedDate, timeZoneOffSet);
				DateTime CurrentUTCTime = CommonUtil.ConvertUTCDateToLocal(DateTime.UtcNow, timeZoneOffSet);
				if (localVersion.Date == CurrentUTCTime.Date)
				{
					recievedDate = localVersion.ToString("HH:mm");
				}
				else
				{
					var test = localVersion.ToShortDateString();
					recievedDate = localVersion.ToString("dd MMM"); // workaround since the date was not being formatted with a "/"
				}
			}
			return recievedDate;
		}

		/// <summary>
		/// Gets the channel messages.
		/// </summary>
		/// <param name="pageRequest">The page request.</param>
		/// <param name="channelId">The channel identifier.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public async Task<PagedResult<PagedList<NotificationUserMessageViewModel>>> GetChannelMessages(DataTablePageRequest<string> pageRequest, string channelId, int timeZoneOffSet)
		{
			PagedResult<PagedList<NotificationUserMessageViewModel>> result = new PagedResult<PagedList<NotificationUserMessageViewModel>>();
			result.Results = new PagedList<NotificationUserMessageViewModel>();
			PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

			var request = new Dictionary<string, object>()
			{
				{ "channelId", Convert.ToInt32(channelId) },
				{ "pageRequest", pagedRequest }
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetChannelMessages"));
			var response = await PostAsync<PagedResult<PagedList<NotificationUserMessage>>>(requestUrl, CreateHttpContent(request));

			if (response != null && response.Results != null && response.Results.Any())
			{
				foreach (NotificationUserMessage item in response.Results)
				{
					NotificationUserMessageViewModel channelMessage = new NotificationUserMessageViewModel();
					channelMessage.ChannelId = item.ChannelId;
					channelMessage.CreatedOnUtc = item.CreatedOnUtc;
					channelMessage.MessageDescription = item.MessageDescription;
					channelMessage.MessageId = item.Id;
					channelMessage.SSUserId = item.SSUserId;
					channelMessage.Status = item.Status;
					channelMessage.Username = item.Username;
					channelMessage.CreatedOnUTCDateFormat = CommonUtil.GetLocalConvertedSpecificTimeFormat(item.CreatedOnUtc, timeZoneOffSet, Constants.NotificationChatDateTimeFormat);
					channelMessage.UserShortName = CommonUtil.GetUserShortName(item.Username);
					channelMessage.IsSeen = item.IsSeen;
					channelMessage.IsSent = item.IsSent;
					channelMessage.IsUnreadMessage = item.IsUnreadMessage;
					channelMessage.IsAttachment = Convert.ToBoolean(item.IsAttachment);
					channelMessage.Attachments = GetAttachments(item);
					channelMessage.IsMessageEdited = item.ModifiedOnUtc != null ? item.ModifiedOnUtc > item.CreatedOnUtc : false;
					channelMessage.ReadParticipantCount = item.ReadParticipantCount;
					result.Results.Add(channelMessage);
				}
				result.Count = response.Count;
				result.CurrentPage = response.CurrentPage;
				result.PageSize = response.PageSize;
				result.TotalCount = response.TotalCount;
				result.TotalPages = response.TotalPages;
			}

			return result;
		}

		/// <summary>
		/// Gets the attachments.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		private List<AttachmentViewModel> GetAttachments(NotificationUserMessage item)
		{
			List<AttachmentViewModel> result = new List<AttachmentViewModel>();
			if (Convert.ToBoolean(item.IsAttachment))
			{
				List<UploadAttachmentResponse> attachments = JsonConvert.DeserializeObject<List<UploadAttachmentResponse>>(item.AttachmentDetails);
				if (attachments.Count > 0)
				{
					attachments.Sort((x, y) => x.Sequence.CompareTo(y.Sequence));
					foreach (var attachment in attachments)
					{
						result.Add(new AttachmentViewModel
						{
							Description = attachment.Description,
							EttId = attachment.EttId,
							FileExtension = attachment.FileExtension,
							Sequence = attachment.Sequence
						});
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Gets the new unread messages.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public async Task<List<NotificationUserMessageViewModel>> GetNewUnreadMessages(string channelId, int timeZoneOffSet)
		{
			List<NotificationUserMessageViewModel> result = new List<NotificationUserMessageViewModel>();
			var request = new Dictionary<string, object>()
			{
				{ "channelId", Convert.ToInt32(channelId) }
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetNewUnreadMessages"));
			var response = await PostAsync<List<NotificationUserMessage>>(requestUrl, CreateHttpContent(request));
			if (response != null && response.Any())
			{
				foreach (NotificationUserMessage item in response)
				{
					NotificationUserMessageViewModel channelMessage = new NotificationUserMessageViewModel();
					channelMessage.ChannelId = item.ChannelId;
					channelMessage.CreatedOnUtc = item.CreatedOnUtc;
					channelMessage.MessageDescription = item.MessageDescription;
					channelMessage.MessageId = item.Id;
					channelMessage.SSUserId = item.SSUserId;
					channelMessage.Username = item.Username;
					channelMessage.CreatedOnUTCDateFormat = CommonUtil.GetLocalConvertedSpecificTimeFormat(item.CreatedOnUtc, timeZoneOffSet, Constants.NotificationChatDateTimeFormat);
					channelMessage.UserShortName = CommonUtil.GetUserShortName(item.Username);
					result.Add(channelMessage);
				}
			}

			return result.OrderBy(x => x.CreatedOnUtc).ToList();
		}

		/// <summary>
		/// Gets the new channel messages.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public async Task<List<NotificationUserMessageViewModel>> GetNewChannelMessages(string channelId, int timeZoneOffSet)
		{
			List<NotificationUserMessageViewModel> result = new List<NotificationUserMessageViewModel>();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetNewChannelMessages"), "channelId=" + channelId);
			List<NotificationUserMessage> response = await GetAsync<List<NotificationUserMessage>>(requestUrl);

			if (response != null && response.Any())
			{
				foreach (NotificationUserMessage item in response)
				{
					result.Add(new NotificationUserMessageViewModel()
					{
						ChannelId = item.ChannelId,
						CreatedOnUtc = item.CreatedOnUtc,
						MessageDescription = item.MessageDescription,
						MessageId = item.Id,
						SSUserId = item.SSUserId,
						Username = item.Username,
						CreatedOnUTCDateFormat = CommonUtil.GetLocalConvertedSpecificTimeFormat(item.CreatedOnUtc, timeZoneOffSet, Constants.NotificationChatDateTimeFormat),
						UserShortName = CommonUtil.GetUserShortName(item.Username),
						IsSent = item.IsSent,
						IsSeen = item.IsSeen,
						IsUnreadMessage = item.IsUnreadMessage,
						IsAttachment = Convert.ToBoolean(item.IsAttachment),
						Attachments = GetAttachments(item),
						Status = item.Status,
					});
				}
			}
			return result.OrderBy(x => x.CreatedOnUtc).ToList();
		}

		/// <summary>
		/// Sends the notification.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public async Task<int> SendNotification(NotificationSendRequest data)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/SendNotification"));
			int response = await PostAsync<int>(requestUrl, CreateHttpContent(data));
			return response;
		}

		/// <summary>
		/// Gets the channel participants.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <returns></returns>
		public async Task<List<NotificationChannelSubscriptionViewModel>> GetChannelParticipants(string channelId)
		{
			List<NotificationChannelSubscriptionViewModel> result = new List<NotificationChannelSubscriptionViewModel>();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetChannelParticipants"), "channelId=" + channelId);
			List<NotificationChannelSubscription> response = await GetAsync<List<NotificationChannelSubscription>>(requestUrl);

			if (response != null && response.Any())
			{
				foreach (NotificationChannelSubscription item in response)
				{
					result.Add(new NotificationChannelSubscriptionViewModel()
					{
						ChannelId = item.ChannelId,
						LastReadTimeUtc = item.LastReadTimeUtc,
						UserId = item.UserId,
						SSUserId = item.SSUserId,
						Username = item.Username,
						UserShortName = CommonUtil.GetUserShortName(item.Username)
					});
				}
			}

			return result;
		}

		/// <summary>
		/// Creates the channel.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="sender">The sender.</param>
		/// <returns></returns>
		public async Task<int> CreateChannel(CreateChannelRequestDtoViewModel input, NotificationChannelSubscription sender, int applicationId)
		{
			CreateChannelRequestDto request = new CreateChannelRequestDto();
			request.CategoryId = input.CategoryId;
			request.InitialMsg = input.InitialMsg;
			request.SourceAppId = applicationId;
			request.Title = input.Title;
			request.VesselId = input.VesselId;
			request.IsAttachment = input.IsAttachment;
			request.AttachmentList = input.AttachmentList;
			request.ContextPaylod = input.ContextPaylod;
			request.Subscribers = new List<NotificationChannelSubscription>();
			request.IsSavedAsDraft = input.IsSaveAsDraft;
			request.ChannelId = input.ChannelId;
			request.ReferenceIdentifier = input.ReferenceIdentifier;

			if (input.Subscribers != null && input.Subscribers.Any())
			{
				foreach (var item in input.Subscribers)
				{
					NotificationChannelSubscription localSubscriber = new NotificationChannelSubscription();
					localSubscriber.SSUserId = item.Id;
					localSubscriber.Username = item.Text;
					request.Subscribers.Add(localSubscriber);
				}
			}
			request.Subscribers.Add(sender);

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/CreateChannel"));
			int response = await PostAsync<int>(requestUrl, CreateHttpContent(request));
			return response;
		}

		/// <summary>
		/// Gets the area list.
		/// </summary>
		/// <returns></returns>
		public List<Lookup> GetAreaList()
		{
			List<Lookup> areaList = new List<Lookup>();
			foreach (ModuleArea val in Enum.GetValues(typeof(ModuleArea)))
			{
				areaList.Add(new Lookup() { Identifier = EnumsHelper.GetKeyValue(val), Description = EnumsHelper.GetDescription(val) });
			}
			return areaList;
		}

		/// <summary>
		/// Gets the channel detail.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public async Task<NotificationChannelViewModel> GetChannelDetail(string channelId, int timeZoneOffSet)
		{
			NotificationChannelViewModel result = new NotificationChannelViewModel();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetChannelDetail"), "channelId=" + channelId);
			NotificationChannel response = await GetAsync<NotificationChannel>(requestUrl);

			if (response != null)
			{
				result.ChannelId = response.Id;
				result.ContextPayload = response.ContextPayload;
				result.CreatedBy = response.CreatedBy;
				result.Payload = response.Payload;
				result.Title = string.IsNullOrWhiteSpace(response.Title) ? string.Empty : response.Title;
				result.VesselId = response.VesId ?? string.Empty;
				result.RecievedDate = GetLocalConvertedTime(response.ModifiedOn, timeZoneOffSet);
				result.HasUnreadMessages = response.HasUnreadMessages;
				result.IsOneToOneChat = response.IsOneToOneChat;
				result.IsGeneralCat = response.CatId.GetValueOrDefault().ToString() == EnumsHelper.GetKeyValue(MessageCategoryEnum.General);
				result.CategoryId = response.CatId ?? 0;
				result.IsSaveAsDraft = response.IsSaveAsDraft;
				result.ActiveParticipantsCount = response.ActiveParticipantsCount;
			}

			return result;
		}

		/// <summary>
		/// Gets the unread channel count.
		/// </summary>
		/// <returns></returns>
		public async Task<int> GetUnreadChannelCount()
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetUnreadChannelCount"));
			int response = await GetAsync<int>(requestUrl);
			return response;
		}

		/// <summary>
		/// Gets the read message participants.
		/// </summary>
		/// <param name="messageId">The message identifier.</param>
		/// <returns></returns>
		public async Task<List<NotificationChannelSubscriptionViewModel>> GetReadMessageParticipants(int messageId)
		{
			List<NotificationChannelSubscriptionViewModel> result = new List<NotificationChannelSubscriptionViewModel>();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetReadMessageParticipants"), "messageId=" + messageId);

			List<NotificationChannelSubscription> response = await GetAsync<List<NotificationChannelSubscription>>(requestUrl);
			if (response != null && response.Any())
			{
				foreach (var item in response)
				{
					result.Add(new NotificationChannelSubscriptionViewModel
					{
						Username = item.Username,
					});
				}
			}
			return result;
		}

		/// <summary>
		/// Gets the last read delivered message for channel.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <returns></returns>
		public async Task<List<LastReadMessageResponseViewModel>> GetLastReadDeliveredMessageForChannel(int channelId)
		{
			List<LastReadMessageResponseViewModel> result = new List<LastReadMessageResponseViewModel>();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetLastReadDeliveredMessageForChannel"), "channelId=" + channelId);

			List<LastReadMessageResponse> response = await GetAsync<List<LastReadMessageResponse>>(requestUrl);
			if (response != null && response.Any())
			{
				foreach (var item in response)
				{
					result.Add(new LastReadMessageResponseViewModel
					{
						IsDelivered = item.IsDelivered,
						IsSeen = item.IsSeen,
						MessageId = item.MessageId
					});
				}
			}
			return result;
		}

		/// <summary>
		/// Gets my last all read MSG for channel.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <returns></returns>
		public async Task<ChannelMessageViewModel> GetMyLastAllReadMsgForChannel(int channelId)
		{
			ChannelMessageViewModel result = new ChannelMessageViewModel();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetMyLastAllReadMsgForChannel"), "channelId=" + channelId);

			ChannelMessage response = await GetAsync<ChannelMessage>(requestUrl);
			if (response != null)
			{
				result = new ChannelMessageViewModel
				{
					ChannelId = response.ChannelId,
					CreatedOnUtc = response.CreatedOnUtc,
					Id = response.Id,
					MessageDescription = response.MessageDescription,
					ParentMessageId = response.ParentMessageId,
					SourceAppId = response.SourceAppId,
					SSUserId = response.SSUserId,
					UserId = response.UserId,
					Username = response.Username,
					VesId = response.VesId
				};
			}
			return result;
		}

		/// <summary>
		/// Adds the new participants to channel.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<bool> AddNewParticipantsToChannel(CreateChannelRequestDtoViewModel input)
		{
			CreateChannelRequestDto request = new CreateChannelRequestDto();
			request.ChannelId = input.ChannelId;
			request.SourceAppId = AppSettings.ApplicationId;
			request.Subscribers = new List<NotificationChannelSubscription>();

			if (input.Subscribers != null && input.Subscribers.Any())
			{
				foreach (var item in input.Subscribers)
				{
					NotificationChannelSubscription localSubscriber = new NotificationChannelSubscription();
					localSubscriber.SSUserId = item.Id;
					localSubscriber.Username = item.Text;
					request.Subscribers.Add(localSubscriber);
				}
			}

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/AddParticipantsToChannel"));
			bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(request));
			return response;
		}

		/// <summary>
		/// Gets the channel messages test.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		public async Task<bool> GetChannelMessagesTest(string message)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetChannelMessagesTest"), "message=" + message);
			bool result = await GetAsync<bool>(requestUrl);
			return result;
		}

		/// <summary>
		/// Gets the message categories.
		/// </summary>
		/// <param name="appId">The application identifier.</param>
		/// <returns></returns>
		public async Task<List<MessageCategoryDtoViewModel>> GetMessageCategories(int appId)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetMessageCategories"), "appId=" + appId);
			List<MessageCategoryDtoViewModel> result = new List<MessageCategoryDtoViewModel>();
			List<MessageCategoryDto> response = await GetAsync<List<MessageCategoryDto>>(requestUrl);

			if (response != null && response.Any())
			{
				foreach (MessageCategoryDto item in response)
				{
					result.Add(new MessageCategoryDtoViewModel()
					{
						CategoryName = item.CategoryName,
						CatId = item.CatId,
					});
				}
			}
			return result;
		}

		/// <summary>
		/// Gets the message category detail.
		/// </summary>
		/// <param name="catId">The cat identifier.</param>
		/// <param name="appId">The application identifier.</param>
		/// <returns></returns>
		public async Task<MessageCategoryDetails> GetMessageCategoryDetail(int catId, int appId)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetMessageCategoryDetail"), "catId=" + catId + "&appId=" + appId);
			MessageCategoryDetails response = await GetAsync<MessageCategoryDetails>(requestUrl);

			return response;
		}

		/// <summary>
		/// Gets the record level details.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <returns></returns>
		public async Task<RecordLevelDetailsViewModel> GetRecordLevelDetails(int channelId, int applicationId)
		{
			RecordLevelDetailsRequest request = new RecordLevelDetailsRequest
			{
				ChannelId = channelId,
				ApplicationId = applicationId
			};

			RecordLevelDetailsViewModel result = new RecordLevelDetailsViewModel();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetRecordLevelDetails"));
			RecordLevelDetailsResponse response = await PostAsync<RecordLevelDetailsResponse>(requestUrl, CreateHttpContent(request));
			result.IsSuccess = response.IsSuccess;
			result.ErrorMessage = response.ErrorMessage;
			result.Details = new List<KeyValuePair<string, string>>();
			if (response.Details != null)
			{
				foreach (KeyValuePair<string, object> pair in response.Details)
				{
					//need to consider white spaces/empty string
					if (pair.Value != null && !string.IsNullOrWhiteSpace(pair.Value.ToString()))
					{
						if (pair.Value.GetType() == typeof(DateTime))
						{
							result.Details.Add(new KeyValuePair<string, string>(pair.Key, ((DateTime)pair.Value).ToString(Constants.DateFormat)));
						}
						else
						{
							result.Details.Add(new KeyValuePair<string, string>(pair.Key, pair.Value.ToString()));
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Gets the channel additional details.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <returns></returns>
		public async Task<ChannelAdditionalDetailsResponse> GetChannelAdditionalDetails(int channelId, int applicationId)
		{
			ChannelAdditionalDetailsRequest request = new ChannelAdditionalDetailsRequest
			{
				ChannelId = channelId,
				ApplicationId = applicationId
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetChannelAdditionalDetails"));
			ChannelAdditionalDetailsResponse response = await PostAsync<ChannelAdditionalDetailsResponse>(requestUrl, CreateHttpContent(request));


			return response;
		}

		/// <summary>
		/// Gets the user details.
		/// </summary>
		/// <param name="ssUserId">The ss user identifier.</param>
		/// <returns></returns>
		public async Task<NotificationUserDetails> GetUserDetails(string ssUserId)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetUserDetails"), "ssUserId=" + ssUserId);
			NotificationUser response = await GetAsync<NotificationUser>(requestUrl);

			NotificationUserDetails result = null;

			if (response != null)
			{
				result = new NotificationUserDetails
				{
					Username = response.Username,
					UserShortName = CommonUtil.GetUserShortName(response.Username)
				};
			}
			return result;
		}

		/// <summary>
		/// Adds the messaging user if not exists.
		/// </summary>
		/// <param name="userToBeAdded">The user to be added.</param>
		/// <returns></returns>
		public async Task<bool> AddMessagingUserIfNotExists(NotificationChannelSubscription userToBeAdded)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/AddUserNotExist"));
			bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(userToBeAdded));
			return response;
		}

		/// <summary>
		/// Deletes the message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		public async Task<bool> DeleteMessage(ChannelMessage message)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/DeleteMessage"));
			bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(message));
			return response;
		}

		/// <summary>
		/// Deletes the channel by identifier.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <param name="applicationId">The application identifier.</param>
		/// <returns></returns>
		public async Task<NotificationChannel> DeleteChannelById(int channelId, int applicationId)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/DeleteChannelById"));
			var request = new Dictionary<string, object>()
			{
				{ "channelId", channelId },
				{ "sourceAppId", applicationId }
			};

			NotificationChannel response = await PostAsync<NotificationChannel>(requestUrl, CreateHttpContent(request));
			return response;
		}

		/// <summary>
		/// Edits the message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		public async Task<bool> EditMessage(ChannelMessage message)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/EditMessage"));
			bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(message));
			return response;
		}

		#region My Notes

		/// <summary>
		/// Gets the notes list.
		/// </summary>
		/// <param name="pageRequest">The page request.</param>
		/// <param name="requestViewModel">The request view model.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public async Task<PagedResult<PagedList<NotesListViewModel>>> GetNotesList(DataTablePageRequest<string> pageRequest, NotesRequestViewModel requestViewModel, int timeZoneOffSet)
		{
			PagedResult<PagedList<NotesListViewModel>> result = new PagedResult<PagedList<NotesListViewModel>>();
			result.Results = new PagedList<NotesListViewModel>();
			PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

			NotesRequest inputRequest = new NotesRequest();
			inputRequest.SearchText = requestViewModel.SearchText;
			inputRequest.StatusIds = requestViewModel.StatusIds;
			if (!string.IsNullOrWhiteSpace(requestViewModel.MessageDetailsJSON))
			{
				NewMessageParametersViewModel messageDetails = JsonConvert.DeserializeObject<NewMessageParametersViewModel>(requestViewModel.MessageDetailsJSON);
				inputRequest.CategoryId = messageDetails.CategoryId;
				inputRequest.ContextParams = messageDetails.ContextPayload;
				inputRequest.ReferenceIdentifier = messageDetails.ReferenceIdentifier;
			}


			var request = new Dictionary<string, object>()
			{
				{ "pageRequest", pagedRequest },
				{ "request", inputRequest }
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/GetNotes"));
			var response = await PostAsync<PagedResult<PagedList<NotesResponse>>>(requestUrl, CreateHttpContent(request));

			if (response != null && response.Results != null && response.Results.Any())
			{
				foreach (NotesResponse item in response.Results)
				{
					NotesListViewModel note = new NotesListViewModel();
					note.NoteId = CommonUtil.GetEncryptedURL(_provider, Constants.NoteIdEncryptionText, item.NoteId);
					note.NoteTitle = item.NoteTitle;
					note.NoteDescription = string.IsNullOrEmpty(item.NoteDescription) ? "" : item.NoteDescription;
					note.CategoryName = item.CategoryName;
					note.CatId = item.CatId;
					note.ExpectedExecutionDateTime = CommonUtil.GetLocalConvertedSpecificTimeFormat(item.ExecutionDateTime, timeZoneOffSet, Constants.DateTime24HrFormat); ;
					note.ShortDate = CommonUtil.GetLocalConvertedSpecificTimeFormat(item.NoteDateUTC, timeZoneOffSet, Constants.ShortDateTimeFormat);
					note.NoteDateUTC = CommonUtil.GetLocalConvertedSpecificTimeFormat(item.NoteDateUTC, timeZoneOffSet, Constants.DateFormat);
					note.Status = item.Status;
					note.VesselId = CommonUtil.GetEncryptedURL(_provider, Constants.VesselIdEncryptionText, item.VesselId);
					note.VesselName = item.VesselName;
					note.IsReminderExpired = item.IsReminderExpired.GetValueOrDefault();
					note.IsAttachment = item.IsAttachment.GetValueOrDefault();
					if (item.IsAttachment.GetValueOrDefault())
					{
						List<AttachmentViewModel> attachmentViewModels = new List<AttachmentViewModel>();
						List<UploadAttachmentResponse> attachments = JsonConvert.DeserializeObject<List<UploadAttachmentResponse>>(item.AttachmentDetails);
						if (attachments.Count > 0)
						{
							attachments.Sort((x, y) => x.Sequence.CompareTo(y.Sequence));
							foreach (var attachment in attachments)
							{
								attachmentViewModels.Add(new AttachmentViewModel
								{
									Description = attachment.Description,
									EttId = attachment.EttId,
									FileExtension = attachment.FileExtension,
									Sequence = attachment.Sequence
								});
							}
						}
						note.AttachmentDetails = attachmentViewModels;
					}
					result.Results.Add(note);
				}

				result.Count = response.Count;
				result.CurrentPage = response.CurrentPage;
				result.PageSize = response.PageSize;
				result.TotalCount = response.TotalCount;
				result.TotalPages = response.TotalPages;
			}

			return result;
		}

		/// <summary>
		/// Saves the note details.
		/// </summary>
		/// <param name="inputRequest">The input request.</param>
		/// <returns></returns>
		public async Task<long> SaveNoteDetails(CreateNoteRequestViewModel inputRequest)
		{
			CreateNoteRequest request = new CreateNoteRequest();
			long noteId = String.IsNullOrWhiteSpace(inputRequest.NoteId) ? 0 : long.Parse(CommonUtil.GetDecryptedRequest<string>(_provider, Constants.NoteIdEncryptionText, inputRequest.NoteId));
			if (inputRequest != null)
			{
				request.CatId = inputRequest.CatId;
				request.NoteDescription = inputRequest.NoteDescription;
				request.NoteId = noteId;
				request.NoteTitle = inputRequest.NoteTitle;
				request.Status = inputRequest.Status;
				request.VesselId = inputRequest.VesselId;
				request.ContextParams = inputRequest.ContextParams; //only used in case of add
				request.ReferenceIdentifier = inputRequest.ReferenceIdentifier;
				request.NotesReminders = new List<NotesReminder>();
				if (inputRequest.NotesReminders != null && inputRequest.NotesReminders.Any())
				{
					foreach (NotesReminderViewModel item in inputRequest.NotesReminders)
					{
						NotesReminder reminder = new NotesReminder();
						reminder.IsAlertDismissed = item.IsAlertDismissed;
						reminder.IsCronexpScheduled = true;
						reminder.IsReminderExpired = item.IsReminderExpired;
						reminder.IsSingleTimeEvent = true;
						reminder.ReminderId = item.ReminderId;
						reminder.ExecutionDateTime = item.ExecutionDateTime;
						reminder.NoteId = noteId;
						if (item.ExpectedExecutionTime.HasValue)
						{
							reminder.ExpectedExecutionTime = item.ExpectedExecutionTime.Value;
						}
						else
						{
							reminder.ExpectedExecutionTime = null;
						}
						request.NotesReminders.Add(reminder);
					}
				}
				if (inputRequest.AttachmentList != null && inputRequest.AttachmentList.Count > 0)
				{
					request.IsAttachment = true;
					request.AttachmentList = inputRequest.AttachmentList;
				}
			}
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/AddEditNotes"));
			long response = await PostAsync<long>(requestUrl, CreateHttpContent(request));
			return response;
		}

		/// <summary>
		/// Gets the note details by identifier.
		/// </summary>
		/// <param name="noteId">The note identifier.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public async Task<NoteDetailsResponseViewModel> GetNoteDetailsById(long noteId, int timeZoneOffSet)
		{
			NoteDetailsResponseViewModel noteDetails = null;

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/NoteDetailsById"), "noteId=" + noteId);
			NoteDetailsResponse response = await GetAsync<NoteDetailsResponse>(requestUrl);
			if (response != null)
			{
				noteDetails = new NoteDetailsResponseViewModel();
				noteDetails.CatId = response.CatId;
				noteDetails.CategoryName = response.CategoryName;
				noteDetails.NoteId = response.NoteId;
				noteDetails.NoteTitle = response.NoteTitle;
				noteDetails.NoteDescription = response.NoteDescription;
				noteDetails.VesselId = response.VesselId;
				noteDetails.VesselName = response.VesselName;
				noteDetails.NoteDateUTC = response.NoteDateUTC;
				noteDetails.Status = response.Status;
				noteDetails.IsAttachment = response.IsAttachment;
				noteDetails.ReferenceIdentifier = response.ReferenceIdentifier;
				noteDetails.ContextParams = response.ContextParams;

				if (response.NotesReminders != null && response.NotesReminders.Any())
				{
					noteDetails.NotesReminders = new List<NotesReminderViewModel>();

					foreach (NotesReminder item in response.NotesReminders)
					{
						NotesReminderViewModel reminder = new NotesReminderViewModel();
						if (item.ExpectedExecutionTime != null && item.ExpectedExecutionTime.HasValue)
						{
							reminder.ExpectedExecutionTime = CommonUtil.ConvertUTCDateToLocal(item.ExpectedExecutionTime, timeZoneOffSet);
						}
						else
						{
							reminder.ExpectedExecutionTime = null;
						}
						reminder.IsReminderExpired = item.IsReminderExpired;
						reminder.IsSingleTimeEvent = item.IsSingleTimeEvent;
						reminder.NoteId = item.NoteId;
						reminder.ReminderId = item.ReminderId;
						reminder.IsAlertDismissed = item.IsAlertDismissed;
						noteDetails.NotesReminders.Add(reminder);
					}
				}

				if (response.IsAttachment)
				{
					List<AttachmentViewModel> attachmentViewModels = new List<AttachmentViewModel>();
					List<UploadAttachmentResponse> attachments = JsonConvert.DeserializeObject<List<UploadAttachmentResponse>>(response.AttachmentDetails);
					if (attachments.Count > 0)
					{
						attachments.Sort((x, y) => x.Sequence.CompareTo(y.Sequence));
						foreach (var attachment in attachments)
						{
							attachmentViewModels.Add(new AttachmentViewModel
							{
								Description = attachment.Description,
								EttId = attachment.EttId,
								FileExtension = attachment.FileExtension,
								Sequence = attachment.Sequence
							});
						}
					}
					noteDetails.AttachmentDetails = attachmentViewModels;
				}
			}
			return noteDetails;
		}

		/// <summary>
		/// Deletes the note by identifier.
		/// </summary>
		/// <param name="noteId">The note identifier.</param>
		/// <returns></returns>
		public async Task<bool> DeleteNoteById(long noteId)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/DeleteNoteById"), "noteId=" + noteId);
			bool response = await GetAsync<bool>(requestUrl);
			return response;
		}

		/// <summary>
		/// Gets the note additional details.
		/// </summary>
		/// <param name="noteId">The note identifier.</param>
		/// <returns></returns>
		public async Task<NoteAdditionalDetailsResponse> GetNoteAdditionalDetails(int noteId, int applicationId)
		{
			NoteAdditionalDetailsRequest request = new NoteAdditionalDetailsRequest
			{
				NoteId = noteId,
				ApplicationId = applicationId
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetNoteAdditionalDetails"));
			NoteAdditionalDetailsResponse response = await PostAsync<NoteAdditionalDetailsResponse>(requestUrl, CreateHttpContent(request));


			return response;
		}


		/// <summary>
		/// Updates the note status.
		/// </summary>
		/// <param name="noteId">The note identifier.</param>
		/// <param name="status">The status.</param>
		/// <returns></returns>
		public async Task<bool> UpdateNoteStatus(long noteId, int status)
		{
			UpdateNoteStatusRequest request = new UpdateNoteStatusRequest
			{
				NoteId = noteId,
				Status = status
			};
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/UpdateNoteStatus"));
			bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(request));
			return response;
		}

		/// <summary>
		/// Marks the job as done.
		/// </summary>
		/// <param name="reminderVM">The reminder vm.</param>
		/// <returns></returns>
		public async Task<bool> MarkJobAsDone(NotesReminderViewModel reminderVM)
		{
			NotesReminder reminder = new NotesReminder();
			reminder.ReminderId = reminderVM.ReminderId;
			reminder.IsReminderExpired = reminderVM.IsReminderExpired;
			reminder.IsCronexpScheduled = reminderVM.IsCronexpScheduled;

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/UpdateStatusOfJob"));
			bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(reminder));
			return response;
		}

		/// <summary>
		/// Updates the reminder status.
		/// </summary>
		/// <param name="reminderVM">The reminder vm.</param>
		/// <returns></returns>
		public async Task<bool> UpdateReminderStatus(NotesReminderViewModel reminderVM)
		{
			NotesReminder reminder = new NotesReminder();
			reminder.ReminderId = reminderVM.ReminderId;
			reminder.ReminderFeatureStatus = reminderVM.ReminderFeatureStatus;
			reminder.NoteId = reminderVM.NoteId;
			reminder.ExpectedExecutionTime = reminderVM.ExpectedExecutionTime;
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/UpdateReminderStatus"));
			bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(reminder));
			return response;
		}

		/// <summary>
		/// Gets the record level discussion and notes count.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<RecordDiscussionResponseViewModel> GetRecordLevelDiscussionAndNotesCount(RecordLevelDiscussionCountViewModel request)
		{
			string queryParameter = "catId=" + request.CategoryId + "&contextPayload=" + request.ContextPayload;
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/GetRecordDiscussionCount"), queryParameter);
			RecordDiscussionResponse response = await GetAsync<RecordDiscussionResponse>(requestUrl);
			return new RecordDiscussionResponseViewModel { ChannelCount = response.ChannelCount, NotesCount = response.NotesCount };
		}

		/// <summary>
		/// Gets the reminder alert.
		/// </summary>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public async Task<List<ReminderAlertResponseViewModel>> GetReminderAlert(int timeZoneOffSet)
		{
			List<ReminderAlertResponseViewModel> reminderAlertList = new List<ReminderAlertResponseViewModel>();
			int dateDiff = Constants.ReminderAlertDayDifference;
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/ReminderAlerts"), "dateDiff=" + dateDiff);
			List<ReminderAlertResponse> response = await GetAsync<List<ReminderAlertResponse>>(requestUrl);
			if (response != null && response.Any())
			{
				foreach (var reminderAlert in response)
				{
					ReminderAlertResponseViewModel reminderAlertVM = new ReminderAlertResponseViewModel();
					reminderAlertVM.ExpectedExecutionTime = CommonUtil.GetLocalConvertedSpecificTimeFormat(reminderAlert.ExpectedExecutionTime, timeZoneOffSet, Constants.DateTime24HrFormat);
					reminderAlertVM.NoteDescription = reminderAlert.NoteDescription;
					reminderAlertVM.IsReminderExpired = reminderAlert.IsReminderExpired;
					reminderAlertVM.NoteId = CommonUtil.GetEncryptedURL(_provider, Constants.NoteIdEncryptionText, reminderAlert.NoteId);
					reminderAlertVM.EncryptedReminderId = CommonUtil.GetEncryptedURL(_provider, Constants.ReminderIdEncryptionText, reminderAlert.ReminderId);
					reminderAlertVM.NoteTitle = reminderAlert.NoteTitle;
					reminderAlertVM.NoteCreatedDate = CommonUtil.GetLocalConvertedSpecificTimeFormat(reminderAlert.NoteCreatedDate, timeZoneOffSet, Constants.ShortDateTimeFormat);
					reminderAlertList.Add(reminderAlertVM);
				}
			}
			return reminderAlertList;
		}

		/// <summary>
		/// Reminders the alert dismissed.
		/// </summary>
		/// <param name="reminderId">The reminder identifier.</param>
		/// <returns></returns>
		public async Task<bool> ReminderAlertDismissed(long reminderId)
		{
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/ReminderAlertDismissed"), "reminderId=" + reminderId);
			bool response = await GetAsync<bool>(requestUrl);
			return response;
		}

		#endregion

		/// <summary>
		/// Gets the channel detail draft.
		/// </summary>
		/// <param name="channelId">The channel identifier.</param>
		/// <returns></returns>
		public async Task<DraftMessageParametersViewModel> GetChannelDetailDraft(string channelId)
		{
			DraftMessageParametersViewModel result = new DraftMessageParametersViewModel();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetChannelDetailDraft"), "channelId=" + channelId);
			NotificationChannel response = await GetAsync<NotificationChannel>(requestUrl);

			if (response != null)
			{
				NotificationUserMessage notificationUserMessage = response.NotificationUserMessages.FirstOrDefault();

				result.ChannelId = response.Id;
				result.Title = string.IsNullOrWhiteSpace(response.Title) ? string.Empty : response.Title;
				result.VesselId = response.VesId ?? string.Empty;
				result.IsGeneralCat = response.CatId.GetValueOrDefault().ToString() == EnumsHelper.GetKeyValue(MessageCategoryEnum.General);
				result.CategoryId = response.CatId ?? 0;
				result.IsSaveAsDraft = response.IsSaveAsDraft;
				result.Participants = new List<DraftChannelSubscriptionViewModel>();

				if (notificationUserMessage != null)
				{
					result.Attachments = GetAttachments(notificationUserMessage);
					result.MessageDescription = notificationUserMessage.MessageDescription;
					result.IsAttachment = Convert.ToBoolean(notificationUserMessage.IsAttachment);
				}

				if (response.NotificationChannelSubscriptions != null && response.NotificationChannelSubscriptions.Any())
				{
					foreach (NotificationChannelSubscription item in response.NotificationChannelSubscriptions)
					{
						result.Participants.Add(new DraftChannelSubscriptionViewModel()
						{
							UserId = item.UserId,
							SSUserId = item.SSUserId,
							Username = item.Username,
							UserShortName = CommonUtil.GetUserShortName(item.Username)
						});
					}
				}
			}
			return result;
		}


		/// <summary>
		/// Gets the unread messages.
		/// </summary>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <param name="pageRequest">The page request.</param>
		/// <returns></returns>
		public async Task<List<UnreadChannelMessageResponseViewModel>> GetUnreadMessages(int timeZoneOffSet, DataTablePageRequest<string> pageRequest)
		{
			List<UnreadChannelMessageResponseViewModel> unreadMessages = new List<UnreadChannelMessageResponseViewModel>();
			ChannelRequest inputRequest = new ChannelRequest();
			PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

			var request = new Dictionary<string, object>()
			{
				{ "pageRequest", pagedRequest },
				{ "request", inputRequest }
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(CultureInfo.InvariantCulture, "api/Messaging/GetChannelList"));
			var response = await PostAsync<PagedResult<PagedList<NotificationChannel>>>(requestUrl, CreateHttpContent(request));

			if (response != null && response.Results != null && response.Results.Any())
			{
				foreach (var item in response.Results)
				{
					unreadMessages.Add(new UnreadChannelMessageResponseViewModel
					{
						ChannelName = item.Title,
						CreatedLocalDate = GetLocalConvertedTime(item.ModifiedOn, timeZoneOffSet),
						ChannelId = item.Id,
						IsUnread = item.HasUnreadMessages,
						VesselName = item.VesName,
						UnreadMessageCount = item.UnreadMessageCount,
						LastSender = item.LastSenderName ?? "",
						LastMessageDescription = item.LastSentMessageInChannel,
						MessageHasAttachment = item.IsAttachment,
						IsDraft = item.IsSaveAsDraft,
						ParticipantsInitials = GetParticipantsInitials(item.ActiveParticipantsCount, item.ParticipantsInitials),
						ParticipantsNames = item.ParticipantsInitials
					});
				}
			}
			return unreadMessages;
		}

		/// <summary>
		/// Gets the participants initials.
		/// </summary>
		/// <param name="activeParticipantCount">The active participant count.</param>
		/// <param name="participantsNames">The participants names.</param>
		/// <returns></returns>
		private string GetParticipantsInitials(int activeParticipantCount, string participantsNames)
		{
			int visibleInitialsCount = 5;
			string participantsInitials = "";
			if (!String.IsNullOrWhiteSpace(participantsNames))
			{
				if (activeParticipantCount == 2)
				{
					participantsInitials = participantsNames;
				}
				else if (activeParticipantCount > 2)
				{
					int iterateCount = Math.Min(activeParticipantCount - 1, visibleInitialsCount);
					IEnumerable<string> particiapants = participantsNames.Split(',').Take(iterateCount);
					List<string> participantsShortname = particiapants.Select(x => CommonUtil.GetUserShortName(x)).ToList();
					participantsInitials = String.Join(", ", participantsShortname);
					if (activeParticipantCount - 1 > visibleInitialsCount)
					{
						participantsInitials += ", +" + (activeParticipantCount - 1 - visibleInitialsCount);
					}
				}

			}
			return participantsInitials;
		}

		/// <summary>
		/// Gets the record level discussion and notes count.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<List<RecordDiscussionResponse>> GetListLevelRecordDiscussionCountByReferenceId(RecordDiscussionRequestViewModel input)
		{
			RecordDiscussionRequest request = new RecordDiscussionRequest()
			{
				CategoryId = input.CategoryId,
				ReferenceIds = input.ReferenceIds
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetRecordDiscussionCountByReferenceId"));
			var response = await PostAsync<List<RecordDiscussionResponse>>(requestUrl, CreateHttpContent(request));

			return response;
		}

		/// <summary>
		/// Gets the channel message by identifier.
		/// </summary>
		/// <param name="MessageId">The message identifier.</param>
		/// <returns></returns>
		public async Task<EditMessageViewModel> GetChannelMessageById(int MessageId)
		{
			EditMessageViewModel result = new EditMessageViewModel();
			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Messaging/GetMessageById"), "MessagId=" + MessageId);
			NotificationUserMessage response = await GetAsync<NotificationUserMessage>(requestUrl);

			if (response != null)
			{
				result.MessageId = response.Id;
				result.ChannelId = response.ChannelId;
				result.Attachments = GetAttachments(response);
				result.MessageDescription = response.MessageDescription;
				result.IsAttachment = Convert.ToBoolean(response.IsAttachment);
			}
			return result;
		}

	}
}
