namespace GoogleSearch.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoogleSearch.Models;

    /// <summary>
    /// The google search service.
    /// </summary>
    public class GoogleSearchService : IGoogleSearchService
    {
        /// <summary>
        /// The _http client service.
        /// </summary>
        private readonly IHttpClientService _httpClientService;

        /// <summary>
        /// The _analyse service.
        /// </summary>
        private readonly IAnalyseService _analyseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleSearchService"/> class.
        /// </summary>
        /// <param name="httpClientService">
        /// The http client service.
        /// </param>
        /// <param name="analyseService">
        /// The analyse Service.
        /// </param>
        public GoogleSearchService(IHttpClientService httpClientService, IAnalyseService analyseService)
        {
            _httpClientService = httpClientService;
            _analyseService = analyseService;
        }

        /// <summary>
        /// The search async.
        /// </summary>
        /// <param name="searchRequest">
        /// The search request.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public async Task<IEnumerable<SearchResult>> SearchAsync(SearchRequest searchRequest)
        {
            if (!IsValidRequest(searchRequest))
            {
                return new List<SearchResult>();
            }
            var q = searchRequest.Keywords.ToLowerInvariant().Replace(" ", "+");
            var start = searchRequest.PageSize * searchRequest.Page;
            var num = searchRequest.PageSize <= 0 || searchRequest.PageSize > 100 ? 100 : searchRequest.PageSize;
            var result = string.Empty;

            var response = await _httpClientService.GetAsync(searchRequest.BaseAddress, $"/search?q={q}&num={num}&start={start}");

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            var searchResults = new List<SearchResult>();

            switch (searchRequest.Type)
            {
                case ResultType.Ad:
                    searchResults.AddRange(_analyseService.FindAdResults(result, searchRequest.Keywords));
                    break;
                case ResultType.Normal:
                    searchResults.AddRange(_analyseService.FindNormalResults(result, searchRequest.Keywords));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return searchResults;
        }

        /// <summary>
        /// The is valid request.
        /// </summary>
        /// <param name="searchRequest">
        /// The search request.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsValidRequest(SearchRequest searchRequest)
        {
            var result = !string.IsNullOrEmpty(searchRequest.Keywords);

            if (string.IsNullOrEmpty(searchRequest.BaseAddress))
            {
                result = false;
            }

            return result;
        }
    }
}