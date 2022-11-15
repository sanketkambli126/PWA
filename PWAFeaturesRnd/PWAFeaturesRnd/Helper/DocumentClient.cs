using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// Document client
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class DocumentClient : BaseHttpClient
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
        /// Initializes a new instance of the <see cref="DocumentClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public DocumentClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider)
            : base(client, true)
        {
            client.BaseAddress = new Uri(AppSettings.DocumentApiUrl);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        /// <summary>
        /// Posts the get vessel picture.
        /// </summary>
        /// <param name="documentDetails">The list of document details.</param>
        /// <returns>
        /// Task of stream object
        /// </returns>
        public async Task<Stream> PostGetVesselPicture(List<DocumentDetail> documentDetails)
        {
            if (documentDetails != null && documentDetails.Any())
            {
                CloudDocumentDownloadRequest request = new CloudDocumentDownloadRequest();
                request.Identifier = documentDetails.FirstOrDefault().EttId;
                request.DocumentCategory = DocumentCategory.Vessel;
                request.DocumentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(documentDetails.FirstOrDefault().CloudFileName)).FirstOrDefault();
                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/document/GetCloudDocument"));
                Stream response = await PostFetchStream(requestUrl.ToString(), CreateHttpContent(request));

                return response;
            }
            return null;
        }

        /// <summary>
        /// Downloads the document
        /// </summary>
        /// <param name="request">The document details.</param>
        /// <returns>
        /// Task of stream object
        /// </returns>
        public async Task<Stream> DownloadDocument(CloudDocumentDownloadRequest request)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/document/GetCloudDocument"));
            Stream response = await PostFetchStream(requestUrl.ToString(), CreateHttpContent(request));
            return response;
        }

        /// <summary>
        /// Downloads Invoice document
        /// </summary>
        /// <param name="docId">The document id.</param>
        /// <returns>
        /// Task of DownloadResponseViewModel object
        /// </returns>
        public async Task<DownloadResponseViewModel> DownloadInvoiceDocument(string docId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/document/GetInvoiceDocument/" + docId));
            DownloadResponseViewModel response = await DownloadResponse(requestUrl.ToString(), CreateHttpContent(docId));
            return response;
        }

        /// <summary>
        /// Downloads the crew image.
        /// </summary>
        /// <param name="crewId">The crew identifier.</param>
        /// <returns>
        /// Task of stream object
        /// </returns>
        public async Task<Stream> DownloadCrewImage(string crewId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/document/GetCrewImage/" + crewId));
            Stream response = await PostFetchStream(requestUrl.ToString(), CreateHttpContent(crewId));
            return response;
        }

        /// <summary>
        /// Downloads the report and log history.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<Stream> DownloadReportAndLogHistory(ReportDownloadHistoryRequest request)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/document/DownloadReportAndLogHistory"));
            Stream response = await PostFetchStream(requestUrl.ToString(), CreateHttpContent(request));
            return response;
        }

        /// <summary>
        /// Downloads the company logo image.
        /// </summary>
        /// <param name="cmpId">The CMP identifier.</param>
        /// <returns></returns>
        public async Task<Stream> DownloadCompanyLogoImage(string cmpId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "api/document/GetCompanyMaintainerLogo/" + cmpId));
            Stream response = await PostFetchStream(requestUrl.ToString(), CreateHttpContent(cmpId));
            return response;
        }
    }
}
