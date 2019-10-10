namespace GoogleSearch.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// The HttpClientService interface.
    /// </summary>
    public interface IHttpClientService
    {
        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="baseAddress">
        /// The base address.
        /// </param>
        /// <param name="requestUri">
        /// The request uri.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<HttpResponseMessage> GetAsync(string baseAddress,string requestUri);
    }
}
