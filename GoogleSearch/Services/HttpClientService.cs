namespace GoogleSearch.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// The google search service.
    /// </summary>
    public class HttpClientService : IHttpClientService
    {
        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="baseAddress">
        /// The base Address.
        /// </param>
        /// <param name="requestUri">
        /// The request uri.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> GetAsync(string baseAddress, string requestUri)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            return await httpClient.GetAsync(requestUri);
        }

    }
}