using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.ViewModels.Notification;

namespace PWAFeaturesRnd.Helper
{
	/// <summary>
	/// Notification Document Client
	/// </summary>
	/// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
	public class NotificationDocumentClient : BaseHttpClient
	{
		#region Private Poperties

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
		/// Initializes a new instance of the <see cref="NotificationDocumentClient"/> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="provider">The provider.</param>
		public NotificationDocumentClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider) : base(client)
		{
			client.BaseAddress = new Uri(AppSettings.NotificationDocumentURL);
			_client = client;
			_configuration = configuration;
			_provider = provider;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Downloads the document.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<DocumentResponse> DownloadDocument(DownloadDocumentRequest request)
		{
			DocumentResponse result = new DocumentResponse();
			var requestUri = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Document/messagedocument/download/"));
			result = await PostAsync<DocumentResponse>(requestUri, CreateHttpContent(request));
			return result;
		}

		/// <summary>
		/// Uploads the file.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public async Task<UploadAttachmentResponse> UploadFile(UploadAttachmentRequestViewModel input)
		{
			UploadAttachmentRequest request = new UploadAttachmentRequest()
			{
				DocumentStream = Convert.FromBase64String(input.FileBase64String),
				FileName = input.FileName,
				Sequence = input.Sequence,
				LoginUserName = input.LoginUserName
			};

			Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Document/messagedocument/upload"));
			UploadAttachmentResponse response = await PostAsync<UploadAttachmentResponse>(requestUrl, CreateHttpContent(request));
			return response;
		}

		/// <summary>
		/// Deletes the document.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public async Task<DocumentResponse> DeleteDocument(DownloadDocumentRequest request)
		{
			DocumentResponse result = new DocumentResponse();
			var requestUri = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/Document/messagedocument/deletedocument/"));
			result = await PostAsync<DocumentResponse>(requestUri, CreateHttpContent(request));
			return result;
		}

		#endregion
	}
}
