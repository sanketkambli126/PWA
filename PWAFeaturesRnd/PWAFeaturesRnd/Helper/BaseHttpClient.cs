using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Services;
using PWAFeaturesRnd.ViewModels.Shared;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// Base Http Client
    /// </summary>
    public class BaseHttpClient
    {
        /// <summary>
        /// The page request constant
        /// </summary>
        private const string _pageRequestConstant = "pageRequest";

        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The access token
        /// </summary>
        public string AccessToken;

        /// <summary>
        /// The session
        /// </summary>
        private readonly ISession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHttpClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public BaseHttpClient(HttpClient httpClient, bool isNewHandler = false, IHttpContextAccessor httpContextAccessor = null)
        {
            if (httpContextAccessor != null)
            {
                _session = httpContextAccessor.HttpContext.Session;
            }
            if (isNewHandler)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, errors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                _httpClient = new HttpClient(clientHandler);
            }
            else
            {
                _httpClient = httpClient;
            }
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl">The request URL.</param>		
        /// <returns></returns>
        protected async Task<T> GetAsync<T>(Uri requestUrl)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            return await HandleResponse<T>(response);
        }

        /// <summary>
        /// Handles the response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                if(_session != null)
                {
                    if (CommonUtil.GetSessionObject<bool>(_session, Constants.DemoMode))
                    {
                        var list = CommonUtil.GetSessionObject<Dictionary<string, string>>(_session, "DemoValueList");
                        data = CommonUtil.ReplaceDemoData(data, list);
                    }
                }
                return JsonConvert.DeserializeObject<T>(data);
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var data = await response.Content.ReadAsStringAsync();

                Exception businessException = JsonConvert.DeserializeObject<Exception>(data);
                throw businessException;
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden) 
            {
                var data = await response.Content.ReadAsStringAsync();
                Exception exception = JsonConvert.DeserializeObject<Exception>(data);
                BusinessException businessException = new BusinessException(exception.Message);
                throw businessException;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        protected async Task<T> PostAsync<T>(Uri requestUrl, HttpContent content)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            AddAuthorizationHeader();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), content);

            return await HandleResponse<T>(response);
        }

        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        protected async Task<T> PutAsync<T>(Uri requestUrl, HttpContent content)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            AddAuthorizationHeader();
            var response = await _httpClient.PutAsync(requestUrl.ToString(), content);

            return await HandleResponse<T>(response);
        }

        /// <summary>
        /// Posts the asynchronous automatic paged.
        /// To be used only when the paged data is to be loaded at once.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        protected async Task<List<T>> PostAsyncAutoPaged<T>(Uri requestUrl, Dictionary<string, object> parameters, int pageSize)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            AddAuthorizationHeader();

            PagedRequest pageRequest = new PagedRequest();
            pageRequest.PageNumber = 1;
            pageRequest.PageSize = pageSize;

            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            parameters.Add(_pageRequestConstant, pageRequest);

            var result = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent(parameters));
            PagedResponse<List<T>> response = await HandleResponse<PagedResponse<List<T>>>(result);

            List<T> data = new List<T>();

            if (response.TotalPages > 1)
            {
                data.AddRange(response.Result);
                for (int i = 2; i <= response.TotalPages; i++)
                {
                    ((PagedRequest)parameters[_pageRequestConstant]).PageNumber = i;

                    result = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent(parameters));
                    response = await HandleResponse<PagedResponse<List<T>>>(result);

                    if (response.Result != null)
                    {
                        data.AddRange(response.Result);
                    }
                }
            }
            else
            {
                if (response.Result != null)
                {
                    data.AddRange(response.Result);
                }
            }

            return data;
        }

        /// <summary>
        /// Adds the authorization header.
        /// </summary>
        private void AddAuthorizationHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        }

        /// <summary>
        /// Adds the headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        private void addHeaders(List<HttpHeader> headers)
        {
            foreach (HttpHeader header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Creates the request URI.
        /// </summary>
        /// <param name="baseEndpoint">The base endpoint.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns></returns>
        public Uri CreateRequestUri(Uri baseEndpoint, string relativePath, string queryString = "")
        {
            var endpoint = new Uri(baseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Creates the content of the HTTP.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        protected HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="content">The content.</param>
        /// <returns>Stream object</returns>
        public async Task<Stream> PostFetchStream(string url, HttpContent content)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            AddAuthorizationHeader();
            var response = await _httpClient.PostAsync(new Uri(url), content);

            if (response.IsSuccessStatusCode)
            {
                var Stream = await response.Content.ReadAsStreamAsync();

                if (Stream == null) return null;
                return Stream;
            }

            //TODO: to be added after the try catch mechanism
            //else if (response.StatusCode == HttpStatusCode.InternalServerError)
            //{
            //    var data = await response.Content.ReadAsStringAsync();

            //    Exception businessException = JsonConvert.DeserializeObject<Exception>(data);
            //    throw businessException;
            //}
            //else if (response.StatusCode == HttpStatusCode.Forbidden)
            //{
            //    var data = await response.Content.ReadAsStringAsync();
            //    Exception exception = JsonConvert.DeserializeObject<Exception>(data);
            //    BusinessException businessException = new BusinessException(exception.Message);
            //    throw businessException;
            //}
            //else if (response.StatusCode == HttpStatusCode.Unauthorized)
            //{
            //    throw new UnauthorizedAccessException();
            //}
            //else
            //{
            //    try
            //    {
            //        response.EnsureSuccessStatusCode();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
            return null;
        }

        /// <summary>
        /// Downloads the response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public async Task<DownloadResponseViewModel> DownloadResponse(string url, HttpContent content)
        {
            DownloadResponseViewModel result = new DownloadResponseViewModel();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            AddAuthorizationHeader();
            var response = await _httpClient.PostAsync(new Uri(url), content);
            if (response.IsSuccessStatusCode)
            {
                result.DocumentStream = await response.Content.ReadAsStreamAsync();
                result.FileName = response.Content.Headers.ContentDisposition.FileName;
                result.MediaType = response.Content.Headers.ContentType.MediaType;
                if (result.DocumentStream == null) return null;
                return result;
            }
            return null;
        }
    }
}
