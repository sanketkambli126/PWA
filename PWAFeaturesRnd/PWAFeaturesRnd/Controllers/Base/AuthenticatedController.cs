using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.ViewModels.Notification;

namespace PWAFeaturesRnd.Controllers.Base
{
    /// <summary>
    /// Authenticated Controller
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.BaseController" />
    [Authorize]
	public class AuthenticatedController : BaseController
	{
        #region Public Methods

        /// <summary>
        /// Gets the record level features (DIscussion, Notes) json string.
        /// </summary>
        /// <param name="notificationClient">The notification client.</param>
        /// <param name="messageCategory">The message category.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="vesselName">Name of the vessel.</param>
        /// <param name="contextParams">The context parameters.</param>
        /// <param name="messageParams">The message parameters.</param>
        /// <param name="referenceId">The reference identifier.</param>
        /// <returns></returns>
        public string GetRecordLevelFeaturesJsonString(NotificationClient notificationClient, MessageCategoryEnum messageCategory, string vesselId, string vesselName, string[] contextParams, string[] messageParams, string referenceId = null)
		{
			int categoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(messageCategory));

			notificationClient.AccessToken = GetAccessToken();
			Task<MessageCategoryDetails> taskMessageCategoryDetails = notificationClient.GetMessageCategoryDetail(categoryId, AppSettings.ApplicationId);
			MessageCategoryDetails messageCategoryDetails = taskMessageCategoryDetails.Result;

			NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
			{
				CategoryId = messageCategoryDetails.CategoryId,
				VesselId = vesselId,
				VesselName = vesselName,
				ReferenceIdentifier = referenceId,
				ApplicationId = AppSettings.ApplicationId,
				UserId = Request.Cookies["UserId"],
				NotificationJwtToken = GetAccessToken()
		};

			string contextPayload = messageCategoryDetails != null ? messageCategoryDetails.ContextPayloadTemplate : string.Empty;
			string defaultMessage = messageCategoryDetails != null ? messageCategoryDetails.MessageTemplate : string.Empty;

			if (!string.IsNullOrWhiteSpace(contextPayload))
			{
				for (int i = 0; i < contextParams.Length; i++)
				{
					contextPayload = contextPayload.Replace("{" + i + "}", contextParams[i]);
				}
			}

			if (!string.IsNullOrWhiteSpace(defaultMessage))
			{
				for (int i = 0; i < messageParams.Length; i++)
				{
					defaultMessage = defaultMessage.Replace("{" + i + "}", messageParams[i]);
				}
			}

			newMessageDetails.ContextPayload = contextPayload;
			newMessageDetails.DefaultMessage = defaultMessage;

			return JsonConvert.SerializeObject(newMessageDetails);
		}

		#endregion

	}
}