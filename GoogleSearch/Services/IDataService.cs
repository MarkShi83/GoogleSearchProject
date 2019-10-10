namespace GoogleSearch.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoogleSearch.Models;

    /// <summary>
    /// The DataService interface.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// The save async.
        /// </summary>
        /// <param name="searchResults">
        /// The search results.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> SaveAsync(IEnumerable<SearchResult> searchResults);

        /// <summary>
        /// The save async.
        /// </summary>
        /// <param name="searchResults">
        /// The search results.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> SaveAllAsync(IEnumerable<SearchResult> searchResults);

        /// <summary>
        /// The get all async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IEnumerable<SearchResult>> GetAsync();

        /// <summary>
        /// The get all async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IEnumerable<SearchResult>> GetAllAsync();
    }
}
