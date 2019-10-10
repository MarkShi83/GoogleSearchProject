namespace GoogleSearch.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoogleSearch.Models;

    /// <summary>
    /// The GoogleSearchService interface.
    /// </summary>
    public interface IGoogleSearchService
    {
        /// <summary>
        /// The search async.
        /// </summary>
        /// <param name="searchRequest">
        /// The search request.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IEnumerable<SearchResult>> SearchAsync(SearchRequest searchRequest);
    }
}
